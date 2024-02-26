﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteHub.Data.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                comment: "Recipe instructions",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldComment: "Recipe instructions");

            migrationBuilder.AlterColumn<string>(
                name: "Ingredients",
                table: "Recipes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "Recipe ingredients",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldComment: "Recipe ingredients");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Recipes",
                type: "varbinary(MAX)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "Image of the food");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6131b2c1-b80a-49ec-83ae-51fb006b5c89", 0, "9e9cb0e1-89ec-49e6-84eb-d5fbd2d6a806", "creator@mail.com", false, false, null, "creator@mail.com", "creator@mail.com", "AQAAAAEAACcQAAAAEGTLi4wW+cP972Ye8C1e136bJvMMF58Ps3O3WAinUkxnToNchZbpn3GNCMjzOksXbQ==", null, false, "46d1cfad-bb4c-4238-a2dd-4ba92655da1d", false, "creator@mail.com" },
                    { "c208dab4-2a45-43e5-81dd-eb173111575b", 0, "b97c547b-5ea6-4f8a-a743-627833650648", "guest@mail.com", false, false, null, "guest@mail.com", "guest@mail.com", "AQAAAAEAACcQAAAAEPL0e9tOjX8DZ339MtDSzQa+mKz2Xml58hHsiIWCuf7/HvoLYRefSbWMh5hGThXB6Q==", null, false, "4a5d1cb1-b823-4de7-bb67-7611b6c49bdf", false, "guest@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sweets" },
                    { 2, "Sandwiches" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreationDate", "CreatorId", "Description", "Image", "Ingredients", "Instructions", "Title" },

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreationDate", "CreatorId", "Description", "Image", "Ingredients", "Instructions", "Title" },

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { 1, "Amazing recipe!", new DateTime(2024, 2, 26, 20, 30, 0, 163, DateTimeKind.Local).AddTicks(7506), 1, "c208dab4-2a45-43e5-81dd-eb173111575b" },
                    { 2, "Well done!", new DateTime(2024, 2, 26, 20, 30, 0, 163, DateTimeKind.Local).AddTicks(7508), 2, "6131b2c1-b80a-49ec-83ae-51fb006b5c89" }
                });

            migrationBuilder.InsertData(
                table: "FavoriteRecipes",
                columns: new[] { "RecipeId", "UserId" },
                values: new object[] { 2, "6131b2c1-b80a-49ec-83ae-51fb006b5c89" });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RecipeId", "UserId", "Value" },
                values: new object[,]
                {
                    { 2, "6131b2c1-b80a-49ec-83ae-51fb006b5c89", 4.5999999999999996 },
                    { 1, "c208dab4-2a45-43e5-81dd-eb173111575b", 5.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FavoriteRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 2, "6131b2c1-b80a-49ec-83ae-51fb006b5c89" });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 2, "6131b2c1-b80a-49ec-83ae-51fb006b5c89" });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 1, "c208dab4-2a45-43e5-81dd-eb173111575b" });

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6131b2c1-b80a-49ec-83ae-51fb006b5c89");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c208dab4-2a45-43e5-81dd-eb173111575b");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Recipes");

            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                comment: "Recipe instructions",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldComment: "Recipe instructions");

            migrationBuilder.AlterColumn<string>(
                name: "Ingredients",
                table: "Recipes",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                comment: "Recipe ingredients",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "Recipe ingredients");
        }
    }
}