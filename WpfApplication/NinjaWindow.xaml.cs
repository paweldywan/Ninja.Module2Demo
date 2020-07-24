using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for NinjaWindow.xaml
    /// </summary>
    public partial class NinjaWindow : Window
    {
        public NinjaWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource ninjaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ninjaViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // ninjaViewSource.Source = [generic data source]
        }
    }
}
