using IntHouse2App.Models;
using IntHouse2App.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace IntHouse2App.Services
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IGenericRepository _genericRepository;
        public MeasurementsService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
        }

        public Task<Measurement> GetLatestMeasurement()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Measurement>> GetMeasurementsTimeFiltered(TimeFrame timeFrame)
        {
            throw new NotImplementedException();
        }
    }
}
