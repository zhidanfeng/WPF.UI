using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZdfFlatUI.Test.Model
{
    public class Dept
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private ObservableCollection<Dept> _Children;

        public ObservableCollection<Dept> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }
    }
}
