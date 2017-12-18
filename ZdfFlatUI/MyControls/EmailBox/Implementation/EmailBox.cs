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
        public Style TagButtonStyle
        {
            get { return (Style)GetValue(TagButtonStyleProperty); }
            set { SetValue(TagButtonStyleProperty, value); }
        }
        
        public static readonly DependencyProperty TagButtonStyleProperty =
            DependencyProperty.Register("TagButtonStyle", typeof(Style), typeof(EmailBox));

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

            this.PreviewKeyDown += EmailBox_KeyDown;
            //解决所有的通过InlineUIContainer添加的控件的Enable都为false的问题
            this.IsDocumentEnabled = true;
        }

        private void EmailBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                TextRange tr = new TextRange(this.Document.ContentStart, this.Document.ContentEnd);
                string text = tr.Text.Trim();

                this.Document.Blocks.Clear();

                Button btn = new Button();
                btn.Content = text;
                btn.VerticalAlignment = VerticalAlignment.Stretch;
                btn.Margin = new Thickness(0, 2, 4, 0);

                //TextPointer pointer = TextPointer.

                InlineUIContainer container = new InlineUIContainer(btn, this.CaretPosition);
                container.SetValue(FocusManager.IsFocusScopeProperty, true);
            }
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        #endregion
    }
}
