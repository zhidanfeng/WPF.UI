using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFRichTextEditor.Extensions
{
    public class FontFormatComboBox : ComboBox
    {
        #region 依赖属性
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title"
            , typeof(string), typeof(FontFormatComboBox));
        /// <summary>
        /// TextBox前面的标签，比如用户信息
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty PopupWidthProperty = DependencyProperty.Register("PopupWidth"
            , typeof(double), typeof(FontFormatComboBox));
        /// <summary>
        /// TextBox前面的标签，比如用户信息
        /// </summary>
        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }
        #endregion

        #region 事件
        public event EventHandler<SetFontFormatEventArgs<object>> SetFontFormatHandler;
        public event EventHandler<SetFontFormatEventArgs<object>> SetDefaultHandler;
        #endregion

        #region 构造函数
        public FontFormatComboBox() : base()
        {
            //获取资源文件信息
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = new Uri("/ZEditor;component/Styles/FontComboBox.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(rd);

            this.Style = this.Resources.MergedDictionaries[0]["ComboBoxStyle"] as Style;
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var grid = Extensions.VisualHelper.FindVisualElement<Grid>(this, "grid");
            var popup = grid.FindName("Popup") as Popup;
            var grid1 = popup.FindName("DropDown") as Grid;
            var button = grid1.FindName("PART_SetDefaultButton") as Button;

            button.Click += SetDefaultButton_Click;
        }

        private void SetDefaultButton_Click(object sender, RoutedEventArgs e)
        {
            this.SetDefaultHandler(this, SetFontFormatEventArgs<object>.SetFontFormat(this.SelectedItem));
            this.IsDropDownOpen = false;
        }

        #region 重写函数
        /// <summary>
        /// 重写ComboBoxItem之后，该方法需重写
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new MyComboBoxItem();
            item.OnClickHandler += Item_OnClickHandler;
            return item;
        }
        #endregion

        #region 事件实现
        private void Item_OnClickHandler(object sender, SetFontFormatEventArgs<object> e)
        {
            MyComboBoxItem item = sender as MyComboBoxItem;
            this.SetFontFormatHandler(this, SetFontFormatEventArgs<object>.SetFontFormat(item.Content));
        }
        #endregion
    }

    /// <summary>
    /// 重写ComboBox，定义行单击事件
    /// </summary>
    public class MyComboBoxItem : System.Windows.Controls.ComboBoxItem
    {
        #region 事件

        public event EventHandler<SetFontFormatEventArgs<object>> OnClickHandler;
        #endregion

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            string text = ((System.Windows.FrameworkElement)e.OriginalSource).DataContext.ToString();
            this.OnClickHandler(this, SetFontFormatEventArgs<object>.SetFontFormat(text));
            base.OnMouseLeftButtonDown(e);
        }
    }

    /// <summary>
    /// 设置字体样式事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SetFontFormatEventArgs<T> : EventArgs
    {
        public SetFontFormatEventArgs() { }

        public T NewValue { get; private set; }

        public static SetFontFormatEventArgs<T> SetFontFormat(T newValue)
        {
            return new SetFontFormatEventArgs<T>() { NewValue = newValue };
        }
    }
}
