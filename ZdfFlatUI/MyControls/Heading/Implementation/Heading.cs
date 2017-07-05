using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class Heading : TextBlock
    {
        #region DependencyProperty

        #region HeaderType

        public EnumHeadingType HeaderType
        {
            get { return (EnumHeadingType)GetValue(HeaderTypeProperty); }
            set { SetValue(HeaderTypeProperty, value); }
        }
        
        public static readonly DependencyProperty HeaderTypeProperty =
            DependencyProperty.Register("HeaderType", typeof(EnumHeadingType), typeof(Heading));

        #endregion

        #endregion

        #region Constructors

        static Heading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Heading), new FrameworkPropertyMetadata(typeof(Heading)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        #endregion
    }
}
