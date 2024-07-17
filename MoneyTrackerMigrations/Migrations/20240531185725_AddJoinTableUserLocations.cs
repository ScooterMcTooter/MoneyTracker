using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinTableUserLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationModelId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Locations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Locations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "userLocations",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userLocations", x => new { x.UserId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_userLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userLocations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationModelId",
                table: "Users",
                column: "LocationModelId");

            migrationBuilder.CreateIndex(
                name: "IX_userLocations_LocationId",
                table: "userLocations",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_LocationModelId",
                table: "Users",
                column: "LocationModelId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_LocationModelId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "userLocations");

            migrationBuilder.DropIndex(
                name: "IX_Users_LocationModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocationModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Locations");
        }
    }
}
