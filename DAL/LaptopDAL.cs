using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class LaptopDAL : DBConnect
    {
        // Get Laptop Preview Info
        public DataTable GetLaptopPreviewInfo()
        {
            DataTable previewInfo = new DataTable();
            string SQL = "Select LaptopID, LaptopName, LaptopPrice, LaptopDiscountPercentage, LaptopImage From tblLaptop";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(SQL, _conn);
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                adapter.Fill(previewInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return previewInfo;
        }
        // Get Laptop Detail
        public DataTable GetLaptopDetail(int laptopID)
        {
            DataTable laptopDetail = new DataTable();
            string SQL = "Select LaptopCPU, LaptopGPU, LaptopRAM, LaptopStorage, LaptopDisplay From tblLaptop Where LaptopID=@ID";
            try
            {
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = laptopID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                adapter.Fill(laptopDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return laptopDetail;
        }
    } // End Class
} // End Namespace
