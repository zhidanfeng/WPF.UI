using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ButtonGroup : ItemsControl
    {
        #region 路由事件

        #region ItemClickEvent

        public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(ButtonGroup));

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

        #endregion

        #region DependencyProperty

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ButtonGroup));

        #endregion

        #endregion

        #region Constructors

        static ButtonGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonGroup), new FrameworkPropertyMetadata(typeof(ButtonGroup)));
        }

        #endregion

        #region Override

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            int index = this.ItemContainerGenerator.IndexFromContainer(element);
            ButtonGroupItem buttonGroupItem = element as ButtonGroupItem;
            if (buttonGroupItem == null)
            {
                return;
            }

            if (index == 0)
            {
                buttonGroupItem.IsFirstItem = true;
                buttonGroupItem.CornerRadius = new CornerRadius(this.CornerRadius.TopLeft, 0, 0, this.CornerRadius.BottomLeft);
            }

            if (index == this.Items.Count - 1)
            {
                buttonGroupItem.IsLastItem = true;
                buttonGroupItem.CornerRadius = new CornerRadius(0, this.CornerRadius.TopRight, this.CornerRadius.BottomRight, 0);
            }

            base.PrepareContainerForItemOverride(buttonGroupItem, item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SegmentItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            //以下代码是为了新增项或者移除项时，正确设置每个Item的外观
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex == 0) //如果新添加项是放在第一位，则更改原来的第一位的属性值
                    {
                        this.SetButtonGroupItem(e.NewStartingIndex + e.NewItems.Count);
                    }

                    //如果新添加项是放在最后一位，则更改原来的最后一位的属性值
                    if (e.NewStartingIndex == this.Items.Count - e.NewItems.Count)
                    {
                        this.SetButtonGroupItem(e.NewStartingIndex - 1);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex == 0) //如果移除的是第一个，则更改更新后的第一项的属性值
                    {
                        this.SetButtonGroupItem(0);
                    }
                    else
                    {
                        this.SetButtonGroupItem(e.OldStartingIndex - 1);
                    }
                    break;
            }
        }

        #endregion

        #region private function

        /// <summary>
        /// 设置SegmentItem的位置属性
        /// </summary>
        /// <param name="index"></param>
        private void SetButtonGroupItem(int index)
        {
            if (index > this.Items.Count || index < 0)
            {
                return;
            }

            ButtonGroupItem buttonGroupItem = this.ItemContainerGenerator.ContainerFromIndex(index) as ButtonGroupItem;
            if (buttonGroupItem == null)
            {
                return;
            }
            buttonGroupItem.IsFirstItem = index == 0;
            buttonGroupItem.IsLastItem = index == this.Items.Count - 1;
            buttonGroupItem.IsMiddleItem = index > 0 && index < this.Items.Count - 1;
        }

        #endregion

        #region Event Implement Function

        #endregion
    }
}
