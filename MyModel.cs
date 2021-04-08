using System;
// using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.IO;
using System.Xml;
using System.Data;
using System.Threading;


namespace FG_Program
{
    class MyModel
    {
        private const float TIME_JUMPS = 1;
        public List<string> Colnames;
        private Dictionary<string, List<float>> mapCSV;
        public List<float> SelectedColumnsY;
        public List<float> SelectedColumnsXOne;
        public List<float> SelectedColumnsXTwo;
        private string csvPath;
        private string xmlPath;

        private connection ctd;
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string SelectedItem { get; set; }


        /////////////////////////////////
        /**
        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern void getNumberCheck();

        

        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern void getAnomalies(string inputFile, string outputFile);


        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern IntPtr getCorrelativeFeatureName(string CSVfileName, string selectedFeatureName);

        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern int len(IntPtr str);

        [DllImport("AnomalyDetectorDLL.dll")]
        public static extern char getCharByIndex(IntPtr str, int x);


        public string getCorrelativeFeature()
        {
            string correlativeFeatureName = "";
            IntPtr str = getCorrelativeFeatureName("new_reg_flight.csv", SelectedItem);
            int str_len = len(str);
            for (int i = 0; i < str_len; i++)
            {
                char c = getCharByIndex(str, i);
                correlativeFeatureName += c.ToString();
            }
            //Console.WriteLine(correlativeFeatureName);
            return correlativeFeatureName;
        }
       **/
        /////////////////////////////////


        public MyModel(string csvPath, string xmlPath)
        {
            this.Colnames = new List<string>();
            this.SelectedColumnsY = new List<float>();
            this.SelectedColumnsXOne = new List<float>();
            this.SelectedColumnsXTwo = new List<float>();
            this.csvPath = csvPath;
            this.xmlPath = xmlPath;
            this.mapCSV = new Dictionary<string, List<float>>();

            setColnamesFromXml(xmlPath);
            //create a map and a new CSV file with the headers
            setMap(csvPath);

            ////////////////////////////
            ctd = new connection();
            ctd.setSelectedName("elevator");
           //getNumberCheck();
        ////////////////////////////
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
            string strFilePath = @"C:\peleg\MitkademTwo\FG_Program\bin\x86\Debug\new_anomaly_flight.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine(string.Join(strSeperator, Colnames));
            ////////////////////////



            string filePath = @"C:\peleg\MitkademTwo\FG_Program\bin\x86\Debug\anomaly_flight.csv";
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
            //string correlativeFeature = ctd.getCorrelativeFeature();
            int columnSize = (mapCSV[SelectedItem]).Count;
            for (int i = 0; i < columnSize; i++)
            {
                this.SelectedColumnsY.Add((mapCSV[SelectedItem]).ElementAt(i));
                this.SelectedColumnsXOne.Add(TIME_JUMPS * (i + 1));
                //this.SelectedColumnsXTwo.Add((mapCSV[correlativeFeature]).ElementAt(i));
                this.SelectedColumnsXTwo.Add(TIME_JUMPS * (i + 1));

                //read the data of the values of the points in 4Hz
                //Thread.Sleep(250); 
            }
        }





    }
}





/**
 int columnSize = (mapCSV[SelectedItem]).Count;
            new Thread(delegate ()
            {
                for (int i = 0; i < columnSize; i++)
                {
                    this.SelectedColumnsY.Add((mapCSV[SelectedItem]).ElementAt(i));
                    this.SelectedColumnsXOne.Add(TIME_JUMPS * (i + 1));

                    //read the data of the values of the points in 4Hz
                    //Thread.Sleep(250);

                }
            }).Start();
**/