using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml;

namespace FlightInspectionApp
{
    public class FlightGearClient : IModel
    {
        private const float TIME_JUMPS = 1;
        public List<string> Colnames;
        private Dictionary<string, List<float>> mapCSV;
        public List<float> SelectedColumnsY;
        public List<float> SelectedColumnsXOne;
        public List<float> SelectedColumnsXTwo;

        private float throttle;
        private float rudder;
        private float aileron;
        private float elevator;
        private float altimeter;
        private float airSpeed;
        private float direction;
        private float pitch;
        private float roll;
        private float yaw;

        private double playbackSpeed;
        private int port;
        private int lineNumber;
        private int numberOfLines;
        private bool isRunning;
        private static EventWaitHandle waitHandle = new ManualResetEvent(initialState: true);

        public static Mutex mutex = new Mutex();

        public event PropertyChangedEventHandler PropertyChanged;

        public FlightGearClient()
        {
            this.port = 5400;
            this.playbackSpeed = 10;
            this.Colnames = new List<string>();
            this.SelectedColumnsY = new List<float>();
            this.SelectedColumnsXOne = new List<float>();
            this.SelectedColumnsXTwo = new List<float>();
            this.mapCSV = new Dictionary<string, List<float>>();
        }

        public FlightGearClient(int port)
        {
            this.port = port;
            this.playbackSpeed = 10;
        }

        public string SelectedItem { get; set; }

        public float Altimeter
        {
            get { return this.altimeter; }
            set
            {
                if (this.altimeter != value)
                {
                    this.altimeter = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Altimeter"));
                    }
                }
            }
        }

        public float AirSpeed
        {
            get { return this.airSpeed; }
            set
            {
                if (this.airSpeed != value)
                {
                    this.airSpeed = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("AirSpeed"));
                    }
                }
            }
        }

        public float Direction
        {
            get { return this.direction; }
            set
            {
                if (this.direction != value)
                {
                    this.direction = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Direction"));
                    }
                }
            }
        }

        public float Roll
        {
            get { return this.roll; }
            set
            {
                if (this.roll != value)
                {
                    this.roll = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Roll"));
                    }
                }
            }
        }

        public float Pitch
        {
            get { return this.pitch; }
            set
            {
                if (this.pitch != value)
                {
                    this.pitch = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Pitch"));
                    }
                }
            }
        }

        public float Yaw
        {
            get { return this.yaw; }
            set
            {
                if (this.yaw != value)
                {
                    this.yaw = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Yaw"));
                    }
                }
            }
        }

        public float Throttle
        {
            get { return this.throttle; }
            set
            {
                if (this.throttle != value)
                {
                    this.throttle = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Throttle"));
                    }
                }
            }
        }
        public float Rudder
        {
            get { return this.rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Rudder"));
                    }
                }
            }
        }
        public float Aileron
        {
            get { return this.aileron; }
            set
            {
                if (this.aileron != value)
                {
                    this.aileron = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Aileron"));
                    }
                }
            }
        }
        public float Elevator
        {
            get { return this.elevator; }
            set
            {
                if (this.elevator != value)
                {
                    this.elevator = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Elevator"));
                    }
                }
            }
        }

        public int NumberOfLines
        {
            get { return this.numberOfLines; }
            set
            {
                if (this.numberOfLines != value)
                {
                    this.numberOfLines = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("NumberOfLines"));
                    }
                }
            }
        }

        public int LineNumber
        {
            get { return this.lineNumber; }
            set
            {
                if (this.lineNumber != value)
                {
                    this.lineNumber = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("LineNumber"));
                    }
                }
            }
        }


        public void SendFile(string path) //Sending the CSV file
        {
            try
            {
                List<float> elevator = mapCSV["elevator"];
                List<float> throttle = mapCSV["throttle"];
                List<float> aileron = mapCSV["aileron"];
                List<float> rudder = mapCSV["rudder"];

                List<float> roll = mapCSV["roll-deg"];
                List<float> pitch = mapCSV["pitch-deg"];
                List<float> yaw = mapCSV["heading-deg"];
                List<float> airSpeed = mapCSV["airspeed-kt"];

                this.isRunning = true;
                TcpClient client = new TcpClient("127.0.0.1", this.port);
                NetworkStream stream = client.GetStream();

                var data = File.ReadLines(path);

                mutex.WaitOne();
                NumberOfLines = data.Count() - 1;
                LineNumber = 0;
                mutex.ReleaseMutex();

                string line = data.ElementAt(LineNumber);
                while (line != null)
                {
                    Byte[] dataBytes = System.Text.Encoding.ASCII.GetBytes(line + "\r\n");
                    stream.Write(dataBytes, 0, dataBytes.Length);
                    //Console.WriteLine("Line:  " + LineNumber);
                    Thread.Sleep((int)(1000 / this.playbackSpeed));
                    mutex.WaitOne();
                    Elevator = elevator[this.lineNumber];
                    Throttle = throttle[this.lineNumber];
                    Aileron = aileron[this.lineNumber];
                    Rudder = rudder[this.lineNumber];
                    Yaw = yaw[this.lineNumber];
                    Roll = roll[this.lineNumber];
                    Pitch = pitch[this.lineNumber];
                    LineNumber++;
                    mutex.ReleaseMutex();
                    line = data.ElementAt(LineNumber);
                    waitHandle.WaitOne();
                }

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Connection Error", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void SetSpeed(double newPlayBackSpeed)
        {
            mutex.WaitOne();
            this.playbackSpeed= 10 * newPlayBackSpeed;
            mutex.ReleaseMutex();
        }

        public int GetCurrentLine()
        {
            return this.lineNumber;
        }

        public void SetCurrentLine(int line)
        {
            mutex.WaitOne();
            LineNumber = line;
            mutex.ReleaseMutex();
        }

        public int GetNumberOfElements()
        {
            return this.numberOfLines;
        }

        public void Stop()
        {
            LineNumber = 0;
            if (this.isRunning == true)
            {
                this.isRunning = false;
                waitHandle.Reset();
            }
        }

        public void Start()
        {
            if (this.isRunning == false)
            {
                waitHandle.Set();
                this.isRunning = true;
            }
        }

        public void Pause()
        {
            if (this.isRunning == true)
            {
                this.isRunning = false;
                waitHandle.Reset();
            }
        }

        public void SetNumberOfElements(int value)
        {
            NumberOfLines = value;
        }

        public void ParseFile(string path)
        {
            if (Regex.Match(path, @"(.{3})\s*$").ToString().Equals("xml"))
            {
                setColnamesFromXml(path);
            }
            else if (Regex.Match(path, @"(.{3})\s*$").ToString().Equals("csv"))
            {
                setMap(path);
            }
        }


        public void setColnamesFromXml(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList featuresNames = xmlDoc.GetElementsByTagName("name");
            for (int i = 0; i < featuresNames.Count / 2; i++)
            {
                if (Colnames.Contains(featuresNames[i].InnerXml))
                {
                    int j = 2;
                    while (Colnames.Contains(featuresNames[i].InnerXml + "_" + j.ToString()))
                    {
                        j++;
                    }
                    Colnames.Add(featuresNames[i].InnerXml + "_" + j.ToString());
                }
                else
                {
                    Colnames.Add(featuresNames[i].InnerXml);
                }

            }
        }

        public List<float> GetColumn(float[,] matrix, int columnNumber)
        {
            float[] colAsArray = Enumerable.Range(0, matrix.GetLength(0)).Select(x => matrix[x, columnNumber]).ToArray();
            List<float> colAsList = colAsArray.OfType<float>().ToList();
            return colAsList;
        }


        public void setMap(string csvPath)
        {
            ////////////////////////
            string strFilePath = @"C:\test\test.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine(string.Join(strSeperator, Colnames));
            ////////////////////////



            //string filePath = @"C:\peleg\MitkademTwo\FG_Program\bin\x86\Debug\anomaly_flight.csv";
            StreamReader sr = new StreamReader(csvPath);
            var lines = new List<float[]>();
            int Row = 0;
            while (!sr.EndOfStream)
            {
                string[] Line = sr.ReadLine().Split(',');
                if (Row != 0)
                {
                    ////////////////////////
                    sbOutput.AppendLine(string.Join(strSeperator, Line));
                    ////////////////////////

                    float[] LineAsFloat = new float[Line.Length];
                    for (int i = 0; i < Line.Length; i++)
                    {
                        LineAsFloat[i] = float.Parse(Line[i]);
                    }
                    lines.Add(LineAsFloat);
                }
                Row++;
            }

            ////////////////////////
            // Create and write the csv file
            File.WriteAllText(strFilePath, sbOutput.ToString());
            ////////////////////////


            var arrTwoD = new float[lines.Count(), lines[0].Count()];

            for (int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Count(); j++)
                    arrTwoD[i, j] = lines[i][j];
            }

            for (int i = 0; i < Colnames.Count; i++)
            {
                List<float> colList = GetColumn(arrTwoD, i);
                mapCSV.Add(Colnames[i], colList);
            }
        }




        public void setSelectedColumns()
        {
            //string correlativeFeature = ctd.getCorrelativeFeature();
            int columnSize = (mapCSV[SelectedItem]).Count;
            for (int i = 0; i < columnSize; i++)
            {
                this.SelectedColumnsY.Add((mapCSV[SelectedItem]).ElementAt(i));
                this.SelectedColumnsXOne.Add(TIME_JUMPS * (i + 1));
                //this.SelectedColumnsXTwo.Add((mapCSV[correlativeFeature]).ElementAt(i));
                this.SelectedColumnsXTwo.Add(TIME_JUMPS * (i + 1));

                //read the data of the values of the points in 4Hz
                //Thread.Sleep(250); 
            }
        }



    }
}
