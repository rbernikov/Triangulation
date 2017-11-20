using System.Collections.Generic;
using System.Drawing;
using Triangulation.Geometry;

namespace Triangulation.Controls.Layers
{
    public class GraphLayer : BaseLayer
    {
        private readonly IList<Vertex> _vertices;

        public GraphLayer(IList<Vertex> vertices)
        {
            _vertices = vertices;
        }
        
        public override void Render(Graphics graphics)
        {
            if (_vertices == null || _vertices.Count == 0) return;

            foreach (var vertex in _vertices)
            {
                var rect = new RectangleF((float)(vertex.X - 2f), (float)(vertex.Y - 2f), 2, 2);

                graphics.FillEllipse(Brushes.Blue, rect);
            }
        }
    }
}