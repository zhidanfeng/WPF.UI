using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ZdfFlatUI
{
    public class FlatProgressBar : RangeBase
    {
        #region Private属性
        private FrameworkElement Indicator;
        #endregion

        #region 依赖属性定义
        /// <summary>
        /// 圆角
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty;
        /// <summary>
        /// 内圆角
        /// </summary>
        public static readonly DependencyProperty InnerCornerRadiusProperty;
        /// <summary>
        /// 内圆角
        /// </summary>
        public static readonly DependencyProperty SkinProperty;
        #endregion

        #region Constructors
        static FlatProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatProgressBar), new FrameworkPropertyMetadata(typeof(FlatProgressBar)));

            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlatProgressBar));
            InnerCornerRadiusProperty = DependencyProperty.Register("InnerCornerRadius", typeof(CornerRadius), typeof(FlatProgressBar));
            SkinProperty = DependencyProperty.Register("Skin", typeof(ProgressBarSkinEnum), typeof(FlatProgressBar));

            //ProgressBar.ValueProperty.OverrideMetadata(typeof(FlatProgressBar), new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));
        }

        //private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    double oldValue = Convert.ToDouble(e.OldValue);
        //    double newValue = Convert.ToDouble(e.NewValue);

        //    FlatProgressBar progressBar = d as FlatProgressBar;
        //    var perWidth = progressBar.Width / (progressBar.Maximum - progressBar.Minimum);
        //    var oldWidth = oldValue * perWidth;
        //    var newWidth = newValue * perWidth;

        //    DoubleAnimation doubleAnimation = new DoubleAnimation();
        //    doubleAnimation.From = oldWidth;
        //    doubleAnimation.To = newWidth - progressBar.BorderThickness.Right * 2;
        //    doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(400));
        //    if (progressBar.Indicator != null)
        //    {
        //        progressBar.Indicator.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);
        //    }
        //}
        #endregion

        #region 依赖属性set,get

        /// <summary>
        /// 圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 内圆角
        /// </summary>
        public CornerRadius InnerCornerRadius
        {
            get { return (CornerRadius)GetValue(InnerCornerRadiusProperty); }
            set { SetValue(InnerCornerRadiusProperty, value); }
        }

        /// <summary>
        /// 皮肤
        /// </summary>
        public ProgressBarSkinEnum Skin
        {
            get { return (ProgressBarSkinEnum)GetValue(SkinProperty); }
            set { SetValue(SkinProperty, value); }
        }
        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //CornerRadius corner = new CornerRadius();
            //corner.TopLeft = this.CornerRadius.TopLeft - this.BorderThickness.Left;
            //corner.TopRight = this.CornerRadius.TopRight - this.BorderThickness.Top;
            //corner.BottomRight = this.CornerRadius.BottomRight - this.BorderThickness.Right;
            //corner.BottomLeft = this.CornerRadius.BottomLeft - this.BorderThickness.Bottom;
            //this.InnerCornerRadius = corner;

            this.Indicator = GetTemplateChild("Indicator") as FrameworkElement;
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            var perWidth = this.Width / (this.Maximum - this.Minimum);
            var oldWidth = oldValue * perWidth;
            var newWidth = newValue * perWidth;

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = oldWidth;
            doubleAnimation.To = newWidth - this.BorderThickness.Right * 2;
            doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            if (this.Indicator != null)
            {
                this.Indicator.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);
            }
        }
        #endregion
    }
}
