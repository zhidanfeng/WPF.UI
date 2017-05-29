using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class Timeline : ItemsControl
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region FirstItemStyle

        public Style FirstItemStyle
        {
            get { return (Style)GetValue(FirstItemStyleProperty); }
            set { SetValue(FirstItemStyleProperty, value); }
        }
        
        public static readonly DependencyProperty FirstItemStyleProperty =
            DependencyProperty.Register("FirstItemStyle", typeof(Style), typeof(Timeline), new PropertyMetadata(null));

        #endregion

        #endregion

        #region Constructors

        static Timeline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(typeof(Timeline)));
        }

        #endregion

        #region Override

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            int index = this.ItemContainerGenerator.IndexFromContainer(element);
            TimelineItem timelineItem = element as TimelineItem;
            if(index == 0)
            {
                timelineItem.IsFirstItem = true;
            }

            if(index == this.Items.Count - 1)
            {
                timelineItem.IsLastItem = true;
            }

            base.PrepareContainerForItemOverride(timelineItem, item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TimelineItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.Items.Count.ToString() + e.NewStartingIndex);
            base.OnItemsChanged(e);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex == 0) //如果新添加项是放在第一位，则更改原来的第一位的属性值
                    {
                        this.SetTimelineItem(e.NewStartingIndex + e.NewItems.Count);
                    }

                    //如果新添加项是放在最后一位，则更改原来的最后一位的属性值
                    if (e.NewStartingIndex == this.Items.Count - e.NewItems.Count)
                    {
                        this.SetTimelineItem(e.NewStartingIndex - 1);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if(e.OldStartingIndex == 0) //如果移除的是第一个，则更改更新后的第一项的属性值
                    {
                        this.SetTimelineItem(0);
                    }
                    else
                    {
                        this.SetTimelineItem(e.OldStartingIndex - 1);
                    }
                    break;
            }
        }
        #endregion

        #region private function
        /// <summary>
        /// 设置TimelineItem的位置属性
        /// </summary>
        /// <param name="index"></param>
        private void SetTimelineItem(int index)
        {
            if(index > this.Items.Count || index < 0)
            {
                return;
            }

            TimelineItem timelineItem = this.ItemContainerGenerator.ContainerFromIndex(index) as TimelineItem;
            if(timelineItem == null)
            {
                return;
            }
            timelineItem.IsFirstItem = index == 0;
            timelineItem.IsLastItem = index == this.Items.Count - 1;
            timelineItem.IsMiddleItem = index > 0 && index < this.Items.Count - 1;
        }
        #endregion

        #region Event Implement Function

        #endregion
    }
}
