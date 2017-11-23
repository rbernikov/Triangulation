using System.Collections.Generic;
using Triangulation.Geometry;
using Triangulation.Tree;
using Triangulation.Zones;

namespace Triangulation.Loader
{
    public interface ILoader
    {
        Node Load(string filename, Dictionary<int, ZoneInfo> zones);
    }
}