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
        public DataTable SignIn(string email, string password)
        {
            return laptopDAL.SignIn(email, password);
        }
    }
}
