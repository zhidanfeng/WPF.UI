using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace ZdfFlatUI
{
    public class ComboTree : ItemsControl
    {
        #region private fields
        private TreeView PART_TreeView;
        private Popup PART_Popup;
        private List<object> selectedList = new List<object>();
        #endregion

        #region DependencyProperty

        #region MaxDropDownHeight

        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }
        
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(ComboTree), new PropertyMetadata(300d));

        #endregion

        #region Content

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ComboTree), new PropertyMetadata(null));

        #endregion

        #region SelectedItem

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboTree), new PropertyMetadata(null));

        #endregion

        #region SelectedValue

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(ComboTree));

        #endregion

        #region SelectedValuePath

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(ComboTree), new PropertyMetadata(string.Empty));

        #endregion

        #region IsDropDownOpen

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(ComboTree), new PropertyMetadata(false));

        #endregion

        #region DisplayMemberPath

        /// <summary>
        /// 摘要:
        ///     获取或设置源对象上某个值的路径，该值作为对象的可视化表示形式。
        ///
        /// 返回结果:
        ///     源对象上的值的路径。这可以是任何路径，或 XPath（如“@Name”）。默认值为空字符串 ("")。
        ///     
        /// 使用了new隐藏基类的该依赖属性是为了可以同时使用ItemTemplate和DisplayMemberPath
        /// </summary>
        [Bindable(true)]
        public new string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }
        
        public static readonly new DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(ComboTree), new PropertyMetadata(string.Empty));

        #endregion

        #region IsCloseWhenSelected

        public bool IsCloseWhenSelected
        {
            get { return (bool)GetValue(IsCloseWhenSelectedProperty); }
            set { SetValue(IsCloseWhenSelectedProperty, value); }
        }
        
        public static readonly DependencyProperty IsCloseWhenSelectedProperty =
            DependencyProperty.Register("IsCloseWhenSelected", typeof(bool), typeof(ComboTree), new PropertyMetadata(true));

        #endregion

        #endregion

        #region Constructors

        static ComboTree()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboTree), new FrameworkPropertyMetadata(typeof(ComboTree)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_TreeView = this.GetTemplateChild("PART_TreeView") as TreeView;
            this.PART_Popup = this.GetTemplateChild("PART_Popup") as Popup;
            if(this.PART_Popup != null)
            {
                this.PART_Popup.Opened += PART_Popup_Opened;
            }
            this.AddHandler(TreeViewItem.MouseDoubleClickEvent, new RoutedEventHandler(TreeNode_MouseDoubleClick), true);
        }
        
        #endregion

        #region private function

        /// <summary>
        /// 反射获取指定值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private object GetPropertyValue(object obj, string path)
        {
            Type type = obj.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(path);
            return propertyInfo.GetValue(obj, null);
        }

        private void SetNodeSelected(System.Windows.Controls.ItemsControl targetItemContainer)
        {
            if (targetItemContainer == null) return;
            if (targetItemContainer.Items == null) return;
            foreach (var item in targetItemContainer.Items)
            {
                TreeViewItem treeItem = targetItemContainer.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem == null) continue;

                if (this.SelectedValue.Equals(this.GetPropertyValue(item, this.SelectedValuePath)))
                {
                    treeItem.IsExpanded = true;
                    treeItem.IsSelected = true;
                }
                else
                {
                    SetNodeSelected(treeItem);
                }
            }
        }

        private TreeViewItem GetNode(System.Windows.Controls.ItemsControl targetItemContainer)
        {
            if (targetItemContainer == null) return null;
            if (targetItemContainer.Items == null) return null;
            foreach (var item in targetItemContainer.Items)
            {
                TreeViewItem treeItem = targetItemContainer.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem == null) continue;

                if (this.SelectedItem == item)
                {
                    return treeItem;
                }
                else
                {
                    SetNodeSelected(treeItem);
                }
            }

            return null;
        }

        #endregion

        #region Event Implement Function

        /// <summary>
        /// 树节点鼠标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeNode_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.PART_TreeView == null)
            {
                return;
            }

            if (this.PART_TreeView.SelectedItem == null)
            {
                return;
            }

            this.SelectedItem = this.PART_TreeView.SelectedItem;

            TreeViewItem treeViewItem = this.GetNode(this.PART_TreeView);
            this.SetSelected(treeViewItem);


            //根据参数设置当选择树节点后是否自动关闭Popup
            this.IsDropDownOpen = !this.IsCloseWhenSelected;

            this.Content = string.IsNullOrEmpty(this.DisplayMemberPath) ? this.SelectedItem : this.GetPropertyValue(this.SelectedItem, this.DisplayMemberPath);
            this.SelectedValue = string.IsNullOrEmpty(this.SelectedValuePath) ? string.Empty : this.GetPropertyValue(this.SelectedItem, this.SelectedValuePath);
        }

        private void SetSelected(TreeViewItem item)
        {
            while ((item = item.GetAncestor<TreeViewItem>()) != null)
            {
                this.selectedList.Insert(0, item.DataContext);
            }
        }

        private void PART_Popup_Opened(object sender, EventArgs e)
        {
            if (this.SelectedValue != null && !string.IsNullOrEmpty(this.SelectedValuePath))
            {
                this.SetNodeSelected(this.PART_TreeView);
            }
        }
        #endregion
    }
}
