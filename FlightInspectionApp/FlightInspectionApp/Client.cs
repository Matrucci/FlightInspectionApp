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
    public class Client
    {
        protected int port;

        public Client(int port)
        {
            this.port = port;
        }
    }

    public class FlightGearClient : Client
    {
        private double playbackSpeed;
        private int lineNumber;
        public static Mutex mutex = new Mutex();

        public FlightGearClient() : base(5400)
        {
            this.playbackSpeed = 10;
        }

        public FlightGearClient(int port) : base(port)
        {
            this.playbackSpeed = 10;
        }

        public void SendCSV(string path)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", base.port);
                NetworkStream stream = client.GetStream();

                var data = File.ReadLines(path);
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
        }

        public void SetSpeed(double newPlayBackSpeed)
        {
            mutex.WaitOne();
            this.playbackSpeed= newPlayBackSpeed;
            mutex.ReleaseMutex();
        }

    }
}
