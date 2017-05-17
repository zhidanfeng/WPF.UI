using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    internal class CascaderListBox : ListBox
    {
        public Cascader Owner { get; set; }

        #region ParentItem

        public object ParentItem
        {
            get { return (object)GetValue(ParentItemProperty); }
            set { SetValue(ParentItemProperty, value); }
        }

        public static readonly DependencyProperty ParentItemProperty =
            DependencyProperty.Register("ParentItem", typeof(object), typeof(CascaderListBox), new PropertyMetadata(null));

        #endregion

        #region Deep

        public int Deep
        {
            get { return (int)GetValue(DeepProperty); }
            set { SetValue(DeepProperty, value); }
        }
        
        public static readonly DependencyProperty DeepProperty =
            DependencyProperty.Register("Deep", typeof(int), typeof(CascaderListBox), new PropertyMetadata(0));

        #endregion

        #region ItemClickEvent

        public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(CascaderListBox));

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

        static CascaderListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CascaderListBox), new FrameworkPropertyMetadata(typeof(CascaderListBox)));
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            CascaderItem cascaderItem = element as CascaderItem;
            
            if(this.Owner != null)
            {
                Type type = item.GetType();
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(this.Owner.ChildMemberPath);
                IList list = (IList)propertyInfo.GetValue(item, null); //获取属性值
                if (list != null && list.Count > 0)
                {
                    cascaderItem.HasItems = true;
                }
                else
                {
                    cascaderItem.HasItems = false;
                }
                cascaderItem.ParentItem = this.ParentItem;
            }

            cascaderItem.ItemClick += CascaderItem_ItemClick;
        }

        private void CascaderItem_ItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.OnItemClick(e.OldValue, e.NewValue);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CascaderItem();
        }
    }
}
