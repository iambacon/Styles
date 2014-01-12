namespace IAmBacon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeoNameFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "SeoName", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Posts", "SeoTitle", c => c.String(nullable: false, maxLength: 510));
            AddColumn("dbo.Tags", "SeoName", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "SeoName");
            DropColumn("dbo.Posts", "SeoTitle");
            DropColumn("dbo.Categories", "SeoName");
        }
    }
}
