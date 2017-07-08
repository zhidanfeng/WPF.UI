using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI.MyControls.Primitives
{
    public class ZTextBoxBase : TextBox
    {
        #region Watermark

        /// <summary>
        /// 获取或者设置水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }
        
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(ZTextBoxBase));

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ZTextBoxBase), new PropertyMetadata(CornerRadiusChanged));

        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZTextBoxBase textbox = d as ZTextBoxBase;
            if(textbox != null && e.NewValue != null)
            {
                textbox.OnCornerRadiusChanged((CornerRadius)e.NewValue);
            }
        }

        #endregion

        public virtual void OnCornerRadiusChanged(CornerRadius newValue)
        {

        }
    }
}
