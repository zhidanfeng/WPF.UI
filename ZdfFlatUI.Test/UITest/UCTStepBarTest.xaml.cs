using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// UCTStepBarTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTStepBarTest : UserControl
    {
        private int _Step;

        public int Step
        {
            get
            {
                int temp = this.stepBar.Progress;
                return ++temp;
            }
        }

        ObservableCollection<string> list = new ObservableCollection<string>();
        public UCTStepBarTest()
        {
            InitializeComponent();

            
            list.Add("已完成");
            list.Add("进行中");
            list.Add("已完成");
            list.Add("进行中");

            this.stepBar.ItemsSource = list;
            this.text.DataContext = Step;
        }

        private void FlatButton_Click(object sender, RoutedEventArgs e)
        {
            //list.Add("进行中");
            this.stepBar.Progress++;
            this.text.DataContext = Step;
        }

        private void FlatButton_Click1(object sender, RoutedEventArgs e)
        {
            this.stepBar.Progress--;
            this.text.DataContext = Step;
        }

        private void btn_AddItem(object sender, RoutedEventArgs e)
        {
            list.Add("进行中");
        }

        private void btn_RemoveItem(object sender, RoutedEventArgs e)
        {
            list.RemoveAt(0);
            this.text.DataContext = Step;
        }
    }
}
