using InventoryManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Interface.ServiceInterface
{
    public interface Iauth
    {
        Task<dynamic> registerAuth(authVM userVm);
        Task<dynamic> loginAuth(authVM userVm);

    }
}
