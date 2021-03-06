﻿namespace Triangulation.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGraphItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMapItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveZonesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processingItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractWatershedItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractBoundaryItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unionZoneItem = new System.Windows.Forms.ToolStripMenuItem();
            this.map = new Triangulation.Controls.Map();
            this.listBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLoad = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.contextTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextTreeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileItem,
            this.processingItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(956, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileItem
            // 
            this.fileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openGraphItem,
            this.openMapItem,
            this.toolStripSeparator2,
            this.saveZonesItem,
            this.toolStripSeparator1,
            this.exitItem});
            this.fileItem.Name = "fileItem";
            this.fileItem.Size = new System.Drawing.Size(48, 20);
            this.fileItem.Text = "Файл";
            // 
            // openGraphItem
            // 
            this.openGraphItem.Name = "openGraphItem";
            this.openGraphItem.Size = new System.Drawing.Size(221, 22);
            this.openGraphItem.Text = "Открыть граф";
            this.openGraphItem.Click += new System.EventHandler(this.OnOpenGraph);
            // 
            // openMapItem
            // 
            this.openMapItem.Name = "openMapItem";
            this.openMapItem.Size = new System.Drawing.Size(221, 22);
            this.openMapItem.Text = "Открыть карту затоплений";
            this.openMapItem.Click += new System.EventHandler(this.OnOpenMap);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(218, 6);
            // 
            // saveZonesItem
            // 
            this.saveZonesItem.Enabled = false;
            this.saveZonesItem.Name = "saveZonesItem";
            this.saveZonesItem.Size = new System.Drawing.Size(221, 22);
            this.saveZonesItem.Text = "Сохранить карту зон";
            this.saveZonesItem.Click += new System.EventHandler(this.OnSaveZones);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(218, 6);
            // 
            // exitItem
            // 
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(221, 22);
            this.exitItem.Text = "Выход";
            this.exitItem.Click += new System.EventHandler(this.OnExit);
            // 
            // processingItem
            // 
            this.processingItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractWatershedItem,
            this.extractBoundaryItem,
            this.unionZoneItem});
            this.processingItem.Enabled = false;
            this.processingItem.Name = "processingItem";
            this.processingItem.Size = new System.Drawing.Size(79, 20);
            this.processingItem.Text = "Обработка";
            // 
            // extractWatershedItem
            // 
            this.extractWatershedItem.Name = "extractWatershedItem";
            this.extractWatershedItem.Size = new System.Drawing.Size(193, 22);
            this.extractWatershedItem.Text = "Извлечь зоны";
            this.extractWatershedItem.Click += new System.EventHandler(this.OnWatershedExtract);
            // 
            // extractBoundaryItem
            // 
            this.extractBoundaryItem.Name = "extractBoundaryItem";
            this.extractBoundaryItem.Size = new System.Drawing.Size(193, 22);
            this.extractBoundaryItem.Text = "Извлечь границы зон";
            this.extractBoundaryItem.Click += new System.EventHandler(this.OnBoundaryExtract);
            // 
            // unionZoneItem
            // 
            this.unionZoneItem.Name = "unionZoneItem";
            this.unionZoneItem.Size = new System.Drawing.Size(193, 22);
            this.unionZoneItem.Text = "Объединение зон";
            this.unionZoneItem.Click += new System.EventHandler(this.OnZoneUnion);
            // 
            // map
            // 
            this.map.Location = new System.Drawing.Point(12, 27);
            this.map.MapSize = new System.Drawing.Size(944, 944);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(700, 500);
            this.map.TabIndex = 1;
            this.map.Text = "map1";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 17;
            this.listBox.Location = new System.Drawing.Point(718, 295);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(226, 225);
            this.listBox.TabIndex = 2;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.OnZoneSelected);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(715, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Номера зон:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLoad,
            this.statusProgress,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(956, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLoad
            // 
            this.statusLoad.Name = "statusLoad";
            this.statusLoad.Size = new System.Drawing.Size(55, 17);
            this.statusLoad.Text = "Загрузка";
            // 
            // statusProgress
            // 
            this.statusProgress.ForeColor = System.Drawing.Color.Lime;
            this.statusProgress.MarqueeAnimationSpeed = 50;
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(100, 16);
            this.statusProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.statusProgress.ToolTipText = "Загрузка";
            this.statusProgress.Visible = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(718, 44);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(226, 228);
            this.treeView.TabIndex = 5;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnNodeSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnNodeClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(715, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Дерево русел:";
            // 
            // contextTreeMenu
            // 
            this.contextTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unionMenu,
            this.selectAllMenu});
            this.contextTreeMenu.Name = "contextTreeMenu";
            this.contextTreeMenu.Size = new System.Drawing.Size(149, 48);
            // 
            // unionMenu
            // 
            this.unionMenu.Name = "unionMenu";
            this.unionMenu.Size = new System.Drawing.Size(148, 22);
            this.unionMenu.Text = "Объединить";
            this.unionMenu.Click += new System.EventHandler(this.OnNodeUnion);
            // 
            // selectAllMenu
            // 
            this.selectAllMenu.Name = "selectAllMenu";
            this.selectAllMenu.Size = new System.Drawing.Size(148, 22);
            this.selectAllMenu.Text = "Выделить все";
            this.selectAllMenu.Click += new System.EventHandler(this.OnNodeSelectAll);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(956, 552);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.map);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextTreeMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileItem;
        private System.Windows.Forms.ToolStripMenuItem openGraphItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitItem;
        private System.Windows.Forms.ToolStripMenuItem processingItem;
        private System.Windows.Forms.ToolStripMenuItem extractWatershedItem;
        private System.Windows.Forms.ToolStripMenuItem saveZonesItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem openMapItem;
        private Controls.Map map;
        private System.Windows.Forms.ToolStripMenuItem extractBoundaryItem;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ToolStripMenuItem unionZoneItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLoad;
        private System.Windows.Forms.ContextMenuStrip contextTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem unionMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllMenu;
    }
}

