using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsCatalogTestTask.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_films_name",
                table: "films",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_categories_name",
                table: "categories",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_films_name",
                table: "films");

            migrationBuilder.DropIndex(
                name: "IX_categories_name",
                table: "categories");
        }
    }
}
