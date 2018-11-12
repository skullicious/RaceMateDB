namespace RaceMateDb.Migrations
{
    using RaceMateDB.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RMDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RMDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.CourseModels.AddOrUpdate(r => r.Name,

                      new CourseModel
                      {

                          Name = "Hillingdon",

                          Events = new List<EventModel>
                          {
                               new EventModel
                               {
                                  Name = "Full Gas Crit #1",

                                  EventReviews = new List <EventReview>
                                  {
                                      new EventReview
                                      {
                                          Review = "Full Gas at Hillingdon was great!",
                                          ReviewerName = "Hilligdon Roadman"
                                      }
                                  }

                               },

                               new EventModel
                               {
                                  Name = "Full Gas Crit #2",
                                  EventReviews = new List <EventReview>
                                  {
                                      new EventReview
                                      {
                                          Review = "Full Gas at Hillingdon was great!",
                                          ReviewerName = "Hillingdon Roadman #2"
                                      }
                                  }
                               },
                                new EventModel
                                {
                                  Name = "Full Gas Crit #3",
                                  EventReviews = new List <EventReview>
                                  {
                                      new EventReview
                                      {
                                          Review = "Full Gas at Hillingdon was great!",
                                          ReviewerName = "Hillingdon Roadman #3"
                                      },

                                      new EventReview
                                      {
                                          Review = "Full Gas at Hillingdon was okay!",
                                          ReviewerName = "Hillingdon Roadman #4"
                                      },

                                      new EventReview
                                      {
                                          Review = "Full Gas at Hillingdon was the bomb!",
                                          ReviewerName = "Hillingdon Roadman #5"
                                      },

                                      new EventReview
                                      {
                                          Review = "Full Gas at Hillingdon was scary!",
                                          ReviewerName = "Hillingdon Roadman #6"
                                      },
                                  }

                                }
                          }



                      },


            new CourseModel
            {

                Name = "Lee Valley",

                Events = new List<EventModel>
                          {
                               new EventModel
                               {
                                  Name = "Go Ride Crit #1",

                                  EventReviews = new List <EventReview>
                                  {
                                      new EventReview
                                      {
                                          Review = "Go Ride at Lee Valley was great!",
                                          ReviewerName = "Lee Valley Roadman"
                                      }
                                  }

                               },

                               new EventModel
                               {
                                  Name = "Go Ride Crit #2",
                                  EventReviews = new List <EventReview>
                                  {
                                      new EventReview
                                      {
                                          Review = "Go Ride at Lee Valley was great!",
                                          ReviewerName = "Lee Valley Roadman #2"
                                      }
                                  }
                               },
                                new EventModel
                                {
                                  Name = "Go Ride Crit #3",
                                  EventReviews = new List <EventReview>
                                  {
                                      new EventReview
                                      {
                                          Review = "Go Ride at Lee Valley was great!",
                                          ReviewerName = "Lee Valley Roadman #3"
                                      }
                                  }

                                }

                }
            });

        }






    }


                
    
}
