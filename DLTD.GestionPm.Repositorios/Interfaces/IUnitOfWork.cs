using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ITecnicoRepository TecnicoRepo { get; }
        IMarcaRepository MarcaRepo { get; }
        IModeloRepository ModeloRepo { get; }
        IMaquinaRepository MaquinaRepo { get; }
        ITipoActividadRepository TipoActividadRepo { get; }
        ITipoDemoraRepository TipoDemoraRepo { get; }
        ITipoHallazgoRepository TipoHallazgoRepo { get; }
        ITipoPmRepository TipoPmRepo { get; }
        IRutaRepository RutaRepo { get; }
        ITareaRepository TareaRepo { get; }
        ICheckListRepository CheckListRepo { get; }
        IGrupoTrabajoRepository GrupoTrabajoRepo { get; }
        IPmRepository PmRepo { get; }
        IPmTareaTecnicoRepository PmTareaTecnicoRepo { get; }
        IPmTareaDemoraRepository PmTareaDemoraRepo { get; }

        Task<int> SaveAsync();
    }
}
