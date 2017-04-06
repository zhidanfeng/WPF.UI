using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace ZdfFlatUI
{
    public class AutoCloseWindow : BaseWindow
    {
        static AutoCloseWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCloseWindow), new FrameworkPropertyMetadata(typeof(AutoCloseWindow)));
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

        public static readonly DependencyProperty CloseButtonTypeProperty = DependencyProperty.Register("CloseButtonType"
            , typeof(CloseBoxTypeEnum), typeof(AutoCloseWindow), new PropertyMetadata(CloseBoxTypeEnum.Close));
        /// <summary>
        /// 关闭按钮执行类型：Close还是Hide
        /// </summary>
        public CloseBoxTypeEnum CloseButtonType
        {
            get { return (CloseBoxTypeEnum)GetValue(CloseButtonTypeProperty); }
            set { SetValue(CloseButtonTypeProperty, value); }
        }

        public static readonly DependencyProperty AutoCloseProperty = DependencyProperty.Register("AutoClose"
            , typeof(bool), typeof(AutoCloseWindow), new PropertyMetadata(false));
        /// <summary>
        /// 自动关闭窗体
        /// </summary>
        public bool AutoClose
        {
            get { return (bool)GetValue(AutoCloseProperty); }
            set { SetValue(AutoCloseProperty, value); }
        }

        public static readonly DependencyProperty AutoCloseIntervalProperty = DependencyProperty.Register("AutoCloseInterval"
            , typeof(double), typeof(AutoCloseWindow), new PropertyMetadata(3d));
        /// <summary>
        /// 自动关闭窗体时间间隔
        /// </summary>
        public double AutoCloseInterval
        {
            get { return (double)GetValue(AutoCloseIntervalProperty); }
            set { SetValue(AutoCloseIntervalProperty, value); }
        }
        
        #endregion

        #region 构造函数
        public AutoCloseWindow() : base()
        {
            this.Loaded += AutoCloseWindow_Loaded;
        }

        private void AutoCloseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //窗体Loaded的时候启动定时关闭窗体的定时器，因此只能将窗体的CloseButtonType属性设置为Close
            //否则不会触发Loaded事件
            if (this.AutoClose)
            {
                this.mAutoCloseTimer.Interval = this.AutoCloseInterval * 1000;
                this.mAutoCloseTimer.Elapsed += MAutoCloseTimer_Elapsed;
                if (!this.mAutoCloseTimer.Enabled)
                {
                    this.mAutoCloseTimer.Enabled = true;
                }
            }
        }

        private void MAutoCloseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.mAutoCloseTimer.Enabled)
            {
                this.mAutoCloseTimer.Enabled = false;
                this.Dispatcher.Invoke(new Action(delegate
                {
                    this.Close();
                }));
            }
        }
        #endregion


        #region 方法重写

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
            if ((this.AutoClose && !this.mIsMaximized) || this.mIsMaximized)
            {
                this.mAutoCloseTimer.Enabled = false;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (this.mIsMaximized)
            {
                this.mAutoCloseTimer.Enabled = false;
                return;
            }

            //如果窗口设置了定时关闭，则当鼠标离开窗口时，定时器重新开始
            if (this.AutoClose && this.IsLoaded)
            {
                this.mAutoCloseTimer.Enabled = true;
            }
        }
        #endregion
    }
}
