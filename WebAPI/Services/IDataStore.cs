using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IDataStore<T>
    {
        Task<Measurement> GetMeasurementAsync();
        Task<IEnumerable<Measurement>> GetMeasurementsAsync(TimeFrame timeFrame, bool forceRefresh = false);
    }
}
