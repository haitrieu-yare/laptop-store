using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class OrderDAL : DBConnect
    {
        public int GetOrderID()
        {
            int orderID = 0;
            string SQL = "Select Count(OrderID) As OrderID From tblOrder";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    orderID = (int)reader["OrderID"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return orderID;
        }
        public int GetOrderUnitID()
        {
            int orderUnitID = 0;
            string SQL = "Select Count(OrderUnitID) As OrderUnitID From tblOrderUnit";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    orderUnitID = (int)reader["OrderUnitID"];
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
            return orderUnitID;
        }
        public bool AddNewOrder(DataTable newOrder)
        {
            bool result = false;

            return result;
        }
        public bool AddNewOrderUnit(DataTable newOrder)
        {
            bool result = false;

            return result;
        }
    }
}
