using ExpenseTrackerAPI.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTrackerAPI.Utilities
{
    public class PasswordHashing
    {
        private readonly string _salt;

        public PasswordHashing(IOptions<PasswordSalt> options)
        {
            _salt = options.Value.Salt;
        }

        //hashing password using salt
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var saltedPassword = password + _salt;
            var bytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hashedpassword)
        {
           return HashPassword(password) == hashedpassword;
        }
    }
}
