using DyOrderAuto.DyManager;
using DyOrderAuto.DyMods;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace DyOrderAuto.DataManager
{
    internal class DataTools
    {

        internal static void StarSql() 
        {
            sqlDataManager.StarSql();

                //初始化订单表
                sqlDataManager.initTableLuckDraw("DyOrderList", "CREATE TABLE 'main'.'DyOrderList'(" +
                    " 'orderid' text NOT NULL,'shop_id' text," +
                    " 'shop_name' TEXT," +
                    "'shop_typename' TEXT," +
                    "'shop_amount' TEXT," +
                    "'pay_shopamount' TEXT," +
                    " 'pay_sf' TEXT," +
                    " 'pay_yf' TEXT, " +
                    " 'pay_yh' TEXT, " +
                    " 'buy_nickname' TEXT," +
                    "  'buy_userid' text, " +
                    " 'buy_sh_name' TEXT," +
                    "  'buy_sh_telephonenumber' text, " +
                    " 'buy_address' TEXT," +
                    "  'buy_message' TEXT, " +
                    " 'buy_Time' TEXT, " +
                    " 'LuckyBag' integer, " +
                    " 'Refund' integer,  " +
                    " 'Updata_Time' TEXT, " +
                    "PRIMARY KEY('orderid'))");

            sqlDataManager.initTableLuckDraw("DyUserList", "CREATE TABLE 'main'.'DyUserList' (" +
                "  'Userid' text NOT NULL," +
                "  'UserName' TEXT," +
                "  'UserAvateIcon' TEXT, " +
                " 'RecordsNumber' INTEGER, " +
                " 'ListTime' TEXT," +
                "  'TodayNumber' INTEGER, " +
                " PRIMARY KEY ('Userid'));");

            sqlDataManager.initTableLuckDraw("DyQueueList", "CREATE TABLE 'main'.'DyQueueList' ( " +
                " 'Orderid' text NOT NULL," +
                "  'BuyTime' TEXT, " +
                " 'Ranking' integer,  " +
                "'OverOrder' integer, " +
                "'Demolished' integer, " +
                "'TodayNumber' integer, " +
                "'RecordsNumber' integer, " +
                " PRIMARY KEY ('Orderid'));");



            sqlDataManager.initTableLuckDraw("DyBlackUser", "CREATE TABLE \"main\".\"DyBlackUser\" ( " +
                " \"Userid\" TEXT NOT NULL, " +
                " \"BuyTime\" TEXT, " +
                " \"BlackType\" TEXT, " +
                " \"BlackOrderid\" TEXT, " +
                " \"BlackText\" TEXT, " +
                " \"AddTime\" TEXT, " +
                " PRIMARY KEY (\"Userid\")" +
                ");");


        }
        /// <summary>
        /// 获取订单id是否已存在于数据库中
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <returns>返回是否</returns>
        internal static bool GetOrderExist(string orderID)
        {
            string sql = $"SELECT * FROM \"DyOrderList\"  WHERE \"orderid\"= '{orderID}';";
            
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                if (result.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

        


        /// <summary>
        /// 写入订单
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <returns>返回是否</returns>
        internal static bool ReadAddOrder(DyOerderListType dytype)
        {
            string sql = $"INSERT INTO \"main\".\"DyOrderList\"" +
                $"(\"orderid\", \"shop_id\", \"shop_name\", \"shop_typename\", \"shop_amount\", \"pay_shopamount\", \"pay_sf\", \"pay_yf\", \"pay_yh\", \"buy_nickname\", \"buy_userid\", \"buy_sh_name\", \"buy_sh_telephonenumber\", \"buy_address\", \"buy_message\", \"buy_Time\", \"LuckyBag\", \"Refund\", \"Updata_Time\") " +
                $"VALUES ('{dytype.orderid}', '{dytype.shop_id}', '{dytype.shop_name}', '{dytype.shop_typename}', '{dytype.shop_amount}', '{dytype.pay_shopamount}', '{dytype.pay_sf}', '{dytype.pay_yf}', '{dytype.pay_yh}', '{dytype.buy_nickname}', '{dytype.buy_userid}', '{dytype.buy_sh_name}', '{dytype.buy_sh_telephonenumber}', '{dytype.buy_address}', '{dytype.buy_message}', '{dytype.buy_Time}', {dytype.LuckyBag}, {dytype.Refund},'{dytype.Updata_Time}')";
            //Debug.WriteLine(sql);
            var result = sqlDataManager.ExecuteQuery(EQueryType.NonQuery, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检测是否已存在退单记录
        /// </summary>
        /// <param name="dyid"></param>
        /// <returns></returns>
        internal static bool GetTuidan(string dyid)
        {
            string sql = $"SELECT * FROM \"main\".\"DyOrderList\" WHERE \"orderid\" = '{dyid}' AND \"Refund\" = '1';";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                if (result.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新退单信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="Refund"></param>
        /// <param name="Updata_Time"></param>
        /// <returns></returns>
        internal static bool UpdataOrderList(string orderid, string Refund,string Updata_Time) 
        {
            string sql = $"UPDATE \"main\".\"DyOrderList\" SET \"Refund\" ={Refund},\"Updata_Time\" = '{Updata_Time}' WHERE \"orderid\" = '{orderid}';";

            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户信息更新
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="RecordsNumber"></param>
        /// <param name="ListTime"></param>
        /// <param name="TodayNumber"></param>
        /// <returns></returns>
        internal static bool UpdataUserList(string UserId, string UserName, string RecordsNumber,string ListTime,string TodayNumber)
        {
            string sql =$"UPDATE \"main\".\"DyUserList\" SET \"UserName\" = \"{UserName}\" ,\"RecordsNumber\" = '{RecordsNumber}',\"ListTime\" = '{ListTime}',\"TodayNumber\" = '{TodayNumber}' WHERE \"Userid\" = {UserId};";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="dytype"></param>
        /// <returns></returns>
        internal static bool ReadAddNewUser(DyUserList dytype)
        {
            string sql = $"INSERT INTO \"main\".\"DyUserList\"" +
                $"(\"Userid\", \"UserName\", \"UserAvateIcon\", \"RecordsNumber\", \"ListTime\", \"TodayNumber\") " +
                $"VALUES ('{dytype.Userid}', '{dytype.UserName}', '{dytype.UserAvateIcon}', '{dytype.RecordsNumber}', '{dytype.ListTime}', '{dytype.TodayNumber}')";
            
            var result = sqlDataManager.ExecuteQuery(EQueryType.NonQuery, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="dyid"></param>
        /// <returns></returns>
        internal static DyOerderListType GetDyOerderLast(string dyid) 
        {
            string sql;
            sql = $"SELECT * FROM \"main\".\"DyOrderList\" WHERE \"orderid\" = '{dyid}'";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyOerderListType tasks = JsonConvert.DeserializeObject<DyOerderListType>(jsonString);
                    return tasks;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定日期的订单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal static List<DyOerderListType> GetDayOerderList(DateTime time) 
        {
            string sql;
            List<DyOerderListType> back = new List<DyOerderListType>();
            sql = $"SELECT * FROM \"main\".\"DyOrderList\"";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyOerderListType tasks = JsonConvert.DeserializeObject<DyOerderListType>(jsonString);

                    if (tasks.buy_Time.Date == time.Date ) 
                    {
                        back.Add(tasks);
                    }
                }
            }
            return back;

        }
        /// <summary>
        /// 读取指定id的用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        internal static DyUserList GetUserList(string UserID) 
        {

            string sql;
            sql = $"SELECT * FROM \"main\".\"DyUserList\" WHERE \"Userid\" = '{UserID}'";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyUserList tasks = JsonConvert.DeserializeObject<DyUserList>(jsonString);
                    return tasks;
                }
            }
            return null;
        }
        
        /// <summary>
        /// 添加排单信息到数据库中
        /// </summary>
        /// <param name="dyq"></param>
        /// <returns></returns>
        internal static bool AddQueueList(DyQueueList dyq ) 
        {
            string sql = $"INSERT INTO \"main\".\"DyQueueList\"" +
                $"(\"Orderid\", \"BuyTime\", \"Ranking\", \"OverOrder\", \"Demolished\",\"RecordsNumber\",\"TodayNumber\") " +
                $"VALUES ('{dyq.Orderid}', '{dyq.BuyTime}', '{dyq.Ranking}', '{dyq.OverOrder}', '{dyq.Demolished}', '{dyq.RecordsNumber}', '{dyq.TodayNumber}')";
            var result = sqlDataManager.ExecuteQuery(EQueryType.NonQuery, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 更新排单售后状态
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <param name="demolished">订单状态，0正常 1售后 </param>
        /// <returns></returns>
        internal static bool UpdataQueueList (string orderid,string demolished)
        {
            string sql = $"UPDATE \"main\".\"DyQueueList\" SET \"Demolished\" = ' {demolished}' WHERE \"Orderid\" = {orderid};";

            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新排单状态
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <param name="OverOrder">排单状态，0正在排单，1过单 2已拆 3直邮</param>
        /// <returns></returns>
        internal static bool UpdataQueueList(string orderid, int OverOrder)
        {
            string sql = $"UPDATE \"main\".\"DyQueueList\" SET \"OverOrder\" = ' {OverOrder}' WHERE Orderid = {orderid};";

            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取排单信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        internal static DyQueueList GetDyQueueList(string orderid) 
        {
            string sql;
            sql = $"SELECT * FROM \"main\".\"DyQueueList\" WHERE \"Orderid\" = '{orderid}'";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyQueueList tasks = JsonConvert.DeserializeObject<DyQueueList>(jsonString);
                    return tasks;
                }
            }
            return null;
        }


        /// <summary>
        /// 获取用户订单信息
        /// </summary>
        /// <param name="userint"></param>
        /// <returns></returns>
        internal static List<DyOerderListType> GetUserOrderList(string userint) 
        {
            string sql;
            List<DyOerderListType> back = new List<DyOerderListType>();
            sql = $"SELECT * FROM \"main\".\"DyOrderList\" WHERE \"buy_userid\" = '{userint}'";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyOerderListType tasks = JsonConvert.DeserializeObject<DyOerderListType>(jsonString);
                    back.Add(tasks);
                    
                }
            }
            return back;


        }
        /// <summary>
        /// 检测用户是否存在于黑名单中
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal static bool GetUserInBlackList(string userid) 
        {
            string sql;
            sql = $"SELECT * FROM \"main\".\"DyBlackUser\" WHERE \"Userid\" = '{userid}' AND \"BlackType\" = '1';";

            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyBlackUser tasks = JsonConvert.DeserializeObject<DyBlackUser>(jsonString);

                    if (tasks.Userid == userid) 
                    {
                        return true;
                    }

                }
            }
            return false;

        }
        /// <summary>
        /// 将用户添加到黑名单中
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="BlackOrderid"></param>
        /// <param name="BlackText"></param>
        /// <returns></returns>
        internal static bool AddUserBlack(string userid,string BlackOrderid,string BlackText ) 
        {

            string sql = $"INSERT INTO \"main\".\"DyBlackUser\"" +
                $"(\"Userid\", \"BlackType\", \"BlackOrderid\", \"BlackText\", \"AddTime\")  " +
                $"VALUES ('{userid}', '1', '{BlackOrderid}', '{BlackText}', '{DateTime.Now}')";
            var result = sqlDataManager.ExecuteQuery(EQueryType.NonQuery, sql);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal static List<DyBlackUser> GetBlackUserList()
        {
            string sql;
            List<DyBlackUser> back = new List<DyBlackUser>();
            sql = $"SELECT * FROM \"main\".\"DyBlackUser\" WHERE \"BlackType\" = '1'";
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyBlackUser tasks = JsonConvert.DeserializeObject<DyBlackUser>(jsonString);
                    back.Add(tasks);

                }
            }
            return back;


        }
        /// <summary>
        /// 获取指定列表总数量
        /// </summary>
        /// <param name="listname"></param>
        /// <returns></returns>
        internal static int GetListSl(string listname,string where) 
        {
            string sql;
            sql = $"SELECT COUNT(*) AS count FROM `{listname}` {where};";

            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                return int.Parse(result[0]["count"].ToString());
            }
            return 0;

        }
        /// <summary>
        /// 获取全部商品列表，指定查询或指定位置
        /// </summary>
        /// <param name="h_q"></param>
        /// <param name="h_h"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        internal static List<DyOerderListType> GetDyOerderLists(int h_q,int h_h,string where) 
        {

            string sql;
            List<DyOerderListType> back = new List<DyOerderListType>();
            sql = $"SELECT * FROM \"main\".\"DyOrderList\" {where} LIMIT {h_q},{h_h};";
            Debug.WriteLine(sql);
            var result = sqlDataManager.ExecuteQuery(EQueryType.Reader, sql) as List<Dictionary<string, object>>;
            if (result != null && result.Count > 0)
            {
                foreach (var k in result)
                {
                    string jsonString = JsonConvert.SerializeObject(k);
                    DyOerderListType tasks = JsonConvert.DeserializeObject<DyOerderListType>(jsonString);
                    back.Add(tasks);

                }
            }
            return back;


        }



    }
}
