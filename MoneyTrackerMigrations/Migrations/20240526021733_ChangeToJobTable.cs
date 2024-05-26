using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Jobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Jobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkLocation",
                table: "Jobs",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkLocation",
                table: "Jobs");
        }
    }
}
