using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ZTreeView : TreeView
    {
        #region Private属性
        private CheckBox PART_CheckBox;
        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty IsShowCheckBoxProperty = DependencyProperty.Register("IsShowCheckBox"
            , typeof(bool), typeof(ZTreeView), new PropertyMetadata(false));

        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 是否显示CheckBox
        /// </summary>
        public bool IsShowCheckBox
        {
            get { return (bool)GetValue(IsShowCheckBoxProperty); }
            set { SetValue(IsShowCheckBoxProperty, value); }
        }
        #endregion

        #region Constructors
        static ZTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZTreeView), new FrameworkPropertyMetadata(typeof(ZTreeView)));
        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        #endregion

        #region Private方法

        #endregion
    }
}
