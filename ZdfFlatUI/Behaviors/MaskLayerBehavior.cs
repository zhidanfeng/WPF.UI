using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ZdfFlatUI.Behaviors
{
    public class MaskLayerBehavior
    {
        #region Owner
        public static UIElement GetOwner(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(OwnerProperty);
        }

        public static void SetOwner(DependencyObject obj, UIElement value)
        {
            obj.SetValue(OwnerProperty, value);
        }

        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.RegisterAttached("Owner", typeof(UIElement), typeof(MaskLayerBehavior), new PropertyMetadata(null, OwnerChangedCallback));

        private static void OwnerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
        #endregion

        public static bool GetIsOpen(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenProperty, value);
        }
        
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(MaskLayerBehavior), new PropertyMetadata(false, IsOpenChangedCallback));

        private static void IsOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool isOpen = (bool)e.NewValue;
            ContentControl owner = MaskLayerBehavior.GetOwner(d) as ContentControl;
            MaskLayer layerContent = d as MaskLayer;

            if (owner != null)
            {
                if((bool)e.NewValue)
                {
                    //蒙板
                    Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(160, 0, 0, 0)) };

                    layer.PreviewMouseLeftButtonUp += delegate
                    {
                        MaskLayerBehavior.SetIsOpen(d, false);
                    };

                    //父级窗体原来的内容
                    UIElement original = owner.Content as UIElement;
                    owner.Content = null;

                    //容器Grid
                    Grid container = new Grid();
                    container.Children.Add(original);//放入原来的内容
                    container.Children.Add(layer);//在上面放一层蒙板，将装有原来内容和蒙板的容器赋给父级窗体
                    owner.Content = container;

                    layerContent.AllowsTransparency = true;
                    layerContent.StaysOpen = true;
                    layerContent.SetValue(PopopHelper.PopupPlacementTargetProperty, owner);
                    layerContent.PlacementTarget = owner;
                    layerContent.Placement = PlacementMode.Center;
                    layerContent.IsOpen = true;
                }
                else
                {
                    //容器Grid
                    Grid grid = owner.Content as Grid;
                    //父级窗体原来的内容
                    UIElement original = VisualTreeHelper.GetChild(grid, 0) as UIElement;
                    //将父级窗体原来的内容在容器Grid中移除
                    grid.Children.Remove(original);
                    //赋给父级窗体
                    owner.Content = original;
                }
            }
        }
    }
}
