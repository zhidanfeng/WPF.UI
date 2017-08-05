using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTButtonTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTButtonTest : UserControl
    {
        DataTable dt = new DataTable();

        public UCTButtonTest()
        {
            InitializeComponent();

            List<string> list = new List<string>();
            list.Add("全部");
            list.Add("主任医师");
            list.Add("副主任医师");
            list.Add("住院医生");
            list.Add("其他");


            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            DataRow dr = dt.NewRow();
            dr["ID"] = "1";
            dr["NAME"] = "zhi";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "2";
            dr["NAME"] = "dan";
            dt.Rows.Add(dr);
        }

        private void FlatButton_Click(object sender, RoutedEventArgs e)
        {
            //ZMessageBox messageBox = new ZMessageBox();
            //messageBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ////messageBox.ShowDialog();
            //ZdfFlatUI.Utils.DialogHelper.ShowDialog(messageBox, this);
            //if(ZMessageBox.Show("") == MessageBoxResult.OK)
            //{

            //}
            ZWindowTest window = new ZWindowTest();
            window.Title = "测试窗口";
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            //window.WindowState = WindowState.Maximized;
        }

        private void FlatButton_Click_1(object sender, RoutedEventArgs e)
        {
            BaseWindowTest window = new BaseWindowTest();
            //window.CloseButtonType = CloseBoxTypeEnum.Close;
            ZdfFlatUI.Utils.DialogHelper.ShowDialog(window, this);
        }

        private void Upload_FileUpload(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Array files = (System.Array)e.NewValue;
        }
    }
}
