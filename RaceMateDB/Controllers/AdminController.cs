using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaceMateDB.Models;

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
