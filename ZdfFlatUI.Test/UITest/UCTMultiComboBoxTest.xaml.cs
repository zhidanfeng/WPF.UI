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
    /// UCTMultiComboBoxTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTMultiComboBoxTest : UserControl
    {
        private ObservableCollection<Student> _Datas;

        public ObservableCollection<Student> Datas
        {
            get { return _Datas; }
            set { _Datas = value; }
        }


        public UCTMultiComboBoxTest()
        {
            InitializeComponent();

            this.Datas = new ObservableCollection<Student>();
            for (int i = 0; i < 10; i++)
            {
                this.Datas.Add(new Student()
                {
                    ID = "1",
                    Name = "zhi" + i,
                });
            }
            this.MultiComboBox.ItemsSource = this.Datas;
            this.MultiComboBox1.ItemsSource = this.Datas;
        }
    }

    public class Student
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
