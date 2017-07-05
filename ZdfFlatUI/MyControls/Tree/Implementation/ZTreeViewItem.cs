using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public enum EnumTreeNodeType
    {
        /// <summary>
        /// 根节点
        /// </summary>
        RootNode,
        /// <summary>
        /// 中间节点。有父节点有子节点
        /// </summary>
        MiddleNode,
        /// <summary>
        /// 叶子节点
        /// </summary>
        LeafNode,
    }
    public class ZTreeViewItem : TreeViewItem
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region TreeNodeType

        public EnumTreeNodeType TreeNodeType
        {
            get { return (EnumTreeNodeType)GetValue(TreeNodeTypeProperty); }
            set { SetValue(TreeNodeTypeProperty, value); }
        }
        
        public static readonly DependencyProperty TreeNodeTypeProperty =
            DependencyProperty.Register("TreeNodeType", typeof(EnumTreeNodeType), typeof(ZTreeViewItem), new PropertyMetadata(EnumTreeNodeType.RootNode));

        #endregion

        #endregion

        #region Constructors

        static ZTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZTreeViewItem), new FrameworkPropertyMetadata(typeof(ZTreeViewItem)));
        }

        #endregion

        #region Override

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            ZTreeViewItem treeViewItem = element as ZTreeViewItem;

            if (treeViewItem.HasItems)
            {
                treeViewItem.TreeNodeType = EnumTreeNodeType.MiddleNode;
            }
            else
            {
                treeViewItem.TreeNodeType = EnumTreeNodeType.LeafNode;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ZTreeViewItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        #endregion
        
    }
}
