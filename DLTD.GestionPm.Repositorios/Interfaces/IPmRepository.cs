
using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Interfaces
{
    public interface IPmRepository: IBaseRepository<Pm>
    {
        Task AddMasterDetailsAsync(Pm master);

        Task<Pm?> FindByIdWithDetailAsync(int id);

        Task<bool> FindPm(int idTipopm, int idModelo, string NoEquipo,string WorkOrder);
        Task<IEnumerable<Tarea>> FindTareas(int idTipoPm, int idModelo);
    }
}
