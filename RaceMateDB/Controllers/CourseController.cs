using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Filters;
using RaceMateDB.Models;
using PagedList;
using RaceMateDB.Repositories;
using System.Data.Entity;

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





        // GET: CourseModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }                       

         CourseModel courseModel = _db.CourseModels                                     
                                .Where(i => i.Id == id)
                                .Single();
                       

            return View(courseModel);
        }

        // POST: CourseModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,VeloViewerUrl")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(courseModel).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseModel);
        }



        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }


            base.Dispose(disposing);

        }

    }
}