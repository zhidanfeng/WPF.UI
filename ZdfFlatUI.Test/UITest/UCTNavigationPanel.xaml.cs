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
    /// UCTNavigationPanel.xaml 的交互逻辑
    /// </summary>
    public partial class UCTNavigationPanel : UserControl
    {
        public UCTNavigationPanel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.navigationPanel.IndicatorSelectedIndex = 2;
            Button btn = sender as Button;
            switch (btn.Content.ToString())
            {
                case "上":
                    this.navigationPanel.IndicatorPlacement = Dock.Top;
                    break;
                case "下":
                    this.navigationPanel.IndicatorPlacement = Dock.Bottom;
                    break;
                case "左":
                    this.navigationPanel.IndicatorPlacement = Dock.Left;
                    break;
                case "右":
                    this.navigationPanel.IndicatorPlacement = Dock.Right;
                    break;
                default:
                    break;
            }
        }
    }
}
