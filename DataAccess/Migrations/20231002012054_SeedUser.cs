using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "RegisterDate", "RoleID", "UserName" },
                values: new object[,]
                {
                    { 1, "Quy Nhon", "kienltqe170124@fpt.edu.vn", "Kien", "123", new DateTime(2023, 10, 2, 8, 20, 53, 970, DateTimeKind.Local).AddTicks(785), 1, "kien1004" },
                    { 2, "Quy Nhon", "kiencvqe1@fpt.edu.vn", "Vo Cong Huy", "123456789", new DateTime(2023, 10, 2, 8, 20, 53, 970, DateTimeKind.Local).AddTicks(803), 2, "huy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
