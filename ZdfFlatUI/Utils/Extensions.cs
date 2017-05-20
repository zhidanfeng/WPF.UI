using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public static class Extensions
    {
        public static TChildItem FindVisualChild<TChildItem>(this DependencyObject dependencyObject, String name) where TChildItem : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);


            if (childrenCount == 0 && dependencyObject is Popup)
            {
                var popup = dependencyObject as Popup;
                return popup.Child != null ? popup.Child.FindVisualChild<TChildItem>(name) : null;
            }

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);
                var nameOfChild = child.GetValue(FrameworkElement.NameProperty) as String;

                if (child is TChildItem && (name == String.Empty || name == nameOfChild))
                    return (TChildItem)child;
                var childOfChild = child.FindVisualChild<TChildItem>(name);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }

        public static TChildItem FindVisualChild<TChildItem>(this DependencyObject dependencyObject) where TChildItem : DependencyObject
        {
            return dependencyObject.FindVisualChild<TChildItem>(String.Empty);
        }
    }
}
