namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedoAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POS", "Course11", c => c.String());
            AddColumn("dbo.POS", "Course12", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.POS", "Course12");
            DropColumn("dbo.POS", "Course11");
        }
    }
}
