using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Pm;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Pm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IPmService
    {
        Task<BaseResponse> AddMasterDetailsAsync(PmRequest request);
        Task<BaseResponse> UpdateMasterDetailsAsync(int id, PmRequest request);
        Task<PaginationResponse<ListaPmResponse>> ListaAsync(PaginationRequest request);        
        Task<BaseResponse<PmResponse>> FindByIdAsync(int id);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse<bool>> ExistePm(int idTipopm, int idModelo, string NoEquipo, string WorkOrder);
        Task<BaseResponse<IEnumerable<PmDetallesResponse>>> FindTareas(int idTipoPm, int idModelo);


    }
}
