using System.Collections.Generic;
using Triangulation.Geometry;
using Triangulation.Zones;

namespace Triangulation.Loader
{
    public interface ILoader
    {
        void Load(string filename, List<Vertex> vertices, Dictionary<int, ZoneInfo> zones);
    }
}