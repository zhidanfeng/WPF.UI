using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using ZdfFlatUI.MyControls.Primitives;

namespace ZdfFlatUI
{
    /// <summary>
    /// 环形进度条
    /// </summary>
    public class CircleProgressBar : CircleBase
    {
        #region Private属性
        private Arc Indicator;
        private double oldAngle;
        #endregion

        #region 依赖属性定义
        /// <summary>
        /// 刻度盘当前值所对应的角度依赖属性
        /// </summary>
        public static readonly DependencyProperty AngleProperty;
        /// <summary>
        /// 动画持续时长依赖属性
        /// </summary>
        public static readonly DependencyProperty DurtionProperty;
        #endregion

        #region Constructors
        static CircleProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleProgressBar), new FrameworkPropertyMetadata(typeof(CircleProgressBar)));

            CircleProgressBar.AngleProperty = DependencyProperty.Register("Angle",
                typeof(double),
                typeof(CircleProgressBar),
                new PropertyMetadata(0d));

            CircleProgressBar.DurtionProperty = DependencyProperty.Register("Durtion",
                typeof(double),
                typeof(CircleProgressBar),
                new PropertyMetadata(500d));
        }
        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 刻度盘当前值所对应的角度
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            private set { SetValue(AngleProperty, value); }
        }

        /// <summary>
        /// 刻度盘数值变化时的动画执行时长
        /// </summary>
        public double Durtion
        {
            get { return (double)GetValue(DurtionProperty); }
            set { SetValue(DurtionProperty, value); }
        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Indicator = GetTemplateChild("Indicator") as Arc;
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            this.oldAngle = this.Angle;

            var valueDiff = this.Value - this.Minimum;
            this.Angle = this.StartAngle + (Math.Abs(this.EndAngle - this.StartAngle)) / (this.Maximum - this.Minimum) * valueDiff;
            this.TransformAngle(this.oldAngle, this.Angle, this.Durtion);
        }
        #endregion

        #region Private方法
        private void SetAngle()
        {
            if(this.Value < this.Minimum)
            {
                this.Angle = this.StartAngle;
                return;
            }
            if(this.Value > this.Maximum)
            {
                this.Angle = this.EndAngle;
                return;
            }
        }

        /// <summary>
        /// 角度值变化动画
        /// </summary>
        private void TransformAngle(double From, double To, double durtion)
        {
            if (this.Indicator != null)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation(From, this.Angle, new Duration(TimeSpan.FromMilliseconds(durtion)));
                this.Indicator.BeginAnimation(Arc.EndAngleProperty, doubleAnimation);
            }
        }
        #endregion
    }
}
