using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Epic.Components
{
    /// <summary>
    /// 通用状态标示
    /// </summary>
    [Flags]
    public enum CommonStatusType
    {
        /// <summary>
        /// 正常(在需审核状态时 为 未审核)
        /// </summary>
        [Description("正常")]
        Normal = 1 << 0,


        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Audit = 1 << 1,

        /// <summary>
        /// 锁定
        /// </summary>
        [Description("锁定")]
        Lock = 1 << 2,

        /// <summary>
        /// 屏蔽
        /// </summary>
        [Description("屏蔽")]
        Ban = 1 << 3,

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 1 << 4,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 1 << 5,


        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1 << 10,

        /// <summary>
        /// 失败(如审核失败 等)
        /// </summary>
        [Description("失败")]
        Fail = 1 << 11,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Terminated = 1 << 12,

        /// <summary>
        /// 反义
        /// </summary>
        [Description("反义")]
        Anti = 1 << 12,


        /// <summary>
        /// 之前
        /// </summary>
        [Description("之前")]
        Pre = 1 << 15,

        /// <summary>
        /// 进行时
        /// </summary>
        [Description("进行时")]
        Inprogress = 1 << 16,

        /// <summary>
        /// 结束
        /// </summary>
        [Description("结束")]
        End = 1 << 17,



        /// <summary>
        /// 正在审核
        /// </summary>
        [Description("正在审核")]
        Auditing = Audit | Inprogress,

        /// <summary>
        /// 回收站(标记删除)
        /// </summary>
        [Description("回收站(标记删除)")]
        Trash = Mark | Delete,


        

        /// <summary>
        /// 支付
        /// </summary>
        [Description("支付")]
        Pay = 1 << 20,

        /// <summary>
        /// 导航(作为导航项)
        /// </summary>
        [Description("导航")]
        Nav = 1 << 21,


        /// <summary>
        /// 标记
        /// </summary>
        [Description("标记")]
        Mark = 1 << 22,

        /// <summary>
        /// 导入
        /// </summary>
        [Description("导入")]
        Import = 1 << 23,

        /// <summary>
        /// 导出
        /// </summary>
        [Description("导出")]
        Export = 1 << 24,





        /// <summary>
        /// 喜欢/收藏
        /// </summary>
        [Description("喜欢/收藏")]
        Fav = 1 << 25,

        /// <summary>
        /// 订阅
        /// </summary>
        [Description("订阅")]
        Subscription = 1 << 26,

        /// <summary>
        /// 关注
        /// </summary>
        [Description("关注")]
        Follow = 1 << 27,

        /// <summary>
        /// 赞
        /// </summary>
        [Description("赞")]
        Praise = 1 << 28,


        /// <summary>
        /// 阅读
        /// </summary>
        [Description("阅读")]
        Read = 1 << 29,

        /// <summary>
        /// 提醒
        /// </summary>
        [Description("提示")]
        Notify = 1 << 30,


        [Description("可选")]
        Optional = 1 << 31,

        /// <summary>
        /// 已提醒
        /// </summary>
        [Description("已提醒")]
        Remind = Notify | End
    }
}
