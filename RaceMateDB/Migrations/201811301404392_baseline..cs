namespace RaceMateDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baseline : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClubOrTeamModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        VeloViewerURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        CourseName = c.String(),
                        CourseId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        HasResult = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseModels", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.ResultModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        Course_Id = c.Int(),
                        Event_Id = c.Int(),
                        Rider_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseModels", t => t.Course_Id)
                .ForeignKey("dbo.EventModels", t => t.Event_Id)
                .ForeignKey("dbo.RiderModels", t => t.Rider_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Rider_Id);
            
            CreateTable(
                "dbo.RiderModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Review = c.String(),
                        EventId = c.Int(nullable: false),
                        ReviewerName = c.String(),
                        EventModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventModels", t => t.EventModelId, cascadeDelete: true)
                .Index(t => t.EventModelId);
            
            CreateTable(
                "dbo.RiderModelEventModels",
                c => new
                    {
                        RiderModel_Id = c.Int(nullable: false),
                        EventModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RiderModel_Id, t.EventModel_Id })
                .ForeignKey("dbo.RiderModels", t => t.RiderModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.EventModels", t => t.EventModel_Id, cascadeDelete: true)
                .Index(t => t.RiderModel_Id)
                .Index(t => t.EventModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventReviews", "EventModelId", "dbo.EventModels");
            DropForeignKey("dbo.RiderModelEventModels", "EventModel_Id", "dbo.EventModels");
            DropForeignKey("dbo.RiderModelEventModels", "RiderModel_Id", "dbo.RiderModels");
            DropForeignKey("dbo.ResultModels", "Rider_Id", "dbo.RiderModels");
            DropForeignKey("dbo.ResultModels", "Event_Id", "dbo.EventModels");
            DropForeignKey("dbo.ResultModels", "Course_Id", "dbo.CourseModels");
            DropForeignKey("dbo.EventModels", "CourseId", "dbo.CourseModels");
            DropIndex("dbo.RiderModelEventModels", new[] { "EventModel_Id" });
            DropIndex("dbo.RiderModelEventModels", new[] { "RiderModel_Id" });
            DropIndex("dbo.EventReviews", new[] { "EventModelId" });
            DropIndex("dbo.ResultModels", new[] { "Rider_Id" });
            DropIndex("dbo.ResultModels", new[] { "Event_Id" });
            DropIndex("dbo.ResultModels", new[] { "Course_Id" });
            DropIndex("dbo.EventModels", new[] { "CourseId" });
            DropTable("dbo.RiderModelEventModels");
            DropTable("dbo.EventReviews");
            DropTable("dbo.RiderModels");
            DropTable("dbo.ResultModels");
            DropTable("dbo.EventModels");
            DropTable("dbo.CourseModels");
            DropTable("dbo.ClubOrTeamModels");
        }
    }
}
