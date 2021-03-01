using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpBudgetMe.Migrations
{
    public partial class AllTimeAddedDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Expenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "AllTimeEarned",
                table: "AspNetUsers",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0.00m);

            migrationBuilder.AddColumn<decimal>(
                name: "AllTimeSpent",
                table: "AspNetUsers",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0.00m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "AllTimeEarned",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AllTimeSpent",
                table: "AspNetUsers");
        }
    }
}
