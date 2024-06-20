using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retrograf.Migrations
{
    /// <inheritdoc />
    public partial class Carrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cliente",
                table: "Carrito",
                newName: "Nombre");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Carrito",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Carrito");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Carrito",
                newName: "Cliente");
        }
    }
}
