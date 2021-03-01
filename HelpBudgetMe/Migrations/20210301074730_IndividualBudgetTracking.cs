using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpBudgetMe.Migrations
{
    public partial class IndividualBudgetTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentMoney",
                table: "AspNetUsers",
                type: "decimal(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AllTimeSpent",
                table: "AspNetUsers",
                type: "decimal(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AllTimeEarned",
                table: "AspNetUsers",
                type: "decimal(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "BudgetedForNeeds",
                table: "AspNetUsers",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BudgetedForSavings",
                table: "AspNetUsers",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BudgetedForWants",
                table: "AspNetUsers",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetedForNeeds",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BudgetedForSavings",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BudgetedForWants",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentMoney",
                table: "AspNetUsers",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AllTimeSpent",
                table: "AspNetUsers",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AllTimeEarned",
                table: "AspNetUsers",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)");
        }
    }
}
