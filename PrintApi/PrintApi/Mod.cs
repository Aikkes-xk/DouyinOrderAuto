using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintApi
{
    public class DyOerderListType
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string shop_id { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string shop_name { get; set; }
        /// <summary>
        /// 商品类别名
        /// </summary>
        public string shop_typename { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public string shop_amount { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public string pay_shopamount { get; set; }
        /// <summary>
        /// 买家实付
        /// </summary>
        public string pay_sf { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public string pay_yf { get; set; }
        /// <summary>
        /// 买家优惠
        /// </summary>
        public string pay_yh { get; set; }
        /// <summary>
        /// 买家抖音昵称
        /// </summary>
        public string buy_nickname { get; set; }
        /// <summary>
        /// 买家用户id
        /// </summary>
        public string buy_userid { get; set; }
        /// <summary>
        /// 买家收货名称
        /// </summary>
        public string buy_sh_name { get; set; }
        /// <summary>
        /// 买家收货电话
        /// </summary>
        public string buy_sh_telephonenumber { get; set; }
        /// <summary>
        /// 买家收货地址
        /// </summary>
        public string buy_address { get; set; }
        /// <summary>
        /// 买家备注
        /// </summary>
        public string buy_message { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime buy_Time { get; set; }
        /// <summary>
        /// 是否为福袋 0不是 1是福袋
        /// </summary>
        public int LuckyBag { get; set; }
        /// <summary>
        /// 是否存在售后 0无 1有申请退款 
        /// </summary>
        public int Refund { get; set; }
    }

    public class SendPrint
    {
        /// <summary>
        /// 打印机昵称
        /// </summary>
        public string PrintName { get; set; }
        /// <summary>
        /// 打印模板路径
        /// </summary>
        public string PrintType { get; set; }
        /// <summary>
        /// 大标签
        /// </summary>
        public string Pdata { get; set; }

        /// <summary>
        /// 用户下单次数
        /// </summary>
        public string Userint {  get; set; }

        /// <summary>
        /// 累计购买次数
        /// </summary>
        public string BuyNum { get; set; }

        /// <summary>
        /// 今日第几单
        /// </summary>
        public string DayNum { get; set; }
        /// <summary>
        /// 推推乐小标记
        /// </summary>
        public string TuiText { get; set; }
        public string DiyShopName { get; set; }

        public DyOerderListType DyOerderListType { get; set; }

    }
    public class RePrint
    {
        /// <summary>
        /// 打印机昵称
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 打印模板路径
        /// </summary>
        public string msg { get; set; }


    }


}
