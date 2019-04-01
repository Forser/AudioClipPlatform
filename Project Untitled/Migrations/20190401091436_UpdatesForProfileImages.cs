using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Untitled.Migrations
{
    public partial class UpdatesForProfileImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeaderImage",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "UserSettings");

            migrationBuilder.CreateTable(
                name: "ProfileImage",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfileImg = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImage", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProfileImage_UserSettings_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserSettings",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_OwnerId",
                table: "ProfileImage",
                column: "OwnerId",
                unique: true,
                filter: "[OwnerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileImage");

            migrationBuilder.AddColumn<string>(
                name: "HeaderImage",
                table: "UserSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "UserSettings",
                nullable: true);
        }
    }
}
