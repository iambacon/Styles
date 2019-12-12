using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAmBacon.Core.Infrastructure.Migrations.Post
{
    public partial class NewContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Posts",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Active = table.Column<bool>(nullable: false),
            //        AuthorId = table.Column<int>(nullable: false),
            //        CategoryId = table.Column<int>(nullable: false),
            //        Content = table.Column<string>(type: "varchar(MAX)", nullable: false),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        Image = table.Column<string>(type: "varchar(MAX)", nullable: true),
            //        Markdown = table.Column<string>(type: "varchar(MAX)", nullable: true),
            //        NoCss = table.Column<bool>(nullable: false),
            //        SeoTitle = table.Column<string>(maxLength: 510, nullable: true),
            //        Title = table.Column<string>(maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Posts", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tag",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Deleted = table.Column<bool>(nullable: false),
            //        Name = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tag", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PostTags",
            //    columns: table => new
            //    {
            //        PostId = table.Column<int>(nullable: false),
            //        TagId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PostTags", x => new { x.PostId, x.TagId });
            //        table.ForeignKey(
            //            name: "FK_PostTags_Posts_PostId",
            //            column: x => x.PostId,
            //            principalTable: "Posts",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PostTags_Tag_TagId",
            //            column: x => x.TagId,
            //            principalTable: "Tag",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_PostTags_TagId",
            //    table: "PostTags",
            //    column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
