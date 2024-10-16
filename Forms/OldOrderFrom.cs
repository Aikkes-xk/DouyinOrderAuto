using DyOrderAuto.DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DyOrderAuto.Forms
{
    public partial class OldOrderFrom : Form
    {

        internal static string WhereText = "";


        internal static int oerderListy = 1;
        internal static int maxlistty = 1;



        public OldOrderFrom()
        {
            InitializeComponent();
        }

        private void OldOrderFrom_Load(object sender, EventArgs e)
        {
            //初始化历史订单信息
            LoadOldOrderList(oerderListy);
        }



        private void LoadOldOrderList(int cont)
        {
            //获取行总数
            float zsl = DataTools.GetListSl("DyOrderList", WhereText);
            //前
            int c_q = (cont - 1) * 40;
            //后
            int c_h = cont * 40 - c_q;
            //总页数
            double jsz = zsl / 40;
            int zys = (int)Math.Ceiling(jsz);
            maxlistty = zys;
            //输入页数信息
            Ding_F.Text = $"{cont}/{zys}";


            var las = DataTools.GetDyOerderLists(c_q, 40, WhereText);

            OldOrderListView.Items.Clear();


            foreach (var item in las)
            {
                // 创建新的 ListViewItem
                ListViewItem dinga = new ListViewItem();

                // 添加子项
                //添加唯一标识符
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
                    dinga.SubItems.Add("无排单信息");
                    dinga.SubItems.Add("无排单信息");
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
                OldOrderListView.Items.Add(dinga);

            }






        }



        /// <summary>
        /// 历史订单上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ding_Upper_Click(object sender, EventArgs e)
        {
            if (oerderListy <= 1)
            {


            }
            else
            {
                oerderListy--;
                //初始化历史订单信息
                LoadOldOrderList(oerderListy);
            }
        }
        /// <summary>
        /// 历史订单下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ding_Next_Click(object sender, EventArgs e)
        {
            if (oerderListy > maxlistty)
            {


            }
            else
            {
                oerderListy++;
                //初始化历史订单信息
                LoadOldOrderList(oerderListy);
            }
        }

        private void OldOrderListView_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
