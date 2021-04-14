using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightInspectionApp
{
    class AdvancedDetailsVM : IViewModel
    {
        public PlotModel plotModel { get; set; }
        public PlotModel plotModelTwo { get; set; }
        public PlotModel plotModelThree { get; set; }

        public LineSeries lineSeries1 = new LineSeries();
        public LineSeries lineSeries2 = new LineSeries();
        public LineSeries lineSeries3 = new LineSeries();
        public ScatterSeries lineSeries3Scatter = new ScatterSeries();
        public ScatterSeries lineSeries3AnomalyPoints = new ScatterSeries();
        public ScatterSeries lineSeries3ShapePoints = new ScatterSeries();
        public Boolean stop;
        Thread t;
        public bool rewind;
        public bool threadIsRunning;
        public static Mutex mutex = new Mutex();

        public int iteration = 0;

        private AdvancedDetailsModel user;
        

        public AdvancedDetailsVM()
        {
            user = new AdvancedDetailsModel("anomaly_flight.csv", "playback_small.xml");
            stop = false;
            rewind = false;
            threadIsRunning = false;


            // Create the plot model
            plotModel = new PlotModel();
            plotModel.Title = "";
            plotModel.TitleFontSize = 14;
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });
            //lineSeries1.Title = "hey";
            lineSeries1.DataFieldX = "HeadPosition";
            lineSeries1.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries1.Color = OxyColors.Green;
            plotModel.Series.Add(lineSeries1);

            // Create the plot model;
            plotModelTwo = new PlotModel();
            plotModelTwo.Title = "";
            plotModelTwo.TitleFontSize = 14;
            plotModelTwo.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });
            //lineSeries2.Title = "hey";
            lineSeries2.DataFieldX = "HeadPosition";
            lineSeries2.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries2.Color = OxyColors.Green;
            plotModelTwo.Series.Add(lineSeries2);


            // Create the plot model
            plotModelThree = new PlotModel();
            plotModelThree.Title = "";
            //plotModelThree.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "" });
            lineSeries3.Title = "Regression Line";
            lineSeries3Scatter.Title = "samples";
            lineSeries3AnomalyPoints.Title = "Anomalies";
            lineSeries3.DataFieldX = "HeadPosition";
            lineSeries3.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries3.Color = OxyColors.Blue;
            lineSeries3Scatter.BinSize = 2;
            lineSeries3Scatter.MarkerFill = OxyColors.DarkOrange;
            lineSeries3Scatter.MarkerType = MarkerType.Circle;
            lineSeries3AnomalyPoints.BinSize = 4;
            lineSeries3AnomalyPoints.MarkerFill = OxyColors.Red;
            lineSeries3AnomalyPoints.MarkerType = MarkerType.Circle;

            /////////////////////////
            lineSeries3ShapePoints.BinSize = 1;
            lineSeries3ShapePoints.MarkerFill = OxyColors.Black;
            lineSeries3ShapePoints.MarkerType = MarkerType.Circle;
            plotModelThree.Series.Add(lineSeries3ShapePoints);
            /////////////////////////
            plotModelThree.Series.Add(lineSeries3);
            plotModelThree.Series.Add(lineSeries3Scatter);
            plotModelThree.Series.Add(lineSeries3AnomalyPoints);
        }

        
        public AdvancedDetailsVM(string xml, string csv)
        {
            user = new AdvancedDetailsModel(csv, xml);
            stop = false;
            rewind = false;

            // Create the plot model
            plotModel = new PlotModel();
            plotModel.Title = "";
            plotModel.TitleFontSize = 10;
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });
            //lineSeries1.Title = "hey";
            lineSeries1.DataFieldX = "HeadPosition";
            lineSeries1.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries1.Color = OxyColors.Green;
            plotModel.Series.Add(lineSeries1);

            // Create the plot model;
            plotModelTwo = new PlotModel();
            plotModelTwo.Title = "";
            plotModelTwo.TitleFontSize = 10;
            plotModelTwo.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });
            //lineSeries2.Title = "hey";
            lineSeries2.DataFieldX = "HeadPosition";
            lineSeries2.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries2.Color = OxyColors.Green;
            plotModelTwo.Series.Add(lineSeries2);


            // Create the plot model
            plotModelThree = new PlotModel();
            plotModelThree.Title = "";
            //plotModelThree.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "" });
            lineSeries3.Title = "Regression Line";
            lineSeries3Scatter.Title = "samples";
            lineSeries3AnomalyPoints.Title = "Anomalies";
            lineSeries3.DataFieldX = "HeadPosition";
            lineSeries3.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries3.Color = OxyColors.Blue;
            lineSeries3Scatter.BinSize = 2;
            lineSeries3Scatter.MarkerFill = OxyColors.DarkOrange;
            lineSeries3Scatter.MarkerType = MarkerType.Circle;
            lineSeries3AnomalyPoints.BinSize = 1;
            lineSeries3AnomalyPoints.MarkerFill = OxyColors.Red;
            lineSeries3AnomalyPoints.MarkerType = MarkerType.Circle;

            /////////////////////////
            lineSeries3ShapePoints.BinSize = 1;
            lineSeries3ShapePoints.MarkerFill = OxyColors.Black;
            lineSeries3ShapePoints.MarkerType = MarkerType.Circle;
            plotModelThree.Series.Add(lineSeries3ShapePoints);
            /////////////////////////
            plotModelThree.Series.Add(lineSeries3);
            plotModelThree.Series.Add(lineSeries3Scatter);
            plotModelThree.Series.Add(lineSeries3AnomalyPoints);
        }
        

        public string vm_SelectedItem
        {
            get { return user.getSelectedItem(); }
            set
            {
                if (user.getSelectedItem() != value)
                {
                    user.setSelectedItem(value);
                    OnPropertyChange("vm_SelectedItem");
                }
            }
        }



        public string vm_CorrelativeItem
        {
            get { return user.getCorrelativeFeature(); }
            set
            {
                if (user.getCorrelativeFeature() != value)
                {
                    user.setCorrelativeFeature(value);
                    OnPropertyChange("vm_CorrelativeItem");
                }
            }
        }

        public List<string> vm_Colnames
        {
            get { return user.getColnames(); }
            set
            {
                if (user.getColnames() != value)
                {
                    OnPropertyChange("vm_Colnames");
                }
            }
        }

        public List<float> vm_SelectedColumnAxis
        {
            get { return user.getSelectedColumnAxis(); }
            set
            {
                if (user.getSelectedColumnAxis() != value)
                {
                    //OnPropertyChange("vm_SelectedColumnAxis");
                }
            }
        }

        public List<float> vm_CorrelativeColumnAxis
        {
            get { return user.getCorrelativeColumnAxis(); }
            set
            {
                if (user.getCorrelativeColumnAxis() != value)
                {
                    //OnPropertyChange("vm_CorrelativeColumnAxis");
                }
            }
        }

        public List<float> vm_TimeAxis
        {
            get { return user.getTimeAxis(); }
            set
            {
                if (user.getTimeAxis() != value)
                {
                    //OnPropertyChange("vm_TimeAxis");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void vm_setSelectedColumns()
        {
            this.t = new Thread(() =>
            {
                this.threadIsRunning = true;
                if (rewind == false)
                {
                    lineSeries1.Points.Clear();
                    lineSeries2.Points.Clear();
                    lineSeries3Scatter.Points.Clear();
                }
                rewind = false;
                lineSeries3.Points.Clear();
                lineSeries3AnomalyPoints.Points.Clear();
                lineSeries3ShapePoints.Points.Clear();
                plotModelThree.Axes.Clear();

                user.setSelectedColumns();
                plotModel.Title = vm_SelectedItem;
                plotModel.TitleFontSize = 16;
                plotModelTwo.Title = user.getCorrelativeFeature();
                plotModelTwo.TitleFontSize = 16;
                plotModelThree.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = vm_SelectedItem });
                plotModelThree.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = user.getCorrelativeFeature() });
                //int iteration = 0;
                stop = false;
                int sizeOfIteration = (vm_TimeAxis.Count) - 1;
                int numberOfAnomalies = (user.getAnomalyPoints()).Count;
                int numberOfShapePoints = (user.getShapePoints()).Count;

                //get the minimum point of the linear regration line
                DataPoint minLinearRegPoint = (user.getLinearRegPoints()).ElementAt(0);
                //get the maximum point of the linear regration line
                DataPoint maxLinearRegPoint = (user.getLinearRegPoints()).ElementAt(1);

                //add the min point to the line of plotModelThree
                lineSeries3.Points.Add(minLinearRegPoint);
                //add the max point to the line of plotModelThree
                lineSeries3.Points.Add(maxLinearRegPoint);
                plotModelThree.InvalidatePlot(true);

                for (int i = 0; i < numberOfAnomalies; i++)
                {
                    ScatterPoint currentPoint = (user.getAnomalyPoints()).ElementAt(i);
                    lineSeries3AnomalyPoints.Points.Add((user.getAnomalyPoints()).ElementAt(i));
                }
                plotModelThree.InvalidatePlot(true);


                for (int i = 0; i < numberOfShapePoints; i++)
                {
                    ScatterPoint currentPoint = (user.getShapePoints()).ElementAt(i);
                    lineSeries3ShapePoints.Points.Add((user.getShapePoints()).ElementAt(i));
                }
                plotModelThree.InvalidatePlot(true);
                //plotModelThree.Series.Add(new FunctionSeries((x) => Math.Sqrt(Math.Max(Math.Pow(radius, 2) - Math.Pow(x, 2), 0)), (y- radius), (y + radius), 0.1, "x^2 + y^2 ="+ Math.Pow(radius, 2)) { Color = OxyColors.Red });
                //plotModelThree.Series.Add(new FunctionSeries((x) => -Math.Sqrt(Math.Max(Math.Pow(radius, 2) - Math.Pow(x, 2), 0)), (y - radius), (y + radius), 0.1) { Color = OxyColors.Red });
                //plotModelThree.InvalidatePlot(true);

               // new Thread(delegate ()
                //{
                    while (!stop)
                    {
                        lineSeries1.Points.Add(new DataPoint(vm_TimeAxis.ElementAt(iteration), vm_SelectedColumnAxis.ElementAt(iteration)));
                        lineSeries2.Points.Add(new DataPoint(vm_TimeAxis.ElementAt(iteration), vm_CorrelativeColumnAxis.ElementAt(iteration)));

                        if (iteration % 10 == 0)
                        {
                            if (iteration > 300)
                            {
                                lineSeries3Scatter.Points.RemoveAt(0);
                            }
                            lineSeries3Scatter.Points.Add(new ScatterPoint(vm_SelectedColumnAxis.ElementAt(iteration), vm_CorrelativeColumnAxis.ElementAt(iteration), 3));
                        }


                        plotModel.InvalidatePlot(true);
                        plotModelTwo.InvalidatePlot(true);
                        plotModelThree.InvalidatePlot(true);
                        OnPropertyChange("plotModel");
                        OnPropertyChange("plotModelTwo");
                        OnPropertyChange("plotModelThree");
                        if (iteration == sizeOfIteration)
                        {
                        stop = true;
                        iteration = 0;
                            /////////////////////////////////
                            //plotModel.Title = "";
                            //plotModelTwo.Title = "";
                            /////////////////////////////////
                        }
                        else
                        {
                            iteration++;
                            //read the data of the values of the points in 4Hz
                            Thread.Sleep(2);
                        }
                    }
                //}).Start();
            }
            ); this.t.Start();
            this.threadIsRunning = false;
        }

        public int GetMinimumSliderValue()
        {
            throw new NotImplementedException();
        }

        public int GetMaximumSliderValue()
        {
            throw new NotImplementedException();
        }


        public void Start()
        {
            this.stop = false;
            //this.t.Abort();
            lineSeries1.Points.Clear();
            lineSeries2.Points.Clear();
            lineSeries3Scatter.Points.Clear();
            rewind = false;
            lineSeries3.Points.Clear();
            lineSeries3AnomalyPoints.Points.Clear();
            lineSeries3ShapePoints.Points.Clear();
            plotModelThree.Axes.Clear();
        }
        public void Stop()
        {
            this.stop = true;
            iteration = 0;
            this.t.Abort();
        }

        public void Rewind()
        {
            //mutex.WaitOne();
            this.rewind = true;
            //mutex.ReleaseMutex();
            if(this.stop == true)
            {
                this.stop = false;
                this.vm_setSelectedColumns();
            }
            iteration = 300;
            lineSeries1.Points.Clear();
            lineSeries2.Points.Clear();
            lineSeries3Scatter.Points.Clear();
            for (int i = 0; i <= iteration; i++)
            {
                lineSeries1.Points.Add(new DataPoint(vm_TimeAxis.ElementAt(i), vm_SelectedColumnAxis.ElementAt(i)));
                lineSeries2.Points.Add(new DataPoint(vm_TimeAxis.ElementAt(i), vm_CorrelativeColumnAxis.ElementAt(i)));

                if (i % 10 == 0)
                {
                    if (i > 300)
                    {
                        lineSeries3Scatter.Points.RemoveAt(0);
                    }
                    lineSeries3Scatter.Points.Add(new ScatterPoint(vm_SelectedColumnAxis.ElementAt(i), vm_CorrelativeColumnAxis.ElementAt(i), 3));
                }
            }
            plotModel.InvalidatePlot(true);
            plotModelTwo.InvalidatePlot(true);
            plotModelThree.InvalidatePlot(true);
            OnPropertyChange("plotModel");
            OnPropertyChange("plotModelTwo");
            OnPropertyChange("plotModelThree");
        }

    }
}
