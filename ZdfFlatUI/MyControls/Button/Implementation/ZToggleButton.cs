using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    public class ZToggleButton : ToggleButton
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
            , typeof(System.Windows.CornerRadius), typeof(ZToggleButton));
        /// <summary>
        /// 边框圆角
        /// </summary>
        public System.Windows.CornerRadius CornerRadius
        {
            get { return (System.Windows.CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region Constructors
        static ZToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZToggleButton), new FrameworkPropertyMetadata(typeof(ZToggleButton)));
        }
        #endregion

        #region 依赖属性set get

        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
