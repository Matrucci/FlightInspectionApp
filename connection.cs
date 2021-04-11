﻿using System;
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

namespace FG_Final
{
    class connection
    {
        private string selectedFeature;
        private float MinY;
        private float MaxY;

        ////////////////////////////////////////////

        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern void getNumberCheck();

        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern void getCorrelativeFeatureNameNew(string CSVfileName, string selectedFeatureName);

        //////////////////////////////////////////////


        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern IntPtr getCorrelativeFeatureData(string CSVfileName, string selectedFeatureName, float MinX, float MaxX);

        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern int correlativeStrLen(IntPtr str);

        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern char getCharByIndex(IntPtr str, int x);


        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern float getMinY(IntPtr str);
        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern float getMaxY(IntPtr str);


        public string getCorrelativeFeature(float minX, float maxX)
        {

            string correlativeFeatureName = "";
            //IntPtr str = getCorrelativeFeatureName("new_reg_flight.csv", selected);
            IntPtr str = getCorrelativeFeatureData("new_reg_flight.csv", selectedFeature, minX, maxX);
            int str_len = correlativeStrLen(str);
            for (int i = 0; i < str_len; i++)
            {
                char c = getCharByIndex(str, i);
                correlativeFeatureName += c.ToString();
            }

            MinY = getMinY(str);
            MaxY = getMaxY(str);
            ////////////////////////////
            using (StreamWriter writetext = new StreamWriter("writenewwwwCon.txt"))
            {
                writetext.WriteLine("the correlativeFeatureName is: " + correlativeFeatureName);
                writetext.WriteLine("the selectedFeature is: " + selectedFeature);
                writetext.WriteLine("minX is: " + minX + "MinY is: " + MinY);
                writetext.WriteLine("maxX is: " + maxX + "MaxY is: " + MaxY);
            }
            ////////////////////////////

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

        public connection()
        {
            selectedFeature = "";
            MinY = 0;
            MaxY = 0;
        }

    }
}