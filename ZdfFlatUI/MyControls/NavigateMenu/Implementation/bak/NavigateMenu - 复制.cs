using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ZdfFlatUI.bak
{
    public class NavigateMenu : Selector
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        #endregion

        #region Constructors
        static NavigateMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigateMenu), new FrameworkPropertyMetadata(typeof(NavigateMenu)));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NavigateMenuItem();
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }
        #endregion

        #region Private方法

        #endregion

        #region Public方法
        public void NotifyListItemClicked(NavigateMenuItem item, MouseButton mouseButton)
        {
            if (mouseButton == MouseButton.Left && Mouse.Captured != this)
            {
                Mouse.Capture(this, CaptureMode.SubTree);
                //base.SetInitialMousePosition();
            }

            if (!item.IsSelected)
            {
                item.SetCurrentValue(Selector.IsSelectedProperty, true);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                item.SetCurrentValue(Selector.IsSelectedProperty, false);
            }
            //this.UpdateAnchorAndActionItem(base.ItemInfoFromContainer(item));
            return;

            //switch (this.SelectionMode)
            //{
            //    case SelectionMode.Single:
            //        if (!item.IsSelected)
            //        {
            //            item.SetCurrentValue(Selector.IsSelectedProperty, true);
            //        }
            //        else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            //        {
            //            item.SetCurrentValue(Selector.IsSelectedProperty, false);
            //        }
            //        //this.UpdateAnchorAndActionItem(base.ItemInfoFromContainer(item));
            //        return;
            //case SelectionMode.Multiple:
            //    this.MakeToggleSelection(item);
            //    return;
            //case SelectionMode.Extended:
            //    if (mouseButton != MouseButton.Left)
            //    {
            //        if (mouseButton == MouseButton.Right && (Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == ModifierKeys.None)
            //        {
            //            if (item.IsSelected)
            //            {
            //                this.UpdateAnchorAndActionItem(base.ItemInfoFromContainer(item));
            //                return;
            //            }
            //            this.MakeSingleSelection(item);
            //        }
            //        return;
            //    }
            //    if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
            //    {
            //        this.MakeAnchorSelection(item, false);
            //        return;
            //    }
            //    if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            //    {
            //        this.MakeToggleSelection(item);
            //        return;
            //    }
            //    if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            //    {
            //        this.MakeAnchorSelection(item, true);
            //        return;
            //    }
            //    this.MakeSingleSelection(item);
            //    return;
            //default:
            //    return;
            //}
        }
        #endregion
    }
}
