using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZdfFlatUI
{
    #region DashboardSkinEnum
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
    #endregion

    #region ProgressBarSkinEnum
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
    #endregion

    #region EnumPlacement
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
    #endregion

    #region PlacementDirection
    public enum EnumPlacementDirection
    {
        Left,
        Top,
        Right,
        Bottom,
    }
    #endregion

    #region EnumPromptType
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
    #endregion

    #region EnumCompare
    public enum EnumCompare
    {
        /// <summary>
        /// 小于
        /// </summary>
        Less,
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 大于
        /// </summary>
        Large,
        None,
    }
    #endregion

    #region EnumLoadingType
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
        /// <summary>
        /// 仿苹果加载条
        /// </summary>
        Apple,
        Cogs,
        Normal,
    }
    #endregion

    #region CloseBoxTypeEnum
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
    #endregion

    #region FlatButtonSkinEnum
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
    #endregion

    #region EnumTrigger
    public enum EnumTrigger
    {
        /// <summary>
        /// 悬浮
        /// </summary>
        Hover,
        /// <summary>
        /// 点击
        /// </summary>
        Click,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom,
    }
    #endregion

    #region EnumTabControlType
    public enum EnumTabControlType
    {
        Line,
        Card,
    }
    #endregion

    #region IconType
    public enum EnumIconType
    {
        Info,
        Error,
        Warning,
        Success,
        MacOS,
        Windows,
        Linux,
        Android,
        Star_Empty,
        Star_Half,
        Star_Full,
    }
    #endregion

    #region EnumDatePickerType
    public enum EnumDatePickerType
    {
        /// <summary>
        /// 单个日期
        /// </summary>
        SingleDate,
        /// <summary>
        /// 连续的多个日期
        /// </summary>
        SingleDateRange,
        /// <summary>
        /// 只显示年份
        /// </summary>
        Year,
        /// <summary>
        /// 只显示月份
        /// </summary>
        Month,
        /// <summary>
        /// 显示一个日期和时间
        /// </summary>
        DateTime,
        /// <summary>
        /// 显示连续的日期和时间
        /// </summary>
        DateTimeRange,
    }
    #endregion

    #region DayTitle
    public enum DayTitle
    {
        日 = 0,
        一,
        二,
        三,
        四,
        五,
        六,
    }
    #endregion
}
