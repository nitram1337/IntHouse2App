using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : Controller
    {
        private readonly IDataStore<Measurement> _dataService;

        public MeasurementsController(IDataStore<Measurement> dataService)
        {
            _dataService = dataService;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public async Task<ActionResult<Measurement>> Get()
        {
            return Ok(await _dataService.GetMeasurementAsync());
        }

        // GET api/<ItemController>/5
        [HttpGet("{timeFrame}", Name = "Get")]
        public async Task<ActionResult<IEnumerable<Measurement>>> Get(TimeFrame timeFrame)
        {
            //if (timeFrame == null)
            //{
            //    return NotFound();
            //}
            return Ok(await _dataService.GetMeasurementsAsync(timeFrame));
        }
    }
}
