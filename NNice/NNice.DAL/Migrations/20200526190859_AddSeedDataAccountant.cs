using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNice.DAL.Migrations
{
    public partial class AddSeedDataAccountant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Avatar", "Password", "PasswordHash", "PasswordSalt", "Role", "Token", "Username" },
                values: new object[] { 2, null, "accountant", new byte[] { 41, 216, 201, 158, 194, 91, 39, 16, 7, 240, 94, 172, 232, 126, 192, 9, 89, 116, 102, 135, 170, 150, 237, 120, 61, 215, 162, 243, 191, 219, 57, 139 }, new byte[] { 41, 216, 201, 158, 194, 91, 39, 16, 7, 240, 94, 172, 232, 126, 192, 9, 89, 116, 102, 135, 170, 150, 237, 120, 61, 215, 162, 243, 191, 219, 57, 139 }, 3, null, "accountant" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Avatar", "Password", "PasswordHash", "PasswordSalt", "Role", "Token", "Username" },
                values: new object[] { 3, null, "Cashier", new byte[] { 174, 156, 136, 76, 231, 55, 219, 170, 118, 106, 60, 149, 183, 112, 25, 116, 217, 25, 109, 180, 17, 193, 206, 169, 176, 54, 192, 68, 127, 169, 163, 151 }, new byte[] { 174, 156, 136, 76, 231, 55, 219, 170, 118, 106, 60, 149, 183, 112, 25, 116, 217, 25, 109, 180, 17, 193, 206, 169, 176, 54, 192, 68, 127, 169, 163, 151 }, 2, null, "Cashier" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
