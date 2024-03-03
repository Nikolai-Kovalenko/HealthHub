using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctorProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropColumn(
                name: "PathToPhoto",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "DoctorProfile",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PathToPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    ChangeTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfile_UserId",
                schema: "dbo",
                table: "DoctorProfile",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorProfile",
                schema: "dbo")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "DoctorProfileHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.AddColumn<string>(
                name: "PathToPhoto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PathToPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId");
        }
    }
}
