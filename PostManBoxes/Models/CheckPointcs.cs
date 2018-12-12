using PostManBoxes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostManBoxes.Models
{
    public class CheckPointcs
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}