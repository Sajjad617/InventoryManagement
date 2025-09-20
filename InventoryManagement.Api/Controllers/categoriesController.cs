using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriesController : ControllerBase
    {
        private readonly Icategories _category;
        public categoriesController(Icategories category)
        {
            _category = category;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(categoriesVM CategoryVM)
        {
            var data = await _category.SaveCategory(CategoryVM);
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var data = await _category.GetAllCategory();
            return Ok(data);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategorybyId(int id)
        {
            var data = await _category.GetCategorybyId(id);
            return Ok(data);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategorybyId(int id, categoriesVM CategoryVM)
        {
            var data = await _category.UpdateCategorybyId(id, CategoryVM);
            return Ok(data);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorybyId(int id)
        {
            var data = await _category.DeleteCategorybyId(id);
            return Ok(data);

        }

    }
}
