using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Triangulation.Collection;
using Triangulation.Tree;

namespace Triangulation.MapReduce
{
    public class CommonBoundary
    {
        public Dictionary<Pair<int, int>, IList<Point>> Execute(Node value)
        {
            var mapResult = new List<Pair<Point, int>>();

            value.Traverse(node => mapResult.AddRange(Map(node)));

            var reduceSource = mapResult.GroupBy(
                pair => pair.Key,
                (key, values) => new Pair<Point, IEnumerable<int>>(key, values.Select(v => v.Value)));

            var result = new Dictionary<Pair<int, int>, IList<Point>>();

            foreach (var reduce in reduceSource)
            {
                if (reduce.Value.Count() < 2) continue;

                var info = Reduce(reduce);
                if (result.ContainsKey(info.Key)) result[info.Key].Add(info.Value);
                else result.Add(info.Key, new List<Point> { info.Value });
            }

            return result;
        }

        public IEnumerable<Pair<Point, int>> Map(Node value)
        {
            return from point in value.Boundary
                   select new Pair<Point, int>(point, value.Label);
        }

        public Pair<Pair<int, int>, Point> Reduce(Pair<Point, IEnumerable<int>> value)
        {
            var list = value.Value.ToList();

            var key = new Pair<int, int>(list[0], list[1]);
            return new Pair<Pair<int, int>, Point>(key, value.Key);
        }
    }
}