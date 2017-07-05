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

        private Arc PART_SecondCircle;
        private DispatcherTimer mSecondTimer;
        private double OldAngle;

        #endregion

        #region DependencyProperty

        #region Hour

        public int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(int), typeof(Clock), new PropertyMetadata(0));

        #endregion

        #region Minute

        public int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(int), typeof(Clock), new PropertyMetadata(0));

        #endregion

        #region Second

        public int Second
        {
            get { return (int)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Second.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(int), typeof(Clock), new PropertyMetadata(0));

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

            this.PART_SecondCircle = this.GetTemplateChild("PART_SecondCircle") as Arc;

            this.SetTicks();

            if(this.mSecondTimer == null)
            {
                this.mSecondTimer = new DispatcherTimer();
                this.mSecondTimer.Interval = new TimeSpan(0, 0, 0, 1); ;
                this.mSecondTimer.Tick += MSecondTimer_Tick; ;
            }

            this.Loaded += Clock_Loaded;  
        }

        private void MSecondTimer_Tick(object sender, EventArgs e)
        {
            int second = DateTime.Now.Second;
            if (second == 0)
            {
                this.PART_SecondCircle.EndAngle = 360;
            }
            else
            {
                this.PART_SecondCircle.EndAngle = second * 6;
            }

            this.Hour = DateTime.Now.Hour;
            this.Minute = DateTime.Now.Minute;
            this.Second = DateTime.Now.Second;
        }

        private void Clock_Loaded(object sender, RoutedEventArgs e)
        {
            this.mSecondTimer.Start();
            if (this.PART_SecondCircle != null)
            {
                int second = DateTime.Now.Second;
                if(second == 0)
                {
                    this.PART_SecondCircle.EndAngle = 360;
                }
                else
                {
                    this.PART_SecondCircle.EndAngle = second * 6;
                }
                this.Hour = DateTime.Now.Hour;
                this.Minute = DateTime.Now.Minute;
                this.Second = DateTime.Now.Second;
            }
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

        private void TransformAngle(double oldAngle, double newAngle)
        {
            if (this.PART_SecondCircle != null)
            {
                Duration TickDurtion = new Duration(new TimeSpan(800));
                DoubleAnimation doubleAnimation = new DoubleAnimation(oldAngle, newAngle, TickDurtion);
                this.PART_SecondCircle.BeginAnimation(Arc.EndAngleProperty, doubleAnimation);
            }
        }

        #endregion

        #region Event Implement Function

        #endregion
    }
}
