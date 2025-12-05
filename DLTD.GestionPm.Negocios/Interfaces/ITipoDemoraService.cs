using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoDemora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ITipoDemoraService
    {
        Task<BaseResponse> AddAsync(TipoDemoraRequest request);
        Task<BaseResponse> UpdateAsync(int id, TipoDemoraRequest request);
        Task<BaseResponse<TipoDemoraResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaTipoDemoraResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoDemoraResponse>>> ListaSelectAsync();
        Task<BaseResponse<TipoDemoraResponse>> DeleteAsync(int id);
    }
}
