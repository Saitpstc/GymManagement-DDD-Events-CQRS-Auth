using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization_Authentication.Migrations
{
    public partial class PermissionTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "Context",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c28efd96-582e-4855-9822-5cfe4d988543"),
                column: "ConcurrencyStamp",
                value: "e7c69356-37d4-41cc-9871-59a5482e762b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f482bcca-98db-438b-906b-4860e14adcce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1a89c31-6f3b-4aae-9e56-015c1259d539", "AQAAAAEAACcQAAAAENgt5R/0VWuvXhxjCTCaFKEAfi6vZTOIIb4BERu5C5hR9FnqG+frAoyXfEMweKeSLw==", "6cd014e1-44a0-451a-95b4-0d76fd574a93" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Context",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c28efd96-582e-4855-9822-5cfe4d988543"),
                column: "ConcurrencyStamp",
                value: "6c0b23c9-65e5-4cc5-8545-851dd06b087c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f482bcca-98db-438b-906b-4860e14adcce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "511414af-c11c-4fd5-bb62-b3dc235ca092", "AQAAAAEAACcQAAAAEACisumSD1le8WICxaXDiiLVMAqT4KfTewXL1dLeWmTrnmIOkWtJfqxZN9p1v5PgbA==", null });
        }
    }
}
