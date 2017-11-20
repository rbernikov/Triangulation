using System.Collections.Generic;
using System.Drawing;
using Triangulation.Geometry;

namespace Triangulation.Controls.Layers
{
    public class WatershedLayer : BaseLayer
    {
        private readonly IList<Edge> _edges;

        public WatershedLayer(IList<Edge> edges)
        {
            _edges = edges;
        }
        
        public override void Render(Graphics graphics)
        {
            if (_edges == null || _edges.Count == 0) return;

            var pen = new Pen(Color.Green, 1f);

            foreach (var edge in _edges)
            {
                graphics.DrawLine(pen, new PointF((float)edge.V0.X, (float)edge.V0.Y),
                    new PointF((float)edge.V1.X, (float)edge.V1.Y));
            }
        }
    }
}