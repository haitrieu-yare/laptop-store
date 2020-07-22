using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class UserDAL : DBConnect
    {
        public bool CheckAccountExist(string email)
        {
            bool result = true;
            String SQL = "Select Top 1 UserEmail From tblUser Where UserEmail=@Email";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                result = cmd.ExecuteScalar() != null;
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
        public int CheckUserCount()
        {
            int count = 0;
            string SQL = "Select Count(UserEmail) As UserEmail From tblUser";
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
        // Get PasswordSalt
        public Byte[] GetPasswordSalt(string email)
        {
            Byte[] salt = null;
            String SQL = "Select UserPasswordSalt From tblUser Where UserEmail=@Email";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    salt = (byte[])reader["UserPasswordSalt"];
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
            return salt;
        }
        // Sign In
        public DataTable SignIn(string email, string password)
        {
            DataTable userDetail = new DataTable();
            String SQL = "Select UserName, UserAddress, UserPhone From tblUser Where UserEmail=@Email AND UserPassword=@Password";
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
        public bool SignUp(string email, string password, byte[] salt)
        {
            bool result = false;
            int newUserNumber = CheckUserCount() + 1;
            string userName = "User" + newUserNumber;
            string SQL = "Insert tblUser Values(@Email,@Name,@Password,@Salt,null,null)";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = userName;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                cmd.Parameters.Add("@Salt", SqlDbType.VarBinary).Value = salt;
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
        public bool UpdateProfile(string userEmail, string userName, string userAddress, string userPhone)
        {
            bool result = true;
            try
            {
                string SQL = "Update tblUser Set UserName=@UserName, UserAddress=@UserAddress, UserPhone=@UserPhone Where UserEmail=@UserEmail";
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                cmd.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = userEmail;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                cmd.Parameters.Add("@UserAddress", SqlDbType.VarChar).Value = userAddress;
                cmd.Parameters.Add("@UserPhone", SqlDbType.VarChar).Value = userPhone;
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
