using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class DropDownButton : ItemsControl
    {
        #region Private属性
        public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(DropDownButton));

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

        public virtual void OnItemClick(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg = new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, ItemClickEvent);
            this.RaiseEvent(arg);
        }
        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty IsDropDownOpenProperty;
        #endregion

        #region 依赖属性set get
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        #endregion

        #region Constructors
        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
            DropDownButton.IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DropdownButtonItem();
        }
        #endregion

        #region Private方法

        #endregion
    }
}
