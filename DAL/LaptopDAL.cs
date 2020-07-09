using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class LaptopDAL : DBConnect
    {
        // GetLaptopImage
        public List<string> GetLaptopImage()
        {
            List<string> ImageList = new List<string>();
            string SQL = "Select LaptopImage From tblLaptop";
            try
            {
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ImageList.Add((string)reader["LaptopImage"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return ImageList;
        }
    } // End Class
} // End Namespace
