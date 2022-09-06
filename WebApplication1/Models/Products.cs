using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Products
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public string status { get; set; }
        public string inventory { get; set; }
        public string warehouses { get; set; }

    }
}
