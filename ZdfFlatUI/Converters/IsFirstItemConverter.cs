using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ZdfFlatUI.Converters
{
    /// <summary>
    /// 判断该项是否是第一项。
    /// </summary>
    public class IsFirstItemConverter : IMultiValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ContentControl contentPresenter = value[0] as ContentControl;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(contentPresenter);

            bool flag = false;
            if(itemsControl != null)
            {
                int index = itemsControl.ItemContainerGenerator.IndexFromContainer(contentPresenter);
                flag = (index == 0);
            }

            return flag;
        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        #endregion
    }
}
