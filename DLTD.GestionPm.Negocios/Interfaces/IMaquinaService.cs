using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Maquina;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Maquina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IMaquinaService
    {
        Task<BaseResponse> AddAsync(MaquinaRequest request);
        Task<BaseResponse> UpdateAsync(int id, MaquinaRequest request);
        Task<BaseResponse<MaquinaResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaMaquinaResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<MaquinaResponse>> DeleteAsync(int id);
    }
}
