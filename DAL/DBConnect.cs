using System;
using System.Data.SqlClient;

namespace DAL
{
    public class DBConnect
    {
        private const string ConnectionString = "server=OPENSKY\\SQLEXPRESS;database=LaptopStore;uid=sa;pwd=123";
        protected SqlConnection _conn = new SqlConnection(ConnectionString);
    }
}
