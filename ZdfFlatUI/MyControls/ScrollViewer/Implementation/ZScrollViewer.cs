using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ZScrollViewer : ScrollViewer
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        public double VerticalOffsetEx
        {
            get { return (double)GetValue(VerticalOffsetExProperty); }
            set { SetValue(VerticalOffsetExProperty, value); }
        }
        
        public static readonly DependencyProperty VerticalOffsetExProperty =
            DependencyProperty.Register("VerticalOffsetEx", typeof(double), typeof(ZScrollViewer), new PropertyMetadata(0d, VerticalOffsetExChangedCallback));

        private static void VerticalOffsetExChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZScrollViewer scrollViewer = d as ZScrollViewer;
            scrollViewer.ScrollToVerticalOffset((double)e.NewValue);
        }

        #endregion

        #region Constructors
        static ZScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZScrollViewer), new FrameworkPropertyMetadata(typeof(ZScrollViewer)));
        }
        #endregion

        #region Override方法
        
        #endregion

        #region Private方法
        public void aa()
        {
            
        }
        #endregion
    }
}
