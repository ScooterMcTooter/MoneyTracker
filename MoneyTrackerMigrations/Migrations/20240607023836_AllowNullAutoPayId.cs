using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullAutoPayId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoPays_Loans_LoanId",
                table: "AutoPays");

            migrationBuilder.DropIndex(
                name: "IX_AutoPays_LoanId",
                table: "AutoPays");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AutoPayId",
                table: "Loans",
                column: "AutoPayId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AutoPays_AutoPayId",
                table: "Loans",
                column: "AutoPayId",
                principalTable: "AutoPays",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AutoPays_AutoPayId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AutoPayId",
                table: "Loans");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPays_LoanId",
                table: "AutoPays",
                column: "LoanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AutoPays_Loans_LoanId",
                table: "AutoPays",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
