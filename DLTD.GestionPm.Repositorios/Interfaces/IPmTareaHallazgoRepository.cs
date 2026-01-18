using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Interfaces
{
    public interface IPmTareaHallazgoRepository: IBaseRepository<PmTareaHallazgo>
    {
        Task<string?> FindEquipo(int id);
    }
}
