﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laptop_store.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserEmail { get; set; }
        public float OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
