using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceMateDB.Models
{
    

    public class CourseModel
               

      
    {
        public int Id { get; set; }           
        public string Name { get; set; }      
        public string Description { get; set; }

        public string VeloViewerURL { get; set; }

        public virtual ICollection<EventModel> Events { get; set; }
    

    }

}