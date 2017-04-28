using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTDateControl.xaml 的交互逻辑
    /// </summary>
    public partial class UCTDateControl : UserControl
    {
        public UCTDateControl()
        {
            InitializeComponent();

            this.dateControl1.SelectedDate = DateTime.Now;
            this.dateControl5.SelectedDateStart = DateTime.Today.AddDays(-10);
            this.dateControl5.SelectedDateEnd = DateTime.Today;
            this.dateControl7.SelectedDate = DateTime.Now;
        }

        private void btnSetDate_Click(object sender, RoutedEventArgs e)
        {
            this.timePicker.Value = DateTime.Now;
        }

        private void btnGetDate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.timePicker.Value.Value.ToString());
            MessageBox.Show(DateTime.DaysInMonth(2017, 4).ToString());
            MessageBox.Show(DateTime.Now.DayOfWeek.ToString());
        }

        private void btnGetDateTimePicker_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.dateTimePicker.Value.ToString());
        }

        private void btnSetDateTimePicker_Click(object sender, RoutedEventArgs e)
        {
            this.dateTimePicker.Value = DateTime.Now.AddDays(5);
        }

        private void ZCalendar_SelectedDateChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {

        }
    }
}
