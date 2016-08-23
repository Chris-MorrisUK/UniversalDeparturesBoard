using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace DarwinFeed
{
    /// <summary>
    /// Interaction logic for DestinationRow.xaml
    /// </summary>
    public partial class DestinationRow : UserControl
    {
        public DestinationRow()
        {
            InitializeComponent();
            this.Loaded += DestinationRow_Loaded;
        }

        void DestinationRow_Loaded(object sender, RoutedEventArgs e)
        {
            grdOutter.ColumnDefinitions[0].MaxWidth = this.ActualWidth * 0.7;
        }

  

        
    }
}
