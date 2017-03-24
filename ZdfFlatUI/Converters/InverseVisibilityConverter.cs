using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ZdfFlatUI.Converters
{
    /// <summary>
    /// 反转Visibility，两个控件可见性互斥，即一个可见，那么另外一个就不可见
    /// </summary>
    public class InverseVisibilityConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(Visibility.Hidden) || value.Equals(Visibility.Collapsed))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
