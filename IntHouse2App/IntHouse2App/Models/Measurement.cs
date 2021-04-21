using System;
using System.Collections.Generic;
using System.Text;

namespace IntHouse2App.Models
{
    public class Measurement
    {
        public DateTime TimeCreated { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}
