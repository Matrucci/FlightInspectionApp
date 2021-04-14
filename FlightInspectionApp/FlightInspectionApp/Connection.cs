/*
using System;
using System.Runtime.InteropServices;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;

namespace FlightInspectionApp
{
    class Connection
    {
        private string selectedFeature;
        private float MinY;
        private float MaxY;

        ////////////////////////////////////////////

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getNumberCheck();

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getCorrelativeFeatureNameNew(string CSVfileName, string selectedFeatureName);

        //////////////////////////////////////////////
        
         /////////
        [DllImport("AnomalyDetectorLineDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getAnomaly(string CSVLearnFileName, string CSVTestFileName, string txtFileName);
        /////////



            
        /////////
        //[DllImport("AnomalyDetectorCircleDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void getAnomaly(string CSVLearnFileName, string CSVTestFileName, string txtFileName);
        //////
        


        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getCorrelativeFeatureData(string CSVfileName, string selectedFeatureName, float MinX, float MaxX);

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int correlativeStrLen(IntPtr str);

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern char getCharByIndex(IntPtr str, int x);


        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float getMinY(IntPtr str);
        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float getMaxY(IntPtr str);


        public string getCorrelativeFeature(string csvPath, float minX, float maxX)
        {
            Console.WriteLine("connection1");

            string correlativeFeatureName = "";
            //IntPtr str = getCorrelativeFeatureName("new_reg_flight.csv", selected);
            IntPtr str = getCorrelativeFeatureData(csvPath, selectedFeature, minX, maxX);
            Console.WriteLine("connection2");
            int str_len = correlativeStrLen(str);
            for (int i = 0; i < str_len; i++)
            {
                char c = getCharByIndex(str, i);
                correlativeFeatureName += c.ToString();
            }

            MinY = getMinY(str);
            MaxY = getMaxY(str);

            return correlativeFeatureName;
        }

        public void setSelectedName(string selectedName)
        {
            selectedFeature = selectedName;
        }

        public float getMinY()
        {
            return MinY;
        }
        public float getMaxY()
        {
            return MaxY;
        }

        public Connection()
        {
            selectedFeature = "";
            MinY = 0;
            MaxY = 0;
            getAnomaly("new_reg_flight.csv", "new_anomaly_flight.csv", "testLineOne.txt");
        }

    }
}
*/

using System;
using System.Runtime.InteropServices;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;
using System.Reflection;

namespace FlightInspectionApp
{
    class Connection
    {
        string dllFile;
        private string selectedFeature;
        private float MinY;
        private float MaxY;

        ////////////////////////////////////////////

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getNumberCheck();

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getCorrelativeFeatureNameNew(string CSVfileName, string selectedFeatureName);

        //////////////////////////////////////////////

        /*
        /////////
        [DllImport("AnomalyDetectorLineDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getAnomaly(string CSVLearnFileName, string CSVTestFileName, string txtFileName);
        /////////
        */


        
        /////////
        [DllImport("AnomalyDetectorCircleDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void getAnomaly(string CSVLearnFileName, string CSVTestFileName, string txtFileName);
        /////////
        



        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getCorrelativeFeatureData(string CSVfileName, string selectedFeatureName, float MinX, float MaxX);

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int correlativeStrLen(IntPtr str);

        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern char getCharByIndex(IntPtr str, int x);


        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float getMinY(IntPtr str);
        [DllImport("AnomalyDetectorDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float getMaxY(IntPtr str);


        public string getCorrelativeFeature(string csvPath, float minX, float maxX)
        {
            Console.WriteLine("connection1");

            string correlativeFeatureName = "";
            //IntPtr str = getCorrelativeFeatureName("new_reg_flight.csv", selected);
            IntPtr str = getCorrelativeFeatureData(csvPath, selectedFeature, minX, maxX);
            Console.WriteLine("connection2");
            int str_len = correlativeStrLen(str);
            for (int i = 0; i < str_len; i++)
            {
                char c = getCharByIndex(str, i);
                correlativeFeatureName += c.ToString();
            }

            MinY = getMinY(str);
            MaxY = getMaxY(str);


            return correlativeFeatureName;
        }

        public void setSelectedName(string selectedName)
        {
            selectedFeature = selectedName;
        }

        public float getMinY()
        {
            return MinY;
        }
        public float getMaxY()
        {
            return MaxY;
        }

        public Connection(string dllPath)
        {
            
            dllFile = @"H:\Advanced Programming\FlightInspectionApp\Plugin\AnomalyDetectorCircleDLL\Debug\CSdll.dll";
            var assembly = Assembly.LoadFile(dllFile);
            var type = assembly.GetType("CSdll.Class1");
            var obj = Activator.CreateInstance(type);
            var method = type.GetMethod("getAnomaly");
            method.Invoke(obj, new object[]{"new_reg_flight.csv", "new_anomaly_flight.csv", "testLineOne.txt"});
            
            selectedFeature = "";
            MinY = 0;
            MaxY = 0;
            //getAnomaly("new_reg_flight.csv", "new_anomaly_flight.csv", "testLineOne.txt");
        }

    }
}
