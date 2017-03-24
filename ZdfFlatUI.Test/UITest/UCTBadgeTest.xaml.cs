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
    /// UCTBadgeTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTBadgeTest : UserControl
    {
        public UCTBadgeTest()
        {
            InitializeComponent();
        }

        private void FlatButton_Click(object sender, RoutedEventArgs e)
        {
            int number = (int)this.button.GetValue(BadgeAdorner.NumberProperty);
            this.button.SetValue(BadgeAdorner.NumberProperty, ++number);
        }

        private void btnChangeAdornerType_Click(object sender, RoutedEventArgs e)
        {
            //BadgeType badgeType = (BadgeType)this.text.GetValue(BadgeAdorner.BadgeTypeProperty);

            if(this.btnChangeAdornerType.IsChecked == true)
            {
                this.text.SetValue(BadgeAdorner.BadgeTypeProperty, BadgeType.Dot);
            }
            else
            {
                this.text.SetValue(BadgeAdorner.BadgeTypeProperty, BadgeType.Normal);
            }
        }
    }
}
