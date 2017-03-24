using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class PathButton : Button
    {
        #region 依赖属性

        #region Path相关属性
        public static readonly DependencyProperty PathWidthProperty = DependencyProperty.Register("PathWidth"
            , typeof(double), typeof(PathButton), new FrameworkPropertyMetadata(13d));
        /// <summary>
        /// 
        /// </summary>
        public double PathWidth
        {
            get { return (double)GetValue(PathWidthProperty); }
            set { SetValue(PathWidthProperty, value); }
        }

        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData"
            , typeof(PathGeometry), typeof(PathButton));
        /// <summary>
        /// 
        /// </summary>
        public PathGeometry PathData
        {
            get { return (PathGeometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }
        #endregion

        #region 按钮属性
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground"
            , typeof(Brush), typeof(PathButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(79, 193, 233))));
        /// <summary>
        /// 鼠标悬浮时按钮的背景色
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground"
            , typeof(Brush), typeof(PathButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(74, 137, 220))));
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground"
            , typeof(Brush), typeof(PathButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(79, 193, 233))));
        /// <summary>
        /// 鼠标悬浮时按钮的前景色
        /// </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register("PressedForeground"
            , typeof(Brush), typeof(PathButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(74, 137, 220))));
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
            , typeof(CornerRadius), typeof(PathButton));
        /// <summary>
        /// 按钮四周圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #endregion

        #region 构造函数
        static PathButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathButton), new FrameworkPropertyMetadata(typeof(PathButton)));
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
