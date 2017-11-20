using System;
using System.Collections.Generic;
using System.Drawing;
using Triangulation.Grd;
using Triangulation.Zones;

namespace Triangulation.Union
{
    /// <summary>
    /// Объединение точек методом определения доли мокрых точек
    /// </summary>
    public class WetUnion: AUnion
    {
        /// <summary>
        /// Условие объединения зон
        /// </summary>
        /// <param name="map">Карта затопления</param>
        /// <param name="points">Общая граница двух зон</param>
        /// <param name="arg">Процент мокрых точек</param>
        /// <returns></returns>
        public override bool Condition(GrdFile map, IEnumerable<Point> points, float arg)
        {
            return new Random().NextDouble() > arg;

            Preconditions.CheckNotNull(map, "map");
            Preconditions.CheckNotNull(points, "points");

            var x = 0;
            var y = 0;

            foreach (var p in points)
            {
                y++;
                if (map.Data[p.X, p.Y] >= 0.5) // высота, с которой считаем что точка мокрая
                {
                    x++;
                }
            }

            return x / (double)y > arg;
        }
    }
}