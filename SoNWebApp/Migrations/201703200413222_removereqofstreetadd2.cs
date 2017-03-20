namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removereqofstreetadd2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UDApplications", "StreetAddress2", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UDApplications", "StreetAddress2", c => c.String(nullable: false));
        }
    }
}
