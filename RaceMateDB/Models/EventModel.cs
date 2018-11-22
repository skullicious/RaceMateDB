using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceMateDB.Models
{
    public class EventModel
    {

        public EventModel()
        {
            EventResults = new List<ResultModel>
            {
               new ResultModel

               {
                   RiderModelId = 1,
                   Position = 0
                   

               }

            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }


        //lzy lod
        public virtual CourseModel Course { get; set; }
        public virtual string CourseName { get; set; } //should this be here??

        //lzy lod
        public virtual int CourseId { get; set; }

        public ICollection<EventReview> EventReviews { get; set; }

        public ICollection<ResultModel> EventResults { get; set; }

        public virtual ICollection<RiderModel> RiderModels { get; set; }

        public DateTime Date { get; set; }
       
        public bool? HasResult { get; set; }

    }
}