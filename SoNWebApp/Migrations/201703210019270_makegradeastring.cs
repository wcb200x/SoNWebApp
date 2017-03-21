namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makegradeastring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enrollments", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enrollments", "Grade", c => c.Int());
        }
    }
}
