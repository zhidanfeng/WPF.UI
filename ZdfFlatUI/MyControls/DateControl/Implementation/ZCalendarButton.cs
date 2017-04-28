using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI.Primitives
{
    public class ZCalendarButton : Button
    {
        #region Private属性

        #endregion

        #region Fields
        public ZCalendar Owner { get; set; }
        #endregion

        #region 依赖属性set get

        #region HasSelectedDates
        public bool HasSelectedDates
        {
            get { return (bool)GetValue(HasSelectedDatesProperty); }
            set { SetValue(HasSelectedDatesProperty, value); }
        }
        
        public static readonly DependencyProperty HasSelectedDatesProperty =
            DependencyProperty.Register("HasSelectedDates", typeof(bool), typeof(ZCalendarButton), new PropertyMetadata(false));
        #endregion

        #endregion

        #region Constructors
        static ZCalendarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZCalendarButton), new FrameworkPropertyMetadata(typeof(ZCalendarButton)));
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
