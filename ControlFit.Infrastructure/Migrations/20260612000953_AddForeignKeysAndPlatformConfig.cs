using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysAndPlatformConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrador_Gimnasio_GimnasioId",
                table: "Administrador");

            migrationBuilder.CreateTable(
                name: "ConfiguracionPlataforma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSistema = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorreoContacto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TelefonoContacto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionPlataforma", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_AsignacionMembresiaId",
                table: "Asistencia",
                column: "AsignacionMembresiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_MiembroId",
                table: "Asistencia",
                column: "MiembroId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignacion_MembresiaId",
                table: "Asignacion",
                column: "MembresiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignacion_MiembroId",
                table: "Asignacion",
                column: "MiembroId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_Correo",
                table: "Administrador",
                column: "Correo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Administrador_Gimnasio_GimnasioId",
                table: "Administrador",
                column: "GimnasioId",
                principalTable: "Gimnasio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignacion_Membresia_MembresiaId",
                table: "Asignacion",
                column: "MembresiaId",
                principalTable: "Membresia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignacion_Miembro_MiembroId",
                table: "Asignacion",
                column: "MiembroId",
                principalTable: "Miembro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Asignacion_AsignacionMembresiaId",
                table: "Asistencia",
                column: "AsignacionMembresiaId",
                principalTable: "Asignacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Miembro_MiembroId",
                table: "Asistencia",
                column: "MiembroId",
                principalTable: "Miembro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrador_Gimnasio_GimnasioId",
                table: "Administrador");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignacion_Membresia_MembresiaId",
                table: "Asignacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignacion_Miembro_MiembroId",
                table: "Asignacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Asignacion_AsignacionMembresiaId",
                table: "Asistencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Miembro_MiembroId",
                table: "Asistencia");

            migrationBuilder.DropTable(
                name: "ConfiguracionPlataforma");

            migrationBuilder.DropIndex(
                name: "IX_Asistencia_AsignacionMembresiaId",
                table: "Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_Asistencia_MiembroId",
                table: "Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_Asignacion_MembresiaId",
                table: "Asignacion");

            migrationBuilder.DropIndex(
                name: "IX_Asignacion_MiembroId",
                table: "Asignacion");

            migrationBuilder.DropIndex(
                name: "IX_Administrador_Correo",
                table: "Administrador");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrador_Gimnasio_GimnasioId",
                table: "Administrador",
                column: "GimnasioId",
                principalTable: "Gimnasio",
                principalColumn: "Id");
        }
    }
}
