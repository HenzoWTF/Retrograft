﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retrograf.Migrations
{
    /// <inheritdoc />
    public partial class Cuadre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Cuadres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Cuadres");
        }
    }
}
