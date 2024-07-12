using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class LoanAmountColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoanName",
                table: "Loans",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LoanAmount",
                table: "Loans",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Loans",
                newName: "LoanName");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Loans",
                newName: "LoanAmount");
        }
    }
}
