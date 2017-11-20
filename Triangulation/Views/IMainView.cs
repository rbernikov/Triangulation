﻿using System.Collections.Generic;
using System.Drawing;
using Triangulation.Geometry;
using Triangulation.Zones;

namespace Triangulation.Views
{
    public interface IMainView
    {
        void OnUpdateView();
        void OnGraphLoaded(List<Vertex> vertices);
        void OnWatershedExtracted(List<Edge> edges);
        void OnBoundaryExtracted(Dictionary<int, ZoneInfo> zones);
        void OnZoneUnioned(int first, int second, ZoneInfo newZone);

        void OnShowError(string message);
        void OnShowProgress(bool show);
    }
}