using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    Zip = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_LocationId",
                table: "Jobs",
                column: "LocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Locations_LocationId",
                table: "Jobs",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Locations_LocationId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_LocationId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Jobs",
                type: "TEXT",
                nullable: true);
        }
    }
}
