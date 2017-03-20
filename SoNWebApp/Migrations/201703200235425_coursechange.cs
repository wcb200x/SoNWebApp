namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coursechange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "CatalogNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CatalogNumber", c => c.Int(nullable: false));
        }
    }
}
