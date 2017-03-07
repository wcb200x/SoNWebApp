namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POScourses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POS", "course1", c => c.String());
            AddColumn("dbo.POS", "course2", c => c.String());
            AddColumn("dbo.POS", "course3", c => c.String());
            AddColumn("dbo.POS", "course4", c => c.String());
            AddColumn("dbo.POS", "course5", c => c.String());
            AddColumn("dbo.POS", "course6", c => c.String());
            AddColumn("dbo.POS", "course7", c => c.String());
            AddColumn("dbo.POS", "course8", c => c.String());
            AddColumn("dbo.POS", "course9", c => c.String());
            AddColumn("dbo.POS", "course10", c => c.String());
            AddColumn("dbo.POS", "course11", c => c.String());
            AddColumn("dbo.POS", "course12", c => c.String());
            AddColumn("dbo.POS", "course13", c => c.String());
            AddColumn("dbo.POS", "course14", c => c.String());
            AddColumn("dbo.POS", "course15", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.POS", "course15");
            DropColumn("dbo.POS", "course14");
            DropColumn("dbo.POS", "course13");
            DropColumn("dbo.POS", "course12");
            DropColumn("dbo.POS", "course11");
            DropColumn("dbo.POS", "course10");
            DropColumn("dbo.POS", "course9");
            DropColumn("dbo.POS", "course8");
            DropColumn("dbo.POS", "course7");
            DropColumn("dbo.POS", "course6");
            DropColumn("dbo.POS", "course5");
            DropColumn("dbo.POS", "course4");
            DropColumn("dbo.POS", "course3");
            DropColumn("dbo.POS", "course2");
            DropColumn("dbo.POS", "course1");
        }
    }
}
