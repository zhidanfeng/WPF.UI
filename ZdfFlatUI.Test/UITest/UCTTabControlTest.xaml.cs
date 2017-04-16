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
    /// UCTTabControlTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTTabControlTest : UserControl
    {
        ObservableCollection<TabInfo> list = new ObservableCollection<TabInfo>();

        public UCTTabControlTest()
        {
            InitializeComponent();
            
            list.Add(new TabInfo() { Title = "Windows", Type = 1 });
            list.Add(new TabInfo() { Title = "MacOS", Type = 2 });
            list.Add(new TabInfo() { Title = "Linux", Type = 3 });
            this.tabControl3.ItemsSource = list;
        }

        protected class TabInfo
        {
            public string Title { get; set; }
            public int Type { get; set; }
        }

        private void btnTab3AddItem_Click(object sender, RoutedEventArgs e)
        {
            this.list.Add(new TabInfo() { Title = "Android", Type = 4 });
            this.btnTab3AddItem.IsEnabled = false;
        }
    }
}
