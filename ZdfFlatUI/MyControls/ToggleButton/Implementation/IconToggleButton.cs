using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class IconToggleButton : ToggleButton
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region Orientation
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(IconToggleButton), new PropertyMetadata(Orientation.Horizontal));
        #endregion

        #region CheckedIcon
        public PathGeometry CheckedIcon
        {
            get { return (PathGeometry)GetValue(CheckedIconProperty); }
            set { SetValue(CheckedIconProperty, value); }
        }
        
        public static readonly DependencyProperty CheckedIconProperty =
            DependencyProperty.Register("CheckedIcon", typeof(PathGeometry), typeof(IconToggleButton));
        #endregion

        #region UncheckedIcon
        public PathGeometry UncheckedIcon
        {
            get { return (PathGeometry)GetValue(UncheckedIconProperty); }
            set { SetValue(UncheckedIconProperty, value); }
        }

        public static readonly DependencyProperty UncheckedIconProperty =
            DependencyProperty.Register("UncheckedIcon", typeof(PathGeometry), typeof(IconToggleButton));
        #endregion

        #region CheckedContent
        public string CheckedContent
        {
            get { return (string)GetValue(CheckedContentProperty); }
            set { SetValue(CheckedContentProperty, value); }
        }

        public static readonly DependencyProperty CheckedContentProperty =
            DependencyProperty.Register("CheckedContent", typeof(string), typeof(IconToggleButton));
        #endregion

        #region UncheckedIconSize
        public double UncheckedIconSize
        {
            get { return (double)GetValue(UncheckedIconSizeProperty); }
            set { SetValue(UncheckedIconSizeProperty, value); }
        }

        public static readonly DependencyProperty UncheckedIconSizeProperty =
            DependencyProperty.Register("UncheckedIconSize", typeof(double), typeof(IconToggleButton), new PropertyMetadata(12d));
        #endregion

        #region CheckedIconSize
        public double CheckedIconSize
        {
            get { return (double)GetValue(CheckedIconSizeProperty); }
            set { SetValue(CheckedIconSizeProperty, value); }
        }

        public static readonly DependencyProperty CheckedIconSizeProperty =
            DependencyProperty.Register("CheckedIconSize", typeof(double), typeof(IconToggleButton), new PropertyMetadata(12d));
        #endregion

        #region CheckedForeground
        public Brush CheckedForeground
        {
            get { return (Brush)GetValue(CheckedForegroundProperty); }
            set { SetValue(CheckedForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.Register("CheckedForeground", typeof(Brush), typeof(IconToggleButton));
        #endregion

        #region ContentMargin
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(IconToggleButton));
        #endregion

        #endregion

        #region Constructors

        static IconToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconToggleButton), new FrameworkPropertyMetadata(typeof(IconToggleButton)));
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
