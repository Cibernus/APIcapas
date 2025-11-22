using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AReyes.Migrations
{
    /// <inheritdoc />
    public partial class nuevatabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<string>(type: "varchar", nullable: false, defaultValueSql: "generar_clave_Cliente()"),
                    NombreUsuario = table.Column<string>(type: "varchar(70)", nullable: false),
                    Correo = table.Column<string>(type: "varchar(150)", nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteId", x => x.ClienteId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
