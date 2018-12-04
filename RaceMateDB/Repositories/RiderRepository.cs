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
    public class RiderRepository
    {


        public RiderEditViewModel EditRider()
        {
            using (var _db = new RMDBContext())

            {
                var clubOrTeamRepo = new ClubOrTeamRepository();

                var aRider = new RiderEditViewModel();
                {                             
               
                aRider.ClubsOrTeams = clubOrTeamRepo.GetClubsOrTeams();
                                        
                };

                return aRider;

            }

          

        }




        


    }

}