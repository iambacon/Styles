namespace IAmBacon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddPostsActiveColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Active", c => c.Boolean(nullable: false, defaultValue: true));
        }

        public override void Down()
        {
            DropColumn("dbo.Posts", "Active");
        }
    }
}
