using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.GrupoTrabajo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.GrupoTrabajo;
using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IGrupoTrabajoService
    {
        Task<BaseResponse> AddAsync(GrupoTrabajoRequest request);        
        
        Task<PaginationResponse<ListaGrupoTrabajoResponse>> ListaAsync(PaginationRequest request);
        
        Task<BaseResponse<GrupoTrabajoResponse>> DeleteAsync(int id);
    }
}
