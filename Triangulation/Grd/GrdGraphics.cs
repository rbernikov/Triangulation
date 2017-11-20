using System;

namespace Triangulation.Grd
{
    public class GrdGraphics
    {
        public static void DrawLine(GrdFile grd, int x1, int y1, int x2, int y2)
        {
            Preconditions.CheckOutOfRange(x1, 0, 944, "x1");
            Preconditions.CheckOutOfRange(y1, 0, 944, "y1");
            Preconditions.CheckOutOfRange(x2, 0, 944, "x2");
            Preconditions.CheckOutOfRange(y2, 0, 944, "y2");

            int dx = Math.Abs(x2 - x1), sx = x1 < x2 ? 1 : -1;
            int dy = Math.Abs(y2 - y1), sy = y1 < y2 ? 1 : -1;
            var err = (dx > dy ? dx : -dy) / 2;
            while (true)
            {
                grd.Data[x1, y1] = -1;
                if (x1 == x2 && y1 == y2) break;

                var e2 = err;
                if (e2 > -dx) { err -= dy; x1 += sx; }
                if (e2 < dy) { err += dx; y1 += sy; }
            }
        }
    }
}