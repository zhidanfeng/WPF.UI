using DoubanFM.Bass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace ZdfFlatUI
{
    /// <summary>
    /// 音频播放器
    /// </summary>
    public class MusicPlayer : Control
    {
        #region private fields
        private BassEngine bassPlayer = null;
        private DispatcherTimer timer = new DispatcherTimer();
        private Enum playState = EnumPlayState.Stop;
        #endregion

        #region Control Part
        private ToggleButton PART_PlayAndPauseButton;
        private ToggleButton PART_VolumeButton;
        private Slider PART_MusicProgress;
        private TextBlock PART_CurrentProgress;
        private TextBlock PART_MusicTotalLength;
        #endregion

        #region DependencyProperty

        #endregion

        #region Inner DependencyProperty

        #region MusicTotalLength

        /// <summary>
        /// 音频文件时长
        /// </summary>
        public string MusicTotalLength
        {
            get { return (string)GetValue(MusicTotalLengthProperty); }
            private set { SetValue(MusicTotalLengthProperty, value); }
        }
        
        public static readonly DependencyProperty MusicTotalLengthProperty =
            DependencyProperty.Register("MusicTotalLength", typeof(string), typeof(MusicPlayer));

        private static object CoreceMusicTotalLength(DependencyObject d, object baseValue)
        {
            DateTime dt = DateTime.MinValue;
            if(DateTime.TryParse(Convert.ToString(baseValue), out dt))
            {
                return dt.ToString("HH:mm:ss");
            }
            
            return null;
        }

        private static void MusicTotalLengthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        #endregion

        #region CurrentProgress

        public string CurrentProgress
        {
            get { return (string)GetValue(CurrentProgressProperty); }
            set { SetValue(CurrentProgressProperty, value); }
        }
        
        public static readonly DependencyProperty CurrentProgressProperty =
            DependencyProperty.Register("CurrentProgress", typeof(string), typeof(MusicPlayer));

        #endregion

        #region IsPlayingInner

        /// <summary>
        /// 当前音频是否正在播放。true：正在播放，显示暂停图标；false：没有在播放，显示播放图标
        /// </summary>
        public bool IsPlayingInner
        {
            get { return (bool)GetValue(IsPlayingInnerProperty); }
            set { SetValue(IsPlayingInnerProperty, value); }
        }
        
        public static readonly DependencyProperty IsPlayingInnerProperty =
            DependencyProperty.Register("IsPlayingInner", typeof(bool), typeof(MusicPlayer), new PropertyMetadata(false, IsPlayInnerChanged));

        private static void IsPlayInnerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MusicPlayer musicPlayer = d as MusicPlayer;
            if(bool.Parse(e.NewValue.ToString()))
            {
                musicPlayer.bassPlayer.Play();
                musicPlayer.playState = EnumPlayState.Play;
            }
            else
            {
                musicPlayer.bassPlayer.Pause();
                musicPlayer.playState = EnumPlayState.Pause;
            }
        }

        #endregion

        #endregion

        #region Constructors

        static MusicPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MusicPlayer), new FrameworkPropertyMetadata(typeof(MusicPlayer)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Loaded += MusicPlayer_Loaded;

            this.PART_MusicProgress = this.GetTemplateChild("PART_MusicProgress") as Slider;
        }

        private void MusicPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitPlayer();

            if(this.PART_MusicProgress != null)
            {
                this.PART_MusicProgress.Maximum = bassPlayer.ChannelLength.TotalSeconds;
                this.PART_MusicProgress.Minimum = 0d;
            }
        }
        #endregion

        #region private function

        /// <summary>
        /// 初始化播放器
        /// </summary>
        private void InitPlayer()
        {
            //if (Un4seen.Bass.Utils.Is64Bit)
            //    targetPath = Path.Combine(Path.GetDirectoryName(typeof(BassEngine).Assembly.GetModules()[0].FullyQualifiedName), "x64");
            //else
            //    targetPath = Path.Combine(Path.GetDirectoryName(typeof(BassEngine).Assembly.GetModules()[0].FullyQualifiedName), "x86");

            DoubanFM.Bass.BassEngine.ExplicitInitialize(null);
            bassPlayer = BassEngine.Instance;
            bassPlayer.Volume = 15 / 100.0;
            this.PlayMusic(@"D:\私人文件夹\Music\林俊杰 - 巴洛克先生 (feat.王力宏 小提琴特别演奏).mp3");

            bassPlayer.TrackEnded += BassPlayer_TrackEnded;
            bassPlayer.PropertyChanged += BassPlayer_PropertyChanged;

            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;

            bassPlayer.Play();

            this.Timer_Tick(null, null);
            timer.Start();
        }

        private void PlayMusic(string filePath)
        {
            bassPlayer.OpenFile(filePath);

            this.MusicTotalLength = this.GetFormatTime(bassPlayer.ChannelLength.Hours
                , bassPlayer.ChannelLength.Minutes
                , bassPlayer.ChannelLength.Seconds
                , bassPlayer.ChannelLength.Milliseconds);

            this.CurrentProgress = this.GetFormatTime(bassPlayer.ChannelPosition.Hours
                , bassPlayer.ChannelPosition.Minutes
                , bassPlayer.ChannelPosition.Seconds
                , bassPlayer.ChannelPosition.Milliseconds);
        }

        private string GetFormatTime(int hour, int minute, int second, int Milliseconds)
        {
            string hourInner = "00";
            string minuteInner = "00";
            string secondInner = "00";
            if(hour == 0)
            {
                minuteInner = (minute < 10) ? "0" + minute : minute.ToString();
                //if(Milliseconds > 500)
                //{
                //    second++;
                //}
                secondInner = (second < 10) ? "0" + second : second.ToString();
            }
            else
            {

            }
            return string.Format("{0}:{1}", minuteInner, secondInner);
        }
        #endregion

        #region Event Implement Function

        /// <summary>
        /// 当前音频文件播放完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BassPlayer_TrackEnded(object sender, EventArgs e)
        {
            this.playState = EnumPlayState.Stop;
            this.timer.Stop();
        }

        /// <summary>
        /// 音频播放进度定时器执行事件，主要是为了显示当前进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.CurrentProgress = this.GetFormatTime(bassPlayer.ChannelPosition.Hours
                , bassPlayer.ChannelPosition.Minutes
                , bassPlayer.ChannelPosition.Seconds
                , bassPlayer.ChannelPosition.Milliseconds);
            this.PART_MusicProgress.Value = this.bassPlayer.ChannelPosition.TotalSeconds;

            System.Diagnostics.Debug.WriteLine(this.CurrentProgress + ", " + this.PART_MusicProgress.Value);
        }


        private void BassPlayer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsPlaying")
            {
                this.IsPlayingInner = bassPlayer.IsPlaying;
            }
        }
        #endregion
    }
}
