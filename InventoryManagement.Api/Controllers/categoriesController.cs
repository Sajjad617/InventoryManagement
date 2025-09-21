using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            try
            {
                var data = await _category.SaveCategory(CategoryVM);

                return Ok(new
                {
                    result = data,
                    status = true,
                    statusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    status = false,
                    statusCode = 500
                });
            }

        }
        [HttpGet]
      
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var data = await _category.GetAllCategory();

                if (data == null)
                {
                    return NoContent();
                }

                return Ok(new
                {
                    result = data,
                    status = true,
                    statusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    status = false,
                    statusCode = 500
                });
            }

        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetCategorybyId(int id)
        {
            try
            {
                var data = await _category.GetCategorybyId(id);

                if (data == null)
                {
                    return NotFound(new
                    {
                        message = $"Category with Id {id} not found",
                        status = false,
                        statusCode = 404
                    });
                }

                return Ok(new
                {
                    result = data,
                    status = true,
                    statusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    status = false,
                    statusCode = 500
                });
            }

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCategorybyId(int id, categoriesVM CategoryVM)
        {
            try
            {
                var data = await _category.UpdateCategorybyId(id, CategoryVM);

                if (data == null)
                {
                    return NotFound(new
                    {
                        message = $"Category with Id {id} not found",
                        status = false,
                        statusCode = 404
                    });
                }

                return Ok(new
                {
                    result = data,
                    status = true,
                    statusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    status = false,
                    statusCode = 500
                });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorybyId(int id)
        {
            try
            {
                var data = await _category.DeleteCategorybyId(id);

                if (data == null)
                {
                    return NotFound(new
                    {
                        message = $"Category with Id {id} not found",
                        status = false,
                        statusCode = 404
                    });
                }

                return Ok(new
                {
                    message = $"Category with Id {id} deleted successfully",
                    status = true,
                    statusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    status = false,
                    statusCode = 500
                });
            }

        }

    }
}
