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
    /// UCTTimeline.xaml 的交互逻辑
    /// </summary>
    public partial class UCTTimeline : UserControl
    {
        public UCTTimeline()
        {
            InitializeComponent();

            this.InitTimeline2();
        }

        private void InitTimeline2()
        {
            ObservableCollection<Tuple<int, string, string>> list = new ObservableCollection<Tuple<int, string, string>>();
            for (int i = 0; i < 5; i++)
            {
                //System.Threading.Thread.Sleep(1000);
                list.Add(new Tuple<int, string, string>(i, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "哈哈哈哈"));
            }

            this.timeline2.ItemsSource = list;
            this.timeline3.ItemsSource = list;
            this.timeline4.ItemsSource = list;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            switch (btn.Tag.ToString())
            {
                case "First":
                    this.timeline.Items.Insert(0, new TimelineItem() { Content = "Add First" });
                    break;
                case "Middle":
                    this.timeline.Items.Insert(1, new TimelineItem() { Content = "Add Middle" });
                    break;
                case "Last":
                    this.timeline.Items.Add(new TimelineItem() { Content = "Add Last" });
                    break;
                case "RemoveFirst":
                    this.timeline.Items.RemoveAt(0);
                    break;
                case "RemoveLast":
                    this.timeline.Items.RemoveAt(this.timeline.Items.Count - 1);
                    break;
                case "RemoveMiddle":
                    this.timeline.Items.RemoveAt(1);
                    break;
                default:
                    break;
            }
        }
    }
}
