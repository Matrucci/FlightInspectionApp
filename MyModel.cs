using System;
// using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.IO;
using System.Xml;
using System.Data;
using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel;
using System.Threading;

namespace FG_Final
{
    class MyModel
    {
        private const float TIME_JUMPS = 1;
        private List<string> Colnames;
        private Dictionary<string, List<float>> mapCSV;
        private List<float> SelectedColumnAxis;
        private List<float> TimeAxis;
        private List<float> CorrelativeColumnAxis ;
        private string csvPath;
        private string xmlPath;
        private List<DataPoint> linearRegPoints;
        private connection ctd;
        private string SelectedItem { get; set; }
        private string correlativeFeature { get; set; }

        public MyModel(string csvPath, string xmlPath)
        {
            this.Colnames = new List<string>();
            this.SelectedColumnAxis = new List<float>();
            this.CorrelativeColumnAxis = new List<float>();
            this.TimeAxis = new List<float>();
            this.csvPath = csvPath;
            this.xmlPath = xmlPath;
            this.mapCSV = new Dictionary<string, List<float>>();
            this.linearRegPoints = new List<DataPoint>();
            correlativeFeature = "";
            setColnamesFromXml(xmlPath);
            //create a map and a new CSV file with the headers
            setMap(csvPath);
            ctd = new connection();
            ctd.setSelectedName(SelectedItem);
        }

        public void setColnamesFromXml(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList featuresNames = xmlDoc.GetElementsByTagName("name");
            for (int i = 0; i < featuresNames.Count / 2; i++)
            {
                if (Colnames.Contains(featuresNames[i].InnerXml))
                {
                    int j = 2;
                    while (Colnames.Contains(featuresNames[i].InnerXml + "_" + j.ToString()))
                    {
                        j++;
                    }
                    Colnames.Add(featuresNames[i].InnerXml + "_" + j.ToString());
                }
                else
                {
                    Colnames.Add(featuresNames[i].InnerXml);
                }

            }
        }

        public List<float> GetColumn(float[,] matrix, int columnNumber)
        {
            float[] colAsArray = Enumerable.Range(0, matrix.GetLength(0)).Select(x => matrix[x, columnNumber]).ToArray();
            List<float> colAsList = colAsArray.OfType<float>().ToList();
            return colAsList;
        }


        public void setMap(string csvPath)
        {
            ////////////////////////
            string strFilePath = @"C:\peleg\MitkademTwo\FG_Final\bin\x86\Debug\new_anomaly_flight.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine(string.Join(strSeperator, Colnames));
            ////////////////////////



            string filePath = @"C:\peleg\MitkademTwo\FG_Final\bin\x86\Debug\anomaly_flight.csv";
            StreamReader sr = new StreamReader(filePath);
            var lines = new List<float[]>();
            int Row = 0;
            while (!sr.EndOfStream)
            {
                string[] Line = sr.ReadLine().Split(',');
                if (Row != 0)
                {
                    ////////////////////////
                    sbOutput.AppendLine(string.Join(strSeperator, Line));
                    ////////////////////////

                    float[] LineAsFloat = new float[Line.Length];
                    for (int i = 0; i < Line.Length; i++)
                    {
                        LineAsFloat[i] = float.Parse(Line[i]);
                    }
                    lines.Add(LineAsFloat);
                }
                Row++;
            }

            ////////////////////////
            // Create and write the csv file
            File.WriteAllText(strFilePath, sbOutput.ToString());
            ////////////////////////


            var arrTwoD = new float[lines.Count(), lines[0].Count()];

            for (int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Count(); j++)
                    arrTwoD[i, j] = lines[i][j];
            }

            for (int i = 0; i < Colnames.Count; i++)
            {
                List<float> colList = GetColumn(arrTwoD, i);
                mapCSV.Add(Colnames[i], colList);
            }
        }




        public void setSelectedColumns()
        {
            this.SelectedColumnAxis.Clear();
            this.CorrelativeColumnAxis.Clear();
            this.TimeAxis.Clear();
            this.linearRegPoints.Clear();

            float minX = (mapCSV[SelectedItem]).Min();
            float maxX = (mapCSV[SelectedItem]).Max();
            ctd.setSelectedName(SelectedItem);
            correlativeFeature = ctd.getCorrelativeFeature(minX, maxX);
            
            int columnSize = (mapCSV[SelectedItem]).Count;

            //////////////////////////////////
            using (StreamWriter writetext = new StreamWriter("writenewwwwTwo.txt"))
            {
                writetext.WriteLine(correlativeFeature);
                for (int i = 0; i < columnSize; i++)
                {
                    (mapCSV[correlativeFeature]).ElementAt(i);
                    writetext.WriteLine(((mapCSV[correlativeFeature]).ElementAt(i)).ToString());
                }
            }
            //////////////////////////////////
            
            this.SelectedColumnAxis = mapCSV[SelectedItem];
            this.CorrelativeColumnAxis = mapCSV[correlativeFeature];
            for (int i = 0; i < columnSize; i++)
            {
                //this.SelectedColumnAxis.Add((mapCSV[SelectedItem]).ElementAt(i));
                this.TimeAxis.Add(TIME_JUMPS * (i + 1));
                //this.CorrelativeColumnAxis.Add((mapCSV[correlativeFeature]).ElementAt(i));
            }
            linearRegPoints.Add(new DataPoint(minX, ctd.getMinY()));
            linearRegPoints.Add(new DataPoint(maxX, ctd.getMaxY()));
        }

        //return the two points that we added of the linear regration line
        public List<DataPoint> getLinearRegPoints()
        {
            return linearRegPoints;
        }

        public List<string> getColnames ()
        {
            return Colnames;
        }

        public List<float> getSelectedColumnAxis()
        {
            return SelectedColumnAxis;
        }

        public List<float> getCorrelativeColumnAxis()
        {
            return CorrelativeColumnAxis;
        }

        public List<float> getTimeAxis()
        {
            return TimeAxis;
        }

        public string getSelectedItem()
        {
            return SelectedItem;
        }

        public void setSelectedItem(string selectedItem)
        {
            SelectedItem = selectedItem;
        }

        public string getCorrelativeFeature()
        {
            return correlativeFeature;
        }

        public void setCorrelativeFeature(string correlativeFeatureName)
        {
            correlativeFeature = correlativeFeatureName;
        }



    }

}
