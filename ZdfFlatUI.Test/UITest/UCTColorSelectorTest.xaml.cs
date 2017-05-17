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
    /// UCTColorSelectorTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTColorSelectorTest : UserControl
    {
        public UCTColorSelectorTest()
        {
            InitializeComponent();

            List<Color> list = new List<Color>
            {
                Color.FromRgb(0, 0, 0),
                Color.FromRgb(153, 51, 0),
                Color.FromRgb(51, 51, 0),
                Color.FromRgb(0, 51, 0),
                Color.FromRgb(0, 51, 102),
                Color.FromRgb(0, 0, 128),
                Color.FromRgb(51, 51, 153),
                Color.FromRgb(51, 51, 51),
                Color.FromRgb(128, 0, 0),
                Color.FromRgb(255, 102, 0),
                Color.FromRgb(128, 128, 0),
                Color.FromRgb(0, 128, 0),
                Color.FromRgb(0, 128, 128),
                Color.FromRgb(0, 0, 255),
                Color.FromRgb(102, 102, 153),
                Color.FromRgb(128, 128, 128),
                Color.FromRgb(255, 0, 0),
                Color.FromRgb(255, 153, 0),
                Color.FromRgb(153, 204, 0),
                Color.FromRgb(51, 153, 102),
                Color.FromRgb(51, 204, 204),
                Color.FromRgb(51, 102, 255),
                Color.FromRgb(128, 0, 128),
                Color.FromRgb(153, 153, 153),
                Color.FromRgb(255, 0, 255),
                Color.FromRgb(255, 204, 0),
                Color.FromRgb(255, 255, 0),
                Color.FromRgb(0, 255, 0),
                Color.FromRgb(0, 255, 255),
                Color.FromRgb(0, 204, 255),
                Color.FromRgb(153, 51, 102),
                Color.FromRgb(192, 192, 192),
                Color.FromRgb(255, 153, 204),
                Color.FromRgb(255, 204, 153),
                Color.FromRgb(255, 255, 153),
                Color.FromRgb(0, 255, 0),
                Color.FromRgb(204, 255, 204),
                Color.FromRgb(153, 204, 255),
                Color.FromRgb(204, 153, 255),
            };
            this.ColorSelector.ItemsSource = new ReadOnlyCollection<Color>(list);
        }
    }
}
