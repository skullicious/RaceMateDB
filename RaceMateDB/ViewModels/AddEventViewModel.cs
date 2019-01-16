using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RaceMateDB.Models;

namespace RaceMateDB.ViewModels
{
    public class AddEventViewModel
    {
      

        [Required(ErrorMessage = "Please enter an event name..")]
        [Display(Name = "Event Name")]
        [StringLength(75)]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Please select a valid date..")]
        [Display(Name = "Event Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select a valid course..")]
        [Display(Name = "Course")]
        public string SelectedCourse { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }

        
       
        public int ResultId { get; set; }       

        public int? EventId { get; set; }








    }
}