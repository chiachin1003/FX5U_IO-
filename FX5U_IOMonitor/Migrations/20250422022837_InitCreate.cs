using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX5U_IOMonitor.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alarm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceDbName = table.Column<string>(type: "TEXT", nullable: false),
                    M_Address = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Error = table.Column<string>(type: "TEXT", nullable: false),
                    Possible = table.Column<string>(type: "TEXT", nullable: false),
                    Repair_steps = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alarm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drill_IO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    IOType = table.Column<bool>(type: "INTEGER", nullable: false),
                    RelayType = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassTag = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    MaxLife = table.Column<int>(type: "INTEGER", nullable: false),
                    equipment_use = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting_green = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting_yellow = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting_red = table.Column<int>(type: "INTEGER", nullable: false),
                    percent = table.Column<double>(type: "REAL", nullable: false),
                    current_single = table.Column<bool>(type: "INTEGER", nullable: true),
                    MountTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UnmountTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drill_IO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sawing_IO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    IOType = table.Column<bool>(type: "INTEGER", nullable: false),
                    RelayType = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassTag = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    MaxLife = table.Column<int>(type: "INTEGER", nullable: false),
                    equipment_use = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting_green = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting_yellow = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting_red = table.Column<int>(type: "INTEGER", nullable: false),
                    percent = table.Column<double>(type: "REAL", nullable: false),
                    current_single = table.Column<bool>(type: "INTEGER", nullable: true),
                    MountTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UnmountTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sawing_IO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Total_time",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sawing_electricity = table.Column<string>(type: "TEXT", nullable: false),
                    Drill_electricity = table.Column<string>(type: "TEXT", nullable: false),
                    Sawing_total_Time = table.Column<string>(type: "TEXT", nullable: false),
                    Possible = table.Column<string>(type: "TEXT", nullable: false),
                    Repair_steps = table.Column<string>(type: "TEXT", nullable: false),
                    Servo_drives_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    Spindle_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    PLC_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    Frequency_Converter_usetime = table.Column<string>(type: "TEXT", nullable: false),
                    Runtime = table.Column<string>(type: "TEXT", nullable: false),
                    origin = table.Column<int>(type: "INTEGER", nullable: false),
                    loose_tools = table.Column<int>(type: "INTEGER", nullable: false),
                    measurement = table.Column<int>(type: "INTEGER", nullable: false),
                    clamping = table.Column<int>(type: "INTEGER", nullable: false),
                    feeder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Total_time", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceDbName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    usetime = table.Column<int>(type: "INTEGER", nullable: false),
                    MachineIOId = table.Column<int>(type: "INTEGER", nullable: false),
                    Drill_MachineIOId = table.Column<int>(type: "INTEGER", nullable: true),
                    Sawing_MachineIOId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Drill_IO_Drill_MachineIOId",
                        column: x => x.Drill_MachineIOId,
                        principalTable: "Drill_IO",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Histories_Sawing_IO_Sawing_MachineIOId",
                        column: x => x.Sawing_MachineIOId,
                        principalTable: "Sawing_IO",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_Drill_MachineIOId",
                table: "Histories",
                column: "Drill_MachineIOId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_Sawing_MachineIOId",
                table: "Histories",
                column: "Sawing_MachineIOId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alarm");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Total_time");

            migrationBuilder.DropTable(
                name: "Drill_IO");

            migrationBuilder.DropTable(
                name: "Sawing_IO");
        }
    }
}
