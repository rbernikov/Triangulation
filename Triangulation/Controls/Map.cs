using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Triangulation.Controls.Layers;

namespace Triangulation.Controls
{
    public sealed class Map : Control
    {
        private Image _image;
        private Point _basePoint;
        private Point _startPoint;
        private Size _mapSize;

        private bool _drag;
        private float _scaleFactor = 1;

        private readonly LayersCollection _layersCollection;

        public Map()
        {
            _layersCollection = new LayersCollection(this);

            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        public Map(Size size)
            : this()
        {
            MapSize = size;
        }

        public Map(int width, int height)
            : this(new Size(width, height))
        {

        }

        public Image Image => _image;

        [Browsable(true)]
        [DefaultValue(typeof(Size), "944; 944")]
        public Size MapSize
        {
            get => _mapSize;
            set => _mapSize = value;
        }

        public LayersCollection Layers => _layersCollection;

        protected override void OnPaint(PaintEventArgs e)
        {
            _image = new Bitmap(_mapSize.Width, _mapSize.Height);
            var g = Graphics.FromImage(_image);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PageUnit = GraphicsUnit.Pixel;

            PaintInternal(g);
            g.Dispose();

            var src = new Rectangle(0, 0, Image.Width, Image.Height);
            var dst = new Rectangle(_basePoint.X, _basePoint.Y, (int)(Image.Width * _scaleFactor), (int)(Image.Height * _scaleFactor));
            e.Graphics.DrawImage(_image, dst, src, GraphicsUnit.Pixel);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            LimitBasePoint(_basePoint.X, _basePoint.Y);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                _drag = true;
                _startPoint.X = e.X;
                _startPoint.Y = e.Y;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
                _drag = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!_drag) return;

            LimitBasePoint(_basePoint.X + e.X - _startPoint.X, _basePoint.Y + e.Y - _startPoint.Y);
            _startPoint.X = e.X;
            _startPoint.Y = e.Y;

            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 0)
                ZoomIn();
            else
                ZoomOut();
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

        private void LimitBasePoint(int x, int y)
        {
            if (Image == null) return;

            var width = Width - (int)(Image.Width * _scaleFactor);
            var height = Height - (int)(Image.Height * _scaleFactor);

            if (width < 0)
            {
                x = Math.Max(Math.Min(x, 0), width);
            }
            else
            {
                x = width / 2;
            }

            if (height < 0)
            {
                y = Math.Max(Math.Min(y, 0), height);
            }
            else
            {
                y = height / 2;
            }
            _basePoint = new Point(x, y);
        }

        private void ZoomIn()
        {
            if (!(_scaleFactor <= 5)) return;

            var x = (int)((Width / 2.0 - _basePoint.X) / _scaleFactor);
            var y = (int)((Height / 2.0 - _basePoint.Y) / _scaleFactor);

            _scaleFactor *= 2;
            LimitBasePoint((int)(Width / 2.0 - x * _scaleFactor), (int)(Height / 2.0 - y * _scaleFactor));

            Invalidate();
        }

        private void ZoomOut()
        {
            if (!(_scaleFactor >= 1)) return;

            var x = (int)((Width / 2.0 - _basePoint.X) / _scaleFactor);
            var y = (int)((Height / 2.0 - _basePoint.Y) / _scaleFactor);

            _scaleFactor /= 2;
            LimitBasePoint((int)(Width / 2.0 - x * _scaleFactor), (int)(Height / 2.0 - y * _scaleFactor));

            Invalidate();
        }
    }
}