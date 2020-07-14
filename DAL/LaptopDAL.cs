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
        // Sign In
        public DataTable SignIn(string email, string password)
        {
            DataTable userDetail = new DataTable();
            String SQL = "Select UserID, UserName, UserRole, UserAddress, UserPhone From tblUser Where UserEmail=@Email AND UserPassword=@Password";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(userDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return userDetail;
        }
    } // End Class
} // End Namespace
