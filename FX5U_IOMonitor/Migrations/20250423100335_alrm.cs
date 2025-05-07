using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX5U_IOMonitor.Migrations
{
    /// <inheritdoc />
    public partial class alrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "current_single",
                table: "alarm",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "classTag",
                table: "alarm",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "classTag",
                table: "alarm");

            migrationBuilder.AlterColumn<string>(
                name: "current_single",
                table: "alarm",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");
        }
    }
}
