using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Tarea;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Tarea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ITareaService
    {
        Task<BaseResponse> AddAsync(TareaRequest request);
        Task<BaseResponse> UpdateAsync(int id, TareaRequest request);
        Task<BaseResponse<TareaResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaTareaResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<TareaResponse>> DeleteAsync(int id);
    }
}
