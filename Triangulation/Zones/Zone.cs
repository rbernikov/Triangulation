using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Triangulation.Grd;
using Triangulation.Tree;

namespace Triangulation.Zones
{
    public class Zone
    {
        /// <summary>
        /// Нахождение границ всех зон
        /// </summary>
        /// <param name="grd">Файл grd с зонами</param>
        /// <param name="root">Дерево русел</param>
        public static void ExtractBoundary(GrdFile grd, Node root)
        {
            Preconditions.CheckNotNull(grd, "grd");
            Preconditions.CheckNotNull(root, "root");

            var zones = new Dictionary<int, List<Point>>();
            for (int j = 0; j < grd.Column; j++)
            {
                for (int i = 0; i < grd.Row; i++)
                {
                    var zone = (int)grd.Data[i, j];

                    if (zone > 0 && !zones.ContainsKey(zone))
                    {
                        var boundary = ExtractBoundary(grd, new Point(i, j - 1));
                        zones.Add(zone, boundary);
                    }
                }
            }

            root.Traverse(node =>
            {
                var label = node.Label;

                if (!zones.ContainsKey(label)) return;

                node.Boundary = zones[label];
            });
        }

        /// <summary>
        /// Нахождение общей границы двух соседних зон
        /// </summary>
        /// <param name="first">Первая зона</param>
        /// <param name="second">Вторая зона</param>
        /// <returns>Список точек общей границы</returns>
        public static IList<Point> CommonBoundary(Node first, Node second)
        {
            Preconditions.CheckNotNull(first, "first");
            Preconditions.CheckNotNull(second, "second");

            return first.Boundary.Intersect(second.Boundary).ToList();
        }

        /// <summary>
        /// Объединение двух соседних зон
        /// </summary>
        /// <param name="grd">Файл grd с зонами</param>
        /// <param name="first">Первая зона</param>
        /// <param name="second">Вторая зона</param>
        public static void Union(GrdFile grd, Node first, Node second)
        {
            Preconditions.CheckNotNull(grd, "grd");
            Preconditions.CheckNotNull(first, "first");
            Preconditions.CheckNotNull(second, "second");

            var boundary = CommonBoundary(first, second);
            if (boundary == null || boundary.Count == 0) return;

            foreach (var point in boundary)
            {
                grd.Data[point.X, point.Y] = second.Label;
            }

            FillZone(grd.Data, boundary[0], second.Label, first.Label);
        }

        private static List<Point> ExtractBoundary(GrdFile grd, Point start)
        {
            int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dy = { 0, -1, -1, -1, 0, 1, 1, 1 };

            var x = start.X;
            var y = start.Y;
            var dir = 7;

            IList<Point> contour = new List<Point> { start };

            do
            {
                for (int i = dir; i < dir + 8; i++)
                {
                    var nx = x + dx[i % 8];
                    var ny = y + dy[i % 8];

                    if (grd.Data[nx, ny] == -1)
                    {
                        contour.Add(new Point(nx, ny));
                        dir = i % 2 == 0 ? (i + 7) % 8 : (i + 6) % 8;
                        x = nx;
                        y = ny;
                        break;
                    }
                }
            } while (!(contour.Last() == contour.First()));

            contour.RemoveAt(0);
            return contour.ToList();
        }

        public static void FillZone(float[,] grd, Point start, int from, int to)
        {
            var x = start.X;
            var y = start.Y;

            if (grd[x, y] != from) return;

            var queue = new Queue<Point>();
            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                var p = queue.Dequeue();
                x = p.X;
                y = p.Y;

                if (x - 1 >= 0)
                {
                    if (grd[x - 1, y] == from) { grd[x - 1, y] = to; queue.Enqueue(new Point(x - 1, y)); }
                }
                if (y + 1 < 944)
                {
                    if (grd[x, y + 1] == from) { grd[x, y + 1] = to; queue.Enqueue(new Point(x, y + 1)); }
                }
                if (x + 1 < 944)
                {
                    if (grd[x + 1, y] == from) { grd[x + 1, y] = to; queue.Enqueue(new Point(x + 1, y)); }
                }
                if (y - 1 >= 0)
                {
                    if (grd[x, y - 1] == from) { grd[x, y - 1] = to; queue.Enqueue(new Point(x, y - 1)); }
                }
            }
        }
    }
}