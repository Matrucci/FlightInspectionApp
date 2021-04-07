﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    public class ViewModel : IObserver, IObservable, INotifyPropertyChanged, IViewModel
    {
        private string csvPath;
        private string xmlPath;
        private List<IView> views = new List<IView>();
        private IModel model;
        Thread t;

        //private IView view;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Update Notify;

        public int VM_LineNumber
        {
            get { return this.model.GetCurrentLine(); }
            set
            {
                this.model.SetCurrentLine(value);
            }
        }

        public int VM_NumberOfLines
        {
            get { return this.model.GetNumberOfElements(); }
            set
            {
                this.model.SetNumberOfElements(value);
            }
        }

        public string CSVPath
        {
            get { return this.csvPath; }
            set
            {
                if (value != null)
                {
                    this.csvPath = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs(this.csvPath));
                        //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(this.csvPath));
                    }
                }
            }
        }

        public string XMLPath
        {
            get { return this.xmlPath; }
            set
            {
                if (value != null)
                {
                    this.xmlPath = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs(this.xmlPath));
                        //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(this.xmlPath));
                    }
                }
            }
        }

        public ViewModel()
        {
            this.csvPath = string.Empty;
            this.xmlPath = string.Empty;
            this.model = new FlightGearClient();
            this.model.PropertyChanged += OnPropertyChanged;
        }
        public ViewModel(IView view)
        {
            this.csvPath = string.Empty;
            this.xmlPath = string.Empty;
            this.model = new FlightGearClient();
            this.views.Add(view);
            //this.view = view;
            this.model.PropertyChanged += OnPropertyChanged;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("NumberOfLines"))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
            }
            else if (e.PropertyName.Equals("LineNumber"))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
            }
        }

        void setModel(IModel model)
        {
            this.model = model;
        }

        public void setView(IView view)
        {
            //this.view = view;
            this.views.Add(view);
        }

        public void PlaybackControl(ButtonEventArgs be)
        {
            if (be.GetDescription() == "playback speed")
            {
                this.model.SetSpeed(be.GetValue());
            }
            else if (be.GetDescription() == "start")
            {
                this.model.SetCurrentLine(0);
            }
            else if (be.GetDescription() == "rewind")
            {
                if (this.model.GetCurrentLine() < 50)
                {
                    this.model.SetCurrentLine(0);
                }
                else
                {
                    this.model.SetCurrentLine(this.model.GetCurrentLine() - 50);
                }
            }
            else if (be.GetDescription() == "pause")
            {
                this.model.Pause();
            }
            else if (be.GetDescription() == "play")
            {
                this.model.Start();
            }
            else if (be.GetDescription() == "stop")
            {
                this.model.Stop();
            }
            else if (be.GetDescription() == "forward")
            {
                if (this.model.GetCurrentLine() + 50 > this.model.GetNumberOfElements())
                {
                    this.model.SetCurrentLine(this.model.GetNumberOfElements());
                }
                else
                {
                    this.model.SetCurrentLine(this.model.GetCurrentLine() + 50);
                }
            }
            else if (be.GetDescription() == "end")
            {
                this.model.SetCurrentLine(this.model.GetNumberOfElements());
            }
        }

        public void CsvButtonClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //upload_csv_btn.Content = openFileDialog.SafeFileName;
                
                
                //Get the path of specified file
                CSVPath = openFileDialog.FileName;
                //Console.WriteLine("Test::: " + CSVPath);

                //this.model.SendFile(csvPath);
                this.t = new Thread(() => this.model.SendFile(csvPath));
                this.t.Start();


                //playback_controls.Visibility = Visibility.Visible;
                /*
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }*/
            }
        }

        public void XmlButtonClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //upload_xml_btn.Content = openFileDialog.SafeFileName;
                //upload_csv_btn.Visibility = Visibility.Visible;
                
                
                //Get the path of specified file
                XMLPath = openFileDialog.FileName;

                //Console.WriteLine("Test::: " + XMLPath);


                /*
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }*/
            }
        }

        public int GetMinimumSliderValue()
        {
            return 0;
        }

        public int GetMaximumSliderValue()
        {
            return this.model.GetNumberOfElements();
        }

        public int GetCurrentLine()
        {
            return this.model.GetCurrentLine();
        }
    }
}
