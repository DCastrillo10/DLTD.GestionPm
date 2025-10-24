using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
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
    }
}
