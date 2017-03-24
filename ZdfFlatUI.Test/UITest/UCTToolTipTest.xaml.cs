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
    /// UCTToolTipTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTToolTipTest : UserControl
    {
        private string _ToolTipText;

        public string ToolTipText
        {
            get { return _ToolTipText; }
            set { _ToolTipText = value; }
        }

        public UCTToolTipTest()
        {
            InitializeComponent();

            this.ToolTipText = "哈哈";
            this.text.DataContext = ToolTipText;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            this.ToolTipText = "哈哈哈哈哈哈哈哈哈哈";
            this.text.DataContext = ToolTipText;
        }
    }
}
