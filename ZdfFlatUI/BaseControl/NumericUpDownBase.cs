using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ZdfFlatUI.BaseControl
{
    public class NumericUpDownBase : TextBoxBase
    {
        static NumericUpDownBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownBase), new FrameworkPropertyMetadata(typeof(NumericUpDownBase)));
        }

        public enum UpDownOrientationEnum
        {
            Vertical,
            Horizontal,
        }

        public static readonly DependencyProperty UpDownOrientationProperty = DependencyProperty.Register("UpDownOrientation"
            , typeof(UpDownOrientationEnum), typeof(TextBoxBase));
        /// <summary>
        /// 皮肤
        /// </summary>
        public UpDownOrientationEnum UpDownOrientation
        {
            get { return (UpDownOrientationEnum)GetValue(UpDownOrientationProperty); }
            set { SetValue(UpDownOrientationProperty, value); }
        }
    }
}
