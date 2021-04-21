using IntHouse2App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntHouse2App.Services
{
    public interface IMeasurementsService
    {
        Task<Measurement> GetLatestMeasurementAsync();
        Task<IEnumerable<Measurement>> GetMeasurementsTimeFilteredAsync(TimeFrame timeFrame);
    }
}
