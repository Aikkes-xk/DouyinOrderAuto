using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Data.Common;
using System.Diagnostics;

namespace DyOrderAuto.DataManager
{
    internal class sqlDataManager
    {
        internal static SQLiteConnection _connection;

        internal static void StarSql() 
        {

            //创建sqldb信息
            _connection = new SQLiteConnection("Data Source=OrderListData.db");

            try 
            {
                //打开
                _connection.Open();
                Form1.Instance.SendLogForm("数据库载入成功！");
                _connection.Close();
            }
            catch (Exception ex) 
            {
                Form1.Instance.SendLogForm("数据库初始化异常！");
            }

        }
        /// <summary>
        /// 检查并创建数据表
        /// </summary>
        /// <param name="dbName"></param>
        /// 
        internal static void initTableLuckDraw(string tableName, string sql)
        {
            var test  = sqlDataManager.ExecuteQuery(EQueryType.Reader, $"select * from sqlite_master where type = 'table' and name = '{tableName}'") as List<Dictionary<string, object>>;

            if (test == null || test.Count ==0)
            {
                int i = (int)ExecuteQuery(EQueryType.NonQuery, sql);
                if (i == -1)
                {
                    Form1.Instance.SendLogForm($"数据表{tableName}初始化发生异常！");
                }
                else
                {
                    Form1.Instance.SendLogForm($"数据表{tableName}初始化成功！");
                }
            }



        }
        /// <summary>
        /// 执行sql语句执行
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static object ExecuteQuery(EQueryType queryType, string query) 
        {
            _connection.Open();
            //检查sql链接是否正常
            if (_connection==null)
            {
                return  -1;
            }
            object result = -1;
            SQLiteCommand command;
            const int maxRetries = 5;
            int retryCount = 0;
            //循环执行
            while (true) 
            {
                //启动事务
                using (var transaction = _connection.BeginTransaction())
                using (command = _connection.CreateCommand())
                {
                    try
                    {
                        //写入要执行的sql语句
                        command.CommandText = query;
                        switch (queryType)
                        {
                            //执行
                            case EQueryType.Scalar:
                                result = command.ExecuteScalar();
                                transaction.Commit();
                                _connection.Close();
                                return result;
                                break;
                                //执行
                            case EQueryType.Reader:
                                var readerResult = new List<Dictionary<string, object>>();

                                var reader = command.ExecuteReader();
                                while (reader.Read())
                                    try
                                    {
                                        var values = new Dictionary<string, object>();

                                        for (var i = 0; i < reader.FieldCount; i++)
                                        {
                                            var columnName = reader.GetName(i);
                                            values.Add(columnName, reader[columnName]);
                                        }

                                        readerResult.Add(values);
                                    }
                                    catch (Exception ex)
                                    {
                                        //发生读写错误
                                        DyOrderAuto.Form1.Instance.SendLogForm(
                                            $"发生错误:Query: \"{query}\"Error: {ex.Message}");
                                        _connection.Close();
                                        return result;
                                    }
                                result = readerResult;
                                transaction.Commit();
                                reader.Close();
                                _connection.Close();
                                return result;
                                break;
                                //执行无请求任务
                            case EQueryType.NonQuery:
                                result = command.ExecuteNonQuery();
                                transaction.Commit();
                                command.Clone();
                                _connection.Close();
                                return result;
                                break;
                        }


                    }
                    //发生特殊错误
                    catch (SQLiteException ex) when (ex.ErrorCode == (int)SQLiteErrorCode.Busy )
                    {
                        if (++retryCount >= maxRetries)
                        {
                            Debug.WriteLine($"在 {maxRetries} 次重试后执行非查询时出错：{ex.Message}");
                            _connection.Close();
                            return -1;
                        }

                        Thread.Sleep(100); // 等待一段时间后重试
                    }
                    //通用错误
                    catch (Exception ex)
                    {
                        DyOrderAuto.Form1.Instance.SendLogForm($"发生错误：{ex.Message}");
                        _connection.Close();
                        return -1;
                    }

                    if (++retryCount >= maxRetries)
                    {
                        Debug.WriteLine($"执行已达到上线！");
                        _connection.Close();
                        return -1;
                    }
                }
                _connection.Close();
                return result;
            }
            
            



        }


        internal static void CloseSql() 
        {

            if (_connection != null) 
            {
                _connection.Close();
            }
        }
    }
}
