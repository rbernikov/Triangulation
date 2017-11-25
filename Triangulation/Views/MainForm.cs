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
using Triangulation.Tree;
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

        public void OnGraphLoaded(Node root, List<Vertex> vertices)
        {
            Action<Node, List<Vertex>> action = (x, y) =>
            {
                var layer = new GraphLayer(y) { Name = "graph" };
                map1.Layers.Add(layer);

                BuildTree(x);
            };

            if (InvokeRequired)
                Invoke(action, root, vertices);
            else
                action(root, vertices);
        }

        public void OnWatershedExtracted(List<Edge> edges)
        {
            Action<List<Edge>> action = e =>
            {
                saveZonesItem.Enabled = true;

                var boundary = map1.Layers["boundary"];
                if (boundary != null) boundary.Enabled = false;

                WatershedLayer layer;
                if ((layer = (WatershedLayer)map1.Layers["zones"]) == null)
                {
                    layer = new WatershedLayer { Name = "zones" };
                    map1.Layers.Add(layer);
                }

                layer.Edges = e;
                layer.Enabled = true;

                listBox.Items.Clear();
            };

            if (InvokeRequired)
                Invoke(action, edges);
            else
                action(edges);
        }

        public void OnBoundaryExtracted(Node root)
        {
            Action<Node> action = node =>
            {
                var zones = map1.Layers["zones"];
                if (zones != null) zones.Enabled = false;

                BoundaryLayer layer;
                if ((layer = (BoundaryLayer)map1.Layers["boundary"]) == null)
                {
                    layer = new BoundaryLayer { Name = "boundary" };
                    map1.Layers.Add(layer);
                }

                layer.Root = node;
                layer.Enabled = true;

                var list = new List<int>();
                node.Traverse(x => list.Add(x.Label));

                listBox.Items.Clear();
                foreach (var i in list.OrderBy(i => i))
                {
                    listBox.Items.Add(i);
                }
            };

            if (InvokeRequired)
                Invoke(action, root);
            else
                action(root);
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
            Action action = () =>
            {
                statusLoad.Visible = show;
                statusProgress.Visible = show;
            };

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
            if (listBox.SelectedIndex < 0) return;

            var layer = (BoundaryLayer)map1.Layers["boundary"];
            layer.Selected = (int)listBox.SelectedItem;
        }

        private void OnNodeSelect(object sender, TreeViewEventArgs e)
        {
            var node = (Node)e.Node.Tag;

            if (node == null) return;

            var layer = (GraphLayer)map1.Layers["graph"];
            if (layer == null) return;

            layer.Selected = node.Vertices;
            statusLabel.Text = @"Код русла:" + BuildLabel(node);
        }

        private void OnNodeUnion(object sender, EventArgs e)
        {
            var treeNode = treeView.SelectedNode;
            var node = (Node)treeNode.Tag;

            if (node == null) return;

            if (!node.HasChildren) return;

            var vertices = new List<Vertex>();
            node.Traverse(x =>
            {
                vertices.AddRange(x.Vertices.Select((v, i) => new Vertex(v.X, v.Y, node.Label)));
            });
            node.Vertices = vertices;
            node.ClearChildren();

            treeNode.Collapse();
            treeNode.Nodes.Clear();
        }

        private void OnNodeSelectAll(object sender, EventArgs e)
        {
            var treeNode = treeView.SelectedNode;
            var node = (Node)treeNode.Tag;

            if (node == null) return;

            var layer = (GraphLayer)map1.Layers["graph"];
            if (layer == null) return;

            var vertices = new List<Vertex>();
            node.Traverse(x =>
            {
                vertices.AddRange(x.Vertices);
            });

            layer.Selected = vertices;
        }

        private void OnNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                treeView.SelectedNode = e.Node;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Private methods

        private void BuildTree(Node root)
        {
            var item = new TreeNode("Русло " + root.Id) { Tag = root };
            foreach (var child in root.Children)
            {
                BuildTree(child, item);
            }

            treeView.Nodes.Add(item);
        }

        private void BuildTree(Node root, TreeNode node)
        {
            var item = new TreeNode("Русло " + root.Id) { Tag = root };
            foreach (var child in root.Children)
            {
                BuildTree(child, item);
            }

            if (item.Nodes.Count > 0) item.ContextMenuStrip = contextTreeMenu;

            node.Nodes.Add(item);
        }

        private string BuildLabel(Node node)
        {
            if (node == null) return string.Empty;

            return BuildLabel(node.Parent) + " " + node.Id;
        }

        #endregion
    }
}