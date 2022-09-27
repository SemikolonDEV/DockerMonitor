using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DockerMonitor.Infastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    DBContainerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.DBContainerId);
                });

            migrationBuilder.CreateTable(
                name: "ContainersStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPUUsage = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Cores = table.Column<long>(type: "bigint", nullable: false),
                    MemoryUsage = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MemoryMax = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ReadSize = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    WriteSize = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DBContainerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainersStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainersStats_Containers_DBContainerId",
                        column: x => x.DBContainerId,
                        principalTable: "Containers",
                        principalColumn: "DBContainerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainersStats_DBContainerId",
                table: "ContainersStats",
                column: "DBContainerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainersStats");

            migrationBuilder.DropTable(
                name: "Containers");
        }
    }
}
