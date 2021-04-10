using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    public interface IModel : INotifyPropertyChanged
    {
        void SendFile(string path);
        void SetSpeed(double speed);
        int GetCurrentLine();
        void SetCurrentLine(int line);
        int GetNumberOfElements();
        void Stop();
        void Play();
        void Pause();
        void SetNumberOfElements(int value);
        void ParseFile(string path);
    }
}
