using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteHub.Data.Migrations
{
    public partial class UpdateRecipeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Recipe description",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Recipe description");

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
                columns: new[] { "CreationDate", "Description" },
                values: new object[] { new DateTime(2024, 2, 28, 17, 6, 8, 828, DateTimeKind.Local).AddTicks(3701), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Recipe description",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Recipe description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6131b2c1-b80a-49ec-83ae-51fb006b5c89",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e9cb0e1-89ec-49e6-84eb-d5fbd2d6a806", "AQAAAAEAACcQAAAAEGTLi4wW+cP972Ye8C1e136bJvMMF58Ps3O3WAinUkxnToNchZbpn3GNCMjzOksXbQ==", "46d1cfad-bb4c-4238-a2dd-4ba92655da1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c208dab4-2a45-43e5-81dd-eb173111575b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b97c547b-5ea6-4f8a-a743-627833650648", "AQAAAAEAACcQAAAAEPL0e9tOjX8DZ339MtDSzQa+mKz2Xml58hHsiIWCuf7/HvoLYRefSbWMh5hGThXB6Q==", "4a5d1cb1-b823-4de7-bb67-7611b6c49bdf" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 20, 30, 0, 163, DateTimeKind.Local).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 20, 30, 0, 163, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 20, 30, 0, 160, DateTimeKind.Local).AddTicks(4076));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "Description" },
                values: new object[] { new DateTime(2024, 2, 26, 20, 30, 0, 160, DateTimeKind.Local).AddTicks(6569), "" });
        }
    }
}
