using IntHouse2App.Models;
using IntHouse2App.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;
using IntHouse2App.Constants;
using Xamarin.Essentials;
using MonkeyCache.SQLite;

namespace IntHouse2App.Services
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IGenericRepository _genericRepository;
        public MeasurementsService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
        }

        #region My own WebAPI
        //public async Task<IEnumerable<Measurement>> GetMeasurementsTimeFilteredAsync(TimeFrame timeFrame)
        //{
        //    UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
        //    {
        //        Path = $"{ApiConstants.ItemsEndpoint}/{timeFrame}"
        //    };

        //    string url = builder.Path;

        //    if (Connectivity.NetworkAccess == NetworkAccess.None)
        //    {
        //        return Barrel.Current.Get<IEnumerable<Measurement>>(key: url);
        //    }
        //    if (!Barrel.Current.IsExpired(key: url))
        //    {
        //        return Barrel.Current.Get<IEnumerable<Measurement>>(key: url);
        //    }

        //    var measurement = await _genericRepository.GetAsync<TimeFrame, IEnumerable<Measurement>>(builder.ToString(), timeFrame);
        //    //Saves the cache and pass it a timespan for expiration
        //    Barrel.Current.Add(key: url, data: measurement, expireIn: TimeSpan.FromSeconds(20));
        //    return measurement;
        //}

        //public async Task<Measurement> GetLatestMeasurementAsync()
        //{
        //    UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
        //    {
        //        Path = $"{ApiConstants.ItemsEndpoint}"
        //    };

        //    string url = builder.Path;

        //    if (Connectivity.NetworkAccess == NetworkAccess.None)
        //    {
        //        return Barrel.Current.Get<Measurement>(key: url);
        //    }
        //    if (!Barrel.Current.IsExpired(key: url))
        //    {
        //        return Barrel.Current.Get<Measurement>(key: url);
        //    }

        //    var measurement = await _genericRepository.GetAsync<Measurement>(builder.ToString());
        //    //Saves the cache and pass it a timespan for expiration
        //    Barrel.Current.Add(key: url, data: measurement, expireIn: TimeSpan.FromSeconds(20));
        //    return measurement;
        //}
        #endregion My own WebAPI

        #region ThingSpeak
        public async Task<Measurement> GetLatestMeasurementAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}",
                Query = "results=1"
            };
            string url = builder.Path;

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return Barrel.Current.Get<Measurement>(key: url);
            }
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<Measurement>(key: url);
            }

            Measurement newMeasurement = new Measurement();

            var measurement = await _genericRepository.GetAsync<Root>(builder.ToString());

            if (measurement != null)
            {
                newMeasurement.Temperature = float.Parse(measurement.feeds[0].field1);
                newMeasurement.Humidity = float.Parse(measurement.feeds[0].field2);
                newMeasurement.TimeCreated = measurement.feeds[0].created_at;
            }

            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: url, data: newMeasurement, expireIn: TimeSpan.FromSeconds(20));
            
            return newMeasurement;
        }

        public async Task<List<Measurement>> GetMeasurementsTimeFilteredAsync(TimeFrame timeFrame)
        {
            #region Switching on TimeFrame
            int queryResults = 0;
            switch (timeFrame)
            {
                case TimeFrame.LatestHour:
                    queryResults = 3;
                    break;
                case TimeFrame.LatestDay:
                    queryResults = 6;
                    break;
                case TimeFrame.LatestWeek:
                    queryResults = 15;
                    break;
                default:
                    break;
            }
            #endregion Switching on TimeFrame

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}",
                Query = $"results={queryResults.ToString()}"
            };

            string url = builder.Path + builder.Query + "List";

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return Barrel.Current.Get<List<Measurement>>(key: url);
            }
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<List<Measurement>>(key: url);
            }

            List<Measurement> newMeasurement = new List<Measurement>();

            var measurement = await _genericRepository.GetAsync<Root>(builder.ToString());
            
            if (measurement != null)
            {
                foreach (var feed in measurement.feeds)
                {
                    newMeasurement.Add(new Measurement
                    {
                        Temperature = float.Parse(feed.field1),
                        Humidity = float.Parse(feed.field2),
                        TimeCreated = feed.created_at
                    });
                }

            }

            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: url, data: newMeasurement, expireIn: TimeSpan.FromSeconds(20));

            return newMeasurement;
        }
        #endregion ThingSpeak
    }
}
