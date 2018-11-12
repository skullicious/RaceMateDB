using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Models;
using System.Data.Entity;
using RaceMateDB.Repositories;
using RaceMateDB.ViewModels;

namespace RaceMateDB.Controllers

{



    public class EventController : Controller
    {


        RMDBContext _db = new RMDBContext();



        // GET: Event
        public ActionResult Index(string searchTerm)
        {


            // var model = _db.EventModels.ToList();

            var model =
                        from r in _db.EventModels
                        where searchTerm == null || r.Name.Contains(searchTerm)
                        orderby r.Name descending
                        select r;

            
            return View(model);
                        
         


        }


        // GET: Courses Events
        public ActionResult ShowEvents([Bind(Prefix="id")] int courseId)
        {
            var course = _db.CourseModels.Find(courseId);
            if (course != null)
            {
                
                return View(course);
                    
             }
            return HttpNotFound();

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
        public ActionResult Create()
        {

            var repo = new EventRepository();

            var addEventViewModel = repo.CreateEvent();

            return View(addEventViewModel);
        }




        // POST: Event/Create
        [HttpPost]
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

