using DyOrderAuto.Config;
using DyOrderAuto.DataManager;
using DyOrderAuto.DyMods;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DyOrderAuto.DyManager
{
    public class GetDyApi
    {

        public static string ListUrl = "";
        public static string TkListUrl = "";

        public static CookieContainer cookieContainer = new CookieContainer();
        /// <summary>
        /// http请求发送
        /// </summary>
        /// <param name="url">访问地址</param>
        /// <returns></returns>
        public static string Get_HttpApi(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 5000;
                request.CookieContainer = cookieContainer;
                // 设置User-Agent头部信息，伪装成Chrome浏览器
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var asss = reader.ReadToEnd();
                    response.Close();
                    reader.Close();
                    return asss;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// httpPost请求发送
        /// </summary>
        /// <param name="url">访问地址</param>
        /// <returns></returns>
        public static string Get_HttpPostApi(string url,string data)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 5000;
                // 将POST数据转换为字节数组
                byte[] byteArray = Encoding.UTF8.GetBytes(data);

                // 设置请求的ContentLength属性
                request.ContentLength = byteArray.Length;

                // 获取请求流并写入数据
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                // 设置User-Agent头部信息，伪装成Chrome浏览器
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var asss = reader.ReadToEnd();
                    response.Close();
                    reader.Close();
                    return asss;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void GetUserInfo() 
        {
            string url = "https://fuwu.jinritemai.com/api/login/info/v2";
            string backinfo = Get_HttpApi(url);
            if (backinfo == null || backinfo == string.Empty) 
            {
                Form1.Instance.SendLogForm("用户信息加载异常！");
                return;
            }
            var json = JObject.Parse(backinfo);
            string code = json["code"].ToString();
            if (code!="0") 
            {
                Form1.Instance.SendLogForm($"用户信息加载异常！{json["message"]}");
                return;

            }
            string ShopName = json["data"]["shopInfo"]["shop_name"].ToString();

            string ShopIconUrl = json["data"]["shopInfo"]["shop_logo"].ToString();

            Form1.Instance.SendUserInfo(ShopIconUrl, ShopName);
        }
        /// <summary>
        /// 检测是否有新订单出现
        /// </summary>
        public static void GetOrderList () 
        {
            string backinfo = Get_HttpApi(ListUrl);
            Debug.WriteLine(backinfo);

            if (backinfo == null || backinfo == string.Empty)
            {
                Form1.Instance.SendLogForm("订单获取发生异常！");
                return;
            }
            var json = JObject.Parse(backinfo);
            string code = json["code"].ToString();
            if (code == "10001010A") 
            {
                Form1.Instance.SendLogForm($"请重新登入：{json["msg"]}");
                return;
            }
            if (code != "0")
            {
                Form1.Instance.SendLogForm($"订单获取可能发生异常：{json["msg"]}");
            }
            var data = json["data"].ToList();


            
            foreach ( var item in data ) 
            {
                //读取订单信息
                string order = item["shop_order_id"].ToString();
                if (!DataTools.GetOrderExist(order))
                {
                    try
                    {
                        //检测是否为抽奖
                        int LuckyBag = 0;
                        if (item.SelectToken("shop_order_tag") != null)
                        {
                            if (item.SelectToken("shop_order_tag").ToString().Contains("抽奖"))
                            {

                                LuckyBag = 1;
                            }

                        }

                        var BuyUser = GetUserName(item["shop_order_id"].ToString());


                        //特殊玩法文本
                        string tuile = "";

                        DyOerderListType dyo = new DyOerderListType()
                        {
                            orderid = item["shop_order_id"].ToString(),
                            shop_id = item["product_item"][0]["product_id"].ToString(),
                            shop_name = item["product_item"][0]["product_name"].ToString(),
                            shop_typename = item["product_item"][0]["properties"][0]["text"].ToString(),
                            shop_amount = item["product_count"].ToString(),
                            pay_shopamount = Program.ConvertToDecimalFormat((int)item["product_item"][0]["combo_amount"]),
                            pay_sf = Program.ConvertToDecimalFormat((int)item["pay_amount"]),
                            pay_yf = Program.ConvertToDecimalFormat((int)item["pay_amount_detail"][0]["amount_int"]),
                            pay_yh = "0",
                            buy_nickname = BuyUser.UserName,
                            buy_userid = item["user_id"].ToString(),
                            buy_sh_name = item["receiver_info"]["post_receiver"].ToString(),
                            buy_sh_telephonenumber = item["receiver_info"]["post_tel"].ToString(),
                            buy_address = $"{item["receiver_info"]["post_addr"]["province"]["name"]}{item["receiver_info"]["post_addr"]["city"]["name"]}{item["receiver_info"]["post_addr"]["town"]["name"]}{item["receiver_info"]["post_addr"]["street"]["name"]}{item["receiver_info"]["post_addr"]["detail"]}",
                            buy_message = item["buyer_words"].ToString(),
                            buy_Time = DateTime.Parse(item["create_time_str"].ToString()),
                            LuckyBag = LuckyBag,
                            Refund = 0,
                            Updata_Time = DateTime.Now,

                        };

                        tuile = Program.DetectShopTypeTuileText(dyo);

                        //累计下单
                        int userint = 0;
                        int BuyNum = 0;
                        int DayNum = 0;



                        //检测是否是新买家
                        DyUserList userinfo = DataTools.GetUserList(dyo.buy_userid);

                        if (userinfo == null)
                        {
                            BuyNum = 1;
                            DayNum = 1;
                            //是新买家
                            DyUserList dyUser = new DyUserList()
                            {
                                Userid = dyo.buy_userid,
                                UserName = BuyUser.UserName,
                                UserAvateIcon = BuyUser.avatar_url,
                                RecordsNumber = BuyNum,
                                TodayNumber = DayNum,
                                ListTime = DateTime.Parse(item["create_time_str"].ToString())
                            };
                            //新买家添加
                            DataTools.ReadAddNewUser(dyUser);
                            if (LuckyBag != 1)
                            {
                                //触发订单信息
                                DyEvent.NewUserDing(dyo, userinfo);
                            }

                        }
                        else //之前有过下单的
                        {
                            //增加数量
                            userinfo.RecordsNumber++;
                            //检测是否为同一天的更新时间
                            if (userinfo.ListTime.Date == DateTime.Now.Date)
                            {
                                //今日下单次数加一
                                userinfo.TodayNumber++;

                            }
                            else
                            {
                                //今日下单时间重置为1
                                userinfo.TodayNumber = 1;
                            }
                            userinfo.ListTime = DateTime.Parse(item["create_time_str"].ToString());

                            BuyNum = userinfo.RecordsNumber;
                            DayNum = userinfo.TodayNumber;
                            userinfo.UserName = BuyUser.UserName;
                            userinfo.UserAvateIcon = BuyUser.avatar_url;
                            //更新买家记录信息
                            DataTools.UpdataUserList(dyo.buy_userid, userinfo.UserName, BuyNum.ToString(), userinfo.ListTime.ToString(), DayNum.ToString());
                        }
                        userint = FixedConfig.AllListInt + 1;
                        FixedConfig.AllListInt++;
                        //添加排单信息
                        DyQueueList dataqueue = new DyQueueList()
                        {
                            BuyTime = dyo.buy_Time,
                            Demolished = 0,
                            Orderid = dyo.orderid,
                            OverOrder = 0,
                            Ranking = userint,
                            RecordsNumber = BuyNum,
                            TodayNumber = DayNum,

                        };
                        //添加排单信息
                        DataTools.AddQueueList(dataqueue);
                        //触发订单信息
                        DyEvent.NewOrderEvent(dyo, dataqueue, LuckyBag, userinfo);




                        if (DataTools.ReadAddOrder(dyo))
                        {
                            var black = DataTools.GetUserInBlackList(userint.ToString());
                            if (black)
                            {
                                //处理黑名单
                                Form1.Instance.SendLogForm($"发现新订单：{order},买家：{dyo.buy_nickname},#{userint}，黑名单买家！");
                                daManager.Send_btp(dyo, "黑", userint.ToString(), BuyNum.ToString(), DayNum.ToString(), tuile);

                            }
                            else if (LuckyBag == 1)
                            {
                                Form1.Instance.SendLogForm($"发现新订单：{order},买家：{dyo.buy_nickname},#{userint}");
                                daManager.Send_btp(dyo, "福", userint.ToString(), BuyNum.ToString(), DayNum.ToString(), tuile);
                            }
                            else
                            {
                                if (dyo.shop_typename.Contains("直邮"))
                                {
                                    Form1.Instance.SendLogForm($"发现新订单：{order},买家：{dyo.buy_nickname},#{userint}");
                                    daManager.Send_btp(dyo, "直", userint.ToString(), BuyNum.ToString(), DayNum.ToString(), tuile);

                                }
                                else
                                {
                                    Form1.Instance.SendLogForm($"发现新订单：{order},买家：{dyo.buy_nickname},#{userint}");
                                    daManager.Send_btp(dyo, "", userint.ToString(), BuyNum.ToString(), DayNum.ToString(), tuile);

                                }

                            }

                        }

                        var product_item = item["product_item"].ToList();
                        //如果大于1，将剩下的子单打印出来
                        if (product_item.Count > 1)
                        {
                            //从1开始计算
                            for (int i = 1; i < product_item.Count; i++)
                            {
                                DyOerderListType dyo2 = new DyOerderListType()
                                {
                                    orderid = item["shop_order_id"].ToString(),
                                    shop_id = item["product_item"][i]["product_id"].ToString(),
                                    shop_name = item["product_item"][i]["product_name"].ToString(),
                                    shop_typename = item["product_item"][i]["properties"][0]["text"].ToString(),
                                    shop_amount = item["product_count"].ToString(),
                                    pay_shopamount = Program.ConvertToDecimalFormat((int)item["product_item"][i]["combo_amount"]),
                                    pay_sf = Program.ConvertToDecimalFormat((int)item["pay_amount"]),
                                    pay_yf = Program.ConvertToDecimalFormat((int)item["pay_amount_detail"][0]["amount_int"]),
                                    pay_yh = "0",
                                    buy_nickname = BuyUser.UserName,
                                    buy_userid = item["user_id"].ToString(),
                                    buy_sh_name = item["receiver_info"]["post_receiver"].ToString(),
                                    buy_sh_telephonenumber = item["receiver_info"]["post_tel"].ToString(),
                                    buy_address = $"{item["receiver_info"]["post_addr"]["province"]["name"]}{item["receiver_info"]["post_addr"]["city"]["name"]}{item["receiver_info"]["post_addr"]["town"]["name"]}{item["receiver_info"]["post_addr"]["street"]["name"]}{item["receiver_info"]["post_addr"]["detail"]}",
                                    buy_message = item["buyer_words"].ToString(),
                                    buy_Time = DateTime.Parse(item["create_time_str"].ToString()),
                                    LuckyBag = LuckyBag,
                                    Refund = 0,
                                    Updata_Time = DateTime.Now,

                                };

                                tuile = Program.DetectShopTypeTuileText(dyo2);

                                var black = DataTools.GetUserInBlackList(userint.ToString());
                                if (black)
                                {
                                    //处理黑名单
                                    Form1.Instance.SendLogForm($"发现新订单：{order},的子单，黑名单买家！");
                                    daManager.Send_btp(dyo, "黑", userint.ToString(), BuyNum.ToString(), DayNum.ToString(), tuile);

                                }
                                else if (LuckyBag == 1)
                                {
                                    Form1.Instance.SendLogForm($"打印订单：{order} 的子单{i}");
                                    daManager.Send_btp(dyo2, "福", userint.ToString() + "子单", BuyNum.ToString(), DayNum.ToString(), tuile);
                                }
                                else
                                {
                                    if (dyo2.shop_typename.Contains("直邮"))
                                    {
                                        Form1.Instance.SendLogForm($"打印订单：{order} 的子单{i}");
                                        daManager.Send_btp(dyo2, "直", userint.ToString() + "子单", BuyNum.ToString(), DayNum.ToString(), tuile);

                                    }
                                    else
                                    {
                                        Form1.Instance.SendLogForm($"打印订单：{order} 的子单{i}");
                                        daManager.Send_btp(dyo2, "", userint.ToString() + "子单", BuyNum.ToString(), DayNum.ToString(), tuile);

                                    }
                                }




                            }






                        }






                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }

                }


            }

        }
        /// <summary>
        /// 检测是否有傻逼退款
        /// </summary>
        public static void GetdkOrderList()
        {
            string backinfo = Get_HttpApi(TkListUrl);
            if (backinfo == null || backinfo == string.Empty)
            {
                Form1.Instance.SendLogForm("订单获取发生异常！");
                return;
            }
            var json = JObject.Parse(backinfo);
            string code = json["code"].ToString();
            if (code == "10001010A")
            {
                Form1.Instance.SendLogForm($"请重新登入：{json["msg"]}");
                return;
            }
            var data = json["data"].ToList();
            foreach (var item in data)
            {
                string order = item["shop_order_id"].ToString();
                //检测是否为退款订单
                if (!DataTools.GetTuidan(order))
                {
                    try
                    {
                        int LuckyBag = 0;
                        if (item.SelectToken("shop_order_tag") != null)
                        {
                            if (item.SelectToken("shop_order_tag").ToString().Contains("抽奖"))
                            {

                                LuckyBag = 1;
                            }

                        }






                        DyUser user = GetUserName(item["shop_order_id"].ToString());
                        DyOerderListType dyo = new DyOerderListType()
                        {
                            orderid = item["shop_order_id"].ToString(),
                            shop_id = item["product_item"][0]["product_id"].ToString(),
                            shop_name = item["product_item"][0]["product_name"].ToString(),
                            shop_typename = item["product_item"][0]["properties"][0]["text"].ToString(),
                            shop_amount = item["product_count"].ToString(),
                            pay_shopamount = Program.ConvertToDecimalFormat((int)item["product_item"][0]["combo_amount"]),
                            pay_sf = Program.ConvertToDecimalFormat((int)item["pay_amount"]),
                            pay_yf = Program.ConvertToDecimalFormat((int)item["pay_amount_detail"][0]["amount_int"]),
                            pay_yh = "0",
                            buy_nickname = user.UserName,
                            buy_userid = item["user_id"].ToString(),
                            buy_sh_name = item["receiver_info"]["post_receiver"].ToString(),
                            buy_sh_telephonenumber = item["receiver_info"]["post_tel"].ToString(),
                            buy_address = $"{item["receiver_info"]["post_addr"]["province"]["name"]}{item["receiver_info"]["post_addr"]["city"]["name"]}{item["receiver_info"]["post_addr"]["town"]["name"]}{item["receiver_info"]["post_addr"]["street"]["name"]}{item["receiver_info"]["post_addr"]["detail"]}",
                            buy_message = item["buyer_words"].ToString(),
                            buy_Time = DateTime.Parse(item["create_time_str"].ToString()),
                            LuckyBag = LuckyBag,
                            Refund = 1,
                            Updata_Time = DateTime.Now,
                        };
                        
                        //更新退单信息
                        if (!DataTools.UpdataOrderList(order,"1", DateTime.Now.ToString())) 
                        {
                            Form1.Instance.SendLogForm($"更新退单订单信息时发生异常！异常订单：{order}");
                        }
                        //获取排单信息
                        DyQueueList queue = DataTools.GetDyQueueList(order);
                        //触发退单事件
                        DyEvent.NewTOrderEvent(dyo, queue, user);

                        if (queue == null)
                        {
                            //修改排单信息为售后
                            //DataTools.UpdataQueueList(order, "1");
                            DataTools.ReadAddOrder(dyo);
                            Form1.Instance.SendLogForm($"发现退款订单：{order}，未发现排单信息！请注意检查！");
                            daManager.Send_btp(dyo, "退", "?", "?", "?","");
                        }
                        else 
                        {
                            //修改排单信息为售后
                            DataTools.UpdataQueueList(order, "1");
                            Form1.Instance.SendLogForm($"发现退款订单：{order}");
                            daManager.Send_btp(dyo, "退", queue.Ranking.ToString(), queue.RecordsNumber.ToString(), queue.TodayNumber.ToString(),"");
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }

                }

            }


        }


        /// <summary>
        /// 通过订单号获取用户昵称和信息！
        /// </summary>
        /// <param name="dingcom"></param>
        /// <returns></returns>
        internal static DyUser GetUserName(string dingcom)
        {
            var back = Get_HttpApi($"https://pigeon.jinritemai.com/backstage/getuserbyorder?oid={dingcom}");

            var json = JObject.Parse(back);

            try 
            {
                string code = json["code"].ToString();
                if (code!="10026" || code == "0") 
                {
                    Form1.Instance.SendLogForm($"用户信息读取时发生错误，错误原因：{json["msg"]},错误订单：{dingcom}");
                    return null;
                }
                DyUser dyuser = new DyUser() 
                {
                    UserID = ulong.Parse (json["data"]["id"].ToString()),
                    UserName = json["data"]["screen_name"].ToString(),
                    avatar_url = json["data"]["avatar_url"].ToString()
                };
                return dyuser;
            }
            catch 
            {
                Form1.Instance.SendLogForm($"用户信息读取时发生错误！错误订单：{dingcom}");
                return null;
            }

        }
    }
}
