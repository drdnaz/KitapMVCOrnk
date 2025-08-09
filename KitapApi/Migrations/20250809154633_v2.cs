using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KitapApi.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 4, "George Orwell", 3, null, 45m, "1984" },
                    { 5, "Isaac Asimov", 4, null, 60m, "Foundation" },
                    { 6, "J.K. Rowling", 2, null, 70m, "Harry Potter and the Philosopher's Stone" },
                    { 7, "J.R.R. Tolkien", 3, null, 80m, "The Hobbit" },
                    { 8, "Agetha Cristie", 4, null, 80m, "On Küçük Zenci" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
