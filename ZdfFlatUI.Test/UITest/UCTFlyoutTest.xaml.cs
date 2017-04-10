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
    /// UCTFlyoutTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTFlyoutTest : UserControl
    {
        public UCTFlyoutTest()
        {
            InitializeComponent();
        }

        private void FlatButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.FlyoutWindow window = new Windows.FlyoutWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
    }
}
