using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    public class ColorItem : ContentControl
    {
        #region private fields

        #endregion

        #region Property
        private ColorSelector ParentColorSelector
        {
            get { return this.ParentSelector as ColorSelector; }
        }

        internal Selector ParentSelector
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as Selector; }
        }
        #endregion

        #region DependencyProperty

        #region IsSelected

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        
        public static readonly DependencyProperty IsSelectedProperty =
            Selector.IsSelectedProperty.AddOwner(typeof(ColorItem), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, new PropertyChangedCallback(ColorItem.OnIsSelectedChanged)));

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorItem colorItem = d as ColorItem;
            bool flag = (bool)e.NewValue;
            if (flag)
            {
                colorItem.OnSelected(new RoutedEventArgs(Selector.SelectedEvent, colorItem));
            }
            else
            {
                colorItem.OnUnselected(new RoutedEventArgs(Selector.UnselectedEvent, colorItem));
            }
            colorItem.UpdateVisualState(true);
        }

        #endregion

        #region Color

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(ColorItem));

        #endregion

        #endregion

        #region Constructors

        static ColorItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorItem), new FrameworkPropertyMetadata(typeof(ColorItem)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseLeftButtonUp += ColorItem_MouseLeftButtonUp;
        }
        

        #endregion

        #region private function

        private void UpdateVisualState(bool useTransitions)
        {
            if (!base.IsEnabled)
            {
                VisualStateManager.GoToState(this, (base.Content is Control) ? "Normal" : "Disabled", useTransitions);
            }
            else if (base.IsMouseOver)
            {
                VisualStateManager.GoToState(this, "MouseOver", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            if (this.IsSelected)
            {
                if (Selector.GetIsSelectionActive(this))
                {
                    VisualStateManager.GoToState(this, "Selected", useTransitions);
                }
                else
                {
                    //VisualStates.GoToState(this, useTransitions, new string[]
                    //{
                    //    "SelectedUnfocused",
                    //    "Selected"
                    //});
                }
            }
            else
            {
                VisualStateManager.GoToState(this, "Unselected", useTransitions);
            }
            if (base.IsKeyboardFocused)
            {
                VisualStateManager.GoToState(this, "Focused", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unfocused", useTransitions);
            }
        }

        private void OnUnselected(RoutedEventArgs routedEventArgs)
        {
            this.HandleIsSelectedChanged(false, routedEventArgs);
        }

        private void OnSelected(RoutedEventArgs routedEventArgs)
        {
            this.HandleIsSelectedChanged(true, routedEventArgs);
        }

        private void HandleIsSelectedChanged(bool newValue, RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        #endregion

        #region Event Implement Function

        private void ColorItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ParentColorSelector.SetItemSelected(this);
        }

        #endregion
    }
}
