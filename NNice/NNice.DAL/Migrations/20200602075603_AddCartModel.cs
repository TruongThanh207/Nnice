using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNice.DAL.Migrations
{
    public partial class AddCartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_inventoryDetailModels",
                table: "inventoryDetailModels");

            migrationBuilder.RenameTable(
                name: "inventoryDetailModels",
                newName: "InventoryDetailModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryDetailModels",
                table: "InventoryDetailModels",
                columns: new[] { "InventoryID", "MaterialID" });

            migrationBuilder.CreateTable(
                name: "CartModels",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CartId = table.Column<string>(nullable: true),
                    ProductID = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartModels", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryDetailModels",
                table: "InventoryDetailModels");

            migrationBuilder.RenameTable(
                name: "InventoryDetailModels",
                newName: "inventoryDetailModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventoryDetailModels",
                table: "inventoryDetailModels",
                columns: new[] { "InventoryID", "MaterialID" });
        }
    }
}
