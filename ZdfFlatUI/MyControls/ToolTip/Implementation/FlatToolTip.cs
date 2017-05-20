using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class FlatToolTip : ToolTip
    {
        #region DependencyProperty

        public EnumPlacement PlacementEx
        {
            get { return (EnumPlacement)GetValue(PlacementExProperty); }
            set { SetValue(PlacementExProperty, value); }
        }
        
        public static readonly DependencyProperty PlacementExProperty =
            DependencyProperty.Register("PlacementEx", typeof(EnumPlacement), typeof(FlatToolTip), new PropertyMetadata(EnumPlacement.RightCenter));

        #endregion

        #region Constructors

        static FlatToolTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatToolTip), new FrameworkPropertyMetadata(typeof(FlatToolTip)));
        }

        #endregion
    }
}
