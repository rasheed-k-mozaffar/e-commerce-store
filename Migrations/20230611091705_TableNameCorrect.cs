using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_store.Migrations
{
    /// <inheritdoc />
    public partial class TableNameCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Derscription",
                table: "Products",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Derscription");
        }
    }
}
