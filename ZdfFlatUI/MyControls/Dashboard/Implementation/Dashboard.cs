using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ZdfFlatUI
{
    /// <summary>
    /// 刻度盘控件
    /// </summary>
    /// <remarks>add by zhiddanfeng 2017.2.19</remarks>
    [TemplatePart(Name = "PART_IncreaseCircle", Type = typeof(Arc))]
    [TemplatePart(Name = "PART_LabelPanel", Type = typeof(Panel))]
    public class Dashboard : Control
    {
        private Arc PART_IncreaseCircle;
        private Panel PART_LabelPanel;
        private double OldAngle;

        #region Constructors
        static Dashboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dashboard), new FrameworkPropertyMetadata(typeof(Dashboard)));
        }
        #endregion

        #region 依赖属性

        #region Angle 刻度盘当前值所对应的角度
        /// <summary>
        /// 刻度盘当前值所对应的角度依赖属性
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(
                "Angle",
                typeof(double),
                typeof(Dashboard),
                new PropertyMetadata(0d));

        /// <summary>
        /// 刻度盘当前值所对应的角度
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            private set { SetValue(AngleProperty, value); }
        }
        #endregion

        #region Angle 刻度盘起始角度
        /// <summary>
        /// 刻度盘起始角度依赖属性
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(
                "StartAngle",
                typeof(double),
                typeof(Dashboard),
                new PropertyMetadata(0d));

        /// <summary>
        /// 刻度盘起始角度
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }
        #endregion

        #region Angle 刻度盘结束角度依赖属性
        /// <summary>
        /// 刻度盘结束角度依赖属性
        /// </summary>
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register(
                "EndAngle",
                typeof(double),
                typeof(Dashboard),
                new PropertyMetadata(0d));

        /// <summary>
        /// 刻度盘结束角度依赖属性
        /// </summary>
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }
        #endregion

        #region Minimum 最小值
        /// <summary>
        /// 最小值依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum",
                typeof(double),
                typeof(Dashboard),
                new PropertyMetadata(0.0));

        /// <summary>
        /// 获取或设置最小值.
        /// </summary>
        /// <value>最小值.</value>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        #endregion

        #region Maximum 最大值
        /// <summary>
        /// 最大值依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",
                typeof(double),
                typeof(Dashboard),
                new PropertyMetadata(100.0));

        /// <summary>
        /// 获取或设置最大值.
        /// </summary>
        /// <value>最大值.</value>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        #endregion

        #region Value 当前值
        /// <summary>
        /// 最大值依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(Dashboard),
                new PropertyMetadata(0.0, new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        /// 获取或设置当前值
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Dashboard dashboard = d as Dashboard;
            dashboard.OldAngle = dashboard.Angle;
            dashboard.SetAngle();
            dashboard.TransformAngle();
        }
        #endregion

        #region ShortTicks 短刻度线
        /// <summary>
        /// 短刻度线依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty ShortTicksProperty =
            DependencyProperty.Register(
                "ShortTicks",
                typeof(IList<object>),
                typeof(Dashboard),
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

        #region LongTicks 长刻度线
        /// <summary>
        /// 长刻度线依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty LongTicksProperty =
            DependencyProperty.Register(
                "LongTicks",
                typeof(IList<object>),
                typeof(Dashboard),
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

        #region LongTicks 长刻度线上显示的数字
        /// <summary>
        /// 长刻度线依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty NumberListProperty =
            DependencyProperty.Register(
                "NumberList",
                typeof(IList<object>),
                typeof(Dashboard),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置长刻度线，用于绑定PathListBox的ItemsSource
        /// </summary>
        /// <value>长刻度线.</value>
        public IList<object> NumberList
        {
            get { return (IList<object>)GetValue(NumberListProperty); }
            private set { SetValue(NumberListProperty, value); }
        }
        #endregion

        #region LongTickCount 长刻度个数
        public static readonly DependencyProperty LongTickCountProperty =
            DependencyProperty.Register(
                "LongTickCount",
                typeof(int),
                typeof(Dashboard),
                new PropertyMetadata(5));

        /// <summary>
        /// 获取或设置长刻度个数，用于设置刻度盘显示几个长刻度
        /// </summary>
        public int LongTickCount
        {
            get { return (int)GetValue(LongTickCountProperty); }
            set { SetValue(LongTickCountProperty, value); }
        }
        #endregion

        #region ShortTickCount 短刻度个数
        public static readonly DependencyProperty ShortTickCountProperty =
            DependencyProperty.Register(
                "ShortTickCount",
                typeof(int),
                typeof(Dashboard),
                new PropertyMetadata(3));

        /// <summary>
        /// 获取或设置两个长刻度之间的短刻度的个数
        /// </summary>
        public int ShortTickCount
        {
            get { return (int)GetValue(ShortTickCountProperty); }
            set { SetValue(ShortTickCountProperty, value); }
        }
        #endregion

        #region LabelStyle 文本显示样式
        public static readonly DependencyProperty LabelStyleProperty = DependencyProperty.Register("LabelStyle"
            , typeof(UIElement)
            , typeof(Dashboard),
            new PropertyMetadata(new PropertyChangedCallback(OnLabelPanelPropertyChanged)));

        /// <summary>
        /// 文本显示样式
        /// </summary>
        public UIElement LabelStyle
        {
            get { return (UIElement)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        private static void OnLabelPanelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Dashboard dashboard = d as Dashboard;
            if (dashboard.PART_LabelPanel != null)
            {
                dashboard.PART_LabelPanel.Children.Clear();
                dashboard.PART_LabelPanel.Children.Add(dashboard.LabelStyle);
            }
        }
        #endregion

        #region TickDurtion 刻度改变时的动画显示时长
        public static readonly DependencyProperty TickDurtionProperty = DependencyProperty.Register("TickDurtion"
            , typeof(Duration)
            , typeof(Dashboard),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(400))));

        /// <summary>
        /// 刻度改变时的动画显示时长
        /// </summary>
        public Duration TickDurtion
        {
            get { return (Duration)GetValue(TickDurtionProperty); }
            set { SetValue(TickDurtionProperty, value); }
        }
        #endregion

        #region Skin 刻度盘样式
        public static readonly DependencyProperty SkinProperty = DependencyProperty.Register("Skin",
            typeof(DashboardSkinEnum),
            typeof(Dashboard),
            new PropertyMetadata(DashboardSkinEnum.Speed));

        /// <summary>
        /// 刻度盘样式
        /// </summary>
        public DashboardSkinEnum Skin
        {
            get { return (DashboardSkinEnum)GetValue(SkinProperty); }
            set { SetValue(SkinProperty, value); }
        }
        #endregion

        #region LabelStyle 文本显示样式
        public static readonly DependencyProperty ShortTicksBrushProperty = DependencyProperty.Register("ShortTicksBrush"
            , typeof(Brush)
            , typeof(Dashboard));

        /// <summary>
        /// 短刻度颜色
        /// </summary>
        public UIElement ShortTicksBrush
        {
            get { return (UIElement)GetValue(ShortTicksBrushProperty); }
            set { SetValue(ShortTicksBrushProperty, value); }
        }

        public static readonly DependencyProperty LongTicksBrushProperty = DependencyProperty.Register("LongTicksBrush"
            , typeof(Brush)
            , typeof(Dashboard));

        /// <summary>
        /// 长刻度颜色
        /// </summary>
        public UIElement LongTicksBrush
        {
            get { return (UIElement)GetValue(LongTicksBrushProperty); }
            set { SetValue(LongTicksBrushProperty, value); }
        }
        #endregion

        #endregion

        #region 重载
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_IncreaseCircle = GetTemplateChild("PART_IncreaseCircle") as Arc;
            this.PART_LabelPanel = GetTemplateChild("PART_LabelPanel") as Panel;

            this.SetTicks();
            this.SetAngle();
            this.TransformAngle();

            if (this.PART_LabelPanel != null)
            {
                this.PART_LabelPanel.Children.Clear();
                this.PART_LabelPanel.Children.Add(this.LabelStyle);
            }
        }
        #endregion

        #region Private方法
        /// <summary>
        /// 设置刻度线
        /// </summary>
        private void SetTicks()
        {
            List<object> numbers = new List<object>();
            List<object> shortticks = new List<object>();
            List<object> longticks = new List<object>();

            for (int i = 0; i < this.LongTickCount; i++)
            {
                numbers.Add(Math.Round(this.Minimum + (this.Maximum - this.Minimum) / (this.LongTickCount - 1) * (i)));
                longticks.Add(new object());
            }

            for (int i = 0; i < (this.LongTickCount - 1) * (this.ShortTickCount + 1) + 1; i++)
            {
                shortticks.Add(new object());
            }

            this.ShortTicks = shortticks;
            this.LongTicks = longticks;
            this.NumberList = numbers;
        }

        /// <summary>
        /// 根据当前值设置圆弧的EndAngle
        /// </summary>
        private void SetAngle()
        {
            if(this.Value < this.Minimum)
            {
                this.Angle = this.StartAngle;
                return;
            }

            if(this.Value > this.Maximum)
            {
                this.Angle = this.EndAngle;
                return;
            }

            var diff = this.Maximum - this.Minimum;
            var valueDiff = this.Value - this.Minimum;
            this.Angle = this.StartAngle + (this.EndAngle - this.StartAngle) / diff * valueDiff;
        }

        /// <summary>
        /// 角度值变化动画
        /// </summary>
        private void TransformAngle()
        {
            if (this.PART_IncreaseCircle != null)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation(this.OldAngle, this.Angle, this.TickDurtion);
                this.PART_IncreaseCircle.BeginAnimation(Arc.EndAngleProperty, doubleAnimation);
            }
        }
        #endregion
    }
}
