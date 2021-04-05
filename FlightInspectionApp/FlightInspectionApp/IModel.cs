using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    public interface IModel
    {
        void SendFile(string path);
        void SetSpeed(double speed);
        int GetCurrentLine();
        void SetCurrentLine(int line);
        int GetNumberOfElements();
        void Stop();
        void Start();
        void Pause();
    }
}
