using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class creacionAsignaiones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Miembro_Gimasio_GimnasioId",
                table: "Miembro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gimasio",
                table: "Gimasio");

            migrationBuilder.RenameTable(
                name: "Gimasio",
                newName: "Gimnasio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gimnasio",
                table: "Gimnasio",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Asignacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MiembroId = table.Column<int>(type: "int", nullable: false),
                    MembresiaId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membresia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duración = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    MaximoIngresosPorDia = table.Column<int>(type: "int", nullable: true),
                    MaximoIngresosPorSemana = table.Column<int>(type: "int", nullable: true),
                    MaximoIngresosTotales = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membresia", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Miembro_Gimnasio_GimnasioId",
                table: "Miembro",
                column: "GimnasioId",
                principalTable: "Gimnasio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Miembro_Gimnasio_GimnasioId",
                table: "Miembro");

            migrationBuilder.DropTable(
                name: "Asignacion");

            migrationBuilder.DropTable(
                name: "Membresia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gimnasio",
                table: "Gimnasio");

            migrationBuilder.RenameTable(
                name: "Gimnasio",
                newName: "Gimasio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gimasio",
                table: "Gimasio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Miembro_Gimasio_GimnasioId",
                table: "Miembro",
                column: "GimnasioId",
                principalTable: "Gimasio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
