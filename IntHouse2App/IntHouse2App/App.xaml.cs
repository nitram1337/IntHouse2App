using IntHouse2App.Repository;
using IntHouse2App.Services;
using IntHouse2App.Views;
using System;
using TinyIoC;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntHouse2App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MonkeyCache.SQLite.Barrel.ApplicationId = "MyApp";

            var container = TinyIoCContainer.Current;
            container.Register<IMeasurementsService, MeasurementsService>();
            container.Register<IGenericRepository, GenericRepository>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
