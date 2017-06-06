using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ZdfFlatUI.Utils;

namespace ZdfFlatUI.MyControls.Primitives
{
    /// <summary>
    /// 用于日历显示时分秒的控件
    /// </summary>
    public class NumberBox : ComboBox
    {
        static NumberBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberBox), new FrameworkPropertyMetadata(typeof(NumberBox)));
        }

        #region 依赖属性
        public static readonly DependencyProperty StartNumberProperty = DependencyProperty.Register("StartNumber"
            , typeof(int), typeof(NumberBox));

        /// <summary>
        /// 起始数字
        /// </summary>
        public int StartNumber
        {
            get { return (int)GetValue(StartNumberProperty); }
            set { SetValue(StartNumberProperty, value); }
        }

        public static readonly DependencyProperty EndNumberProperty = DependencyProperty.Register("EndNumber"
            , typeof(int), typeof(NumberBox));

        /// <summary>
        /// 结束数字
        /// </summary>
        public int EndNumber
        {
            get { return (int)GetValue(EndNumberProperty); }
            set { SetValue(EndNumberProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title"
            , typeof(string), typeof(NumberBox));

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty MaxDropDownWidthProperty = DependencyProperty.Register("MaxDropDownWidth"
            , typeof(double), typeof(NumberBox));

        /// <summary>
        /// 弹出框的最大宽度
        /// </summary>
        public double MaxDropDownWidth
        {
            get { return (double)GetValue(MaxDropDownWidthProperty); }
            set { SetValue(MaxDropDownWidthProperty, value); }
        }

        public static readonly DependencyProperty ShowShadowProperty = DependencyProperty.Register("ShowShadow"
            , typeof(bool), typeof(NumberBox));

        /// <summary>
        /// 是否显示阴影
        /// </summary>
        public bool ShowShadow
        {
            get { return (bool)GetValue(ShowShadowProperty); }
            set { SetValue(ShowShadowProperty, value); }
        }

        public static readonly DependencyProperty ShadowBlurProperty = DependencyProperty.Register("ShadowBlur"
            , typeof(Thickness), typeof(NumberBox));

        /// <summary>
        /// 阴影的显示方向
        /// </summary>
        public Thickness ShadowBlur
        {
            get { return (Thickness)GetValue(ShadowBlurProperty); }
            set { SetValue(ShadowBlurProperty, value); }
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            List<int> list = new List<int>();
            for (int i = StartNumber; i <= EndNumber; i++)
            {
                list.Add(i);
            }
            this.ItemsSource = list;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new NumberBoxItem();
            item.OnItemSingleClickHandler += Item_OnClickHandler;
            return item;
        }

        private void Item_OnClickHandler(object sender, ItemMouseSingleClickEventArgs<object> e)
        {
            NumberBoxItem item = sender as NumberBoxItem;
            this.SelectedItem = item.Content;
        }
    }

    /// <summary>
    /// 重写ListViewItem，定义行单击、双击事件
    /// </summary>
    public class NumberBoxItem : System.Windows.Controls.ComboBoxItem
    {
        #region 事件
        /// <summary>
        /// Item单击事件
        /// </summary>
        public event EventHandler<ItemMouseSingleClickEventArgs<object>> OnItemSingleClickHandler;
        #endregion

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var selectedItem = ((System.Windows.FrameworkElement)e.OriginalSource).DataContext;
            this.OnItemSingleClickHandler(this, ItemMouseSingleClickEventArgs<object>.ItemSingleClick(selectedItem));
            base.OnMouseLeftButtonDown(e);
        }
    }
}
