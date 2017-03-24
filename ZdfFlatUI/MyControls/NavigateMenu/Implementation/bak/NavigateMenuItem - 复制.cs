using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ZdfFlatUI.bak
{
    public class NavigateMenuItem : ContentControl
    {
        #region Private属性
        private NavigateMenu ParentListBox
        {
            get
            {
                return this.ParentSelector as NavigateMenu;
            }
        }

        private Selector ParentSelector
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this) as Selector;
            }
        }
        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty IsSelectedProperty;
        #endregion

        #region 依赖属性set get
        [Bindable(true), Category("Appearance")]
        public bool IsSelected
        {
            get
            {
                return (bool)base.GetValue(NavigateMenuItem.IsSelectedProperty);
            }
            set
            {
                base.SetValue(NavigateMenuItem.IsSelectedProperty, value);
            }
        }
        #endregion

        #region Constructors
        static NavigateMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigateMenuItem), new FrameworkPropertyMetadata(typeof(NavigateMenuItem)));
            NavigateMenuItem.IsSelectedProperty = Selector.IsSelectedProperty.AddOwner(typeof(NavigateMenuItem), new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, 
                new PropertyChangedCallback(NavigateMenuItem.OnIsSelectedChanged)));
            UIElement.IsEnabledProperty.OverrideMetadata(typeof(NavigateMenuItem), new UIPropertyMetadata(true));
        }

        #endregion

        #region Override方法
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            VisualStateManager.GoToState(this, "MouseOver", true);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                this.HandleMouseButtonDown(MouseButton.Left);
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                this.HandleMouseButtonDown(MouseButton.Right);
            }
            base.OnMouseRightButtonDown(e);
        }
        #endregion

        #region Private方法
        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private void HandleMouseButtonDown(MouseButton mouseButton)
        {
            if (NavigateMenuItem.UiGetIsSelectable(this) && base.Focus())
            {
                NavigateMenu parentListBox = this.ParentListBox;
                if (parentListBox != null)
                {
                    parentListBox.NotifyListItemClicked(this, mouseButton);
                }
            }
        }

        private static bool ItemGetIsSelectable(object item)
        {
            return item != null && !(item is Separator);
        }

        private static bool UiGetIsSelectable(DependencyObject o)
        {
            if (o != null)
            {
                if (!NavigateMenuItem.ItemGetIsSelectable(o))
                {
                    return false;
                }
                ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(o);
                if (itemsControl != null)
                {
                    object obj = itemsControl.ItemContainerGenerator.ItemFromContainer(o);
                    return obj == o || NavigateMenuItem.ItemGetIsSelectable(obj);
                }
            }
            return false;
        }
        #endregion
    }
}
