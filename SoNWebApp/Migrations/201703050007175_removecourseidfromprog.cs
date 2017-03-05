namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecourseidfromprog : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "CampusID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CampusID", c => c.Int(nullable: false));
        }
    }
}
