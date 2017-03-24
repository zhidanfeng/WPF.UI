using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ZdfFlatUI
{
    public class NoticeMessage : System.Windows.Controls.Control
    {
        static NoticeMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NoticeMessage), new FrameworkPropertyMetadata(typeof(NoticeMessage)));
        }

        private Timer mTimer;

        #region 依赖属性
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content"
            , typeof(string), typeof(NoticeMessage));

        /// <summary>
        /// 显示内容
        /// </summary>
        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty IsShowProperty = DependencyProperty.Register("IsShow"
            , typeof(bool), typeof(NoticeMessage), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIsShowChanged)));

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        public static readonly DependencyProperty MessageTypeProperty = DependencyProperty.Register("MessageType"
            , typeof(EnumMessageType), typeof(NoticeMessage), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMessageTypeChanged)));

        /// <summary>
        /// 是否显示
        /// </summary>
        public EnumMessageType MessageType
        {
            get { return (EnumMessageType)GetValue(MessageTypeProperty); }
            set
            {
                SetValue(MessageTypeProperty, value);
                this.MessageTypeStr = value.ToString();
            }
        }

        public static readonly DependencyProperty MessageTypeStrProperty = DependencyProperty.Register("MessageTypeStr"
            , typeof(string), typeof(NoticeMessage));

        /// <summary>
        /// 提示类型文本
        /// </summary>
        public string MessageTypeStr
        {
            get { return (string)GetValue(MessageTypeStrProperty); }
            set { SetValue(MessageTypeStrProperty, value); }
        }

        #region Path相关属性
        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth"
            , typeof(double), typeof(NoticeMessage), new FrameworkPropertyMetadata(13d));
        /// <summary>
        /// 图标的宽度
        /// </summary>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData"
            , typeof(PathGeometry), typeof(NoticeMessage));
        /// <summary>
        /// 图标资源，这里使用的Path作为图标
        /// </summary>
        public PathGeometry PathData
        {
            get { return (PathGeometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register("IconColor"
            , typeof(Brush), typeof(NoticeMessage));
        /// <summary>
        /// 图标的颜色
        /// </summary>
        public Brush IconColor
        {
            get { return (Brush)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }
        #endregion

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
            , typeof(System.Windows.CornerRadius), typeof(NoticeMessage));
        /// <summary>
        /// 文本输入框的边框圆角
        /// </summary>
        public System.Windows.CornerRadius CornerRadius
        {
            get { return (System.Windows.CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        #region 显示时间
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration"
            , typeof(double), typeof(NoticeMessage)
            , new FrameworkPropertyMetadata(new PropertyChangedCallback(OnDurationChanged)));

        /// <summary>
        /// 显示时间
        /// </summary>
        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }
        #endregion

        #endregion

        #region 依赖属性回调方法
        private static void OnIsShowChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NoticeMessage noticeMessage = (NoticeMessage)sender;
            if(e.Property == IsShowProperty)
            {
                if(Convert.ToBoolean(e.NewValue))
                {
                    noticeMessage.IsShow = false;
                    noticeMessage.mTimer.Enabled = false;

                    noticeMessage.mTimer.Enabled = true;
                    noticeMessage.ShowAnimation();
                }
            }
        }

        private static void OnDurationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NoticeMessage noticeMessage = (NoticeMessage)sender;
            if (e.Property == DurationProperty)
            {
                noticeMessage.mTimer.Interval = Convert.ToDouble(e.NewValue);
            }
        }

        private static void OnMessageTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //NoticeMessage noticeMessage = (NoticeMessage)sender;
            //if (e.Property == MessageTypeProperty)
            //{
            //    switch ((EnumMessageType)e.NewValue)
            //    {
            //        case EnumMessageType.Warn:
            //            noticeMessage.IconColor = new SolidColorBrush(Color.FromRgb(239, 186, 72));
            //            break;
            //        case EnumMessageType.Info:
            //            noticeMessage.IconColor = new SolidColorBrush(Color.FromRgb(83, 194, 232));
            //            break;
            //        case EnumMessageType.Error:
            //            noticeMessage.IconColor = new SolidColorBrush(Color.FromRgb(228, 99, 99));
            //            break;
            //        case EnumMessageType.Success:
            //            noticeMessage.IconColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }
        #endregion

        #region 构造函数
        public NoticeMessage() : base()
        {
            mTimer = new Timer();
            mTimer.Interval = this.Duration == 0 ? 1500 : this.Duration;
            mTimer.Elapsed += MTimer_Elapsed;
            this.Opacity = 0;
        }

        private void MTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke((System.Threading.ThreadStart)delegate
            {
                IsShow = false;
                this.HideAnimation();
                this.mTimer.Enabled = false;

            }, System.Windows.Threading.DispatcherPriority.Normal);
        }
        #endregion

        private void ShowAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            this.BeginAnimation(OpacityProperty, animation);
        }

        private void HideAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            this.BeginAnimation(OpacityProperty, animation);
        }
    }

    public enum EnumMessageType
    {
        Warn,
        Info,
        Error,
        Success,
    }
}
