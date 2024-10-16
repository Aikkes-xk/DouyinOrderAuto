using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DyOrderAuto.Config
{
    internal class FixedConfig
    {

        /// <summary>
        /// 抖店订单列表
        /// </summary>
        internal static string DYUrl {  get; set; }
        /// <summary>
        /// 抖店聊天页面
        /// </summary>
        internal static string DyIMUrl { get; set; }
        /// <summary>
        /// 自动打印
        /// </summary>
        internal static bool AutoPrint { get; set; }
        /// <summary>
        /// 打印机名称
        /// </summary>
        internal static string PrintName { get; set; }
        /// <summary>
        /// 打印服务端口
        /// </summary>
        internal static string PrintPort { get; set; }

        internal static int MaxPrint { set; get; }

        internal static string PrintMuban { get; set; }

        internal static string ShopDiyName { get; set;}
        /// <summary>
        /// 最大数量
        /// </summary>
        internal static int MaxGamePlay {  set; get; }
        /// <summary>
        /// 全局排单编号信息
        /// </summary>
        internal static int AllListInt { get; set;}

    }
}
