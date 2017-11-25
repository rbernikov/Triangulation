using System.Collections.Generic;
using System.Drawing;
using Triangulation.Geometry;
using Triangulation.Tree;
using Triangulation.Zones;

namespace Triangulation.Views
{
    public interface IMainView
    {
        void OnUpdateView();
        void OnGraphLoaded(Node root, List<Vertex> vertices);
        void OnWatershedExtracted(List<Edge> edges);
        void OnBoundaryExtracted(Node root);

        void OnShowError(string message);
        void OnShowProgress(bool show);
    }
}