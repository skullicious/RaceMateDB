using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Models;
using PagedList;


namespace RaceMateDB.Controllers
{
    public class RiderController : Controller
    {

        RMDBContext _db = new RMDBContext();





        //use a data- attribute to wire up this action
        public ActionResult Autocomplete(string term) //term is supported paramater

        {
            var model = _db.RiderModels
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new     
                {
                    label = r.Name         //project into object with label property - autocomplete has label prop in DOM
                });

            return Json(model, JsonRequestBehavior.AllowGet);  //serialize into json


        }


        // GET: Rider
        public ActionResult Index(string searchTerm, int page =1)   //add default page for pagelist
        {


            //var model =
            // from r in _db.RiderModels
            // where searchTerm == null || r.Name.Contains(searchTerm)
            // orderby r.Name ascending
            // select r;

            //refactored into expression

            var model = _db.RiderModels
                                        .OrderBy(r => r.Name)
                                        .Where(r => searchTerm == null || r.Name.Contains(searchTerm))
                                        .ToPagedList(page, 5);   //default page
            //   .Take(10);                           



            if (Request.IsAjaxRequest())

            {
                return PartialView("_Riders", model);
            }

            return View(model);

        }

        public ActionResult ViewRiderEvents(int Id)
        {
                       
            var model = _db.ResultModels.Include(e => e.Event)
                                      .Include(e => e.Rider)
                                     //.Where(i => e.Id == id);
                                     .Where(i => i.RiderModelId == Id)
                                     .OrderByDescending(i=> i.Event.Date);

            return View(model);

            



        }

        //// GET: Rider/Details/5
        //public ActionResult Details(int Id)
        //{
        //    var model = _db.RiderModels.Include(i => i.ClubOrTeamId);
                                                                        

        //    return View(model);
        //}

        // GET: Rider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rider/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(RiderModel riderModel)
        {
            if (ModelState.IsValid)
            {
                _db.RiderModels.Add(riderModel);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();

        }

        // GET: Rider/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {

            var rider = _db.RiderModels.Single(r => r.Id == Id);

            return View(rider);
        }

        // POST: Rider/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id, FormCollection collection)
        {

            var rider = _db.RiderModels.Single(r => r.Id == Id);
            if (TryUpdateModel(rider))

            {
                //Write to DB
                return RedirectToAction("Index");

            }

            return View(rider);
        }



        // GET: Rider/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)

        {
            return View();
        }

        // POST: Rider/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }
}
