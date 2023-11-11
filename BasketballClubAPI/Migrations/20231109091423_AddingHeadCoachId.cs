using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballClubAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingHeadCoachId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Coach_HeadCoachId",
                table: "Team");

            migrationBuilder.AlterColumn<int>(
                name: "HeadCoachId",
                table: "Team",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Coach_HeadCoachId",
                table: "Team",
                column: "HeadCoachId",
                principalTable: "Coach",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Coach_HeadCoachId",
                table: "Team");

            migrationBuilder.AlterColumn<int>(
                name: "HeadCoachId",
                table: "Team",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Coach_HeadCoachId",
                table: "Team",
                column: "HeadCoachId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
