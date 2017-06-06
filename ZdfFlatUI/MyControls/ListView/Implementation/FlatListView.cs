using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class FlatListView : ListView
    {
        #region Constructors

        static FlatListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatListView), new FrameworkPropertyMetadata(typeof(FlatListView)));
        }

        #endregion
    }
}
