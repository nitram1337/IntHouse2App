using IntHouse2App.Models;
using IntHouse2App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TinyIoC;
using Xamarin.Forms;

namespace IntHouse2App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        protected readonly IMeasurementsService _measurementsService;
        public BaseViewModel()
        {
            _measurementsService = TinyIoCContainer.Current.Resolve<IMeasurementsService>();
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        bool isConnected;
        public bool IsConnected                     //
        {
            get => isConnected;
            set { SetProperty(ref isConnected, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
