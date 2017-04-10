using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class FlyoutControl : ItemsControl
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        #endregion

        #region Constructors
        static FlyoutControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlyoutControl), new FrameworkPropertyMetadata(typeof(FlyoutControl)));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new Flyout();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is Flyout;
        }
        #endregion

        #region Private方法

        #endregion
    }
}
