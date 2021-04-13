﻿using OxyPlot;
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

        private AdvancedDetailsModel user;
        public Boolean stop;
        Thread t;

        public bool rewind;
        public static Mutex mutex = new Mutex();

        public AdvancedDetailsVM()
        {
            user = new AdvancedDetailsModel("anomaly_flight.csv", "playback_small.xml");
            stop = false;
            rewind = false;

            // Create the plot model
            plotModel = new PlotModel();
            plotModel.Title = "";
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });
            //lineSeries1.Title = "hey";
            lineSeries1.DataFieldX = "HeadPosition";
            lineSeries1.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries1.Color = OxyColors.Green;
            plotModel.Series.Add(lineSeries1);

            // Create the plot model;
            plotModelTwo = new PlotModel();
            plotModelTwo.Title = "";
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
            lineSeries3.DataFieldX = "HeadPosition";
            lineSeries3.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries3.Color = OxyColors.Blue;
            lineSeries3Scatter.BinSize = 2;
            lineSeries3Scatter.MarkerFill = OxyColors.DarkOrange;
            lineSeries3Scatter.MarkerType = MarkerType.Circle;
            plotModelThree.Series.Add(lineSeries3);
            plotModelThree.Series.Add(lineSeries3Scatter);
        }

        public AdvancedDetailsVM(string xml, string csv)
        {
            user = new AdvancedDetailsModel(csv, xml);
            stop = false;

            // Create the plot model
            plotModel = new PlotModel();
            plotModel.Title = "";
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });
            //lineSeries1.Title = "hey";
            lineSeries1.DataFieldX = "HeadPosition";
            lineSeries1.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries1.Color = OxyColors.Green;
            plotModel.Series.Add(lineSeries1);

            // Create the plot model;
            plotModelTwo = new PlotModel();
            plotModelTwo.Title = "";
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
            lineSeries3.DataFieldX = "HeadPosition";
            lineSeries3.DataFieldY = "MeasuredCoilingTemperature";
            lineSeries3.Color = OxyColors.Blue;
            lineSeries3Scatter.BinSize = 2;
            lineSeries3Scatter.MarkerFill = OxyColors.DarkOrange;
            lineSeries3Scatter.MarkerType = MarkerType.Circle;
            plotModelThree.Series.Add(lineSeries3);
            plotModelThree.Series.Add(lineSeries3Scatter);
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
                lineSeries1.Points.Clear();
                lineSeries2.Points.Clear();
                lineSeries3.Points.Clear();
                lineSeries3Scatter.Points.Clear();
                plotModelThree.Axes.Clear();

                user.setSelectedColumns();
                plotModel.Title = vm_SelectedItem;
                plotModelTwo.Title = user.getCorrelativeFeature();
                plotModelThree.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = vm_SelectedItem });
                plotModelThree.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = user.getCorrelativeFeature() });
                int iteration = 0;
                stop = false;
                int sizeOfIteration = (vm_TimeAxis.Count) - 1;

                //get the minimum point of the linear regration line
                DataPoint minLinearRegPoint = (user.getLinearRegPoints()).ElementAt(0);
                //get the maximum point of the linear regration line
                DataPoint maxLinearRegPoint = (user.getLinearRegPoints()).ElementAt(1);

                //add the min point to the line of plotModelThree
                lineSeries3.Points.Add(minLinearRegPoint);
                //add the max point to the line of plotModelThree
                lineSeries3.Points.Add(maxLinearRegPoint);
                plotModelThree.InvalidatePlot(true);


                //new Thread(delegate ()
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

                    /**
                    if (stop = true && iteration >= 50)
                    {
                        Console.WriteLine("YDB");
                        lineSeries1.Points.RemoveRange(iteration - 50, 50);
                        lineSeries2.Points.RemoveRange(iteration - 50, 50);
                        lineSeries3Scatter.Points.RemoveRange(iteration - 50, 50);
                        iteration = iteration - 1;
                        mutex.WaitOne();
                        this.rewind = false;
                        mutex.ReleaseMutex();
                    }
                    **/
                    

                    plotModel.InvalidatePlot(true);
                    plotModelTwo.InvalidatePlot(true);
                    plotModelThree.InvalidatePlot(true);
                    OnPropertyChange("plotModel");
                    OnPropertyChange("plotModelTwo");
                    OnPropertyChange("plotModelThree");
                    if (iteration == sizeOfIteration)
                    {
                        stop = true;
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
                //this.stop = false;
                //}).Start();
            }
            );
            this.t.Start();

            
        }

        public void Start()
        {
            this.stop = false;
            //this.t.Abort();
        }

        public void Stop()
        {
           this.stop = true;
           this.t.Abort();
        }

        public void Rewind()
        {
            mutex.WaitOne();
            this.rewind = true;
            mutex.ReleaseMutex();
        }

        public int GetMinimumSliderValue()
        {
            throw new NotImplementedException();
        }

        public int GetMaximumSliderValue()
        {
            throw new NotImplementedException();
        }
   
    }
}
