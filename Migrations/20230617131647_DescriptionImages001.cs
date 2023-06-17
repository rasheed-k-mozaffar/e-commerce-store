using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_store.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionImages001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DescriptionImages_Products_DescriptionImagesId",
                table: "DescriptionImages");

            migrationBuilder.DropIndex(
                name: "IX_DescriptionImages_DescriptionImagesId",
                table: "DescriptionImages");

            migrationBuilder.DropColumn(
                name: "DescriptionImagesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DescriptionImagesId",
                table: "DescriptionImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionImagesId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DescriptionImagesId",
                table: "DescriptionImages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionImages_DescriptionImagesId",
                table: "DescriptionImages",
                column: "DescriptionImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DescriptionImages_Products_DescriptionImagesId",
                table: "DescriptionImages",
                column: "DescriptionImagesId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
