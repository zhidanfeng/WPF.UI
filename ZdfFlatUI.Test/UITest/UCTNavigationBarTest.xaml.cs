using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdfFlatUI.Test.Model;
using ZdfFlatUI.Test.ViewModel;
using ZdfFlatUI.Utils;

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTNavigationBarTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTNavigationBarTest : UserControl
    {
        public UCTNavigationBarTest()
        {
            InitializeComponent();

            this.PointInputResource.ItemsSource = this.GetData("PointInputResource");
            this.PointIncept.ItemsSource = this.GetData("PointIncept");
            this.PointCommand.ItemsSource = this.GetData("PointCommand");
            this.PointProcess.ItemsSource = this.GetData("PointProcess");

            this.DataContext = new NavigationBarTestViewModel();
        }

        private List<string> GetData(string str)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                list.Add(str + i.ToString());
            }
            return list;
        }

        private void ChangeSource_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AnchorPointPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
            var scrollBar = MyVisualTreeHelper.FindChild<ScrollBar>(this.scrollViewer, "PART_VerticalScrollBar");
            scrollBar.BeginAnimation(UIElement.OpacityProperty, daV);
        }

        private void AnchorPointPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //DoubleAnimation daV = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(1)));
            //var scrollBar = MyVisualTreeHelper.FindChild<ScrollBar>(this.scrollViewer, "PART_VerticalScrollBar");
            //scrollBar.BeginAnimation(UIElement.OpacityProperty, daV);
        }

        private void PointInputResource_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.scrollViewer.ScrollToVerticalOffset(this.scrollViewer.VerticalOffset - e.Delta);
        }
    }
}
