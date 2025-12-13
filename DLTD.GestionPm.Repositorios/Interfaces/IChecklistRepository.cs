
using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Interfaces
{
    public interface ICheckListRepository: IBaseRepository<PmcheckList>
    {
        Task AddMasterDetailsAsync(PmcheckList master);

        Task<PmcheckList?> FindByIdWithDetailAsync(int id);

        Task<bool> FindCheckList(int idTipopm, int idModelo);
    }
}
