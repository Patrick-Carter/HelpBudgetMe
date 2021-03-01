using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpBudgetMe.Migrations
{
    public partial class AddingPaycheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paycheck_AspNetUsers_UserId",
                table: "Paycheck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paycheck",
                table: "Paycheck");

            migrationBuilder.RenameTable(
                name: "Paycheck",
                newName: "Paychecks");

            migrationBuilder.RenameIndex(
                name: "IX_Paycheck_UserId",
                table: "Paychecks",
                newName: "IX_Paychecks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paychecks",
                table: "Paychecks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paychecks_AspNetUsers_UserId",
                table: "Paychecks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paychecks_AspNetUsers_UserId",
                table: "Paychecks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paychecks",
                table: "Paychecks");

            migrationBuilder.RenameTable(
                name: "Paychecks",
                newName: "Paycheck");

            migrationBuilder.RenameIndex(
                name: "IX_Paychecks_UserId",
                table: "Paycheck",
                newName: "IX_Paycheck_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paycheck",
                table: "Paycheck",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paycheck_AspNetUsers_UserId",
                table: "Paycheck",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
