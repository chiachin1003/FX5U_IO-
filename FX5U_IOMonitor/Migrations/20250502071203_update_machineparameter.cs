using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX5U_IOMonitor.Migrations
{
    /// <inheritdoc />
    public partial class update_machineparameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumericValue",
                table: "MachineParameters");

            migrationBuilder.RenameColumn(
                name: "ValueType",
                table: "MachineParameters",
                newName: "write_address");

            migrationBuilder.RenameColumn(
                name: "TextValue",
                table: "MachineParameters",
                newName: "now_TextValue");

            migrationBuilder.RenameColumn(
                name: "IsWritable",
                table: "MachineParameters",
                newName: "history_NumericValue");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "MachineParameters",
                newName: "read_address");

            migrationBuilder.AddColumn<bool>(
                name: "calculate",
                table: "MachineParameters",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "history_TextValue",
                table: "MachineParameters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<ushort>(
                name: "now_NumericValue",
                table: "MachineParameters",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "calculate",
                table: "MachineParameters");

            migrationBuilder.DropColumn(
                name: "history_TextValue",
                table: "MachineParameters");

            migrationBuilder.DropColumn(
                name: "now_NumericValue",
                table: "MachineParameters");

            migrationBuilder.RenameColumn(
                name: "write_address",
                table: "MachineParameters",
                newName: "ValueType");

            migrationBuilder.RenameColumn(
                name: "read_address",
                table: "MachineParameters",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "now_TextValue",
                table: "MachineParameters",
                newName: "TextValue");

            migrationBuilder.RenameColumn(
                name: "history_NumericValue",
                table: "MachineParameters",
                newName: "IsWritable");

            migrationBuilder.AddColumn<double>(
                name: "NumericValue",
                table: "MachineParameters",
                type: "REAL",
                nullable: true);
        }
    }
}
