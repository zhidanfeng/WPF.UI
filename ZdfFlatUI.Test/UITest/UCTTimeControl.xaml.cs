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
    /// UCTTimeControl.xaml 的交互逻辑
    /// </summary>
    public partial class UCTTimeControl : UserControl
    {
        public UCTTimeControl()
        {
            InitializeComponent();
        }

        private void SetTimePicker2_Click(object sender, RoutedEventArgs e)
        {
            this.TimePicker2.SelectedTime = DateTime.Now;
        }

        private void TimeSelector_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            //MessageBox.Show(Convert.ToString(e.NewValue));
        }
    }
}
