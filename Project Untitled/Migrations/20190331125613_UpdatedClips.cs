using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Untitled.Migrations
{
    public partial class UpdatedClips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Livestream",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Periscope",
                table: "UserSettings");

            migrationBuilder.AddColumn<string>(
                name: "ContentCreator",
                table: "Clips",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentPlatform",
                table: "Clips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Clips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentCreator",
                table: "Clips");

            migrationBuilder.DropColumn(
                name: "ContentPlatform",
                table: "Clips");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Clips");

            migrationBuilder.AddColumn<string>(
                name: "Livestream",
                table: "UserSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Periscope",
                table: "UserSettings",
                nullable: true);
        }
    }
}
