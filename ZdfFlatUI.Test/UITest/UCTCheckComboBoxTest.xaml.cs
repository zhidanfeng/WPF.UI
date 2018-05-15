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

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTCheckComboBoxTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTCheckComboBoxTest : UserControl
    {
        public UCTCheckComboBoxTest()
        {
            InitializeComponent();

            List<CheckComboBoxTest> data = new List<CheckComboBoxTest>();
            data.Add(new CheckComboBoxTest(1, "C#"));
            data.Add(new CheckComboBoxTest(2, "C++"));
            data.Add(new CheckComboBoxTest(3, "C语言"));
            data.Add(new CheckComboBoxTest(4, "Javascript"));
            data.Add(new CheckComboBoxTest(5, "Object C"));
            data.Add(new CheckComboBoxTest(6, "Java"));

            this.CheckComboBox.ItemsSource = data;
            this.CheckComboBox.DisplayMemberPath = "Content";
            this.CheckComboBox.SelectedObjList.Add(data[1]);
            this.CheckComboBox.SelectedObjList.Add(data[3]);
        }

        internal class CheckComboBoxTest
        {
            public int ID { get; set; }
            public string Content { get; set; }

            public CheckComboBoxTest(int id, string content)
            {
                this.ID = id;
                this.Content = content;
            }
        }

        private void btnGetContent_Click(object sender, RoutedEventArgs e)
        {
            ZdfFlatUI.ZMessageBox.Show(this.CheckComboBox.Content.ToString(), "", MessageBoxButton.YesNoCancel);
        }
    }
}
