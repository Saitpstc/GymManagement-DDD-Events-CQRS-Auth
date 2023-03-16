using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionType = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AvailableFreezePeriod = table.Column<int>(type: "int", nullable: false),
                    TotalMonthsOfMembership = table.Column<int>(type: "int", nullable: false),
                    LastUpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailDb_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipDbId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NameDb_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameDb_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberDb_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberDb_CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Membership_MembershipDbId",
                        column: x => x.MembershipDbId,
                        principalTable: "Membership",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MembershipDbId",
                table: "Customers",
                column: "MembershipDbId",
                unique: true,
                filter: "[MembershipDbId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Membership");
        }
    }
}
