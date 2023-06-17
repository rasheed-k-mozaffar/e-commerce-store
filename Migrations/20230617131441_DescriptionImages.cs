using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_store.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionImagesId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DescriptionImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    URL = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionImagesId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptionImages_Products_DescriptionImagesId",
                        column: x => x.DescriptionImagesId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DescriptionImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionImages_DescriptionImagesId",
                table: "DescriptionImages",
                column: "DescriptionImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionImages_ProductId",
                table: "DescriptionImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescriptionImages");

            migrationBuilder.DropColumn(
                name: "DescriptionImagesId",
                table: "Products");
        }
    }
}
