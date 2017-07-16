using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    /// <summary>
    /// 瀑布流容器
    /// </summary>
    public class WaterfallPanel1 : Panel
    {
        /// <summary>  
        /// 每列的高度  
        /// </summary>  
        private static double[] ColumnHeight;

        #region 构造函数  
        
        public WaterfallPanel1()
        {
            //根据列数，实例化用来存放每列高度的数组  
            ColumnHeight = new double[ColumnCount];
        }

        #endregion 构造函数  

        #region Dependency Property

        #region ColumnCount

        /// <summary>  
        /// 列数  
        /// </summary>
        public int ColumnCount
        {
            get { return (int)this.GetValue(ColumnCountProperty); }
            set { this.SetValue(ColumnCountProperty, value); }
        }

        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(WaterfallPanel1)
                , new PropertyMetadata(3, PropertyChanged));

        public static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColumnHeight = new double[(int)e.NewValue];
            if (sender == null || e.NewValue == e.OldValue)
            {
                return;
            }

            sender.SetValue(ColumnCountProperty, e.NewValue);
        }

        #endregion

        #endregion

        #region Override

        /// <summary>  
        /// 当在派生类中重写时，请测量子元素在布局中所需的大小，然后确定 <see cref="T:System.Windows.FrameworkElement" /> 派生类的大小。  
        /// 更新当前元素与其子元素的布局，以下处理都属于 测量 处理，并非实际布局  
        /// </summary>  
        /// <param name="availableSize">此元素可以赋给子元素的可用大小。可以指定无穷大值，这表示元素的大小将调整为内容的可用大小。</param>  
        /// <returns>此元素在布局过程中所需的大小，这是由此元素根据对其子元素大小的计算而确定的。</returns>  
        protected override Size MeasureOverride(Size availableSize)
        {
            //清空所有列的高度  
            for (int i = 0; i < ColumnHeight.Count(); i++)
            {
                ColumnHeight[i] = 0;
            }
            //计算行数  
            int indexY = this.Children.Count / ColumnCount;

            //计算行数  
            if (this.Children.Count % ColumnCount > 0)
            {
                indexY++;
            }
            
            //第几行  
            int flagY = 0;
            //声明一个尺寸，用来存放测量后面板的尺寸  
            Size resultSize = new Size(0, 0);

            #region 测量值

            //循环所有行  
            for (int i = 0; i < indexY; i++)//行  
            {
                //计算面板要呈现的宽度  
                resultSize.Width = Children[i].DesiredSize.Width * ColumnCount;
                //处理最后一行  
                if (i == indexY - 1)
                {
                    //剩余内容项个数  
                    int residual = Children.Count - i * ColumnCount;
                    //如果集合总数小于列数，那么剩余内容项就是集合总数  
                    if (Children.Count <= ColumnCount)
                    {
                        residual = Children.Count;
                    }
                    //循环剩余元素，设置元素呈现大小，计算当前列需要的高度  
                    for (int h = 0; h < residual; h++)
                    {
                        //更新当前循环元素的布局  
                        Children[ColumnCount * flagY + h].Measure(availableSize);
                        //累加每一列元素的高度  
                        ColumnHeight[h] += Children[ColumnCount * flagY + h].DesiredSize.Height;
                    }
                }
                else
                {
                    for (int y = 0; y < ColumnCount; y++)
                    {
                        Children[ColumnCount * flagY + y].Measure(availableSize);
                        ColumnHeight[y] += Children[ColumnCount * flagY + y].DesiredSize.Height;
                    }
                    flagY++;
                }
            }

            #endregion 测量值  

            //面板的高度等于所有列中最高的值  
            resultSize.Height = ColumnHeight.Max();

            //设置面板呈现的高度  
            //如果父元素给子元素提供的是一个无穷的宽，则使用计算的宽度，否则使用父元素的宽  
            resultSize.Width =
            double.IsPositiveInfinity(availableSize.Width) ?
            resultSize.Width : availableSize.Width;

            //设置面板呈现的高度  
            //如果父元素给子元素提供的是一个无穷的高，则使用计算的宽度，否则使用父元素的高  
            resultSize.Height =
            double.IsPositiveInfinity(availableSize.Height) ?
            resultSize.Height : availableSize.Height;
            //返回测量尺寸  
            return resultSize;
        }

        /// <summary>  
        /// 在派生类中重写时，请为 <see cref="T:System.Windows.FrameworkElement" /> 派生类定位子元素并确定大小。  
        /// 更新当前元素与其子元素的布局，以下处理都属于 实际 处理，元素布局都将基于此  
        /// </summary>  
        /// <param name="finalSize">父级中此元素应用来排列自身及其子元素的最终区域。</param>  
        /// <returns>所用的实际大小。</returns>  
        protected override Size ArrangeOverride(Size finalSize)
        {
            //清空所有列的高度  
            for (int i = 0; i < ColumnHeight.Count(); i++)
            {
                ColumnHeight[i] = 0;
            }

            //计算行数  
            int indexY = this.Children.Count / ColumnCount;
            if (this.Children.Count % ColumnCount > 0) indexY++;

            //当前行  
            int flagY = 0;

            //当前行高  
            double flagX = 0;

            #region 实际值  

            //循环所有行  
            for (int i = 0; i < indexY; i++)
            {
                //元素最终的宽度  
                finalSize.Width = Children[i].DesiredSize.Width * ColumnCount;

                //处理最后一行  
                if (i == indexY - 1)
                {
                    //列宽  
                    flagX = 0;
                    //剩余项个数  
                    int residual = Children.Count - i * ColumnCount;
                    if (Children.Count <= ColumnCount)
                    {
                        residual = Children.Count;
                    }

                    for (int h = 0; h < residual; h++)
                    {
                        Children[ColumnCount * i + h].Arrange(new Rect(new Point(flagX, ColumnHeight[h]), Children[ColumnCount * i + h].DesiredSize));
                        ColumnHeight[h] += Children[ColumnCount * i + h].DesiredSize.Height;
                        flagX += Children[ColumnCount * i + h].DesiredSize.Width;
                    }
                }
                else
                {
                    flagX = 0;
                    for (int y = 0; y < ColumnCount; y++)
                    {
                        Children[ColumnCount * flagY + y].Arrange(new Rect(new Point(flagX, ColumnHeight[y]), Children[ColumnCount * i + y].DesiredSize));
                        ColumnHeight[y] += Children[ColumnCount * flagY + y].DesiredSize.Height;
                        flagX += Children[ColumnCount * flagY + y].DesiredSize.Width;
                    }
                    flagY++;
                }
            }

            #endregion 测量值  

            return finalSize;
        }

        #endregion
    }
}
