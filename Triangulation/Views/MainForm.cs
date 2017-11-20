using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Triangulation.Controllers;
using Triangulation.Controls.Layers;
using Triangulation.Geometry;
using Triangulation.MapReduce;
using Triangulation.Zones;

namespace Triangulation.Views
{
    public partial class MainForm : Form, IMainView
    {
        private readonly IMainController _controller;

        public MainForm()
        {
            InitializeComponent();

            _controller = new MainControllerImpl(this);
        }

        #region IView implements

        public void OnUpdateView()
        {
            Action action = () => processingItem.Enabled = true;

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        public void OnGraphLoaded(List<Vertex> vertices)
        {
            Action<List<Vertex>> action = v =>
            {
                var layer = new GraphLayer(v) { Name = "graph" };
                map1.Layers.Add(layer);
            };

            if (InvokeRequired)
                Invoke(action, vertices);
            else
                action(vertices);
        }

        public void OnWatershedExtracted(List<Edge> edges)
        {
            Action<List<Edge>> action = e =>
            {
                saveZonesItem.Enabled = true;

                if (map1.Layers["zones"] != null) return;

                var layer = new WatershedLayer(edges) { Name = "zones" };
                map1.Layers.Add(layer);
            };

            if (InvokeRequired)
                Invoke(action, edges);
            else
                action(edges);
        }

        public void OnBoundaryExtracted(Dictionary<int, ZoneInfo> zones)
        {
            Action<Dictionary<int, ZoneInfo>> action = z =>
           {
               map1.Layers["zones"].Enabled = false;

               BoundaryLayer layer;
               if ((layer = (BoundaryLayer)map1.Layers["boundary"]) == null)
               {
                   layer = new BoundaryLayer { Name = "boundary" };
                   map1.Layers.Add(layer);
               }

               layer.Zones = z;
               listBox1.Items.Clear();
               foreach (var key in z.Keys.OrderBy(i => i))
               {
                   listBox1.Items.Add(key);
               }
           };

            if (InvokeRequired)
                Invoke(action, zones);
            else
                action(zones);
        }

        public void OnZoneUnioned(int first, int second, ZoneInfo newZone)
        {

        }

        public void OnShowError(string message)
        {
            Action action = () => MessageBox.Show(message);

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        public void OnShowProgress(bool show)
        {
            Action action = () => statusProgress.Visible = show;

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        #endregion

        #region Events

        private void OnOpenMap(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = @"Grd file(*.grd)|*.grd" };
            if (dialog.ShowDialog(this) == DialogResult.OK)
                _controller.OnMapLoad(dialog.FileName);
        }

        private void OnOpenGraph(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = @"Excel file(*.xlsm)|*.xlsm" };
            if (dialog.ShowDialog(this) == DialogResult.OK)
                _controller.OnLoad(dialog.FileName);
        }

        private void OnSaveZones(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = @"Grd file(*.grd)|*.grd" };
            if (dialog.ShowDialog(this) == DialogResult.OK)
                _controller.OnSaveZones(dialog.FileName);
        }

        private void OnWatershedExtract(object sender, EventArgs e)
        {
            _controller.OnWatershedExtract();
        }

        private void OnBoundaryExtract(object sender, EventArgs e)
        {
            _controller.OnBoundaryExtract();
        }

        private void OnZoneUnion(object sender, EventArgs e)
        {
            var form = new UnionForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var method = form.Method;
                var percent = form.Percent / 100.0f;

                _controller.OnZoneUnion(method, percent);
            }
        }

        private void OnZoneSelected(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            var layer = (BoundaryLayer)map1.Layers["boundary"];
            layer.Selected = (int)listBox1.SelectedItem;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}