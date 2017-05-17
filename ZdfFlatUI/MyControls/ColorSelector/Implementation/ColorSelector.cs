using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace ZdfFlatUI
{
    /// <summary>
    /// 颜色选择器
    /// </summary>
    public class ColorSelector : Selector
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #endregion

        #region Constructors

        static ColorSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSelector), new FrameworkPropertyMetadata(typeof(ColorSelector)));
        }

        #endregion

        #region Override

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            
            if(!(item is ColorItem))
            {
                ColorItem colorItem = element as ColorItem;
                if (!string.IsNullOrEmpty(this.DisplayMemberPath))
                {
                    Binding binding = new Binding(this.DisplayMemberPath);
                    colorItem.SetBinding(ColorItem.BackgroundProperty, binding);
                }
                else
                {
                    Color color;
                    try
                    {
                        color = (Color)ColorConverter.ConvertFromString(Convert.ToString(item));
                    }
                    catch (Exception ex)
                    {
                        color = Color.FromRgb(255, 255, 255);
                    }
                    colorItem.SetValue(ColorItem.ColorProperty, new SolidColorBrush(color));
                }
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ColorItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region private function

        public void SetItemSelected(ColorItem selectedItem)
        {
            if(this.Items == null)
            {
                return;
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                ColorItem colorItem = this.ItemContainerGenerator.ContainerFromIndex(i) as ColorItem;
                if (colorItem == selectedItem)
                {
                    colorItem.SetCurrentValue(ColorItem.IsSelectedProperty, true);
                }
                else
                {
                    colorItem.SetCurrentValue(ColorItem.IsSelectedProperty, false);
                }
            }
        }

        #endregion

        #region Event Implement Function

        #endregion
    }
}
