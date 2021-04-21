﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class DataStore : IDataStore<Measurement>
    {
        readonly List<Measurement> measurements;

        public DataStore()
        {
            measurements = new List<Measurement>()
            {
                new Measurement { Temperature = 20.0f, Humidity = 80.0f, TimeCreated = new DateTime(2021, 4, 21, 10, 00, 00) },
                new Measurement { Temperature = 21.0f, Humidity = 85.0f, TimeCreated = new DateTime(2021, 4, 21, 10, 05, 00) },
                new Measurement { Temperature = 23.0f, Humidity = 90.0f, TimeCreated = new DateTime(2021, 4, 21, 10, 10, 00) },
                new Measurement { Temperature = 24.0f, Humidity = 94.0f, TimeCreated = new DateTime(2021, 4, 21, 10, 15, 00) },
                new Measurement { Temperature = 28.0f, Humidity = 60.0f, TimeCreated = new DateTime(2021, 4, 22, 05, 15, 00) },
                new Measurement { Temperature = 29.0f, Humidity = 70.0f, TimeCreated = new DateTime(2021, 4, 22, 06, 15, 00) },
                new Measurement { Temperature = 31.0f, Humidity = 80.0f, TimeCreated = new DateTime(2021, 4, 22, 07, 15, 00) },
            };
        }

        public async Task<Measurement> GetMeasurementAsync()
        {
            return await Task.FromResult(measurements.OrderByDescending(g => g.TimeCreated).FirstOrDefault());
        }

        public async Task<IEnumerable<Measurement>> GetMeasurementsAsync(TimeFrame timeFrame, bool forceRefresh = false)
        {
            switch (timeFrame)
            {
                case TimeFrame.LatestHour:
                    return await Task.FromResult(measurements.Where(m => m.TimeCreated >= DateTime.Now.AddHours(-1) && m.TimeCreated <= DateTime.Now));
                case TimeFrame.LatestDay:
                    return await Task.FromResult(measurements.Where(m => m.TimeCreated >= DateTime.Now.AddDays(-1) && m.TimeCreated <= DateTime.Now));
                case TimeFrame.LatestWeek:
                    return await Task.FromResult(measurements.Where(m => m.TimeCreated >= DateTime.Now.AddDays(-7) && m.TimeCreated <= DateTime.Now));
                default:
                    return await Task.FromResult(measurements);
            }
        }
    }
}