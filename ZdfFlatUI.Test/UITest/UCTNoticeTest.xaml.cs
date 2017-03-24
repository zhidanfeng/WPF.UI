using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ZdfFlatUI.Test.Model;

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTNoticeTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTNoticeTest : UserControl
    {
        private ObservableCollection<NoticeInfo> _NoticeList;

        public ObservableCollection<NoticeInfo> NoticeList
        {
            get { return _NoticeList; }
            set { _NoticeList = value; }
        }

        public UCTNoticeTest()
        {
            InitializeComponent();
            this.NoticeList = new ObservableCollection<NoticeInfo>();
            this.NoticeList.Add(new NoticeInfo()
            {
                Title = "这是通知标题1",
                Content = "这条通知不会自动关闭，需要点击关闭按钮才1",
                Type = "Info",
            });
            //this.NoticeList.Add(new NoticeInfo()
            //{
            //    Title = "这是通知标题2",
            //    Content = "这条通知不会自动关闭，需要点击关闭按钮才2",
            //    Type = "Success",
            //});
            //this.NoticeList.Add(new NoticeInfo()
            //{
            //    Title = "这是通知标题3",
            //    Content = "这条通知不会自动关闭，需要点击关闭按钮才3",
            //    Type = "Warn",
            //});
            //this.NoticeList.Add(new NoticeInfo()
            //{
            //    Title = "这是通知标题4",
            //    Content = "这条通知不会自动关闭，需要点击关闭按钮才4",
            //    Type = "Error",
            //});
            this.notice.ItemsSource = this.NoticeList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.NoticeList.Add(new NoticeInfo()
            {
                Title = "这是通知标题4",
                Content = "这条通知不会自动关闭",
                Type = "Error",
            });
        }

        Notifiaction notifiaction = new Notifiaction();

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            notifiaction.AddNotifiaction(new NotifiactionModel()
            {
                Title = "这是Info通知标题",
                Content = "这条通知不会自动关闭"
            });
        }

        private void btnSuccess_Click(object sender, RoutedEventArgs e)
        {
            notifiaction.AddNotifiaction(new NotifiactionModel()
            {
                Title = "这是Success通知标题",
                Content = "这条通知不会自动关闭，需要点击关闭按钮这条通知不会自动关闭，需要点击关闭按钮这条通知不会自动关闭，需要点击关闭按钮",
                NotifiactionType = EnumPromptType.Success
            });
        }

        private void btnError_Click(object sender, RoutedEventArgs e)
        {
            notifiaction.AddNotifiaction(new NotifiactionModel()
            {
                Title = "这是Error通知标题",
                Content = "这条通知不会自动关闭，需要点击关闭按钮",
                NotifiactionType = EnumPromptType.Error
            });
        }

        private void btnWarn_Click(object sender, RoutedEventArgs e)
        {
            notifiaction.AddNotifiaction(new NotifiactionModel()
            {
                Title = "这是Warn通知标题",
                Content = "这条通知不会自动关闭，需要点击关闭按钮",
                NotifiactionType = EnumPromptType.Warn
            });
        }
    }
}
