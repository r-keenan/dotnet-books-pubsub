using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Books.API.Migrations
{
    /// <inheritdoc />
    public partial class MoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8760), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8760), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8760), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateCreated", "DateModified", "DateOfBirth", "FirstName", "LastName", "MiddleName", "WritingAwards" },
                values: new object[,]
                {
                    { 5, new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770), new DateOnly(1943, 6, 9), "Joe", "Haldeman", "", new string[0] },
                    { 6, new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770), new DateOnly(1932, 1, 18), "Robert", "Wilson", "Anton", new string[0] }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8850), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8850) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8860) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8830), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address1", "Address2", "City", "DateCreated", "DateFounded", "DateModified", "Name", "State", "ZipCode" },
                values: new object[,]
                {
                    { 5, "", "", "", new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), "St. Martin's Press", 0, "" },
                    { 6, "", "", "", new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), "Dell Publishing", 0, "" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "DateCreated", "DateModified", "DatePublished", "Genre", "PageLength", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 9, 5, new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateOnly(1974, 1, 1), 0, 0, 5, "The Forever War" },
                    { 10, 6, new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateOnly(1975, 1, 1), 0, 0, 6, "The Illuminatus! Trilogy" },
                    { 11, 6, new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateOnly(1988, 1, 1), 0, 0, 6, "Schrödinger's Cat Trilogy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9970), new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9970) });
        }
    }
}
