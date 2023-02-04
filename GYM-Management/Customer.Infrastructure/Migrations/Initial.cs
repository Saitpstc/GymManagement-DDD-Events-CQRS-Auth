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
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailDb_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipDb_SubscriptionType = table.Column<int>(type: "int", nullable: false),
                    MembershipDb_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipDb_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameDb_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameDb_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberDb_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberDb_CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMonthsOfMembership = table.Column<int>(type: "int", nullable: false),
                    LastUpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
