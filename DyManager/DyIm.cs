using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DyOrderAuto.DyManager
{
    internal class DyIm
    {

        //创建全局Page
        public static IPage Page { get; set; }

        public static bool IsRunning { get; set; }


        public static string DQURL { get; set; }


        public static IBrowserContext context { set; get; }

        public static IBrowser browser { set; get; }



        /// <summary>
        /// 抖店Cookie获取
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="headless"></param>
        /// <returns></returns>
        public static async Task AutoImStar()
        {

            // 创建Playwright实例
            using var playwright = await Playwright.CreateAsync();

            // 启动浏览器实例
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, // 设置为false以便在有UI的模式下运行
                Channel = "chrome", // 指定采用chrome浏览器类型
                ExecutablePath = string.Empty, // 不指定浏览器可执行文件位置，会自动寻找 ms-playwright 下载的浏览器
            });
            // 指定存储状态文件路径
            string storageStatePath = "DyCookie.json";

            if (File.Exists(storageStatePath) )
            {
                context = await browser.NewContextAsync(new BrowserNewContextOptions
                {
                    ViewportSize = new ViewportSize
                    {
                        Width = 1365, // 1920 * 0.8
                        Height = 865, // 1080 * 0.8
                    }, // 窗口大小
                    Locale = "zh-CN", // 指定语言(区域)
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36",
                    TimezoneId = "Asia/Shanghai", // 指定时区
                    StorageStatePath = storageStatePath
                });
            }
            else
            {
                context = await browser.NewContextAsync(new BrowserNewContextOptions
                {
                    ViewportSize = new ViewportSize
                    {
                        Width = 1365, // 1920 * 0.8
                        Height = 865, // 1080 * 0.8
                    }, // 窗口大小
                    Locale = "zh-CN", // 指定语言(区域)
                    TimezoneId = "Asia/Shanghai", // 指定时区
                    StorageStatePath = storageStatePath
                });
            }

            // 创建一个新页面
            Page = await context.NewPageAsync();

            // 导航到目标网页
            await Page.GotoAsync("https://im.jinritemai.com/pc_seller_v2/main/workspace");
            //是否持续运行
            IsRunning = true;

            while (IsRunning)
            {

                if (DQURL != Page.Url)
                {

                    if (Page.Url.Contains("https://fxg.jinritemai.com/login/common"))
                    {
                        Form1.Instance.SendLogForm($"自动化聊天窗口抖店账号已离线，请在窗口中重新操作！");
                        
                    }

                    DQURL = Page.Url;
                }


                // 等待一段时间再重复操作，避免过于频繁的请求
                await Task.Delay(500);

            }
            // 关闭页面
            await Page.CloseAsync();
            // 关闭上下文
            await context.CloseAsync();
            // 关闭浏览器
            await browser.CloseAsync();
            if (IsRunning == false)
            {
                DQURL = "";
            }
        }

        public static async void StarDebugUI()
        {
            await Page.PauseAsync();
        }
        public static async Task<bool> SendDyIm(string imurl,string sendtext)
        {

            try
            {
                await Page.GetByPlaceholder("用户昵称/抖音号/订单号(Ctrl+F)").ClickAsync();
                await Page.GetByPlaceholder("用户昵称/抖音号/订单号").FillAsync(imurl);
                await Page.GetByPlaceholder("用户昵称/抖音号/订单号(Ctrl+F)").PressAsync("Enter");
                await Task.Delay(1000);
                await Page.GetByRole(AriaRole.Button, new() { Name = "来自订单:" }).ClickAsync();
                await Task.Delay(1000);
                await Page.GetByRole(AriaRole.Button, new() { Name = "/800" }).ClickAsync();
                await Task.Delay(2000);
                await Page.GetByPlaceholder("使用Enter 发送消息").FillAsync(sendtext);
                await Page.GetByPlaceholder("使用Enter 发送消息").PressAsync("Enter");

                    
                return true;
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"{ex.Message}");
                return false;
            }


        }
    }
}
