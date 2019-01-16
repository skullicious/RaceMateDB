namespace RaceMateDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAnnotationsForCourseModelv2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseModels", "Description", c => c.String(nullable: true));
            AlterColumn("dbo.CourseModels", "VeloViewerURL", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseModels", "VeloViewerURL", c => c.String());
            AlterColumn("dbo.CourseModels", "Description", c => c.String());
        }
    }
}
