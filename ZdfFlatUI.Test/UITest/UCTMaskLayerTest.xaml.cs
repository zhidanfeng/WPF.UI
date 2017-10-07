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
    /// UCTMaskLayerTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTMaskLayerTest : UserControl
    {
        public UCTMaskLayerTest()
        {
            InitializeComponent();
        }

        private void btnOpenMaskLayer_Click(object sender, RoutedEventArgs e)
        {
            this.maskLayer.SetValue(ZdfFlatUI.Behaviors.MaskLayerBehavior.IsOpenProperty, true);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.maskLayer.SetValue(ZdfFlatUI.Behaviors.MaskLayerBehavior.IsOpenProperty, false);
        }
    }
}
