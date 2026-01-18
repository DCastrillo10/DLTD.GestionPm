using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaHallazgo;
using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IPmTareaHallazgoService
    {
        Task<BaseResponse> AddAsync(PmTareaHallazgoRequest request);
        Task<BaseResponse> UpdateAsync(int id, PmTareaHallazgoRequest request);
        Task<BaseResponse<PmTareaHallazgoResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaPmTareaHallazgoResponse>> ListaAsync(PaginationRequest request);        
        Task<BaseResponse<PmTareaHallazgoResponse>> DeleteAsync(int id);

        Task<BaseResponse<string>> GetNoEquipo(int idPmDetalle);


    }
}
