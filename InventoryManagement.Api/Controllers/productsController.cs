using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly Iproducts _product;
        public productsController(Iproducts product)
        {
            _product = product;
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductVM productVM)
        {
            var data = await _product.SaveProduct(productVM);
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var data = await _product.GetAllProduct();
            return Ok(data);

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

    }
}
