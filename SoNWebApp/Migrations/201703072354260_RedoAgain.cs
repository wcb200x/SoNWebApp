namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedoAgain : DbMigration
    {
        public override void Up()
        {
          
        }
        
        public override void Down()
        {
            DropColumn("dbo.POS", "Course13");
            DropColumn("dbo.POS", "Course14");
            DropColumn("dbo.POS", "Course15");
        }
    }
}
