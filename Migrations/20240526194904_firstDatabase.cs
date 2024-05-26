using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talaria.Migrations
{
    public partial class firstDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorDatas",
                columns: table => new
                {
                    MyDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    packageNumber = table.Column<int>(type: "int", nullable: false),
                    satelliteStatus = table.Column<int>(type: "int", nullable: false),
                    ErrorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pressure1 = table.Column<float>(type: "real", nullable: false),
                    pressure2 = table.Column<float>(type: "real", nullable: false),
                    height1 = table.Column<int>(type: "int", nullable: false),
                    height2 = table.Column<int>(type: "int", nullable: false),
                    altitudeDif = table.Column<int>(type: "int", nullable: false),
                    descentSpeed = table.Column<int>(type: "int", nullable: false),
                    tempature = table.Column<int>(type: "int", nullable: false),
                    batteryVoltage = table.Column<float>(type: "real", nullable: false),
                    gps1Latitude = table.Column<float>(type: "real", nullable: false),
                    gps1Longitude = table.Column<float>(type: "real", nullable: false),
                    gps1altitude = table.Column<float>(type: "real", nullable: false),
                    roll = table.Column<float>(type: "real", nullable: false),
                    pitch = table.Column<float>(type: "real", nullable: false),
                    yaw = table.Column<float>(type: "real", nullable: false),
                    rhrh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IoTData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDatas", x => x.MyDataId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorDatas");
        }
    }
}
