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
    /// UCTFloatingActionControl.xaml 的交互逻辑
    /// </summary>
    public partial class UCTFloatingActionControl : UserControl
    {
        public UCTFloatingActionControl()
        {
            InitializeComponent();

            ObservableCollection<Student> list = new ObservableCollection<Student>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Student() { Name = "姓名" + i, ID = i.ToString() });
            }

            this.FloatingActionMenu.ItemsSource = list;
            this.FloatingActionMenu.DisplayMemberPath = "Name";
            this.FloatingActionMenu.DisplayTipContentMemberPath = "ID";
        }

        private void FloatingActionMenu_ItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //MessageBox.Show(string.Format("OldValue={0}, NewValue={1}", e.OldValue, e.NewValue));
        }
    }
}
