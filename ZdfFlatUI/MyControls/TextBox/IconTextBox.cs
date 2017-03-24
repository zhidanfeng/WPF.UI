using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    /// <summary>
    /// 带图标的文本输入框
    /// </summary>
    public class IconTextBox : TextBox
    {
        public enum IconPlacementEnum
        {
            Left,
            Right,
        }

        static IconTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconTextBox), new FrameworkPropertyMetadata(typeof(IconTextBox)));
        }

        #region 事件定义

        public static readonly RoutedEvent EnterKeyClickEvent = EventManager.RegisterRoutedEvent("EnterKeyClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(IconTextBox));

        public event RoutedPropertyChangedEventHandler<object> EnterKeyClick
        {
            add
            {
                this.AddHandler(EnterKeyClickEvent, value);
            }
            remove
            {
                this.RemoveHandler(EnterKeyClickEvent, value);
            }
        }

        protected virtual void OnEnterKeyClick(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg =
                new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, EnterKeyClickEvent);
            this.RaiseEvent(arg);
        }
        #endregion

        #region 依赖属性

        #region Path相关属性

        public static readonly DependencyProperty IconPlacementProperty = DependencyProperty.Register("IconPlacement"
            , typeof(IconPlacementEnum), typeof(IconTextBox));
        /// <summary>
        /// 文本输入框的图标显示位置
        /// </summary>
        public IconPlacementEnum IconPlacement
        {
            get { return (IconPlacementEnum)GetValue(IconPlacementProperty); }
            set { SetValue(IconPlacementProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth"
            , typeof(double), typeof(IconTextBox), new FrameworkPropertyMetadata(13d));
        /// <summary>
        /// 图标的宽度
        /// </summary>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData"
            , typeof(PathGeometry), typeof(IconTextBox));
        /// <summary>
        /// 图标资源，这里使用的Path作为图标
        /// </summary>
        public PathGeometry PathData
        {
            get { return (PathGeometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register("IconColor"
            , typeof(Brush), typeof(IconTextBox));
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
            , typeof(System.Windows.CornerRadius), typeof(IconTextBox));
        /// <summary>
        /// 边框圆角
        /// </summary>
        public System.Windows.CornerRadius CornerRadius
        {
            get { return (System.Windows.CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark"
            , typeof(string), typeof(IconTextBox));
        /// <summary>
        /// 文本输入框的水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty IsShowIconProperty = DependencyProperty.Register("IsShowIcon"
            , typeof(bool), typeof(IconTextBox), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示图标
        /// </summary>
        public bool IsShowIcon
        {
            get { return (bool)GetValue(IsShowIconProperty); }
            set { SetValue(IsShowIconProperty, value); }
        }
        #endregion

        public IconTextBox() : base()
        {
            this.KeyUp += IconTextBox_KeyUp;
        }

        private void IconTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                this.OnEnterKeyClick(null, null);
            }
        }
    }
}
