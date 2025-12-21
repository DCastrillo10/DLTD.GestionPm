using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaTecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaTecnico;
using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IPmTareaTecnicoService
    {
        Task<BaseResponse> AddAsync(PmTareaTecnicoRequest request);
        Task<BaseResponse> UpdateAsync(int id, PmTareaTecnicoRequest request);
        Task<BaseResponse<PmTareaTecnicoResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaPmTareaTecnicoResponse>> ListaAsync(PaginationRequest request);
        
        Task<BaseResponse<PmTareaTecnicoResponse>> DeleteAsync(int id);
    }
}
