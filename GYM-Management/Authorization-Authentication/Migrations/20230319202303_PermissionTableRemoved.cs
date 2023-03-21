using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization_Authentication.Migrations
{
    public partial class PermissionTableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissionMaps_Permissions_PermissionId",
                table: "RolePermissionMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissionMaps",
                table: "RolePermissionMaps");

            migrationBuilder.AlterColumn<Guid>(
                name: "PermissionId",
                table: "RolePermissionMaps",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "PermissionType",
                table: "RolePermissionMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PermissionContext",
                table: "RolePermissionMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissionMaps",
                table: "RolePermissionMaps",
                columns: new[] { "PermissionType", "PermissionContext", "RoleId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c28efd96-582e-4855-9822-5cfe4d988543"),
                column: "ConcurrencyStamp",
                value: "8fc74bdd-ca85-4d84-af2c-9b1e5b513769");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f482bcca-98db-438b-906b-4860e14adcce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "630113ee-2155-48e3-afc2-9254e511c611", "AQAAAAEAACcQAAAAEH2TfU620rkePzXUk56nOAs94Kis0Z6caqz7U23jEVaYc255b7A9RN8Z+gTK7Bnjkw==" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMaps_PermissionId",
                table: "RolePermissionMaps",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissionMaps_Permissions_PermissionId",
                table: "RolePermissionMaps",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissionMaps_Permissions_PermissionId",
                table: "RolePermissionMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissionMaps",
                table: "RolePermissionMaps");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissionMaps_PermissionId",
                table: "RolePermissionMaps");

            migrationBuilder.DropColumn(
                name: "PermissionType",
                table: "RolePermissionMaps");

            migrationBuilder.DropColumn(
                name: "PermissionContext",
                table: "RolePermissionMaps");

            migrationBuilder.AlterColumn<Guid>(
                name: "PermissionId",
                table: "RolePermissionMaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissionMaps",
                table: "RolePermissionMaps",
                columns: new[] { "PermissionId", "RoleId" });

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a1a89c31-6f3b-4aae-9e56-015c1259d539", "AQAAAAEAACcQAAAAENgt5R/0VWuvXhxjCTCaFKEAfi6vZTOIIb4BERu5C5hR9FnqG+frAoyXfEMweKeSLw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissionMaps_Permissions_PermissionId",
                table: "RolePermissionMaps",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
