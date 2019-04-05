using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectUntitled.Migrations
{
    public partial class UpdatedClipsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "Clips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "Clips");
        }
    }
}
