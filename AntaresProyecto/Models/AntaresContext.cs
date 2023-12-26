using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AntaresProyecto.Models;

public partial class AntaresContext : DbContext
{
    public AntaresContext()
    {
    }

    public AntaresContext(DbContextOptions<AntaresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Etapa> Etapas { get; set; }

    public virtual DbSet<ProjectType> ProjectTypes { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<UnidadesDeNegocio> UnidadesDeNegocios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MARTINA\\MARTINASQL; Database=Antares; User Id= sa; Password=mabril21; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK__Areas__70B82028DB1CE009");

            entity.Property(e => e.AreaId).HasColumnName("AreaID");
            entity.Property(e => e.NombreArea).HasMaxLength(50);
            entity.Property(e => e.UnidadId).HasColumnName("UnidadID");

            entity.HasOne(d => d.Unidad).WithMany(p => p.Areas)
                .HasForeignKey(d => d.UnidadId)
                .HasConstraintName("FK__Areas__UnidadID__398D8EEE");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0877C82D4DC");

            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PK__Estado__FEF86B60DFE400DD");

            entity.ToTable("Estado");

            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.NombreEstado).HasMaxLength(50);
        });

        modelBuilder.Entity<Etapa>(entity =>
        {
            entity.HasKey(e => e.EtapasId).HasName("PK__Etapas__59483EF7CD3C72E5");

            entity.Property(e => e.EtapasId).HasColumnName("EtapasID");
            entity.Property(e => e.NombreEtapa).HasMaxLength(50);
        });

        modelBuilder.Entity<ProjectType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__ProjectT__516F0395DB99C3E2");

            entity.ToTable("ProjectType");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3213E83F62B6980E");

            entity.ToTable("Proyecto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categorias)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("categorias");
            entity.Property(e => e.Cliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente");
            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Comentarios)
                .HasColumnType("text")
                .HasColumnName("comentarios");
            entity.Property(e => e.DiaFin)
                .HasColumnType("date")
                .HasColumnName("diaFin");
            entity.Property(e => e.DiaInicio)
                .HasColumnType("date")
                .HasColumnName("diaInicio");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Etapa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("etapa");
            entity.Property(e => e.LiderProyecto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("liderProyecto");
            entity.Property(e => e.NombreProyecto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProyecto");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("prioridad");
        });

        modelBuilder.Entity<UnidadesDeNegocio>(entity =>
        {
            entity.HasKey(e => e.UnidadId).HasName("PK__Unidades__C6F324364D8D7A3D");

            entity.ToTable("UnidadesDeNegocio");

            entity.Property(e => e.UnidadId).HasColumnName("UnidadID");
            entity.Property(e => e.NombreUnidad).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
