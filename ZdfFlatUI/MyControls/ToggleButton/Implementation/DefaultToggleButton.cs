using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    public class DefaultToggleButton : ToggleButton
    {
        static DefaultToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DefaultToggleButton), new FrameworkPropertyMetadata(typeof(DefaultToggleButton)));
        }
    }
}
