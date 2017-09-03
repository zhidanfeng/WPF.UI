using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ValidateBehavior
    {
        public static bool GetStartValidate(DependencyObject obj)
        {
            return (bool)obj.GetValue(StartValidateProperty);
        }

        public static void SetStartValidate(DependencyObject obj, bool value)
        {
            obj.SetValue(StartValidateProperty, value);
        }
        
        public static readonly DependencyProperty StartValidateProperty =
            DependencyProperty.RegisterAttached("StartValidate", typeof(bool), typeof(ValidateBehavior), new PropertyMetadata(false, OnStartValidateChanged));

        private static void OnStartValidateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Panel panel = d as Panel;
            bool flag = false;
            if(bool.TryParse(Convert.ToString(e.NewValue), out flag))
            {
                List<NumbericTextBox> list = Utils.VisualHelper.FindVisualChildrenEx<NumbericTextBox>(panel);
                foreach (NumbericTextBox textBox in list)
                {
                    //textBox.IsStartValidate = false;
                    textBox.IsStartValidate = true;
                }
            }
        }
    }
}
