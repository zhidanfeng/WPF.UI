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
using ZdfFlatUI.Test.Model;

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTNavigateMenuTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTNavigateMenuTest : UserControl
    {
        private ObservableCollection<MenuInfo> _MenuList;

        public ObservableCollection<MenuInfo> MenuList
        {
            get { return _MenuList; }
            set { _MenuList = value; }
        }

        public UCTNavigateMenuTest()
        {
            InitializeComponent();

            this.MenuList = new ObservableCollection<MenuInfo>();
            this.MenuList.Add(new MenuInfo()
            {
                Name = "文章管理",
                GroupName = "内容管理",
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "评论管理",
                GroupName = "内容管理",
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "举报管理",
                GroupName = "内容管理",
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "新增用户",
                GroupName = "用户管理",
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "活跃用户",
                GroupName = "用户管理",
            });

            this.menu.GroupItemsSource = this.MenuList;
            this.menu.GroupDescriptions = "GroupName";
            //CollectionViewSource view = this.Resources["Data"] as CollectionViewSource;
            //view.Source = this.MenuList;
        }
    }
}
