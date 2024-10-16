
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrintApi.HttpApi
{
    internal class HTTPMain
    {
        /**
         * 获取请求方法名
         * 
         * */
        public static string getMethos(string url)
        {
            //分割获取请求方法名
            int endLength = url.IndexOf('?');
            string method = "";
            try
            {
                if (endLength == -1)
                {
                    method = url.Substring(1, url.Length - 1);
                }
                else
                {
                    method = url.Substring(1, endLength - 1);
                }
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            return method;

        }
        /**
 *获取请求参数
 */
        public static Dictionary<string, object> getParaMap(string url)
        {
            //分割获取请求方法名
            int start = url.LastIndexOf("?");
            int end = url.Length;
            string txt = "";
            txt = url.Substring(start + 1, end - start - 1);
            string[] txt1 = txt.Split('&');
            Dictionary<string, object> map = new Dictionary<string, object>();
            try
            {
                for (int i = 0; i < txt1.Length; i++)
                {
                    string[] str = txt1[i].Split('=');
                    map.Add(str[0], str[1]);
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
            return map;

        }
        public static string Command(HttpListenerRequest request, HttpListenerResponse response)
        {
            // Logger.LogWarning("进入命令分配方法！");
            string obj = "123";
            string url = request.RawUrl;
            string post = request.HttpMethod.ToUpper();
            string method = getMethos(url);
            Dictionary<string, object> map = getParaMap(url);

            //Debug.WriteLine(url, post, method,obj);

            
            return obj;
        }
        public static string PostCommand(HttpListenerRequest request, HttpListenerResponse response,string data)
        {
            // Logger.LogWarning("进入命令分配方法！");
            string obj = "";
            string url = request.RawUrl;
            string method = getMethos(url);
            Dictionary<string, object> map = getParaMap(url);
            if ("stop".Equals(method)) 
            {
                Console.WriteLine("[NGE_Dy打印模块]收到关闭通知，即将关闭！");
                Program.stop=true;
            }
            if ("PrintDy".Equals(method))
            {
                SendPrint task = JsonConvert.DeserializeObject<SendPrint>(data);
                try 
                {
                    obj = Program.Send_btp(task);
                }catch (Exception e) 
                {
                    Console.WriteLine(e.ToString());
                }

                
            }


            return obj;
        }

    }
}

