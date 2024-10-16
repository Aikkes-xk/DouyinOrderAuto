using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DyOrderAuto.DyMods
{
    internal class DyQueueList
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string Orderid { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime BuyTime { get; set; }
        /// <summary>
        /// 排单id
        /// </summary>
        public int Ranking { get; set; }
        /// <summary>
        /// 排单状态，0正在排单，1过单 2已拆 3直邮
        /// </summary>
        public int OverOrder { get; set; }
        /// <summary>
        /// 订单状态，0正常 1售后 
        /// </summary>
        public int Demolished { get; set; }
        /// <summary>
        /// 买家下单时是今日第几单
        /// </summary>
        public int TodayNumber {  get; set; }
        /// <summary>
        /// 买家下单时是累计第几单
        /// </summary>
        public int RecordsNumber { get; set; }

    }
}
