using DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace BUS
{
    public class LaptopBUS
    {
        private readonly LaptopDAL laptopDAL = new LaptopDAL();
        public DataTable GetLaptopPreviewInfo()
        {
            return laptopDAL.GetLaptopPreviewInfo();
        }
        public DataTable GetLaptopDetail(int laptopID)
        {
            return laptopDAL.GetLaptopDetail(laptopID);
        }
        public int GetLaptopQuantity(int laptopID)
        {
            return laptopDAL.GetLaptopQuantity(laptopID);
        }
        public bool UpdateLaptopQuantity(int laptopID, int laptopNewQuantity)
        {
            return laptopDAL.UpdateLaptopQuantity(laptopID, laptopNewQuantity);
        }
        public string GetLaptopName(int laptopID)
        {
            return laptopDAL.GetLaptopName(laptopID);
        }
    }
}
