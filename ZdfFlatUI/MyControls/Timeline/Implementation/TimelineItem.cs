using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace ZdfFlatUI
{
    public class TimelineItem : ContentControl
    {
        #region DependencyProperty

        #region IsFirstItem
        /// <summary>
        /// 获取或者设置项是否是第一个
        /// </summary>
        public bool IsFirstItem
        {
            get { return (bool)GetValue(IsFirstItemProperty); }
            set { SetValue(IsFirstItemProperty, value); }
        }
        
        public static readonly DependencyProperty IsFirstItemProperty =
            DependencyProperty.Register("IsFirstItem", typeof(bool), typeof(TimelineItem), new PropertyMetadata(false));

        #endregion

        #region IsLastItem
        /// <summary>
        /// 获取或者设置项是否是最后一个
        /// </summary>
        public bool IsLastItem
        {
            get { return (bool)GetValue(IsLastItemProperty); }
            set { SetValue(IsLastItemProperty, value); }
        }
        
        public static readonly DependencyProperty IsLastItemProperty =
            DependencyProperty.Register("IsLastItem", typeof(bool), typeof(TimelineItem), new PropertyMetadata(false));

        #endregion

        #region IsMiddleItem

        /// <summary>
        /// 获取或者设置项是否是中间的一个
        /// </summary>
        //[Bindable(true), Category("")]
        public bool IsMiddleItem
        {
            get { return (bool)GetValue(IsMiddleItemProperty); }
            set { SetValue(IsMiddleItemProperty, value); }
        }
        
        public static readonly DependencyProperty IsMiddleItemProperty =
            DependencyProperty.Register("IsMiddleItem", typeof(bool), typeof(TimelineItem), new PropertyMetadata(false));

        #endregion

        #endregion

        #region Constructors

        static TimelineItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineItem), new FrameworkPropertyMetadata(typeof(TimelineItem)));
        }

        #endregion
    }
}
