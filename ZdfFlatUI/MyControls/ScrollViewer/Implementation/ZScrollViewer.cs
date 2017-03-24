using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ZScrollViewer : ScrollViewer
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #endregion

        #region 依赖属性set get

        #endregion

        #region Constructors
        static ZScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZScrollViewer), new FrameworkPropertyMetadata(typeof(ZScrollViewer)));
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
