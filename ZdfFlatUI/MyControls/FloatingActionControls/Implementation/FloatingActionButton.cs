using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    public class FloatingActionButton : ContentControl
    {
        #region private fields

        #endregion

        #region Property
        private FloatingActionMenu ParentItemsControl
        {
            get { return this.ParentSelector as FloatingActionMenu; }
        }

        internal ItemsControl ParentSelector
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as ItemsControl; }
        }
        #endregion

        #region DependencyProperty

        #region TipContent

        public string TipContent
        {
            get { return (string)GetValue(TipContentProperty); }
            set { SetValue(TipContentProperty, value); }
        }
        
        public static readonly DependencyProperty TipContentProperty =
            DependencyProperty.Register("TipContent", typeof(string), typeof(FloatingActionButton), new PropertyMetadata(string.Empty));

        #endregion

        #endregion

        #region Constructors

        static FloatingActionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingActionButton), new FrameworkPropertyMetadata(typeof(FloatingActionButton)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseLeftButtonDown += FloatingActionButton_MouseLeftButtonDown;
        }

        private void FloatingActionButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(this.ParentItemsControl != null)
            {
                this.ParentItemsControl.OnItemClick(this.Content, this.Content);
                this.ParentItemsControl.IsDropDownOpen = false;
            }
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        #endregion
    }
}
