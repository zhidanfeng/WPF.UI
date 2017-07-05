using DoubanFM.Bass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
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
        #endregion

        #region Control Part
        private ToggleButton PART_PlayAndPauseButton;
        private ToggleButton PART_VolumeButton;
        private FlatSilder PART_MusicProgress;
        private TextBlock PART_CurrentProgress;
        private TextBlock PART_MusicTotalLength;
        #endregion

        #region DependencyProperty

        #region IsAutoPlay

        /// <summary>
        /// 获取或者设置是否自动播放音频文件
        /// </summary>
        public bool IsAutoPlay
        {
            get { return (bool)GetValue(IsAutoPlayProperty); }
            set { SetValue(IsAutoPlayProperty, value); }
        }
        
        public static readonly DependencyProperty IsAutoPlayProperty =
            DependencyProperty.Register("IsAutoPlay", typeof(bool), typeof(MusicPlayer), new PropertyMetadata(true));

        #endregion

        #region SoundSource

        /// <summary>
        /// 获取或者设置音频文件路径
        /// </summary>
        public string SoundSource
        {
            get { return (string)GetValue(SoundSourceProperty); }
            set { SetValue(SoundSourceProperty, value); }
        }
        
        public static readonly DependencyProperty SoundSourceProperty =
            DependencyProperty.Register("SoundSource", typeof(string), typeof(MusicPlayer), new PropertyMetadata(string.Empty));

        #endregion

        #region CurrentProgress

        /// <summary>
        /// 获取或者设置当前音频的播放进度
        /// </summary>
        public string CurrentProgress
        {
            get { return (string)GetValue(CurrentProgressProperty); }
            set { SetValue(CurrentProgressProperty, value); }
        }

        public static readonly DependencyProperty CurrentProgressProperty =
            DependencyProperty.Register("CurrentProgress", typeof(string), typeof(MusicPlayer));

        #endregion

        #region PlayState

        /// <summary>
        /// 获取或者设置播放器的播放状态
        /// </summary>
        public EnumPlayState PlayState
        {
            get { return (EnumPlayState)GetValue(PlayStateProperty); }
            set { SetValue(PlayStateProperty, value); }
        }
        
        public static readonly DependencyProperty PlayStateProperty =
            DependencyProperty.Register("PlayState", typeof(EnumPlayState), typeof(MusicPlayer), new PropertyMetadata(EnumPlayState.Stop));

        #endregion

        #endregion

        #region Inner DependencyProperty

        #region MusicTotalLength

        /// <summary>
        /// 获取或者设置音频文件时长
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
            if (DateTime.TryParse(Convert.ToString(baseValue), out dt))
            {
                return dt.ToString("HH:mm:ss");
            }

            return null;
        }

        private static void MusicTotalLengthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region IsPlayingInner

        /// <summary>
        /// 当前音频是否正在播放。true：正在播放，显示暂停图标；false：没有在播放，显示播放图标
        /// </summary>
        public bool IsPlayingInner
        {
            get { return (bool)GetValue(IsPlayingInnerProperty); }
            private set { SetValue(IsPlayingInnerProperty, value); }
        }
        
        public static readonly DependencyProperty IsPlayingInnerProperty =
            DependencyProperty.Register("IsPlayingInner", typeof(bool), typeof(MusicPlayer), new PropertyMetadata(false, IsPlayInnerChanged));

        private static void IsPlayInnerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MusicPlayer musicPlayer = d as MusicPlayer;
            bool newValue = bool.Parse(e.NewValue.ToString());
            if (newValue)
            {
                if(musicPlayer.PlayState == EnumPlayState.Stop)
                {
                    musicPlayer.PlayMusic(musicPlayer.SoundSource);
                }
                else
                {
                    musicPlayer.bassPlayer.Play();
                }
                musicPlayer.PlayState = EnumPlayState.Play;
            }
            else
            {
                musicPlayer.bassPlayer.Pause();
                if(musicPlayer.PlayState == EnumPlayState.Play)
                {
                    musicPlayer.PlayState = EnumPlayState.Pause;
                }
            }

            //继续或者停止更新音频播放进度条的进度和当前的播放时长
            musicPlayer.timer.IsEnabled = newValue;
        }

        #endregion

        #endregion

        #region Constructors

        static MusicPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MusicPlayer), new FrameworkPropertyMetadata(typeof(MusicPlayer)));

            string targetPath;
            if (Un4seen.Bass.Utils.Is64Bit)
            {
                targetPath = Path.Combine(Path.GetDirectoryName(typeof(BassEngine).Assembly.GetModules()[0].FullyQualifiedName), "x64");
            }
            else
            {
                targetPath = Path.Combine(Path.GetDirectoryName(typeof(BassEngine).Assembly.GetModules()[0].FullyQualifiedName), "x86");
            }
            Un4seen.Bass.Bass.LoadMe(targetPath);
            DoubanFM.Bass.BassEngine.ExplicitInitialize(null);
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Loaded += MusicPlayer_Loaded;

            this.PART_MusicProgress = this.GetTemplateChild("PART_MusicProgress") as FlatSilder;
            this.PART_CurrentProgress = this.GetTemplateChild("PART_CurrentProgress") as TextBlock;

            if (this.PART_MusicProgress != null)
            {
                this.PART_MusicProgress.DropValueChanged += PART_MusicProgress_DropValueChanged;
            }
        }

        private void PART_MusicProgress_DropValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.bassPlayer.ChannelPosition = TimeSpan.FromSeconds(e.NewValue);
        }
        #endregion

        #region private function

        /// <summary>
        /// 初始化播放器
        /// </summary>
        private void InitPlayer()
        {
            bassPlayer = BassEngine.Instance;
            bassPlayer.Volume = 15 / 100.0;

            bassPlayer.TrackEnded += BassPlayer_TrackEnded;
            bassPlayer.OpenFailed += BassPlayer_OpenFailed;

            timer.Interval = new TimeSpan(1000000);
            timer.Tick += Timer_Tick;

            this.IsPlayingInner = this.IsAutoPlay;
            this.LoadMusicFile(this.SoundSource);
        }

        private void BassPlayer_OpenFailed(object sender, EventArgs e)
        {
            
        }

        private void PlayMusic(string filePath)
        {
            this.LoadMusicFile(this.SoundSource);

            bassPlayer.Play();
            
            timer.Start();
        }

        private void LoadMusicFile(string filePath)
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

        private string GetFormatTime(int hour, int minute, int second, int Milliseconds, bool isRound = false)
        {
            string hourInner = "00";
            string minuteInner = "00";
            string secondInner = "00";
            if(hour == 0)
            {
                minuteInner = (minute < 10) ? "0" + minute : minute.ToString();
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
            this.PlayState = EnumPlayState.Stop;
            this.timer.Stop();
            this.IsPlayingInner = false;
            this.PART_MusicProgress.Value = 0;
            this.CurrentProgress = "00:00";
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
                , bassPlayer.ChannelPosition.Milliseconds, true);

            this.PART_MusicProgress.Value = this.bassPlayer.ChannelPosition.TotalSeconds;
        }

        private void MusicPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitPlayer();

            if (this.PART_MusicProgress != null)
            {
                this.PART_MusicProgress.Maximum = Math.Max(1.0, bassPlayer.ChannelLength.TotalSeconds);
                this.PART_MusicProgress.Minimum = 0d;
            }
        }
        #endregion
    }
}
