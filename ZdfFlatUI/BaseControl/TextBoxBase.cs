using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI.BaseControl
{
    public class TextBoxBase : TextBox
    {
        
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark"
            , typeof(string), typeof(TextBoxBase));
        /// <summary>
        /// 文本框内的水印提示
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }
    }
}
