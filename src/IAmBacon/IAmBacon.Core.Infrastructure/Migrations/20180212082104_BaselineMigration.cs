using Microsoft.EntityFrameworkCore.Migrations;

namespace IAmBacon.Core.Infrastructure.Migrations
{
    public partial class BaselineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Empty migration because we are using an existing db
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
