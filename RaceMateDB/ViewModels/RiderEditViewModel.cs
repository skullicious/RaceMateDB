using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RaceMateDB.ViewModels
{
    public class RiderEditViewModel
    {
        [Display(Name = "Rider Id")]
        public int RiderID { get; set; }

        [Required(ErrorMessage = "Please enter the rider name..")]
        [Display(Name = "Rider Name")]
        [StringLength(75)]
        public string RiderName { get; set; }

        [Required(ErrorMessage = "Please select a club or team..")]
        [Display(Name = "Club or Team")]
        public string SelectedClubOrTeam { get; set; }
        public IEnumerable<SelectListItem> ClubsOrTeams {get; set;}
        
    }
}