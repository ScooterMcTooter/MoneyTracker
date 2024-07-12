using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AdditionsToLoanModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentOwner",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DisbursementDate",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FedLoanType",
                table: "Loans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guarantor",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InterestType",
                table: "Loans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RemainingInterest",
                table: "Loans",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RepaymentPlan",
                table: "Loans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "Loans",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentOwner",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DisbursementDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "FedLoanType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Guarantor",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "InterestType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RemainingInterest",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RepaymentPlan",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "Loans");
        }
    }
}
