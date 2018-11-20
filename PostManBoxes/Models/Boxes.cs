using PostManBoxes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostManBoxes.Models
{
    public class Boxes
    {
        public long Id { get; set; }
        public string CustomerKey { get; set; }
        public string PackNumber { get; set; }
        public short Dimensions { get; set; }
        public decimal Weight { get; set; }
        public decimal ToPay { get; set; }
        public long DestinationId { get; set; }
        public int CurrentCheckPoint { get; set; }
        public byte DeliveryStatus { get; set; }
    }
}