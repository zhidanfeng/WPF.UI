using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    /// <summary>
    /// 级联选择控件
    /// </summary>
    public class CascaderBox : Control
    {
        #region Private属性
        private Popup PART_Popup;
        private StackPanel PART_Panel;
        private ObservableCollection<ListBox> ListBoxContainer = new ObservableCollection<ListBox>();
        #endregion

        #region 依赖属性set get

        #region ItemsSource

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CascaderBox), new PropertyMetadata(null));

        #endregion

        #region ChildMemberPath

        public string ChildMemberPath
        {
            get { return (string)GetValue(ChildMemberPathProperty); }
            set { SetValue(ChildMemberPathProperty, value); }
        }
        
        public static readonly DependencyProperty ChildMemberPathProperty =
            DependencyProperty.Register("ChildMemberPath", typeof(string), typeof(CascaderBox), new PropertyMetadata(string.Empty));

        #endregion

        #region DisplayMemberPath

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }
        
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(CascaderBox), new PropertyMetadata(string.Empty));

        #endregion

        #region ValueBoxStyle

        public Style ValueBoxStyle
        {
            get { return (Style)GetValue(ValueBoxStyleProperty); }
            set { SetValue(ValueBoxStyleProperty, value); }
        }
        
        public static readonly DependencyProperty ValueBoxStyleProperty =
            DependencyProperty.Register("ValueBoxStyle", typeof(Style), typeof(CascaderBox));

        #endregion

        #region ValueItemStyle

        public Style ValueItemStyle
        {
            get { return (Style)GetValue(ValueItemStyleProperty); }
            set { SetValue(ValueItemStyleProperty, value); }
        }
        
        public static readonly DependencyProperty ValueItemStyleProperty =
            DependencyProperty.Register("ValueItemStyle", typeof(Style), typeof(CascaderBox));

        #endregion

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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Popup = this.GetTemplateChild("PART_Popup") as Popup;
            this.PART_Panel = this.GetTemplateChild("PART_Panel") as StackPanel;
            this.ListBoxContainer.CollectionChanged += ListBoxContainer_CollectionChanged;
            CreateFirstContainer();
        }

        private void ListBoxContainer_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<ListBox> collection = sender as ObservableCollection<ListBox>;
            
            this.PART_Panel.Children.Clear();
            foreach (ListBox item in collection)
            {
                this.PART_Panel.Children.Add(item);
            }
        }

        private void CreateFirstContainer()
        {
            this.CreateContainer(this.ItemsSource, 0);
        }

        private void FirstContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            this.CreateNextContainer(e.AddedItems, (int)listBox.Tag + 1);
        }

        private void CreateNextContainer(IList selectedItem, int deep)
        {
            if(selectedItem.Count <= 0)
            {
                return;
            }

            object obj = selectedItem[0];
            Type type = obj.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(this.ChildMemberPath);
            IList list = (IList)propertyInfo.GetValue(obj, null); //获取属性值

            if(list != null)
            {
                if(this.ListBoxContainer.Count > deep)
                {
                    ListBox listBox = this.ListBoxContainer[deep];
                    listBox.SetValue(ListBox.VisibilityProperty, Visibility.Visible);
                    listBox.SetValue(ListBox.ItemsSourceProperty, list);

                    for (int i = deep + 1; i < this.ListBoxContainer.Count; i++)
                    {
                        this.ListBoxContainer[i].Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    this.CreateContainer(list, deep);
                }
            }
            else
            {
                if (this.ListBoxContainer.Count > deep)
                {
                    for (int i = deep; i < this.ListBoxContainer.Count; i++)
                    {
                        this.ListBoxContainer[i].Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void CreateContainer(object itemsSource, int deep)
        {
            ListBox container = new ListBox();
            container.SetValue(ListBox.ItemsSourceProperty, itemsSource);
            container.SetValue(ListBox.DisplayMemberPathProperty, this.DisplayMemberPath);
            container.SelectionChanged += Container_SelectionChanged;
            container.SetValue(ListBox.TagProperty, deep);
            container.SetValue(ListBox.StyleProperty, this.ValueBoxStyle);
            container.SetValue(ListBox.ItemContainerStyleProperty, this.ValueItemStyle);
            this.ListBoxContainer.Add(container);
        }

        private void Container_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            this.CreateNextContainer(e.AddedItems, (int)listBox.Tag + 1);
        }
        #endregion

        #region Private方法

        #endregion
    }
}
