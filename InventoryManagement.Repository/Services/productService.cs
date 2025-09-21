using Azure.Core;
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
                string? imageBase64 = null;

                // 🔹 Convert IFormFile to Base64 (optional if you want to store in DB)
                if (productVM.ImageFile != null && productVM.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await productVM.ImageFile.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        imageBase64 = Convert.ToBase64String(fileBytes);
                    }
                }

                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Name == productVM.Name && p.CategoryId == productVM.CategoryId);

                if (existingProduct != null)
                    return new { Message = "Already Existed. Please Update Data" };

                var product = new ProductVM
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Stock = productVM.Stock,
                    CategoryId = productVM.CategoryId,
                    Image = imageBase64
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

        public async Task<dynamic> GetAllProduct(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            int page = 1,
            int limit = 10
            )
        {
            try
            {
                // 🔹 Base query with JOIN (no Include)
                var filteredQuery =
                    from p in _context.Products
                    join c in _context.Categories on p.CategoryId equals c.Id
                    where (!categoryId.HasValue || p.CategoryId == categoryId.Value)
                       && (!minPrice.HasValue || p.Price >= minPrice.Value)
                       && (!maxPrice.HasValue || p.Price <= maxPrice.Value)
                    orderby p.Id
                    select new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Stock,
                        p.Description,
                        Category = new
                        {
                            c.Id,
                            c.Name,
                            c.Description
                        }
                    };

                // 🔹 Count total items
                var totalItems = await filteredQuery.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)limit);

                // 🔹 Apply pagination
                var products = await (
                    from p in filteredQuery
                    select p
                )
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

                return new
                {
                    Page = page,
                    Limit = limit,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Data = products
                };
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

                string? imageBase64 = null;

                // 🔹 Convert IFormFile to Base64 (optional if you want to store in DB)
                if (productVM.ImageFile != null && productVM.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await productVM.ImageFile.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        imageBase64 = Convert.ToBase64String(fileBytes);
                    }
                }

                product.Name = productVM.Name;
                product.Description = productVM.Description;
                product.Price = productVM.Price;
                product.Stock = productVM.Stock;
                product.CategoryId = productVM.CategoryId;
                product.Image = imageBase64;

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
        public async Task<dynamic> SearchProduct(string str, int page = 1, int limit = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                    return new { message = "Search keyword Something text is required." };

                var query =
                    from p in _context.Products
                    join c in _context.Categories on p.CategoryId equals c.Id
                    where p.Name.Contains(str) || p.Description.Contains(str)
                    orderby p.Id
                    select new
                    {
                        ProductId = p.Id,
                        p.Name,
                        p.Description,
                        p.Price,
                        p.Stock,
                        Category = new
                        {
                            CategoryId = c.Id,
                            c.Name,
                            c.Description
                        }
                    };

                // for Pagination
                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)limit);

                var products = await query
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

                return new
                {
                    Page = page,
                    Limit = limit,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Data = products
                };
            }
            catch (Exception ex)
            {
                return new { message = "Error while searching products", error = ex.Message };
            }
        }
        

        

    }
}
