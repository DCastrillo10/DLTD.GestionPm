using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Ruta;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DLTD.GestionPm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RutaController : ControllerBase
    {
        private readonly IRutaService _service;

        public RutaController(IRutaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RutaRequest request)
        {
            var response = await _service.AddAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] RutaRequest request)
        {
            var response = await _service.UpdateAsync(id,request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Pagination")]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        {
            var response = await _service.ListaAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> Get()
        {
            var response = await _service.ListaSelectAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
