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
    public class productService : Iproducts
    {
        private readonly EFContext _context;
        public productService(EFContext context)
        {
            _context = context;

        }

        public async Task<dynamic> SaveProduct(ProductVM productVM)
        {
            try
            {
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Name == productVM.Name);

                if (existingProduct != null)
                    return new { Message = "Already Existed This Data Please Update Data" };

                var product = new ProductVM
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Stock = productVM.Stock,
                    CategoryId = productVM.CategoryId,
                    Image = productVM.Image
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving product: " + ex.Message);
            }
        }

        public async Task<dynamic> GetAllProduct()
        {
            try
            {
                return await _context.Products.ToListAsync();
                //var productsWithCategory = await (from p in _context.Products
                //                                  join c in _context.Categories
                //                                  on p.CategoryId equals c.Id
                //                                  select new
                //                                  {
                //                                      ProductId = p.Id,
                //                                      ProductName = p.Name,
                //                                      CategoryId = c.Id,
                //                                      CategoryName = c.Name
                //                                  }).ToListAsync();

                //return productsWithCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching products: " + ex.Message);
            }
        }

        public async Task<dynamic> GetProductbyId(int id)
        {
            try
            {
                var productWithCategory = await (from p in _context.Products
                                                 join c in _context.Categories
                                                 on p.CategoryId equals c.Id
                                                 where p.Id == id
                                                 select new
                                                 {
                                                     ProductId = p.Id,
                                                     ProductName = p.Name,
                                                     CategoryId = c.Id,
                                                     CategoryName = c.Name
                                                 }).FirstOrDefaultAsync();

                if (productWithCategory == null)
                    throw new Exception("Product not found");

                return productWithCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching product: " + ex.Message);
            }
        }

        public async Task<dynamic> UpdateProductbyId(int id, ProductVM productVM)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    throw new Exception("Product not found");

                product.Name = productVM.Name;
                product.Description = productVM.Description;
                product.Price = productVM.Price;
                product.Stock = productVM.Stock;
                product.CategoryId = productVM.CategoryId;
                product.Image = productVM.Image;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating product: " + ex.Message);
            }
        }

        public async Task<dynamic> DeleteProductbyId(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    throw new Exception("Product not found");

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return new { Message = "Product deleted successfully" };
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting product: " + ex.Message);
            }
        }

        

    }
}
