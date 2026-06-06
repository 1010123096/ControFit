using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTelefonoToGimnasio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Gimnasio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Gimnasio");
        }
    }
}
