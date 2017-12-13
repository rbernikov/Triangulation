using System.Collections.Generic;
using System.Drawing;
using Triangulation.Collection;
using Triangulation.Grd;

namespace Triangulation.Union
{
    public abstract class AUnion
    {
        /// <summary>
        /// Объединение зон
        /// </summary>
        /// <param name="map">Карта затопления</param>
        /// <param name="zones">Зоны и их общие точки</param>
        /// <param name="percent">Условие</param>
        /// <returns>Зоны, которые нужно объединить</returns>
        public Dictionary<Pair<int, int>, IList<Point>> Union(GrdFile map, Dictionary<Pair<int, int>, IList<Point>> zones, float percent)
        {
            Preconditions.CheckNotNull(map, "map");
            Preconditions.CheckNotNull(zones, "zones");

            var union = new Dictionary<Pair<int, int>, IList<Point>>();

            foreach (var zone in zones)
            {
                var first = zone.Key.Key;
                var second = zone.Key.Value;
                var points = zone.Value;

                if (Condition(map, points, percent))
                {
                    union.Add(new Pair<int, int>(first, second), points);
                }
            }

            return union;
        }

        /// <summary>
        /// Условие объединения зон
        /// </summary>
        /// <param name="map">Карта затопления</param>
        /// <param name="points">Общая граница двух зон</param>
        /// <param name="arg">Параметр объединения</param>
        /// <returns></returns>
        public abstract bool Condition(GrdFile map, IEnumerable<Point> points, float arg);
    }
}