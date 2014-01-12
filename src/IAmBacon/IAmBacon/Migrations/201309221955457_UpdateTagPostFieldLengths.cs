namespace IAmBacon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTagPostFieldLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
