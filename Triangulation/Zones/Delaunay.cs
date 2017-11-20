using System.Collections.Generic;
using System.Linq;
using Triangulation.Geometry;

namespace Triangulation.Zones
{
    /// <summary>
    /// 
    /// </summary>
    public class Delaunay
    {
        private readonly List<Triangle> _triangles;
        private readonly List<Edge> _edges;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Delaunay"/>
        /// </summary>
        public Delaunay()
        {
            _triangles = new List<Triangle>();
            _edges = new List<Edge>();
        }

        /// <summary>
        /// Возвращает список треугольников триангуляции
        /// </summary>
        public List<Triangle> Triangles => _triangles;

        /// <summary>
        /// Возврвщвет список ребер триангуляции
        /// </summary>
        public List<Edge> Edges => _edges;

        /// <summary>
        /// Выполняет триангуляцию
        /// <para>
        /// Алгоритм можно найти <see href="https://en.wikipedia.org/wiki/Bowyer%E2%80%93Watson_algorithm">здесь</see>
        /// </para>
        /// </summary>
        /// <param name="vertices">Список точек</param>
        public void Triangulate(List<Vertex> vertices)
        {
            if (_triangles.Count != 0)
            {
                _triangles.Clear();
                _edges.Clear();
            }

            var st = CreateSuperTriangle(vertices);
            _triangles.Add(st);

            vertices.ForEach(AddVertex);

            _triangles.RemoveAll(triangle => triangle.Contains(st.V0) || triangle.Contains(st.V1) || triangle.Contains(st.V2));

            _triangles.ForEach(triangle =>
            {
                _edges.Add(new Edge(triangle.V0, triangle.V1));
                _edges.Add(new Edge(triangle.V1, triangle.V2));
                _edges.Add(new Edge(triangle.V2, triangle.V0));
            });
        }

        private Triangle CreateSuperTriangle(IList<Vertex> vertices)
        {
            var minX = vertices[0].Y;
            var minY = vertices[0].Y;
            var maxX = minX;
            var maxY = minY;

            foreach (var vertex in vertices)
            {
                if (vertex.X < minX) minX = vertex.X;
                if (vertex.Y < minY) minY = vertex.Y;
                if (vertex.X > maxX) maxX = vertex.X;
                if (vertex.Y > maxY) maxY = vertex.Y;
            }

            var dx = (maxX - minX) * 10;
            var dy = (maxY - minY) * 10;

            var stv0 = new Vertex(minX - dx, minY - dy * 3);
            var stv1 = new Vertex(minX - dx, maxY + dy);
            var stv2 = new Vertex(maxX + dx * 3, maxY + dy);

            return new Triangle(stv0, stv1, stv2);
        }

        private void AddVertex(Vertex v)
        {
            var edges = new List<Edge>();

            _triangles.RemoveAll(triangle =>
            {
                if (triangle.InCircumcircle(v))
                {
                    edges.Add(new Edge(triangle.V0, triangle.V1));
                    edges.Add(new Edge(triangle.V1, triangle.V2));
                    edges.Add(new Edge(triangle.V2, triangle.V0));
                    return true;
                }
                return false;
            });

            edges = UniqueEdges(edges);

            foreach (var edge in edges)
            {
                _triangles.Add(new Triangle(edge.V0, edge.V1, v));
            }
        }

        private List<Edge> UniqueEdges(IList<Edge> edges)
        {
            var uniqueEdges = new List<Edge>();

            for (var i = 0; i < edges.Count; ++i)
            {
                var edge = edges[i];
                var unique = edges.Where((e, j) => i != j).All(edge2 => edge != edge2);

                if (unique) uniqueEdges.Add(edge);
            }

            return uniqueEdges;
        }
    }
}