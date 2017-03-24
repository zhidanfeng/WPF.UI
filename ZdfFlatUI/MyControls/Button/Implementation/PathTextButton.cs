using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class PathTextButton : Button
    {
        #region 依赖属性

        #region Path相关属性
        public static readonly DependencyProperty PathWidthProperty = DependencyProperty.Register("PathWidth"
            , typeof(double), typeof(PathTextButton), new FrameworkPropertyMetadata(13d));
        /// <summary>
        /// 
        /// </summary>
        public double PathWidth
        {
            get { return (double)GetValue(PathWidthProperty); }
            set { SetValue(PathWidthProperty, value); }
        }

        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData"
            , typeof(PathGeometry), typeof(PathTextButton));
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
        
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground"
            , typeof(Brush), typeof(PathTextButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 139, 225))));
        /// <summary>
        /// 鼠标悬浮时按钮的前景色
        /// </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register("PressedForeground"
            , typeof(Brush), typeof(PathTextButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(36, 127, 207))));
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        #endregion

        #endregion

        static PathTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathTextButton), new FrameworkPropertyMetadata(typeof(PathTextButton)));
        }
    }
}
