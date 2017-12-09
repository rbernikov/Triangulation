using System.Collections.Generic;
using System.Linq;
using Triangulation.Collection;
using Triangulation.Geometry;

namespace Triangulation.MapReduce
{
    public class CommonTriangles
    {
        public Dictionary<Edge, IList<Triangle>> Execute(IEnumerable<Triangle> value)
        {
            var mapResult = value.SelectMany(Map);

            var reduceResult = mapResult.GroupBy(
                pair => pair.Key,
                (key, values) => new Pair<Edge, IEnumerable<Triangle>>(key, values.Select(v => v.Value)));

            var result = new Dictionary<Edge, IList<Triangle>>();

            foreach (var reduce in reduceResult)
            {
                if (reduce.Value.Count() < 2) continue;

                var info = Reduce(reduce);
                result.Add(info.Key, info.Value);
            }

            return result;
        }

        public IEnumerable<Pair<Edge, Triangle>> Map(Triangle value)
        {
            return new List<Pair<Edge, Triangle>>
            {
                new Pair<Edge, Triangle>(value.E0, value),
                new Pair<Edge, Triangle>(value.E1, value),
                new Pair<Edge, Triangle>(value.E2, value)
            };
        }

        public Pair<Edge, IList<Triangle>> Reduce(Pair<Edge, IEnumerable<Triangle>> value)
        {
            return new Pair<Edge, IList<Triangle>>(value.Key, value.Value.ToList());
        }
    }
}