using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AdditionsToLoanModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AutoPays_LoanId",
                table: "AutoPays");

            migrationBuilder.RenameColumn(
                name: "PaidOff",
                table: "Loans",
                newName: "MonthlyPaid");

            migrationBuilder.AlterColumn<string>(
                name: "LoanType",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AutoPayId",
                table: "Loans",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoanStatus",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Servicer",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPays_LoanId",
                table: "AutoPays",
                column: "LoanId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AutoPays_LoanId",
                table: "AutoPays");

            migrationBuilder.DropColumn(
                name: "AutoPayId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanStatus",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Servicer",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "MonthlyPaid",
                table: "Loans",
                newName: "PaidOff");

            migrationBuilder.AlterColumn<string>(
                name: "LoanType",
                table: "Loans",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPays_LoanId",
                table: "AutoPays",
                column: "LoanId");
        }
    }
}
