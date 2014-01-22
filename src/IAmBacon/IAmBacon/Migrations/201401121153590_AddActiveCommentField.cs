namespace IAmBacon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiveCommentField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Active", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Active");
        }
    }
}
