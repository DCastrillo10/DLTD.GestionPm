using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Ruta;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Ruta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IRutaService
    {
        Task<BaseResponse> AddAsync(RutaRequest request);
        Task<BaseResponse> UpdateAsync(int id, RutaRequest request);
        Task<BaseResponse<RutaResponse>> FindByIdAsync(int id);
        Task<BaseResponse<ICollection<ListaRutaResponse>>> ListaSelectAsync();
        Task<PaginationResponse<ListaRutaResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<RutaResponse>> DeleteAsync(int id);
    }
}
