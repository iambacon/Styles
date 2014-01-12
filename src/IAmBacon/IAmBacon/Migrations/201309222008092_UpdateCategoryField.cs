namespace IAmBacon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategoryField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
