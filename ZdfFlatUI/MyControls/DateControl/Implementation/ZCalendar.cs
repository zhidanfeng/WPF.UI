using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public enum EnumCalendarType
    {
        One,
        Second,
    }

    public class ZCalendar : Calendar
    {
        #region Private属性
        private CalendarItem PART_CalendarItem;
        #endregion

        #region 依赖属性定义
        
        #endregion

        #region 依赖属性set get

        public EnumCalendarType Type
        {
            get { return (EnumCalendarType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(EnumCalendarType), typeof(ZCalendar), new PropertyMetadata(EnumCalendarType.One));

        #endregion

        #region Constructors
        static ZCalendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZCalendar), new FrameworkPropertyMetadata(typeof(ZCalendar)));
        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_CalendarItem = this.GetTemplateChild("PART_CalendarItem") as CalendarItem;
            if(this.PART_CalendarItem != null)
            {
                this.PART_CalendarItem.AddHandler(Button.MouseLeftButtonDownEvent, new RoutedEventHandler(DayButton_MouseLeftButtonUp), true);
            }

            //Calendar有个问题，当选中一个日期之后，似乎焦点并没有得到释放，当鼠标移动其他位置时，需要先点击一下鼠标
            //然后鼠标对应的部分才能获取到焦点，为了解决这个问题，作此处理
            this.PreviewMouseUp += ZCalendar_PreviewMouseUp;
        }

        private void ZCalendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void DayButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("点击了按钮");
            if(sender is CalendarItem)
            {
                Point position = Mouse.GetPosition(e.Source as FrameworkElement);
                HitTestResult result = VisualTreeHelper.HitTest(this.PART_CalendarItem, position);
                if(result == null)
                {
                    return;
                }
                PathIconButton preYearButton = Utils.VisualHelper.FindParent<PathIconButton>(result.VisualHit, "PART_PreviousYearButton");
                if(preYearButton != null)
                {
                    switch (this.DisplayMode)
                    {
                        case CalendarMode.Month:
                        case CalendarMode.Year:
                            this.DisplayDate = this.DisplayDate.AddYears(-1);
                            this.SelectedDate = this.DisplayDate;
                            break;
                        case CalendarMode.Decade:
                            this.DisplayDate = this.DisplayDate.AddYears(-10);
                            break;
                        default:
                            break;
                    }
                }
                PathIconButton nextYearButton = Utils.VisualHelper.FindParent<PathIconButton>(result.VisualHit, "PART_NextYearButton");
                if(nextYearButton != null)
                {
                    switch (this.DisplayMode)
                    {
                        case CalendarMode.Month:
                        case CalendarMode.Year:
                            this.DisplayDate = this.DisplayDate.AddYears(1);
                            this.SelectedDate = this.DisplayDate;
                            break;
                        case CalendarMode.Decade:
                            this.DisplayDate = this.DisplayDate.AddYears(10);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region Private方法

        #endregion
    }
}
