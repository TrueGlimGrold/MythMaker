using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MythMaker.Migrations
{
    /// <inheritdoc />
    public partial class addProductsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "DisplayOrder", "ImagePath", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Meet Luna, the Moonlit Watcher. With scales that shift in shades of deep sapphire blue flecked with silver, Luna looks as if she’s bathed in starlight. Her emerald green eyes are fierce and intelligent, and her slender, curving horns add to her elegance. She watches over the world from rocky outcrops under a foggy, dawn-lit sky, surrounded by mystical wisps of smoke. Luna is calm, reflective, and perfect for those who feel drawn to moonlit mysteries and dreamlike landscapes.", 1, "images/products/dragon-1", "Luna, the Moonlit Watcher", 1.75 },
                    { 2, 1, "Meet Sylvandar, the Forest Guardian. With shimmering emerald green and teal scales flecked with gold, Sylvandar looks like a living piece of the forest itself. His amber eyes hold a wise yet powerful gaze, and his twisted, branch-like horns tell of ancient times. Sylvandar lives in a dense, misty forest, where sunlight filters softly through the trees at dusk, illuminating his presence. He’s a gentle giant who loves peace and balance, perfect for those who feel most at home among the trees and whispering leaves.", 1, "images/products/dragon-2", "Sylvandar, the Forest Guardian", 1.8 }
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
