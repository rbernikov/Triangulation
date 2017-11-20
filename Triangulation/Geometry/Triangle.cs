using System;

namespace Triangulation.Geometry
{
    public class Triangle
    {
        private double _radiusSquared;

        public Triangle(Vertex v0, Vertex v1, Vertex v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;

            E0 = new Edge(V0, V1);
            E1 = new Edge(V1, V2);
            E2 = new Edge(V2, V0);

            CalcCircumcircle();
        }

        public Vertex V0 { get; }

        public Vertex V1 { get; }

        public Vertex V2 { get; }

        public Edge E0 { get; }

        public Edge E1 { get; }

        public Edge E2 { get; }

        public Vertex Center { get; private set; }

        public double Radius { get; private set; }

        public bool InCircumcircle(Vertex v)
        {
            var dx = Center.X - v.X;
            var dy = Center.Y - v.Y;
            var distSquared = dx * dx + dy * dy;
            return distSquared <= _radiusSquared;
        }

        public bool Contains(Vertex v)
        {
            return V0 == v || V1 == v || V2 == v;
        }

        public bool Contains(Edge e)
        {
            return E0 == e || E1 == e || E2 == e;
        }

        private void CalcCircumcircle()
        {
            var a = V1.X - V0.X;
            var b = V1.Y - V0.Y;
            var c = V2.X - V0.X;
            var d = V2.Y - V0.Y;

            var e = a * (V0.X + V1.X) + b * (V0.Y + V1.Y);
            var f = c * (V0.X + V2.X) + d * (V0.Y + V2.Y);

            var g = 2.0 * (a * (V2.Y - V1.Y) - b * (V2.X - V1.X));

            double dx, dy;

            if (Math.Abs(g) < 1e-6)
            {
                var minx = Math.Min(V0.X, Math.Min(V1.X, V2.X));
                var miny = Math.Min(V0.Y, Math.Min(V1.Y, V2.Y));
                var maxx = Math.Max(V0.X, Math.Max(V1.X, V2.X));
                var maxy = Math.Max(V0.Y, Math.Max(V1.Y, V2.Y));

                Center = new Vertex((minx + maxx) / 2.0, (miny + maxy) / 2.0);

                dx = Center.X - minx;
                dy = Center.Y - miny;
            }
            else
            {
                var cx = (d * e - b * f) / g;
                var cy = (a * f - c * e) / g;

                Center = new Vertex(cx, cy);

                dx = Center.X - V0.X;
                dy = Center.Y - V0.Y;
            }

            _radiusSquared = dx * dx + dy * dy;
            Radius = Math.Sqrt(_radiusSquared);
        }
    }
}