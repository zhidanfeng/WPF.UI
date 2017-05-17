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
using ZdfFlatUI.Test.Model;

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTCascaderBoxTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTCascaderBoxTest : UserControl
    {
        public UCTCascaderBoxTest()
        {
            InitializeComponent();

            ObservableCollection<Dept> datas = new ObservableCollection<Dept>();
            object item = new object();

            for (int i = 0; i < 10; i++)
            {
                Dept dept = new Dept();
                dept.ID = i.ToString();
                dept.Name = "第一级" + i;
                if (i % 2 == 0)
                {
                    dept.Children = new ObservableCollection<Dept>();
                    for (int j = 0; j < 5; j++)
                    {
                        Dept child = new Dept();
                        child.ID = i.ToString() + j.ToString();
                        child.Name = "第二级第二级第二级" + i.ToString() + j.ToString();

                        

                        if (j % 2 == 0)
                        {
                            if(j == 0)
                            {
                                child.Name = "第二级第二" + i.ToString() + j.ToString();
                            }
                            child.Children = new ObservableCollection<Dept>();
                            for (int k = 0; k < 2; k++)
                            {
                                Dept three = new Dept();
                                three.ID = i.ToString() + j.ToString() + k.ToString();
                                three.Name = "第二级第二级" + i.ToString() + j.ToString() + k.ToString();
                                child.Children.Add(three);
                            }
                        }
                        dept.Children.Add(child);

                        if (i == 0 && j == 0)
                        {
                            item = dept;
                        }
                    }
                }

                datas.Add(dept);
            }

            this.treeView.ItemsSource = datas;
            this.treeView.ChildMemberPath = "Children";
            this.treeView.DisplayMemberPath = "Name";
            this.treeView.SelectedItem = item;

            this.treeView1.ItemsSource = datas;
            this.treeView1.ChildMemberPath = "Children";
            this.treeView1.DisplayMemberPath = "Name";
        }
    }
}
