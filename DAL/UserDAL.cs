using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class UserDAL : DBConnect
    {
        // Check UserID
        public int CheckUserCount()
        {
            int count = 0;
            string SQL = "Select Count(UserEmail) As UserEmail From tblUser ";
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
                    count = (int)reader["UserEmail"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                _conn.Close();
            }
            return count;
        }
        // Sign In
        public DataTable SignIn(string email, string password)
        {
            DataTable userDetail = new DataTable();
            String SQL = "Select UserName, UserRole, UserAddress, UserPhone From tblUser Where UserEmail=@Email AND UserPassword=@Password";
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
            }
            finally
            {
                _conn.Close();
            }
            return userDetail;
        }
        // Sign Up
        public bool SignUp(string email, string password)
        {
            bool result = false;
            int newUserNumber = CheckUserCount() + 1;
            string userName = "User" + newUserNumber;
            string SQL = "Insert tblUser Values(@Email,@Name,'User',@Password,null,null)";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = userName;
                result = cmd.ExecuteNonQuery() > 0;
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
    }
}
