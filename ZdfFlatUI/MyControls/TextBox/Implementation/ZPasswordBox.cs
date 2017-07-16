using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ZdfFlatUI.MyControls.Primitives;

namespace ZdfFlatUI
{
    public class ZPasswordBox : IconTextBoxBase
    {
        #region private fields

        private ToggleButton PART_SeePassword;

        /// <summary>
        /// 该属性是为了防止明文转化为密文后，设置Text时，再次触发Text_Changed事件
        /// </summary>
        private bool mIsHandledTextChanged = true;
        private StringBuilder mPasswordBuilder;

        #endregion

        #region DependencyProperty

        #region IsCanSeePassword

        /// <summary>
        /// 获取或者设置是否能看见密码
        /// </summary>
        [Bindable(true), Description("获取或者设置是否能看见密码")]
        public bool IsCanSeePassword
        {
            get { return (bool)GetValue(IsCanSeePasswordProperty); }
            set { SetValue(IsCanSeePasswordProperty, value); }
        }
        
        public static readonly DependencyProperty IsCanSeePasswordProperty =
            DependencyProperty.Register("IsCanSeePassword", typeof(bool), typeof(ZPasswordBox), new PropertyMetadata(true, IsCanSeePasswordChangedCallback));

        private static void IsCanSeePasswordChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZPasswordBox passowrdBox = d as ZPasswordBox;
            if(passowrdBox != null && passowrdBox.PART_SeePassword != null)
            {
                passowrdBox.PART_SeePassword.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        #region Password

        /// <summary>
        /// 获取或者设置当前密码值
        /// </summary>
        [Bindable(true), Description("获取或者设置当前密码值")]
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(ZPasswordBox), new PropertyMetadata(string.Empty));

        #endregion

        #region PasswordChar

        /// <summary>
        /// 获取或者设置PasswordBox的屏蔽字符
        /// </summary>
        [Bindable(true), Description("获取或者设置PasswordBox的屏蔽字符")]
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }
        
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(ZPasswordBox), new PropertyMetadata('●'));

        #endregion

        #endregion

        #region Private DependencyProperty

        #region ShowPassword

        public bool ShowPassword
        {
            get { return (bool)GetValue(ShowPasswordProperty); }
            private set { SetValue(ShowPasswordProperty, value); }
        }
        
        public static readonly DependencyProperty ShowPasswordProperty =
            DependencyProperty.Register("ShowPassword", typeof(bool), typeof(ZPasswordBox), new PropertyMetadata(false, ShowPasswordChanged));

        private static void ShowPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZPasswordBox passwordBox = d as ZPasswordBox;
            if(passwordBox != null)
            {
                passwordBox.SelectionStart = passwordBox.Text.Length + 1;
            }
        }

        #endregion

        #endregion

        #region Constructors

        static ZPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZPasswordBox), new FrameworkPropertyMetadata(typeof(ZPasswordBox)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_SeePassword = this.GetTemplateChild("PART_SeePassword") as ToggleButton;
            if(this.PART_SeePassword != null)
            {
                this.PART_SeePassword.Visibility = this.IsCanSeePassword ? Visibility.Visible : Visibility.Collapsed;
            }
            this.SetEvent();

            //回显密码
            this.SetText(this.ConvertToPasswordChar(this.Password.Length));

            //密码框禁止复制
            this.CommandBindings.Add(new System.Windows.Input.CommandBinding(ApplicationCommands.Copy, CommandBinding_Executed, CommandBinding_CanExecute));
        }
        
        public override void OnCornerRadiusChanged(CornerRadius newValue)
        {
            //根据密码框边框圆角自动设置图标背景框圆角
            this.IconCornerRadius = new CornerRadius(newValue.TopLeft, 0, 0, newValue.BottomLeft);
        }
        #endregion

        #region private function

        private void SetEvent()
        {
            this.TextChanged += ZPasswordBox_TextChanged;
            if(this.PART_SeePassword != null)
            {
                this.PART_SeePassword.Checked += (o, e) => 
                {
                    this.SetText(this.Password);
                    this.ShowPassword = true;
                };
                this.PART_SeePassword.Unchecked += (o, e) =>
                {
                    this.SetText(this.ConvertToPasswordChar(this.Password.Length));
                    this.ShowPassword = false;
                };
            }
        }

        private void SetText(string str)
        {
            this.mIsHandledTextChanged = false;
            this.Text = str;
            this.mIsHandledTextChanged = true;
        }

        /// <summary>
        /// 明文密码转化为特定字符
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ConvertToPasswordChar(int length)
        {
            if (mPasswordBuilder != null)
            {
                mPasswordBuilder.Clear();
            }
            else
            {
                mPasswordBuilder = new StringBuilder();
            }
                
            for (var i = 0; i < length; i++)
            {
                mPasswordBuilder.Append(this.PasswordChar);
            }
                
            return mPasswordBuilder.ToString();
        }

        #endregion

        #region Event Implement Function

        private void ZPasswordBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!this.mIsHandledTextChanged)
                return;

            foreach (TextChange c in e.Changes)
            {
                //从密码文中根据本次Change对象的索引和长度删除对应个数的字符
                this.Password = this.Password.Remove(c.Offset, c.RemovedLength);
                //将Text新增的部分记录给密码文
                this.Password = this.Password.Insert(c.Offset, Text.Substring(c.Offset, c.AddedLength));
            }

            if(!this.ShowPassword)
            {
                this.SetText(ConvertToPasswordChar(Text.Length));
            }

            //将光标放到最后面
            this.SelectionStart = this.Text.Length + 1;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = true;
        }
        #endregion
    }
}
