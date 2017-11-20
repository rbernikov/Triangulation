using System.Collections.Generic;
using System.Linq;

namespace Triangulation.Utils
{
    public static class EnumerableEx
    {
        public static IEnumerable<T> SymmetricDifference<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.Except(second).Concat(second.Except(first));
        }
    }
}