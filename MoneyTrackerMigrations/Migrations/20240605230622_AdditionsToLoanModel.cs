using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AdditionsToLoanModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoanType",
                table: "Loans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentLoanType",
                table: "Loans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalInterest",
                table: "Loans",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "StudentLoanType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "Loans");
        }
    }
}
