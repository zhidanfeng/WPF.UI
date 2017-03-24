using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ZTextBox : TextBox
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty CornerRadiusProperty;
        public static readonly DependencyProperty WatermarkProperty;
        public static readonly DependencyProperty MultiRowProperty;
        #endregion

        #region Constructors
        static ZTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZTextBox), new FrameworkPropertyMetadata(typeof(ZTextBox)));

            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", 
                typeof(System.Windows.CornerRadius), typeof(ZTextBox));
            WatermarkProperty = DependencyProperty.Register("Watermark", 
                typeof(string), typeof(ZTextBox));
            MultiRowProperty = DependencyProperty.Register("MultiRow",
                typeof(bool), typeof(ZTextBox));
        }
        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 边框圆角
        /// </summary>
        public System.Windows.CornerRadius CornerRadius
        {
            get { return (System.Windows.CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 文本输入框的水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        /// <summary>
        /// 多行
        /// </summary>
        public bool MultiRow
        {
            get { return (bool)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
