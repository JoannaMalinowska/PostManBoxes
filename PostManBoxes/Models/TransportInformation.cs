using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostManBoxes.Models
{
    public class TransportInformation
    {

        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public byte Delivery_status { get; set; }

    }
}