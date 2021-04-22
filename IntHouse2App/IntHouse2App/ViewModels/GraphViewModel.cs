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
        #region ChartTemperature
        private Chart _chartTemperature;

        public Chart ChartTemperature
        {
            get => _chartTemperature;
            set => SetProperty(ref _chartTemperature, value);
        }
        #endregion ChartTemperature

        #region ChartHumidity
        private Chart _chartHumidity;

        public Chart ChartHumidity
        {
            get => _chartHumidity;
            set => SetProperty(ref _chartHumidity, value);
        }
        #endregion ChartHumidity

        public List<TimeFrame> TimeFrames { get; } = new List<TimeFrame> { TimeFrame.LatestHour, TimeFrame.LatestDay, TimeFrame.LatestWeek };

        TimeFrame _pickedTimeFrame;

        public TimeFrame PickedTimeFrame
        {
            get => _pickedTimeFrame;
            set
            {
                SetProperty(ref _pickedTimeFrame, value);
                
                    ExecuteLoadAllChartMeasurementsCommand();
                
            }
        }

        public Command LoadChartMeasurementsCommand { get; }

        public GraphViewModel()
        {
            Title = "Graph Measurements";

            _pickedTimeFrame = TimeFrame.LatestHour;
            LoadChartMeasurementsCommand = new Command(() => ExecuteLoadAllChartMeasurementsCommand());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async void ExecuteLoadAllChartMeasurementsCommand()
        {
            IsBusy = true;
            try
            {
                List<Entry> NewTemperatureEntries = new List<Entry>();
                List<Entry> NewHumidityEntries = new List<Entry>();
                var measurements = await _measurementsService.GetMeasurementsTimeFilteredAsync(PickedTimeFrame);
                foreach (Measurement measurement in measurements)
                {
                    #region TemperatureEntriesAdd
                    NewTemperatureEntries.Add(new Entry(measurement.Temperature)
                    {
                        Color = SKColor.Parse("#8a3644"), // #038cfc
                        Label = measurement.TimeCreated.ToString("dd/MM H:mm"),
                        TextColor = SKColor.Parse("#000000"),
                        ValueLabel = measurement.Temperature.ToString()
                    });
                    #endregion TemperatureEntriesAdd

                    #region HumidityEntriesAdd
                    NewHumidityEntries.Add(new Entry(measurement.Humidity)
                    {
                        Color = SKColor.Parse("#368a53"),
                        Label = measurement.TimeCreated.ToString("dd/MM H:mm"),
                        TextColor = SKColor.Parse("#000000"),
                        ValueLabel = measurement.Humidity.ToString()
                    });
                    #endregion HumidityEntriesAdd
                }

                ChartTemperature = new LineChart { Entries = NewTemperatureEntries, LabelTextSize = 28, ValueLabelOrientation = Orientation.Horizontal};
                ChartHumidity = new PointChart { Entries = NewHumidityEntries, LabelTextSize = 28, ValueLabelOrientation = Orientation.Horizontal};
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
