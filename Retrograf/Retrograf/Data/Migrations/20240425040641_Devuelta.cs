using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retrograf.Migrations
{
    /// <inheritdoc />
    public partial class Devuelta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Devolucion",
                table: "Ventas",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "CompraDetalle",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_IdCliente",
                table: "CompraDetalle",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraDetalle_Clientes_IdCliente",
                table: "CompraDetalle",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraDetalle_Clientes_IdCliente",
                table: "CompraDetalle");

            migrationBuilder.DropIndex(
                name: "IX_CompraDetalle_IdCliente",
                table: "CompraDetalle");

            migrationBuilder.DropColumn(
                name: "Devolucion",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "CompraDetalle");
        }
    }
}
