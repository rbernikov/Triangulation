using System.Collections.Generic;
using System.Drawing;
using Triangulation.Geometry;
using Triangulation.Tree;

namespace Triangulation.Controls.Layers
{
    public class GraphLayer : BaseLayer
    {
        private IList<Vertex> _selected;
        private readonly IList<Vertex> _vertices;

        public GraphLayer(List<Vertex> vertices)
        {
            _vertices = vertices;
            _selected = new List<Vertex>();
        }

        public IList<Vertex> Selected
        {
            get => _selected;
            set
            {
                _selected = value;

                Parent.Invalidate();
            }
        }

        public override void Render(Graphics graphics)
        {
            if (_vertices == null || _vertices.Count == 0) return;

            InternalRender(graphics, Brushes.Blue, _vertices);

            if (_selected != null)
            {
                InternalRender(graphics, Brushes.Red, _selected);
            }
        }

        private void InternalRender(Graphics graphics, Brush brush, IEnumerable<Vertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                var rect = new RectangleF((float) (vertex.X - 2f), (float) (vertex.Y - 2f), 2, 2);

                graphics.FillEllipse(brush, rect);
            }
        }
    }
}