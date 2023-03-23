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
                    Email_MailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Membership_AvailableFreezePeriod = table.Column<int>(type: "int", nullable: true),
                    Membership_EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Membership_StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Membership_Status = table.Column<int>(type: "int", nullable: true),
                    Membership_TotalMonthsOfMembership = table.Column<int>(type: "int", nullable: true),
                    Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber_CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
