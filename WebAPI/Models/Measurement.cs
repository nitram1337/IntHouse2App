using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Measurement
    {
        public DateTime TimeCreated { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}
