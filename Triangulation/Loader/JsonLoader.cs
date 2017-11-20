using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Triangulation.Geometry;
using Triangulation.Json;
using Triangulation.Zones;

namespace Triangulation.Loader
{
    public class JsonLoader:ILoader
    {
        public void Load(string filename, List<Vertex> vertices, Dictionary<int, ZoneInfo> zones)
        {
            var json = JObject.Parse(File.ReadAllText(filename));
            var list = json["nodes"].Children().ToList();

            foreach (var token in list)
            {
                var node = JsonConvert.DeserializeObject<JNode>(token.ToString());

                var count = node.X.Count;
                if (count != node.Y.Count) continue;

                var point = new Vertex(node.X[1], node.Y[1], node.Id);
                var info = new ZoneInfo(node.Id, node.Group, point);
                zones.Add(node.Id, info);

                for (int i = 0; i < count; i++)
                {
                    var x = node.X[i];
                    var y = node.Y[i];

                    var vertex = new Vertex(x, y, node.Id);

                    vertices.Add(vertex);
                }
            }
        }
    }
}