using System.Data;
using DAL;

namespace BUS
{
    public class LaptopBUS
    {
        private readonly LaptopDAL laptopDAL;
        public LaptopBUS(string connectionString)
        {
            laptopDAL = new LaptopDAL(connectionString);
        }
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
        public DataTable SearchLaptopName(string search)
        {
            return laptopDAL.SearchLaptopName(search);
        }
    }
}