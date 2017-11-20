using System.Collections.Generic;

namespace Triangulation.Json
{
    public class JNode
    {
        public int Id { get; set; }
        public int Group { get; set; }
        public List<int> X { get; set; }
        public List<int> Y { get; set; }
    }
}