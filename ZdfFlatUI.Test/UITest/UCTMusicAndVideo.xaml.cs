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

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTMusicAndVideo.xaml 的交互逻辑
    /// </summary>
    public partial class UCTMusicAndVideo : UserControl
    {
        public UCTMusicAndVideo()
        {
            InitializeComponent();

            string musicPath = @"D:\私人文件夹\Music\林俊杰 - 巴洛克先生 (feat.王力宏 小提琴特别演奏).mp3";
            //this.musicPlayer.SoundSource = musicPath;
        }
    }
}
