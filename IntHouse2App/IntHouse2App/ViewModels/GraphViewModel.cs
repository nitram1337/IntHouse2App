using IntHouse2App.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;

namespace IntHouse2App.ViewModels
{
    public class GraphViewModel : BaseViewModel
    {
        //public ObservableCollection<Measurement> Measurements;
        public List<Entry> Entries;
        private Chart _chartMeasurements;

        public Chart ChartMeasurements
        {
            get => _chartMeasurements;
            set => SetProperty(ref _chartMeasurements, value);
        }

        public Command LoadChartMeasurementsCommand { get; }

        public GraphViewModel()
        {
            Title = "Graph Measurements";
            //Measurements = new ObservableCollection<Measurement>();
            Entries = new List<Entry>();
            LoadChartMeasurementsCommand = new Command(async () => await ExecuteLoadAllChartMeasurementsCommand());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async Task ExecuteLoadAllChartMeasurementsCommand()
        {
            IsBusy = true;
            try
            {
                //Measurements.Clear();
                Entries.Clear();
                var measurements = await _measurementsService.GetMeasurementsTimeFilteredAsync(TimeFrame.LatestWeek);
                foreach (Measurement measurement in measurements)
                {
                    //Measurements.Add(measurement);
                    Entries.Add(new Entry(measurement.Temperature)
                    {
                        Color = SKColor.Parse("#038cfc"),
                        Label = measurement.TimeCreated.ToString(),
                        TextColor = SKColor.Parse("#000000"),
                        ValueLabel = measurement.Temperature.ToString()
                    });
                }

                ChartMeasurements = new LineChart { Entries = Entries };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }


        public void Dispose()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

    }
}
