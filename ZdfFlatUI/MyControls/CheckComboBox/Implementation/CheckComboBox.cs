using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ZdfFlatUI
{
    /// <summary>
    /// 多选下拉框
    /// </summary>
    public class CheckComboBox : Selector
    {
        #region private fields
        private ContentPresenter PART_ContentSite;
        private TextBox PART_FilterTextBox;
        private ICollectionView view;
        private Popup PART_Popup;

        private bool mPopupIsFirstOpen;
        #endregion

        #region DependencyProperty

        #region Content

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(CheckComboBox), new PropertyMetadata(string.Empty));

        #endregion

        #region Value

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(CheckComboBox), new PropertyMetadata(string.Empty));

        #endregion

        #region SelectedObjList

        public ObservableCollection<object> SelectedObjList
        {
            get { return (ObservableCollection<object>)GetValue(SelectedObjListProperty); }
            private set { SetValue(SelectedObjListProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedObjListProperty =
            DependencyProperty.Register("SelectedObjList", typeof(ObservableCollection<object>), typeof(CheckComboBox), new PropertyMetadata(null));

        #endregion

        #region SelectedStrList

        public ObservableCollection<string> SelectedStrList
        {
            get { return (ObservableCollection<string>)GetValue(SelectedStrListProperty); }
            private set { SetValue(SelectedStrListProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedStrListProperty =
            DependencyProperty.Register("SelectedStrList", typeof(ObservableCollection<string>), typeof(CheckComboBox));

        #endregion

        #region IsDropDownOpen

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(CheckComboBox), new PropertyMetadata(false, OnIsDropDownOpenChanged));

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckComboBox checkComboBox = d as CheckComboBox;
            
        }

        #endregion

        #region IsShowFilterBox
        /// <summary>
        /// 获取或者设置下拉列表过滤文本框的显示与隐藏
        /// </summary>
        public bool IsShowFilterBox
        {
            get { return (bool)GetValue(IsShowFilterBoxProperty); }
            set { SetValue(IsShowFilterBoxProperty, value); }
        }
        
        public static readonly DependencyProperty IsShowFilterBoxProperty =
            DependencyProperty.Register("IsShowFilterBox", typeof(bool), typeof(CheckComboBox), new PropertyMetadata(false));

        #endregion

        #region MaxShowNumber
        /// <summary>
        /// 获取或者设置最多显示的选中个数
        /// </summary>
        public int MaxShowNumber
        {
            get { return (int)GetValue(MaxShowNumberProperty); }
            set { SetValue(MaxShowNumberProperty, value); }
        }
        
        public static readonly DependencyProperty MaxShowNumberProperty =
            DependencyProperty.Register("MaxShowNumber", typeof(int), typeof(CheckComboBox), new PropertyMetadata(4));

        #endregion

        #region MaxDropDownHeight

        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }
        
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(CheckComboBox), new PropertyMetadata(200d));

        #endregion

        #region FilterBoxWatermark

        public string FilterBoxWatermark
        {
            get { return (string)GetValue(FilterBoxWatermarkProperty); }
            set { SetValue(FilterBoxWatermarkProperty, value); }
        }
        
        public static readonly DependencyProperty FilterBoxWatermarkProperty =
            DependencyProperty.Register("FilterBoxWatermark", typeof(string), typeof(CheckComboBox), new PropertyMetadata("Enter keyword filtering"));

        #endregion

        #endregion

        private bool HasCapture
        {
            get
            {
                return Mouse.Captured == this;
            }
        }

        #region Constructors

        static CheckComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckComboBox), new FrameworkPropertyMetadata(typeof(CheckComboBox)));
        }

        public CheckComboBox()
        {
            this.SelectedObjList = new ObservableCollection<object>();
            this.SelectedStrList = new ObservableCollection<string>();
        }
        #endregion

        #region Override
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.PART_FilterTextBox != null)
            {
                this.PART_FilterTextBox.TextChanged -= PART_FilterTextBox_TextChanged;
            }
            if (PART_Popup != null)
            {
                this.PART_Popup.Opened -= PART_Popup_Opened;
            }

            this.PART_ContentSite = this.GetTemplateChild("PART_ContentSite") as ContentPresenter;
            this.PART_FilterTextBox = this.GetTemplateChild("PART_FilterTextBox") as TextBox;
            this.PART_Popup = this.GetTemplateChild("PART_Popup") as Popup;
            if (this.PART_FilterTextBox != null)
            {
                this.PART_FilterTextBox.TextChanged += PART_FilterTextBox_TextChanged;
            }

            view = CollectionViewSource.GetDefaultView(this.ItemsSource);

            if(PART_Popup != null)
            {
                this.PART_Popup.Opened += PART_Popup_Opened;
            }

            this.Init();
        }
        
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (!(item is CheckComboBoxItem))
            {
                CheckComboBoxItem checkComboBoxItem = element as CheckComboBoxItem;
                if (checkComboBoxItem != null && !string.IsNullOrEmpty(this.DisplayMemberPath))
                {
                    Binding binding = new Binding(this.DisplayMemberPath);
                    checkComboBoxItem.SetBinding(CheckComboBoxItem.ContentProperty, binding);
                }
            }

            base.PrepareContainerForItemOverride(element, item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CheckComboBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is CheckComboBoxItem);
        }

        #endregion

        #region private function
        private void Init()
        {
            this.mPopupIsFirstOpen = true;

            if (this.SelectedObjList != null)
            {
                foreach (var obj in this.SelectedObjList)
                {
                    if (string.IsNullOrWhiteSpace(this.DisplayMemberPath))
                    {
                        this.SelectedStrList.Add(obj.ToString());
                    }
                    else
                    {
                        this.SelectedStrList.Add(Utils.CommonUtil.GetPropertyValue(obj, this.DisplayMemberPath).ToString());
                    }
                }
            }
            this.SetCheckComboBoxValueAndContent();
        }

        private void SetCheckComboBoxValueAndContent()
        {
            if (this.SelectedStrList == null) return;

            if (this.SelectedStrList.Count > this.MaxShowNumber)
            {
                this.Content = this.SelectedStrList.Count + " Selected";
            }
            else
            {
                this.Content = this.SelectedStrList.Aggregate("", (current, p) => current + (p + ", ")).TrimEnd(new char[] { ' ' }).TrimEnd(new char[] { ',' });
            }

            this.Value = this.SelectedStrList.Aggregate("", (current, p) => current + (p + ",")).TrimEnd(new char[] { ',' });
        }
        #endregion

        #region internal
        /// <summary>
        /// 行选中
        /// </summary>
        /// <param name="item"></param>
        internal void NotifyCheckComboBoxItemClicked(CheckComboBoxItem item)
        {
            item.SetValue(CheckComboBoxItem.IsSelectedProperty, !item.IsSelected);
            string itemContent = Convert.ToString(item.Content);
            if (item.IsSelected)
            {
                if (!this.SelectedStrList.Contains(item.Content))
                {
                    this.SelectedStrList.Add(itemContent);
                }
                if(!this.SelectedObjList.Contains(item.DataContext))
                {
                    this.SelectedObjList.Add(item.DataContext);
                }
            }
            else
            {
                if (this.SelectedStrList.Contains(itemContent))
                {
                    this.SelectedStrList.Remove(itemContent);
                }
                if (this.SelectedObjList.Contains(item.DataContext))
                {
                    this.SelectedObjList.Remove(item.DataContext);
                }
            }

            this.SetCheckComboBoxValueAndContent();
        }
        
        #endregion

        #region Event Implement Function
        /// <summary>
        /// 每次Open回显数据不太好，先这么处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_Popup_Opened(object sender, EventArgs e)
        {
            if (!this.mPopupIsFirstOpen) return;

            this.mPopupIsFirstOpen = false;

            if (this.ItemsSource == null || this.SelectedObjList == null) return;

            foreach (var obj in this.SelectedObjList)
            {
                foreach (var item in this.ItemsSource)
                {
                    if (item == obj)
                    {
                        CheckComboBoxItem checkComboBoxItem = this.ItemContainerGenerator.ContainerFromItem(item) as CheckComboBoxItem;
                        if (checkComboBoxItem != null)
                        {
                            checkComboBoxItem.IsSelected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void PART_FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.PART_FilterTextBox == null || view == null) return;

            view.Filter += (o) =>
            {
                string value = Convert.ToString(Utils.CommonUtil.GetPropertyValue(o, this.DisplayMemberPath)).ToLower();
                return value.IndexOf(this.PART_FilterTextBox.Text.ToLower()) != -1;
            };
        }
        #endregion
    }
}
