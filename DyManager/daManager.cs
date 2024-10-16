using DyOrderAuto.Config;
using DyOrderAuto.DyMods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DyOrderAuto.DyManager
{
    internal class daManager
    {
        internal static void CloseSendBtp() 
        {
            GetDyApi.Get_HttpPostApi($"http://127.0.0.1:{FixedConfig.PrintPort}/stop", "");
        }


        internal static void Send_btp(DyOerderListType dolt,string bqtext,string userint, string BuyNum ,string DayNum,string TuiText)
        {
            if (!FixedConfig.AutoPrint) 
            {
                Form1.Instance.SendLogForm($"检测到订单:{dolt.orderid} 未开启自动打印！");

                return;
            }


            string file = $@"{Environment.CurrentDirectory}\PtwMod\{FixedConfig.PrintMuban}";
            try 
            {
                var k = new SendPrint()
                {
                    Pdata = bqtext,
                    PrintName = FixedConfig.PrintName,
                    PrintType = file,
                    DyOerderListType = dolt,
                    Userint = userint,
                    BuyNum = BuyNum,
                    DayNum = DayNum,
                    TuiText = TuiText,

                    DiyShopName = FixedConfig.ShopDiyName
                };

                string jsonString = JsonConvert.SerializeObject(k);
                Debug.WriteLine(jsonString);

                var r = GetDyApi.Get_HttpPostApi($"http://127.0.0.1:{FixedConfig.PrintPort}/PrintDy", jsonString);

                RePrint task = JsonConvert.DeserializeObject<RePrint>(r);

                if (task.code=="100") 
                {
                    //Form1.Instance.SendLogForm($"订单:{dolt.orderid} 打印成功！");
                    return;
                }
                if (task.code == "404")
                {
                    //Form1.Instance.SendLogForm($"订单:{dolt.orderid} 打印异常！错误码：{task.msg}");
                    return;
                }

            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex);
            }


        }
        // 用于发送打印请求的类
        public class PrintRequest
        {
            public string LabelPath { get; set; }
            public string PrinterName { get; set; }
            public string JobName { get; set; }
        }

        // 用于接收打印响应的类
        public class PrintResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }








    }
}
