using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class Loading : Control
    {
        #region Private属性
        private FrameworkElement PART_Root;
        #endregion

        #region 依赖属性定义
        public bool IsActived
        {
            get { return (bool)GetValue(IsActivedProperty); }
            set { SetValue(IsActivedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActived.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActivedProperty =
            DependencyProperty.Register("IsActived", typeof(bool), typeof(Loading), new PropertyMetadata(true, OnIsActivedChangedCallback));

        private static void OnIsActivedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Loading loading = d as Loading;
            if(loading.PART_Root == null)
            {
                return;
            }
            VisualStateManager.GoToElementState(loading.PART_Root, (bool)e.NewValue ? "Active" : "Inactive", true);
        }

        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpeedRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(Loading), new PropertyMetadata(1d, OnSpeedRatioChangedCallback));

        private static void OnSpeedRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Loading loading = d as Loading;
            if(loading.PART_Root == null || !loading.IsActived)
            {
                return;
            }
            loading.SetSpeedRatio(loading.PART_Root, loading.SpeedRatio);
        }

        public EnumLoadingType Type
        {
            get { return (EnumLoadingType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(EnumLoadingType), typeof(Loading), new PropertyMetadata(EnumLoadingType.DoubleArc));


        #endregion

        #region 依赖属性set get

        #endregion

        #region Constructors
        static Loading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loading), new FrameworkPropertyMetadata(typeof(Loading)));
        }
        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_Root = this.GetTemplateChild("PART_Root") as FrameworkElement;
            if(this.PART_Root != null)
            {
                VisualStateManager.GoToElementState(this.PART_Root, this.IsActived ? "Active" : "Inactive", true);
                this.SetSpeedRatio(this.PART_Root, this.SpeedRatio);
            }
        }
        #endregion

        #region Private方法
        private void SetSpeedRatio(FrameworkElement element, double speedRatio)
        {
            foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(element))
            {
                if (group.Name == "ActiveStates")
                {
                    foreach (VisualState state in group.States)
                    {
                        if (state.Name == "Active")
                        {
                            state.Storyboard.SetSpeedRatio(element, speedRatio);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
