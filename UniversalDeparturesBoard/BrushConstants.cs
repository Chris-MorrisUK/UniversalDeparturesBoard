using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace DarwinFeed
{
    /// <summary>
    /// These are brushes used from code
    /// </summary>
    public static class BrushConstants
    {
        public static SolidColorBrush CancelledRed
        {
            get
            {
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0, 0));
            }
        }
        public static SolidColorBrush WarningGold
        {
            get
            {
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xA3, 0x1A));
            }
        }
        public static SolidColorBrush DarkRed
        {
            get
            {
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x4D, 0x00, 0x00));
            }
        }
    }
}
