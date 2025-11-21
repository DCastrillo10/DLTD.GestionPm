using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Marca;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Marca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IMarcaService
    {
        Task<BaseResponse> AddAsync(MarcaRequest request);
        Task<BaseResponse> UpdateAsync(int id, MarcaRequest request);
        Task<BaseResponse<MarcaResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaMarcaResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<MarcaResponse>> DeleteAsync(int id);
    }
}
