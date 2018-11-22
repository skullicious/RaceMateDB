﻿using System;
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


namespace OdeToRacing.Controllers
{
    public class AdminController : Controller
    {
        private RMDBContext db = new RMDBContext();

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




        // POST: Admin/CommitCourse
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CommitCoursesToDB() //List<CourseModel> courseModels
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

            return RedirectToAction("UploadEventSuccess");


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






        // GET: CourseModels/Delete/5
        public ActionResult Delete(string name, int page = 1, int resultsPerPage = 100)
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











        //Get is to look through imported results and check they look okay.
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UploadCourseFile(int page = 1, int resultsPerPage = 100)


        {
            var importedCourseModelList = new List<CourseModel>();


            if (Request.IsAjaxRequest())

            {
                var courseModels = (List<CourseModel>)Session["UploadCourseFileSession"];
                return PartialView("_UploadCourseFile", courseModels.ToPagedList(page, resultsPerPage));
            }

            return View();

        }

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


           

            try
            {
                                

                if (file.ContentLength > 0)
                {



                    var column1 = new List<string>();
                    var column2 = new List<string>();

                    using (var rdr = new StreamReader(file.InputStream))
                    {
                        //   while (!rdr.EndOfStream)

                        rdr.ReadLine();                     //Peeks first line and ignores it.
                        while (rdr.Peek() != -1)
                        {
                            var splits = rdr.ReadLine().Split(',');

                            if (!String.IsNullOrEmpty(splits[0]))
                            {

                                var newEvent = new EventModel();

                                newEvent.Name = splits[0];

                                // GET COURSEIDBYNAMEMETHOD                                                             
                                string searchTerm = splits[1]; // Array reference will not run inside of query

                                var eventCourseModel = db.CourseModels                                                
                                                   .Where(r => r.Name.Contains(searchTerm));  //Need some whitespace handling!!!
                                                                                              
                                
                                newEvent.CourseId = eventCourseModel.FirstOrDefault().Id;                           //Sets up pnjects         
                                newEvent.CourseName = eventCourseModel.FirstOrDefault().Name;
                                newEvent.Date = Convert.ToDateTime(splits[2]);                                
                                
                                
                                importedEventModelList.Add(newEvent);

                                Console.WriteLine(newEvent.Name + " " + newEvent.Date.ToString() +  " added to model");


                            }


                         

                        }
                        Console.WriteLine("Done");

                    }


                }




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

            try
            {
               

                if (file.ContentLength > 0)
                {

                                       

                    var column1 = new List<string>();
                    var column2 = new List<string>();

                    using (var rdr = new StreamReader(file.InputStream))
                    {
                        while (!rdr.EndOfStream)
                        {
                            var splits = rdr.ReadLine().Split(',');

                            if (!String.IsNullOrEmpty(splits[0]))
                            {

                                var newCourse = new CourseModel();

                                newCourse.Name = splits[0];
                                newCourse.Description = splits[1];
                                newCourse.VeloViewerURL = splits[2];
                                importedCourseModelList.Add(newCourse);

                                Console.WriteLine(newCourse.Name + "added to model");


                            }

                          

                        }
                        Console.WriteLine("Done");

                    }

                    
                }


            

                //Success


                var pagedImportedCourseModelList = importedCourseModelList.ToPagedList(page, resultsPerPage);

                

                // Save data in session so user can confirm and write to to database on next page
                //Get working with PageList if possible
                Session.Add("UploadCourseFileSession", importedCourseModelList);

                return View(pagedImportedCourseModelList);


            }
            

            catch
            {
                ViewBag.Message = "File upload failed!!";
                Console.WriteLine("Fail");
                return View();
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
