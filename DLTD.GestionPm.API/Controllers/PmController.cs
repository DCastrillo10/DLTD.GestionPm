using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Pm;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DLTD.GestionPm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PmController : ControllerBase
    {
        private readonly IPmService _service;

        public PmController(IPmService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PmRequest request)
        {
            var response = await _service.AddMasterDetailsAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PmRequest request)
        {
            var response = await _service.UpdateMasterDetailsAsync(id, request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("tareasactualizar/{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PmDetallesRequest request)
        {
            var response = await _service.UpdateDetailsById(id, request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Pagination")]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest request)
        {
            var response = await _service.ListaAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("existe")]
        public async Task<IActionResult> Get([FromQuery] int idTipopm, [FromQuery] int idModelo, [FromQuery] string NoEquipo, [FromQuery] string WorkOrder)
        {
            var response = await _service.ExistePm(idTipopm, idModelo, NoEquipo, WorkOrder);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("tareas")]
        public async Task<IActionResult> Get(int idTipoPm, int idModelo)
        {
            var response = await _service.FindTareas(idTipoPm, idModelo);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("tareasdetalles/{id:int}")]
        public async Task<IActionResult> GetTareaDetalle(int id)
        {
            var response = await _service.GetDetalleTareaPmById(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);       }

        

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }   

        
    }
}
