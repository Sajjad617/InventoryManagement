using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly Iauth _auth;

        public authController(Iauth auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> registerAuth(authVM userVm)
        {
            var data = await _auth.registerAuth(userVm);
            return Ok(data);

        }
        [HttpPost("login")]
        public async Task<IActionResult> loginAuth(authVM userVm)
        {
            var data = await _auth.loginAuth(userVm);
            return Ok(data);

        }
    }
}
