using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Models;
using System.Data.Entity;
using RaceMateDB.Repositories;
using RaceMateDB.ViewModels;
using PagedList;
using System.Net;


namespace RaceMateDB.Controllers

{



    public class EventController : Controller
    {


        RMDBContext _db = new RMDBContext();




        //use a data- attribute to wire up this action
        public ActionResult Autocomplete(string term) //term is supported paramater

        {
            var model = _db.EventModels
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.Name         //project into object with label property - autocomplete has label prop in DOM
                });

            return Json(model, JsonRequestBehavior.AllowGet);  //serialize into json


        }



        // GET: Event
        public ActionResult Index(string searchTerm, int page = 1)
        {

                      
            var model = _db.EventModels
                                       .OrderBy(r => r.Name)
                                       .Where(r => searchTerm == null || r.Name.Contains(searchTerm))
                                       .ToPagedList(page, 5);


            if (Request.IsAjaxRequest())

            {
                return PartialView("_Events", model);
            }

            return View(model);

            
                        
         


        }


        // GET: Courses Events
        public ActionResult CourseEvents([Bind(Prefix="id")] int courseId, int page = 1)
        {
            //var courseEvents = _db.EventModels.Find(courseId);
            //if (courseEvents != null)
            //{

            //    return View(courseEvents);

            //}
            //return HttpNotFound();


            var model = _db.EventModels
                                      .OrderBy(r => r.Name)
                                     .Where(r => r.CourseId == courseId)
                                     .ToPagedList(page, 5);


           if (Request.IsAjaxRequest())

            {

              return PartialView("_Events", model);
               
            }

           return View(model);





        }

        // get: event
        public ActionResult results(int id)
        {


            var model = _db.ResultModels
                                        .Include(e => e.Event)
                                      .Include(e => e.Rider)
                                      .Where(i => i.EventModelId == id);


           
                return View(model);
          
        }









        // GET: Event/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            var repo = new EventRepository();

            var addEventViewModel = repo.CreateEvent();

            return View(addEventViewModel);
        }




        // POST: Event/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(AddEventViewModel addEventViewModel)
        {
            
            var model = new EventModel()

            {
                Name = addEventViewModel.EventName,
                Date = addEventViewModel.Date,

                //Should I be converting this backward and forwards??
                CourseId = Convert.ToInt32(addEventViewModel.SelectedCourse)
       
            };

            if (ModelState.IsValid)
            {
                _db.EventModels.Add(model);
               
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = model.CourseId });

            }

            return View(model);

        }




        // GET: Event/ViewEventReview
        [HttpGet]
        public ActionResult ViewEventReview([Bind(Prefix = "id")] int id)
        {

            var model =
                    from r in _db.EventReviews
                    where (r.EventModelId== id)
                    select r;

            
            return View(model);
        }


        //// GET: Event/Comment
        //[HttpGet]
        //public ActionResult EventReview(int id)
        //{
        //    var model =
        //           from r in _db.EventReviews
        //           where (r.EventModelId == id)
        //           select r;


        //    return View(model);
        //}

        // GET: Event/AddEventReview
       
      

    
         [HttpGet]
         public ActionResult AddEventReview(int eventModelId)
        {
            return View();
        }
 




        // POST: Event/AddEventReview
        [HttpPost]
        public ActionResult AddEventReview(EventReview eventReview)
        {
            if (ModelState.IsValid)
            {
                _db.EventReviews.Add(eventReview);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = eventReview.EventModelId });

            }

            return View(eventReview);

        }






        //Get: /Event/Edit/5

        [HttpGet]
        public ActionResult EditEventReview(int Id)
            {
            var model = _db.EventReviews.Find(Id);
          
            return View(model);

             }




        //Post: /Event/Edit/5


        [HttpPost]
        public ActionResult EditEventReview(EventReview eventReview)
        {
            if (ModelState.IsValid)


            {
                _db.Entry(eventReview).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = eventReview.EventModelId });

            }
                       

            return View(eventReview);
        }






        // POST: Event/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(AddEventViewModel addEventViewModel)
        {
            if (addEventViewModel.EventId == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var model = new EventModel()

            {
                
                Id = (int)addEventViewModel.EventId,
                Name = addEventViewModel.EventName,
                Date = addEventViewModel.Date,

                //Should I be converting this backward and forwards??
                CourseId = Convert.ToInt32(addEventViewModel.SelectedCourse)

            };

            if (ModelState.IsValid)
            {

            

                
                
                _db.Entry(model).State = EntityState.Modified;
               _db.SaveChanges();
                return RedirectToAction("Index", new { id = model.Id});

            }

            return View(addEventViewModel);

        }












        // GET: EventModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var courseRepo = new CourseRepository();
            //stick some includes here

            //EventModel editEventModel = _db.EventModels.Find(id);
            EventModel editEventModel = _db.EventModels
                                        .Include(i => i.EventResults)
                                        .Where(i => i.EventResults.FirstOrDefault().EventModelId == id)                                      
                                        .Single();
                                                
            AddEventViewModel addEventViewModel = new AddEventViewModel();

            addEventViewModel.EventId = editEventModel.EventResults.FirstOrDefault().EventModelId;
            addEventViewModel.Courses = courseRepo.GetCourses();
            addEventViewModel.EventName = editEventModel.Name;
            addEventViewModel.Date = editEventModel.Date;
            addEventViewModel.SelectedCourse = editEventModel.Course.Name;
                                
            return View(addEventViewModel);
                             
        }


        // GET: EventModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventModel eventModel = _db.EventModels.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // POST: EventModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventModel eventModel = _db.EventModels.Find(id);
            _db.EventModels.Remove(eventModel);
            _db.SaveChanges();
            return RedirectToAction("Index");
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

