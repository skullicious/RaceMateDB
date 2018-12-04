using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceMateDB.Models
{
    public class RiderModel
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public ICollection<ResultModel> EventResults { get; set; }

        public ICollection<EventModel> Events { get; set; }

        //lzy lod
        public virtual int ClubOrTeamId { get; set; }

        public virtual ClubOrTeamModel ClubOrTeam {get; set;}
    }
}