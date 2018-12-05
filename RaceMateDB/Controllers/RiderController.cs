using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Models;
using PagedList;
using System.Net;
using RaceMateDB.Repositories;
using RaceMateDB.ViewModels;

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
                                     .Where(i => i.RiderModelId == Id)
                                     .OrderByDescending(i=> i.Event.Date);

            return View(model);
                    
        }
            
        // GET: Rider/Create
        public ActionResult Create()
        {
            var repo = new RiderRepository();

            var riderEditViewModel = repo.EditRider();

            return View(riderEditViewModel);

        }
        
        // POST: Rider/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(RiderEditViewModel riderEditViewModel)
        {

            var model = new RiderModel()

            {
                Name = riderEditViewModel.RiderName,
                ClubOrTeamId = Convert.ToInt32(riderEditViewModel.SelectedClubOrTeam)
                                                            
            };

            if (ModelState.IsValid)
            {
                _db.RiderModels.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = model.Id});

            }

            return View(model);

        }

     
        // GET: Rider/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {

            var rider = _db.RiderModels.Single(r => r.Id == Id);                   
                     
            var repo = new RiderRepository();

            var riderEditViewModel = repo.EditRider();

            riderEditViewModel.RiderID = rider.Id;
            riderEditViewModel.RiderName = rider.Name;
            riderEditViewModel.SelectedClubOrTeam = rider.ClubOrTeam.Name;

            return View(riderEditViewModel);
                      
            
        }


        // POST: Rider/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(RiderEditViewModel riderEditViewModel)
        {
            if (riderEditViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new RiderModel()

            {
                Id = riderEditViewModel.RiderID,
                Name = riderEditViewModel.RiderName,
                ClubOrTeamId = Convert.ToInt32(riderEditViewModel.SelectedClubOrTeam)              
                               
            };

            if (ModelState.IsValid)
            {
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = model.Id });
            }
            return View(riderEditViewModel);
        }





        // GET: RiderModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderModel riderModel = _db.RiderModels.Find(id);
            if (riderModel == null)
            {
                return HttpNotFound();
            }
            return View(riderModel);
        }



        // GET: RiderModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderModel riderModel = _db.RiderModels.Find(id);
            if (riderModel == null)
            {
                return HttpNotFound();
            }
            return View(riderModel);
        }

        // POST: RiderModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiderModel riderModel = _db.RiderModels.Find(id);
            _db.RiderModels.Remove(riderModel);
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
