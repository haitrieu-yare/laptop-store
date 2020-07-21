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
            bool result = true;
            string SQL = "Insert Into tblOrder Values(@ID, @Email, @Price, GETDATE())";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = newOrder.Rows[0]["OrderID"];
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = newOrder.Rows[0]["UserEmail"];
                cmd.Parameters.Add("@Price", SqlDbType.Float).Value = newOrder.Rows[0]["OrderPrice"];
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            } finally {
                _conn.Close();
            }
            return result;
        }
        public bool AddNewOrderUnit(DataTable newOrderUnit)
        {
            bool result = true;
            string SQL = "Insert Into tblOrderUnit Values(@OrderUnitID, @OrderID, @LaptopID, @Quantity, @Price)";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                for (int i = 0; i < newOrderUnit.Rows.Count; i++)
                {
                    cmd.Parameters.Add("@OrderUnitID", SqlDbType.Int).Value = newOrderUnit.Rows[i]["OrderUnitID"];
                    cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = newOrderUnit.Rows[i]["OrderID"];
                    cmd.Parameters.Add("@LaptopID", SqlDbType.Int).Value = newOrderUnit.Rows[i]["LaptopID"];
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = newOrderUnit.Rows[i]["Quantity"];
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = newOrderUnit.Rows[i]["Price"];
                    result = cmd.ExecuteNonQuery() > 0; 
                    if (!result)
                    {
                        break;
                    } else
                    {
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return result;
        }
        public DataTable GetOrder(int begin, int end, string userEmail)
        {
            DataTable listOrder = new DataTable();
            string SQL = "Select * From tblOrder Where (OrderID Between @Begin And @End) And UserEmail=@UserEmail";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@Begin", SqlDbType.Int).Value = begin;
                cmd.Parameters.Add("@End", SqlDbType.Int).Value = end;
                cmd.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = userEmail;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(listOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return listOrder;
        }
        public DataTable GetOrderUnit(int orderID)
        {
            DataTable orderUnitList = new DataTable();
            string SQL = "Select LaptopID, Quantity, Price From tblOrderUnit Where OrderID=@OrderID";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(orderUnitList);
                // Get Laptop Name
                LaptopDAL laptopDAL = new LaptopDAL();
                orderUnitList.Columns.Add("LaptopName");
                for (int i = 0; i < orderUnitList.Rows.Count; i++)
                {
                    orderUnitList.Rows[i]["LaptopName"] = laptopDAL.GetLaptopName((int)orderUnitList.Rows[i]["LaptopID"]);
                }
                orderUnitList.Columns.Remove("LaptopID");
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return orderUnitList;
        }
    }
}
