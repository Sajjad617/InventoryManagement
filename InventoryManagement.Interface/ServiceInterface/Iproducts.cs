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
        Task<dynamic> GetAllProduct(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            int page = 1,
            int limit = 10
            );
        Task<dynamic> GetProductbyId(int id);
        Task<dynamic> UpdateProductbyId(int id, ProductVM productVM);
        Task<dynamic> DeleteProductbyId(int id);
        Task<dynamic> SearchProduct(string str, int page = 1, int limit = 10);

    }
}
