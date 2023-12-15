using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballClubAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwayTeamScore",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamScore",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeamScore",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "HomeTeamScore",
                table: "Match");
        }
    }
}
