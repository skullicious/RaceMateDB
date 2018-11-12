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
    public class EventRepository
    {


        public AddEventViewModel CreateEvent()
        {
            using (var _db = new RMDBContext())

            {
                var courseRepo = new CourseRepository();

                var anEvent = new AddEventViewModel();
                {
                                    
                anEvent.Courses = courseRepo.GetCourses();
                                        
                };

                return anEvent;

            }

          

        }


    }

}