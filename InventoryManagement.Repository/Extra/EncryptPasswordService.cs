using InventoryManagement.Interface.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Extra
{
    public class EncryptPasswordService : IEncryptPassword
    {
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        // Hash password
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        // Verify password
        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
