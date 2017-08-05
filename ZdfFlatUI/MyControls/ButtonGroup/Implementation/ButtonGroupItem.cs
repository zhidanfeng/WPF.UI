using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ButtonGroupItem : ContentControl
    {
        #region Property
        private ButtonGroup ParentItemsControl
        {
            get { return this.ParentSelector as ButtonGroup; }
        }

        internal ItemsControl ParentSelector
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as ItemsControl; }
        }
        #endregion

        #region DependencyProperty

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ButtonGroupItem));

        #endregion

        #region IsFirstItem
        /// <summary>
        /// 获取或者设置该项在列表中是否是第一个
        /// </summary>
        [Bindable(true), Description("获取或者设置该项在列表中是否是第一个")]
        public bool IsFirstItem
        {
            get { return (bool)GetValue(IsFirstItemProperty); }
            set { SetValue(IsFirstItemProperty, value); }
        }

        public static readonly DependencyProperty IsFirstItemProperty =
            DependencyProperty.Register("IsFirstItem", typeof(bool), typeof(ButtonGroupItem), new PropertyMetadata(false));

        #endregion

        #region IsMiddleItem

        /// <summary>
        /// 获取或者设置该项在列表中是否是中间的一个
        /// </summary>
        [Bindable(true), Description("获取或者设置该项在列表中是否是中间的一个")]
        public bool IsMiddleItem
        {
            get { return (bool)GetValue(IsMiddleItemProperty); }
            set { SetValue(IsMiddleItemProperty, value); }
        }

        public static readonly DependencyProperty IsMiddleItemProperty =
            DependencyProperty.Register("IsMiddleItem", typeof(bool), typeof(ButtonGroupItem), new PropertyMetadata(false));

        #endregion

        #region IsLastItem
        /// <summary>
        /// 获取或者设置该项在列表中是否是最后一个
        /// </summary>
        [Bindable(true), Description("获取或者设置该项在列表中是否是最后一个")]
        public bool IsLastItem
        {
            get { return (bool)GetValue(IsLastItemProperty); }
            set { SetValue(IsLastItemProperty, value); }
        }

        public static readonly DependencyProperty IsLastItemProperty =
            DependencyProperty.Register("IsLastItem", typeof(bool), typeof(ButtonGroupItem), new PropertyMetadata(false));

        #endregion

        #endregion

        #region Constructors

        static ButtonGroupItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonGroupItem), new FrameworkPropertyMetadata(typeof(ButtonGroupItem)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseLeftButtonUp += ButtonGroupItem_MouseLeftButtonUp;
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        private void ButtonGroupItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ParentItemsControl.OnItemClick(this, this);
        }

        #endregion
    }
}
