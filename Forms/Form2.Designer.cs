namespace DyOrderAuto.Forms
{
    partial class Form2
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
            imicon = new PictureBox();
            DingList = new ListView();
            Order = new ColumnHeader();
            ShopName = new ColumnHeader();
            ShopType = new ColumnHeader();
            ShopCont = new ColumnHeader();
            BuyTime = new ColumnHeader();
            UserCont = new ColumnHeader();
            BuyType = new ColumnHeader();
            dypint = new TextBox();
            button1 = new Button();
            UserName = new Label();
            Leijibuycon = new Label();
            Leijibuy = new Label();
            DayBuy = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            daybuyint = new Label();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)imicon).BeginInit();
            SuspendLayout();
            // 
            // imicon
            // 
            imicon.Location = new Point(15, 14);
            imicon.Margin = new Padding(4, 4, 4, 4);
            imicon.Name = "imicon";
            imicon.Size = new Size(129, 118);
            imicon.TabIndex = 0;
            imicon.TabStop = false;
            // 
            // DingList
            // 
            DingList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DingList.BorderStyle = BorderStyle.FixedSingle;
            DingList.Columns.AddRange(new ColumnHeader[] { Order, ShopName, ShopType, ShopCont, BuyTime, UserCont, BuyType });
            DingList.FullRowSelect = true;
            DingList.GridLines = true;
            DingList.Location = new Point(6, 153);
            DingList.Margin = new Padding(3, 4, 3, 4);
            DingList.Name = "DingList";
            DingList.Size = new Size(1352, 420);
            DingList.TabIndex = 1;
            DingList.UseCompatibleStateImageBehavior = false;
            DingList.View = View.Details;
            // 
            // Order
            // 
            Order.Text = "订单号";
            Order.TextAlign = HorizontalAlignment.Center;
            Order.Width = 140;
            // 
            // ShopName
            // 
            ShopName.Text = "商品名";
            ShopName.TextAlign = HorizontalAlignment.Center;
            ShopName.Width = 240;
            // 
            // ShopType
            // 
            ShopType.Text = "规格";
            ShopType.TextAlign = HorizontalAlignment.Center;
            ShopType.Width = 180;
            // 
            // ShopCont
            // 
            ShopCont.Text = "价格";
            ShopCont.TextAlign = HorizontalAlignment.Center;
            ShopCont.Width = 100;
            // 
            // BuyTime
            // 
            BuyTime.Text = "下单时间";
            BuyTime.TextAlign = HorizontalAlignment.Center;
            BuyTime.Width = 180;
            // 
            // UserCont
            // 
            UserCont.Text = "个人累计";
            UserCont.TextAlign = HorizontalAlignment.Center;
            UserCont.Width = 90;
            // 
            // BuyType
            // 
            BuyType.Text = "订单状态";
            BuyType.TextAlign = HorizontalAlignment.Center;
            BuyType.Width = 100;
            // 
            // dypint
            // 
            dypint.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            dypint.Location = new Point(687, 14);
            dypint.Margin = new Padding(4, 4, 4, 4);
            dypint.Name = "dypint";
            dypint.Size = new Size(567, 41);
            dypint.TabIndex = 2;
            dypint.KeyPress += dypint_KeyPress;
            // 
            // button1
            // 
            button1.Location = new Point(1263, 14);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(96, 40);
            button1.TabIndex = 3;
            button1.Text = "查询";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // UserName
            // 
            UserName.AutoSize = true;
            UserName.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            UserName.Location = new Point(163, 18);
            UserName.Margin = new Padding(4, 0, 4, 0);
            UserName.Name = "UserName";
            UserName.Size = new Size(62, 39);
            UserName.TabIndex = 4;
            UserName.Text = "xxx";
            // 
            // Leijibuycon
            // 
            Leijibuycon.AutoSize = true;
            Leijibuycon.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            Leijibuycon.ForeColor = SystemColors.MenuHighlight;
            Leijibuycon.Location = new Point(1116, 116);
            Leijibuycon.Margin = new Padding(4, 0, 4, 0);
            Leijibuycon.Name = "Leijibuycon";
            Leijibuycon.Size = new Size(225, 35);
            Leijibuycon.TabIndex = 5;
            Leijibuycon.Text = "累计下单：100单";
            // 
            // Leijibuy
            // 
            Leijibuy.AutoSize = true;
            Leijibuy.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            Leijibuy.ForeColor = Color.Coral;
            Leijibuy.Location = new Point(163, 95);
            Leijibuy.Margin = new Padding(4, 0, 4, 0);
            Leijibuy.Name = "Leijibuy";
            Leijibuy.Size = new Size(251, 39);
            Leijibuy.TabIndex = 6;
            Leijibuy.Text = "累计消费：100元";
            // 
            // DayBuy
            // 
            DayBuy.AutoSize = true;
            DayBuy.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            DayBuy.ForeColor = Color.LimeGreen;
            DayBuy.Location = new Point(843, 116);
            DayBuy.Margin = new Padding(4, 0, 4, 0);
            DayBuy.Name = "DayBuy";
            DayBuy.Size = new Size(225, 35);
            DayBuy.TabIndex = 7;
            DayBuy.Text = "今日下单：100单";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // daybuyint
            // 
            daybuyint.AutoSize = true;
            daybuyint.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            daybuyint.ForeColor = Color.ForestGreen;
            daybuyint.Location = new Point(568, 116);
            daybuyint.Margin = new Padding(4, 0, 4, 0);
            daybuyint.Name = "daybuyint";
            daybuyint.Size = new Size(225, 35);
            daybuyint.TabIndex = 8;
            daybuyint.Text = "今日消费：100元";
            // 
            // button2
            // 
            button2.Location = new Point(568, 14);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(111, 40);
            button2.TabIndex = 9;
            button2.Text = "加入黑名单";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1368, 576);
            Controls.Add(button2);
            Controls.Add(daybuyint);
            Controls.Add(DayBuy);
            Controls.Add(Leijibuy);
            Controls.Add(Leijibuycon);
            Controls.Add(UserName);
            Controls.Add(button1);
            Controls.Add(dypint);
            Controls.Add(DingList);
            Controls.Add(imicon);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            Text = "[Dy]赳赳拆卡-用户信息查询";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)imicon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imicon;
        internal ListView DingList;
        private ColumnHeader Order;
        private ColumnHeader ShopName;
        private ColumnHeader ShopType;
        private ColumnHeader ShopCont;
        private ColumnHeader BuyTime;
        private ColumnHeader UserCont;
        private ColumnHeader BuyType;
        private Button button1;
        private Label UserName;
        private Label Leijibuycon;
        private Label Leijibuy;
        private Label DayBuy;
        internal TextBox dypint;
        private ContextMenuStrip contextMenuStrip1;
        private Label daybuyint;
        private Button button2;
    }
}