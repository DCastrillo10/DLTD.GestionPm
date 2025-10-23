using System;
using System.Collections.Generic;
using DLTD.GestionPm.Entidad;
using Microsoft.EntityFrameworkCore;

namespace DLTD.GestionPm.AccesoDatos.Contexto;

public partial class GestionPmBdContext : DbContext
{
    public GestionPmBdContext()
    {
    }

    public GestionPmBdContext(DbContextOptions<GestionPmBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Horometro> Horometros { get; set; }

    public virtual DbSet<Maquina> Maquinas { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Pm> Pms { get; set; }

    public virtual DbSet<PmcheckList> PmcheckLists { get; set; }

    public virtual DbSet<Pmdetalle> Pmdetalles { get; set; }

    public virtual DbSet<PmtareaActividad> PmtareaActividads { get; set; }

    public virtual DbSet<PmtareaDemora> PmtareaDemoras { get; set; }

    public virtual DbSet<PmtareaHallazgo> PmtareaHallazgos { get; set; }

    public virtual DbSet<PmtareaTecnico> PmtareaTecnicos { get; set; }

    public virtual DbSet<PmtecnicoTurno> PmtecnicoTurnos { get; set; }

    public virtual DbSet<Ruta> Rutas { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<TipoActividad> TipoActividads { get; set; }

    public virtual DbSet<TipoDemora> TipoDemoras { get; set; }

    public virtual DbSet<TipoHallazgo> TipoHallazgos { get; set; }

    public virtual DbSet<TipoPm> TipoPms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Horometro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Horometros_PK");

            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.HorometroActual)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HorometroPrevio)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.Horometros)
                .HasForeignKey(d => d.IdMaquina)
                .HasConstraintName("Horometros_Maquinas_FK");
        });

        modelBuilder.Entity<Maquina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Maquinas_PK");

            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Maquinas)
                .HasForeignKey(d => d.IdModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Maquinas_Modelos_FK");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Marcas_PK");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ModeloMaquinas_PK");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Modelo1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Modelo");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Modelos_Marcas_FK");
        });

        modelBuilder.Entity<Pm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PM_PK");

            entity.ToTable("PM");

            entity.Property(e => e.Duracion).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.FechaFinalPm)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Pm");
            entity.Property(e => e.FechaInicialPm)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Pm");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Horometro).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NoEquipo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NoHangar)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusPm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Status_Pm");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.WorkOrder)
                .HasMaxLength(100)
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

            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pmdetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMDetalles_PK");

            entity.ToTable("PMDetalles");

            entity.Property(e => e.Duracion)
                .HasComment("Duracion de la tarea desde el parametro")
                .HasColumnType("decimal(6, 2)");
            entity.Property(e => e.DuracionTarea)
                .HasComment("Duracion de la tarea en la ejecucion")
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("Duracion_Tarea");
            entity.Property(e => e.FechaActualizacion).HasPrecision(0);
            entity.Property(e => e.FechaFinalTarea)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Tarea");
            entity.Property(e => e.FechaInicialTarea)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Tarea");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.NoTecnicos).HasComment("Numero Tecnicos desde la parametrizacion");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Realizado).HasDefaultValue(false);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusTarea)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Programado,Enprogreso,Completado")
                .HasColumnName("Status_Tarea");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Valor1).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Valor2).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Valor3).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPmNavigation).WithMany(p => p.Pmdetalles)
                .HasForeignKey(d => d.IdPm)
                .HasConstraintName("PMDetalles_PM_FK");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.Pmdetalles)
                .HasForeignKey(d => d.IdTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PMDetalles_Tareas_FK");
        });

        modelBuilder.Entity<PmtareaActividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTareaActividad_PK");

            entity.ToTable("PMTareaActividad");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DuracionActividad)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("Duracion_Actividad");
            entity.Property(e => e.FechaFinalActividad)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Actividad");
            entity.Property(e => e.FechaInicialActividad)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Actividad");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPmDetalleNavigation).WithMany(p => p.PmtareaActividads)
                .HasForeignKey(d => d.IdPmDetalle)
                .HasConstraintName("PMTareaActividad_PMDetalles_FK");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.PmtareaActividads)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("PMTareaActividad_Tecnicos_FK");

            entity.HasOne(d => d.IdTipoActividadNavigation).WithMany(p => p.PmtareaActividads)
                .HasForeignKey(d => d.IdTipoActividad)
                .HasConstraintName("PMTareaActividad_TipoActividad_FK");
        });

        modelBuilder.Entity<PmtareaDemora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTareaDemoras_PK");

            entity.ToTable("PMTareaDemoras");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DuracionDemora)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("Duracion_Demora");
            entity.Property(e => e.FechaFinalDemora)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Demora");
            entity.Property(e => e.FechaInicialDemora)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Demora");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro).HasPrecision(0);

            entity.HasOne(d => d.IdPmDetalleNavigation).WithMany(p => p.PmtareaDemoras)
                .HasForeignKey(d => d.IdPmDetalle)
                .HasConstraintName("PMTareaDemoras_PMDetalles_FK");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.PmtareaDemoras)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("PMTareaDemoras_Tecnicos_FK");

            entity.HasOne(d => d.IdTipoDemoraNavigation).WithMany(p => p.PmtareaDemoras)
                .HasForeignKey(d => d.IdTipoDemora)
                .HasConstraintName("PMTareaDemoras_TipoDemoras_FK");
        });

        modelBuilder.Entity<PmtareaHallazgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTareaHallazgos_PK");

            entity.ToTable("PMTareaHallazgos");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaHallazgo).HasPrecision(0);
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ValidadoPor).HasMaxLength(450);

            entity.HasOne(d => d.IdPmDetalleNavigation).WithMany(p => p.PmtareaHallazgos)
                .HasForeignKey(d => d.IdPmDetalle)
                .HasConstraintName("PMTareaHallazgos_PMDetalles_FK");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.PmtareaHallazgos)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("PMTareaHallazgos_Tecnicos_FK");

            entity.HasOne(d => d.IdTipoHallazgoNavigation).WithMany(p => p.PmtareaHallazgos)
                .HasForeignKey(d => d.IdTipoHallazgo)
                .HasConstraintName("PMTareaHallazgos_TipoHallazgos_FK");
        });

        modelBuilder.Entity<PmtareaTecnico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTareaTecnico_PK");

            entity.ToTable("PMTareaTecnico");

            entity.Property(e => e.DuracionAsignacion).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.FechaFinalAsignacion).HasPrecision(0);
            entity.Property(e => e.FechaInicialAsignacion).HasPrecision(0);
            entity.Property(e => e.FechaRegistro).HasPrecision(0);            
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPmDetalleNavigation).WithMany(p => p.PmtareaTecnicos)
                .HasForeignKey(d => d.IdPmDetalle)
                .HasConstraintName("PMTareaTecnico_PMDetalles_FK");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.PmtareaTecnicos)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("PMTareaTecnico_Tecnicos_FK");
        });

        modelBuilder.Entity<PmtecnicoTurno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PMTecnicoTurno_PK");

            entity.ToTable("PMTecnicoTurno");

            entity.Property(e => e.DuracionTurno)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("Duracion_Turno");
            entity.Property(e => e.FechaFinalTurno)
                .HasPrecision(0)
                .HasColumnName("FechaFinal_Turno");
            entity.Property(e => e.FechaInicialTurno)
                .HasPrecision(0)
                .HasColumnName("FechaInicial_Turno");
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Turno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Dia o Noche");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPmNavigation).WithMany(p => p.PmtecnicoTurnos)
                .HasForeignKey(d => d.IdPm)
                .HasConstraintName("PMTecnicoTurno_PM_FK");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.PmtecnicoTurnos)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("PMTecnicoTurno_Tecnicos_FK");
        });

        modelBuilder.Entity<Ruta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rutas_PK");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tareas_PK");

            entity.Property(e => e.CodigoTarea)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Duracion).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdRuta)
                .HasConstraintName("Tareas_Rutas_FK");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tecnicos_PK");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TurnoActual)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoActividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoActividad_PK");

            entity.ToTable("TipoActividad");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(0);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDemora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoDemoras_PK");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoHallazgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoHallazgos_PK");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoPm_PK");

            entity.ToTable("TipoPm");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
