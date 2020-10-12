using Microsoft.EntityFrameworkCore.Migrations;

namespace NNice.DAL.Migrations
{
    public partial class AddEmployeeShiftsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeShifts",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false),
                    WorkShiftID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeShifts", x => new { x.EmployeeID, x.WorkShiftID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeShifts");
        }
    }
}
