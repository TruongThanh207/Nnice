using Microsoft.EntityFrameworkCore.Migrations;

namespace NNice.DAL.Migrations
{
    public partial class RemoveOrderIDToCartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "CartModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "CartModels",
                nullable: true);
        }
    }
}
