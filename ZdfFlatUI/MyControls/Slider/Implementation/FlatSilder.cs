using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class FlatSilder : Slider
    {
        #region Private属性

        #endregion

        #region 依赖属性

        #region DecreaseColor

        public Brush DecreaseColor
        {
            get { return (Brush)GetValue(DecreaseColorProperty); }
            set { SetValue(DecreaseColorProperty, value); }
        }
        
        public static readonly DependencyProperty DecreaseColorProperty =
            DependencyProperty.Register("DecreaseColor", typeof(Brush), typeof(FlatSilder));

        #endregion

        #region IncreaseColor

        public Brush IncreaseColor
        {
            get { return (Brush)GetValue(IncreaseColorProperty); }
            set { SetValue(IncreaseColorProperty, value); }
        }
        
        public static readonly DependencyProperty IncreaseColorProperty =
            DependencyProperty.Register("IncreaseColor", typeof(Brush), typeof(FlatSilder));

        #endregion

        #endregion

        #region Constructors
        static FlatSilder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatSilder), new FrameworkPropertyMetadata(typeof(FlatSilder)));
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
