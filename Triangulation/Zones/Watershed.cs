using System;
using System.Collections.Generic;
using System.Linq;
using Triangulation.Geometry;
using Triangulation.MapReduce;

namespace Triangulation.Zones
{
    public class Watershed
    {
        private const int Junction = 0;

        private readonly List<Edge> _edges;
        private readonly List<Edge> _crust;
        private readonly Delaunay _delaunay;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Watershed"/>
        /// </summary>
        /// <param name="delaunay"></param>
        public Watershed(Delaunay delaunay)
        {
            _delaunay = delaunay;
            _edges = new List<Edge>();
            _crust = new List<Edge>();
        }

        /// <summary>
        /// Возвращает список ребер, образующие зоны
        /// </summary>
        public List<Edge> Edges => _edges;

        /// <summary>
        /// Возвращает список ребер, образующие русла
        /// </summary>
        public List<Edge> Crust => _crust;

        /// <summary>
        /// Извлекает зоны
        /// </summary>
        /// <param name="vertices">Список точек</param>
        public void Extract(List<Vertex> vertices)
        {
            if (_edges.Count > 0) _edges.Clear();

            _delaunay.Triangulate(vertices);

            var mapReduce = new CommonTriangles();
            var triangles = mapReduce.Execute(_delaunay.Triangles);

            foreach (var edge in _delaunay.Edges)
            {
                if (!triangles.ContainsKey(edge)) continue;
                var list = triangles[edge];

                var d1 = edge.V0;
                var d2 = edge.V1;
                var v1 = list[0].Center;
                var v2 = list[1].Center;

                if (Orientation(d1, d2, v1) != 1)
                {
                    v1 = list[1].Center;
                    v2 = list[0].Center;
                }

                var h = InCircle(d1, d2, v1, v2);
                if (h < 0 && (d1.Id == d2.Id || d1.Id == Junction || d2.Id == Junction))
                {
                    _crust.Add(new Edge(d1, d2));
                }
                else if (h > 0 && d1.Id != d2.Id)
                {
                    _edges.Add(new Edge(v1, v2));
                }
            }
        }

        private List<Triangle> FindTriangles(List<Triangle> triangles, Edge edge)
        {
            return triangles.Where(triangle => triangle.Contains(edge)).ToList();
        }

        private double InCircle(Vertex d1, Vertex d2, Vertex v1, Vertex v2)
        {
            double det = (v1.X * v1.X + v1.Y * v1.Y) * (d2.Y - v2.Y) * d1.X +
                         (v2.X * v2.X + v2.Y * v2.Y) * (v1.Y - d1.Y) * d2.X +
                         (d1.X * d1.X + d1.Y * d1.Y) * (v2.Y - d2.Y) * v1.X +
                         (d2.X * d2.X + d2.Y * d2.Y) * (d1.Y - v1.Y) * v2.X;
            return det;
        }

        private int Orientation(Vertex v1, Vertex v2, Vertex v3)
        {
            var value = (v2.Y - v1.Y) * (v3.X - v2.X) - (v2.X - v1.X) * (v3.Y - v2.Y);

            if (Math.Abs(value) < 1e-6) return 0; // colinear

            return value > 0 ? 1 : 2; // clock or counterclock wise
        }
    }
}