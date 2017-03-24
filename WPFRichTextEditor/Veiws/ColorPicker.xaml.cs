using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFRichTextEditor.Veiws
{
    /// <summary>
    /// ColorPicker1.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        private bool IsRaiseColorChangedEvent = true;

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor"
            , typeof(Color), typeof(ColorPicker)
            , new UIPropertyMetadata(Colors.Transparent, new PropertyChangedCallback(ColorPicker.OnSelectedColorPropertyChanged)));

        public Color SelectedColor
        {
            get
            {
                return (Color)base.GetValue(ColorPicker.SelectedColorProperty);
            }
            set
            {
                base.SetValue(ColorPicker.SelectedColorProperty, value);
            }
        }

        private static void OnSelectedColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }

        public void Reset()
        {
            this.IsRaiseColorChangedEvent = false;
            this.ColorPicker1.SelectedItem = null;
            this.SelectedColor = Color.FromRgb(1, 1, 1);
            this.IsRaiseColorChangedEvent = true;
        }

        public ColorPicker()
        {
            this.InitializeComponent();
            this.InitColors();
        }

        private void HandleSelect(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            bool flag = listBoxItem != null;
            if (flag)
            {
                this.SelectedColor = (Color)listBoxItem.Content;
                this.SetColorHandler(this, SetForegroundEventArgs<Color>.SetColor(this.SelectedColor));
            }
        }

        public event EventHandler<PropertyChangedEventArgs<Color>> SelectedColorChanged;
        public Action<object, SetForegroundEventArgs<Color>> SetColorHandler { get; set; }

        private void InitColors()
        {
            List<Color> list = new List<Color>
            {
                Color.FromRgb(0, 0, 0),
                Color.FromRgb(153, 51, 0),
                Color.FromRgb(51, 51, 0),
                Color.FromRgb(0, 51, 0),
                Color.FromRgb(0, 51, 102),
                Color.FromRgb(0, 0, 128),
                Color.FromRgb(51, 51, 153),
                Color.FromRgb(51, 51, 51),
                Color.FromRgb(128, 0, 0),
                Color.FromRgb(255, 102, 0),
                Color.FromRgb(128, 128, 0),
                Color.FromRgb(0, 128, 0),
                Color.FromRgb(0, 128, 128),
                Color.FromRgb(0, 0, 255),
                Color.FromRgb(102, 102, 153),
                Color.FromRgb(128, 128, 128),
                Color.FromRgb(255, 0, 0),
                Color.FromRgb(255, 153, 0),
                Color.FromRgb(153, 204, 0),
                Color.FromRgb(51, 153, 102),
                Color.FromRgb(51, 204, 204),
                Color.FromRgb(51, 102, 255),
                Color.FromRgb(128, 0, 128),
                Color.FromRgb(153, 153, 153),
                Color.FromRgb(255, 0, 255),
                Color.FromRgb(255, 204, 0),
                Color.FromRgb(255, 255, 0),
                Color.FromRgb(0, 255, 0),
                Color.FromRgb(0, 255, 255),
                Color.FromRgb(0, 204, 255),
                Color.FromRgb(153, 51, 102),
                Color.FromRgb(192, 192, 192),
                Color.FromRgb(255, 153, 204),
                Color.FromRgb(255, 204, 153),
                Color.FromRgb(255, 255, 153),
                Color.FromRgb(0, 255, 0),
                Color.FromRgb(204, 255, 204),
                Color.FromRgb(153, 204, 255),
                Color.FromRgb(204, 153, 255),
                Color.FromRgb(255, 255, 255)
            };
            this.ColorPicker1.ItemsSource = new ReadOnlyCollection<Color>(list);
        }
    }

    public class PropertyChangedEventArgs<T> : EventArgs
    {
        private PropertyChangedEventArgs() { }

        public T NewValue { get; private set; }
        public T OldValue { get; private set; }

        public static PropertyChangedEventArgs<T> Create(T newValue, T oldValue)
        {
            return new PropertyChangedEventArgs<T>() { NewValue = newValue, OldValue = oldValue };
        }
    }

    public class SetForegroundEventArgs<T> : EventArgs
    {
        public SetForegroundEventArgs() { }

        public T NewValue { get; private set; }

        public static SetForegroundEventArgs<T> SetColor(T newValue)
        {
            return new SetForegroundEventArgs<T>() { NewValue = newValue };
        }
    }
}
