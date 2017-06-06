using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    public class SwitchMenu : Selector
    {
        private Button PART_PreviousButton;
        private Button PART_NextButton;
        private Button PART_UpButton;
        private Button PART_DownButton;
        private ScrollViewer PART_ScrollViewer;
        private double offset = 70;
        #region 依赖属性
        #region Orientation
        [Bindable(true), Category("Appearance"), Description("aaaa")]
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(SwitchMenu), new PropertyMetadata(Orientation.Horizontal));
        #endregion
        #endregion
        #region 事件

        #endregion
        static SwitchMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchMenu), new FrameworkPropertyMetadata(typeof(SwitchMenu)));
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            ContentControl item = new ContentControl();
            item.MouseLeftButtonUp += item_MouseLeftButtonUp;
            //item.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Item_Click));
            //item.AddHandler(Button.ClickEvent, new RoutedEventHandler(Item_Click1));
            return item;
        }

        void item_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_PreviousButton = this.GetTemplateChild("PART_PreviousButton") as Button;
            this.PART_NextButton = this.GetTemplateChild("PART_NextButton") as Button;
            this.PART_UpButton = this.GetTemplateChild("PART_UpButton") as Button;
            this.PART_DownButton = this.GetTemplateChild("PART_DownButton") as Button;
            this.PART_ScrollViewer = this.GetTemplateChild("PART_ScrollViewer") as ScrollViewer;
            if (this.PART_PreviousButton != null)
            {
                this.PART_PreviousButton.Click += PART_PreviousButton_Click;
            }
            if (this.PART_NextButton != null)
            {
                this.PART_NextButton.Click += PART_NextButton_Click;
            }
            if (this.PART_UpButton != null)
            {
                this.PART_UpButton.Click += PART_UpButton_Click;
            }
            if (this.PART_DownButton != null)
            {
                this.PART_DownButton.Click += PART_DownButton_Click;
            }
            if (this.PART_ScrollViewer != null)
            {
                this.PART_ScrollViewer.ScrollChanged += PART_ScrollViewer_ScrollChanged;
            }
        }
        private void PART_UpButton_Click(object sender, RoutedEventArgs e)
        {
            this.ScrollToOffset(Orientation.Vertical, -this.offset);
        }
        private void PART_DownButton_Click(object sender, RoutedEventArgs e)
        {
            this.ScrollToOffset(Orientation.Vertical, this.offset);
        }
        void PART_ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (this.PART_ScrollViewer != null)
            {
                this.PART_PreviousButton.Visibility = (this.PART_ScrollViewer.HorizontalOffset == 0.0) ? Visibility.Hidden : Visibility.Visible;
                this.PART_NextButton.Visibility = (this.PART_ScrollViewer.ScrollableWidth == this.PART_ScrollViewer.HorizontalOffset) ? Visibility.Hidden : Visibility.Visible;
                this.PART_UpButton.Visibility = (this.PART_ScrollViewer.VerticalOffset == 0.0) ? Visibility.Hidden : Visibility.Visible;
                this.PART_DownButton.Visibility = (this.PART_ScrollViewer.ScrollableHeight == this.PART_ScrollViewer.VerticalOffset) ? Visibility.Hidden : Visibility.Visible;
            }
        }
        void PART_PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            this.ScrollToOffset(Orientation.Horizontal, -this.offset);
        }
        void PART_NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.ScrollToOffset(Orientation.Horizontal, this.offset);
        }
        void ScrollToOffset(Orientation orientation, double scrollOffset)
        {
            if (this.PART_ScrollViewer == null)
            {
                return;
            }
            switch (orientation)
            {
                case Orientation.Horizontal:
                    this.PART_ScrollViewer.ScrollToHorizontalOffset(this.PART_ScrollViewer.HorizontalOffset + scrollOffset);
                    break;
                case Orientation.Vertical:
                    this.PART_ScrollViewer.ScrollToVerticalOffset(this.PART_ScrollViewer.VerticalOffset + scrollOffset);
                    break;
                default:
                    break;
            }
        }
    }
}
