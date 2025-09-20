using InventoryManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Interface.ServiceInterface
{
    public interface Iproducts
    {
        Task<dynamic> SaveProduct(ProductVM productVM);
        Task<dynamic> GetAllProduct();
        Task<dynamic> GetProductbyId(int id);
        Task<dynamic> UpdateProductbyId(int id, ProductVM productVM);
        Task<dynamic> DeleteProductbyId(int id);

    }
}
