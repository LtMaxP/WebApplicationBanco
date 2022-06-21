using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplicationBanco.Models
{
    public partial class TestBancoContext : DbContext
    {
        public TestBancoContext()
        {
        }

        public TestBancoContext(DbContextOptions<TestBancoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cuentum> Cuenta { get; set; }
        public virtual DbSet<Registro> Registros { get; set; }
        public virtual DbSet<Tarjetum> Tarjeta { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-SLGG4A0\\SQLEXPRESS; Database=TestBanco; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuentum>(entity =>
            {
                entity.HasKey(e => e.IdCuenta);

                entity.Property(e => e.IdCuenta)
                    .ValueGeneratedNever()
                    .HasColumnName("id_cuenta");

                entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdTarjetaNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdTarjeta)
                    .HasConstraintName("FK_Cuenta_Tarjeta");
            });

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(e => e.IdRegistro);

                entity.ToTable("Registro");

                entity.Property(e => e.IdRegistro).HasColumnName("id_Registro");

                entity.Property(e => e.Accion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.IdUsuario).HasColumnName("id_Usuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Registros)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registro_Usuario");
            });

            modelBuilder.Entity<Tarjetum>(entity =>
            {
                entity.HasKey(e => e.IdTarjeta);

                entity.Property(e => e.IdTarjeta)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tarjeta");

                entity.Property(e => e.Bloqueo).HasColumnName("bloqueo");

                entity.Property(e => e.IntentosFallidos).HasColumnName("intentosFallidos");

                entity.Property(e => e.NumeroTarjeta).HasColumnName("numeroTarjeta");

                entity.Property(e => e.Pin).HasColumnName("pin");

                entity.Property(e => e.Vencimiento)
                    .HasColumnType("date")
                    .HasColumnName("vencimiento");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdCuenta)
                    .HasConstraintName("FK_Usuario_Cuenta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
