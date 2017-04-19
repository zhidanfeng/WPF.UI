using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    sealed class RatingBarButton : ButtonBase
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        #region IsSelected 是否选中
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(RatingBarButton), new PropertyMetadata(false));
        #endregion

        #region IsHalf 是否显示半颗星
        public bool IsHalf
        {
            get { return (bool)GetValue(IsHalfProperty); }
            set { SetValue(IsHalfProperty, value); }
        }
        
        public static readonly DependencyProperty IsHalfProperty =
            DependencyProperty.Register("IsHalf", typeof(bool), typeof(RatingBarButton), new PropertyMetadata(false));
        #endregion

        #region Value 当前值
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(RatingBarButton));
        #endregion

        #endregion

        #region Constructors
        static RatingBarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingBarButton), new FrameworkPropertyMetadata(typeof(RatingBarButton)));
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
