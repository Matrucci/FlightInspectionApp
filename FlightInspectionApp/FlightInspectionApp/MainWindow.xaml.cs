using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CSVPath
        {
            get;
            set;
        }
        private string XMLPath
        {
            get;
            set;
        }
        //string xmlPath = String.Empty;
        //string csvPath = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
            upload_csv_btn.Visibility = Visibility.Hidden;
        }

        private void Upload_xml_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                upload_xml_btn.Content = openFileDialog.SafeFileName;
                upload_csv_btn.Visibility = Visibility.Visible;
                //Get the path of specified file
                XMLPath = openFileDialog.FileName;
                /*
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }*/
            }
        }

        private void Upload_csv_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                upload_csv_btn.Content = openFileDialog.SafeFileName;
                //Get the path of specified file
                CSVPath = openFileDialog.FileName;
                playback_controls.Visibility = Visibility.Visible;
                /*
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }*/
            }
        }
    }
}
