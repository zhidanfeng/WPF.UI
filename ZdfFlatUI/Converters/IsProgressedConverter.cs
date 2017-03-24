using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ZdfFlatUI.Converters
{
    public class IsProgressedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((values[0] is ContentControl && values[1] is int) == false)
            {
                return EnumCompare.None;
            }
            //bool checkNextItem = System.Convert.ToBoolean(parameter.ToString());

            ContentControl contentControl = values[0] as ContentControl;
            int progress = (int)values[1];
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(contentControl);

            if(itemsControl == null)
            {
                return EnumCompare.None;
            }

            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(contentControl);
            
            if(index < progress)
            {
                return EnumCompare.Less;
            }
            else if(index == progress)
            {
                return EnumCompare.Equal;
            }
            return EnumCompare.Large;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
