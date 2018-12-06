using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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




        // Added for result to be built when uploading.. Should create separate view model?
        [NotMapped]
        public string EventName { get; set; }
        [NotMapped]
        public string RiderName { get; set; }

        // This field used to calculate predictedResults
        [NotMapped]
        public int WeightedPoints { get; set; }

        
    }

}