using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.Infrastructure.Migrations
{
    public partial class aklsdfhasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Membership_AvailableFreezeDays",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Membership_EndDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Membership_StartDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Membership_Status",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailableFreezeDays = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status_CurrentStatus = table.Column<int>(type: "int", nullable: false),
                    Status_StatusReason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Membership_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.AddColumn<int>(
                name: "Membership_AvailableFreezeDays",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Membership_EndDate",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Membership_StartDate",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Membership_Status",
                table: "Customers",
                type: "int",
                nullable: true);
        }
    }
}
