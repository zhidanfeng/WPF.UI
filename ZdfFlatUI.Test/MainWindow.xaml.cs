using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdfFlatUI.Test.ViewModel;

namespace ZdfFlatUI.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> tagList;
        public MainWindow()
        {
            InitializeComponent();

            this.InitSegmentButton();
            this.DataContext = new MainViewModel();
        }

        private void InitSegmentButton()
        {
            List<string> list = new List<string>();
            list.Add("全部");
            list.Add("主任医师");
            list.Add("副主任医师");
            list.Add("住院医生");
            list.Add("其他");
            //哈哈放大师傅
            this.segmentButton.ItemsSource = list;

            List<string> list2 = new List<string>();
            list2.Add("全部");
            list2.Add("主任医师");
            list2.Add("其他");
            //哈哈放大师傅
            this.segmentButton2.ItemsSource = list2;
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Content.ToString())
            {
                case "Info":
                    this.noticeMessage.MessageTypeStr = "Info";
                    //this.noticeMessage.MessageType = EnumMessageType.Info;
                    break;
                case "Error":
                    this.noticeMessage.MessageType = EnumMessageType.Error;
                    break;
                case "Warn":
                    this.noticeMessage.MessageType = EnumMessageType.Warn;
                    break;
                case "Success":
                    this.noticeMessage.MessageType = EnumMessageType.Success;
                    this.noticeMessage.Content = "设置已更新已更新已更新";
                    break;
                default:
                    break;
            }
            this.noticeMessage.IsShow = true;
            this.noticeMessage.Duration = 2000;
        }

        private void TagTextBox_AddItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var vm = this.DataContext as MainViewModel;
            vm.AddCommand.Execute(e.NewValue);
        }

        private void TagTextBox_RemoveItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var vm = this.DataContext as MainViewModel;
            vm.RemoveCommand.Execute(e.NewValue);
        }

        private void beginValidate_Click(object sender, RoutedEventArgs e)
        {
            this.validateTextBox1.IsValidate = true;
            this.validateTextBox2.IsValidate = true;
            this.validateTextBox3.IsValidate = true;
            this.validateTextBox4.IsValidate = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.validateTextBox1.IsEnabled = false;
        }

        private void IconTextBox_EnterKeyClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
