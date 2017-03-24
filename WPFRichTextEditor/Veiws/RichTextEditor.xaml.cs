using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.XPath;
using WPFRichTextEditor.Extensions;

namespace WPFRichTextEditor.Veiws
{
    /// <summary>
    /// RichTextEditor.xaml 的交互逻辑
    /// </summary>
    public partial class RichTextEditor : UserControl
    {
        #region 私有属性
        private static readonly string ConfigPath = "RichTextEditor.config.xml";
        private static readonly string VisualFontFamiliesPath = @"/RichTextEditor/VisualMode/FontFamilies/add/@value";
        private static readonly string VisualFontSizePath = @"/RichTextEditor/VisualMode/FontSizes/add/@value";
        private static readonly string VisualDefaultFontSizePath = @"/RichTextEditor/VisualMode/Default/FontSize/@value";
        private static readonly string VisualDefaultFontFamilyPath = @"/RichTextEditor/VisualMode/Default/FontFamily/@value";
        private static readonly string VisualEnabledFuncAttchmentPath = @"/RichTextEditor/VisualMode/EnabledFunc/Attachment/@enable";
        private static readonly string VisualEnabledFuncImagePath = @"/RichTextEditor/VisualMode/EnabledFunc/Image/@enable";
        private static readonly string VisualEnabledFuncHyperlinkPath = @"/RichTextEditor/VisualMode/EnabledFunc/Hyperlink/@enable";
        #endregion

        #region 依赖属性
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(RichTextEditor),
                new FrameworkPropertyMetadata(string.Empty));
        /// <summary>
        /// 纯文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty HtmlTextProperty =
            DependencyProperty.Register("HtmlText", typeof(string), typeof(RichTextEditor),
                new FrameworkPropertyMetadata(string.Empty));
        /// <summary>
        /// Html文本
        /// </summary>
        public string HtmlText
        {
            get { return (string)GetValue(HtmlTextProperty); }
            set { SetValue(HtmlTextProperty, value); }
        }

        #endregion

        #region 构造函数
        public RichTextEditor()
        {
            InitializeComponent();

            //加粗
            this.ToggleBold.Command = System.Windows.Documents.EditingCommands.ToggleBold;
            //斜体
            this.ToggleItalic.Command = System.Windows.Documents.EditingCommands.ToggleItalic;
            //下划线
            this.ToggleUnderline.Command = System.Windows.Documents.EditingCommands.ToggleUnderline;
            //符号列表
            this.ToggleBullets.Command = System.Windows.Documents.EditingCommands.ToggleBullets;
            //数字列表
            this.ToggleNumbering.Command = System.Windows.Documents.EditingCommands.ToggleNumbering;
            //增加缩进
            this.ToggleIncreaseIndentation.Command = System.Windows.Documents.EditingCommands.IncreaseIndentation;
            //减少缩进
            this.ToggleDecreaseIndentation.Command = System.Windows.Documents.EditingCommands.DecreaseIndentation;
            //左对齐
            this.ToggleAlignLeft.Command = System.Windows.Documents.EditingCommands.AlignLeft;
            //居中对齐
            this.ToggleAlignCenter.Command = System.Windows.Documents.EditingCommands.AlignCenter;
            //右对齐
            this.ToggleAlignRight.Command = System.Windows.Documents.EditingCommands.AlignRight;
            //弹出文本颜色选择框
            this.ToggleFontForeground.Click += ToggleFontForeground_Click;
            //弹出文本背景色选择框
            this.ToggleFontBackground.Click += ToggleFontBackground_Click;
            //添加图片
            this.ButtonAddImage.Click += ButtonAddImage_Click;
            //超链接
            this.ButtonAddHyperLink.Click += ButtonAddHyperLink_Click;
            //删除线
            this.ToggleStrikethrough.Click += ToggleOverLine_Click;
            //设置选中文本颜色
            this.FontForegroundPicker.SetColorHandler += FontForegroundPicker_SetColorHandler;
            //设置选中文本背景色
            this.FontBackgroundPicker.SetColorHandler += FontBackgroundPicker_SetColorHandler;
            //设置选中文本字体大小
            this.FontFamilyList.SetFontFormatHandler += FontFamilyList_SetFontSizeHandler;
            //设置选中文本字体
            this.FontSizeList.SetFontFormatHandler += FontSizeList_SetFontSizeHandler;

            this.FontSizeList.SetDefaultHandler += FontSizeList_SetDefaultHandler;
            this.FontFamilyList.SetDefaultHandler += FontFamilyList_SetDefaultHandler;

            this.richTextBox.LostFocus += RichTextBox_LostFocus; ;

            //初始化编辑器，获取字体、字体大小列表，设置默认字体、字体大小等
            this.InitEditor();
        }

        private void RichTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextRange tr = new TextRange(this.richTextBox.Document.ContentStart, this.richTextBox.Document.ContentEnd);
            this.Text = tr.Text;

            this.HtmlText = System.Windows.Markup.XamlWriter.Save(this.richTextBox.Document);
        }
        #endregion

        #region 事件实现
        /// <summary>
        /// 设置选中文本删除线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleOverLine_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            if (Convert.ToBoolean(toggleButton.IsChecked))
            {
                this.richTextBox.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Strikethrough);
            }
            else
            {
                this.richTextBox.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, null);
            }
        }

        /// <summary>
        /// 添加超链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddHyperLink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = new Hyperlink();
            link.Inlines.Add("http://www.baidu.com");
            link.NavigateUri = new Uri("http://www.baidu.com");
            link.Foreground = new SolidColorBrush(Colors.Blue);
            new Span(link, this.richTextBox.Selection.Start);
        }

        /// <summary>
        /// 往光标所在位置插入图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = string.Empty;
                string fileName = string.Empty;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = ".jpg|*.jpg|.png|*.png|.jpeg|*.jpeg";
                if (openFileDialog.ShowDialog() == true)
                {
                    filePath = openFileDialog.FileName;
                    Image img = new Image();
                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(filePath, UriKind.Relative);
                    bitmapImage.EndInit();

                    img.Source = bitmapImage;

                    //图片大小如果超过100x100，则等比缩小
                    if (bitmapImage.Width > 100 || bitmapImage.Height > 100)
                    {
                        img.Width = bitmapImage.Width * 0.5;
                        img.Height = bitmapImage.Height * 0.5;
                    }

                    img.Stretch = Stretch.Uniform;
                    new InlineUIContainer(img, this.richTextBox.Selection.Start);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 设置选中文本的字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontFamilyList_SetFontSizeHandler(object sender, SetFontFormatEventArgs<object> e)
        {
            this.richTextBox.Selection.ApplyPropertyValue(FontFamilyProperty, e.NewValue);
        }

        /// <summary>
        /// 设置选中文本字体大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeList_SetFontSizeHandler(object sender, SetFontFormatEventArgs<object> e)
        {
            this.richTextBox.Selection.ApplyPropertyValue(FontSizeProperty, Convert.ToDouble(e.NewValue));
        }

        /// <summary>
        /// 设置默认字体大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeList_SetDefaultHandler(object sender, SetFontFormatEventArgs<object> e)
        {

        }

        /// <summary>
        /// 设置默认字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontFamilyList_SetDefaultHandler(object sender, SetFontFormatEventArgs<object> e)
        {

        }

        /// <summary>
        /// 设置选中文本前景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontForegroundPicker_SetColorHandler(object sender, SetForegroundEventArgs<Color> e)
        {
            this.richTextBox.Selection.ApplyPropertyValue(ForegroundProperty, new SolidColorBrush(e.NewValue));
        }

        /// <summary>
        /// 设置选中文本背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontBackgroundPicker_SetColorHandler(object sender, SetForegroundEventArgs<Color> e)
        {
            this.richTextBox.Selection.ApplyPropertyValue(Run.BackgroundProperty, new SolidColorBrush(e.NewValue));
        }

        /// <summary>
        /// 文本颜色按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleFontForeground_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElement = sender as FrameworkElement;
            if (fxElement != null && FontForegroundMenu != null)
            {
                FontForegroundMenu.PlacementTarget = fxElement;
                FontForegroundMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                FontForegroundMenu.IsOpen = true;
            }
        }

        /// <summary>
        /// 背景色按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleFontBackground_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement fxElement = sender as FrameworkElement;
            if (fxElement != null && FontBackgroundMenu != null)
            {
                FontBackgroundMenu.PlacementTarget = fxElement;
                FontBackgroundMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                FontBackgroundMenu.IsOpen = true;
            }
        }

        /// <summary>
        /// RichTextBox文本选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //获得当前选中的文本
            var selection = this.richTextBox.Selection;

            //设置当前文本的格式按钮选中
            this.ToggleBold.IsChecked = EditorMethod.IsBold(selection, this.richTextBox.CaretPosition);
            this.ToggleItalic.IsChecked = EditorMethod.IsItalic(selection, this.richTextBox.CaretPosition);
            this.ToggleUnderline.IsChecked = EditorMethod.IsUnderline(selection, this.richTextBox.CaretPosition);
            this.ToggleStrikethrough.IsChecked = EditorMethod.IsStrikethrough(selection, this.richTextBox.CaretPosition);
            this.ToggleAlignLeft.IsChecked = EditorMethod.IsAlignLeft(selection, this.richTextBox.CaretPosition);
            this.ToggleAlignCenter.IsChecked = EditorMethod.IsAlignCenter(selection, this.richTextBox.CaretPosition);
            this.ToggleAlignRight.IsChecked = EditorMethod.IsAlignRight(selection, this.richTextBox.CaretPosition);

            string fontsize = EditorMethod.GetSelectionFontSize(selection, this.richTextBox.CaretPosition);
            this.FontSizeList.SelectedValue = Math.Round(Convert.ToDouble(fontsize), 0, MidpointRounding.AwayFromZero).ToString();
            this.FontFamilyList.SelectedValue = EditorMethod.GetSelectionFontFamily(selection, this.richTextBox.CaretPosition);
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化编辑器
        /// 
        /// 获取字体、字体大小列表，并设置默认字体、字体大小
        /// </summary>
        private void InitEditor()
        {
            List<string> fontSizeList = new List<string>();
            List<string> fontFamilyList = new List<string>();
            string defaultFontSize = "12";
            string defaultFontFamily = "微软雅黑";

            try
            {
                using (XmlReader reader = XmlTextReader.Create(ConfigPath))
                {
                    XPathDocument xmlDoc = new XPathDocument(reader);
                    XPathNavigator navDoc = xmlDoc.CreateNavigator();
                    XPathNodeIterator it;

                    //获取配置文件中的字体
                    it = navDoc.Select(VisualFontFamiliesPath);
                    while (it.MoveNext())
                    {
                        fontFamilyList.Add(it.Current.Value);
                    }
                    //获取配置文件中字体大小
                    it = navDoc.Select(VisualFontSizePath);
                    while (it.MoveNext())
                    {
                        fontSizeList.Add(it.Current.Value);
                    }
                    //获取默认文字大小
                    it = navDoc.Select(VisualDefaultFontSizePath);
                    while (it.MoveNext())
                    {
                        defaultFontSize = it.Current.Value;
                    }
                    //获取默认字体
                    it = navDoc.Select(VisualDefaultFontFamilyPath);
                    while (it.MoveNext())
                    {
                        defaultFontFamily = it.Current.Value;
                    }

                    #region 设置初始启用功能
                    it = navDoc.Select(VisualEnabledFuncAttchmentPath);
                    while (it.MoveNext())
                    {
                        if(!Convert.ToBoolean(it.Current.Value))
                        {
                            this.ButtonAddAttachment.Visibility = Visibility.Collapsed;
                        }
                    }
                    it = navDoc.Select(VisualEnabledFuncImagePath);
                    while (it.MoveNext())
                    {
                        if (!Convert.ToBoolean(it.Current.Value))
                        {
                            this.ButtonAddImage.Visibility = Visibility.Collapsed;
                        }
                    }
                    it = navDoc.Select(VisualEnabledFuncHyperlinkPath);
                    while (it.MoveNext())
                    {
                        if (!Convert.ToBoolean(it.Current.Value))
                        {
                            this.ButtonAddHyperLink.Visibility = Visibility.Collapsed;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }

            #region 读取系统字体
            //InstalledFontCollection MyFont = new InstalledFontCollection();
            //System.Drawing.FontFamily[] MyFontFamilies = MyFont.Families;
            //int Count = MyFontFamilies.Length;
            //for (int i = 0; i < Count; i++)
            //{
            //    string FontName = MyFontFamilies[i].Name;
            //    fontFamilyList.Add(FontName);
            //}
            #endregion

            this.FontSizeList.ItemsSource = new ReadOnlyCollection<string>(fontSizeList);
            this.FontSizeList.SelectedIndex = 2;
            this.FontFamilyList.ItemsSource = new ReadOnlyCollection<string>(fontFamilyList);
            this.FontFamilyList.SelectedIndex = 0;

            //读取配置，设置默认字体、字体大小
            this.richTextBox.FontSize = Convert.ToDouble(defaultFontSize);
            this.richTextBox.FontFamily = new FontFamily(defaultFontFamily);

            this.richTextBox.Focus();
        }
        #endregion
    }
}