using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PipeManagmentApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    packageNumber = table.Column<int>(type: "integer", nullable: false),
                    packageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pipes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    quality = table.Column<string>(type: "text", nullable: false),
                    steelGrade = table.Column<string>(type: "text", nullable: false),
                    dimensions = table.Column<string>(type: "text", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    packageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pipes_Packages_packageId",
                        column: x => x.packageId,
                        principalTable: "Packages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pipes_packageId",
                table: "Pipes",
                column: "packageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pipes");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
