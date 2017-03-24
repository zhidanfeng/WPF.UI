using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI.StyleSelectors
{
    public class NavigateBarStyleSelector : StyleSelector
    {
        public Style LeftItemStyle { get; set; }

        public Style MiddleItemStyle { get; set; }

        public Style RightItemStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            ListBox listbox = ListBox.ItemsControlFromItemContainer(container) as ListBox;
            Style style;

            ListBox.GetItemsOwner(container);
            int index = listbox.ItemContainerGenerator.IndexFromContainer(container);
            
            if (index == 0)
            {
                style = LeftItemStyle;
            }
            else if (index == listbox.Items.Count - 1)
            {
                style = RightItemStyle;
            }
            else
            {
                style = MiddleItemStyle;
            }

            return style;
        }
    }
}
