using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Books.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DatePublished",
                table: "Books",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateCreated", "DateModified", "DateOfBirth", "FirstName", "LastName", "MiddleName", "WritingAwards" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateOnly(1903, 6, 25), "George", "Orwell", "", new[] { "Prometheus Hall of Fame Award" } },
                    { 2, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateOnly(1915, 3, 18), "Richard", "Condon", "", new string[0] },
                    { 3, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateOnly(1821, 11, 11), "Fyodor", "Dostoevsky", "", new string[0] },
                    { 4, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateOnly(1907, 7, 7), "Robert", "Heinlein", "A.", new string[0] }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address1", "Address2", "City", "DateCreated", "DateFounded", "DateModified", "Name", "State", "ZipCode" },
                values: new object[,]
                {
                    { 1, "", "", "", new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), "Penguin Books", 0, "" },
                    { 2, "", "", "", new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), "McGraw-Hill", 0, "" },
                    { 3, "", "", "", new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), "The Russian Messenger", 0, "" },
                    { 4, "", "", "", new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9970), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9970), "G. P. Puntnam's Sons", 0, "" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "DateCreated", "DateModified", "DatePublished", "Genre", "PageLength", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateOnly(1949, 6, 8), 0, 0, 1, "1984" },
                    { 2, 1, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateOnly(1945, 8, 17), 0, 0, 1, "Animal Farm" },
                    { 3, 2, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateOnly(1959, 4, 27), 0, 0, 2, "The Manchurian Candidate" },
                    { 4, 3, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateOnly(1866, 1, 1), 0, 0, 3, "Crime and Punishment" },
                    { 5, 3, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateOnly(1871, 1, 1), 0, 0, 3, "Demons" },
                    { 6, 4, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateOnly(1961, 6, 1), 0, 0, 4, "Stranger in a Strange Land" },
                    { 7, 4, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990), new DateOnly(1959, 11, 5), 0, 0, 4, "Starship Troopers" },
                    { 8, 4, new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990), new DateOnly(1966, 6, 2), 0, 0, 4, "The Moon is a Harsh Mistress" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
