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
    /// UCTMessageBoxTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTMessageBoxTest : UserControl
    {
        public UCTMessageBoxTest()
        {
            InitializeComponent();
        }

        private void FlatButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Content.ToString())
            {
                case "Info":
                    if(ZMessageBox.Show("内容", "标题", EnumPromptType.Error) == MessageBoxResult.OK)
                    {
                        MessageBox.Show("点击了OK");
                    }
                    break;
                case "Error":
                    ZMessageBox.Show(Window.GetWindow(this), "您目前使用的浏览器版本过低，可能导致产品部分功能无法正常使用", "", MessageBoxButton.YesNoCancel, EnumPromptType.Warn);
                    break;
                case "Warn":
                    ZMessageBox.Show("内容", EnumPromptType.Warn);
                    break;
                case "Success":
                    ZMessageBox.Show("内容", EnumPromptType.Success);
                    break;
                default:
                    break;
            }
        }
    }
}
