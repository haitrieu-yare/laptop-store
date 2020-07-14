using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class UserDAL : DBConnect
    {
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
            }
            finally
            {
                _conn.Close();
            }
            return userDetail;
        }
    }
}
