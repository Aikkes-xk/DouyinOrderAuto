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
    public partial class BlackListFrom : Form
    {
        internal static ListViewItem BlackCListItem;
        public BlackListFrom()
        {
            InitializeComponent();
        }

        private void BlackListFrom_Load(object sender, EventArgs e)
        {
            //初始化黑名单列表
            LoadBlackUserList();
        }
        /// <summary>
        /// 初始化黑名单列表
        /// </summary>
        internal void LoadBlackUserList()
        {
            BlackListView.Items.Clear();

            var ass = DataTools.GetBlackUserList();

            foreach (var item in ass)
            {
                // 创建新的 ListViewItem
                ListViewItem dinga = new ListViewItem();

                // 添加子项
                //添加唯一标识符
                dinga.Name = item.Userid;
                var queue = DataTools.GetUserList(item.Userid);
                //检测用户信息是否存在
                if (queue != null)
                {
                    dinga.Text = item.Userid;
                    dinga.SubItems.Add(queue.UserName);

                }
                else
                {
                    dinga.Text = item.Userid;
                    dinga.SubItems.Add("未知用户名");
                }
                //黑名单订单
                dinga.SubItems.Add(item.BlackOrderid);
                dinga.SubItems.Add(item.BlackText);
                dinga.SubItems.Add(item.AddTime.ToString());
                // 将项添加到 ListView
                BlackListView.Items.Add(dinga);

            }
        }


        /// <summary>
        /// 黑名单列表操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlackListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var clickedItem = BlackListView.GetItemAt(e.X, e.Y);
                if (clickedItem != null)
                {
                    BlackCListItem = clickedItem;
                }
                else
                {
                    //Debug.WriteLine("未选中项目");
                }

            }
        }


    }
}
