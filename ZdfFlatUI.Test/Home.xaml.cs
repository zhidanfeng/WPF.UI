using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZdfFlatUI.Test.Model;

namespace ZdfFlatUI.Test
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Window
    {
        private ObservableCollection<MenuInfo> _MenuList;

        public ObservableCollection<MenuInfo> MenuList
        {
            get { return _MenuList; }
            set { _MenuList = value; }
        }

        #region 单例
        private static Home instance;

        public static Home Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Home();
                }
                return instance;
            }
        }
        #endregion

        public Home()
        {
            InitializeComponent();

            Utils.PaletteHelper.SetLightDarkTheme(false);

            this.MenuList = new ObservableCollection<MenuInfo>();

            #region Base
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Color色彩",
                GroupName = ControlType.Basic.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Icon图标",
                GroupName = ControlType.Basic.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Button按钮",
                GroupName = ControlType.Basic.ToString(),
            });
            #endregion

            #region Form
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Input输入框",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Radio单选框",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "CheckBox多选框",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Switch开关",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Select选择器",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "UpDown数字选择器",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Slider滑块",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Progress进度条",
                GroupName = ControlType.Form.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Upload上传",
                GroupName = ControlType.Form.ToString(),
            }); 
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Validate数据校验",
                GroupName = ControlType.Form.ToString(),
            });
            #endregion

            #region View
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Tree树形控件",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Dashboard表盘",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Badge徽标数",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Notice通知提醒",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "ToolTip文字提示",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Carousel走马灯",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "MessageBox提示框",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Page分页",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Cascader级联选择",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "StepBar步骤条",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Loading加载中",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "BusyIndicator遮罩层",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "分组面板",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "DropDown下拉",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "TabControl页签",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Flayout悬浮面板",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Editor富文本编辑器",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Rate评分",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Date日历控件",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "TimePicker时间选择器",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "锚点定位",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "ColorSelector颜色选择",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "定制主题",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "悬浮按钮",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Tag标签",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Timeline时间轴",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Waterfall瀑布流",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "SwitchMenu菜单",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Accordion手风琴",
                GroupName = ControlType.View.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "音视频播放器",
                GroupName = ControlType.View.ToString(),
            });
            #endregion

            this.menu.GroupItemsSource = this.MenuList;
            this.menu.GroupDescriptions = "GroupName";
        }

        private void menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.menu.SelectedItem != null)
            {
                MenuInfo info = this.menu.SelectedItem as MenuInfo;
                switch (info.Name)
                {
                    #region Base
                    case "Color色彩":
                        this.ControlPanel.Content = new UITest.Base.Color();
                        break;
                    case "Icon图标":
                        this.ControlPanel.Content = new UITest.Base.Icon();
                        break;
                    #endregion

                    #region Form
                    case "Button按钮":
                        this.ControlPanel.Content = new UITest.UCTButtonTest();
                        break;
                    case "Input输入框":
                        this.ControlPanel.Content = new UITest.UCTTextBoxTest();
                        break;
                    case "Radio单选框":
                        this.ControlPanel.Content = new UITest.UCTRadioButtonTest();
                        break;
                    case "CheckBox多选框":
                        this.ControlPanel.Content = new UITest.UCTCheckBoxTest();
                        break;
                    case "Switch开关":
                        this.ControlPanel.Content = new UITest.UCTToggleButtonTest();
                        break;
                    case "Select选择器":
                        this.ControlPanel.Content = new UITest.UCTMultiComboBoxTest();
                        break;
                    case "UpDown数字选择器":
                        this.ControlPanel.Content = new UITest.UCTNumericUpDownTest();
                        break;
                    case "Slider滑块":
                        this.ControlPanel.Content = new UITest.UCTSilderTest();
                        break;
                    case "Progress进度条":
                        this.ControlPanel.Content = new UITest.UCTProgressBarTest();
                        break;
                    case "Upload上传":
                        this.ControlPanel.Content = new UITest.UCTUploadTest();
                        break;
                    case "Validate数据校验":
                        break;
                    #endregion

                    #region View
                    case "Tree树形控件":
                        this.ControlPanel.Content = new UITest.UCTTreeViewTest();
                        break;
                    case "Dashboard表盘":
                        this.ControlPanel.Content = new UITest.UCTDashboardTest();
                        break;
                    case "Badge徽标数":
                        this.ControlPanel.Content = new UITest.UCTBadgeTest();
                        break;
                    case "Notice通知提醒":
                        this.ControlPanel.Content = new UITest.UCTNoticeTest();
                        break;
                    case "ToolTip文字提示":
                        this.ControlPanel.Content = new UITest.UCTToolTipTest();
                        break;
                    case "Carousel走马灯":
                        this.ControlPanel.Content = new UITest.UCTCarouselTest();
                        break;
                    case "MessageBox提示框":
                        this.ControlPanel.Content = new UITest.UCTMessageBoxTest();
                        break;
                    case "Page分页":
                        break;
                    case "Cascader级联选择":
                        this.ControlPanel.Content = new UITest.UCTCascaderBoxTest();
                        break;
                    case "Naviagtion导航条":
                        this.ControlPanel.Content = new UITest.UCTNavigationBarTest();
                        break;
                    case "StepBar步骤条":
                        this.ControlPanel.Content = new UITest.UCTStepBarTest();
                        break;
                    case "Loading加载中":
                        this.ControlPanel.Content = new UITest.UCTLoadingTest();
                        break;
                    case "BusyIndicator遮罩层":
                        this.ControlPanel.Content = new UITest.UCTBusyIndicatorTest();
                        break;
                    case "分组面板":
                        this.ControlPanel.Content = new UITest.UCTGroupPanelTest();
                        break;
                    case "DropDown下拉":
                        this.ControlPanel.Content = new UITest.UCTDropDownTest();
                        break;
                    case "TabControl页签":
                        this.ControlPanel.Content = new UITest.UCTTabControlTest();
                        break;
                    case "Flayout悬浮面板":
                        this.ControlPanel.Content = new UITest.UCTFlyoutTest();
                        break;
                    case "Editor富文本编辑器":
                        this.ControlPanel.Content = new UITest.UCTRichTextEditor();
                        break;
                    case "Rate评分":
                        this.ControlPanel.Content = new UITest.UCTRate();
                        break;
                    case "Date日历控件":
                        this.ControlPanel.Content = new UITest.UCTDateControl();
                        break;
                    case "TimePicker时间选择器":
                        this.ControlPanel.Content = new UITest.UCTTimeControl();
                        break;
                    case "锚点定位":
                        this.ControlPanel.Content = new UITest.UCTNavigationBarTest();
                        break;
                    case "ColorSelector颜色选择":
                        this.ControlPanel.Content = new UITest.UCTColorSelectorTest();
                        break;
                    case "定制主题":
                        this.ControlPanel.Content = new UITest.ChangeTheme.UCTChangeTheme();
                        break;
                    case "悬浮按钮":
                        this.ControlPanel.Content = new UITest.UCTFloatingActionControl();
                        break;
                    case "Tag标签":
                        this.ControlPanel.Content = new UITest.UCTTag();
                        break;
                    case "Timeline时间轴":
                        this.ControlPanel.Content = new UITest.UCTTimeline();
                        break;
                    case "Waterfall瀑布流":
                        this.ControlPanel.Content = new UITest.UCTWaterfallPanel();
                        break;
                    case "SwitchMenu菜单":
                        this.ControlPanel.Content = new UITest.UCTSwitchMenu();
                        break;
                    case "Accordion手风琴":
                        this.ControlPanel.Content = new UITest.UCTAccordion();
                        break;
                    case "音视频播放器":
                        this.ControlPanel.Content = new UITest.UCTMusicAndVideo();
                        break;
                        #endregion
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            Environment.Exit(0);
            base.OnClosed(e);
        }
    }

    public enum ControlType
    {
        Basic,
        Form,
        View,
    }
}
