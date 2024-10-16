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

            //���ö��������б�url
            FixedConfig.DYUrl = "https://fxg.jinritemai.com/ffa/morder/order/list";
            //���ö���im�ͷ�url
            FixedConfig.DyIMUrl = "https://im.jinritemai.com/pc_seller_v2/main/workspace?fromOrder=";
            Form1.Instance.SendLogForm("���������ɹ����ѳ�ʼ�����ã�");
            //������ҳ����ȡ�µ�����API
            DyManager.DyOrderList.AutoStar(FixedConfig.DYUrl, true, false);


        }

        internal static string ConvertToDecimalFormat(int input)
        {
            // ������ת��Ϊ�ַ���
            string inputStr = input.ToString();

            // ����С����
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
            // Ҫ�����ĳ���·��
            string programPath = $@"{Environment.CurrentDirectory}\PrintApi\PrintApi.exe";

            // Ҫ���ݵ������в���
            string arguments = $"-Port={FixedConfig.PrintPort}";

            // ����һ���µĽ���������Ϣ
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = programPath,
                Arguments = arguments
            };

            try
            {
                // ��������
                using (Process process = Process.Start(startInfo))
                {
                    if (process != null)
                    {

                        var task1 = new Task(() =>
                        {
                            // ��ѡ���ȴ������˳�����ȡ�˳�����
                            yunx = true;
                            process.WaitForExit();
                            yunx = false;

                        });
                        task1.Start();
                        Form1.Instance.rebootPrintServer.Enabled=false;

                        Form1.Instance.SendLogForm("��ӡ������������");
                    }
                    else
                    {
                        Form1.Instance.rebootPrintServer.Enabled = true;
                        Form1.Instance.SendLogForm("��ӡ��������ʧ�ܣ������Ƿ������ô���");
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.Instance.rebootPrintServer.Enabled = true;
                Form1.Instance.SendLogForm($"��ӡ��������ʱ�������⣡\n {ex.Message}");
            }

        }
        /// <summary>
        /// �ŵ�״̬idת��
        /// </summary>
        /// <param name="typeid">�ŵ�״̬��0�����ŵ���1���� 2�Ѳ� 3ֱ��</param>
        /// <returns></returns>
        internal static string GetListTypeName(int typeid) 
        {
            switch (typeid) 
            {
                case 0:
                    return "�����ŵ�";
                case 1:
                    return "����";
                case 2:
                    return "�Ѳ�";
                case 3:
                    return "ֱ��";
                default:
                    return "δ֪";
            }


        }
        /// <summary>
        /// ȡһ�������
        /// </summary>
        /// <returns></returns>
        internal static int GetSuijiInt() 
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 50);

            return randomNumber;
        }
        /// <summary>
        /// ȡ�������
        /// </summary>
        /// <returns></returns>
        internal static string GetSuijiRL()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);

            if (randomNumber < 50)
            {
                return "����";
            }
            else 
            {
                return "����";
            }

        }
        internal static void AddOldOrderListForCsv(string fill) 
        {
            using (var reader = new StreamReader(fill))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // ��ȡCSV�ļ��еļ�¼
                var records = csv.GetRecords<DataModel>();

                foreach (var record in records)
                {
                    // ֻ����ָ���е�����
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
            //����Ƿ�Ϊ�������淨
            if (dyo.shop_name.Contains("��������1232131312"))
            {
                int tuint = Program.GetSuijiInt();

                //��������
                if (dyo.shop_typename.Contains("����"))
                {
                    return $"������:���� ��{tuint}��";

                }
                if (dyo.shop_typename.Contains("����"))
                {
                    return $"������:���� ��{tuint}��";

                }
                if (dyo.shop_typename.Contains("��ѡ"))
                {
                    string tutext = Program.GetSuijiRL();
                    return $"������:{tutext} ��{tuint}��";
                }
            }

            if (dyo.shop_name.Contains("Сŷ��")) 
            {
                try
                {
                    int chouint = GameplayProcessing.GetNextRandom();
                    return $"Сŷ��: ��{chouint}��,����ʣ��:{GameplayProcessing.numbers.Count}��";
                }
                catch (InvalidOperationException ex)
                {
                    return $"Сŷ��:�������ѿգ��벹������ã�";
                }
            }




            //û�м�鵽�����淨
            return "";
        }

        public static void SendWindowsTs() 
        {



        }


    }
}