using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laptop_store.Models
{
    public class OrderUnit
    {
        public int OrderUnitID { get; set; }
        public int OrderID { get; set; }
        public int LaptopID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
