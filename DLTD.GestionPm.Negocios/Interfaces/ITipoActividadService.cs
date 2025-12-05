using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoActividad;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoActividad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ITipoActividadService
    {
        Task<BaseResponse> AddAsync(TipoActividadRequest request);
        Task<BaseResponse> UpdateAsync(int id, TipoActividadRequest request);
        Task<BaseResponse<TipoActividadResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaTipoActividadResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoActividadResponse>>> ListaSelectAsync();
        Task<BaseResponse<TipoActividadResponse>> DeleteAsync(int id);
    }
}
