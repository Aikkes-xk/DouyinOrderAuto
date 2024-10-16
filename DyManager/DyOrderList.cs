using DyOrderAuto.Config;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace DyOrderAuto.DyManager
{
    public class DyOrderList
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
        public static async Task AutoStar(string URL, bool headless, bool DyCookieGet)
        {

            // 创建Playwright实例
            using var playwright = await Playwright.CreateAsync();

            // 启动浏览器实例
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless, // 设置为false以便在有UI的模式下运行
                Channel = "chrome", // 指定采用chrome浏览器类型
                ExecutablePath = string.Empty, // 不指定浏览器可执行文件位置，会自动寻找 ms-playwright 下载的浏览器
            });
            // 指定存储状态文件路径
            string storageStatePath = "DyCookie.json";

            if (File.Exists(storageStatePath) && DyCookieGet == false)
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
                });
            }

            // 创建一个新页面
            Page = await context.NewPageAsync();
            //是否持续运行
            IsRunning = true;


            // 导航到目标网页
            await Page.GotoAsync(URL);


            Page.RequestFinished += Page_RequestFinished;

            while (IsRunning)
            {

                if (DQURL != Page.Url)
                {
                    try 
                    {
                        Debug.WriteLine($"跳转到了一个新的网页：{Page.Url}|{await Page.TitleAsync()}");
                        if (Page.Url == "https://fxg.jinritemai.com/ffa/morder/order/list")
                        {
                            await Page.Locator("div").Filter(new() { HasTextRegex = new System.Text.RegularExpressions.Regex("^10 条\\/页$") }).Nth(1).ClickAsync();
                            await Page.GetByText("20 条/页").ClickAsync();
                            await Page.GetByText("待发货").First.ClickAsync();
                            await Task.Delay(1000);
                            await Page.GetByText("售后中").First.ClickAsync();
                            SaveConfig();
                        }
                        if (Page.Url.Contains("https://fxg.jinritemai.com/login/common") && headless == true)
                        {
                            Form1.Instance.SendLogForm($"账号失效，请重新登入抖店！");
                            break;
                        }
                    }
                    catch(Exception ex) 
                    {
                        Debug.WriteLine($"{ex.Message}");
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

        
        public static async void SaveConfig()
        {
            if (context != null)
            {
                // 获取页面的 cookies
                var cookies = await context.CookiesAsync();
                foreach (var cookie in cookies)
                {
                    // 转换 Playwright 的 Cookie 为 System.Net.Cookie
                    var netCookie = new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain)
                    {
                        Secure = cookie.Secure,
                        HttpOnly = cookie.HttpOnly,
                        Expires = DateTimeOffset.FromUnixTimeSeconds((long)cookie.Expires).UtcDateTime
                    };

                    // 将转换后的 Cookie 添加到 CookieContainer
                    GetDyApi.cookieContainer.Add(netCookie);
                }
                Debug.WriteLine($"保存Cookie");
                GetDyApi.GetUserInfo();
                // 存储当前会话
                await context.StorageStateAsync(new BrowserContextStorageStateOptions
                {
                    Path = "DyCookie.json"
                });
            }



        }
        private static void Page_RequestFinished(object? sender, IRequest e)
        {

            if (e.Url.Contains("https://fxg.jinritemai.com/api/order/searchlist?page=0&pageSize=20&order_status=stock_up"))
            {
                Debug.WriteLine($"基础订单读取成功：{e.Url}");
                GetDyApi.ListUrl = e.Url;
                

            }
            if (e.Url.Contains("https://fxg.jinritemai.com/api/order/searchlist?page=0&pageSize=20&aftersale_status"))
            {
                Debug.WriteLine($"退款订单读取成功：{e.Url}");
                GetDyApi.TkListUrl = e.Url;
                Form1.Instance.SendLogForm($"登入成功，开始获取订单信息！");
                //启动im聊天窗口
                Form1.Instance.StarIM();
                IsRunning = false;
            }


        }


        public static async void StarDebugUI()
        {
            await Page.PauseAsync();
        }
    }
}
