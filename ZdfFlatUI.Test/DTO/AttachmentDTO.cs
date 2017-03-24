using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdfFlatUI.Test.DTO
{
    public class AttachmentDTO : ViewModelBase
    {
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged("ID"); }
        }

        private string _ZDDWID;

        public string ZDDWID
        {
            get { return _ZDDWID; }
            set { _ZDDWID = value; RaisePropertyChanged("ZDDWID"); }
        }

        private string _ZDDWMC;

        public string ZDDWMC
        {
            get { return _ZDDWMC; }
            set { _ZDDWMC = value; RaisePropertyChanged("ZDDWMC"); }
        }

        private string _FJMC;

        public string FJMC
        {
            get { return _FJMC; }
            set { _FJMC = value; RaisePropertyChanged("FJMC"); }
        }


        private string _FJLX;

        public string FJLX
        {
            get { return _FJLX; }
            set { _FJLX = value; RaisePropertyChanged("FJLX"); }
        }
    }
}
