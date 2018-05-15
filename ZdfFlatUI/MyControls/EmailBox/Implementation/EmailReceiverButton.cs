using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    /// <summary>
    /// 收件人控件
    /// </summary>
    internal class EmailReceiverButton : Button
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region IsError
        /// <summary>
        /// 获取或者设置该收件人格式是否正确
        /// </summary>
        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set { SetValue(IsErrorProperty, value); }
        }
        
        public static readonly DependencyProperty IsErrorProperty =
            DependencyProperty.Register("IsError", typeof(bool), typeof(EmailReceiverButton), new PropertyMetadata(false));

        #endregion

        #region IsSelected
        /// <summary>
        /// 获取或者设置该收件人格式是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(EmailReceiverButton), new PropertyMetadata(false));

        #endregion

        #endregion

        #region Constructors

        static EmailReceiverButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EmailReceiverButton), new FrameworkPropertyMetadata(typeof(EmailReceiverButton)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        #endregion
    }
}
