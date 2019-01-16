namespace RaceMateDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAnnotationsForCourseModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseModels", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseModels", "Name", c => c.String());
        }
    }
}
