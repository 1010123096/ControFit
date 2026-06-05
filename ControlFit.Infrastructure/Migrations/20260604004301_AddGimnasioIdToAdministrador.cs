using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGimnasioIdToAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GimnasioId",
                table: "Membresia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GimnasioId",
                table: "Administrador",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MiembroId = table.Column<int>(type: "int", nullable: false),
                    AsignacionMembresiaId = table.Column<int>(type: "int", nullable: false),
                    FechaHoraAcceso = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Membresia_GimnasioId",
                table: "Membresia",
                column: "GimnasioId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_GimnasioId",
                table: "Administrador",
                column: "GimnasioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrador_Gimnasio_GimnasioId",
                table: "Administrador",
                column: "GimnasioId",
                principalTable: "Gimnasio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Membresia_Gimnasio_GimnasioId",
                table: "Membresia",
                column: "GimnasioId",
                principalTable: "Gimnasio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrador_Gimnasio_GimnasioId",
                table: "Administrador");

            migrationBuilder.DropForeignKey(
                name: "FK_Membresia_Gimnasio_GimnasioId",
                table: "Membresia");

            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_Membresia_GimnasioId",
                table: "Membresia");

            migrationBuilder.DropIndex(
                name: "IX_Administrador_GimnasioId",
                table: "Administrador");

            migrationBuilder.DropColumn(
                name: "GimnasioId",
                table: "Membresia");

            migrationBuilder.DropColumn(
                name: "GimnasioId",
                table: "Administrador");
        }
    }
}
