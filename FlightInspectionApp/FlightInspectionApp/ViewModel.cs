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
    /********************
     * View model class.
     *******************/
    public class ViewModel : INotifyPropertyChanged, IViewModel
    {
        private string csvPath;
        private string xmlPath;
        private IView view;
        private FlightGearClient model;
        //private IModel model;
        Thread t;
        private AdvancedDetailsVM advm;

        public event PropertyChangedEventHandler PropertyChanged;

        /**********************************************
         * Convert the ranges: -1  -  1 to 80  -  170
         * in order for the joystick to work.
         *********************************************/
        private int ConvertValues(float value)
        {
            int oldRange = 2;
            int newRange = 90;
            int newValue = (int)(((value - (-1)) * newRange) / oldRange) + 80;
            return newValue;
        }

        //Properties
        public float VM_Altimeter
        {
            get
            {
                float altimeter = this.model.Altimeter;
                float altimeterR = (float)Math.Round(altimeter * 100f) / 100f;
                return altimeterR;
            }
        }

        public float VM_Altimeter_Top
        {
            get
            {
                return VM_Altimeter + 100;
            }
        }

        public float VM_Altimeter_Bot
        {
            get
            {
                return VM_Altimeter - 100;
            }
        }

        public float VM_AirSpeed
        {
            get
            {
                float airSpeed = this.model.AirSpeed;
                float airSpeedR = (float)Math.Round(airSpeed * 100f) / 100f;
                return airSpeedR;
            }
        }

        public float VM_AirSpeed_Center
        {
            get
            {
                return VM_AirSpeed + 50;
            }
        }

        public float VM_AirSpeed_Top
        {
            get
            {
                return VM_AirSpeed + 100;
            }
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
            get
            {
                float yaw = this.model.Yaw;
                float yawR = (float)Math.Round(yaw * 100f) / 100f;
                return yawR;
            }
        }

        public float VM_Yaw_Left
        {
            get
            {
                float left = (VM_Yaw - 50) % 360;
                float leftR = (float)Math.Round(left * 100f) / 100f;
                return leftR;
            }
        }

        public float VM_Yaw_Right
        {
            get
            {
                float right = (VM_Yaw + 50) % 360;
                float rightR = (float)Math.Round(right * 100f) / 100f;
                return rightR;
            }
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
            get 
            {
                return this.model.GetCurrentLine(); 
            }
            set
            {
                //this.advm.Rewind(value);
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

        //Constructors
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

        public FlightGearClient GetFlightGear()
        {
            return this.model;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Yaw"))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Yaw_Left"));
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Yaw_Right"));
            } 
            else if (e.PropertyName.Equals("AirSpeed"))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_AirSpeed_Top"));
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_AirSpeed_Center"));
            }
            else if (e.PropertyName.Equals("Altimeter"))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Altimeter_Top"));
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Altimeter_Bot"));
            }
            else if (e.PropertyName.Equals("LineNumber"))
            {
                //this.advm.Rewind(this.model.GetCurrentLine());
            }
            this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        //Setting the model
        void setModel(FlightGearClient model)
        {
            this.model = model;
        }

        //Setting the view
        public void setView(IView view)
        {
            this.view = view;
        }

        //Dealing with playback controls press.
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
                this.model.Play();
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


        //Getting a CSV from the user.
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

        public void RegCSVButton()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //TODO add logic
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string destFile = startupPath + @"\new_reg_flight.csv";
                System.IO.File.Copy(openFileDialog.FileName, destFile, true);
                this.PropertyChanged(this, new PropertyChangedEventArgs("Reg"));
            }
        }

        public void PressDLL()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "dll files (*.dll)|*.dll";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(openFileDialog.FileName));
            }
        }


        //Getting an XML from the user
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

        public void vm_setSelectedColumns()
        {
            throw new NotImplementedException();
        }
    }
}
