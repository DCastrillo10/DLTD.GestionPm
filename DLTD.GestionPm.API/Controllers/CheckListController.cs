using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.CheckList;
using DLTD.GestionPm.Negocios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DLTD.GestionPm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckListController : ControllerBase
    {
        private readonly ICheckListService _service;

        public CheckListController(ICheckListService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CheckListRequest request)
        {
            var response = await _service.AddMasterDetailsAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CheckListRequest request)
        {
            var response = await _service.UpdateMasterDetailsAsync(id, request);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
