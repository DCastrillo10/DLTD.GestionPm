using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.GrupoTrabajo;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DLTD.GestionPm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GrupoTrabajoController : ControllerBase
    {
        private readonly IGrupoTrabajoService _service;

        public GrupoTrabajoController(IGrupoTrabajoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GrupoTrabajoRequest request)
        {
            var response = await _service.AddAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        

        [HttpGet("Pagination")]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        {
            var response = await _service.ListaAsync(request);
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
