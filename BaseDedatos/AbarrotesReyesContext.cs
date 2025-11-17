
using AReyes.Models;
using Microsoft.EntityFrameworkCore;
namespace Areyes.BaseDedatos
{
    public class AbarrotesReyesContext : DbContext
    {
        public AbarrotesReyesContext(DbContextOptions<AbarrotesReyesContext> options) : base(options) { }

        public virtual DbSet<ProductoEntity> Productos { get; set; }

        public virtual DbSet<EmpleadoEntity> Empleados { get; set; }

        public virtual DbSet<ProveedorEntity> Proveedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductoEntity>(entity =>

            {
                entity.ToTable("Productos");
                entity.HasKey(p => p.ProductoId).HasName("PK_productoId");

                entity.Property(u => u.ProductoId)
                   .HasColumnType("varchar")
                   .HasDefaultValueSql("generar_clave_producto()");


                entity.Property(p => p.NombreProducto)
                    .IsRequired();

                entity.Property(p => p.Cantidad)
                    .IsRequired();

                entity.Property(p => p.Precio)
                    .HasColumnType("numeric(10,2)");

                entity.Property(p => p.Descripcion)
                 .HasColumnType("varchar(200)")
                .IsRequired();

            }
         );

            modelBuilder.Entity<EmpleadoEntity>(entity =>

            {
                entity.ToTable("Empleados");
                entity.HasKey(p => p.EmpleadoId).HasName("PK_EmpleadoId");

                entity.Property(u => u.EmpleadoId)
                   .HasColumnType("varchar")
                   .HasDefaultValueSql("generar_clave_Empleado()");

                entity.Property(p => p.NombreEmpleado)
                    .IsRequired();

                entity.Property(p => p.ApellidoPaterno)
                    .IsRequired();

                entity.Property(p => p.ApellidoMaterno)
                    .IsRequired();

                entity.Property(p => p.Telefono)
                  .HasColumnType("varchar(15)")
                    .IsRequired();

                entity.Property(p => p.Curp)
                 .HasColumnType("varchar(18)")
                    .IsRequired();

            });

            modelBuilder.Entity<ProveedorEntity>(entity =>

            {
                entity.ToTable("Proveedores");
                entity.HasKey(p => p.ProveedorId).HasName("PK_ProveedorId");

                entity.Property(u => u.ProveedorId)
                   .HasColumnType("varchar")
                   .HasDefaultValueSql("generar_clave_Proveedor()");

                entity.Property(p => p.NombreProveedor)
                    .IsRequired();

                entity.Property(p => p.ApellidoPaterno)
                    .IsRequired();

                entity.Property(p => p.ApellidoMaterno)
                    .IsRequired();

                entity.Property(p => p.Telefono)
                  .HasColumnType("varchar(15)")
                    .IsRequired();

                entity.Property(p => p.Rfc)
                 .HasColumnType("varchar(18)")
                    .IsRequired();
            });

        }
    }
}
