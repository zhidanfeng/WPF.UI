using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace ZdfFlatUI
{
    public static class VisualService
    {
        public static double GetDpiFactor(this Visual target)
        {
            var source = PresentationSource.FromVisual(target);
            return source == null ? 1.0 : 1 / source.CompositionTarget.TransformToDevice.M11;
        }

        public static T GetAncestor<T>(this DependencyObject target)
            where T : DependencyObject
        {            
            if(target == null)
            {
                return null;
            }

            var parent = VisualTreeHelper.GetParent(target);
            if (parent is T)
                return (T)parent;

            if (parent != null)
                return parent.GetAncestor<T>();

            return null;
        }

        public static T GetDesendentChild<T>(this DependencyObject target)
            where T : DependencyObject
        {
            var childCount = VisualTreeHelper.GetChildrenCount(target);
            if (childCount == 0) return null;

            for (int i = 0; i < childCount; i++)
            {
                var current = VisualTreeHelper.GetChild(target, i);
                if (current is T)
                    return (T)current;

                var desendent = current.GetDesendentChild<T>();
                if (desendent != null)
                    return desendent;
            }
            return null;
        }
    }
}
