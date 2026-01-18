using System;
using System.Collections.Generic;
using DLTD.GestionPm.Comun;
using DLTD.GestionPm.Entidad;
using Microsoft.EntityFrameworkCore;

namespace DLTD.GestionPm.AccesoDatos.Contexto;

public partial class GestionPmBdContext : DbContext
{
    private readonly IUsuarioService? _usuarioService;
    public GestionPmBdContext()
    {
        
    }

    public GestionPmBdContext(DbContextOptions<GestionPmBdContext> options, IUsuarioService usuarioService)
        : base(options)
    {
        _usuarioService = usuarioService;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var usuarioActual = _usuarioService!.GetUserName();
        var entries = ChangeTracker.Entries()
                        .Where(e => e.Entity is EntidadBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entidad = (EntidadBase)entry.Entity;
            if(entry.State == EntityState.Added)
            {
                entidad.UsuarioRegistro = usuarioActual!;
                entidad.FechaRegistro = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("FechaRegistro").IsModified = false;
                entry.Property("UsuarioRegistro").IsModified = false;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }


    //Grupo de DbSets
    public virtual DbSet<GrupoTrabajo> GrupoTrabajos { get; set; }

    public virtual DbSet<Horometro> Horometros { get; set; }

    public virtual DbSet<Maquina> Maquinas { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Pm> Pms { get; set; }

    public virtual DbSet<PmcheckList> PmcheckLists { get; set; }

    public virtual DbSet<PmcheckListDetalle> PmcheckListDetalles { get; set; }

    public virtual DbSet<Pmdetalle> PmDetalles { get; set; }

    public virtual DbSet<PmTareaDemora> PmTareaDemoras { get; set; }

    public virtual DbSet<PmTareaHallazgo> PmTareaHallazgos { get; set; }

    public virtual DbSet<PmTareaTecnico> PmTareaTecnicos { get; set; }

    public virtual DbSet<Ruta> Rutas { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<TipoActividad> TipoActividads { get; set; }

    public virtual DbSet<TipoDemora> TipoDemoras { get; set; }

    public virtual DbSet<TipoHallazgo> TipoHallazgos { get; set; }

    public virtual DbSet<TipoPm> TipoPms { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GrupoTrabajo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GrupoTrabajo_PK");

            entity.ToTable("GrupoTrabajo");

            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.FechaVinculacion).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdTecnicoPrincipalNavigation).WithMany(p => p.GrupoTrabajoIdTecnicoPrincipalNavigations)
                .HasForeignKey(d => d.IdTecnicoPrincipal)
                .HasConstraintName("GrupoTrabajo_Tecnicos_FK");

            entity.HasOne(d => d.IdTecnicoVinculadoNavigation).WithMany(p => p.GrupoTrabajoIdTecnicoVinculadoNavigations)
                .HasForeignKey(d => d.IdTecnicoVinculado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GrupoTrabajo_Tecnicos_FK_1");
        });

        modelBuilder.Entity<Horometro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Horometros_PK");

            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.HorometroActual)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.HorometroPrevio)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Observacion).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.Horometros)
                .HasForeignKey(d => d.IdMaquina)
                .HasConstraintName("Horometros_Maquinas_FK");
        });

        modelBuilder.Entity<Maquina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Maquinas_PK");

            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Maquinas)
                .HasForeignKey(d => d.IdModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Maquinas_Modelos_FK");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Marcas_PK");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ModeloMaquinas_PK");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Referencia).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Modelos_Marcas_FK");
        });

        modelBuilder.Entity<Pm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PM_PK");

            entity.ToTable("PM");

            entity.Property(e => e.Duracion).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.FechaAprobacion).HasPrecision(0);
            entity.Property(e => e.FechaFinalPm)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Pm");
            entity.Property(e => e.FechaInicialPm)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Pm");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.FechaRevision).HasPrecision(0);
            entity.Property(e => e.HorometroActual).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.HorometroPrevio).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.NoEquipo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NoHangar)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Activo,Inactivo,Eliminado,Revisado,Aprobado");
            entity.Property(e => e.StatusPm)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Status_Pm");
            entity.Property(e => e.UsuarioAprobacion).HasMaxLength(20);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
            entity.Property(e => e.UsuarioRevision).HasMaxLength(20);
            entity.Property(e => e.WorkOrder)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Pms)
                .HasForeignKey(d => d.IdModelo)
                .HasConstraintName("PM_Modelos_FK");

            entity.HasOne(d => d.IdTipoPmNavigation).WithMany(p => p.Pms)
                .HasForeignKey(d => d.IdTipoPm)
                .HasConstraintName("PM_TipoPm_FK");
        });

        modelBuilder.Entity<PmcheckList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CheckListPm_PK");

            entity.ToTable("PMCheckList");

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.PmcheckLists)
                .HasForeignKey(d => d.IdModelo)
                .HasConstraintName("PMCheckList_Modelos_FK");

            entity.HasOne(d => d.IdTipoPmNavigation).WithMany(p => p.PmcheckLists)
                .HasForeignKey(d => d.IdTipoPm)
                .HasConstraintName("PMCheckList_TipoPm_FK");
        });

        modelBuilder.Entity<PmcheckListDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMCheckListDetalles_PK");

            entity.ToTable("PMCheckListDetalles");

            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdPmCheckListNavigation).WithMany(p => p.PmcheckListDetalles)
                .HasForeignKey(d => d.IdPmCheckList)
                .HasConstraintName("PMCheckListDetalles_PMCheckList_FK");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.PmcheckListDetalles)
                .HasForeignKey(d => d.IdRuta)
                .HasConstraintName("PMCheckListDetalles_Rutas_FK");
        });

        modelBuilder.Entity<Pmdetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMDetalles_PK");

            entity.ToTable("PMDetalles");

            entity.Property(e => e.Duracion)
                .HasComment("Duracion de la tarea desde el parametro")
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.DuracionTarea)
                .HasComment("Duracion de la tarea en la ejecucion")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("Duracion_Tarea");
            entity.Property(e => e.FechaActualizacion)
                .HasPrecision(0)
                .HasComment("");
            entity.Property(e => e.FechaAprobacion).HasPrecision(0);
            entity.Property(e => e.FechaFinalTarea)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Tarea");
            entity.Property(e => e.FechaInicialTarea)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Tarea");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.FechaRevision).HasPrecision(0);
            entity.Property(e => e.NoTecnicos).HasComment("Numero Tecnicos desde la parametrizacion");
            entity.Property(e => e.Realizado).HasDefaultValue(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Activo,Inactivo,Eliminado,Revisado,Aprobado");
            entity.Property(e => e.StatusTarea)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Programado,Enprogreso,Completado")
                .HasColumnName("Status_Tarea");
            entity.Property(e => e.UsuarioAprobacion).HasMaxLength(20);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
            entity.Property(e => e.UsuarioRevision).HasMaxLength(20);
            entity.Property(e => e.Valor1).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Valor2).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Valor3).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPmNavigation).WithMany(p => p.PmDetalles)
                .HasForeignKey(d => d.IdPm)
                .HasConstraintName("PMDetalles_PM_FK");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.Pmdetalles)
                .HasForeignKey(d => d.IdTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PMDetalles_Tareas_FK");
        });

        modelBuilder.Entity<PmTareaDemora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTareaDemoras_PK");

            entity.ToTable("PMTareaDemoras");

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.DuracionDemora)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("Duracion_Demora");
            entity.Property(e => e.FechaFinalDemora)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Demora");
            entity.Property(e => e.FechaInicialDemora)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Demora");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdPmDetalleNavigation).WithMany(p => p.PmTareaDemoras)
                .HasForeignKey(d => d.IdPmDetalle)
                .HasConstraintName("PMTareaDemoras_PMDetalles_FK");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.PmTareaDemoras)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("PMTareaDemoras_Tecnicos_FK");

            entity.HasOne(d => d.IdTipoDemoraNavigation).WithMany(p => p.PmTareaDemoras)
                .HasForeignKey(d => d.IdTipoDemora)
                .HasConstraintName("PMTareaDemoras_TipoDemoras_FK");
        });

        modelBuilder.Entity<PmTareaHallazgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTareaHallazgos_PK");

            entity.ToTable("PMTareaHallazgos");

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.FechaHallazgo).HasPrecision(0);
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
            entity.Property(e => e.ValidadoPor).HasMaxLength(20);
            entity.Property(e => e.NoEquipo).HasMaxLength(10);
            entity.Property(e => e.Tecnicos).HasMaxLength(50);

            entity.HasOne(d => d.IdPmDetalleNavigation).WithMany(p => p.PmTareaHallazgos)
                .HasForeignKey(d => d.IdPmDetalle)
                .HasConstraintName("PMTareaHallazgos_PMDetalles_FK");

            entity.HasOne(d => d.IdTipoHallazgoNavigation).WithMany(p => p.PmTareaHallazgos)
                .HasForeignKey(d => d.IdTipoHallazgo)
                .HasConstraintName("PMTareaHallazgos_TipoHallazgos_FK");
        });

        modelBuilder.Entity<PmTareaTecnico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTareaTecnico_PK");

            entity.ToTable("PMTareaTecnico");

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.DuracionActividad).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.FechaFinalActividad).HasPrecision(0);
            entity.Property(e => e.FechaInicialActividad).HasPrecision(0);
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdPmDetalleNavigation).WithMany(p => p.PmTareaTecnicos)
                .HasForeignKey(d => d.IdPmDetalle)
                .HasConstraintName("PMTareaTecnico_PMDetalles_FK_1");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.PmTareaTecnicos)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("PMTareaTecnico_Tecnicos_FK_1");

            entity.HasOne(d => d.IdTipoActividadNavigation).WithMany(p => p.PmTareaTecnicos)
                .HasForeignKey(d => d.IdTipoActividad)
                .HasConstraintName("PMTareaTecnico_TipoActividad_FK_1");
        });

        modelBuilder.Entity<Ruta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rutas_PK");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tareas_PK");

            entity.Property(e => e.CodigoTarea)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).HasMaxLength(2000);
            entity.Property(e => e.Duracion).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdRuta)
                .HasConstraintName("Tareas_Rutas_FK");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tecnicos_PK");

            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("0");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Especialidad).HasMaxLength(30);
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.NoIdentificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("0");
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TurnoActual)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
        });

        modelBuilder.Entity<TipoActividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoActividad_PK");

            entity.ToTable("TipoActividad");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasComment("Iniciar Turno,\r\nReanudar Turno,\r\nFinalizar Turno");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
        });

        modelBuilder.Entity<TipoDemora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoDemoras_PK");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
        });

        modelBuilder.Entity<TipoHallazgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoHallazgos_PK");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
        });

        modelBuilder.Entity<TipoPm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoPm_PK");

            entity.ToTable("TipoPm");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
