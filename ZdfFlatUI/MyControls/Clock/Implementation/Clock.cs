using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace ZdfFlatUI
{
    public class Clock : ContentControl
    {
        #region private fields
        
        private DispatcherTimer mSecondTimer;

        #endregion

        #region DependencyProperty

        #region Hour

        public string Hour
        {
            get { return (string)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }
        
        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(string), typeof(Clock));

        #endregion

        #region Minute

        public string Minute
        {
            get { return (string)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }
        
        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(string), typeof(Clock));

        #endregion

        #region Second

        public string Second
        {
            get { return (string)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }
        
        public static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(string), typeof(Clock));

        #endregion

        #endregion

        #region Private DependencyProperty

        #region ShortTicks 短刻度线集合
        /// <summary>
        /// 短刻度线依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty ShortTicksProperty =
            DependencyProperty.Register(
                "ShortTicks",
                typeof(IList<object>),
                typeof(Clock),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置短刻度线，用于绑定PathListBox的ItemsSource
        /// </summary>
        /// <value>短刻度线.</value>
        public IList<object> ShortTicks
        {
            get { return (IList<object>)GetValue(ShortTicksProperty); }
            private set { SetValue(ShortTicksProperty, value); }
        }
        #endregion

        #region LongTicks 长刻度线集合
        /// <summary>
        /// 长刻度线依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty LongTicksProperty =
            DependencyProperty.Register(
                "LongTicks",
                typeof(IList<object>),
                typeof(Clock),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置长刻度线，用于绑定PathListBox的ItemsSource
        /// </summary>
        /// <value>长刻度线.</value>
        public IList<object> LongTicks
        {
            get { return (IList<object>)GetValue(LongTicksProperty); }
            private set { SetValue(LongTicksProperty, value); }
        }
        #endregion

        #region NumberList 长刻度线上显示的数字
        /// <summary>
        /// 长刻度线依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty NumberListProperty =
            DependencyProperty.Register(
                "NumberList",
                typeof(IList<Tuple<object, double>>),
                typeof(Clock),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置长刻度线，用于绑定PathListBox的ItemsSource
        /// </summary>
        /// <value>长刻度线.</value>
        public IList<Tuple<object, double>> NumberList
        {
            get { return (IList<Tuple<object, double>>)GetValue(NumberListProperty); }
            private set { SetValue(NumberListProperty, value); }
        }
        #endregion

        #region HourAngleInner

        public double HourAngleInner
        {
            get { return (double)GetValue(HourAngleInnerProperty); }
            private set { SetValue(HourAngleInnerProperty, value); }
        }
        
        public static readonly DependencyProperty HourAngleInnerProperty =
            DependencyProperty.Register("HourAngleInner", typeof(double), typeof(Clock), new PropertyMetadata(0d));

        #endregion

        #region MinuteAngleInner

        public double MinuteAngleInner
        {
            get { return (double)GetValue(MinuteAngleInnerProperty); }
            private set { SetValue(MinuteAngleInnerProperty, value); }
        }
        
        public static readonly DependencyProperty MinuteAngleInnerProperty =
            DependencyProperty.Register("MinuteAngleInner", typeof(double), typeof(Clock), new PropertyMetadata(0d));

        #endregion

        #region SecondAngleInner

        public double SecondAngleInner
        {
            get { return (double)GetValue(SecondAngleInnerProperty); }
            private set { SetValue(SecondAngleInnerProperty, value); }
        }
        
        public static readonly DependencyProperty SecondAngleInnerProperty =
            DependencyProperty.Register("SecondAngleInner", typeof(double), typeof(Clock), new PropertyMetadata(0d));

        #endregion

        #endregion

        #region Constructors

        static Clock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Clock), new FrameworkPropertyMetadata(typeof(Clock)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.SetTicks();

            if(this.mSecondTimer == null)
            {
                this.mSecondTimer = new DispatcherTimer();
                this.mSecondTimer.Interval = new TimeSpan(0, 0, 0, 1); ;
                this.mSecondTimer.Tick += MSecondTimer_Tick; ;
            }

            int millisecond = DateTime.Now.Millisecond;
            System.Threading.Thread.Sleep(1000 - millisecond);
            this.mSecondTimer.Start();
            this.SetAngle();
        }
        
        #endregion

        #region private function

        private void SetTicks()
        {
            List<Tuple<object, double>> numbers = new List<Tuple<object, double>>();
            List<object> shortticks = new List<object>();
            List<object> longticks = new List<object>();

            for (int i = 1; i <= 12; i++)
            {
                double angle = -(360 / 12); //一圈360度，分12个时钟刻度
                numbers.Add(new Tuple<object, double>(i, i * angle));
                longticks.Add(new object());
            }

            for (int i = 0; i < 60; i++)
            {
                shortticks.Add(new object());
            }

            this.ShortTicks = shortticks;
            this.LongTicks = longticks;
            this.NumberList = numbers;
        }

        private void SetAngle()
        {
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            if (hour == 0)
            {
                this.HourAngleInner = 360;
            }
            else
            {
                this.HourAngleInner = hour % 12 * 30 + (double)minute / 60 * 30;
            }

            if (minute == 0)
            {
                this.MinuteAngleInner = 360;
            }
            else
            {
                this.MinuteAngleInner = minute * 6;
            }

            if (second == 0)
            {
                this.SecondAngleInner = 360;
            }
            else
            {
                this.SecondAngleInner = second * 6;
            }
            this.Hour = hour >= 10 ? hour.ToString() : "0" + hour;
            this.Minute = minute >= 10 ? minute.ToString() : "0" + minute;
            this.Second = second >= 10 ? second.ToString() : "0" + second;

        }

        #endregion

        #region Event Implement Function

        private void MSecondTimer_Tick(object sender, EventArgs e)
        {
            this.SetAngle();
        }

        #endregion
    }
}
