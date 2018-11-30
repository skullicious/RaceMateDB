using RaceMateDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceMateDB.Models
{
    public class ClubOrTeamModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<RiderModel> RiderModels { get; set; }

     
    }


}
