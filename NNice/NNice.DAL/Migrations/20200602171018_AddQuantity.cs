using Microsoft.EntityFrameworkCore.Migrations;

namespace NNice.DAL.Migrations
{
    public partial class AddQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartModels",
                table: "CartModels");

            migrationBuilder.RenameTable(
                name: "CartModels",
                newName: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "InvoiceDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InvoiceDetails");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "CartModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartModels",
                table: "CartModels",
                column: "ID");
        }
    }
}
