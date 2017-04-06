using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class DropdownButtonItem : ContentControl
    {
        #region Private属性
        private UIElement PART_Root;
        #endregion

        private DropDownButton ParentListBox
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this) as DropDownButton;
            }
        }

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        #endregion

        #region Constructors
        static DropdownButtonItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownButtonItem), new FrameworkPropertyMetadata(typeof(DropdownButtonItem)));
        }

        public DropdownButtonItem()
        {
            this.MouseEnter += DropdownButtonItem_MouseEnter;
            this.MouseLeave += DropdownButtonItem_MouseLeave;
            this.MouseLeftButtonUp += DropdownButtonItem_MouseLeftButtonUp;
        }

        private void DropdownButtonItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DropdownButtonItem item = sender as DropdownButtonItem;
            this.ParentListBox.OnItemClick(item, item);
            this.ParentListBox.IsDropDownOpen = false;
            e.Handled = true;
        }

        private void DropdownButtonItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);
        }

        private void DropdownButtonItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(base.IsMouseOver)
            {

            }
            VisualStateManager.GoToState(this, "MouseOver", false);
        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //this.PART_Root = this.GetTemplateChild("PART_Root") as UIElement;
            //if(this.PART_Root != null)
            //{
            //    this.PART_Root.MouseEnter += DropdownButtonItem_MouseEnter;
            //    this.PART_Root.MouseLeave += DropdownButtonItem_MouseLeave;
            //    this.PART_Root.MouseLeftButtonUp += DropdownButtonItem_MouseLeftButtonUp;
            //}
        }
        #endregion

        #region Private方法

        #endregion
    }
}
