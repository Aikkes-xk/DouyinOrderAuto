using DyOrderAuto.DataManager;
using DyOrderAuto.Forms;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace DyOrderAuto.DyManager
{
    internal class AutoWebYd
    {

        //创建全局Page
        public static IPage Page { get; set; }

        public static bool IsRunning { get; set; }


        public static string DQURL { get; set; }


        public static IBrowserContext context { set; get; }

        public static async Task AutoStar(string URL)
        {

            // 创建Playwright实例
            using var playwright = await Playwright.CreateAsync();

            // 启动浏览器实例
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, // 设置为false以便在有UI的模式下运行
                Channel = "chrome", // 指定采用chrome浏览器类型
                ExecutablePath = string.Empty, // 不指定浏览器可执行文件位置，会自动寻找 ms-playwright 下载的浏览器
            });
            // 指定存储状态文件路径
            string storageStatePath = "DyCookie.json";
            if (File.Exists(storageStatePath))
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
            await Page.GotoAsync(URL);

            AutoWebYdForm.Instance.SendLogForm($"启动网页：{URL}");
            // 等待结果加载
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Page.FrameNavigated += Page_FrameNavigated;
            Page.Dialog += Page_Dialog;



            IsRunning = true;

            while (IsRunning)
            {
                if (DQURL != Page.Url)
                {
                    AutoWebYdForm.Instance.SendLogForm($"跳转到了一个新的网页：{Page.Url}|{await Page.TitleAsync()}");
                    DQURL = Page.Url;
                    if (Page.Url == "https://fuwu.jinritemai.com/authorize?service_id=19&from=md&state=1#/&loginType=1" || Page.Url == "https://fuwu.jinritemai.com/authorize?service_id=19&from=md&state=1#/")
                    {
                        await Page.GetByLabel("", new() { Exact = true }).CheckAsync();
                        await Page.GetByRole(AriaRole.Button, new() { Name = "确认授权" }).ClickAsync();
                    }
                    if (Page.Url == "https://douyins.woda.com/#/")
                    {
                        await Page.GotoAsync("https://douyins.woda.com/#/Trades");


                    }
                }
                // 等待一段时间再重复操作，避免过于频繁的请求
                await Task.Delay(1000);

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

        private static void Page_Dialog(object? sender, IDialog e)
        {
            AutoWebYdForm.Instance.SendLogForm($"触发弹窗");
        }

        private static async void Page_FrameNavigated(object? sender, IFrame e)
        {
            //Form1.Instance.SendLogForm($"跳转到了新的网页：{e.Url}");
        }
        public static async void senddid(string id)
        {
            if (DQURL != "https://douyins.woda.com/#/Trades")
            {
                AutoWebYdForm.Instance.SendLogForm($"你当前没在打单页面哦，请先切换到打单页面！");
                return;
            }

            try
            {
                await Page.GetByPlaceholder("收件人/手机号/运单号/订单号").FillAsync(id);
                await Page.GetByPlaceholder("收件人/手机号/运单号/订单号").PressAsync("Enter");

                await Task.Delay(500);
                //从打单系统中获取订单信息
                var d = DataTools.GetDyOerderLast(id);
                if (d == null)
                {
                    AutoWebYdForm.Instance.SendLogForm("未查询到买家信息！");
                    if (AutoWebYdForm.Instance.checkBox2.Checked)
                    {
                        voicsend($"未查询到订单的买家信息！");
                    }
                    return;
                }
                //获取用户信息
                var user = DataTools.GetUserList(d.buy_userid);

                AutoWebYdForm.Instance.label2.Text = $"今日共：{user.TodayNumber} 单";
                AutoWebYdForm.Instance.label3.Text = $"用户昵称：{user.UserName}";
                AutoWebYdForm.Instance.SendLogForm($"用户：{user.UserName}，今日共：{user.TodayNumber} 单");

                if (AutoWebYdForm.Instance.checkBox2.Checked)
                {
                    voicsend($"{user.UserName},今日共{user.TodayNumber} 单");
                }

                if (AutoWebYdForm.Instance.checkBox1.Checked)
                {
                    await Page.Locator("[id=\"_0\"]").GetByRole(AriaRole.Checkbox).CheckAsync();
                    await Page.GetByRole(AriaRole.Button, new() { Name = " 打印快递单" }).ClickAsync();
                    await Task.Delay(2000);
                    await Page.GetByText("关闭", new() { Exact = true }).ClickAsync();

                }
                if (AutoWebYdForm.Instance.checkBox3.Checked)
                {
                    await Page.Locator("[id=\"_0\"]").GetByRole(AriaRole.Checkbox).CheckAsync();
                    await Page.GetByRole(AriaRole.Button, new() { Name = " 打印快递单" }).ClickAsync();
                    await Task.Delay(1000);
                    await Page.GetByText("发货", new() { Exact = true }).Nth(1).ClickAsync();
                    await Task.Delay(1000);
                    await Page.GetByText("不刷新", new() { Exact = true }).ClickAsync();

                }


            }
            catch (Exception ex)
            {

                AutoWebYdForm.Instance.SendLogForm($"操作超时！");
                return;

            }


        }
        /// <summary>
        /// 启动调试窗口
        /// </summary>
        public static async void StarDebugUI()
        {
            await Page.PauseAsync();
        }
        public static async void UrlJump(string url)
        {
            await Page.GotoAsync(url);
        }

        /// <summary>
        /// 语音播报模块
        /// </summary>
        /// <param name="vtext"></param>
        public static void voicsend(string vtext)
        {
            SpeechVoiceSpeakFlags flag = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpVoice voice = new SpVoice();
            voice.Rate = 2;//语速
            voice.Volume = 100;//音量
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);//Item(0)中文、Item(3)英文
            voice.Speak(vtext, flag);
        }
    }
}
