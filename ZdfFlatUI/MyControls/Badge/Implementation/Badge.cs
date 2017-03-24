using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    /// <summary>
    /// 右上角角标控件
    /// </summary>
    public class Badge : Control
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number"
            , typeof(int), typeof(Badge), new FrameworkPropertyMetadata(0));

        public static readonly DependencyProperty IsDotProperty;

        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public bool IsDot
        {
            get { return (bool)GetValue(IsDotProperty); }
            set { SetValue(IsDotProperty, value); }
        }
        #endregion

        #region Constructors
        static Badge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));

            IsDotProperty = DependencyProperty.Register("IsDot", typeof(bool), typeof(Badge), new FrameworkPropertyMetadata(false));
        }

        public Badge()
        {
            
        }
        #endregion

        #region Override方法
      
        #endregion

        #region Private方法

        #endregion
    }
}
