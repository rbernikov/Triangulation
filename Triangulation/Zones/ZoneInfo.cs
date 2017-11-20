using System.Collections.Generic;
using System.Drawing;
using Triangulation.Geometry;

namespace Triangulation.Zones
{
    public class ZoneInfo
    {
        public ZoneInfo(int id, int group, Vertex point)
        {
            Id = id;
            Group = group;
            Point = point;
        }

        public int Id { get; set; }
        public int Group { get; set; }
        public Vertex Point {get; set; }
        public IList<Point> Boundary { get; set; }
    }
}