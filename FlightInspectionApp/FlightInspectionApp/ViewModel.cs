using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    public class ViewModel : INotifyPropertyChanged, IViewModel
    {
        private string csvPath;
        private string xmlPath;
        private IView view;
        private FlightGearClient model;
        //private IModel model;
        Thread t;

        public event PropertyChangedEventHandler PropertyChanged;

        private int ConvertValues(float value)
        {
            int oldRange = 2;
            int newRange = 90;
            int newValue = (int)(((value - (-1)) * newRange) / oldRange) + 80;
            return newValue;
        }

        public float VM_Altimeter
        {
            get { return this.model.Altimeter; }
        }

        public float VM_AirSpeed
        {
            get { return this.model.AirSpeed; }
        }

        public float VM_Direction
        {
            get { return this.model.Direction; }
        }

        public float VM_Roll
        {
            get { return this.model.Roll; }
        }

        public float VM_Pitch
        {
            get { return this.model.Pitch; }
        }

        public float VM_Yaw
        {
            get { return this.model.Yaw; }
        }
        public float VM_Throttle
        {
            get { return this.model.Throttle; }
            set
            {
                this.model.Throttle = value;
            }
        }
        public float VM_Rudder
        {
            get { return this.model.Rudder; }
            set
            {
                this.model.Rudder = value;
            }
        }
        public float VM_Aileron
        {
            get { return ConvertValues(this.model.Aileron); }
            set
            {
                this.model.Aileron = value;
            }
        }
        public float VM_Elevator
        {
            get { return ConvertValues(this.model.Elevator); }
            set
            {
                this.model.Elevator = value;
            }
        }

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
            this.view = view;
            this.model.PropertyChanged += OnPropertyChanged;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
            /*
            if (e.PropertyName.Equals("NumberOfLines"))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
            }
            else if (e.PropertyName.Equals("LineNumber"))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
            }*/
        }

        void setModel(FlightGearClient model)
        {
            this.model = model;
        }

        public void setView(IView view)
        {
            this.view = view;
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
                //Get the path of specified file
                CSVPath = openFileDialog.FileName;
                this.model.ParseFile(CSVPath);

                this.t = new Thread(() => this.model.SendFile(csvPath));
                this.t.Start();
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
                //Get the path of specified file
                XMLPath = openFileDialog.FileName;
                this.model.ParseFile(XMLPath);
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
