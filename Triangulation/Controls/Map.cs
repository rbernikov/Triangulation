using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Triangulation.Controls.Layers;

namespace Triangulation.Controls
{
    public sealed class Map : Control
    {
        private const float ScaleMul = 1.05f;
        
        private readonly Matrix _transform;
        private readonly LayersCollection _layersCollection;

        public Map()
        {
            _transform = new Matrix();
            _layersCollection = new LayersCollection(this);
            
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        public Map(Size size)
            : this()
        {
            Size = size;
        }

        public Map(int width, int height)
            : this(new Size(width, height))
        {

        }

        public LayersCollection Layers => _layersCollection;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.Transform = _transform;

            PaintInternal(e.Graphics);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            var matrixOrder = MatrixOrder.Append;
            var k = e.Delta > 0 ? ScaleMul : 1 / ScaleMul;
            _transform.Multiply(new Matrix(1, 0, 0, 1, -e.Location.X, -e.Location.Y), matrixOrder);
            _transform.Multiply(new Matrix(k, 0, 0, k, 0, 0), matrixOrder);
            _transform.Multiply(new Matrix(1, 0, 0, 1, e.Location.X, e.Location.Y), matrixOrder);

            Invalidate();
        }

        private void PaintInternal(Graphics g)
        {
            g.Clear(BackColor);

            foreach (var layer in _layersCollection)
            {
                if (layer.Enabled)
                    layer.Render(g);
            }
        }
    }
}