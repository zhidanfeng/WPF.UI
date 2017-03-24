using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ZToolTip : ToolTip
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty PlacementExProperty = DependencyProperty.Register("PlacementEx"
            , typeof(EnumPlacement), typeof(ZToolTip), new PropertyMetadata(EnumPlacement.TopLeft));
        public static readonly DependencyProperty IsShowShadowProperty = DependencyProperty.Register("IsShowShadow"
            , typeof(bool), typeof(ZToolTip), new PropertyMetadata(true));
        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public EnumPlacement PlacementEx
        {
            get { return (EnumPlacement)GetValue(PlacementExProperty); }
            set { SetValue(PlacementExProperty, value); }
        }

        /// <summary>
        /// 是否显示阴影
        /// </summary>
        public bool IsShowShadow
        {
            get { return (bool)GetValue(IsShowShadowProperty); }
            set { SetValue(IsShowShadowProperty, value); }
        }
        #endregion

        #region Constructors
        static ZToolTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZToolTip), new FrameworkPropertyMetadata(typeof(ZToolTip)));
        }
        #endregion

        #region Override方法
        public ZToolTip()
        {
            
        }
        #endregion

        #region Private方法

        #endregion
    }
}
