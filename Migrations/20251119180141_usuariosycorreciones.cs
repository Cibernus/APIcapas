using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AReyes.Migrations
{
    /// <inheritdoc />
    public partial class usuariosycorreciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreProveedor",
                table: "Proveedores",
                type: "varchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "Proveedores",
                type: "varchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "Proveedores",
                type: "varchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NombreEmpleado",
                table: "Empleados",
                type: "varchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "Empleados",
                type: "varchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "Empleados",
                type: "varchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "varchar", nullable: false, defaultValueSql: "generar_clave_Usuario()"),
                    NombreUsuario = table.Column<string>(type: "varchar(70)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "varchar(70)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "varchar(70)", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "varchar(150)", nullable: false),
                    Contraseña = table.Column<string>(type: "varchar(256)", nullable: false),
                    Rol = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioId", x => x.UsuarioId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "NombreProveedor",
                table: "Proveedores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "Proveedores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "Proveedores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreEmpleado",
                table: "Empleados",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "Empleados",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "Empleados",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)");
        }
    }
}
