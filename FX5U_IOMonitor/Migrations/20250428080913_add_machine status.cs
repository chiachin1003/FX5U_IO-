using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX5U_IOMonitor.Migrations
{
    /// <inheritdoc />
    public partial class add_machinestatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Total_time");

            migrationBuilder.CreateTable(
                name: "MachineParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    IsWritable = table.Column<bool>(type: "INTEGER", nullable: false),
                    ValueType = table.Column<string>(type: "TEXT", nullable: false),
                    NumericValue = table.Column<double>(type: "REAL", nullable: true),
                    TextValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTag", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ScheduleTag",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "None" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineParameters");

            migrationBuilder.DropTable(
                name: "ScheduleTag");

            migrationBuilder.CreateTable(
                name: "Total_time",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Drill_electricity = table.Column<string>(type: "TEXT", nullable: false),
                    Frequency_Converter_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    PLC_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    Possible = table.Column<string>(type: "TEXT", nullable: false),
                    Repair_steps = table.Column<string>(type: "TEXT", nullable: false),
                    Runtime = table.Column<string>(type: "TEXT", nullable: false),
                    Sawing_electricity = table.Column<string>(type: "TEXT", nullable: false),
                    Sawing_total_Time = table.Column<string>(type: "TEXT", nullable: false),
                    Servo_drives_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    Spindle_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    clamping = table.Column<int>(type: "INTEGER", nullable: false),
                    feeder = table.Column<int>(type: "INTEGER", nullable: false),
                    loose_tools = table.Column<int>(type: "INTEGER", nullable: false),
                    measurement = table.Column<int>(type: "INTEGER", nullable: false),
                    origin = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Total_time", x => x.Id);
                });
        }
    }
}
