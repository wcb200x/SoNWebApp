namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredgradeinenrollment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enrollments", "Semester", c => c.String(nullable: false));
            AlterColumn("dbo.Enrollments", "Grade", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enrollments", "Grade", c => c.String());
            AlterColumn("dbo.Enrollments", "Semester", c => c.String());
        }
    }
}
