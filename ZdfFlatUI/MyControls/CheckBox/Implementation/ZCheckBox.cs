using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class ZCheckBox : CheckBox
    {
        static ZCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZCheckBox), new FrameworkPropertyMetadata(typeof(ZCheckBox)));
        }

        #region 依赖属性

        public static readonly DependencyProperty UnCheckedColorProperty = DependencyProperty.Register("UnCheckedColor"
            , typeof(Brush), typeof(ZCheckBox));
        /// <summary>
        /// CheckBox未选中时的颜色
        /// </summary>
        public Brush UnCheckedColor
        {
            get { return (Brush)GetValue(UnCheckedColorProperty); }
            set { SetValue(UnCheckedColorProperty, value); }
        }

        public static readonly DependencyProperty CheckedColorProperty = DependencyProperty.Register("CheckedColor"
            , typeof(Brush), typeof(ZCheckBox));
        /// <summary>
        /// CheckBox选中后的颜色
        /// </summary>
        public Brush CheckedColor
        {
            get { return (Brush)GetValue(CheckedColorProperty); }
            set { SetValue(CheckedColorProperty, value); }
        }
        #endregion
    }
}
