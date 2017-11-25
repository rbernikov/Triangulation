using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Triangulation.Tree;
using Triangulation.Zones;

namespace Triangulation.MapReduce
{
    public class CommonBoundary
    {
        public Dictionary<KeyValuePair<int, int>, IList<Point>> Execute(Node value)
        {
            var mapResult = new List<KeyValuePair<Point, int>>();

            value.Traverse(node => mapResult.AddRange(Map(node)));

            var reduceSource = mapResult.GroupBy(
                pair => pair.Key,
                (key, values) => new KeyValuePair<Point, IEnumerable<int>>(key, values.Select(v => v.Value)));

            var result = new Dictionary<KeyValuePair<int, int>, IList<Point>>();

            foreach (var reduce in reduceSource)
            {
                if (reduce.Value.Count() < 2) continue;

                var info = Reduce(reduce);
                if (result.ContainsKey(info.Key)) result[info.Key].Add(info.Value);
                else result.Add(info.Key, new List<Point> { info.Value });
            }

            return result;
        }

        public IEnumerable<KeyValuePair<Point, int>> Map(Node value)
        {
            return from point in value.Boundary
                   select new KeyValuePair<Point, int>(point, value.Label);
        }

        public KeyValuePair<KeyValuePair<int, int>, Point> Reduce(KeyValuePair<Point, IEnumerable<int>> value)
        {
            var list = value.Value.ToList();

            var key = new KeyValuePair<int, int>(list[0], list[1]);
            return new KeyValuePair<KeyValuePair<int, int>, Point>(key, value.Key);
        }
    }
}