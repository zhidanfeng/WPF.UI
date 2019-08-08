using System;
using System.Collections.Generic;
using System.IO;
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
    /// UCTVideoPlayerTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTVideoPlayerTest : UserControl
    {
        public UCTVideoPlayerTest()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(tbVideoSource.Text);
            if (fileInfo.Exists)
            {
                this.videoPlayer.Play(fileInfo);
            }
        }

        private void btnPlayUri_Click(object sender, RoutedEventArgs e)
        {
            this.videoPlayer.Play(new Uri(tbVideoSource.Text));
        }
    }
}
