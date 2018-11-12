using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Filters;
using RaceMateDB.Models;

namespace RaceMateDControllers
{

    [Log]
    public class CourseController : Controller
    {
        RMDBContext _db = new RMDBContext();
       

        // GET: Course
        public ActionResult Index(string searchTerm = null)
        {

            //  var model = _db.CourseModels.ToList();
           
            var model =
              from r in _db.CourseModels
              where searchTerm == null || r.Name.Contains(searchTerm)
              orderby r.Name ascending
              select r;
          

            return View(model);
        }

                     

      //   GET: Course


       //   public ActionResult Search(string name = "Hillingdon")
            public ActionResult Search(string name)
             {
        //  throw new Exception("OH MY GAWD!");
               
                           
               var message = Server.HtmlEncode(name);
               return Content("We searching for " + name);

             }

        public ActionResult Events(int Id)
        {

            //  var model = _db.CourseModels.ToList();
            var model =
              from r in _db.EventModels
              where r.CourseId == Id
              orderby r.Name ascending
              select r;


            return View(model);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Course/Create
        [HttpPost]
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