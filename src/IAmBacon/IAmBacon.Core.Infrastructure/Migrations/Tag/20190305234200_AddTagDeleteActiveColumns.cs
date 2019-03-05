using Microsoft.EntityFrameworkCore.Migrations;

namespace IAmBacon.Core.Infrastructure.Migrations.Tag
{
    public partial class AddTagDeleteActiveColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Tags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("Update Tags set Active = 1");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Tags",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tags");
        }
    }
}
