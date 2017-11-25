using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Triangulation.Geometry;
using Triangulation.Json;
using Triangulation.Tree;

namespace Triangulation.Loader
{
    public class JsonLoader : ILoader
    {
        public Node Load(string filename)
        {
            var json = JObject.Parse(File.ReadAllText(filename));
            var list = json["nodes"].Children().ToList();

            var root = new Node("0");

            foreach (var token in list)
            {
                var node = JsonConvert.DeserializeObject<JNode>(token.ToString());

                var count = node.X.Count;
                if (count != node.Y.Count) continue;

                for (int i = 0; i < count; i++)
                {
                    var x = node.X[i];
                    var y = node.Y[i];

                    var vertex = new Vertex(x, y, node.Id);

                    root.AddVertex(vertex);
                }
            }

            return root;
        }
    }
}