using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FlightInspectionApp
{
    public class FlightGearClient : IObservable, IModel
    {
        private double playbackSpeed;
        private int port;
        private int lineNumber;
        private int numberOfLines;
        private bool isRunning;
        private Thread t;

        public static Mutex mutex = new Mutex();
        //private TcpClient client;
        //private NetworkStream stream;

        public event Update Notify;
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightGearClient()
        {
            this.port = 5400;
            this.playbackSpeed = 10;
            try
            {
                //client = new TcpClient("127.0.0.1", port);
                //stream = client.GetStream();
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public FlightGearClient(int port)
        {
            this.port = port;
            this.playbackSpeed = 10;
            //client = new TcpClient("127.0.0.1", this.port);
            //stream = client.GetStream();
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
                        //Application.Current.Dispatcher.Invoke(() => PropertyChanged(this, new PropertyChangedEventArgs("NumberOfLines")), DispatcherPriority.ContextIdle);
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
                        //Application.Current.Dispatcher.Invoke(() => PropertyChanged(this, new PropertyChangedEventArgs("LineNumber")), DispatcherPriority.ContextIdle);
                        PropertyChanged(this, new PropertyChangedEventArgs("LineNumber"));
                    }
                }
            }
        }


        public void SendFile(string path) //Sending the CSV file
        {
            
            try
            {

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
                    Console.WriteLine("Line:  " + LineNumber);
                    Thread.Sleep((int)(1000 / this.playbackSpeed));
                    mutex.WaitOne();
                    LineNumber++;
                    mutex.ReleaseMutex();
                    line = data.ElementAt(LineNumber);
                }

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Connection Error", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            /*
           this.t = new Thread(() =>
            {
                try
                {
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
                        Console.WriteLine("LINE " +  LineNumber);
                        Thread.Sleep((int)(1000 / this.playbackSpeed));
                        mutex.WaitOne();
                        LineNumber++;
                        mutex.ReleaseMutex();
                        line = data.ElementAt(LineNumber);
                    }

                    stream.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Connection Error", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            this.t.Start();*/

            this.isRunning = true;
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

        [Obsolete]
        public void Stop()
        {
            LineNumber = 0;
            if (this.isRunning == true)
            {
                this.isRunning = false;
                this.t.Suspend();
            }
        }

        [Obsolete]
        public void Start()
        {
            if (this.isRunning == false)
            {
                this.t.Resume();
                this.isRunning = true;
            }
        }

        [Obsolete]
        public void Pause()
        {
            if (this.isRunning == true)
            {
                this.isRunning = false;
                this.t.Suspend();
            }
        }

        public void SetNumberOfElements(int value)
        {
            NumberOfLines = value;
        }
    }
}
