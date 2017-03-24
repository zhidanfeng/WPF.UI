using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using ZdfFlatUI.Test.DTO;

namespace ZdfFlatUI.Test.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        #region 单例
        private static HomeViewModel instance;

        public static HomeViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HomeViewModel();
                }
                return instance;
            }
        }
        #endregion

        private ObservableCollection<AttachmentDTO> _UploadFileList;

        public ObservableCollection<AttachmentDTO> UploadFileList
        {
            get { return _UploadFileList; }
            set { _UploadFileList = value; RaisePropertyChanged("UploadFileList"); }
        }

        public HomeViewModel()
        {
            this.UploadFileList = new ObservableCollection<AttachmentDTO>();

            this.UploadFileList.Add(new AttachmentDTO()
            {
                ID = "1",
                FJMC = "受理席",
                FJLX = "zip",
            });
        }

        #region 命令
        private RelayCommand<object> _FileUploadCommand;
        public RelayCommand<object> FileUploadCommand
        {
            get
            {
                return _FileUploadCommand ?? (new RelayCommand<object>(HandleFileUpload));
            }

            set
            {
                _FileUploadCommand = value;
            }
        }
        #endregion

        #region 命令执行方法
        private void HandleFileUpload(object param)
        {
            Array files = param as Array;

            for (int i = 0; i < files.Length; i++)
            {
                string filePath = files.GetValue(i).ToString();
                FileInfo fileInfo = new FileInfo(filePath);

                this.UploadFileList.Add(new AttachmentDTO()
                {
                    ID = "1",
                    FJMC = Path.GetFileName(filePath),
                    FJLX = Path.GetExtension(filePath),
                });
            }
        }
        #endregion
    }
}
