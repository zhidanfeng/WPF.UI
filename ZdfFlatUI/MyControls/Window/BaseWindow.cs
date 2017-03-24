using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZdfFlatUI.Utils;

namespace ZdfFlatUI
{
    /// <summary>
    /// 基础窗体，已实现窗体拖动，双击全屏、还原、最大化、最小化、关闭功能、自动关闭等功能
    /// 
    /// 依赖属性：
    /// ShowMore
    /// MaximizeBox
    /// MinimizeBox
    /// CloseBox
    /// CaptionHeight
    /// CloseButtonType
    /// CanMoveWindow
    /// AutoCloseWindow
    /// AutoCloseInterval
    /// TitleBackground
    /// MenuPanel
    /// 
    /// </summary>
    /// <remarks>add by zhidf 2016.6.1</remarks>
    [TemplatePart(Name = "PART_Btn_Close", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Btn_Minimized", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Btn_Maximized", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Btn_Restore", Type = typeof(Button))]
    [TemplatePart(Name = "PART_TitleBar", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Btn_More", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Popup_Menu", Type = typeof(Popup))]
    public class BaseWindow : Window
    {
        static BaseWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseWindow), new FrameworkPropertyMetadata(typeof(BaseWindow)));
        }

        #region 私有属性
        private Button PART_Btn_Close; //关闭按钮
        private Button PART_Btn_Minimized; //最小化按钮
        private Button PART_Btn_Maximized; //最大化按钮
        private Button PART_Btn_Restore; //还原按钮
        private Button PART_Btn_More; //菜单按钮

        private Grid PART_TitleBar;
        private Popup PART_Popup_Menu;

        /// <summary>
        /// 保存上一次窗体的宽度
        /// </summary>
        private double restore_window_width;
        /// <summary>
        /// 保存上一次窗体的高度
        /// </summary>
        private double restore_window_height;
        /// <summary>
        /// 保存上一次窗体距离屏幕左边位置
        /// </summary>
        private double resotre_left;
        /// <summary>
        /// 保存上一次窗体距离屏幕顶部位置
        /// </summary>
        private double resotre_top;
        /// <summary>
        /// 鼠标点击次数，用于判断鼠标双击
        /// </summary>
        private int mouseClickCount;
        /// <summary>
        /// 当前窗体是否处于最大化状态，用于窗体最大化与原大小间切换
        /// </summary>
        private bool mIsMaximized = false;
        /// <summary>
        /// 窗体自动关闭的定时器
        /// </summary>
        private Timer mAutoCloseTimer = new Timer();
        #endregion

        #region 依赖属性
        public static readonly DependencyProperty ShowMoreProperty = DependencyProperty.Register("ShowMore"
            , typeof(bool), typeof(BaseWindow), new PropertyMetadata(false));
        /// <summary>
        /// 是否显示菜单按钮
        /// </summary>
        public bool ShowMore
        {
            get { return (bool)GetValue(ShowMoreProperty); }
            set { SetValue(ShowMoreProperty, value); }
        }

        public static readonly DependencyProperty MaximizeBoxProperty = DependencyProperty.Register("MaximizeBox"
            , typeof(bool), typeof(BaseWindow), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示最大化按钮
        /// </summary>
        public bool MaximizeBox
        {
            get { return (bool)GetValue(MaximizeBoxProperty); }
            set { SetValue(MaximizeBoxProperty, value); }
        }

        public static readonly DependencyProperty MinimizeBoxProperty = DependencyProperty.Register("MinimizeBox"
            , typeof(bool), typeof(BaseWindow), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示最小化按钮
        /// </summary>
        public bool MinimizeBox
        {
            get { return (bool)GetValue(MinimizeBoxProperty); }
            set { SetValue(MinimizeBoxProperty, value); }
        }

        public static readonly DependencyProperty CloseBoxProperty = DependencyProperty.Register("CloseBox"
            , typeof(bool), typeof(BaseWindow), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool CloseBox
        {
            get { return (bool)GetValue(CloseBoxProperty); }
            set { SetValue(CloseBoxProperty, value); }
        }

        public static readonly DependencyProperty CaptionHeightProperty = DependencyProperty.Register("CaptionHeight"
            , typeof(double), typeof(BaseWindow), new PropertyMetadata(30d));
        /// <summary>
        /// 标题栏高度
        /// </summary>
        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonTypeProperty = DependencyProperty.Register("CloseButtonType"
            , typeof(CloseBoxTypeEnum), typeof(BaseWindow), new PropertyMetadata(CloseBoxTypeEnum.Close));
        /// <summary>
        /// 关闭按钮执行类型：Close还是Hide
        /// </summary>
        public CloseBoxTypeEnum CloseButtonType
        {
            get { return (CloseBoxTypeEnum)GetValue(CloseButtonTypeProperty); }
            set { SetValue(CloseButtonTypeProperty, value); }
        }

        public static readonly DependencyProperty CanMoveWindowProperty = DependencyProperty.Register("CanMoveWindow"
            , typeof(bool), typeof(BaseWindow), new PropertyMetadata(true));
        /// <summary>
        /// 窗体能否被拖动
        /// </summary>
        public bool CanMoveWindow
        {
            get { return (bool)GetValue(CanMoveWindowProperty); }
            set { SetValue(CanMoveWindowProperty, value); }
        }

        public static readonly DependencyProperty AutoCloseWindowProperty = DependencyProperty.Register("AutoCloseWindow"
            , typeof(bool), typeof(BaseWindow), new PropertyMetadata(false));
        /// <summary>
        /// 自动关闭窗体
        /// </summary>
        public bool AutoCloseWindow
        {
            get { return (bool)GetValue(AutoCloseWindowProperty); }
            set { SetValue(AutoCloseWindowProperty, value); }
        }

        public static readonly DependencyProperty AutoCloseIntervalProperty = DependencyProperty.Register("AutoCloseInterval"
            , typeof(double), typeof(BaseWindow), new PropertyMetadata(3d));
        /// <summary>
        /// 自动关闭窗体时间间隔
        /// </summary>
        public double AutoCloseInterval
        {
            get { return (double)GetValue(AutoCloseIntervalProperty); }
            set { SetValue(AutoCloseIntervalProperty, value); }
        }

        public static readonly DependencyProperty TitleBackgroundProperty = DependencyProperty.Register("TitleBackground"
            , typeof(Brush), typeof(BaseWindow));
        /// <summary>
        /// 窗口标题栏背景色
        /// </summary>
        public Brush TitleBackground
        {
            get { return (Brush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MenuPanelProperty = DependencyProperty.Register("MenuPanel"
            , typeof(Panel), typeof(BaseWindow));
        /// <summary>
        /// 窗口标题栏背景色
        /// </summary>
        public Panel MenuPanel
        {
            get { return (Panel)GetValue(MenuPanelProperty); }
            set { SetValue(MenuPanelProperty, value); }
        }
        #endregion

        #region 事件定义

        public static readonly RoutedEvent ShowMoreClickEvent = EventManager.RegisterRoutedEvent("ShowMoreClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(BaseWindow));

        public event RoutedPropertyChangedEventHandler<object> ShowMoreClick
        {
            add
            {
                this.AddHandler(ShowMoreClickEvent, value);
            }
            remove
            {
                this.RemoveHandler(ShowMoreClickEvent, value);
            }
        }

        protected virtual void OnShowMoreClick(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg =
                new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, ShowMoreClickEvent);
            this.RaiseEvent(arg);
        }
        #endregion

        #region 构造函数
        public BaseWindow() : base()
        {
            this.Loaded += BaseWindow_Loaded;
        }

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //窗体Loaded的时候启动定时关闭窗体的定时器，因此只能将窗体的CloseButtonType属性设置为Close
            //否则不会触发Loaded事件
            if (this.AutoCloseWindow)
            {
                this.mAutoCloseTimer.Interval = this.AutoCloseInterval * 1000;
                this.mAutoCloseTimer.Elapsed += MAutoCloseTimer_Elapsed;
                if(!this.mAutoCloseTimer.Enabled)
                {
                    this.mAutoCloseTimer.Enabled = true;
                }
            }

            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    this.WindowState = WindowState.Normal;
                    this.SetWindowMaximized();
                    break;
                default:
                    break;
            }
        }

        private void MAutoCloseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(this.mAutoCloseTimer.Enabled)
            {
                this.mAutoCloseTimer.Enabled = false;
                this.Dispatcher.Invoke(new Action(delegate 
                {
                    this.Close();
                }));
            }
        }
        #endregion

        #region 事件处理

        #region 窗体标题栏双击最大化、还原
        private void GridTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //不允许最大化的时候，双击标题栏自然也不允许将窗体最大化
            if (!this.MaximizeBox) return;

            mouseClickCount += 1;
            DispatcherTimer timer = new DispatcherTimer();
            //设置鼠标左键点击间隔为0.3秒，超过0.3秒将mouseClickCount重置
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; mouseClickCount = 0; };
            timer.IsEnabled = true;
            if (mouseClickCount % 2 == 0)
            {
                timer.IsEnabled = false;
                mouseClickCount = 0;

                //判断当前窗体状态，如果是最大化，则双击之后还原窗体大小，否则将窗体最大化
                if(this.mIsMaximized)
                {
                    this.SetWindowSizeRestore();
                }
                else
                {
                    this.SetWindowMaximized();
                }
            }
        }
        #endregion

        /// <summary>
        /// 还原窗体大小按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_Btn_Restore_Click(object sender, RoutedEventArgs e)
        {
            SetWindowSizeRestore();
        }

        /// <summary>
        /// 窗口最大化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_Btn_Maximized_Click(object sender, RoutedEventArgs e)
        {
            SetWindowMaximized();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_Btn_More_Click(object sender, RoutedEventArgs e)
        {
            //this.OnShowMoreClick(null, null);
            if(this.PART_Btn_More != null)
            {
                this.PART_Popup_Menu.Child = this.MenuPanel;
                this.PART_Popup_Menu.IsOpen = true;
            }
        }
        #endregion

        #region 设置窗口大小
        /// <summary>
        /// 设置窗口最大化
        /// </summary>
        private void SetWindowMaximized()
        {
            if (VisualTreeHelper.GetChildrenCount(this) > 0)
            {
                //最大化窗体前保留窗体的原始大小与位置
                this.restore_window_width = this.Width;
                this.restore_window_height = this.Height;
                this.resotre_left = this.Left;
                this.resotre_top = this.Top;

                Grid a = (Grid)VisualTreeHelper.GetChild(this, 0);
                //设置Grid的Margin为0，用于去除窗体阴影
                a.Margin = new Thickness(0, 0, 0, 0);
                Rectangle b = (Rectangle)VisualTreeHelper.GetChild(a, 0);
                //隐藏阴影
                b.Visibility = Visibility.Collapsed;
                this.Left = 0;
                this.Top = 0;
                Rect rc = SystemParameters.WorkArea;//获取工作区大小
                this.Width = rc.Width;
                this.Height = rc.Height;

                //this.Animation(this.Width, this.Height, rc.Width, rc.Height);

                this.mIsMaximized = true;
                this.PART_Btn_Maximized.Visibility = Visibility.Hidden;
                this.PART_Btn_Restore.Visibility = Visibility.Visible;
            }
        }

        Storyboard storyboard = new Storyboard();
        private void Animation(double oldWidth, double oldHeight, double newWidth, double newHeight)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation(oldWidth, newWidth, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(widthAnimation, this);
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("(Window.Width)"));
            storyboard.Children.Add(widthAnimation);

            DoubleAnimation heightAnimation = new DoubleAnimation(oldWidth, newWidth, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(heightAnimation, this);
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath("(Window.Height)"));
            storyboard.Children.Add(heightAnimation);

            storyboard.Begin();
        }

        /// <summary>
        /// 还原窗体大小
        /// </summary>
        private void SetWindowSizeRestore()
        {
            if (VisualTreeHelper.GetChildrenCount(this) > 0)
            {
                Grid a = (Grid)VisualTreeHelper.GetChild(this, 0);
                //设置Grid的Margin，用于显示窗体阴影
                a.Margin = new Thickness(20, 20, 20, 20);
                Rectangle b = (Rectangle)VisualTreeHelper.GetChild(a, 0);
                //显示阴影
                b.Visibility = Visibility.Visible;
                this.Left = this.resotre_left;
                this.Top = this.resotre_top;
                this.Width = this.restore_window_width;
                this.Height = this.restore_window_height;

                this.mIsMaximized = false;
                this.PART_Btn_Restore.Visibility = Visibility.Hidden;
                this.PART_Btn_Maximized.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region 方法重写
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Btn_Close = VisualHelper.FindVisualElement<Button>(this, "PART_Btn_Close");
            this.PART_Btn_Minimized = VisualHelper.FindVisualElement<Button>(this, "PART_Btn_Minimized");
            this.PART_Btn_Maximized = VisualHelper.FindVisualElement<Button>(this, "PART_Btn_Maximized");
            this.PART_Btn_Restore = VisualHelper.FindVisualElement<Button>(this, "PART_Btn_Restore");
            this.PART_TitleBar = VisualHelper.FindVisualElement<Grid>(this, "PART_TitleBar");
            this.PART_Btn_More = VisualHelper.FindVisualElement<Button>(this, "PART_Btn_More");
            this.PART_Popup_Menu = VisualHelper.FindVisualElement<Popup>(this, "PART_Popup_Menu");

            if (this.PART_Btn_Close != null)
            {
                this.PART_Btn_Close.Click += Btn_close_Click;
            }

            if (this.PART_Btn_Maximized != null)
            {
                this.PART_Btn_Maximized.Click += PART_Btn_Maximized_Click;
            }

            if (this.PART_Btn_Restore != null)
            {
                this.PART_Btn_Restore.Click += PART_Btn_Restore_Click;
            }

            if (!this.MaximizeBox && !this.MinimizeBox && !this.CloseBox && string.IsNullOrEmpty(this.Title.Trim()))
            {
                this.PART_TitleBar.Visibility = Visibility.Collapsed;
            }

            if (this.PART_Btn_More != null)
            {
                this.PART_Btn_More.Click += PART_Btn_More_Click; ;
            }

            if (this.PART_TitleBar != null)
            {
                this.PART_TitleBar.MouseLeftButtonDown += GridTitleBar_MouseLeftButtonDown;
            }
        }

        /// <summary>
        /// 拖动窗体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (!this.CanMoveWindow) return;

            if (this.mIsMaximized) return;

            base.OnMouseLeftButtonDown(e);

            if (PART_TitleBar != null)
            {
                // 获取鼠标相对标题栏位置  
                Point position = e.GetPosition(PART_TitleBar);

                // 如果鼠标位置在标题栏内，允许拖动  
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (position.X >= 0 && position.X < PART_TitleBar.ActualWidth && position.Y >= 0 
                        && position.Y < PART_TitleBar.ActualHeight)
                    {
                        this.DragMove();
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            switch (this.CloseButtonType)
            {
                case CloseBoxTypeEnum.Close:
                    break;
                case CloseBoxTypeEnum.Hide:
                    this.Hide();
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (this.mAutoCloseTimer != null)
            {
                this.mAutoCloseTimer.Enabled = false;
                this.mAutoCloseTimer.Dispose();
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            //如果窗口设置了定时关闭，则当鼠标置于窗口上时，定时器停止
            if((this.AutoCloseWindow && !this.mIsMaximized) || this.mIsMaximized)
            {
                this.mAutoCloseTimer.Enabled = false;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if(this.mIsMaximized)
            {
                this.mAutoCloseTimer.Enabled = false;
                return;
            }

            //如果窗口设置了定时关闭，则当鼠标离开窗口时，定时器重新开始
            if (this.AutoCloseWindow &&　this.IsLoaded)
            {
                this.mAutoCloseTimer.Enabled = true;
            }
        }
        #endregion
    }

    public enum CloseBoxTypeEnum
    {
        /// <summary>
        /// 关闭窗口
        /// </summary>
        Close,
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        Hide,
    }
}
