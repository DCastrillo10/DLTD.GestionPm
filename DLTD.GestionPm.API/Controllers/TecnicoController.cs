using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DLTD.GestionPm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoService _service;

        public TecnicoController(ITecnicoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TecnicoRequest request)
        {
            var response = await _service.AddAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
