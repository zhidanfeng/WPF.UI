using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class TagBox : ListBox
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region IsLineFeed

        /// <summary>
        /// 获取或者设置子项是否换行显示
        /// </summary>
        public bool IsLineFeed
        {
            get { return (bool)GetValue(IsLineFeedProperty); }
            set { SetValue(IsLineFeedProperty, value); }
        }
        
        public static readonly DependencyProperty IsLineFeedProperty =
            DependencyProperty.Register("IsLineFeed", typeof(bool), typeof(TagBox), new PropertyMetadata(true));

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TagBox));

        #endregion

        #endregion

        #region Constructors

        static TagBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagBox), new FrameworkPropertyMetadata(typeof(TagBox)));
        }

        #endregion

        #region Override

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new Tag();
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
