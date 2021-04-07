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
            vm.PropertyChanged += OnPropertyChanged;

            this.playback_controls.Notify += OnPlayback;

            //this.playback_controls.playback_slider.Minimum = 0;
            //this.playback_controls.playback_slider.Maximum = 2170;

            DataContext = this.vm;
            /*
            Binding binding = new Binding("Value");
            binding.Source = this.vm.VM_LineNumber;
            this.playback_controls.playback_slider.DataContext = this.vm.VM_LineNumber;
            this.playback_controls.playback_slider.SetBinding(Slider.ValueProperty, binding);*/

            /*
            this.playback_controls.playback_slider.Minimum = 0;
            this.playback_controls.playback_slider.Maximum = 2170;
            this.playback_controls.playback_slider.DataContext = this.vm.VM_LineNumber;
            */
            
        }

        public MainWindow(ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            vm.PropertyChanged += OnPropertyChanged;
            //vm.PropertyChanged += OnTest;
            //vm.CSVPath.bind(CSVPath);
            
            upload_csv_btn.Visibility = Visibility.Hidden;

            this.playback_controls.Notify += OnPlayback;
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


        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Console.WriteLine(e.PropertyName);
            if (e.PropertyName.Equals("VM_NumberOfLines"))
            {
                
                //this.playback_controls.playback_slider.Maximum = this.vm.GetMaximumSliderValue();
                //this.playback_controls.playback_slider.Minimum = this.vm.GetMinimumSliderValue();
            } 
            else if (Regex.Match(e.PropertyName, @"(.{3})\s*$").ToString().Equals("xml"))
            {
                this.xmlPath = e.PropertyName;
                upload_csv_btn.Visibility = Visibility.Visible;
            }
            else if (Regex.Match(e.PropertyName, @"(.{3})\s*$").ToString().Equals("csv"))
            {
                this.csvPath = e.PropertyName;
                playback_controls.Visibility = Visibility.Visible;
            }
            else if (e.PropertyName.Equals("VM_LineNumber"))
            {
                //this.playback_controls.playback_slider.Value = this.vm.GetMaximumSliderValue();
            }
        }


        public void SetVM(ViewModel vm)
        {
            this.vm = vm;
        }

        private void Upload_xml_btn_Click(object sender, RoutedEventArgs e)
        {
            this.vm.XmlButtonClick();
        }

        private void Upload_csv_btn_Click(object sender, RoutedEventArgs e)
        {
            this.vm.CsvButtonClick();
        }
    }
}
