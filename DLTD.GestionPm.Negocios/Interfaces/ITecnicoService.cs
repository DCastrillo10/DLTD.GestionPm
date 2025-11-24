using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Tecnico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ITecnicoService
    {
        Task<BaseResponse> AddAsync(TecnicoRequest request);
        Task<BaseResponse> UpdateAsync(int id, TecnicoRequest request);
        Task<BaseResponse<TecnicoResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaTecnicoResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<TecnicoResponse>> DeleteAsync(int id);
    }
}
