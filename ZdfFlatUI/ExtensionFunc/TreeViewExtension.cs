using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI.ExtensionFunc
{
    public static class TreeViewExtension
    {
        public static int GetDepth(this TreeViewItem item)
        {
            int depth = 0;
            while ((item = item.GetAncestor<TreeViewItem>()) != null)
            {
                depth++;
            }
            return depth;
        }

        public static T GetAncestor<T>(this DependencyObject source) where T : DependencyObject
        {
            do
            {
                source = VisualTreeHelper.GetParent(source);

            } while (source != null && !(source is T));

            return source as T;
        }
    }
}
