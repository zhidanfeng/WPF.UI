using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class BusyIndicatorAdorner : Adorner
    {
        #region 私有属性
        private VisualCollection _visuals;
        private BusyIndicator busyIndicator;
        #endregion

        #region 附加属性

        #region IsOpen 
        public static bool GetIsOpen(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(BusyIndicatorAdorner)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, BusyIndicatorAdorner.IsOpenCallback, CoerceIsOpen, true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

        private static void IsOpenCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BusyIndicatorAdorner adorner = Utils.UIElementEx.GetAdorner<BusyIndicatorAdorner>(d);
            if(adorner == null)
            {
                return;
            }
        }

        private static object CoerceIsOpen(DependencyObject d, object baseValue)
        {
            if (baseValue == null)
                return false;
            return baseValue;
        }
        #endregion

        #endregion

        public BusyIndicatorAdorner(UIElement adornedElement) : base(adornedElement)
        {

        }
    }
}
