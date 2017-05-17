using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class CascaderItem : ListBoxItem
    {
        #region 事件

        #region ItemClickEvent

        public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(CascaderItem));

        public event RoutedPropertyChangedEventHandler<object> ItemClick
        {
            add
            {
                this.AddHandler(ItemClickEvent, value);
            }
            remove
            {
                this.RemoveHandler(ItemClickEvent, value);
            }
        }

        public virtual void OnItemClickChanged(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg = new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, ItemClickEvent);
            this.RaiseEvent(arg);
        }

        #endregion

        #endregion

        #region 依赖属性

        #region HasItems
        public bool HasItems
        {
            get { return (bool)GetValue(HasItemsProperty); }
            set { SetValue(HasItemsProperty, value); }
        }

        public static readonly DependencyProperty HasItemsProperty =
            DependencyProperty.Register("HasItems", typeof(bool), typeof(CascaderItem), new PropertyMetadata(false));
        #endregion

        #region ParentItem

        public object ParentItem
        {
            get { return (object)GetValue(ParentItemProperty); }
            set { SetValue(ParentItemProperty, value); }
        }

        public static readonly DependencyProperty ParentItemProperty =
            DependencyProperty.Register("ParentItem", typeof(object), typeof(CascaderItem), new PropertyMetadata(null));

        #endregion

        #endregion

        static CascaderItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CascaderItem), new FrameworkPropertyMetadata(typeof(CascaderItem)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(HourButton_Click), true);
        }

        private void HourButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnItemClickChanged(this.Content, this.Content);
        }
    }
}
