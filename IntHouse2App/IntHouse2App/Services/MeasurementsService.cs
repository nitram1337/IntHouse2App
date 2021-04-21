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

        public async Task<IEnumerable<Measurement>> GetMeasurementsTimeFilteredAsync(TimeFrame timeFrame)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}/{timeFrame}"
            };

            string url = builder.Path;

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return Barrel.Current.Get<IEnumerable<Measurement>>(key: url);
            }
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<IEnumerable<Measurement>>(key: url);
            }

            var measurement = await _genericRepository.GetAsync<TimeFrame, IEnumerable<Measurement>>(builder.ToString(), timeFrame);
            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: url, data: measurement, expireIn: TimeSpan.FromSeconds(20));
            return measurement;
        }

        public async Task<Measurement> GetLatestMeasurementAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}"
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

            var measurement = await _genericRepository.GetAsync<Measurement>(builder.ToString());
            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: url, data: measurement, expireIn: TimeSpan.FromSeconds(20));
            return measurement;
        }
    }
}
