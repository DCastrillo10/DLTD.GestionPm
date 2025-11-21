using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Modelo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface IModeloService
    {
        Task<BaseResponse> AddAsync(ModeloRequest request);
        Task<BaseResponse> UpdateAsync(int id, ModeloRequest request);
        Task<BaseResponse<ModeloResponse>> FindByIdAsync(int id);
        Task<PaginationResponse<ListaModeloResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<ModeloResponse>> DeleteAsync(int id);
    }
}
