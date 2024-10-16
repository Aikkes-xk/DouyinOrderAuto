namespace DyOrderAuto.Forms
{
    partial class BlackListFrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            BlackListView = new ListView();
            columnHeader17 = new ColumnHeader();
            columnHeader18 = new ColumnHeader();
            columnHeader19 = new ColumnHeader();
            columnHeader20 = new ColumnHeader();
            columnHeader21 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            删除黑名单ToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // BlackListView
            // 
            BlackListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BlackListView.BorderStyle = BorderStyle.FixedSingle;
            BlackListView.Columns.AddRange(new ColumnHeader[] { columnHeader17, columnHeader18, columnHeader19, columnHeader20, columnHeader21 });
            BlackListView.ContextMenuStrip = contextMenuStrip1;
            BlackListView.FullRowSelect = true;
            BlackListView.GridLines = true;
            BlackListView.Location = new Point(3, 4);
            BlackListView.Margin = new Padding(3, 4, 3, 4);
            BlackListView.Name = "BlackListView";
            BlackListView.Size = new Size(1231, 633);
            BlackListView.TabIndex = 3;
            BlackListView.UseCompatibleStateImageBehavior = false;
            BlackListView.View = View.Details;
            BlackListView.MouseDown += BlackListView_MouseDown;
            // 
            // columnHeader17
            // 
            columnHeader17.Text = "黑名单用户id";
            columnHeader17.Width = 180;
            // 
            // columnHeader18
            // 
            columnHeader18.Text = "买家昵称";
            columnHeader18.TextAlign = HorizontalAlignment.Center;
            columnHeader18.Width = 150;
            // 
            // columnHeader19
            // 
            columnHeader19.Text = "黑名单订单";
            columnHeader19.TextAlign = HorizontalAlignment.Center;
            columnHeader19.Width = 180;
            // 
            // columnHeader20
            // 
            columnHeader20.Text = "黑名单原因";
            columnHeader20.TextAlign = HorizontalAlignment.Center;
            columnHeader20.Width = 200;
            // 
            // columnHeader21
            // 
            columnHeader21.Text = "添加时间";
            columnHeader21.TextAlign = HorizontalAlignment.Center;
            columnHeader21.Width = 200;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 删除黑名单ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(154, 28);
            // 
            // 删除黑名单ToolStripMenuItem
            // 
            删除黑名单ToolStripMenuItem.Name = "删除黑名单ToolStripMenuItem";
            删除黑名单ToolStripMenuItem.Size = new Size(153, 24);
            删除黑名单ToolStripMenuItem.Text = "删除黑名单";
            // 
            // BlackListFrom
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1236, 640);
            Controls.Add(BlackListView);
            Margin = new Padding(4, 4, 4, 4);
            Name = "BlackListFrom";
            Text = "[Dy]赳赳拆卡-店铺黑名单管理";
            Load += BlackListFrom_Load;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal ListView BlackListView;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        private ColumnHeader columnHeader19;
        private ColumnHeader columnHeader20;
        private ColumnHeader columnHeader21;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 删除黑名单ToolStripMenuItem;
    }
}