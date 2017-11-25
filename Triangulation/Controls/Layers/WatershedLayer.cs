using System.Collections.Generic;
using System.Drawing;
using Triangulation.Geometry;

namespace Triangulation.Controls.Layers
{
    public class WatershedLayer : BaseLayer
    {
        private List<Edge> _edges;

        public List<Edge> Edges
        {
            get { return _edges; }
            set
            {
                _edges = value;

                Parent.Invalidate();
            }
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