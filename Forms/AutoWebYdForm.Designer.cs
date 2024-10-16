namespace DyOrderAuto.Forms
{
    partial class AutoWebYdForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoWebYdForm));
            button1 = new Button();
            textBox1 = new TextBox();
            LogText = new TextBox();
            button2 = new Button();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            button3 = new Button();
            label3 = new Label();
            checkBox3 = new CheckBox();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(483, 11);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(76, 25);
            button1.TabIndex = 0;
            button1.Text = "启动浏览器";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(205, 10);
            textBox1.Margin = new Padding(2, 3, 2, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(275, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "https://douyins.woda.com/#/";
            // 
            // LogText
            // 
            LogText.BackColor = SystemColors.Window;
            LogText.Location = new Point(9, 185);
            LogText.Margin = new Padding(2, 3, 2, 3);
            LogText.Multiline = true;
            LogText.Name = "LogText";
            LogText.RightToLeft = RightToLeft.No;
            LogText.ScrollBars = ScrollBars.Both;
            LogText.Size = new Size(551, 365);
            LogText.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(484, 127);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(76, 25);
            button2.TabIndex = 5;
            button2.Text = "查询";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Microsoft YaHei UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(266, 128);
            textBox2.Margin = new Padding(2, 3, 2, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(214, 26);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += textBox2_TextChanged;
            textBox2.KeyDown += textBox2_KeyDown;
            textBox2.KeyPress += textBox2_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(206, 130);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(58, 21);
            label1.TabIndex = 7;
            label1.Text = "订单号";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.MenuHighlight;
            label2.Location = new Point(9, 8);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(123, 30);
            label2.TabIndex = 8;
            label2.Text = "今日共：单";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(95, 158);
            checkBox1.Margin = new Padding(2, 3, 2, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(111, 21);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "直接打印快递单";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(14, 158);
            checkBox2.Margin = new Padding(2, 3, 2, 3);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(75, 21);
            checkBox2.TabIndex = 10;
            checkBox2.Text = "语音播报";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(483, 71);
            button3.Name = "button3";
            button3.Size = new Size(76, 23);
            button3.TabIndex = 12;
            button3.Text = "启动调试页";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.GrayText;
            label3.Location = new Point(9, 46);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(101, 30);
            label3.TabIndex = 14;
            label3.Text = "用户名：";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(205, 158);
            checkBox3.Margin = new Padding(2, 3, 2, 3);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(75, 21);
            checkBox3.TabIndex = 15;
            checkBox3.Text = "直接发货";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(484, 42);
            button4.Name = "button4";
            button4.Size = new Size(76, 23);
            button4.TabIndex = 16;
            button4.Text = "重新跳转";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // AutoWebYdForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 561);
            Controls.Add(button4);
            Controls.Add(checkBox3);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(LogText);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            Name = "AutoWebYdForm";
            RightToLeftLayout = true;
            Text = "[Dy]赳赳拆卡-全自动快递打单器";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox LogText;
        private Button button2;
        private TextBox textBox2;
        public Label label1;
        public Label label2;
        public CheckBox checkBox1;
        public CheckBox checkBox2;
        private Button button3;
        public Label label3;
        public CheckBox checkBox3;
        private Button button4;
    }
}
