using IntHouse2App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IntHouse2App.ViewModels
{
    public class OverviewViewModel : BaseViewModel
    {
        #region Properties

        float _temperature;
        public float Temperature
        {
            get => _temperature;
            set { SetProperty(ref _temperature, value); }
        }

        float _humidity;
        public float Humidity
        {
            get => _humidity;
            set { SetProperty(ref _humidity, value); }
        }

        DateTime _timeCreated;
        public DateTime TimeCreated
        {
            get => _timeCreated;
            set { SetProperty(ref _timeCreated, value); }
        }

        #endregion Properties


        public Command LoadMeasurementCommand { get; }

        public OverviewViewModel()
        {
            Title = "Overview";

            LoadMeasurementCommand = new Command(async () => await ExecuteLoadMeasurementCommand());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async Task ExecuteLoadMeasurementCommand()
        {
            IsBusy = true;

            try
            {
                var measurement = await _measurementsService.GetLatestMeasurementAsync();
                Temperature = measurement.Temperature;
                Humidity = measurement.Humidity;
                TimeCreated = measurement.TimeCreated;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
        }

        public void Dispose()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
    }
}
