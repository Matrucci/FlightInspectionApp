using Microsoft.Win32;
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

namespace FlightInspectionApp
{
    /// <summary>
    /// Interaction logic for PreparationStage.xaml
    /// </summary>
    public partial class PreparationStage : UserControl
    {
        private string xmlPath;
        private string csvPath;
        private bool isCSV;

        public PreparationStage()
        {
            InitializeComponent();
            this.isCSV = false;
            this.xmlPath = string.Empty;
            this.csvPath = string.Empty;
            title_tb.Text = "Please choose an XML file with configurations";
        }


        private void choose_btn_Click(object sender, RoutedEventArgs e)
        {
            //var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            if (this.isCSV == false)
            {
                openFileDialog.Filter = "xml files (*.xml)|*.xml";
            }
            else
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
            }
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                /*
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }*/
            }
            filepath_tb.Text = filePath;
        }

        private void upload_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isCSV == false)
            {
                this.xmlPath = filepath_tb.Text;
                continue_btn.Visibility = Visibility.Visible;
            }
            else
            {
                this.csvPath = filepath_tb.Text;
                continue_btn.Visibility = Visibility.Visible;
            }
        }

        private void continue_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.isCSV == false)
            {
                this.isCSV = true;
                title_tb.Text = "Please choose a CSV file with flight data";
                filepath_tb.Text = "";
                continue_btn.Visibility = Visibility.Hidden;
            }
            else
            {
                //TODO start simulation
            }
        }
    }
}
