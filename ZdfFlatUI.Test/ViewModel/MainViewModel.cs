using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ZdfFlatUI.Test.Model;

namespace ZdfFlatUI.Test.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region 界面绑定属性

        #region TagTextBox
        private ObservableCollection<string> tagList;

        public ObservableCollection<string> TagList
        {
            get { return tagList; }
            set { tagList = value; RaisePropertyChanged("TagList"); }
        }

        private string tagTextBoxContent;

        public string TagTextBoxContent
        {
            get { return tagTextBoxContent; }
            set { tagTextBoxContent = value; RaisePropertyChanged("TagTextBoxContent"); }
        }

        #endregion

        #endregion

        #region 界面绑定命令

        #region TagTextBox
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

        private DelegateCommand removeCommand;
        /// <summary>
        /// 选择应急联动席位命令
        /// </summary>
        public DelegateCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new DelegateCommand { ExecuteCommand = new Action<object>(OnRemoveCommand) });
            }
        }

        private void OnAddCommand(object obj)
        {
            string content = Convert.ToString(obj);
            if (!this.TagList.Contains(content) && !string.IsNullOrEmpty(content))
            {
                this.TagList.Add(content);
            }
        }

        private void OnRemoveCommand(object obj)
        {
            if (this.TagList.Count > 0)
            {
                this.TagList.RemoveAt(Convert.ToInt32(obj));
            }
        }
        #endregion

        #endregion

        public MainViewModel()
        {
            this.InitTagTextBox();
        }

        private void InitTagTextBox()
        {
            TagList = new ObservableCollection<string>();
            //TagList.Add(new Student() { Name = "之大风", Age = 1 });
            //TagList.Add(new Student() { Name = "zhidanfeng", Age = 1 });
            //TagList.Add(new Student() { Name = "zhidanfeng", Age = 1 });
            TagList.Add("sdfdsfadfdasfadsfdsafsdafadsfsdafdsafdasfdsafdsafdsafdsafdsafdsafdasfsadfdsafdsafdsafdas");
            TagList.Add("sdfdsfadfdasfadsfdsafsdafadsfsdafdsafdasfdsafdsafdsafdsafdsafdsafdasfsadfdsafdsafdsafdas");
            TagList.Add("sdfdsfadfdasfadsfdsafsdafadsfsdafdsafdasfdsafdsafdsafdsafdsafdsafdasfsadfdsafdsafdsafdas");
            TagList.Add("sdfdsfadfdasfadsfdsafsdafadsfsdafdsafdasfdsafdsafdsafdsafdsafdsafdasfsadfdsafdsafdsafdas");
        }
    }
}
