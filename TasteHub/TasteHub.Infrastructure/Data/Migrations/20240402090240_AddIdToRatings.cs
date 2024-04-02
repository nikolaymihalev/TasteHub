using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteHub.Data.Migrations
{
    public partial class AddIdToRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 2, "6131b2c1-b80a-49ec-83ae-51fb006b5c89" });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 1, "c208dab4-2a45-43e5-81dd-eb173111575b" });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Rating identifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6131b2c1-b80a-49ec-83ae-51fb006b5c89",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fec44097-0894-44f5-a433-f0c45f62d7be", "AQAAAAEAACcQAAAAEA6EBxNc3ZO/D9LesD16y7yhF3t25uJkaPj/WbZgmESqtnAjAtbQJ3RC+vNAQZlklA==", "b80df96f-a10f-4550-afc2-142f4cab318c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c208dab4-2a45-43e5-81dd-eb173111575b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e63b92b5-63c0-46ce-abe9-f5bb79d73df7", "AQAAAAEAACcQAAAAEDhh2Uhypnivz1ZmvpW8DyYgC7snSjWknbrPO6bzr/A9yJpKXoD2wzVwnSNOjpi/Fg==", "090ccc0c-a1f3-497f-ae3f-56cf197e2f85" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 4, 2, 9, 2, 40, 11, DateTimeKind.Local).AddTicks(9216));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 4, 2, 9, 2, 40, 11, DateTimeKind.Local).AddTicks(9218));

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "RecipeId", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, 1, "c208dab4-2a45-43e5-81dd-eb173111575b", 5.0 },
                    { 2, 2, "6131b2c1-b80a-49ec-83ae-51fb006b5c89", 4.5999999999999996 }
                });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 4, 2, 9, 2, 40, 6, DateTimeKind.Local).AddTicks(8454));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 4, 2, 9, 2, 40, 7, DateTimeKind.Local).AddTicks(3944));

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ratings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                columns: new[] { "UserId", "RecipeId" });

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
        }
    }
}
