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
    /// UCTTextBoxTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTTextBoxTest : UserControl
    {
        public UCTTextBoxTest()
        {
            InitializeComponent();

            List<Student> list = new List<Student>();
            list.Add(new Student() { ID = "1", Name = "1" });
            list.Add(new Student() { ID = "2", Name = "111" });
            list.Add(new Student() { ID = "3", Name = "121" });
            list.Add(new Student() { ID = "4", Name = "2" });
            this.AutoCompleteBox.ItemsSource = list;
            this.AutoCompleteBox.DisplayMemberPath = "Name";
        }

        private void btnStartValidate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
