using System.Collections.Generic;
using System.Drawing;
using Triangulation.Zones;

namespace Triangulation.Controls.Layers
{
    public class BoundaryLayer : BaseLayer
    {
        private Dictionary<int, ZoneInfo> _zones;
        private int _selected;

        public Dictionary<int, ZoneInfo> Zones
        {
            get => _zones;
            set
            {
                if (value == null) Enabled = false;
                if (value == _zones) return;

                _zones = value;
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
            if (_zones == null || _zones.Count == 0) return;

            foreach (var zone in _zones)
            {
                InternalRender(graphics, Brushes.Green, zone.Value.Boundary);
            }

            if (_selected > 0) InternalRender(graphics, Brushes.Red, _zones[_selected].Boundary);
        }

        private void InternalRender(Graphics graphics, Brush brush, IEnumerable<Point> zone)
        {
            if (zone == null) return;

            foreach (var point in zone)
            {
                var rect = new RectangleF(point.X - 0.5f, point.Y - 0.5f, 1, 1);

                graphics.FillEllipse(brush, rect);
            }
        }
    }
}