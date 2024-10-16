namespace DyOrderAuto.Forms
{
    partial class OldOrderFrom
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
            Restart = new Button();
            Ding_F = new Label();
            Ding_Upper = new Button();
            Ding_Next = new Button();
            GetOrderList = new Button();
            label8 = new Label();
            Selet_Time = new DateTimePicker();
            OldOrderListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            GetAllOrder = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            重新打印ToolStripMenuItem = new ToolStripMenuItem();
            删除订单ToolStripMenuItem = new ToolStripMenuItem();
            添加黑名单ToolStripMenuItem = new ToolStripMenuItem();
            查看买家统计ToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Restart
            // 
            Restart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Restart.Location = new Point(534, 698);
            Restart.Margin = new Padding(3, 4, 3, 4);
            Restart.Name = "Restart";
            Restart.Size = new Size(89, 34);
            Restart.TabIndex = 19;
            Restart.Text = "重置";
            Restart.UseVisualStyleBackColor = true;
            // 
            // Ding_F
            // 
            Ding_F.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Ding_F.AutoSize = true;
            Ding_F.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            Ding_F.Location = new Point(1365, 701);
            Ding_F.Margin = new Padding(4, 0, 4, 0);
            Ding_F.Name = "Ding_F";
            Ding_F.Size = new Size(62, 24);
            Ding_F.TabIndex = 18;
            Ding_F.Text = "1/100";
            // 
            // Ding_Upper
            // 
            Ding_Upper.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Ding_Upper.BackColor = Color.Transparent;
            Ding_Upper.FlatStyle = FlatStyle.System;
            Ding_Upper.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            Ding_Upper.Location = new Point(1268, 698);
            Ding_Upper.Margin = new Padding(4, 4, 4, 4);
            Ding_Upper.Name = "Ding_Upper";
            Ding_Upper.Size = new Size(89, 31);
            Ding_Upper.TabIndex = 17;
            Ding_Upper.Text = "上一页";
            Ding_Upper.TextAlign = ContentAlignment.TopCenter;
            Ding_Upper.UseVisualStyleBackColor = false;
            Ding_Upper.Click += Ding_Upper_Click;
            // 
            // Ding_Next
            // 
            Ding_Next.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Ding_Next.BackColor = Color.Transparent;
            Ding_Next.FlatStyle = FlatStyle.System;
            Ding_Next.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            Ding_Next.Location = new Point(1433, 698);
            Ding_Next.Margin = new Padding(4, 4, 4, 4);
            Ding_Next.Name = "Ding_Next";
            Ding_Next.Size = new Size(89, 31);
            Ding_Next.TabIndex = 16;
            Ding_Next.Text = "下一页";
            Ding_Next.TextAlign = ContentAlignment.TopCenter;
            Ding_Next.UseVisualStyleBackColor = false;
            Ding_Next.Click += Ding_Next_Click;
            // 
            // GetOrderList
            // 
            GetOrderList.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            GetOrderList.Location = new Point(325, 698);
            GetOrderList.Margin = new Padding(3, 4, 3, 4);
            GetOrderList.Name = "GetOrderList";
            GetOrderList.Size = new Size(75, 34);
            GetOrderList.TabIndex = 15;
            GetOrderList.Text = "查询";
            GetOrderList.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.Green;
            label8.Location = new Point(4, 704);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(92, 27);
            label8.TabIndex = 14;
            label8.Text = "查询订单";
            // 
            // Selet_Time
            // 
            Selet_Time.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Selet_Time.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            Selet_Time.Location = new Point(107, 701);
            Selet_Time.Margin = new Padding(4, 4, 4, 4);
            Selet_Time.Name = "Selet_Time";
            Selet_Time.Size = new Size(211, 30);
            Selet_Time.TabIndex = 13;
            Selet_Time.Value = new DateTime(2024, 7, 18, 0, 0, 0, 0);
            // 
            // OldOrderListView
            // 
            OldOrderListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OldOrderListView.BorderStyle = BorderStyle.FixedSingle;
            OldOrderListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7, columnHeader9 });
            OldOrderListView.FullRowSelect = true;
            OldOrderListView.GridLines = true;
            OldOrderListView.Location = new Point(3, 3);
            OldOrderListView.Margin = new Padding(3, 4, 3, 4);
            OldOrderListView.Name = "OldOrderListView";
            OldOrderListView.Size = new Size(1523, 687);
            OldOrderListView.TabIndex = 12;
            OldOrderListView.UseCompatibleStateImageBehavior = false;
            OldOrderListView.View = View.Details;
            OldOrderListView.MouseDown += OldOrderListView_MouseDown;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "订单号";
            columnHeader1.TextAlign = HorizontalAlignment.Center;
            columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "商品名";
            columnHeader2.TextAlign = HorizontalAlignment.Center;
            columnHeader2.Width = 260;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "规格";
            columnHeader3.TextAlign = HorizontalAlignment.Center;
            columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "价格";
            columnHeader4.TextAlign = HorizontalAlignment.Center;
            columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "昵称";
            columnHeader5.TextAlign = HorizontalAlignment.Center;
            columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "下单时间";
            columnHeader6.TextAlign = HorizontalAlignment.Center;
            columnHeader6.Width = 180;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "个人累计";
            columnHeader7.TextAlign = HorizontalAlignment.Center;
            columnHeader7.Width = 90;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "订单状态";
            columnHeader9.TextAlign = HorizontalAlignment.Center;
            columnHeader9.Width = 100;
            // 
            // GetAllOrder
            // 
            GetAllOrder.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            GetAllOrder.Location = new Point(405, 698);
            GetAllOrder.Margin = new Padding(3, 4, 3, 4);
            GetAllOrder.Name = "GetAllOrder";
            GetAllOrder.Size = new Size(123, 34);
            GetAllOrder.TabIndex = 11;
            GetAllOrder.Text = "导入历史订单";
            GetAllOrder.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 重新打印ToolStripMenuItem, 删除订单ToolStripMenuItem, 添加黑名单ToolStripMenuItem, 查看买家统计ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(169, 100);
            // 
            // 重新打印ToolStripMenuItem
            // 
            重新打印ToolStripMenuItem.Name = "重新打印ToolStripMenuItem";
            重新打印ToolStripMenuItem.Size = new Size(168, 24);
            重新打印ToolStripMenuItem.Text = "重新打印";
            // 
            // 删除订单ToolStripMenuItem
            // 
            删除订单ToolStripMenuItem.Name = "删除订单ToolStripMenuItem";
            删除订单ToolStripMenuItem.Size = new Size(168, 24);
            删除订单ToolStripMenuItem.Text = "删除订单";
            // 
            // 添加黑名单ToolStripMenuItem
            // 
            添加黑名单ToolStripMenuItem.Name = "添加黑名单ToolStripMenuItem";
            添加黑名单ToolStripMenuItem.Size = new Size(168, 24);
            添加黑名单ToolStripMenuItem.Text = "添加黑名单";
            // 
            // 查看买家统计ToolStripMenuItem
            // 
            查看买家统计ToolStripMenuItem.Name = "查看买家统计ToolStripMenuItem";
            查看买家统计ToolStripMenuItem.Size = new Size(168, 24);
            查看买家统计ToolStripMenuItem.Text = "查看买家统计";
            // 
            // OldOrderFrom
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1528, 736);
            Controls.Add(Restart);
            Controls.Add(Ding_F);
            Controls.Add(Ding_Upper);
            Controls.Add(Ding_Next);
            Controls.Add(GetOrderList);
            Controls.Add(label8);
            Controls.Add(Selet_Time);
            Controls.Add(OldOrderListView);
            Controls.Add(GetAllOrder);
            Margin = new Padding(4, 4, 4, 4);
            Name = "OldOrderFrom";
            Text = "[Dy]赳赳拆卡-历史订单";
            Load += OldOrderFrom_Load;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Restart;
        private Label Ding_F;
        private Button Ding_Upper;
        private Button Ding_Next;
        private Button GetOrderList;
        private Label label8;
        private DateTimePicker Selet_Time;
        internal ListView OldOrderListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader9;
        private Button GetAllOrder;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 重新打印ToolStripMenuItem;
        private ToolStripMenuItem 删除订单ToolStripMenuItem;
        private ToolStripMenuItem 添加黑名单ToolStripMenuItem;
        private ToolStripMenuItem 查看买家统计ToolStripMenuItem;
    }
}