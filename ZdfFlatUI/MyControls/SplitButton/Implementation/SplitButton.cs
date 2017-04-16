using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    /// <summary>
    /// 分割按钮
    /// </summary>
    /// <remarks>add by zhidanfeng 2017.4.14</remarks>
    public class SplitButton : ItemsControl
    {
        private Button PART_Button;

        #region Private属性
        public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(SplitButton));

        public event RoutedPropertyChangedEventHandler<object> ItemClick
        {
            add
            {
                this.AddHandler(ItemClickEvent, value);
            }
            remove
            {
                this.RemoveHandler(ItemClickEvent, value);
            }
        }

        public virtual void OnItemClick(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg = new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, ItemClickEvent);
            this.RaiseEvent(arg);
        }
        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty IsDropDownOpenProperty;
        public static readonly DependencyProperty ContentProperty;
        #endregion

        #region 依赖属性set get
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(SplitButton));
        #endregion

        #region Constructors
        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
            SplitButton.IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(SplitButton), new PropertyMetadata(false));
            SplitButton.ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(SplitButton));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SplitButtonItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Button = this.GetTemplateChild("PART_Button") as Button;
            if(this.PART_Button != null)
            {
                this.PART_Button.Click += PART_Button_Click;
            }
        }
        #endregion

        #region Private方法
        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            this.OnItemClick(this.PART_Button.Content, this.PART_Button.Content);
        }
        #endregion
    }
}
