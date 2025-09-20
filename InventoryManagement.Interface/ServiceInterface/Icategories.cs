using InventoryManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Interface.ServiceInterface
{
    public interface Icategories
    {
        Task<dynamic> SaveCategory(categoriesVM CategoryVM);
        Task<dynamic> GetAllCategory();
        Task<dynamic> GetCategorybyId(int id);
        Task<dynamic> UpdateCategorybyId(int id, categoriesVM categoryVM);
        Task<dynamic> DeleteCategorybyId(int id);
    }
}
