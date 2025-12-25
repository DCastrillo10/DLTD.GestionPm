using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaDemora;
using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IPmTareaDemoraService
    {
        Task<BaseResponse> AddAsync(PmTareaDemoraRequest request);
        Task<BaseResponse> UpdateAsync(int id, PmTareaDemoraRequest request);
        Task<BaseResponse<PmTareaDemoraResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaPmTareaDemoraResponse>> ListaAsync(PaginationRequest request);        
        Task<BaseResponse<PmTareaDemoraResponse>> DeleteAsync(int id);

        Task<BaseResponse<ICollection<ListaPmTareaDemoraResponse>>> ListarHistoricoxIdPmDetalle(int idPmDetalle);
        
    }
}
