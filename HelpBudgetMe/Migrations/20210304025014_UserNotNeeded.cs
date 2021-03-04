using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpBudgetMe.Migrations
{
    public partial class UserNotNeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Needs_AspNetUsers_UserId",
                table: "Needs");

            migrationBuilder.DropForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings");

            migrationBuilder.DropForeignKey(
                name: "FK_Wants_AspNetUsers_UserId",
                table: "Wants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Savings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Needs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Needs_AspNetUsers_UserId",
                table: "Needs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wants_AspNetUsers_UserId",
                table: "Wants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Needs_AspNetUsers_UserId",
                table: "Needs");

            migrationBuilder.DropForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings");

            migrationBuilder.DropForeignKey(
                name: "FK_Wants_AspNetUsers_UserId",
                table: "Wants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Savings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Needs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Needs_AspNetUsers_UserId",
                table: "Needs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wants_AspNetUsers_UserId",
                table: "Wants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
