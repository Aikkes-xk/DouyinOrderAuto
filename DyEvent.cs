using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DyOrderAuto.DataManager;
using DyOrderAuto.DyManager;
using DyOrderAuto.DyMods;

namespace DyOrderAuto
{
    internal class DyEvent
    {

        internal static void NewOrderEvent(DyOerderListType dyOerderListType, DyQueueList dyQueueList,int LuckyBag, DyUserList userinfo) 
        {
            Form1 form = Form1.Instance;
            //添加项目到列表中
            ListViewItem dinga = new ListViewItem();
            //添加唯一标识符
            dinga.Name = dyOerderListType.orderid;

            if (dyQueueList == null)
            {
                dinga.Text = dyOerderListType.orderid;
                dinga.SubItems.Add(dyOerderListType.shop_name);
                dinga.SubItems.Add(dyOerderListType.shop_typename);
                dinga.SubItems.Add(dyOerderListType.pay_sf);
                dinga.SubItems.Add(dyOerderListType.buy_nickname);
                dinga.SubItems.Add(dyOerderListType.buy_Time.ToString());
                dinga.SubItems.Add("无排单信息");
                dinga.SubItems.Add("无排单信息");
                dinga.SubItems.Add("无排单信息");
                dinga.SubItems.Add("无排单信息");
            }
            else
            {
                dinga.Text = dyOerderListType.orderid;
                dinga.SubItems.Add(dyOerderListType.shop_name);
                dinga.SubItems.Add(dyOerderListType.shop_typename);
                dinga.SubItems.Add(dyOerderListType.pay_sf);
                dinga.SubItems.Add(dyOerderListType.buy_nickname);
                dinga.SubItems.Add(dyOerderListType.buy_Time.ToString());
                dinga.SubItems.Add(dyQueueList.RecordsNumber.ToString());
                dinga.SubItems.Add(dyQueueList.TodayNumber.ToString());
                if (dyQueueList.Demolished == 1)
                {
                    dinga.SubItems.Add("售后");
                }
                else
                {
                    if (dyOerderListType.LuckyBag == 1)
                    {
                        dinga.SubItems.Add("福袋");
                    }
                    else 
                    {
                        dinga.SubItems.Add("");
                    }
                }
                dinga.SubItems.Add(Program.GetListTypeName(dyQueueList.OverOrder));
            }
            form.DingList.Invoke(new Action(() => form.DingList.Items.Insert(0, dinga)));
            //更新数据
            form.UpPaidanInfo();

        }
        internal static void NewTOrderEvent(DyOerderListType dyOerderListType , DyQueueList dyQueueList, DyUser user)
        {
            Form1 form = Form1.Instance;
            //添加项目到列表中

            form.Invoke((Action)(() =>
            {
                var dinga = form.DingList.Items.Find(dyOerderListType.orderid, false);
                foreach (var item in dinga)
                {
                    //检测是否相同
                    if (item.Text == dyOerderListType.orderid)
                    {

                        item.SubItems[8].Text = "售后";
                    }

                }

            }));


        }
        internal static void UpdataPDEvent()
        {



        }


        /// <summary>
        /// 新用户首单
        /// </summary>
        /// <param name="dyOerderListType"></param>
        /// <param name="userinfo"></param>
        internal static void NewUserDing(DyOerderListType dyOerderListType,DyUserList userinfo) 
        {

            DyIm.SendDyIm(dyOerderListType.orderid, Form1.Instance.NewPtcim.Text);




        }

    }
}
