using DyOrderAuto.DataManager;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DyOrderAuto.Forms
{
    public partial class Form2 : Form
    {
        internal string dingid;

        public Form2()
        {
            InitializeComponent();
        }
        //开始查询
        internal void button1_Click(object sender, EventArgs e)
        {
            var d = DataTools.GetDyOerderLast(dypint.Text);
            dypint.Text = "";
            if (d == null)
            {
                UserName.Text = "未查询到买家信息！";
                return;
            }

            dingid = d.orderid.ToString();
            var user = DataTools.GetUserList(d.buy_userid);
            UserName.Text = user.UserName;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(user.UserAvateIcon);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        imicon.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    Form1.Instance.SendLogForm("头像加载异常！");
                }
            }

            var dingdanlist = DataTools.GetUserOrderList(d.buy_userid);
            dingdanlist.Sort((x, y) => y.Updata_Time.CompareTo(x.Updata_Time));
            DingList.Items.Clear();

            decimal xflj = 0;
            decimal dayxf = 0;




            foreach (var item in dingdanlist)
            {

                // 创建新的 ListViewItem
                ListViewItem dinga = new ListViewItem();

                // 添加子项
                //添加唯一标识符
                dinga.Name = item.orderid;
                var queue = DataTools.GetDyQueueList(item.orderid);
                xflj = xflj + decimal.Parse(item.pay_sf);

                //统计今日订单
                if (item.buy_Time.Date == DateTime.Now.Date)
                {
                    dayxf = dayxf + decimal.Parse(item.pay_sf);
                }



                if (queue == null)
                {
                    dinga.Text = item.orderid;
                    dinga.SubItems.Add(item.shop_name);
                    dinga.SubItems.Add(item.shop_typename);
                    dinga.SubItems.Add(item.pay_sf);
                    dinga.SubItems.Add(item.buy_Time.ToString());
                    dinga.SubItems.Add("无排单信息");
                    if (queue.Demolished == 1)
                    {
                        dinga.SubItems.Add("售后");
                    }
                    else
                    {
                        if (item.LuckyBag == 1)
                        {
                            dinga.SubItems.Add("福袋");
                        }
                        else
                        {
                            dinga.SubItems.Add("");
                        }
                    }
                }
                else
                {
                    dinga.Text = item.orderid;
                    dinga.SubItems.Add(item.shop_name);
                    dinga.SubItems.Add(item.shop_typename);
                    dinga.SubItems.Add(item.pay_sf);
                    dinga.SubItems.Add(item.buy_Time.ToString());
                    dinga.SubItems.Add(queue.RecordsNumber.ToString());
                    if (queue.Demolished == 1)
                    {
                        dinga.SubItems.Add("售后");
                    }
                    else
                    {
                        if (item.LuckyBag == 1)
                        {
                            dinga.SubItems.Add("福袋");
                        }
                        else
                        {
                            dinga.SubItems.Add("");
                        }
                    }
                }
                // 将项添加到 ListView
                DingList.Items.Add(dinga);
            }
            Leijibuy.Text = $"累计消费：{xflj}元";
            Leijibuycon.Text = $"累计下单：{user.RecordsNumber}单";
            DayBuy.Text = $"今日下单：{user.TodayNumber}单";
            daybuyint.Text = $"今日消费：{dayxf}元";


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dypint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                button1_Click(sender, e);
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 显示输入框并获取用户输入
            string input = Interaction.InputBox("请输入原因", "添加到黑名单", "");

            var ias = DataTools.GetDyOerderLast(dingid);

            DataTools.AddUserBlack(ias.buy_userid, dingid, input);
            MessageBox.Show("添加黑名单成功！", "Dy打单系统");
        }
    }
}
