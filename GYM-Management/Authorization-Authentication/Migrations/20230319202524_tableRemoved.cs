using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization_Authentication.Migrations
{
    public partial class tableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissionMaps_Permissions_PermissionId",
                table: "RolePermissionMaps");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissionMaps_PermissionId",
                table: "RolePermissionMaps");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "RolePermissionMaps");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c28efd96-582e-4855-9822-5cfe4d988543"),
                column: "ConcurrencyStamp",
                value: "39e9d8ff-e09a-431d-a43d-c14927f4d196");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f482bcca-98db-438b-906b-4860e14adcce"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d24fc809-1851-4c0b-bf8e-08c4c80ae5c5", "AQAAAAEAACcQAAAAEJ2fKtOl0O+pP9HPy5C5s7azNMQiBw1wQedbGzbT8RPPDGazyiXXkXEj2Aa9xThYZQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PermissionId",
                table: "RolePermissionMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Context = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

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
    }
}
