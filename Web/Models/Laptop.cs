using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laptop_store.Models
{
    public class Laptop 
    {
        public int LaptopID { get; set; }
        public string LaptopName { get; set; }
        public string LaptopCPU { get; set; }
        public string LaptopGPU { get; set; }
        public string LaptopRAM { get; set; }
        public string LaptopStorage { get; set; }
        public string LaptopDisplay { get; set; }
        public double LaptopPrice { get; set; }
        public int LaptopQuantity { get; set; }
        public int LaptopOrderQuantity { get; set; }
        public DateTime LaptopImportDate { get; set; }
        public float LaptopDiscountPercentage { get; set; }
        public string LaptopImage { get; set; }
    }
}
