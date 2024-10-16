using DyOrderAuto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DyOrderAuto
{
    internal class GameplayProcessing
    {
        internal const string filePath = "usedNumbers.txt";  // 存储已使用数字的文件路径
        internal static List<int> numbers;  // 存储未使用数字的列表
        internal static Random random;  // 用于生成随机数的Random对象
        /// <summary>
        /// 加载未使用的数字列表
        /// </summary>
        internal static void LoadNumbers()
        {
            random = new Random();  // 初始化随机数生成器
            numbers = new List<int>(FixedConfig.MaxGamePlay);  // 初始化一个容量为50的列表
            HashSet<int> usedNumbers = new HashSet<int>();  // 用于存储已使用数字的HashSet

            // 如果文件存在，从文件中读取未使用的数字
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);  // 读取文件中的所有行
                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int num))  // 尝试将每一行转换为整数
                    {
                        numbers.Add(num);  // 将转换成功的数字添加到已使用数字的HashSet中
                    }
                }

                // 初始化数字列表，读取筛选出已使用的数字
                for (int i = 1; i <= FixedConfig.MaxGamePlay; i++)
                {
                    if (!numbers.Contains(i))  // 如果数字已被使用
                    {
                        usedNumbers.Add(i);  // 将已使用的数字添加到数字列表中
                    }
                }
            }
            else 
            {
                numbers = new List<int>(FixedConfig.MaxGamePlay);  // 重新初始化一个容量为50的列表
                for (int i = 1; i <= FixedConfig.MaxGamePlay; i++)
                {
                    numbers.Add(i);  // 添加1到50的所有数字
                }
                SaveNumbers();  // 保存数字列表到文件
            }


            Form1.Instance.KaTextMax.Invoke(new Action(() => Form1.Instance.KaTextMax.Text = $"当前卡池剩余：{numbers.Count}个"));
        }

        /// <summary>
        /// 将当前未使用数字列表写入文件
        /// </summary>
        internal static void SaveNumbers()
        {
            File.WriteAllLines(filePath, numbers.ConvertAll(n => n.ToString()));  // 将数字列表转换为字符串并写入文件
        }

        /// <summary>
        /// 重置数字列表并保存到文件中
        /// </summary>
        internal static void Reset()
        {
            numbers = new List<int>(FixedConfig.MaxGamePlay);  // 重新初始化一个容量为50的列表
            for (int i = 1; i <= FixedConfig.MaxGamePlay; i++)
            {
                numbers.Add(i);  // 添加1到50的所有数字
            }

            Form1.Instance.KaTextMax.Invoke(new Action(() => Form1.Instance.KaTextMax.Text = $"当前卡池剩余：{numbers.Count}个"));


            SaveNumbers();  // 保存数字列表到文件
        }

        /// <summary>
        /// 生成一个唯一的随机数，并从列表中移除该数字
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static int GetNextRandom()
        {
            if (numbers.Count == 0)  // 如果数字列表为空
            {
                Form1.Instance.KaTextMax.Invoke(new Action(() => Form1.Instance.KaTextMax.Text = $"当前卡池剩余：卡池已空"));
                throw new InvalidOperationException("卡池已完");  // 抛出异常并提示“卡池已完”
            }

            int index = random.Next(numbers.Count);  // 生成一个范围在0到数字列表长度之间的随机索引
            int nextNumber = numbers[index];  // 获取该索引对应的数字
            numbers.RemoveAt(index);  // 从数字列表中移除该数字

            Form1.Instance.KaTextMax.Invoke(new Action(() => Form1.Instance.KaTextMax.Text = $"当前卡池剩余：{numbers.Count}个"));
            SaveNumbers();  // 保存更新后的数字列表到文件
            return nextNumber;  // 返回生成的随机数
        }


    }
}
