using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ZdfFlatUI.MyControls.Primitives;

namespace ZdfFlatUI
{
    public class AutoCompleteBox : ZTextBoxBase
    {
        #region private fields
        ICollectionView collectionView;
        /// <summary>
        /// 程序中使用了Enter作为自动完成列表的选择键，当选择了一项之后会将该项自动填充到文本框中，因此会再次触发TextChanged事件
        /// 为了避免此事件被再次触发导致联想列表又被显示出来，因此定义该临时变量
        /// </summary>
        private bool mIsEnterKeyDown;
        private ListBox PART_ListBox;
        #endregion

        #region event

        #region FilterItemSelectedEvent

        public static readonly RoutedEvent FilterItemSelectedEvent = EventManager.RegisterRoutedEvent("FilterItemSelected",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(AutoCompleteBox));

        public event RoutedPropertyChangedEventHandler<object> FilterItemSelected
        {
            add
            {
                this.AddHandler(FilterItemSelectedEvent, value);
            }
            remove
            {
                this.RemoveHandler(FilterItemSelectedEvent, value);
            }
        }

        public virtual void OnFilterItemSelected(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg = new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, FilterItemSelectedEvent);
            this.RaiseEvent(arg);
        }

        #endregion

        #endregion

        #region DependencyProperty

        #region ItemsSource

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AutoCompleteBox), new PropertyMetadata(null));

        #endregion

        #region DisplayMemberPath

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }
        
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(AutoCompleteBox), new PropertyMetadata(string.Empty));

        #endregion

        #region FilterMemberSource

        public ObservableCollection<PropertyFilterDescription> FilterMemberSource
        {
            get { return (ObservableCollection<PropertyFilterDescription>)GetValue(FilterMemberSourceProperty); }
            set { SetValue(FilterMemberSourceProperty, value); }
        }
        
        public static readonly DependencyProperty FilterMemberSourceProperty =
            DependencyProperty.Register("FilterMemberSource", typeof(ObservableCollection<PropertyFilterDescription>), typeof(AutoCompleteBox), new PropertyMetadata(new ObservableCollection<PropertyFilterDescription>()));

        #endregion

        #region IsDropDownOpen

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(AutoCompleteBox), new PropertyMetadata(false));

        #endregion

        #region DropDownBoxStyle

        public Style DropDownBoxStyle
        {
            get { return (Style)GetValue(DropDownBoxStyleProperty); }
            set { SetValue(DropDownBoxStyleProperty, value); }
        }
        
        public static readonly DependencyProperty DropDownBoxStyleProperty =
            DependencyProperty.Register("DropDownBoxStyle", typeof(Style), typeof(AutoCompleteBox), new PropertyMetadata(null));

        #endregion

        #region DropDownBoxItemContainerStyle

        public Style DropDownBoxItemContainerStyle
        {
            get { return (Style)GetValue(DropDownBoxItemContainerStyleProperty); }
            set { SetValue(DropDownBoxItemContainerStyleProperty, value); }
        }
        
        public static readonly DependencyProperty DropDownBoxItemContainerStyleProperty =
            DependencyProperty.Register("DropDownBoxItemContainerStyle", typeof(Style), typeof(AutoCompleteBox));

        #endregion

        #region DropDownBoxGroupStyle

        public ObservableCollection<GroupStyle> DropDownBoxGroupStyle
        {
            get { return (ObservableCollection<GroupStyle>)GetValue(DropDownBoxGroupStyleProperty); }
            set { SetValue(DropDownBoxGroupStyleProperty, value); }
        }
        
        public static readonly DependencyProperty DropDownBoxGroupStyleProperty =
            DependencyProperty.Register("DropDownBoxGroupStyle", typeof(ObservableCollection<GroupStyle>), typeof(AutoCompleteBox));

        #endregion

        #region SelectedItem

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(AutoCompleteBox), new PropertyMetadata(null));

        #endregion

        #region MaxDropDownHeight

        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }
        
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(AutoCompleteBox), new PropertyMetadata(200d));

        #endregion

        #endregion

        #region 私有依赖属性

        #region IsBusy

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }
        
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(AutoCompleteBox), new PropertyMetadata(false));

        #endregion

        #region SelectedIndex

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            private set { SetValue(SelectedIndexProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(AutoCompleteBox), new PropertyMetadata(0));

        #endregion

        #endregion

        #region Constructors

        static AutoCompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteBox), new FrameworkPropertyMetadata(typeof(AutoCompleteBox)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_ListBox = this.GetTemplateChild("PART_ListBox") as ListBox;
            if(this.PART_ListBox != null)
            {
                this.PART_ListBox.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(ItemSelected), true);
            }
            //this.FilterItemsSource = this.ItemsSource;
            //collectionView = CollectionViewSource.GetDefaultView(this.FilterItemsSource);
            this.TextChanged += AutoCompleteBox_TextChanged;
            this.PreviewKeyDown += AutoCompleteBox_PreviewKeyDown;
        }

        private void ItemSelected(object sender, RoutedEventArgs e)
        {
            this.mIsEnterKeyDown = true;
            var item = this.PART_ListBox.SelectedItem;
            this.IsDropDownOpen = false;
            this.Text = Utils.CommonUtil.GetPropertyValue(item, this.DisplayMemberPath).ToString();
            this.SelectionStart = this.Text.Length;
            this.OnFilterItemSelected(item, item);
        }

        #endregion

        #region private function
        /// <summary>
        /// 当按键盘的上下键时，选择联想列表项
        /// </summary>
        /// <param name="isKeyUp"></param>
        private void SelectDropDownBoxItem(bool isKeyUp)
        {
            int count = ((System.Windows.Data.ListCollectionView)collectionView).Count;
            if (isKeyUp) //向上
            {
                //这里有个隐藏逻辑：设置SelectedIndex应该同时设置SelectedItem，但是在xaml中使用了Binding，则设置SelectedIndex
                //会自动设置SelectedItem
                //SelectedItem="{Binding SelectedItem, RelativeSource={RelativeSource TemplatedParent}}"
                this.SelectedIndex = (this.SelectedIndex - 1) < 0 ? 0 : (this.SelectedIndex - 1);
            }
            else
            {
                this.SelectedIndex = (this.SelectedIndex + 1) > count ? --count : (this.SelectedIndex + 1);
            }
        }
        #endregion

        #region Event Implement Function
        private void AutoCompleteBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up || e.Key == System.Windows.Input.Key.Down)
            {
                this.SelectDropDownBoxItem(e.Key == System.Windows.Input.Key.Up);
            }
            else if (e.Key == System.Windows.Input.Key.Enter)
            {
                //当有联想列表时点击Enter是选择Item，当没有联想列表时，点击Enter是弹出联想列表
                if (!this.IsDropDownOpen) 
                {
                    int count = ((System.Windows.Data.ListCollectionView)collectionView).Count;
                    if(count > 0)
                    {
                        this.IsDropDownOpen = true;
                    }
                }
                else
                {
                    this.mIsEnterKeyDown = true;
                    var item = this.SelectedItem;
                    this.IsDropDownOpen = false;
                    this.Text = Utils.CommonUtil.GetPropertyValue(item, this.DisplayMemberPath).ToString();
                    this.SelectionStart = this.Text.Length;
                    this.OnFilterItemSelected(this.SelectedItem, this.SelectedItem);
                }
            }
        }

        private void AutoCompleteBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //如果是因为选择了联想列表项导致文本框文本被改变则不进行再次联想
            if (this.mIsEnterKeyDown)
            {
                this.mIsEnterKeyDown = false;
                return;
            }

            //文本框没有文本时关闭联想列表
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                this.IsDropDownOpen = false;
                return;
            }

            if(collectionView == null)
            {
                collectionView = CollectionViewSource.GetDefaultView(this.ItemsSource);
            }

            Task.Factory.StartNew(() =>
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(()=> 
                {
                    collectionView.Filter = (o) =>
                    {
                        if (string.IsNullOrEmpty(this.DisplayMemberPath)
                            && (this.FilterMemberSource.Count == 0 || this.FilterMemberSource == null))
                        {
                            return Convert.ToString(o).Contains(this.Text);
                        }
                        else
                        {
                            //foreach (PropertyFilterDescription item in this.FilterMemberSource)
                            //{

                            //}
                            object value = Utils.CommonUtil.GetPropertyValue(o, this.DisplayMemberPath);
                            System.Diagnostics.Debug.WriteLine(Convert.ToString(value));
                            return Convert.ToString(value).Contains(this.Text);
                        }
                    };
                }));
            });

            int count = ((System.Windows.Data.ListCollectionView)collectionView).Count;
            if (count > 0)
            {
                this.SelectedIndex = 0;//默认选中第一个联想项
                this.IsDropDownOpen = true;
            }
            else
            {
                this.IsDropDownOpen = false;
            }
        }
        
        #endregion
    }

    public class PropertyFilterDescription
    {
        public string PropertyName { get; set; }
    }
}