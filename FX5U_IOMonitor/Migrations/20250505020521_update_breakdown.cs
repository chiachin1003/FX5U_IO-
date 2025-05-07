using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX5U_IOMonitor.Migrations
{
    /// <inheritdoc />
    public partial class update_breakdown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Breakdown",
                table: "Sawing_IO",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "write_type",
                table: "MachineParameters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Breakdown",
                table: "Drill_IO",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breakdown",
                table: "Sawing_IO");

            migrationBuilder.DropColumn(
                name: "write_type",
                table: "MachineParameters");

            migrationBuilder.DropColumn(
                name: "Breakdown",
                table: "Drill_IO");
        }
    }
}
