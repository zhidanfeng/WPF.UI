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
using System.Collections.ObjectModel;

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTTag.xaml 的交互逻辑
    /// </summary>
    public partial class UCTTag : UserControl
    {
        private ObservableCollection<string> list = new ObservableCollection<string>();

        public UCTTag()
        {
            InitializeComponent();

            list.Add("标签一");
            list.Add("标签二");

            this.TagBox.ItemsSource = list;
            this.TagInputBox.ItemsSource = list;
        }

        private void Tag_Closed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MessageBox.Show("点击了关闭按钮");
        }

        private void AddOneClick(object sender, RoutedEventArgs e)
        {
            Tag tag = new Tag()
            {
                CornerRadius = new CornerRadius(3),
                Content = "新标签",
                //Margin = new Thickness(2, 2, 2, 2)
            };
            //this.stackPanel.Children.Add(new Tag() { CornerRadius = new CornerRadius(3), Content = "新标签", Margin = new Thickness(5,0,5,0) });

            this.list.Add("新标签");
        }
    }
}
