using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ZTabControl : TabControl
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty TypeProperty;
        #endregion

        #region 依赖属性set get
        public EnumTabControlType Type
        {
            get { return (EnumTabControlType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(ZTabControl));


        #endregion

        #region Constructors
        static ZTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZTabControl), new FrameworkPropertyMetadata(typeof(ZTabControl)));
            ZTabControl.TypeProperty = DependencyProperty.Register("Type", typeof(EnumTabControlType), typeof(ZTabControl), new PropertyMetadata(EnumTabControlType.Line));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TabItem();
        }
        #endregion

        #region Private方法

        #endregion
    }
}
