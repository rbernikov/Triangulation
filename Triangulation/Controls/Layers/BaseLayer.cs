using System.Drawing;

namespace Triangulation.Controls.Layers
{
    public abstract class BaseLayer : ILayer
    {
        private bool _enabled = true;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    Parent.Invalidate();
                }
            }
        }

        public string Name { get; set; }
        public Map Parent { get; set; }

        public abstract void Render(Graphics graphics);
    }
}