using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization_Authentication.Migrations
{
    public partial class SuperAdminAndRoleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsActive", "Name", "NormalizedName" },
                values: new object[] { new Guid("c28efd96-582e-4855-9822-5cfe4d988543"), "6c0b23c9-65e5-4cc5-8545-851dd06b087c", true, "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f482bcca-98db-438b-906b-4860e14adcce"), 0, "511414af-c11c-4fd5-bb62-b3dc235ca092", "saitpostaci8@gmail.com", true, false, null, "SAITPOSTACI8@GMAIL.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEACisumSD1le8WICxaXDiiLVMAqT4KfTewXL1dLeWmTrnmIOkWtJfqxZN9p1v5PgbA==", null, true, null, false, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("c28efd96-582e-4855-9822-5cfe4d988543"), new Guid("f482bcca-98db-438b-906b-4860e14adcce") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c28efd96-582e-4855-9822-5cfe4d988543"), new Guid("f482bcca-98db-438b-906b-4860e14adcce") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c28efd96-582e-4855-9822-5cfe4d988543"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f482bcca-98db-438b-906b-4860e14adcce"));
        }
    }
}
