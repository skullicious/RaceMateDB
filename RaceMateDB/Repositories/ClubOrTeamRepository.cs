using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Models;
using RaceMateDB.ViewModels;
using RaceMateDB;

namespace RaceMateDB.Repositories
{
    public class ClubOrTeamRepository
    {
                
        public IEnumerable<SelectListItem> GetClubsOrTeams()
        {
            using (var _db = new RMDBContext())

            {
                List<SelectListItem> clubsOrTeams = _db.ClubOrTeamModels.AsNoTracking()
                    .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {

                        Value = n.Id.ToString(),
                        Text = n.Name.ToString()

                    }).ToList();
                

                var clubOrTeamTip = new SelectListItem()
                {
                    Value = null,
                    Text = "---Select Club---"
                };

                
                clubsOrTeams.Insert(0, clubOrTeamTip);

                return new SelectList(clubsOrTeams, "Value", "Text");

            }

        }
    }
}