using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace ZdfFlatUI
{
    public class NavigateMenu : ListBox
    {
        #region Private属性
        CollectionViewSource viewSource = new CollectionViewSource();
        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty GroupDescriptionsProperty;
        public static readonly DependencyProperty GroupItemsSourceProperty;
        public static readonly DependencyProperty MyGroupStyleProperty;
        public static readonly DependencyProperty ShowGroupProperty;
        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 
        /// </summary>
        public string GroupDescriptions
        {
            get { return (string)GetValue(GroupDescriptionsProperty); }
            set { SetValue(GroupDescriptionsProperty, value); }
        }

        public IEnumerable GroupItemsSource
        {
            get { return (IEnumerable)GetValue(GroupItemsSourceProperty); }
            set { SetValue(GroupItemsSourceProperty, value); }
        }

        public GroupStyle MyGroupStyle
        {
            get { return (GroupStyle)GetValue(MyGroupStyleProperty); }
            set { SetValue(MyGroupStyleProperty, value); }
        }

        public bool ShowGroup
        {
            get { return (bool)GetValue(ShowGroupProperty); }
            set { SetValue(ShowGroupProperty, value); }
        }
        #endregion

        #region Constructors
        static NavigateMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigateMenu), new FrameworkPropertyMetadata(typeof(NavigateMenu)));

            NavigateMenu.GroupDescriptionsProperty = DependencyProperty.Register("GroupDescriptions", typeof(string), typeof(NavigateMenu));
            NavigateMenu.GroupItemsSourceProperty = DependencyProperty.Register("GroupItemsSource", typeof(IEnumerable), typeof(NavigateMenu));
            NavigateMenu.MyGroupStyleProperty = DependencyProperty.Register("MyGroupStyle", typeof(GroupStyle), typeof(NavigateMenu));
            NavigateMenu.ShowGroupProperty = DependencyProperty.Register("ShowGroup", typeof(bool), typeof(NavigateMenu));
        }
        #endregion

        #region Override方法
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NavigateMenuItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //ResourceDictionary rd = new ResourceDictionary();
            //rd.Source = new Uri("/WPF.UI;component/MyControls/NavigateMenu/Themes/Generic.xaml", UriKind.Relative);
            //this.Resources.MergedDictionaries.Add(rd);

            //Style style = this.Resources.MergedDictionaries[0]["NavigateMenuGroupStyle"] as Style;
            //this.GroupStyle.Clear();
            //this.GroupStyle.Add(new System.Windows.Controls.GroupStyle() { ContainerStyle = style });

            if (!string.IsNullOrEmpty(this.GroupDescriptions))
            {
                string[] list = this.GroupDescriptions.Split(',');
                foreach (string desc in list)
                {
                    viewSource.GroupDescriptions.Add(new PropertyGroupDescription(desc));
                }
            }
            viewSource.Source = this.GroupItemsSource;

            Binding binding = new Binding();
            binding.Source = viewSource;

            BindingOperations.SetBinding(this, NavigateMenu.ItemsSourceProperty, binding);

            
        }
        #endregion

        #region Private方法

        #endregion

        #region Public方法

        #endregion
    }
}
