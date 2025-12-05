using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoPm;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoPm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ITipoPmService
    {
        Task<BaseResponse> AddAsync(TipoPmRequest request);
        Task<BaseResponse> UpdateAsync(int id, TipoPmRequest request);
        Task<BaseResponse<TipoPmResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaTipoPmResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoPmResponse>>> ListaSelectAsync();
        Task<BaseResponse<TipoPmResponse>> DeleteAsync(int id);
    }
}
