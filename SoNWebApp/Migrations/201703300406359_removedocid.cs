namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedocid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Compliances", "DocumentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Compliances", "DocumentID", c => c.Int());
        }
    }
}
