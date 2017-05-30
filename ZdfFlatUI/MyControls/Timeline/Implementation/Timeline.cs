using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace ZdfFlatUI
{
    /// <summary>
    /// 时间轴
    /// </summary>
    /// <remarks>add by zhidanfeng 2017.5.29</remarks>
    public class Timeline : ItemsControl
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region FirstSlotTemplate

        /// <summary>
        /// 获取或者设置第一个时间轴点的样子
        /// </summary>
        [Bindable(true), Description("获取或者设置第一个时间轴点的样子")]
        public DataTemplate FirstSlotTemplate
        {
            get { return (DataTemplate)GetValue(FirstSlotTemplateProperty); }
            set { SetValue(FirstSlotTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty FirstSlotTemplateProperty =
            DependencyProperty.Register("FirstSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion

        #region MiddleSlotTemplate

        /// <summary>
        /// 获取或者设置中间的时间轴点的样子
        /// </summary>
        [Bindable(true), Description("获取或者设置中间的时间轴点的样子")]
        public DataTemplate MiddleSlotTemplate
        {
            get { return (DataTemplate)GetValue(MiddleSlotTemplateProperty); }
            set { SetValue(MiddleSlotTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty MiddleSlotTemplateProperty =
            DependencyProperty.Register("MiddleSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion

        #region LastItemTemplate

        /// <summary>
        /// 获取或者设置最后一个时间轴点的样子
        /// </summary>
        [Bindable(true), Description("获取或者设置最后一个时间轴点的样子")]
        public DataTemplate LastSlotTemplate
        {
            get { return (DataTemplate)GetValue(LastSlotTemplateProperty); }
            set { SetValue(LastSlotTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty LastSlotTemplateProperty =
            DependencyProperty.Register("LastSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion

        #region IsCustomEverySlot

        /// <summary>
        /// 获取或者设置是否自定义每一个时间轴点的外观。
        /// </summary>
        [Bindable(true), Description("获取或者设置是否自定义每一个时间轴点的外观。当属性值为True时，FirstSlotTemplate、MiddleSlotTemplate、LastSlotTemplate属性都将失效，只能设置SlotTemplate来定义每一个时间轴点的样式")]
        public bool IsCustomEverySlot
        {
            get { return (bool)GetValue(IsCustomEverySlotProperty); }
            set { SetValue(IsCustomEverySlotProperty, value); }
        }
        
        public static readonly DependencyProperty IsCustomEverySlotProperty =
            DependencyProperty.Register("IsCustomEverySlot", typeof(bool), typeof(Timeline), new PropertyMetadata(false));

        #endregion

        #region SlotTemplate

        /// <summary>
        /// 获取或者设置每个时间轴点的外观
        /// </summary>
        [Bindable(true), Description("获取或者设置每个时间轴点的外观。只有当IsCustomEverySlot属性为True时，该属性才生效")]
        public DataTemplate SlotTemplate
        {
            get { return (DataTemplate)GetValue(SlotTemplateProperty); }
            set { SetValue(SlotTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty SlotTemplateProperty =
            DependencyProperty.Register("SlotTemplate", typeof(DataTemplate), typeof(Timeline));

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
            if(timelineItem == null)
            {
                return;
            }

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
            base.OnItemsChanged(e);

            //以下代码是为了新增项或者移除项时，正确设置每个Item的外观
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
    }
}
