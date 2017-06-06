using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WPF.UI.Media
{
    public class MusicPlayer : Control
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #endregion

        #region Constructors

        static MusicPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MusicPlayer), new FrameworkPropertyMetadata(typeof(MusicPlayer)));
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
