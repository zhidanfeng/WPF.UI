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
    /// UCTAccordion.xaml 的交互逻辑
    /// </summary>
    public partial class UCTAccordion : UserControl
    {
        public UCTAccordion()
        {
            InitializeComponent();

            ObservableCollection<Tuple<int, string, string>> list = new ObservableCollection<Tuple<int, string, string>>();

            for (int i = 0; i < 300; i++)
            {
                list.Add(new Tuple<int, string, string>(i, i.ToString(), i.ToString()));
            }

            this.FlatListView.ItemsSource = list;
        }
    }
}
