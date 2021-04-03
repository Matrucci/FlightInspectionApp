using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    class ViewModelSwitching: IObservable, IObserver
    {
        public static int SwitchView
        {
            get;
            set;
        }

        public ViewModelSwitching()
        {
            SwitchView = 0;
        }

        public event Update notify;
    }
}
