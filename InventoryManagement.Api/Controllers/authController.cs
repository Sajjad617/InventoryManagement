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
            try
            {
                var data = await _auth.registerAuth(userVm);

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
        [HttpPost("login")]
        public async Task<IActionResult> loginAuth(authVM userVm)
        {
            try
            {
                var data = await _auth.loginAuth(userVm);

                if (data == null)
                {
                    return Unauthorized(new
                    {
                        message = "Invalid username or password",
                        status = false,
                        statusCode = 401
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
    }
}
