using ControlFit.Domain.Entidad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Infrastructure.Persistencia
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Miembro> Miembros { get; set; }
        public DbSet<Gimnasio> gimnasios { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<AsignacionMembresia> AsignacionesMembresia { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.ToTable("Administrador");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.NombreCompleto).HasMaxLength(100).IsRequired();
                entity.Property(a => a.Correo).HasMaxLength(100);
                entity.Property(a => a.Contrasena).HasMaxLength(255).IsRequired();
            });
            modelBuilder.Entity<Miembro>(entity =>
            {

                entity.ToTable("Miembro");
                entity.HasKey(a => a.Id);


            });
            modelBuilder.Entity<Gimnasio>(entity =>
            {

                entity.ToTable("Gimnasio");
                entity.HasKey(a => a.Id);


            });
            modelBuilder.Entity<Membresia>(entity =>
            {

                entity.ToTable("Membresia");
                entity.HasKey(a => a.Id);


            });
            modelBuilder.Entity<AsignacionMembresia>(entity =>
            {

                entity.ToTable("Asignacion");
                entity.HasKey(a => a.Id);


            });

            modelBuilder.Entity<Asistencia>(entity =>
            {

                entity.ToTable("Asistencia");
                entity.HasKey(a => a.Id);


            });
        }
    }
}
