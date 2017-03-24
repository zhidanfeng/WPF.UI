using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    /// <summary>
    /// 级联选择控件
    /// </summary>
    public class CascaderBox : ItemsControl
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        #endregion

        #region Constructors
        static CascaderBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CascaderBox), new FrameworkPropertyMetadata(typeof(CascaderBox)));
        }

        public CascaderBox()
        {
            
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CascaderBoxItem();
        }

        public override void OnApplyTemplate()
        {
            List<object> list = new List<object>();
            base.OnApplyTemplate();
            //IEnumerator enumerator = this.ItemsSource.GetEnumerator();
            //while(enumerator.MoveNext())
            //{
            //    object obj = enumerator.Current;
            //    list.Add(obj);
            //}

        }
        #endregion

        #region Private方法

        #endregion
    }
}
