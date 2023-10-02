using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mouse" },
                    { 2, "Key Board" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImgUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 2, "Unique gasket mount design: Silicone gasket mount with three layers of dampening foams combine to provide an unrivaled typing experience", "", "ASUS ROG Azoth 75% Wireless DIY Custom Gaming Keyboard", 229.0, 200 },
                    { 2, 2, "Low-Profile Keys: With slimmer keycaps and shorter switches, enjoy natural hand positioning that allows for long hours of use with little strain", "", "Razer Ornata V3 X Gaming Keyboard", 34.990000000000002, 200 },
                    { 3, 2, "Low-Profile Keys: With slimmer keycaps and shorter switches, enjoy natural hand positioning that allows for long hours of use with little strain", "", "Low-Profile Keys: With slimmer keycaps and shorter switches, enjoy natural hand positioning that allows for long hours of use with little strain", 129.99000000000001, 200 },
                    { 4, 2, "Take your gaming skills to the next level: The Logitech G413 TKL SE is a tenkeyless keyboard with gaming-first features and the durability and performance necessary to compete", "", "Logitech G413 TKL SE Mechanical Gaming Keyboard", 120.0, 200 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
