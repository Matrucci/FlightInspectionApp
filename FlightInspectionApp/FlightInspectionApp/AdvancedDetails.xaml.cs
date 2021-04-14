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

namespace FlightInspectionApp
{
    /// <summary>
    /// Interaction logic for AdvancedDetails.xaml
    /// </summary>
    public partial class AdvancedDetails : Window
    {
        private AdvancedDetailsVM vm;
        public AdvancedDetails()
        {
            InitializeComponent();
            this.InitializeComponent();
            this.vm = new AdvancedDetailsVM();
            this.DataContext = vm;
        }

        public AdvancedDetails(string xml, string csv)
        {
            InitializeComponent();
            this.InitializeComponent();
            this.vm = new AdvancedDetailsVM(csv, xml);
            this.DataContext = vm;
        }
        private void l1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.vm.Start();
            // Get the currently selected item in the ListBox.
            string selectedItem = l1.SelectedItem.ToString();
            MessageBox.Show(selectedItem);
            this.vm.vm_setSelectedColumns();
        }

        private void stop_btn_Click(object sender, RoutedEventArgs e)
        {
            this.vm.Stop();
        }

        private void rewind_btn_Click(object sender, RoutedEventArgs e)
        {

            //this.vm.Rewind();
        }
    }
}
