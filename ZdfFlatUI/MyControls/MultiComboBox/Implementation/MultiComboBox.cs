using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    public class MultiComboBox : Selector
    {
        #region Constructors
        static MultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }

        public MultiComboBox()
        {
            
        }
        #endregion

        #region 依赖属性
        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen"
            , typeof(bool), typeof(MultiComboBox));
        /// <summary>
        /// 
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        #endregion
    }
}
