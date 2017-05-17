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
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_Panel", Type = typeof(StackPanel))]
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class Cascader : Control
    {
        #region Private属性
        private Popup PART_Popup;
        private StackPanel PART_Panel;
        private TextBox PART_TextBox;
        private ObservableCollection<CascaderListBox> ListBoxContainer = new ObservableCollection<CascaderListBox>();
        private string ShowText = string.Empty;
        #endregion

        #region 依赖属性set get

        #region ItemsSource

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(Cascader), new PropertyMetadata(null));

        #endregion

        #region ChildMemberPath

        public string ChildMemberPath
        {
            get { return (string)GetValue(ChildMemberPathProperty); }
            set { SetValue(ChildMemberPathProperty, value); }
        }
        
        public static readonly DependencyProperty ChildMemberPathProperty =
            DependencyProperty.Register("ChildMemberPath", typeof(string), typeof(Cascader), new PropertyMetadata(string.Empty));

        #endregion

        #region DisplayMemberPath

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }
        
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(Cascader), new PropertyMetadata(string.Empty));

        #endregion

        #region ValueBoxStyle

        public Style ValueBoxStyle
        {
            get { return (Style)GetValue(ValueBoxStyleProperty); }
            set { SetValue(ValueBoxStyleProperty, value); }
        }
        
        public static readonly DependencyProperty ValueBoxStyleProperty =
            DependencyProperty.Register("ValueBoxStyle", typeof(Style), typeof(Cascader));

        #endregion

        #region ValueItemStyle

        public Style ValueItemStyle
        {
            get { return (Style)GetValue(ValueItemStyleProperty); }
            set { SetValue(ValueItemStyleProperty, value); }
        }
        
        public static readonly DependencyProperty ValueItemStyleProperty =
            DependencyProperty.Register("ValueItemStyle", typeof(Style), typeof(Cascader));

        #endregion

        #region IsShowEveryItem
        /// <summary>
        /// 获取或设置是否显示每一个节点。值为False则只显示最后一个节点
        /// </summary>
        public bool IsShowEveryItem
        {
            get { return (bool)GetValue(IsShowEveryItemProperty); }
            set { SetValue(IsShowEveryItemProperty, value); }
        }
        
        public static readonly DependencyProperty IsShowEveryItemProperty =
            DependencyProperty.Register("IsShowEveryItem", typeof(bool), typeof(Cascader), new PropertyMetadata(false));

        #endregion

        #region IsChangeOnSelected

        public bool IsChangeOnSelected
        {
            get { return (bool)GetValue(IsChangeOnSelectedProperty); }
            set { SetValue(IsChangeOnSelectedProperty, value); }
        }
        
        public static readonly DependencyProperty IsChangeOnSelectedProperty =
            DependencyProperty.Register("IsChangeOnSelected", typeof(bool), typeof(Cascader), new PropertyMetadata(false));

        #endregion

        #region Separator 分隔符

        public string Separator
        {
            get { return (string)GetValue(SeparatorProperty); }
            set { SetValue(SeparatorProperty, value); }
        }
        
        public static readonly DependencyProperty SeparatorProperty =
            DependencyProperty.Register("Separator", typeof(string), typeof(Cascader), new PropertyMetadata(" / "));

        #endregion

        #region SelectedItem

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(Cascader), new PropertyMetadata(null, SelectedItemChangedCallback));

        private static void SelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Cascader cascader = d as Cascader;
            cascader.GetItemDepth(e.NewValue);
        }

        #endregion

        #region SelectedValues

        public ObservableCollection<object> SelectedValues
        {
            get { return (ObservableCollection<object>)GetValue(SelectedValuesProperty); }
            private set { SetValue(SelectedValuesProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedValuesProperty =
            DependencyProperty.Register("SelectedValues", typeof(ObservableCollection<object>)
                , typeof(Cascader), new PropertyMetadata(null));

        #endregion

        #endregion

        #region Constructors
        static Cascader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Cascader), new FrameworkPropertyMetadata(typeof(Cascader)));
        }

        public Cascader()
        {
            this.SelectedValues = new ObservableCollection<object>();
            //this.SelectedValues.CollectionChanged += (o, e) => 
            //{
            //    this.ShowText = string.Empty;
            //    for (int i = 0; i < this.SelectedValues.Count; i++)
            //    {
            //        CascaderListBox listBox = this.ListBoxContainer[i] as CascaderListBox;
            //        if (listBox.Visibility == Visibility.Visible)
            //        {
            //            this.ShowText = this.ShowText + this.GetPropertyValue(listBox.SelectedItem) + this.Separator;
            //            this.SelectedValues.Add(listBox.SelectedItem);
            //        }
            //    }
            //};
        }
        #endregion

        #region Override方法

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Popup = this.GetTemplateChild("PART_Popup") as Popup;
            this.PART_Panel = this.GetTemplateChild("PART_Panel") as StackPanel;
            this.PART_TextBox = this.GetTemplateChild("PART_TextBox") as TextBox;
            this.ListBoxContainer.CollectionChanged += ListBoxContainer_CollectionChanged;
            CreateFirstContainer();
        }

        #endregion

        #region Private方法
        private void ListBoxContainer_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<CascaderListBox> collection = sender as ObservableCollection<CascaderListBox>;

            this.PART_Panel.Children.Clear();
            foreach (CascaderListBox item in collection)
            {
                this.PART_Panel.Children.Add(item);
            }
        }

        private void CreateFirstContainer()
        {
            this.CreateContainer(this.ItemsSource, 0, null);
        }

        private void CreateNextContainer(IList selectedItem, int deep)
        {
            if (selectedItem.Count <= 0)
            {
                return;
            }

            object obj = selectedItem[0];
            Type type = obj.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(this.ChildMemberPath);
            IList list = (IList)propertyInfo.GetValue(obj, null); //获取属性值
            
            if (list != null)
            {
                if (this.ListBoxContainer.Count > deep)
                {
                    CascaderListBox listBox = this.ListBoxContainer[deep];
                    listBox.SetValue(CascaderListBox.VisibilityProperty, Visibility.Visible);
                    listBox.SetValue(CascaderListBox.ItemsSourceProperty, list);
                    listBox.SetValue(CascaderListBox.ParentItemProperty, selectedItem);

                    for (int i = deep + 1; i < this.ListBoxContainer.Count; i++)
                    {
                        this.ListBoxContainer[i].Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    this.CreateContainer(list, deep, obj);
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

        private void CreateContainer(object itemsSource, int deep, object parent)
        {
            CascaderListBox container = new CascaderListBox();
            container.Owner = this;
            container.SetValue(CascaderListBox.ParentItemProperty, parent);
            container.SetValue(CascaderListBox.ItemsSourceProperty, itemsSource);
            container.SetValue(CascaderListBox.DisplayMemberPathProperty, this.DisplayMemberPath);
            container.SetValue(CascaderListBox.DeepProperty, deep);
            container.SetValue(CascaderListBox.StyleProperty, this.ValueBoxStyle);
            container.SetValue(CascaderListBox.ItemContainerStyleProperty, this.ValueItemStyle);
            container.SelectionChanged += Container_SelectionChanged;
            container.ItemClick += Container_ItemClick;
            this.ListBoxContainer.Add(container);
        }

        private void Container_ItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedValues.Clear();
            CascaderListBox cascaderListBox = sender as CascaderListBox;
            if (!this.HasChildren(e.NewValue))
            {
                this.ShowText = string.Empty;
                for (int i = 0; i < cascaderListBox.Deep + 1; i++)
                {
                    CascaderListBox listBox = this.ListBoxContainer[i] as CascaderListBox;
                    if(listBox.Visibility == Visibility.Visible)
                    {
                        this.ShowText = this.ShowText + this.GetPropertyValue(listBox.SelectedItem) + this.Separator;
                        this.SelectedValues.Add(listBox.SelectedItem);
                    }
                }
            }

            this.PART_TextBox.Text = this.ShowText.TrimEnd(this.Separator.ToCharArray());
        }

        private void Container_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CascaderListBox listBox = sender as CascaderListBox;
            this.CreateNextContainer(e.AddedItems, listBox.Deep + 1);
        }

        private bool HasChildren(object obj)
        {
            Type type = obj.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(this.ChildMemberPath);
            IList list = (IList)propertyInfo.GetValue(obj, null); //获取属性值
            if(list != null && list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private IEnumerable GetChildren(object obj)
        {
            Type type = obj.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(this.ChildMemberPath);
            IEnumerable list = (IEnumerable)propertyInfo.GetValue(obj, null); //获取属性值

            return list;
        }

        private object GetPropertyValue(object obj)
        {
            Type type = obj.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(this.DisplayMemberPath);
            return (object)propertyInfo.GetValue(obj, null);
        }

        private int GetItemDepth(object item)
        {
            this.ForeachItem(this.ItemsSource, item);
            return depth;
        }

        int depth = 0;
        private void ForeachItem(IEnumerable source, object item)
        {
            IEnumerator enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object current = enumerator.Current;
                if(current != item)
                {
                    if (this.HasChildren(current))
                    {
                        ForeachItem(this.GetChildren(current), item);
                    }
                }
                else
                {
                    depth++;
                }
            }
        }
        #endregion
    }
}
