namespace IAmBacon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoCss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "NoCss", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "NoCss");
        }
    }
}
