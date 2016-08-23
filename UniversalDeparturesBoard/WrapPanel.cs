using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
/// <summary>
/// Credit: http://www.visuallylocated.com/post/2015/02/20/Creating-a-WrapPanel-for-your-Windows-Runtime-apps.aspx
/// Because universal apps don't get wrap panels
/// </summary>

namespace BasicControls
{
    public class WrapPanel : Panel
    {

        /// <summary>
        /// Caps the height of the rows to prevent things expanding infinitely
        /// </summary>
        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RowHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowHeightProperty =
            DependencyProperty.Register("RowHeight", typeof(double), typeof(WrapPanel), new PropertyMetadata(0d));

    
    
        protected override Size MeasureOverride(Size availableSize)
        {
            // Just take up all of the width
            Size finalSize = new Size { Width = availableSize.Width };
            double x = 0;
            double rowHeight = 0d;
            foreach (var child in Children)
            {
                // Tell the child control to determine the size needed
                child.Measure(availableSize);

                x += child.DesiredSize.Width;
                if (x > availableSize.Width)
                {
                    // this item will start the next row
                    x = child.DesiredSize.Width;

                    // adjust the height of the panel
                    finalSize.Height += rowHeight;
                    rowHeight = Math.Min(child.DesiredSize.Height, RowHeight);
                   //rowHeight=child.DesiredSize.Height;
                }
                else
                {
                    // Get the tallest item
                    rowHeight = Math.Min(Math.Max(child.DesiredSize.Height, rowHeight), RowHeight);
                    //rowHeight = Math.Max(child.DesiredSize.Height, rowHeight);
                }
            }

            // Add the final height
            finalSize.Height += rowHeight;
            return finalSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect finalRect = new Rect(0, 0, finalSize.Width, finalSize.Height);

            double rowHeight = 0;
            foreach (var child in Children)
            {
                if ((child.DesiredSize.Width + finalRect.X) > finalSize.Width)
                {
                    // next row!
                    finalRect.X = 0;
                    finalRect.Y += rowHeight;
                    rowHeight = 0;
                }
                // Place the item
                child.Arrange(new Rect(finalRect.X, finalRect.Y, child.DesiredSize.Width, child.DesiredSize.Height));

                // adjust the location for the next items
                finalRect.X += child.DesiredSize.Width;
                rowHeight = Math.Min(Math.Max(child.DesiredSize.Height, rowHeight), RowHeight);
            }
            return finalSize;
        }
    }
}
