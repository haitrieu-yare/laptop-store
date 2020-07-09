using DAL;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class LaptopBUS
    {
        public List<string> GetLaptopImage()
        {
            LaptopDAL laptopDAL = new LaptopDAL();
            return laptopDAL.GetLaptopImage();
        }
    }
}
