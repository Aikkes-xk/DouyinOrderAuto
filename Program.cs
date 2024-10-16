using CsvHelper;
using CsvHelper.Configuration;
using DyOrderAuto.Config;
using DyOrderAuto.DyMods;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Diagnostics;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.Xml;

namespace DyOrderAuto
{
    internal static class Program
    {
        internal static bool yunx;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
         static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }




        internal static void StarLoad() 
        {

            //设置抖音订单列表url
            FixedConfig.DYUrl = "https://fxg.jinritemai.com/ffa/morder/order/list";
            //设置抖音im客服url
            FixedConfig.DyIMUrl = "https://im.jinritemai.com/pc_seller_v2/main/workspace?fromOrder=";
            Form1.Instance.SendLogForm("程序启动成功！已初始化配置！");
            //加载网页并获取新的请求API
            DyManager.DyOrderList.AutoStar(FixedConfig.DYUrl, true, false);


        }

        internal static string ConvertToDecimalFormat(int input)
        {
            // 将整数转换为字符串
            string inputStr = input.ToString();

            // 插入小数点
            if (inputStr.Length > 2)
            {
                inputStr = inputStr.Insert(inputStr.Length - 2, ".");
            }
            else if (inputStr.Length == 2)
            {
                inputStr = "0." + inputStr;
            }
            else if (inputStr.Length == 1)
            {
                inputStr = "0.0" + inputStr;
            }

            return inputStr;
        }

        internal static void StarPrintApi() 
        {
            // 要启动的程序路径
            string programPath = $@"{Environment.CurrentDirectory}\PrintApi\PrintApi.exe";

            // 要传递的命令行参数
            string arguments = $"-Port={FixedConfig.PrintPort}";

            // 创建一个新的进程启动信息
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = programPath,
                Arguments = arguments
            };

            try
            {
                // 启动进程
                using (Process process = Process.Start(startInfo))
                {
                    if (process != null)
                    {

                        var task1 = new Task(() =>
                        {
                            // 可选：等待进程退出并获取退出代码
                            yunx = true;
                            process.WaitForExit();
                            yunx = false;

                        });
                        task1.Start();
                        Form1.Instance.rebootPrintServer.Enabled=false;

                        Form1.Instance.SendLogForm("打印服务已启动！");
                    }
                    else
                    {
                        Form1.Instance.rebootPrintServer.Enabled = true;
                        Form1.Instance.SendLogForm("打印服务启动失败，请检查是否有配置错误！");
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.Instance.rebootPrintServer.Enabled = true;
                Form1.Instance.SendLogForm($"打印服务运行时出现问题！\n {ex.Message}");
            }

        }
        /// <summary>
        /// 排单状态id转换
        /// </summary>
        /// <param name="typeid">排单状态，0正在排单，1过单 2已拆 3直邮</param>
        /// <returns></returns>
        internal static string GetListTypeName(int typeid) 
        {
            switch (typeid) 
            {
                case 0:
                    return "正在排单";
                case 1:
                    return "过单";
                case 2:
                    return "已拆";
                case 3:
                    return "直邮";
                default:
                    return "未知";
            }


        }
        /// <summary>
        /// 取一个随机数
        /// </summary>
        /// <returns></returns>
        internal static int GetSuijiInt() 
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 50);

            return randomNumber;
        }
        /// <summary>
        /// 取随机左右
        /// </summary>
        /// <returns></returns>
        internal static string GetSuijiRL()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);

            if (randomNumber < 50)
            {
                return "左推";
            }
            else 
            {
                return "右推";
            }

        }
        internal static void AddOldOrderListForCsv(string fill) 
        {
            using (var reader = new StreamReader(fill))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // 读取CSV文件中的记录
                var records = csv.GetRecords<DataModel>();

                foreach (var record in records)
                {
                    // 只处理指定列的数据
                    Console.WriteLine($"Column1: {record.Column1}, Column3: {record.Column3}");
                }
            }



        }
        public class DataModel
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
        }

        internal static string DetectShopTypeTuileText(DyOerderListType dyo) 
        {
            //检测是否为推推乐玩法
            if (dyo.shop_name.Contains("保留内容1232131312"))
            {
                int tuint = Program.GetSuijiInt();

                //是推推乐
                if (dyo.shop_typename.Contains("左推"))
                {
                    return $"推推乐:左推 第{tuint}个";

                }
                if (dyo.shop_typename.Contains("右推"))
                {
                    return $"推推乐:右推 第{tuint}个";

                }
                if (dyo.shop_typename.Contains("机选"))
                {
                    string tutext = Program.GetSuijiRL();
                    return $"推推乐:{tutext} 第{tuint}个";
                }
            }

            if (dyo.shop_name.Contains("小欧数")) 
            {
                try
                {
                    int chouint = GameplayProcessing.GetNextRandom();
                    return $"小欧数: 第{chouint}个,池内剩余:{GameplayProcessing.numbers.Count}个";
                }
                catch (InvalidOperationException ex)
                {
                    return $"小欧数:卡池数已空，请补充后重置！";
                }
            }




            //没有检查到特殊玩法
            return "";
        }

        public static void SendWindowsTs() 
        {



        }


    }
}