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
    /// UCTDropDownTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTDropDownTest : UserControl
    {
        public UCTDropDownTest()
        {
            InitializeComponent();

            List<string> list = new List<string>();
            list.Add("升级");
            list.Add("卸载");

            //this.DropDownButton.ItemsSource = list;
        }

        private void DropDownButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void DropDownButton_ItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MessageBox.Show(e.NewValue.ToString());
        }
    }
}
