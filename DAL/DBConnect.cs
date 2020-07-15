using System;
using System.Data.SqlClient;

namespace DAL
{
    public class DBConnect
    {
        //private const string ConnectionString = "server=.\\SQLEXPRESS;database=LaptopStore;uid=sa;pwd=123";
        private const string ConnectionString = "server=tcp:phuonglndatabase.database.windows.net;Database=LaptopStore;uid=phuongextra;pwd=Orichimarus6";
        protected SqlConnection _conn = new SqlConnection(ConnectionString);
    }
}
