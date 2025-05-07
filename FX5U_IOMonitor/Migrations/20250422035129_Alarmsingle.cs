using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX5U_IOMonitor.Migrations
{
    /// <inheritdoc />
    public partial class Alarmsingle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "current_single",
                table: "alarm",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_single",
                table: "alarm");
        }
    }
}
