using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ContentNavigation : ContentControl
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #endregion

        #region Constructors

        static ContentNavigation()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentNavigation), new FrameworkPropertyMetadata(typeof(ContentNavigation)));
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
