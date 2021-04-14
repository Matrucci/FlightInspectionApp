﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    public delegate void Update(Object sender, EventArgs e);
    interface IObservable
    {
        event Update Notify;
    }
}
