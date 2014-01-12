namespace IAmBacon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "DateModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "DateModified");
        }
    }
}
