using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ZdfFlatUI
{
    public class Upload : ButtonBase
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty MultiSelectProperty;
        public static readonly DependencyProperty FilterProperty;
        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 是否可以选择多个文件
        /// </summary>
        public bool MultiSelect
        {
            get { return (bool)GetValue(MultiSelectProperty); }
            set { SetValue(MultiSelectProperty, value); }
        }

        /// <summary>
        /// 文件过滤器
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }
        #endregion

        #region 路由事件
        public static readonly RoutedEvent UploadEvent;

        public event RoutedPropertyChangedEventHandler<object> FileUpload
        {
            add
            {
                base.AddHandler(UploadEvent, value);
            }
            remove
            {
                base.RemoveHandler(UploadEvent, value);
            }
        }

        protected virtual void OnFileUpload(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg =
                new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, UploadEvent);
            this.RaiseEvent(arg);
        }
        #endregion

        #region Constructors
        static Upload()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Upload), new FrameworkPropertyMetadata(typeof(Upload)));

            MultiSelectProperty = DependencyProperty.Register("MultiSelect", typeof(bool), typeof(Upload));
            FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(Upload));

            UploadEvent = EventManager.RegisterRoutedEvent("FileUpload"
                , RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>)
                , typeof(Upload));
        }
        #endregion

        #region Override方法
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Array files = (System.Array)e.Data.GetData(DataFormats.FileDrop);
                this.OnFileUpload(null, files);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //    e.Effects = DragDropEffects.Link;
            //else e.Effects = DragDropEffects.None;
        }

        protected override void OnClick()
        {
            base.OnClick();

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Multiselect = this.MultiSelect;
            //"文本文件|*.*|C#文件|*.cs|所有文件|*.*"
            openFileDialog.Filter = this.Filter;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = openFileDialog.FileNames;
                this.OnFileUpload(null, files);
            }
        }
        #endregion

        #region Private方法

        #endregion
    }
}
