using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace ZdfFlatUI
{
    public class CascaderBoxItem : HeaderedItemsControl
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        #endregion

        #region Constructors
        static CascaderBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CascaderBoxItem), new FrameworkPropertyMetadata(typeof(CascaderBoxItem)));
        }

        public CascaderBoxItem()
        {
            
        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //var a = this.ItemsSource;
        }
        #endregion

        #region Private方法
        ItemsControl ParentItemsControl
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this);
            }
        }
        #endregion
    }
}
