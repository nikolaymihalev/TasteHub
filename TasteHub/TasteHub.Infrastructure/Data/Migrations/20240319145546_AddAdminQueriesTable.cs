using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteHub.Data.Migrations
{
    public partial class AddAdminQueriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "AdminQueries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Query identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User identifier"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Query description")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminQueries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Query for becoming admin");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6131b2c1-b80a-49ec-83ae-51fb006b5c89",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b82ad222-137e-49ad-8f73-f6f3b50bbb7f", "AQAAAAEAACcQAAAAELJhFGx4bJLpB+0MiQ5Wg5xjVGwNI47+Wy3hD6xEXcIdzxi+1b3VnBYKQAbXHS3A1Q==", "efa423a3-e6f6-42d8-bb31-96ae3d8e6feb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c208dab4-2a45-43e5-81dd-eb173111575b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8af6fdb5-da36-4ce5-afe8-a4f381ff0094", "AQAAAAEAACcQAAAAEK2cVBVpOr9Dc2fEWD002huq7o8Cwh+Yrp6SS/h+3XsnICwFjk/jp/uiDMID2Mellg==", "75817e95-c646-4189-8a2c-22bec944e481" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 19, 14, 55, 45, 950, DateTimeKind.Local).AddTicks(7122));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 19, 14, 55, 45, 950, DateTimeKind.Local).AddTicks(7124));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 19, 14, 55, 45, 947, DateTimeKind.Local).AddTicks(2667));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 19, 14, 55, 45, 947, DateTimeKind.Local).AddTicks(5500));

            migrationBuilder.CreateIndex(
                name: "IX_AdminQueries_UserId",
                table: "AdminQueries",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminQueries");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6131b2c1-b80a-49ec-83ae-51fb006b5c89",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57f3b940-e6f9-48a6-a1d1-d92c7e50419d", "AQAAAAEAACcQAAAAEI00/LHrYkgMllaz/l/DXjwnYJ4PM0t1is1nZ4OBKH0AzKTxODE8XhO7PPiOCb3WhA==", "5f61446d-673f-4782-b2d8-d01b24b75c5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c208dab4-2a45-43e5-81dd-eb173111575b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58036090-ea85-4a3a-8fd8-1e87012f4969", "AQAAAAEAACcQAAAAENrMG72W0J3v+ilYA0cZSZi8FCLrtAWoyfswQAEGFAUxbqMXRZL3R/0IIhFN9rQgxg==", "b85cef8d-bf14-479a-a1c9-cbaafb189aa1" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 28, 17, 6, 8, 831, DateTimeKind.Local).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 28, 17, 6, 8, 831, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 28, 17, 6, 8, 828, DateTimeKind.Local).AddTicks(335));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 28, 17, 6, 8, 828, DateTimeKind.Local).AddTicks(3701));
        }
    }
}
