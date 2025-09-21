using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class productsController : ControllerBase
    {
        private readonly Iproducts _product;
        public productsController(Iproducts product)
        {
            _product = product;
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct([FromForm] ProductVM productVM)
        {
            var data = await _product.SaveProduct(productVM);
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct(
            [FromQuery] int? categoryId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var data = await _product.GetAllProduct(categoryId, minPrice, maxPrice, page, limit);
                if(data == null)
                {
                    return NoContent();
                }
                return Ok(new
                {
                  Result = data,
                  status=true,
                  stausCode =200
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    message = ex.Message,
                    status = false,
                    statusCode =500
                });
            }
          

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductbyId(int id)
        {
            var data = await _product.GetProductbyId(id);
            return Ok(data);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductbyId(int id, ProductVM productVM)
        {
            var data = await _product.UpdateProductbyId(id, productVM);
            return Ok(data);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductbyId(int id)
        {
            var data = await _product.DeleteProductbyId(id);
            return Ok(data);

        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct([FromQuery] string str, int page = 1, int limit = 10)
        {
            var data = await _product.SearchProduct(str, page, limit);
            return Ok(data);

        }

    }
}
