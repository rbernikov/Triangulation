using System.Drawing;

namespace Triangulation.Controls.Layers
{
    public interface ILayer
    {
        bool Enabled { get; set; }
        string Name { get; set; }
        Map Parent { get; set; }
        void Render(Graphics graphics);
    }
}