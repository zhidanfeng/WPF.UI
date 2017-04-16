using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class Icon : Control
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get
        public EnumIconType Type
        {
            get { return (EnumIconType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(EnumIconType), typeof(Icon));

        public PathFigureCollection Data
        {
            get { return (PathFigureCollection)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(PathFigureCollection), typeof(Icon));



        #endregion

        #region Constructors
        static Icon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
