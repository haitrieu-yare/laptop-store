using DAL;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Data;
using System.Security.Cryptography;

namespace BUS
{
    public class UserBUS
    {
        private readonly UserDAL userDAL = new UserDAL();
        public DataTable SignIn(string email, string password)
        {
            Byte[] salt = GetPasswordSalt(email);
            password = HashPassword(password, salt);
            return userDAL.SignIn(email, password);
        }
        public bool SignUp(string email, string password)
        {
            Byte[] salt = CreateSalt();
            password = HashPassword(password, salt);
            return userDAL.SignUp(email, password, salt);
        }
        public bool UpdateProfile(string userEmail, string userName, string userAddress, string userPhone)
        {
            return userDAL.UpdateProfile(userEmail, userName, userAddress, userPhone);
        }
        public byte[] GetPasswordSalt(string email)
        {
            return userDAL.GetPasswordSalt(email);
        }
        public byte[] CreateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public string HashPassword(string password, byte[] salt)
        {
            
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
