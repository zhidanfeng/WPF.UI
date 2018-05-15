using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace ZdfFlatUI
{
    public class EmailBox : RichTextBox
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region TagButtonStyle
        /// <summary>
        /// 获取或者设置单个收件人的样式
        /// </summary>
        public Style ReceiverButtonStyle
        {
            get { return (Style)GetValue(ReceiverButtonStyleProperty); }
            set { SetValue(ReceiverButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ReceiverButtonStyleProperty =
            DependencyProperty.Register("ReceiverButtonStyle", typeof(Style), typeof(EmailBox));

        #endregion

        #endregion

        #region Constructors

        static EmailBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EmailBox), new FrameworkPropertyMetadata(typeof(EmailBox)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PreviewKeyDown += EmailBox_PreviewKeyDown;
            //解决所有的通过InlineUIContainer添加的控件的Enable都为false的问题
            this.IsDocumentEnabled = true;
        }

        private void EmailBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TextRange tr = new TextRange(this.Document.ContentStart, this.Document.ContentEnd);
                string text = tr.Text.Trim();

                //this.Document.Blocks.Clear();

                EmailReceiverButton btn = new EmailReceiverButton();
                btn.Content = text;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Margin = new Thickness(2);
                btn.Click += EmailReceiverButton_Click;

                //TextPointer pointer = TextPointer.

                //Paragraph p = new Paragraph();
                InlineUIContainer container = new InlineUIContainer(btn, this.Selection.End);
                //p.Inlines.Add(container);
                container.SetValue(FocusManager.IsFocusScopeProperty, true);
            }
        }

        /// <summary>
        /// 单击单个收件人，设置选中状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailReceiverButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var block in this.Document.Blocks)
            {
                var paragraph = block as Paragraph;
                if (paragraph == null)
                {
                    continue;
                }

                foreach (var item in paragraph.Inlines)
                {
                    InlineUIContainer container = item as InlineUIContainer;
                    if (container == null)
                    {
                        continue;
                    }
                    EmailReceiverButton button = container.Child as EmailReceiverButton;
                    if (sender == button)
                    {
                        button.IsSelected = true;
                    }
                    else
                    {
                        button.IsSelected = false;
                    }
                }
            }
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        #endregion
    }
}
