using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CASHKY.YarnSystem.Migrations
{
    /// <inheritdoc />
    public partial class db1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "yarn.categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yarn.categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "yarn.warehousing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YarnCategoryId = table.Column<long>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Batch = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    InventoryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yarn.warehousing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_yarn.warehousing_yarn.categories_YarnCategoryId",
                        column: x => x.YarnCategoryId,
                        principalTable: "yarn.categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_yarn.warehousing_YarnCategoryId",
                table: "yarn.warehousing",
                column: "YarnCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "yarn.warehousing");

            migrationBuilder.DropTable(
                name: "yarn.categories");
        }
    }
}
