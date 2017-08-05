using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ZdfFlatUI.MyControls.Primitives;

namespace ZdfFlatUI
{
    /// <summary>
    /// 带图标的文本输入框
    /// </summary>
    public class IconTextBox : IconTextBoxBase
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
