using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightInspectionApp.controls
{
    /// <summary>
    /// Interaction logic for playback.xaml
    /// </summary>
    public partial class playback : UserControl, IObservable, IView
    {
        public playback()
        {
            InitializeComponent();
        }

        public event Update Notify;

        private void skip_back_Click(object sender, RoutedEventArgs e)
        {
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("start"));
            }
        }

        private void rewind_Click(object sender, RoutedEventArgs e)
        {
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("rewind"));
            }
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("pause"));
            }
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("play"));
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("stop"));
            }
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("forward"));
            }
        }

        private void end_Click(object sender, RoutedEventArgs e)
        {
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("end"));
            }
        }

        private void speed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)speed.SelectedItem;
            double value = Double.Parse(typeItem.Content.ToString());
            if (Notify != null)
            {
                Notify(this, new ButtonEventArgs("playback speed", value));
            }
        }
    }
}
