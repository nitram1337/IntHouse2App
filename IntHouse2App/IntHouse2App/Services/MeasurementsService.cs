using IntHouse2App.Models;
using IntHouse2App.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;
using IntHouse2App.Constants;

namespace IntHouse2App.Services
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IGenericRepository _genericRepository;
        public MeasurementsService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
        }

        public async Task<Measurement> GetLatestMeasurementAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}"
            };
            return await _genericRepository.GetAsync<Measurement>(builder.ToString());
        }

        public Task<IEnumerable<Measurement>> GetMeasurementsTimeFilteredAsync(TimeFrame timeFrame)
        {
            throw new NotImplementedException();
        }
    }
}
