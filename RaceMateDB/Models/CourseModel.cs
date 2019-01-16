using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RaceMateDB.Models
{
    

    public class CourseModel
               

      
    {
        public int Id { get; set; }          
        
        [Required(ErrorMessage = "Please enter a course name..")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description of the course..")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a valid veloviewer url.. For example, https://veloviewer.com/segments/xxxxx/embed2")]
        public string VeloViewerURL { get; set; }

        public virtual ICollection<EventModel> Events { get; set; }
    

    }

}