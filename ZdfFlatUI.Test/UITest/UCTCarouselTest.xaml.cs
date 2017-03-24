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
    /// UCTCarouselTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTCarouselTest : UserControl
    {
        public UCTCarouselTest()
        {
            InitializeComponent();

            ObservableCollection<string> list = new ObservableCollection<string>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(i.ToString());
            }
            this.Carousel.ItemsSource = list;

            ObservableCollection<CarouselModel> data = new ObservableCollection<CarouselModel>();
            for (int i = 1; i <= 5; i++)
            {
                data.Add(new CarouselModel()
                {
                    Title = i.ToString(),
                    ImageUrl = string.Format(@"D:\WorkSpace\MySources\MyWPFSource\Zhidanfeng的个人控件库\ZdfFlatUI.Test\Images\img{0}.png", i),
                });
            }
            this.Carousel2.ItemsSource = data;
        }
    }

    public class CarouselModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
