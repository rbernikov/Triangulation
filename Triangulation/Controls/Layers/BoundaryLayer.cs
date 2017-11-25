using System.Collections.Generic;
using System.Drawing;
using Triangulation.Tree;
using Triangulation.Zones;

namespace Triangulation.Controls.Layers
{
    public class BoundaryLayer : BaseLayer
    {
        private Node _root;
        private int _selected;

        public Node Root
        {
            get => _root;
            set
            {
                if (value == null) Enabled = false;

                _root = value;
                Parent.Invalidate();
            }
        }

        public int Selected
        {
            get => _selected;
            set
            {
                if (value < 0) return;
                if (value == _selected) return;

                _selected = value;

                Parent.Invalidate();
            }
        }

        public override void Render(Graphics graphics)
        {
            if (_root == null) return;

            _root.Traverse(node =>
            {
                InternalRender(graphics, Brushes.Green, node.Boundary);

                if (_selected == node.Label) InternalRender(graphics, Brushes.Red, node.Boundary);
            });
        }

        private void InternalRender(Graphics graphics, Brush brush, IEnumerable<Point> boundary)
        {
            if (boundary == null) return;

            foreach (var point in boundary)
            {
                var rect = new RectangleF(point.X - 0.5f, point.Y - 0.5f, 1, 1);

                graphics.FillEllipse(brush, rect);
            }
        }
    }
}