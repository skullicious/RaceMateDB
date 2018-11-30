using RaceMateDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RaceMateDB.Repositories
{
    public class CourseRepository
    {
        public IEnumerable<SelectListItem> GetCourses()
        {
            using (var _db = new RMDBContext())

            {
                List<SelectListItem> courses = _db.CourseModels.AsNoTracking()
                    .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        
                        Value = n.Id.ToString(),
                        Text = n.Name.ToString()
                        
                    }).ToList();
                                           

                
                var courseTip = new SelectListItem()
                {
                    Value = null,
                    Text = "---Select Course---"
                };



                courses.Insert(0, courseTip);

                return new SelectList(courses, "Value", "Text");

            }

        }

    }
}