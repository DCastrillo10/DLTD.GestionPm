using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoHallazgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ITipoHallazgoService
    {
        Task<BaseResponse> AddAsync(TipoHallazgoRequest request);
        Task<BaseResponse> UpdateAsync(int id, TipoHallazgoRequest request);
        Task<BaseResponse<TipoHallazgoResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaTipoHallazgoResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoHallazgoResponse>>> ListaSelectAsync();
        Task<BaseResponse<TipoHallazgoResponse>> DeleteAsync(int id);
    }
}
