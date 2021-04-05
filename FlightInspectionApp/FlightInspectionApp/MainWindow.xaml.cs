using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class MainWindow : Window, IView, IObserver
    {
        private ViewModel vm;
        private string csvPath;
        private string xmlPath;
        
        private string CSVPath
        {
            get { return this.csvPath; }
            set
            {
                if (!this.csvPath.Equals(value))
                {
                    this.csvPath = value;
                   
                }
            }
        }
        private string XMLPath
        {
            get { return this.xmlPath; }
            set
            {
                if (!this.xmlPath.Equals(value))
                {
                    this.xmlPath = value;
                    
                }
            }
        }

        //string xmlPath = String.Empty;
        //string csvPath = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
            upload_csv_btn.Visibility = Visibility.Hidden;
            this.vm = new ViewModel(this);
            vm.PropertyChanged += OnXMLChange;
            vm.PropertyChanged += OnCSVChange;
            this.playback_controls.Notify += OnPlayback;
        }

        public MainWindow(ViewModel vm)
        {
            this.vm = vm;
            vm.PropertyChanged += OnXMLChange;
            vm.PropertyChanged += OnCSVChange;
            //vm.PropertyChanged += OnTest;
            //vm.CSVPath.bind(CSVPath);
            
            InitializeComponent();
            upload_csv_btn.Visibility = Visibility.Hidden;
        }

        public void OnPlayback(object sender, EventArgs e)
        {
            if (e.GetType().Equals(typeof(ButtonEventArgs)))
            {
                ButtonEventArgs be = (ButtonEventArgs)e;
                vm.PlaybackControl(be);
            }
        }

        /*
        public void OnTest(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("TEST!!");
            Console.WriteLine(e.PropertyName.ToString());
        }*/

        public void OnXMLChange(object sender, PropertyChangedEventArgs e)
        {
            
            string result = Regex.Match(e.PropertyName, @"(.{3})\s*$").ToString();
            if (result.Equals("xml"))
            {
                this.xmlPath = e.PropertyName;
                upload_csv_btn.Visibility = Visibility.Visible;
            }
        }

        public void OnCSVChange(object sender, PropertyChangedEventArgs e)
        {
            string result = Regex.Match(e.PropertyName, @"(.{3})\s*$").ToString();
            if (result.Equals("csv"))
            {
                this.csvPath = e.PropertyName;
                playback_controls.Visibility = Visibility.Visible;
            }
        }

        public void SetVM(ViewModel vm)
        {
            this.vm = vm;
        }

        private void Upload_xml_btn_Click(object sender, RoutedEventArgs e)
        {
            vm.XmlButtonClick();
        }

        private void Upload_csv_btn_Click(object sender, RoutedEventArgs e)
        {
            vm.CsvButtonClick();
        }
    }
}
