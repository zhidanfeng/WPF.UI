using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ZdfFlatUI.Utils
{
    public class MyVisualTreeHelper
    {
        #region 元素查找
        /// <summary>
        /// 查找指定名称元素的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <param name="childName">元素名称</param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj, string childName) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    var frameworkElement = child as FrameworkElement;
                    if (child != null && child is T && frameworkElement.Name.Equals(childName))
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child, childName))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// 查找指定名称的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                T childType = child as T;
                if (childType == null)
                {
                    // 住下查要找的元素
                    foundChild = FindChild<T>(child, childName);

                    // 如果找不到就反回
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // 看名字是不是一样
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        //如果名字一样返回
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // 找到相应的元素了就返回 
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        #endregion
    }
}
