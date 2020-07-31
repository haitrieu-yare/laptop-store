﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class LaptopDAL
    {
        SqlConnection _conn;
        private readonly string _connectionString;
        public LaptopDAL(string connectionString)
        {
            _connectionString = connectionString;
            _conn = new SqlConnection(connectionString);
        }
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
            }
            finally
            {
                _conn.Close();
            }
            return previewInfo;
        }
        // Get Laptop Detail
        public DataTable GetLaptopDetail(int laptopID)
        {
            DataTable laptopDetail = new DataTable();
            string SQL = "Select LaptopCPU, LaptopName, LaptopPrice, LaptopDiscountPercentage, LaptopImage, " +
                "LaptopGPU, LaptopRAM, LaptopStorage, LaptopDisplay From tblLaptop Where LaptopID=@ID";
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
            }
            finally
            {
                _conn.Close();
            }
            return laptopDetail;
        }
        // Get Laptop Quantity
        public int GetLaptopQuantity(int laptopID)
        {
            int laptopQuantity = 0;
            string SQL = "Select LaptopQuantity From tblLaptop Where LaptopID=@ID";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = laptopID;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    laptopQuantity = (int) reader["LaptopQuantity"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return laptopQuantity;
        }
        // Update LaptopQuantity
        public bool UpdateLaptopQuantity(int laptopID, int laptopNewQuantity)
        {
            bool result = true;
            string SQL = "Update tblLaptop Set LaptopQuantity=@LaptopNewQuantity Where LaptopID=@LaptopID";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@LaptopNewQuantity", SqlDbType.Int).Value = laptopNewQuantity;
                cmd.Parameters.Add("@LaptopID", SqlDbType.Int).Value = laptopID;
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }
        // Get Laptop Name
        public string GetLaptopName(int laptopID)
        {
            string laptopName = null;
            string SQL = "Select LaptopName From tblLaptop Where LaptopID=@ID";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = laptopID;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    laptopName = (string) reader["LaptopName"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return laptopName;
        }
        // Search Laptop by Name
        public DataTable SearchLaptopName(string search)
        {
            DataTable listSearchLaptop = new DataTable();
            string SQL = "Select LaptopID, LaptopName, LaptopPrice From tblLaptop Where LaptopName Like '%' + @LaptopName + '%'";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@LaptopName", SqlDbType.VarChar).Value = search; //"%" + search + "%"
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(listSearchLaptop);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return listSearchLaptop;
        }
    } // End Class
} // End Namespace