using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceMateDB.Models
{
    public class ResultModel
    {
       
    
        public int Id { get; set; }
        public virtual int EventModelId { get; set; }
        public virtual int RiderModelId { get; set; }
        public int Position { get; set; }

        public virtual RiderModel Rider { get; set; }

        public virtual EventModel Event { get; set; }

        public virtual CourseModel Course { get; set; }

    }
    
}