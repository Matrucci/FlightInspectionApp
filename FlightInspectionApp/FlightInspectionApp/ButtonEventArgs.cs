using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    public class ButtonEventArgs : EventArgs
    {
        private string description;
        private double value;

        public ButtonEventArgs(string str)
        {
            this.description = str;
            value = 0;
        }
        public ButtonEventArgs(string str, double d)
        {
            this.description = str;
            this.value = d;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public double GetValue()
        {
           return this.value;
        }

    }
}
