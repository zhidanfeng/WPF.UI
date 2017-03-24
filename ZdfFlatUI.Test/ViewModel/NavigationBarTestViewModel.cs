using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZdfFlatUI.Test.Model;

namespace ZdfFlatUI.Test.ViewModel
{
    public class NavigationBarTestViewModel : ViewModelBase
    {
        private ObservableCollection<AnchorInfo> _AnchorList;

        public ObservableCollection<AnchorInfo> AnchorList
        {
            get { return _AnchorList; }
            set { _AnchorList = value; RaisePropertyChanged("AnchorList"); }
        }

        #region Commands
        private DelegateCommand addCommand;
        /// <summary>
        /// 选择应急联动席位命令
        /// </summary>
        public DelegateCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new DelegateCommand { ExecuteCommand = new Action<object>(OnAddCommand) });
            }
        }

        

        private void OnAddCommand(object obj)
        {
            AnchorList = new ObservableCollection<AnchorInfo>();
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息1" });
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息2来电" });
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息3" });
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息2来电" });
        }
        #endregion

        public NavigationBarTestViewModel()
        {
            AnchorList = new ObservableCollection<AnchorInfo>();
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息1" });
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息2来电来电信息2来电" });
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息3" });
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息3" });
            AnchorList.Add(new AnchorInfo() { DisplayTitle = "来电信息3" });
        }
    }
}
