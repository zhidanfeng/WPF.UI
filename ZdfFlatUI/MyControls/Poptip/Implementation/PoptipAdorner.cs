using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class PoptipAdorner : Adorner
    {
        private Popup poptip;

        private VisualCollection _visuals;

        public PoptipAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _visuals = new VisualCollection(this);

            poptip = new Popup()
            {
                AllowsTransparency = true,
                StaysOpen = false,
                Placement = PlacementMode.Top,
            };
            

            //这行代码是使用Path=(ZUI:BadgeAdorner.Number)的关键代码
            poptip.DataContext = adornedElement;

            _visuals.Add(poptip);
        }

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
            poptip.Arrange(new Rect(finalSize));

            return base.ArrangeOverride(finalSize);
        }
    }
}
