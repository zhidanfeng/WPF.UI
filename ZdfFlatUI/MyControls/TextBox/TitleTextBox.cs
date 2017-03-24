using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZdfFlatUI.Utils;

namespace ZdfFlatUI
{
    public enum TitleOrientationEnum
    {
        Horizontal,
        Vertical,
    }

    /// <summary>
    /// 带标题的文本框
    /// </summary>
    [TemplatePart(Name = "PART_ClearText", Type = typeof(Path))]
    [TemplatePart(Name = "PART_ContentHost", Type = typeof(ScrollViewer))]
    public class TitleTextBox : TextBox
    {
        private Path PART_ClearText;
        private ScrollViewer PART_ScrollViewer;

        static TitleTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleTextBox), new FrameworkPropertyMetadata(typeof(TitleTextBox)));
        }

        #region 依赖属性

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title"
            , typeof(string), typeof(TitleTextBox));
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty IsShowTitleProperty = DependencyProperty.Register("IsShowTitle"
            , typeof(bool), typeof(TitleTextBox), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示文本
        /// </summary>
        public bool IsShowTitle
        {
            get { return (bool)GetValue(IsShowTitleProperty); }
            set { SetValue(IsShowTitleProperty, value); }
        }

        public static readonly DependencyProperty CanClearTextProperty = DependencyProperty.Register("CanClearText"
            , typeof(bool), typeof(TitleTextBox));
        /// <summary>
        /// 是否可以清空文本的开关
        /// </summary>
        public bool CanClearText
        {
            get { return (bool)GetValue(CanClearTextProperty); }
            set { SetValue(CanClearTextProperty, value); }
        }

        public static readonly DependencyProperty TitleOrientationProperty = DependencyProperty.Register("TitleOrientation"
            , typeof(TitleOrientationEnum), typeof(TitleTextBox));
        /// <summary>
        /// 标题与输入框的排列方式
        /// </summary>
        public TitleOrientationEnum TitleOrientation
        {
            get { return (TitleOrientationEnum)GetValue(TitleOrientationProperty); }
            set { SetValue(TitleOrientationProperty, value); }
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //注册“清空”图标的点击事件
            this.PART_ClearText = VisualHelper.FindVisualElement<Path>(this, "PART_ClearText");
            if(this.PART_ClearText != null)
            {
                this.PART_ClearText.MouseLeftButtonDown += PART_ClearText_MouseLeftButtonDown;
            }

            this.PART_ScrollViewer = VisualHelper.FindVisualElement<ScrollViewer>(this, "PART_ContentHost");

            //监听TextBox的鼠标滚轮滚动事件
            this.PreviewMouseWheel += TitleTextBox_PreviewMouseWheel;
        }

        /// <summary>
        /// 设置TextBox中的ScrollViewer的样式之后就不能用滚轮滚动滚动条了，不知道为什么，因此在此做额外处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleTextBox_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if(this.TitleOrientation == TitleOrientationEnum.Vertical && this.PART_ScrollViewer != null)
            {
                this.PART_ScrollViewer.ScrollToVerticalOffset(this.PART_ScrollViewer.VerticalOffset - e.Delta);
            }
        }

        /// <summary>
        /// 点击清空按钮后，清空文本框中的文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_ClearText_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Text = string.Empty;
        }
    }
}
