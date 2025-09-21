using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using InventoryManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Services
{
    public class categoriesService : Icategories
    {
        private readonly EFContext _context;

        public categoriesService(EFContext context)
        {
            _context = context;
        }

        //  Create
        public async Task<dynamic> SaveCategory(categoriesVM categoryVM)
        {

            try
            {
                var category = new categoriesVM
                {
                    Name = categoryVM.Name,
                    Description = categoryVM.Description
                };

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Get All
        public async Task<dynamic> GetAllCategory()
        {
            try
            {
                 var categories = await _context.Categories
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Description,
                ProductCount = _context.Products.Count(p => p.CategoryId == c.Id) // count products in this category
            })
            .ToListAsync();

             return categories;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Get by Id
        public async Task<dynamic> GetCategorybyId(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                    throw new Exception("Category not found");

                return category;
            }
            catch (Exception ex)
            {
                return new { Message = ex.Message };
            }
        }

        //  Update
        public async Task<dynamic> UpdateCategorybyId(int id, categoriesVM categoryVM)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                    throw new Exception("Category not found");

                category.Name = categoryVM.Name;
                category.Description = categoryVM.Description;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //  Delete
        public async Task<dynamic> DeleteCategorybyId(int id)
        {
            try
            {
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.CategoryId == id);

                if (existingProduct != null)
                    return new { Message = "There is a Data in this Category" };

                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                    throw new Exception("Category not found");

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new { Message = "Category deleted successfully" };
            }
            catch (Exception ex)
            {
                return new { Message = ex.Message };
            }
        }
    }
}
