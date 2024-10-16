using DyOrderAuto.DyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DyOrderAuto.Forms
{
    internal class TimerTask
    {
        // 一个队列，用于保存要执行的任务
        private static Queue<Func<Task>> taskQueue = new Queue<Func<Task>>();
        // 一个锁对象，确保多线程访问队列时的线程安全
        private static object queueLock = new object();
        // 标识当前是否有任务在运行
        private static bool isTaskRunning = false;

        // Timer回调方法，添加任务到队列中
        internal static void AddTask()
        {
            // 每次Timer触发时添加一个任务
            EnqueueTask(() => GetTimeAsync());
        }
        internal static async Task GetTimeAsync()
        {
            if (GetDyApi.ListUrl == string.Empty)
            {
                Form1.Instance.SendLogForm("等待登入！");
                return;
            }
            else
            {

                GetDyApi.GetOrderList();
                GetDyApi.GetdkOrderList();
            }
        }



        // 将任务添加到队列中
        private static void EnqueueTask(Func<Task> task)
        {
            lock (queueLock)
            {
                // 任务入队
                taskQueue.Enqueue(task);
                // 如果当前没有任务在运行，启动下一个任务
                if (!isTaskRunning)
                {
                    StartNextTask();
                }
            }
        }

        // 启动下一个任务
        private static void StartNextTask()
        {
            lock (queueLock)
            {
                // 如果队列中有任务
                if (taskQueue.Count > 0)
                {
                    // 取出队列中的下一个任务
                    var nextTask = taskQueue.Dequeue();
                    // 标记当前有任务在运行
                    isTaskRunning = true;
                    // 在新线程中运行任务
                    Task.Run(async () =>
                    {
                        // 等待任务完成
                        await nextTask();
                        // 通知任务完成
                        TaskFinished();
                    });
                }
            }
        }

        // 任务完成后调用，启动下一个任务（如果有）
        private static void TaskFinished()
        {
            lock (queueLock)
            {
                // 标记当前没有任务在运行
                isTaskRunning = false;
                // 如果队列中还有任务，启动下一个任务
                if (taskQueue.Count > 0)
                {
                    StartNextTask();
                }
            }
        }



    }
    
}

