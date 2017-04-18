using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI.StyleSelectors
{
    public class RatingBarStyleSelector : StyleSelector
    {
        public Style StarFullStyle { get; set; }

        public Style StarHalfStyle { get; set; }

        public Style StarEmptyStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            //RatingBar ratingBar = RatingBar.ItemsControlFromItemContainer(container) as RatingBar;
            Style style = new Style();
            //int index = Convert.ToInt32(item);
            
            
            //if (index == 0)
            //{
            //    style = StarFullStyle;
            //}
            //else if (index == 1)
            //{
            //    style = StarHalfStyle;
            //}
            //else
            //{
            //    style = StarEmptyStyle;
            //}

            return style;
        }
    }
}
