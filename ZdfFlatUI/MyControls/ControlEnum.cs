using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZdfFlatUI
{
    /// <summary>
    /// 仪表盘类型
    /// </summary>
    public enum DashboardSkinEnum
    {
        /// <summary>
        /// 速度
        /// </summary>
        Speed,
        /// <summary>
        /// 流量
        /// </summary>
        Flow,
    }

    /// <summary>
    /// 进度条类型
    /// </summary>
    public enum ProgressBarSkinEnum
    {
        /// <summary>
        /// 方形进度条
        /// </summary>
        Rectangle,
        /// <summary>
        /// 环形进度条
        /// </summary>
        Circle,
    }

    public enum EnumPlacement
    {
        /// <summary>
        /// 左上
        /// </summary>
        LeftTop,
        /// <summary>
        /// 左中
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 左下
        /// </summary>
        LeftCenter,
        /// <summary>
        /// 右上
        /// </summary>
        RightTop,
        /// <summary>
        /// 右下
        /// </summary>
        RightBottom,
        /// <summary>
        /// 右中
        /// </summary>
        RightCenter,
        /// <summary>
        /// 上左
        /// </summary>
        TopLeft,
        /// <summary>
        /// 上中
        /// </summary>
        TopCenter,
        /// <summary>
        /// 上右
        /// </summary>
        TopRight,
        /// <summary>
        /// 下左
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 下中
        /// </summary>
        BottomCenter,
        /// <summary>
        /// 下右
        /// </summary>
        BottomRight,
    }

    /// <summary>
    /// 提示类型
    /// </summary>
    public enum EnumPromptType
    {
        /// <summary>
        /// 消息
        /// </summary>
        Info,
        /// <summary>
        /// 警告
        /// </summary>
        Warn,
        /// <summary>
        /// 失败
        /// </summary>
        Error,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
    }

    public enum EnumCompare
    {
        Less,
        Equal,
        Large,
        None,
    }

    public enum EnumLoadingType
    {
        /// <summary>
        /// 两个环形
        /// </summary>
        DoubleArc,
        /// <summary>
        /// 两个圆
        /// </summary>
        DoubleRound,
        /// <summary>
        /// 一个圆
        /// </summary>
        SingleRound,
        /// <summary>
        /// 仿Win10加载条
        /// </summary>
        Win10,
        /// <summary>
        /// 仿Android加载条
        /// </summary>
        Android,
        Apple,
        Cogs,
        Normal,
    }

    public enum CloseBoxTypeEnum
    {
        /// <summary>
        /// 关闭窗口
        /// </summary>
        Close,
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        Hide,
    }

    /// <summary>
    /// Button类型
    /// </summary>
    public enum FlatButtonSkinEnum
    {
        Yes,
        No,
        Default,
        primary,
        ghost,
        dashed,
        text,
        info,
        success,
        error,
        warning,
    }
}
