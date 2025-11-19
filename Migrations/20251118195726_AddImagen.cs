using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AReyes.Migrations
{
    /// <inheritdoc />
    public partial class AddImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rfc",
                table: "Proveedores",
                type: "varchar(13)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(18)");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Productos",
                type: "varchar(300)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Productos");

            migrationBuilder.AlterColumn<string>(
                name: "Rfc",
                table: "Proveedores",
                type: "varchar(18)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)");
        }
    }
}
