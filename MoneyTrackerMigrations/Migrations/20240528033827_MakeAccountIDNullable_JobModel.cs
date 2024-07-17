using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class MakeAccountIDNullable_JobModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_FinanceAccounts_AccountId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Jobs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_FinanceAccounts_AccountId",
                table: "Jobs",
                column: "AccountId",
                principalTable: "FinanceAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_FinanceAccounts_AccountId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_FinanceAccounts_AccountId",
                table: "Jobs",
                column: "AccountId",
                principalTable: "FinanceAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
