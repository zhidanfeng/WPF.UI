using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace ZdfFlatUI
{
    /// <summary>
    /// Popup帮助类，解决Popup设置StayOpen="True"时，移动窗体或者改变窗体大小时，Popup不随窗体移动的问题
    /// </summary>
    public class PopopHelper
    {
        #region PopupPlacementTarget
        public static DependencyObject GetPopupPlacementTarget(DependencyObject obj)
        {
            return (DependencyObject)obj.GetValue(PopupPlacementTargetProperty);
        }

        public static void SetPopupPlacementTarget(DependencyObject obj, DependencyObject value)
        {
            obj.SetValue(PopupPlacementTargetProperty, value);
        }

        public static readonly DependencyProperty PopupPlacementTargetProperty =
            DependencyProperty.RegisterAttached("PopupPlacementTarget", typeof(DependencyObject), typeof(PopopHelper), new PropertyMetadata(null, OnPopupPlacementTargetChanged));

        private static void OnPopupPlacementTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DependencyObject popupPopupPlacementTarget = e.NewValue as DependencyObject;
                Popup pop = d as Popup;

                Window w = Window.GetWindow(popupPopupPlacementTarget);
                if (null != w)
                {
                    //让Popup随着窗体的移动而移动
                    w.LocationChanged += delegate
                    {
                        UpdatePosition(pop);
                    };

                    //让Popup随着窗体的Size改变而移动位置
                    w.SizeChanged += delegate
                    {
                        UpdatePosition(pop);
                    };
                }
            }
        }
        #endregion

        private static void UpdatePosition(Popup pop)
        {
            if(pop == null)
            {
                return;
            }

            var offset = pop.HorizontalOffset;
            pop.HorizontalOffset = offset + 1;
            pop.HorizontalOffset = offset;

            //MethodInfo mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //try
            //{
            //    if(mi != null)
            //    {
            //        mi.Invoke(pop, null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
        }
    }
}
