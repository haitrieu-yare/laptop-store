using System.Data.SqlClient;

namespace DAL
{
    public abstract class DBConnect
    {
        private const string ConnectionString = "server=.\\SQLEXPRESS;database=LaptopStore;uid=sa;pwd=123";
        protected SqlConnection _conn = new SqlConnection(ConnectionString);
    }
}
