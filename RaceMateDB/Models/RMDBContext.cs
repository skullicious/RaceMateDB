using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RaceMateDB.Models;


namespace RaceMateDB.Models
{
    public class RMDBContext : DbContext
    {
        public DbSet<EventModel> EventModels { get; set; }
        public DbSet<EventReview> EventReviews { get; set; }
        public DbSet<ResultModel> ResultModels { get; set; }
        public DbSet<CourseModel> CourseModels { get; set; }
        public DbSet<RiderModel> RiderModels { get; set; }
     


    }


}