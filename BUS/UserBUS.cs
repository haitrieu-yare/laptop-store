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
    }
}
