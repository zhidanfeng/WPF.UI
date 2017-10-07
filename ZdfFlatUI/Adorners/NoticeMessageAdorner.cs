using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class NoticeMessageAdorner : Adorner
    {
        private VisualCollection _visuals;
        private NoticeMessage message;

        #region IsOpen

        public static bool GetIsOpen(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenProperty, value);
        }
        
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(NoticeMessageAdorner), new PropertyMetadata(false, IsOpenChangedCallback));

        private static void IsOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NoticeMessageAdorner adorner = NoticeMessageAdorner.GetAdorner(d);
            if (adorner == null)
            {
                return;
            }

            adorner.message.Duration = NoticeMessageAdorner.GetDuration(d);
            adorner.message.CornerRadius = NoticeMessageAdorner.GetCornerRadius(d);
            adorner.message.Content = NoticeMessageAdorner.GetContent(d);
            adorner.message.Background = NoticeMessageAdorner.GetBackground(d);
            adorner.message.MessageType = NoticeMessageAdorner.GetMessageType(d);
            adorner.message.SetBinding(NoticeMessage.IsShowProperty, new Binding()
            {
                Path = new PropertyPath("."),
                Source = (bool)e.NewValue,
                Mode = BindingMode.TwoWay
            });
        }

        #endregion

        #region Content

        public static string GetContent(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, string value)
        {
            obj.SetValue(ContentProperty, value);
        }
        
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached("Content", typeof(string), typeof(NoticeMessageAdorner), new PropertyMetadata(string.Empty, ContentChangedCallback));

        private static void ContentChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;

            if (element != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(element);

                if (adornerLayer != null)
                {
                    adornerLayer.Add(new NoticeMessageAdorner(element as UIElement));
                }
                else
                {
                    //layer为null，说明还未load过（整个可视化树中没有装饰层的情况不考虑）
                    //在控件的loaded事件内生成装饰件
                    element.Loaded += (s1, e1) => {
                        var adorner = new NoticeMessageAdorner(element);
                        var v = AdornerLayer.GetAdornerLayer(element);
                        if(v != null)
                        {
                            v.Add(adorner);
                        }
                    };
                }
            }
        }

        #endregion

        #region CornerRadius

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }
        
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(NoticeMessageAdorner), new PropertyMetadata(new CornerRadius(0d)));

        #endregion

        #region Duration

        public static double GetDuration(DependencyObject obj)
        {
            return (double)obj.GetValue(DurationProperty);
        }

        public static void SetDuration(DependencyObject obj, double value)
        {
            obj.SetValue(DurationProperty, value);
        }
        
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.RegisterAttached("Duration", typeof(double), typeof(NoticeMessageAdorner), new PropertyMetadata(2000d));

        #endregion

        #region Background

        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }
        
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(NoticeMessageAdorner)
                , new PropertyMetadata(new SolidColorBrush(Color.FromRgb(48, 49, 49))));

        #endregion

        #region MessageType

        public static EnumMessageType GetMessageType(DependencyObject obj)
        {
            return (EnumMessageType)obj.GetValue(MessageTypeProperty);
        }

        public static void SetMessageType(DependencyObject obj, EnumMessageType value)
        {
            obj.SetValue(MessageTypeProperty, value);
        }
        
        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.RegisterAttached("MessageType", typeof(EnumMessageType), typeof(NoticeMessageAdorner));

        #endregion

        public NoticeMessageAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _visuals = new VisualCollection(this);
            message = new NoticeMessage();
            message.IsHitTestVisible = false;
            this._visuals.Add(message);
        }

        #region override
        protected override int VisualChildrenCount
        {
            get
            {
                return _visuals.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            message.Arrange(new Rect(finalSize));

            return base.ArrangeOverride(finalSize);
        }
        #endregion

        private static NoticeMessageAdorner GetAdorner(DependencyObject d)
        {
            var element = d as FrameworkElement;

            if (element != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(element);
                if (adornerLayer != null)
                {
                    //能够获取装饰层，说明已经load过了，直接生成装饰件
                    var adorners = adornerLayer.GetAdorners(element);
                    if (adorners != null && adorners.Count() != 0)
                    {
                        var adorner = adorners.FirstOrDefault() as NoticeMessageAdorner;

                        return adorner;
                    }
                }
            }

            return null;
        }
    }
}
