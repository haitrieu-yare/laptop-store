using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BUS
{
    public class UserBUS
    {
        private readonly UserDAL userDAL = new UserDAL();
        public DataTable SignIn(string email, string password)
        {
            return userDAL.SignIn(email, password);
        }
        public bool SignUp(string email, string password)
        {
            return userDAL.SignUp(email, password);
        }
        public bool UpdateProfile(string userEmail, string userName, string userAddress, string userPhone)
        {
            return userDAL.UpdateProfile(userEmail, userName, userAddress, userPhone);
        }
    }
}
