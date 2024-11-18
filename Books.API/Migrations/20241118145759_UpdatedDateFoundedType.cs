using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Books.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDateFoundedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "character varying(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Publishers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 999);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateFounded",
                table: "Publishers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Publishers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 998);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Publishers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 999);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 998);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Books",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Authors",
                type: "character varying(75)",
                maxLength: 75,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 999);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .Annotation("Relational:ColumnOrder", 998);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Authors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8790), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8900), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880), new DateOnly(2024, 11, 18), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880), new DateOnly(2024, 11, 18), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880), new DateOnly(2024, 11, 18), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880), new DateOnly(2024, 11, 18), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880), new DateOnly(2024, 11, 18), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880), new DateOnly(2024, 11, 18), new DateTime(2024, 11, 18, 14, 57, 58, 792, DateTimeKind.Utc).AddTicks(8880) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Publishers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 999);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFounded",
                table: "Publishers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Publishers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 998);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Publishers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 999);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 998);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Books",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Authors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 999);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone")
                .OldAnnotation("Relational:ColumnOrder", 998);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Authors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Relational:ColumnOrder", 0);

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

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8770) });

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
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8870) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8830), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateFounded", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 14, 18, 38, 44, 864, DateTimeKind.Utc).AddTicks(8840) });
        }
    }
}
