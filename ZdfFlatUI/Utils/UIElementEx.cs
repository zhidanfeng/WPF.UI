using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace ZdfFlatUI.Utils
{
    public class UIElementEx
    {
        public static double RoundLayoutValue(double value, double dpiScale)
        {
            double num;
            if (!DoubleUtil.AreClose(dpiScale, 1.0))
            {
                num = Math.Round(value * dpiScale) / dpiScale;
                if (DoubleUtil.IsNaN(num) || double.IsInfinity(num) || DoubleUtil.AreClose(num, 1.7976931348623157E+308))
                {
                    num = value;
                }
            }
            else
            {
                num = Math.Round(value);
            }
            return num;
        }

        public static T GetAdorner<T>(DependencyObject d) where T : class
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
                        var adorner = adorners.FirstOrDefault() as T;

                        return adorner;
                    }
                }
            }

            return null;
        }
    }
}
