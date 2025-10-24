using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Interfaces
{
    public interface ITecnicoRepository: IBaseRepository<Tecnico>
    {
        Task<Tecnico?> CreateAsync(Tecnico request, string usuario, string clave);
    }
}
