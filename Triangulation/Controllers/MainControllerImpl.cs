using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Triangulation.Geometry;
using Triangulation.Json;
using Triangulation.Views;
using Triangulation.Grd;
using Triangulation.Loader;
using Triangulation.MapReduce;
using Triangulation.Tree;
using Triangulation.Union;
using Triangulation.Utils;
using Triangulation.Zones;

namespace Triangulation.Controllers
{
    internal class MainControllerImpl : IMainController
    {
        private GrdFile _grd;
        private GrdFile _map;

        private Node _root;

        private readonly IMainView _view;
        private readonly Delaunay _delaunay;
        private readonly Watershed _watershed;
        
        private readonly List<Vertex> _vertices;

        private readonly Dictionary<int, ZoneInfo> _zones;

        public MainControllerImpl(IMainView view)
        {
            _view = view;
            _delaunay = new Delaunay();
            _watershed = new Watershed(_delaunay);

            _root = new Node("0");
            _vertices = new List<Vertex>();
            _zones = new Dictionary<int, ZoneInfo>();
        }

        #region IController implements

        public void OnMapLoad(string fileName)
        {
            Preconditions.CheckNotNullOrEmpty(fileName, "fileName");

            _map = GrdFile.Read(fileName);
        }

        public void OnLoad(string fileName)
        {
            Preconditions.CheckNotNullOrEmpty(fileName, "fileName");

            _view.OnShowProgress(true);

            _root.Clear();
            _zones.Clear();
            _vertices.Clear();

            var loader = LoaderFactory.Create(LoaderFactory.Excel);
            if (!Preconditions.CheckNotNull(loader))
            {
                _view.OnShowError("Ошибка при загрузке файла.");
                return;
            }

            var thread = new Thread(() =>
            {
                _root = loader.Load(fileName, _zones);
                _root.Traverse(AddVertices);

                CreateSquare();

                _view.OnGraphLoaded(_root, _vertices);
                _view.OnUpdateView();
                _view.OnShowProgress(false);
            })
            { IsBackground = true };

            thread.Start();
        }

        public void OnSaveZones(string fileName)
        {
            Preconditions.CheckNotNullOrEmpty(fileName, "fileName");

            if (_watershed.Edges.Count == 0) return;

            _grd.Write(fileName);
        }

        public void OnWatershedExtract()
        {
            if (_vertices.Count == 0) return;

            _view.OnShowProgress(true);

            var thread = new Thread(() =>
            {
                _watershed.Extract(_vertices);
                FillZones(_watershed.Edges);

                _view.OnWatershedExtracted(_watershed.Edges);
                _view.OnShowProgress(false);
            })
            { IsBackground = true };

            thread.Start();
        }

        public void OnBoundaryExtract()
        {
            if (_grd == null) return;

            Zone.ExtractBoundary(_grd, _zones);

            _view.OnBoundaryExtracted(_zones);
        }

        public void OnZoneUnion(int method, float percent)
        {
            if (!Preconditions.CheckNotNull(_map))
            {
                _view.OnShowError("Не загружена карта затоплений.");
                return;
            }

            if (!Preconditions.CheckNotNull(_grd))
            {
                _view.OnShowError("Не извлечны зоны.");
                return;
            }

            var mapReduce = new CommonBoundary();
            var execute = mapReduce.Execute(_zones.Values);

            var union = UnionFactory.Create(method);
            if (!Preconditions.CheckNotNull(union))
            {
                _view.OnShowError("Не найден метод объединения зон.");
                return;
            }

            var dictionary = union.Union(_map, execute, percent);
            foreach (var zone in dictionary.OrderBy(pair => pair.Key, new Comparer()))
            {
                foreach (var point in zone.Value)
                {
                    _grd.Data[point.X, point.Y] = zone.Key.Value;
                }

                var first = zone.Key.Key;
                var second = zone.Key.Value;
                var points = zone.Value;

                if (points.Count < 2) continue;

                Zone.FillZone(_grd.Data, points[1], second, first);
            }
        }

        #endregion

        #region Private methods

        private void CreateSquare()
        {
            var minX = (int)_vertices.Min(x => x.X) - 10;
            var minY = (int)_vertices.Min(x => x.Y) - 10;
            var maxX = (int)_vertices.Max(x => x.X) + 10;
            var maxY = (int)_vertices.Max(x => x.Y) + 10;
            
            for (int i = minX; i < maxX; i += 10)
            {
                _vertices.Add(new Vertex(i, minY));
                _vertices.Add(new Vertex(i, maxY));
            }

            for (int i = minY; i < maxY; i += 10)
            {
                _vertices.Add(new Vertex(minX, i));
                _vertices.Add(new Vertex(maxX, i));
            }
        }

        private void AddVertices(Node root)
        {
            _vertices.AddRange(root.Vertices);
        }

        private void FillZones(IEnumerable<Edge> edges)
        {
            _grd = new GrdFile();

            foreach (var edge in edges)
            {
                GrdGraphics.DrawLine(_grd, (int)edge.V0.X, (int)edge.V0.Y, (int)edge.V1.X, (int)edge.V1.Y);
            }

            foreach (var info in _zones.Values)
            {
                FillZone(_grd.Data, info.Point, info.Id);
            }
        }

        private void FillZone(float[,] grd, Vertex vertex, int zone)
        {
            var x = (int)vertex.X;
            var y = (int)vertex.Y;

            if (grd[x, y] != 0) return;

            var queue = new Queue<Vertex>();
            queue.Enqueue(vertex);

            while (queue.Count != 0)
            {
                var v = queue.Dequeue();
                x = (int)v.X;
                y = (int)v.Y;

                if (x - 1 >= 0)
                {
                    if (grd[x - 1, y] == 0) { grd[x - 1, y] = zone; queue.Enqueue(new Vertex(x - 1, y)); }
                }
                if (y + 1 < 944)
                {
                    if (grd[x, y + 1] == 0) { grd[x, y + 1] = zone; queue.Enqueue(new Vertex(x, y + 1)); }
                }
                if (x + 1 < 944)
                {
                    if (grd[x + 1, y] == 0) { grd[x + 1, y] = zone; queue.Enqueue(new Vertex(x + 1, y)); }
                }
                if (y - 1 >= 0)
                {
                    if (grd[x, y - 1] == 0) { grd[x, y - 1] = zone; queue.Enqueue(new Vertex(x, y - 1)); }
                }
            }
        }

        private class Comparer : IComparer<KeyValuePair<int, int>>
        {
            public int Compare(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
            {
                return x.Key - y.Key + x.Value - y.Value;
            }
        }

        #endregion
    }
}