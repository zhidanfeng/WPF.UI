using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class FlatRadionButton : RadioButton
    {
        static FlatRadionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatRadionButton), new FrameworkPropertyMetadata(typeof(FlatRadionButton)));
        }
    }
}
