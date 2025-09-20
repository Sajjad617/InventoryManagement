using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Interface.ServiceInterface
{
    public interface IEncryptPassword
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string hashedPassword, string password);
    }
}
