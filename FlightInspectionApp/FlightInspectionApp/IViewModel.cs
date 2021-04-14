﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    public interface IViewModel
    {
        int GetMinimumSliderValue();
        int GetMaximumSliderValue();
        void vm_setSelectedColumns();
    }
}
