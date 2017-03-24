using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdfFlatUI.Utils;

namespace ZdfFlatUI
{
    /// <summary>
    /// 分段按钮控件，类似IOS的SegmentControl
    /// </summary>
    /// <remarks>add by zhidf 2016.7.23</remarks>
    [TemplatePart(Name = "PART_ItemBorder", Type =typeof(Border))]
    [TemplatePart(Name = "PART_ButtonSpliteLine", Type = typeof(Border))]
    public class SegmentButton : ListBox
    {
        static SegmentButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SegmentButton), new FrameworkPropertyMetadata(typeof(SegmentButton)));
        }

        #region 构造函数
        public SegmentButton() : base()
        {
            this.Loaded += SegmentButton_Loaded;
            this.SelectionChanged += SegmentButton_SelectionChanged;
        }
        #endregion

        private void SegmentButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.SelectedIndex;
            var listboxitem = this.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
            var border = MyVisualTreeHelper.FindChild<Border>(listboxitem, "PART_ItemBorder");

            if (border != null)
            {
                if (index == 0)
                {
                    border.CornerRadius = new CornerRadius(5, 0, 0, 5);
                }
                else if (index == this.Items.Count - 1)
                {
                    border.CornerRadius = new CornerRadius(0, 5, 5, 0);
                }
                else
                {
                    border.CornerRadius = new CornerRadius(0, 0, 0, 0);
                }
            }
        }

        private void SegmentButton_Loaded(object sender, RoutedEventArgs e)
        {
            var borderList = MyVisualTreeHelper.FindVisualChildren<Border>(this, "PART_ButtonSpliteLine").ToList();
            if(borderList != null)
            {
                for (int i = 0; i < borderList.Count; i++)
                {
                    //隐藏最后一个分隔线
                    if (i == this.Items.Count - 1)
                    {
                        var border = borderList[i];
                        border.Visibility = Visibility.Collapsed;
                    }
                }
            }

            int itemsCount = this.Items.Count;
            if (itemsCount > 0)
            {
                var listboxitem = this.ItemContainerGenerator.ContainerFromIndex(0) as FrameworkElement;
                var border = MyVisualTreeHelper.FindChild<Border>(listboxitem, "PART_ItemBorder");
                if (border != null)
                {
                    //如果控件只有一项的时候，需要重新设置边框的圆角
                    if (itemsCount == 1)
                    {
                        border.CornerRadius = new CornerRadius(5, 5, 5, 5);
                    }
                    else
                    {
                        if (this.SelectedIndex == 0)
                        {
                            border.CornerRadius = new CornerRadius(5, 0, 0, 5);
                        }
                        else if (this.SelectedIndex == itemsCount - 1)
                        {
                            border.CornerRadius = new CornerRadius(5, 5, 5, 5);
                        }
                        else
                        {
                            border.CornerRadius = new CornerRadius(0, 0, 0, 0);
                        }
                    }
                }
            }
        }
    }
}
