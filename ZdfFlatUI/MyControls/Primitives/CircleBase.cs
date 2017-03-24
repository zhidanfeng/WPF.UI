using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI.MyControls.Primitives
{
    /// <summary>
    /// 圆形基类
    /// </summary>
    public class CircleBase : RangeBase
    {
        /// <summary>
        /// 刻度盘起始角度依赖属性
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty;
        /// <summary>
        /// 刻度盘结束角度依赖属性
        /// </summary>
        public static readonly DependencyProperty EndAngleProperty;

        static CircleBase()
        {
            CircleBase.StartAngleProperty = DependencyProperty.Register("StartAngle",
                typeof(double),
                typeof(CircleBase),
                new PropertyMetadata(0d));
            CircleBase.EndAngleProperty = DependencyProperty.Register("EndAngle",
                typeof(double),
                typeof(CircleBase),
                new PropertyMetadata(360d));
        }

        #region Angle 刻度盘起始角度
        /// <summary>
        /// 刻度盘起始角度
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }
        #endregion

        #region Angle 刻度盘结束角度依赖属性
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }
        #endregion
    }
}
