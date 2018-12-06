using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RaceMateDB.ViewModels
{
    public class PredictedResultViewModel
    {     

        [Display(Name = "Rider Id")]
        public int RiderID { get; set; }
        
        [Required]
        [Display(Name = "Rider Name")]
        [StringLength(75)]
        public string RiderName { get; set; }

        [Required]
        [Display(Name = "Result")]
        public string Result{ get; set; }
        public IEnumerable<SelectListItem> Results {get; set;}


        [Required]
        [Display(Name = "ResultWeighting")]
        public int ResultWeighting { get; set; }

        [Required]
        [Display(Name = "NumberOfResults")]
        public int NumberofResults { get; set; }


        




    }
}