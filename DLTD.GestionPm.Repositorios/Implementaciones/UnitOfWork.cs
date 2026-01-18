using DLTD.GestionPm.AccesoDatos.Configuracion;
using DLTD.GestionPm.AccesoDatos.Contexto;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Repositorios.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Implementaciones
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly GestionPmBdContext _contexto;
        private readonly UserManager<SecurityEntity> _userManager;

        public ITecnicoRepository TecnicoRepo { get; }
        public IMarcaRepository MarcaRepo { get; }
        public IModeloRepository ModeloRepo { get; }
        public IMaquinaRepository MaquinaRepo { get; }
        public ITipoActividadRepository TipoActividadRepo { get; }
        public ITipoDemoraRepository TipoDemoraRepo { get; }
        public ITipoHallazgoRepository TipoHallazgoRepo { get; }
        public ITipoPmRepository TipoPmRepo { get; }
        public IRutaRepository RutaRepo { get; }
        public ITareaRepository TareaRepo { get; }
        public ICheckListRepository CheckListRepo { get; }
        public IGrupoTrabajoRepository GrupoTrabajoRepo { get; }
        public IPmRepository PmRepo { get; }
        public IPmTareaTecnicoRepository PmTareaTecnicoRepo { get; }
        public IPmTareaDemoraRepository PmTareaDemoraRepo { get; }
        public IPmTareaHallazgoRepository PmTareaHallazgoRepo { get; }
        


        public UnitOfWork(GestionPmBdContext contexto, UserManager<SecurityEntity> userManager)
        {
            _contexto = contexto;
            _userManager = userManager;

            TecnicoRepo = new TecnicoRepository(_contexto, _userManager);
            MarcaRepo = new MarcaRepository(_contexto);
            ModeloRepo = new ModeloRepository(_contexto);
            MaquinaRepo = new MaquinaRepository(_contexto);
            TipoActividadRepo = new TipoActividadRepository(_contexto);
            TipoDemoraRepo = new TipoDemoraRepository(_contexto);
            TipoHallazgoRepo = new TipoHallazgoRepository(_contexto);
            TipoPmRepo = new TipoPmRepository(_contexto);
            RutaRepo = new RutaRepository(_contexto);
            TareaRepo = new TareaRepository(_contexto);
            CheckListRepo = new CheckListRepository(_contexto);
            GrupoTrabajoRepo = new GrupoTrabajoRepository(_contexto);
            PmRepo = new PmRepository(_contexto);
            PmTareaTecnicoRepo = new PmTareaTecnicoRepository(_contexto);
            PmTareaDemoraRepo = new PmTareaDemoraRepository(_contexto);
            PmTareaHallazgoRepo = new PmTareaHallazgoRepository(_contexto);
            
        }

        public async Task<int> SaveAsync() => await _contexto.SaveChangesAsync();
        public void Dispose() => _contexto.Dispose();
    }
}
