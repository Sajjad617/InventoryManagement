using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using InventoryManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Services
{
    public class authService : Iauth
    {
        private readonly EFContext _context;
        private readonly IEncryptPassword _encrypt;
        public authService(EFContext context, IEncryptPassword encrypt)
        {
            _context = context;
            _encrypt = encrypt;
        }


        public async Task<dynamic> registerAuth(authVM userVm)
        {
            try
            {
                var pass =  _encrypt.HashPassword(userVm.Password);
                var user = new authVM
                {
                    Username = userVm.Username,
                    Email = userVm.Email,
                    Password = pass
                };

                await _context.Auth.AddAsync(user); // ✅ entity, not VM
                await _context.SaveChangesAsync();

                return user; // ✅ return entity (or DTO if needed)
            }
            catch (Exception ex)
            {
                throw new Exception("Error while registering user: " + ex.Message);
            }
        }
        public async Task<dynamic> loginAuth(authVM userVm)
        {
            try
            {

                var user = await _context.Auth
                                .FirstOrDefaultAsync(a => a.Username == userVm.Username);

                if (user == null)
                    throw new Exception("User not found");

                bool isValid = _encrypt.VerifyPassword(user.Password, userVm.Password);
                if (!isValid)
                    throw new Exception("Invalid password");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while logging in: " + ex.Message);
            }
        }

    }
}
