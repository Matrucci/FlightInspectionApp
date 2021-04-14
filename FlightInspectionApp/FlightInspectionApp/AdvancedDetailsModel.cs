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

namespace FlightInspectionApp
{
    class anomalyReport
    {
        public string shape;
        public string featureTwo;
        public List<int> indexOfAnomaly;
        public List<float> drawX;
        public List<float> drawY;
        public anomalyReport(string shapeName, string featureTwoName, List<int> AnomalyIndex, List<float> DrawX, List<float> DrawY)
        {
            shape = shapeName;
            featureTwo = featureTwoName;
            indexOfAnomaly = AnomalyIndex.ToList();
            drawX = DrawX.ToList();
            drawY = DrawY.ToList();
        }
    }

    class AdvancedDetailsModel
    {
        private const float TIME_JUMPS = 1;
        private List<string> Colnames;
        private Dictionary<string, List<float>> mapCSV;
        private Dictionary<string, anomalyReport> anomalyMap;

        private List<float> SelectedColumnAxis;
        private List<float> TimeAxis;
        private List<float> CorrelativeColumnAxis;
        private string csvPath;
        private string xmlPath;
        private string anomalyTXTPath;
        private List<DataPoint> linearRegPoints;
        private List<ScatterPoint> anomalyPoints;
        private List<ScatterPoint> shapePoints;
        private Connection ctd;
        private string SelectedItem { get; set; }
        private string correlativeFeature { get; set; }

        public AdvancedDetailsModel(string csvPath, string xmlPath, string dllPath)
        {
            this.Colnames = new List<string>();
            this.SelectedColumnAxis = new List<float>();
            this.CorrelativeColumnAxis = new List<float>();
            this.TimeAxis = new List<float>();
            this.csvPath = csvPath;
            this.xmlPath = xmlPath;
            this.anomalyTXTPath = "testLineOne.txt";
            this.mapCSV = new Dictionary<string, List<float>>();
            this.anomalyMap = new Dictionary<string, anomalyReport>();
            this.linearRegPoints = new List<DataPoint>();
            this.anomalyPoints = new List<ScatterPoint>();
            this.shapePoints = new List<ScatterPoint>();

            correlativeFeature = "";
            //selectedAnomalyShape = "";
            setColnamesFromXml(xmlPath);
            //create a map and a new CSV file with the headers
            setMap(csvPath);
            ctd = new Connection(dllPath);
            //ctd.setSelectedName(SelectedItem);
            setAnomalyMap();
        }

        public void setColnamesFromXml(string xmlPath)
        {
            Console.WriteLine(xmlPath);
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
            /*
            int pos = csvPath.LastIndexOf(@"\") + 1;
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string strFilePath = startupPath + "new_" +  csvPath.Substring(pos, csvPath.Length - pos);
            */
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string strFilePath = startupPath + "new_anomaly_flight.csv";

            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine(string.Join(strSeperator, Colnames));
            ////////////////////////



            string filePath = csvPath;
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


        public void setAnomalyMap()
        {
            string str = String.Empty;
            StreamReader sr = File.OpenText(anomalyTXTPath);
            string shape = sr.ReadLine();
            while ((str = sr.ReadLine()) != null)
            {
                string[] LineFeature = str.Split(',');
                string featureOne = LineFeature[0];
                string featureTwo = LineFeature[1];
                str = sr.ReadLine();
                string[] LineOfIndex = str.Split(',');
                List<int> anomalyIndex = new List<int>();
                if (int.Parse(LineOfIndex[0]) != -1)
                {
                    for (int i = 0; i < LineOfIndex.Length; i++)
                    {
                        anomalyIndex.Add(int.Parse(LineOfIndex[i]));
                    }
                }

                str = sr.ReadLine();
                string[] strOfDrawX = str.Split(',');
                List<float> DrawX = new List<float>();
                str = sr.ReadLine();
                string[] strOfDrawY = str.Split(',');
                List<float> DrawY = new List<float>();
                if ((!((strOfDrawX[0]).Equals("-"))) && (!((strOfDrawY[0]).Equals("-"))))
                {
                    for (int i = 0; i < strOfDrawX.Length; i++)
                    {
                        DrawX.Add(float.Parse(strOfDrawX[i]));
                    }
                    for (int i = 0; i < strOfDrawY.Length; i++)
                    {
                        DrawY.Add(float.Parse(strOfDrawY[i]));
                    }
                }

                anomalyReport report = new anomalyReport(shape, featureTwo, anomalyIndex, DrawX, DrawY);
                this.anomalyMap.Add(featureOne, report);

            }
        }


        public void setSelectedColumns()
        {
            this.SelectedColumnAxis.Clear();
            this.CorrelativeColumnAxis.Clear();
            this.TimeAxis.Clear();
            this.linearRegPoints.Clear();
            this.anomalyPoints.Clear();
            this.shapePoints.Clear();
            ctd.setSelectedName(SelectedItem);

            float minX = (mapCSV[SelectedItem]).Min();
            float maxX = (mapCSV[SelectedItem]).Max();

            string startupPath = System.IO.Directory.GetCurrentDirectory();

            correlativeFeature = ctd.getCorrelativeFeature(startupPath + @"\new_anomaly_flight.csv", minX, maxX);
            //correlativeFeature = ctd.getCorrelativeFeature(@"H:\Advanced Programming\FlightInspectionApp\FlightInspectionApp\FlightInspectionApp\bin\x86\Debugnew_anomaly_flight.csv", minX, maxX);

            int columnSize = (mapCSV[SelectedItem]).Count;

            /*
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
            ///*/

            this.SelectedColumnAxis = (mapCSV[SelectedItem]).ToList();
            this.CorrelativeColumnAxis = (mapCSV[correlativeFeature]).ToList();
            for (int i = 0; i < columnSize; i++)
            {
                //this.SelectedColumnAxis.Add((mapCSV[SelectedItem]).ElementAt(i));
                this.TimeAxis.Add(TIME_JUMPS * (i + 1));
                //this.CorrelativeColumnAxis.Add((mapCSV[correlativeFeature]).ElementAt(i));
            }
            linearRegPoints.Add(new DataPoint(minX, ctd.getMinY()));
            linearRegPoints.Add(new DataPoint(maxX, ctd.getMaxY()));

            if (anomalyMap.ContainsKey(SelectedItem))
            {
                int anomalyPointsSize = ((anomalyMap[SelectedItem]).indexOfAnomaly).Count;
                List<int> anomalyValues = (anomalyMap[SelectedItem]).indexOfAnomaly;
                for (int i = 0; i < anomalyPointsSize; i++)
                {
                    int indexOfAnomaly = anomalyValues[i];
                    anomalyPoints.Add(new ScatterPoint((mapCSV[SelectedItem]).ElementAt(indexOfAnomaly), (mapCSV[correlativeFeature]).ElementAt(indexOfAnomaly), 1));
                }
            }

            if (anomalyMap.ContainsKey(SelectedItem))
            {
                int shapePointsSize = ((anomalyMap[SelectedItem]).drawX).Count;
                for (int i = 0; i < shapePointsSize; i++)
                {
                    shapePoints.Add(new ScatterPoint(((anomalyMap[SelectedItem]).drawX).ElementAt(i), ((anomalyMap[SelectedItem]).drawY).ElementAt(i), 3));
                }
            }


        }

        //return the list of the anomaly points that we added
        public List<ScatterPoint> getAnomalyPoints()
        {
            return anomalyPoints;
        }

        //return the list of the shape we added
        public List<ScatterPoint> getShapePoints()
        {
            return shapePoints;
        }

        public List<DataPoint> getLinearRegPoints()
        {
            return linearRegPoints;
        }

        public List<string> getColnames()
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
