using Microsoft.EntityFrameworkCore.Migrations;

namespace IAmBacon.Core.Infrastructure.Migrations.Post
{
    public partial class AddPostDeletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Posts");
        }
    }
}
