using Azure.Core;
using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using InventoryManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Services
{
    public class authService : Iauth
    {
        private readonly EFContext _context;
        private readonly IEncryptPassword _encrypt;
        private readonly IConfiguration _config;
        public authService(EFContext context, IEncryptPassword encrypt,IConfiguration config)
        {
            _context = context;
            _encrypt = encrypt;
            _config = config;
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
                var token = GenerateJwtToken(user.Username);

                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error while logging in: " + ex.Message);
            }
        }



        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }



}
