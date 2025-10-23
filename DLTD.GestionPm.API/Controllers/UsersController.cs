using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DLTD.GestionPm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISecurityService _service;

        public UsersController(ISecurityService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _service.Login(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
