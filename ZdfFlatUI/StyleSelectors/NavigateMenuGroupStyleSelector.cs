using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI.StyleSelectors
{
    public class NavigateMenuGroupStyleSelector : StyleSelector
    {
        public Style ExpandStyle { get; set; }

        public Style NoExpandStyle { get; set; }

        private int Type { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            ListBox listbox = ListBox.ItemsControlFromItemContainer(container) as ListBox;
            Style style;
            ListBox.GetItemsOwner(container);

            if (this.Type == 0)
            {
                style = ExpandStyle;
            }
            else
            {
                style = NoExpandStyle;
            }

            return style;
        }
    }
}
