using Microsoft.SqlServer.Server;
using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace PrintApi
{
    internal class Program
    {
        private static int port;
        public static bool stop;
        public static Program Instance;

        private static void Main(string[] args)
        {

            string baseAddress = "";
            if (args.Length > 0)
            {
                // 遍历并打印每个启动项内容
                foreach (string arg in args)
                {
                    var sparg = arg.Split('=');
                    if (sparg.Length > 2)
                    {
                        continue;
                    }
                    if (sparg[0] == "-Port")
                    {
                        port = int.Parse(sparg[1]);
                    }
                }
                if (port == null ||port == 0)
                {
                    port = 49091;
                }
                Console.WriteLine($"[{DateTime.Now}]:Http服务启动：{port}");
                Console.Title = $"[NGE_Dy打印模块]Dy打印模块运行中请勿关闭！";
                HttpApi.SendHttpUtil.startHttp(port);
                stop = false;
                while (true)
                {
                    Thread.Sleep(1000);
                    if (stop)
                    {
                        Console.WriteLine($"[{DateTime.Now}]进程即将退出！");
                        
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("[ERROR]启动项不足！");
                return;
            }


        }

        internal static string Send_btp(SendPrint adolt)
        {
            string file = adolt.PrintType;

            var dolt = adolt.DyOerderListType;

            try 
            { 


                Engine engine = new Engine();//打印机 引擎
                engine.Start();

                var doc = engine.Documents.Open(file);
                doc.SubStrings["店铺简称"].Value = adolt.DiyShopName;
                doc.SubStrings["订单号"].Value = dolt.orderid;
                doc.SubStrings["用户昵称"].Value = "昵称:" + dolt.buy_nickname;
                doc.SubStrings["商品名"].Value = dolt.shop_name;
                doc.SubStrings["规格"].Value = "规格:" + dolt.shop_typename;
                doc.SubStrings["备注"].Value = "备注:" + dolt.buy_message;
                doc.SubStrings["实付价格"].Value = "实付价格:" + dolt.pay_sf;
                doc.SubStrings["下单时间"].Value = "下单时间:" + dolt.buy_Time.ToString("g");
                doc.SubStrings["今日订单"].Value = $"#{adolt.Userint}";
                doc.SubStrings["收货人"].Value = "收货人:" + dolt.buy_sh_name;
                doc.SubStrings["手机号"].Value = "手机号:" + dolt.buy_sh_telephonenumber;
                doc.SubStrings["大标注"].Value = adolt.Pdata;
                doc.SubStrings["买家今日下单数"].Value = $"今日买家:第{adolt.DayNum}单";
                doc.SubStrings["累计下单"].Value = $"累计下单:{adolt.BuyNum} 单";

                doc.SubStrings["推推乐标记"].Value = $"{adolt.TuiText}";

                doc.PrintSetup.IdenticalCopiesOfLabel = 1;
                var result = doc.Print(adolt.PrintName, out var messages);

                doc.Close(SaveOptions.DoNotSaveChanges);
                engine.Stop();
                
                
                Console.WriteLine($"[{DateTime.Now}]:开始打印订单:{dolt.orderid},买家：{dolt.buy_nickname}");

                var n = new RePrint() 
                {
                    code ="100",
                    msg= $"打印订单:{dolt.orderid} 成功！"


                };
                string jsonString = JsonConvert.SerializeObject(n);
                return jsonString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}]:打印:{dolt.orderid} 发生异常！");
                Console.WriteLine(ex);
                var n = new RePrint()
                {
                    code = "404",
                    msg = $"打印异常：{dolt.orderid}"


                };
                string jsonString = JsonConvert.SerializeObject(n);
                return jsonString;
            }


        }



    }

}

