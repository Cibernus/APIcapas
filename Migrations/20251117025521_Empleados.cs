using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AReyes.Migrations
{
    /// <inheritdoc />
    public partial class Empleados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EmpleadoId = table.Column<string>(type: "varchar", nullable: false, defaultValueSql: "generar_clave_Empleado()"),
                    NombreEmpleado = table.Column<string>(type: "text", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "text", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", nullable: false),
                    Curp = table.Column<string>(type: "varchar(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoId", x => x.EmpleadoId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<string>(type: "varchar", nullable: false, defaultValueSql: "generar_clave_producto()"),
                    NombreProducto = table.Column<string>(type: "text", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productoId", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<string>(type: "varchar", nullable: false, defaultValueSql: "generar_clave_Proveedor()"),
                    NombreProveedor = table.Column<string>(type: "text", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "text", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", nullable: false),
                    Rfc = table.Column<string>(type: "varchar(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorId", x => x.ProveedorId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
