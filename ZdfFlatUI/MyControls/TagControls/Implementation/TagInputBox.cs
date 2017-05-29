using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class TagInputBox : Control
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region ItemsSource

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TagInputBox), new PropertyMetadata(null));

        #endregion

        #endregion

        #region private DependencyProperty

        #region ItemsSourceInternal

        public IEnumerable ItemsSourceInternal
        {
            get { return (IEnumerable)GetValue(ItemsSourceInternalProperty); }
            set { SetValue(ItemsSourceInternalProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsSourceInternalProperty =
            DependencyProperty.Register("ItemsSourceInternal", typeof(IEnumerable), typeof(TagInputBox), new PropertyMetadata(null));

        #endregion

        #endregion

        #region Constructors

        static TagInputBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagInputBox), new FrameworkPropertyMetadata(typeof(TagInputBox)));
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
