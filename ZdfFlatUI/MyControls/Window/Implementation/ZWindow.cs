using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class ZWindow : Window
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #region TitleBackground

        public Brush TitleBackground
        {
            get { return (Brush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }
        
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.Register("TitleBackground", typeof(Brush), typeof(ZWindow));

        #endregion

        #region TitleForeground

        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }
        
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(ZWindow));

        #endregion

        #region TitleFontSize

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }
        
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(ZWindow), new PropertyMetadata(12d));

        #endregion

        #region TitleFontFamily

        public FontFamily TitleFontFamily
        {
            get { return (FontFamily)GetValue(TitleFontFamilyProperty); }
            set { SetValue(TitleFontFamilyProperty, value); }
        }
        
        public static readonly DependencyProperty TitleFontFamilyProperty =
            DependencyProperty.Register("TitleFontFamily", typeof(FontFamily), typeof(ZWindow));

        #endregion

        #region MaximizeBox

        public static readonly DependencyProperty MaximizeBoxProperty = DependencyProperty.Register("MaximizeBox"
            , typeof(bool), typeof(ZWindow), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示最大化按钮
        /// </summary>
        public bool MaximizeBox
        {
            get { return (bool)GetValue(MaximizeBoxProperty); }
            set { SetValue(MaximizeBoxProperty, value); }
        }

        #endregion

        #region MinimizeBox

        public static readonly DependencyProperty MinimizeBoxProperty = DependencyProperty.Register("MinimizeBox"
            , typeof(bool), typeof(ZWindow), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示最小化按钮
        /// </summary>
        public bool MinimizeBox
        {
            get { return (bool)GetValue(MinimizeBoxProperty); }
            set { SetValue(MinimizeBoxProperty, value); }
        }

        #endregion

        #region CloseBox

        public static readonly DependencyProperty CloseBoxProperty = DependencyProperty.Register("CloseBox"
            , typeof(bool), typeof(ZWindow), new PropertyMetadata(true));
        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool CloseBox
        {
            get { return (bool)GetValue(CloseBoxProperty); }
            set { SetValue(CloseBoxProperty, value); }
        }

        #endregion

        #region MoreOnTitle

        public object MoreOnTitle
        {
            get { return (object)GetValue(MoreOnTitleProperty); }
            set { SetValue(MoreOnTitleProperty, value); }
        }
        
        public static readonly DependencyProperty MoreOnTitleProperty =
            DependencyProperty.Register("MoreOnTitle", typeof(object), typeof(ZWindow));

        #endregion

        #endregion

        #region Constructors

        static ZWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZWindow), new FrameworkPropertyMetadata(typeof(ZWindow)));
        }

        #endregion

        #region Override

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
