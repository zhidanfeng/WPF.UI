using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class DefaultRadionButton : RadioButton
    {
        static DefaultRadionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DefaultRadionButton), new FrameworkPropertyMetadata(typeof(DefaultRadionButton)));
        }
    }
}
