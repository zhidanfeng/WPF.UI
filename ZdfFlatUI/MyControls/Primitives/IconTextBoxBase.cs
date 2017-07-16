using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ZdfFlatUI.MyControls.Primitives
{
    public class IconTextBoxBase : ZTextBoxBase
    {
        #region 依赖属性

        #region IsShowIcon

        /// <summary>
        /// 获取或者设置是否显示图标
        /// </summary>
        [Bindable(true), Description("获取或者设置是否显示图标")]
        public bool IsShowIcon
        {
            get { return (bool)GetValue(IsShowIconProperty); }
            set { SetValue(IsShowIconProperty, value); }
        }

        public static readonly DependencyProperty IsShowIconProperty =
            DependencyProperty.Register("IsShowIcon", typeof(bool), typeof(IconTextBoxBase), new PropertyMetadata(true));

        #endregion
        
        #region IconBackground

        /// <summary>
        /// 获取或者设置图标边框背景色
        /// </summary>
        [Bindable(true), Description("获取或者设置图标边框背景色")]
        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register("IconBackground", typeof(Brush), typeof(IconTextBoxBase));

        #endregion

        #region IconForeground

        /// <summary>
        /// 获取或者设置图标的颜色
        /// </summary>
        [Bindable(true), Description("获取或者设置图标的颜色")]
        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(IconTextBoxBase));

        #endregion

        #region IconBorderBrush

        /// <summary>
        /// 获取或者设置图标边框的颜色
        /// </summary>
        [Bindable(true), Description("获取或者设置图标边框背景色")]
        public Brush IconBorderBrush
        {
            get { return (Brush)GetValue(IconBorderBrushProperty); }
            set { SetValue(IconBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty IconBorderBrushProperty =
            DependencyProperty.Register("IconBorderBrush", typeof(Brush), typeof(IconTextBoxBase));

        #endregion

        #region IconBorderThickness

        /// <summary>
        /// 获取或者设置图标边框的粗细与大小
        /// </summary>
        public Thickness IconBorderThickness
        {
            get { return (Thickness)GetValue(IconBorderThicknessProperty); }
            set { SetValue(IconBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty IconBorderThicknessProperty =
            DependencyProperty.Register("IconBorderThickness", typeof(Thickness), typeof(IconTextBoxBase));

        #endregion

        #region IconWidth

        /// <summary>
        /// 获取或者设置图标的大小
        /// </summary>
        [Bindable(true), Description("获取或者设置图标的大小")]
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(IconTextBoxBase));

        #endregion

        #region IconPadding

        /// <summary>
        /// 获取或者设置图标的内边距
        /// </summary>
        [Bindable(true), Description("获取或者设置图标的内边距")]
        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }

        public static readonly DependencyProperty IconPaddingProperty =
            DependencyProperty.Register("IconPadding", typeof(Thickness), typeof(IconTextBoxBase));

        #endregion

        #region IconCornerRadius

        /// <summary>
        /// 获取或者设置图标边框的圆角（可以不用手动设置，系统会根据密码框的圆角值自动设置该值）
        /// </summary>
        [Bindable(true), Description("获取或者设置图标边框的圆角（可以不用手动设置，系统会根据密码框的圆角值自动设置该值）")]
        public CornerRadius IconCornerRadius
        {
            get { return (CornerRadius)GetValue(IconCornerRadiusProperty); }
            set { SetValue(IconCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty IconCornerRadiusProperty =
            DependencyProperty.Register("IconCornerRadius", typeof(CornerRadius), typeof(IconTextBoxBase));

        #endregion

        #region IconPathData

        /// <summary>
        /// 获取或者设置密码框图标
        /// </summary>
        [Bindable(true), Description("获取或者设置密码框图标")]
        public PathGeometry IconPathData
        {
            get { return (PathGeometry)GetValue(IconPathDataProperty); }
            set { SetValue(IconPathDataProperty, value); }
        }

        public static readonly DependencyProperty IconPathDataProperty =
            DependencyProperty.Register("IconPathData", typeof(PathGeometry), typeof(IconTextBoxBase));

        #endregion

        #endregion
    }
}
