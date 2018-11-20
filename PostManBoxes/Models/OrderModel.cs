using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostManBoxes.Models
{
    public class OrderModel
    {

        public int CheckPointID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Post_code { get; set; }
        public string Street { get; set; }
        public string House_number { get; set; }
        public string Apartment_number { get; set; }
        public string Recipient_name { get; set; }
        public string Recipient_surname { get; set; }
        public string CustomerKey { get; set; }
        public string PackNumber { get; set; }
        public short Dimensions { get; set; }
        public decimal Weight { get; set; }
        public decimal ToPay { get; set; }
        
    }
}