using DyOrderAuto.DyManager;

namespace DyOrderAuto.Forms
{
    public partial class AutoWebYdForm : Form
    {
        public static AutoWebYdForm Instance;
        public AutoWebYdForm()
        {
            InitializeComponent();
            Instance = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoWebYd.AutoStar(textBox1.Text);
        }
        public void SendLogForm(string text)
        {

            LogText.Invoke(new Action(() => LogText.AppendText($"[{DateTime.Now}]:{text}\r\n")));

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AutoWebYd.senddid(textBox2.Text);
            label2.Text = "";
            textBox2.Text = "";
            label3.Text = "";

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AutoWebYd.StarDebugUI();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                button2_Click(sender, e);
                e.Handled = true;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutoWebYd.IsRunning = false;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AutoWebYd.UrlJump(textBox1.Text);
        }
    }
}
