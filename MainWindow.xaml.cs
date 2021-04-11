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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using System.Xml;

namespace FG_Final
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private readonly MyViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.InitializeComponent();
            _viewModel = new MyViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }

        private void l1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the currently selected item in the ListBox.
            string selectedItem = l1.SelectedItem.ToString();
            MessageBox.Show(selectedItem);
            _viewModel.vm_setSelectedColumns();
        }
    }
}
