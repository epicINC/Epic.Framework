using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Input
{
    enum MouseMessage
    {
        /// <summary>
        /// 当鼠标轮子转动时发送此消息个当前有焦点的控件
        /// </summary>
        WM_MOUSEWHEEL = 0x20A,

        /// <summary>
        /// 双击鼠标中键
        /// </summary>
        WM_MBUTTONDBLCLK = 0x209,

        /// <summary>
        /// 释放鼠标中键
        /// </summary>
        WM_MBUTTONUP = 0x208,

        /// <summary>
        /// 移动鼠标时发生，同WM_MOUSEFIRST
        /// </summary>
        WM_MOUSEMOVE = 0x200,

        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        WM_LBUTTONDOWN = 0x201,

        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        WM_LBUTTONUP = 0x202,

        /// <summary>
        /// 双击鼠标左键
        /// </summary>
        WM_LBUTTONDBLCLK = 0x203,

        /// <summary>
        /// 按下鼠标右键
        /// </summary>
        WM_RBUTTONDOWN = 0x204,

        /// <summary>
        /// 释放鼠标右键
        /// </summary>
        WM_RBUTTONUP = 0x205,

        /// <summary>
        /// 双击鼠标右键
        /// </summary>
        WM_RBUTTONDBLCLK = 0x206,

        /// <summary>
        /// 按下鼠标中键
        /// </summary>
        WM_MBUTTONDOWN = 0x207,
    }
}
