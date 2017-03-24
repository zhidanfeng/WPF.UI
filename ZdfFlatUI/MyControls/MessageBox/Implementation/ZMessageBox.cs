using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ZdfFlatUI
{
    internal sealed class MessageBoxModule : Window
    {
        #region Private属性
        private Button PART_CloseButton;
        private Storyboard openStoryboard;
        private Storyboard closedStoryboard;
        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty TypeProperty;
        public static readonly DependencyProperty MessageTextProperty;
        public static readonly DependencyProperty ButtonCollectionProperty;
        public static readonly DependencyProperty YesButtonTextProperty;
        public static readonly DependencyProperty NoButtonTextProperty;
        public static readonly DependencyProperty OkButtonTextProperty;
        public static readonly DependencyProperty CancelButtonTextProperty;
        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 类型：Info、Warn、Error、Success
        /// </summary>
        public EnumPromptType Type
        {
            get { return (EnumPromptType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string MessageText
        {
            get { return (string)GetValue(MessageTextProperty); }
            set { SetValue(MessageTextProperty, value); }
        }
        /// <summary>
        /// 按钮组
        /// </summary>
        public ObservableCollection<Button> ButtonCollection
        {
            get { return (ObservableCollection<Button>)GetValue(ButtonCollectionProperty); }
            private set { SetValue(ButtonCollectionProperty, value); }
        }
        /// <summary>
        /// 类型为Yes的Button的内容
        /// </summary>
        public string YesButtonText
        {
            get { return (string)GetValue(YesButtonTextProperty); }
            set { SetValue(YesButtonTextProperty, value); }
        }
        /// <summary>
        /// 类型为No的Button的内容
        /// </summary>
        public string NoButtonText
        {
            get { return (string)GetValue(NoButtonTextProperty); }
            set { SetValue(NoButtonTextProperty, value); }
        }
        /// <summary>
        /// 类型为Ok的Button的内容
        /// </summary>
        public string OkButtonText
        {
            get { return (string)GetValue(OkButtonTextProperty); }
            set { SetValue(OkButtonTextProperty, value); }
        }
        /// <summary>
        /// 类型为Cancel的Button的内容
        /// </summary>
        public string CancelButtonText
        {
            get { return (string)GetValue(CancelButtonTextProperty); }
            set { SetValue(CancelButtonTextProperty, value); }
        }
        #endregion

        #region Constructors
        static MessageBoxModule()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxModule), new FrameworkPropertyMetadata(typeof(MessageBoxModule)));
            MessageBoxModule.TypeProperty = DependencyProperty.Register("Type", typeof(EnumPromptType), typeof(MessageBoxModule), new PropertyMetadata(EnumPromptType.Info));
            MessageBoxModule.MessageTextProperty = DependencyProperty.Register("MessageText", typeof(string), typeof(MessageBoxModule));
            MessageBoxModule.ButtonCollectionProperty = DependencyProperty.Register("ButtonCollection", typeof(ObservableCollection<Button>), typeof(MessageBoxModule));
            MessageBoxModule.YesButtonTextProperty = DependencyProperty.Register("YesButtonText", typeof(string), typeof(MessageBoxModule), new PropertyMetadata("是"));
            MessageBoxModule.NoButtonTextProperty = DependencyProperty.Register("NoButtonText", typeof(string), typeof(MessageBoxModule), new PropertyMetadata("否"));
            MessageBoxModule.OkButtonTextProperty = DependencyProperty.Register("OkButtonText", typeof(string), typeof(MessageBoxModule), new PropertyMetadata("确定"));
            MessageBoxModule.CancelButtonTextProperty = DependencyProperty.Register("CancelButtonText", typeof(string), typeof(MessageBoxModule), new PropertyMetadata("取消"));
        }

        public MessageBoxModule()
        {
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //AllowsTransparency、WindowStyle属性不能在样式文件中设置，否则会报错
            this.AllowsTransparency = true;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ShowInTaskbar = false;
            this.Topmost = false;
            this.ButtonCollection = new ObservableCollection<Button>();

            this.Loaded += MessageBoxModule_Loaded;
        }

        

        private void MessageBoxModule_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Loaded", true);


        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_CloseButton = this.GetTemplateChild("PART_CloseButton") as Button;
            Grid  s  = this.GetTemplateChild("grid") as Grid;

            
            if (this.PART_CloseButton != null)
            {
                this.PART_CloseButton.Click += CloseWindow;
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.PART_CloseButton.Click -= CloseWindow;
            this.Close();
        }
        #endregion

        #region Private方法

        #endregion

        #region public static
        public static MessageBoxResult Show(string messageBoxText)
        {
            return MessageBoxModule.Show(messageBoxText, "");
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return MessageBoxModule.Show(messageBoxText, caption, MessageBoxButton.OK);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBoxModule.Show(null, messageBoxText, caption, button);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return MessageBoxModule.Show(owner, messageBoxText, "", MessageBoxButton.OK);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            MessageBoxResult defaultResult = MessageBoxResult.None;
            switch (button)
            {
                case MessageBoxButton.OK:
                    defaultResult = MessageBoxResult.OK;
                    break;
                case MessageBoxButton.OKCancel:
                    defaultResult = MessageBoxResult.Cancel;
                    break;
                case MessageBoxButton.YesNoCancel:
                    defaultResult = MessageBoxResult.Cancel;
                    break;
                case MessageBoxButton.YesNo:
                    defaultResult = MessageBoxResult.No;
                    break;
                default:
                    break;
            }
            return MessageBoxModule.Show(owner, messageBoxText, caption, button, defaultResult);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption
            , MessageBoxButton button, MessageBoxResult defaultResult)
        {
            return MessageBoxModule.Show(owner, messageBoxText, caption, button, defaultResult, EnumPromptType.Info);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption
            , MessageBoxButton button, MessageBoxResult defaultResult, EnumPromptType type)
        {
            MessageBoxModule messageBox = new MessageBoxModule();
            
            if(owner != null)
            {
                //蒙板
                Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) };
                //父级窗体原来的内容
                UIElement original = owner.Content as UIElement;
                owner.Content = null;
                //容器Grid
                Grid container = new Grid();
                container.Children.Add(original);//放入原来的内容
                container.Children.Add(layer);//在上面放一层蒙板
                                              //将装有原来内容和蒙板的容器赋给父级窗体
                owner.Content = container;
                messageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                messageBox.ShowInTaskbar = true;
            }

            messageBox.Owner = owner;
            messageBox.Title = caption;
            messageBox.MessageText = messageBoxText;
            messageBox.Type = type;

            switch (button)
            {
                case MessageBoxButton.OK:
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "确定", FlatButtonSkinEnum.primary, MessageBoxResult.OK));
                    break;
                case MessageBoxButton.OKCancel:
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "取消", FlatButtonSkinEnum.ghost, MessageBoxResult.Cancel));
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "确定", FlatButtonSkinEnum.primary, MessageBoxResult.OK));
                    break;
                case MessageBoxButton.YesNoCancel:
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "取消", FlatButtonSkinEnum.Default, MessageBoxResult.Cancel));
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "否", FlatButtonSkinEnum.ghost, MessageBoxResult.No));
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "是", FlatButtonSkinEnum.primary, MessageBoxResult.Yes));
                    break;
                case MessageBoxButton.YesNo:
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "否", FlatButtonSkinEnum.ghost, MessageBoxResult.No));
                    messageBox.ButtonCollection.Add(CreateButton(messageBox, "是", FlatButtonSkinEnum.primary, MessageBoxResult.Yes));
                    break;
                default:
                    break;
            }

            //该行是关键代码，窗体显示后进入阻塞状态，直到窗体有返回值：即点击了某个按钮或者关闭了窗体
            bool? result = messageBox.ShowDialog();
            switch (button)
            {

                //break;
                case MessageBoxButton.OKCancel:
                    {
                        return result == true ? MessageBoxResult.OK
                            : result == false ? MessageBoxResult.Cancel :
                            MessageBoxResult.None;
                    }
                //break;
                case MessageBoxButton.YesNo:
                    {
                        return result == true ? MessageBoxResult.Yes : MessageBoxResult.No;
                    }
                //break;
                case MessageBoxButton.YesNoCancel:
                    {
                        return result == true ? MessageBoxResult.Yes
                            : result == false ? MessageBoxResult.No :
                            MessageBoxResult.Cancel;
                    }

                case MessageBoxButton.OK:
                default:
                    {
                        return result == true ? MessageBoxResult.OK : MessageBoxResult.None;
                    }
            }
        }

        public static MessageBoxResult Show(string messageBoxText, EnumPromptType type)
        {
            return Show(messageBoxText, "", type);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, EnumPromptType type)
        {
            return MessageBoxModule.Show(null, messageBoxText, caption, MessageBoxButton.OK, MessageBoxResult.OK, type);
        }
        #endregion

        private static string GetDefaultCaption(EnumPromptType type)
        {
            string caption = string.Empty;
            switch (type)
            {
                case EnumPromptType.Info:
                    caption = "提示";
                    break;
                case EnumPromptType.Warn:
                    caption = "警告";
                    break;
                case EnumPromptType.Error:
                    caption = "错误";
                    break;
                case EnumPromptType.Success:
                    caption = "成功";
                    break;
                default:
                    break;
            }

            return caption;
        }

        private static Button CreateButton(MessageBoxModule messageBox, string content, FlatButtonSkinEnum buttonType, MessageBoxResult dialogResult)
        {
            FlatButton button = new FlatButton();
            button.Content = content;
            button.Type = buttonType;
            button.Width = 70;
            button.Height = 28;
            button.CornerRadius = new CornerRadius(3);
            button.Margin = new Thickness(5,0,5,0);
            //注册按钮的点击事件，返回相应的结果用于ShowDialog的返回值
            button.Click += (o, e) => 
            {
                bool? flag = null;
                switch (dialogResult)
                {
                    case MessageBoxResult.None:
                        flag = null;
                        break;
                    case MessageBoxResult.OK:
                        flag = true;
                        break;
                    case MessageBoxResult.Cancel:
                        flag = false;
                        break;
                    case MessageBoxResult.Yes:
                        flag = true;
                        break;
                    case MessageBoxResult.No:
                        flag = false;
                        break;
                    default:
                        break;
                }
                messageBox.DialogResult = flag;
            };

            return button;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(this.Owner != null)
            {
                //容器Grid
                Grid grid = this.Owner.Content as Grid;
                //父级窗体原来的内容
                UIElement original = VisualTreeHelper.GetChild(grid, 0) as UIElement;
                //将父级窗体原来的内容在容器Grid中移除
                grid.Children.Remove(original);
                //赋给父级窗体
                this.Owner.Content = original;
            }
            
            VisualStateManager.GoToState(this, "Closed", true);
            //e.Cancel = true;
        }
    }

    public class ZMessageBox
    {
        public static MessageBoxResult Show(string messageBoxText)
        {
            return MessageBoxModule.Show(messageBoxText);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return MessageBoxModule.Show(messageBoxText, caption);
        }

        public static MessageBoxResult Show(string messageBoxText, EnumPromptType type)
        {
            return MessageBoxModule.Show(messageBoxText, type);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, EnumPromptType type)
        {
            return MessageBoxModule.Show(messageBoxText, caption, type);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return MessageBoxModule.Show(owner, messageBoxText);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return MessageBoxModule.Show(owner, messageBoxText, caption, MessageBoxButton.OK);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBoxModule.Show(messageBoxText, caption, button);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBoxModule.Show(owner, messageBoxText, caption, button);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, EnumPromptType type)
        {
            return MessageBoxModule.Show(owner, messageBoxText, caption, button, MessageBoxResult.OK, type);
        }
    }
}
