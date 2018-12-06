using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Models;
using PagedList;
using RaceMateDB.Repositories;




namespace OdeToRacing.Controllers
{
    public class AdminController : Controller
    {
        public RMDBContext db = new RMDBContext();

        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Upload infrastructrure for bulk imports
        /// </summary>
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }


        // GET: Course/Create
       
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }



        /// <summary>
        /// Drops user to upload confirmation page             
        /// 

        [Authorize(Roles = "Admin")]
        public ActionResult UploadCourseSuccess()
        {
            var courseModels = (List<CourseModel>)Session["UploadCourseFileSession"];
            return View(courseModels);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult UploadEventSuccess()
        {
            var eventModels = (List<EventModel>)Session["UploadEventFileSession"];
            return View(eventModels);                        
        }


        [Authorize(Roles = "Admin")]
        public ActionResult UploadResultSuccess()
        {
            var resultModels = (List<ResultModel>)Session["UploadResultFileSession"];
            return View(resultModels);
        }


        /// <summary>
        /// Commit Results of Upload screens to database
        /// </summary>
                                 
        // POST: Admin/CommitResult
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CommitResultsToDB() 
        {

            var resultModels = (List<ResultModel>)Session["UploadResultFileSession"];



            foreach (var result in resultModels)
            {
                if (ModelState.IsValid)
                {
                    db.ResultModels.Add(result);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("UploadResultSuccess");

        }
                
        // POST: Admin/CommitCourse
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CommitCoursesToDB() 
        {

            var courseModels = (List<CourseModel>)Session["UploadCourseFileSession"];

          

            foreach (var course in courseModels)
            {
                if (ModelState.IsValid)
                {
                    db.CourseModels.Add(course);
                    db.SaveChanges();
                }                              
                
            }

            return RedirectToAction("UploadCourseSuccess");
            
        }
        
        // POST: Admin/CommitEvent
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CommitEventsToDB() 
        {
            var eventModels = (List<EventModel>)Session["UploadEventFileSession"];
            
            foreach (var @event in eventModels)
            {
                if (ModelState.IsValid)
                {
                    db.EventModels.Add(@event);
                    db.SaveChanges();                    
                }
            }
            return RedirectToAction("UploadEventSuccess");
        }


        /// <summary>
        /// Amends provisional data before upload using Ajax
        /// </summary>       

        // GET: CourseModels/DeleteCourseFromSession/5
        public ActionResult DeleteCourseFromSession(string name, int page = 1, int resultsPerPage = 100)
        {           
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseModels = (List<CourseModel>)Session["UploadCourseFileSession"];

            foreach (var course in courseModels.ToList())

            {
                if (course.Name == name)
                {
                    courseModels.Remove(course);
                }                                           
            }
            return View("UploadCourseFile", courseModels.ToPagedList(page, resultsPerPage));

        }
        
        // GET: EventModels/DeleteEventFromSession/5
        public ActionResult DeleteEventFromSession(string name, int page = 1, int resultsPerPage = 100)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventModels = (List<EventModel>)Session["UploadEventFileSession"];

            foreach (var @event in eventModels.ToList())

            {
                if (@event.Name == name)
                {
                    eventModels.Remove(@event);
                }
            }
            return View("UploadEventsFile", eventModels.ToPagedList(page, resultsPerPage));

        }

        // GET: ResultModels/DeleteEventResultFromSession/5
        public ActionResult DeleteResultFromSession(string name, int page = 1, int resultsPerPage = 100)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultModels = (List<ResultModel>)Session["UploadResultFileSession"];

            foreach (var result in resultModels.ToList())

            {
                if (result.RiderName == name )
                {
                    resultModels.Remove(result);
                }
            }
            return View("UploadResultsFile", resultModels.ToPagedList(page, resultsPerPage));

        }


        /// <summary>
        /// Uploads data selected from file explorer using datareader method inside CSVDataReader Repository class
        /// </summary>       

        //// All posts here as uploading data. Using Ajax to  modify results.
       

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UploadEventsFile(HttpPostedFileBase file, int page = 1, int resultsPerPage = 100)
        {
            var importedEventModelList = new List<EventModel>();

            if (Request.IsAjaxRequest())
            {
                var eventModels = (List<EventModel>)Session["UploadEventFileSession"];
                return PartialView("_UploadEventFile", eventModels.ToPagedList(page, resultsPerPage));
            }

            if (file != null)
            {
                try
                {
                    var csvDataReader = new CsvDataReader();
                    csvDataReader.ReadEventCSV(file, importedEventModelList);

                    //Success
                    var pagedImportedEventModelList = importedEventModelList.ToPagedList(page, resultsPerPage);

                    // Save data in session so user can confirm and write to to database on next page
                    //Get working with PageList if possible
                    Session.Add("UploadEventFileSession", importedEventModelList);

                    return View(pagedImportedEventModelList);
                }



                catch
                {
                    ViewBag.Message = "File upload failed!!";
                    Console.WriteLine("Fail");
                    return View();
                }

            }

            else
            {

                ViewBag.Message = "File is null";
                Console.WriteLine("File is null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return View(index);

            }
        }
               
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UploadCourseFile(HttpPostedFileBase file, int page = 1, int resultsPerPage = 100)
        {
            var importedCourseModelList = new List<CourseModel>();

           if (Request.IsAjaxRequest())

           {
              var courseModels = (List<CourseModel>)Session["UploadCourseFileSession"];
              return PartialView("_UploadCourseFile", courseModels.ToPagedList(page, resultsPerPage));
           }

            if (file != null)
            {



                try
                {
                    //instantiate csvDataReader to read course CSV
                    var csvDataReader = new CsvDataReader();
                    csvDataReader.ReadCourseCsv(file, importedCourseModelList);

                    //Success
                    var pagedImportedCourseModelList = importedCourseModelList.ToPagedList(page, resultsPerPage);

                    // Save data in session so user can confirm and write to to database on next page
                    //Get working with PageList if possible
                    Session.Add("UploadCourseFileSession", importedCourseModelList);
                    return View(pagedImportedCourseModelList);
                }


                catch
                {

                    // Is there something better we can do here?
                    ViewBag.Message = "File upload failed!!";
                    Console.WriteLine("Fail");
                    return View();
                }

            }
            else
            {

                ViewBag.Message = "File is null";
                Console.WriteLine("File is null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);               

            }

        }
               
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UploadResultsFile(HttpPostedFileBase file, int page = 1, int resultsPerPage = 100)
        {
            var importedResultModelList = new List<ResultModel>();

            if (Request.IsAjaxRequest())
            {
                var resultModels = (List<EventModel>)Session["UploadResultFileSession"];
                return PartialView("_UploadResultFile", resultModels.ToPagedList(page, resultsPerPage));
            }
            if (file!=null) {
                try
                {
                    var csvDataReader = new CsvDataReader();
                    csvDataReader.ReadResultCsv(file, importedResultModelList);

                    //Success
                    var pagedImportedResultModelList = importedResultModelList.ToPagedList(page, resultsPerPage);

                    // Save data in session so user can confirm and write to to database on next page
                    //Get working with PageList if possible
                    Session.Add("UploadResultFileSession", importedResultModelList);

                    return View(pagedImportedResultModelList);
                }

                catch
                {
                    ViewBag.Message = "File upload failed!!";
                    Console.WriteLine("Fail");
                    return View();
                }
            }
            else
            {

                ViewBag.Message = "File is null";
                Console.WriteLine("File is null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return View(index);

            }
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }    
}

