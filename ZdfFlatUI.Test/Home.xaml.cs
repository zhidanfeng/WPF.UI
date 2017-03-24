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

            this.MenuList = new ObservableCollection<MenuInfo>();
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Color色彩",
                GroupName = ControlType.Basic.ToString(),
            });
            this.MenuList.Add(new MenuInfo()
            {
                Name = "Button按钮",
                GroupName = ControlType.Basic.ToString(),
            });

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
                    case "Color色彩":
                        this.ControlPanel.Content = new UITest.Base.Color();
                        break;

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
                    #endregion

                    default:
                        break;
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
