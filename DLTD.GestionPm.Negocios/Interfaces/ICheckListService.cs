using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.CheckList;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.CheckList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ICheckListService
    {
        Task<BaseResponse> AddMasterDetailsAsync(CheckListRequest request);
        Task<BaseResponse> UpdateMasterDetailsAsync(int id, CheckListRequest request);
        Task<PaginationResponse<ListaCheckListResponse>> ListaAsync(PaginationRequest request);
        Task<BaseResponse<CheckListResponse>> FindByIdAsync(int id);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
