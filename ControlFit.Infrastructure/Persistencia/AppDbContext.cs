using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<ConfiguracionPlataforma> ConfiguracionesPlataforma { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<BiometricDevice> BiometricDevices { get; set; }
        public DbSet<BiometricEvent> BiometricEvents { get; set; }
        public DbSet<BiometricUserMapping> BiometricUserMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.ToTable("Administrador");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.NombreCompleto).HasMaxLength(100).IsRequired();
                entity.Property(a => a.Correo).HasMaxLength(100).IsRequired();
                entity.Property(a => a.Contrasena).HasMaxLength(255).IsRequired();
                entity.HasIndex(a => a.Correo).IsUnique();
                entity.HasOne(a => a.Gimnasio)
                    .WithMany()
                    .HasForeignKey(a => a.GimnasioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Miembro>(entity =>
            {
                entity.ToTable("Miembro");
                entity.HasKey(a => a.Id);
                entity.HasOne(m => m.Gimnasio)
                    .WithMany(g => g.Miembros)
                    .HasForeignKey(m => m.GimnasioId)
                    .OnDelete(DeleteBehavior.Cascade);
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
                entity.HasOne(m => m.Gimnasio)
                    .WithMany()
                    .HasForeignKey(m => m.GimnasioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AsignacionMembresia>(entity =>
            {
                entity.ToTable("Asignacion");
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.MiembroId);
                entity.HasIndex(a => a.MembresiaId);
                entity.HasOne<Miembro>()
                    .WithMany()
                    .HasForeignKey(a => a.MiembroId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Membresia>()
                    .WithMany()
                    .HasForeignKey(a => a.MembresiaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.ToTable("Asistencia");
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.MiembroId);
                entity.HasIndex(a => a.AsignacionMembresiaId);
                entity.Property(a => a.Fuente).HasConversion<int>();
                entity.HasOne<Miembro>()
                    .WithMany()
                    .HasForeignKey(a => a.MiembroId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<AsignacionMembresia>()
                    .WithMany()
                    .HasForeignKey(a => a.AsignacionMembresiaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ConfiguracionPlataforma>(entity =>
            {
                entity.ToTable("ConfiguracionPlataforma");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.NombreSistema).HasMaxLength(100).IsRequired();
                entity.Property(c => c.CorreoContacto).HasMaxLength(100).IsRequired();
                entity.Property(c => c.TelefonoContacto).HasMaxLength(30).IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");
                entity.HasKey(r => r.Id);
                entity.HasIndex(r => r.TokenHash).IsUnique();
                entity.HasIndex(r => r.AdministradorId);
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Accion).HasMaxLength(100).IsRequired();
                entity.Property(a => a.Entidad).HasMaxLength(100).IsRequired();
                entity.HasIndex(a => a.FechaUtc);
            });

            modelBuilder.Entity<BiometricDevice>(entity =>
            {
                entity.ToTable("BiometricDevice");
                entity.HasKey(d => d.Id);
                entity.HasIndex(d => new { d.GimnasioId, d.SerialNumber }).IsUnique();
            });

            modelBuilder.Entity<BiometricEvent>(entity =>
            {
                entity.ToTable("BiometricEvent");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.GimnasioId, e.ExternalEventId }).IsUnique();
            });

            modelBuilder.Entity<BiometricUserMapping>(entity =>
            {
                entity.ToTable("BiometricUserMapping");
                entity.HasKey(m => m.Id);
                entity.HasIndex(m => new { m.GimnasioId, m.ExternalUserId }).IsUnique();
            });
        }
    }
}
