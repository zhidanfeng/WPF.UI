using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class BusyIndicator : Control
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty IsBusyProperty;
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty LoadingColorProperty;
        #endregion

        #region 依赖属性set get
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Brush LoadingColor
        {
            get { return (Brush)GetValue(LoadingColorProperty); }
            set { SetValue(LoadingColorProperty, value); }
        }
        #endregion

        #region 依赖属性回调
        private static void OnIsBusyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BusyIndicator indicator = d as BusyIndicator;
            indicator.Visibility = indicator.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        #region Constructors
        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
            BusyIndicator.IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(false, OnIsBusyChangedCallback));
            BusyIndicator.TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(BusyIndicator), new PropertyMetadata("加载中..."));
            BusyIndicator.LoadingColorProperty = DependencyProperty.Register("LoadingColor", typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 122, 204))));
        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Visibility = this.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        #region Private方法

        #endregion
    }
}
