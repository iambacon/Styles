using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAmBacon.Core.Infrastructure.Migrations.User
{
    public partial class NewContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Active = table.Column<bool>(nullable: false),
            //        Deleted = table.Column<bool>(nullable: false),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        Email = table.Column<string>(maxLength: 100, nullable: false),
            //        FirstName = table.Column<string>(nullable: false),
            //        LastName = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
