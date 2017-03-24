using GalaSoft.MvvmLight;
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
using ZdfFlatUI.Test.ViewModel;

namespace ZdfFlatUI.Test.UITest
{
    /// <summary>
    /// UCTUploadTest.xaml 的交互逻辑
    /// </summary>
    public partial class UCTUploadTest : UserControl
    {
        public UCTUploadTest()
        {
            InitializeComponent();

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                var vm = new Lazy<HomeViewModel>(() => HomeViewModel.Instance);
                this.DataContext = vm.Value;
            }
        }

        private void Upload_FileUpload(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            HomeViewModel vm = this.DataContext as HomeViewModel;
            vm.FileUploadCommand.Execute(e.NewValue);
        }
    }
}
