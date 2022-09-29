using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DockerMonitor.Infastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    DBContainerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.DBContainerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContainersStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CPUUsage = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Cores = table.Column<uint>(type: "int unsigned", nullable: false),
                    MemoryUsage = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    MemoryMax = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    ReadSize = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    WriteSize = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    DBContainerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
