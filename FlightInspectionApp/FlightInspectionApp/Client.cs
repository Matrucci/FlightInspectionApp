using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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

        public event Update Notify;

        public FlightGearClient()
        {
            this.port = 5400;
            this.playbackSpeed = 10;
        }

        public FlightGearClient(int port)
        {
            this.port = port;
            this.playbackSpeed = 10;
        }

        public void SendFile(string path) //Sending the CSV file
        {

            this.t = new Thread(() =>
            {
                try
                {
                    TcpClient client = new TcpClient("127.0.0.1", this.port);
                    NetworkStream stream = client.GetStream();

                    var data = File.ReadLines(path);
                    this.numberOfLines = data.Count() - 1;
                    this.lineNumber = 0;

                    string line = data.ElementAt(this.lineNumber);
                    while (line != null)
                    {
                        Byte[] dataBytes = System.Text.Encoding.ASCII.GetBytes(line + "\r\n");
                        stream.Write(dataBytes, 0, dataBytes.Length);
                        Thread.Sleep((int)(1000 / this.playbackSpeed));
                        mutex.WaitOne();
                        this.lineNumber++;
                        mutex.ReleaseMutex();
                        line = data.ElementAt(this.lineNumber);
                    }

                    stream.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection Error", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            this.t.Start();
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
            this.lineNumber = line;
            mutex.ReleaseMutex();
        }

        public int GetNumberOfElements()
        {
            return this.numberOfLines;
        }

        [Obsolete]
        public void Stop()
        {
            this.lineNumber = 0;
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
    }
}
