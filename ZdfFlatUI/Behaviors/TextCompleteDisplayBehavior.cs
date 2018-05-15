using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ZdfFlatUI.Behaviors
{
    /// <summary>
    /// 双击显示全部文本内容
    /// </summary>
    public class TextCompleteDisplayBehavior : Behavior<TextBlock>
    {
        private Popup popup;
        private TextBox textBox;

        #region Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextCompleteDisplayBehavior), new PropertyMetadata(string.Empty));
        #endregion

        #region ShowWidth
        public double ShowWidth
        {
            get { return (double)GetValue(ShowWidthProperty); }
            set { SetValue(ShowWidthProperty, value); }
        }

        public static readonly DependencyProperty ShowWidthProperty =
            DependencyProperty.Register("ShowWidth", typeof(double), typeof(TextCompleteDisplayBehavior), new PropertyMetadata(0d));
        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObject_PreviewMouseLeftButtonDown;
        }

        /// <summary>
        /// 双击文本时，用Popup显示完整的文本内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.OpenTextShowHost();
            }
        }

        private void OpenTextShowHost()
        {
            try
            {
                if (popup == null)
                {
                    popup = new Popup
                    {
                        PlacementTarget = AssociatedObject,
                        Placement = PlacementMode.Bottom,
                        AllowsTransparency = true,
                        StaysOpen = false,
                        VerticalOffset = -AssociatedObject.ActualHeight,
                    };

                    Grid root = new Grid();
                    root.Margin = new Thickness(5);
                    Border shadow = new Border()
                    {
                        Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                        Effect = new DropShadowEffect()
                        {
                            BlurRadius = 5,
                            Opacity = 0.2,
                            ShadowDepth = 0,
                            Color = Color.FromRgb(64, 64, 64),
                        },
                    };
                    root.Children.Add(shadow);

                    Border border = new Border
                    {
                        Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(204, 206, 219)),
                        Padding = new Thickness(3),
                        Width = (this.ShowWidth == 0 || this.ShowWidth == double.NaN) ? AssociatedObject.ActualWidth : this.ShowWidth,
                        SnapsToDevicePixels = true,
                        UseLayoutRounding = true,
                    };

                    textBox = new TextBox
                    {
                        TextWrapping = TextWrapping.Wrap,
                        BorderThickness = new Thickness(0),
                        IsReadOnly = true
                    };

                    border.Child = textBox;

                    root.Children.Add(border);

                    popup.Child = root;
                }

                this.textBox.Text = this.Text;
                this.popup.IsOpen = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObject_PreviewMouseLeftButtonDown;
        }
    }
}