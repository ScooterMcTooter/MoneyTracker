using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTypeReferenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "Transactions",
                newName: "TransactionTypeId");

            migrationBuilder.AddColumn<string>(
                name: "OtherDescription",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "AutoPays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "Debit" },
                    { 3, "Cash" },
                    { 4, "Credit (Debit Card)" },
                    { 5, "Credit (Credit Card)" },
                    { 6, "ApplePay" },
                    { 7, "Venmo" },
                    { 8, "PayPal" },
                    { 9, "CashApp" },
                    { 10, "ACHRecurring" },
                    { 11, "ACHOnce" },
                    { 12, "Check" },
                    { 13, "InternalTransfer" },
                    { 14, "ExternalTransfer" },
                    { 15, "ATM Withdrawal" },
                    { 16, "ATM Deposit" },
                    { 17, "Mobile Deposit" },
                    { 18, "Deposit" },
                    { 19, "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoPays_TransactionTypeId",
                table: "AutoPays",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoPays_TransactionTypes_TransactionTypeId",
                table: "AutoPays",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoPays_TransactionTypes_TransactionTypeId",
                table: "AutoPays");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_AutoPays_TransactionTypeId",
                table: "AutoPays");

            migrationBuilder.DropColumn(
                name: "OtherDescription",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "AutoPays");

            migrationBuilder.RenameColumn(
                name: "TransactionTypeId",
                table: "Transactions",
                newName: "TransactionType");
        }
    }
}
