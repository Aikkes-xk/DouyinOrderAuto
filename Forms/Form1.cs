using DyOrderAuto.DyManager;
using Microsoft.Playwright;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net;
using System.Security.Policy;
using System.Windows.Forms;
using DyOrderAuto.Config;
using Seagull.BarTender.Print;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DyOrderAuto.DataManager;
using System.Collections.Generic;
using System.Xml.Linq;
using DyOrderAuto.Forms;
using DyOrderAuto.DyMods;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using System.Reflection.Emit;

namespace DyOrderAuto
{
    internal partial class Form1 : Form
    {
        internal static Form1 Instance;
        ConfigUtils configUtils = new ConfigUtils();
        internal static ListViewItem CListItem;



        internal Form1()
        {
            InitializeComponent();
            Instance = this;
        }
        /// <summary>
        /// ���ڼ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();
            //��ʼ������
            Program.StarLoad();
            DataManager.DataTools.StarSql();
            LoadPrintList();
            LoadPrintBUW();
            //��ʼ�����ն���
            LoadUiListDay();


            //��ʼ���������
            GameplayProcessing.LoadNumbers();


        }




        /// <summary>
        /// ������ն���
        /// </summary>
        private void LoadUiListDay()
        {
            var ass = DataTools.GetDayOerderList(DateTime.Now);
            //���ж���ʱ������
            ass.Sort((x, y) => y.Updata_Time.CompareTo(x.Updata_Time));
            DingList.Items.Clear();
            foreach (var item in ass)
            {
                // �����µ� ListViewItem
                ListViewItem dinga = new ListViewItem();

                // �������
                //���Ψһ��ʶ��
                dinga.Name = item.orderid;
                var queue = DataTools.GetDyQueueList(item.orderid);

                if (queue == null)
                {
                    dinga.Text = item.orderid;
                    dinga.SubItems.Add(item.shop_name);
                    dinga.SubItems.Add(item.shop_typename);
                    dinga.SubItems.Add(item.pay_sf);
                    dinga.SubItems.Add(item.buy_nickname);
                    dinga.SubItems.Add(item.buy_Time.ToString());
                    dinga.SubItems.Add("���ŵ���Ϣ");
                    dinga.SubItems.Add("���ŵ���Ϣ");
                    dinga.SubItems.Add("���ŵ���Ϣ");
                    dinga.SubItems.Add("���ŵ���Ϣ");
                }
                else
                {
                    dinga.Text = item.orderid;
                    dinga.SubItems.Add(item.shop_name);
                    dinga.SubItems.Add(item.shop_typename);
                    dinga.SubItems.Add(item.pay_sf);
                    dinga.SubItems.Add(item.buy_nickname);
                    dinga.SubItems.Add(item.buy_Time.ToString());
                    dinga.SubItems.Add(queue.RecordsNumber.ToString());
                    dinga.SubItems.Add(queue.TodayNumber.ToString());
                    if (queue.Demolished == 1)
                    {
                        dinga.SubItems.Add("�ۺ�");
                    }
                    else
                    {
                        if (item.LuckyBag == 1)
                        {
                            dinga.SubItems.Add("����");
                        }
                        else
                        {
                            dinga.SubItems.Add("");
                        }
                    }
                    dinga.SubItems.Add(Program.GetListTypeName(queue.OverOrder));
                }
                // ������ӵ� ListView
                DingList.Items.Add(dinga);


            }

            FixedConfig.AllListInt = DingList.Items.Count;
            UpPaidanInfo();
        }


        /// <summary>
        /// ���¶�����Ϣ
        /// </summary>
        internal void UpPaidanInfo()
        {
            DqListInt.Invoke((Action)(() => DqListInt.Text = $"��ǰ�������� {FixedConfig.AllListInt}"));


        }


        /// <summary>
        /// ���س�ʼ��������Ϣ
        /// </summary>
        /// <param name="logourl"></param>
        /// <param name="dyName"></param>
        internal void SendUserInfo(string logourl, string dyName)
        {
            DydpName.Text = dyName;
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(logourl);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        DyIcon.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    SendLogForm("����ͷ������쳣��");
                }
            }


        }
        /// <summary>
        /// ��ȡ���ش�ӡ��
        /// </summary>
        private void LoadPrintList()
        {
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!PrintName.Items.Contains(fPrinterName))
                {
                    PrintName.Items.Add(fPrinterName);
                }
            }


        }
        /// <summary>
        /// ���ش�ӡ���б�
        /// </summary>
        internal void LoadPrintBUW()
        {
            string path = $@"{Environment.CurrentDirectory}\PtwMod\";
            string[] fileArray = Directory.GetFiles(path);
            foreach (string file in fileArray)
            {
                var filename = Path.GetFileName(file);
                if (!PrintMuban.Items.Contains(filename))
                {
                    PrintMuban.Items.Add(filename);
                }

            }




        }

        /// <summary>
        /// ������־������
        /// </summary>
        /// <param name="text"></param>
        public void SendLogForm(string text)
        {

            LogText.Invoke(new Action(() => LogText.AppendText($"[{DateTime.Now}]:{text}\r\n")));

        }

        /// <summary>
        /// ���¼�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (DydpName.Text != "δ����")
            {
                DyManager.DyOrderList.AutoStar(Config.FixedConfig.DYUrl, false, true);
            }
            else
            {
                DyManager.DyOrderList.AutoStar(Config.FixedConfig.DYUrl, false, false);
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DyManager.DyOrderList.StarDebugUI();
        }


        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (Program.yunx == false)
            {
                rebootPrintServer_Click(sender, e);
            }
            TimerTask.AddTask();

        }






        /// <summary>
        /// ���ڽ��̹ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DyManager.DyOrderList.browser != null)
            {
                if (DyManager.DyOrderList.browser.IsConnected)
                {
                    DyManager.DyOrderList.browser.CloseAsync();
                }
            }
            if (DyManager.DyIm.browser != null)
            {
                if (DyManager.DyIm.browser.IsConnected)
                {
                    DyManager.DyIm.browser.CloseAsync();
                }
            }
            if (Program.yunx)
            {
                daManager.CloseSendBtp();
            }
            DataManager.sqlDataManager.CloseSql();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(sender);
        }

        internal void StarIM()
        {
            if (StarAutohy.Checked)
            {
                DyManager.DyIm.AutoImStar();
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                DyIm.SendDyIm(textBox1.Text, NewPtcim.Text);

            }
            if (radioButton2.Checked)
            {

                DyIm.SendDyIm(textBox1.Text, gdtext.Text);
            }
            textBox1.Text = "";
        }

        /// <summary>
        /// ����ȫ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            configUtils.Write("Diyshop", "ShopName", dyshopnc.Text);
            configUtils.Write("PrintDy", "PrintName", PrintName.Text);
            configUtils.Write("PrintDy", "PrintMuban", PrintMuban.Text);
            configUtils.Write("PrintDy", "AutoPrint", AutoPrint.Checked.ToString());
            configUtils.Write("UserHello", "StarAutohy", StarAutohy.Checked.ToString());
            configUtils.Write("UserHello", "NewPtcim", NewPtcim.Text);
            configUtils.Write("UserHello", "gdtext", gdtext.Text);
            configUtils.Write("PrintDy", "PrintPort", PrintPort.Text);
            configUtils.Write("PrintDy", "MaxPrint", MaxPrint.Text);
            configUtils.Write("PrintDy", "KaMaxPrint", KaMaxInt.Text);

            var flag1 = int.TryParse(TimerText.Text, out var itime);
            if (!flag1 || itime <= 0)
            {
                TimerText.Text = "5";
                itime = 5;
                configUtils.Write("System", "TimeChick", TimerText.Text);
                timer1.Interval = itime * 1000;
            }
            FixedConfig.MaxPrint = int.Parse(MaxPrint.Text);
            FixedConfig.PrintMuban = PrintMuban.Text;
            FixedConfig.PrintPort = PrintPort.Text;
            FixedConfig.PrintName = PrintName.Text;
            FixedConfig.AutoPrint = AutoPrint.Checked;
            FixedConfig.ShopDiyName = dyshopnc.Text;
            FixedConfig.MaxGamePlay = int.Parse(KaMaxInt.Text);


        }
        /// <summary>
        /// ���������ļ�
        /// </summary>
        private void LoadConfig()
        {
            dyshopnc.Text = configUtils.Read("Diyshop", "ShopName");
            PrintName.Text = configUtils.Read("PrintDy", "PrintName");
            PrintMuban.Text = configUtils.Read("PrintDy", "PrintMuban");
            MaxPrint.Text = configUtils.Read("PrintDy", "MaxPrint");
            KaMaxInt.Text = configUtils.Read("PrintDy", "KaMaxPrint");
            if (MaxPrint.Text == "")
            {
                MaxPrint.Text = "50";
            }
            if (KaMaxInt.Text == "")
            {
                KaMaxInt.Text = "50";
            }

            bool.TryParse(configUtils.Read("PrintDy", "AutoPrint"), out var AutoPrintbool);
            AutoPrint.Checked = AutoPrintbool;

            bool.TryParse(configUtils.Read("UserHello", "StarAutohy"), out var StarAutohybool);
            StarAutohy.Checked = StarAutohybool;

            PrintPort.Text = configUtils.Read("PrintDy", "PrintPort");
            if (PrintPort.Text == "")
            {
                PrintPort.Text = "49190";

            }
            NewPtcim.Text = configUtils.Read("UserHello", "NewPtcim");
            gdtext.Text = configUtils.Read("UserHello", "gdtext");

            var flag1 = int.TryParse(configUtils.Read("System", "TimeChick"), out var itime);
            if (!flag1 || itime <= 0)
            {
                TimerText.Text = "5";
                itime = 5;
                configUtils.Write("System", "TimeChick", TimerText.Text);
                timer1.Interval = itime * 1000;
            }
            else
            {
                TimerText.Text = itime.ToString();
                timer1.Interval = itime * 1000;
            }
            FixedConfig.MaxPrint = int.Parse(MaxPrint.Text);
            FixedConfig.PrintMuban = PrintMuban.Text;
            FixedConfig.PrintPort = PrintPort.Text;
            FixedConfig.PrintName = PrintName.Text;
            FixedConfig.AutoPrint = AutoPrint.Checked;
            FixedConfig.ShopDiyName = dyshopnc.Text;
            FixedConfig.MaxGamePlay = int.Parse(KaMaxInt.Text);



        }
        /// <summary>
        /// ������ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rebootPrintServer_Click(object sender, EventArgs e)
        {
            if (Program.yunx)
            {
                daManager.CloseSendBtp();
            }
            Program.StarPrintApi();




        }

        private void DingList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var clickedItem = DingList.GetItemAt(e.X, e.Y);
                if (clickedItem != null)
                {
                    CListItem = clickedItem;
                }
                else
                {
                    //Debug.WriteLine("δѡ����Ŀ");
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FixedConfig.AllListInt = 0;

        }

        private void ���´�ӡToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var task1 = new Task(() =>
            {
                var oerder = DataTools.GetDyOerderLast(CListItem.Text);
                string bq = "";
                if (oerder.LuckyBag == 1)
                {
                    bq = "��";

                }
                if (oerder.Refund == 1)
                {
                    bq = "��";
                }
                var Queue = DataTools.GetDyQueueList(CListItem.Text);
                if (Queue == null)
                {
                    daManager.Send_btp(oerder, bq, "?", "?", "?", "");
                    MessageBox.Show("����ɹ���", "Dy��ϵͳ");

                }
                else
                {
                    daManager.Send_btp(oerder, bq, Queue.Ranking.ToString(), Queue.RecordsNumber.ToString(), Queue.TodayNumber.ToString(), "");
                    MessageBox.Show("����ɹ���", "Dy��ϵͳ");
                }




            });
            task1.Start();



        }
        /// <summary>
        /// ɨ��鵥
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            Form2 ac = new Form2();

            ac.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                button5_Click(sender, e);
                e.Handled = true;
            }
        }

        private void NewpL_Click(object sender, EventArgs e)
        {

            DyIm.SendDyIm(CListItem.Text, NewPtcim.Text);


            MessageBox.Show("ִ�з��ͣ�", "Dy��ϵͳ");
        }

        private void Gdtx_Click(object sender, EventArgs e)
        {
            DyIm.SendDyIm(CListItem.Text, gdtext.Text);
            MessageBox.Show("ִ�з��ͣ�", "Dy��ϵͳ");
        }

        private void �鿴�����ͳ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 ac = new Form2();
            ac.Show();
            ac.dypint.Text = CListItem.Text;
            ac.button1_Click(sender, e);

        }






        private void ��Ӻ�����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CListItem.Text != null)
            {
                // ��ʾ����򲢻�ȡ�û�����
                string input = Interaction.InputBox("������ԭ��", "��ӵ�������", "");

                var ias = DataTools.GetDyOerderLast(CListItem.Text);

                DataTools.AddUserBlack(ias.buy_userid, CListItem.Text, input);
                MessageBox.Show("��Ӻ������ɹ���", "Dy��ϵͳ");
            }

        }


        /// <summary>
        /// ���ÿ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            GameplayProcessing.Reset();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int chouint = GameplayProcessing.GetNextRandom();

                label13.Text = $"�²���: ��{chouint}��,����ʣ��:{GameplayProcessing.numbers.Count}��";

            }
            catch (InvalidOperationException ex)
            {
                label13.Text = $"�²���: �������ѿգ��벹�������";
            }

        }

        private void ���ƶ�����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CListItem.Text);
            MessageBox.Show("���Ƴɹ���", "Dy��ϵͳ");
        }
        /// <summary>
        /// �鿴��ʷ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormListOrder_Click(object sender, EventArgs e)
        {
            OldOrderFrom ac = new OldOrderFrom();

            ac.Show();
        }
        /// <summary>
        /// �������˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            BlackListFrom ac = new BlackListFrom();

            ac.Show();
        }
        /// <summary>
        /// ��ݴ򵥷ּ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            AutoWebYdForm ac = new AutoWebYdForm();

            ac.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Program.SendWindowsTs();
        }
    }

}