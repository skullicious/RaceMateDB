using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Filters;
using RaceMateDB.Models;
using PagedList;

namespace RaceMateDControllers
{

    [Log]
    public class CourseController : Controller
    {
        RMDBContext _db = new RMDBContext();





        //use a data- attribute to wire up this action
        public ActionResult Autocomplete(string term) //term is supported paramater

        {
            var model = _db.CourseModels
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.Name         //project into object with label property - autocomplete has label prop in DOM
                });

            return Json(model, JsonRequestBehavior.AllowGet);  //serialize into json


        }


        // GET: Course
        public ActionResult Index(string searchTerm = null, int page = 1)
        {

            //  var model = _db.CourseModels.ToList();           
            //var model =
            //  from r in _db.CourseModels
            //  where searchTerm == null || r.Name.Contains(searchTerm)
            //  orderby r.Name ascending
            //  select r;


            var model = _db.CourseModels
                                       .OrderBy(r => r.Name)
                                       .Where(r => searchTerm == null || r.Name.Contains(searchTerm))
                                       .ToPagedList(page, 5);   //default page
            //   .Take(10);                           



            if (Request.IsAjaxRequest())

            {
                return PartialView("_Courses", model);
            }

            return View(model);


            
        }

                     

      //   GET: Course


       
            public ActionResult Search(string name)
             {
      
                                          
               var message = Server.HtmlEncode(name);
               return Content("We searching for " + name);

             }

        public ActionResult Events(int Id)
        {

            
            var model =
              from r in _db.EventModels
              where r.CourseId == Id
              orderby r.Name ascending
              select r;


            return View(model);
        }

        // GET: Course/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        // POST: Course/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                _db.CourseModels.Add(courseModel);
                _db.SaveChanges();             
                return RedirectToAction("Index");

            }

            return View();

        }


        // GET: CourseModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = _db.CourseModels.Find(id);

            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }





        // GET: CourseModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = _db.CourseModels.Find(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }

        // POST: CourseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseModel courseModel = _db.CourseModels.Find(id);
            _db.CourseModels.Remove(courseModel);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        //LIST FOR TESTING

        //static List<CourseModel> _courses = new List<CourseModel>
        //{
        //    new CourseModel
        //    {
        //        CourseID = 1,
        //        Name = "Hillingdon",
        //        Description = "Flat with sweeping turns"

        //    },

        //    new CourseModel
        //    {
        //        CourseID = 2,
        //        Name = "Lotus Test Track",
        //        Description = "Very flat with sweeping turns"


        //    },

        //    new CourseModel
        //    {
        //        CourseID = 3,
        //        Name = "Lee Valley Olympic Park",
        //        Description = "Flat with two short climbs + sweeping turns"


        //    },

        //    new CourseModel
        //    {
        //        CourseID = 4,
        //        Name = "Trinity Park",
        //        Description = "Flat with several sharp 90 degree turns. Very technical."


        //    },
        //};




    }
}