using RaceMateDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace RaceMateDB.Controllers
{
    public class HomeController : Controller
    {

       

        public ActionResult Index()
        {

          

            //var controller = RouteData.Values["controller"];
           // var action = RouteData.Values["action"];
           // var id = RouteData.Values["id"];
           // var message = String.Format("{0}::{1} {2}", controller, action, id);
            //ViewBag.Message = message;

            return View();
        }

        public ActionResult About()
        {
            

            var model = new AboutModel();
            model.Name = "Damien";
            model.Location = "London, UK";
            model.Description = "RaceMate is a non-profit and for-fun organisation...";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page!";

            var model = new ContactModel();

            model.Message = "Please contact us...";
            model.PhoneNumber = "07497625914";
            model.MarketingEMail = "marketing@racemate.co.uk";
            model.SupportEmail = "support@racemate.co.uk";
            model.AddressLine1 = "21 Catherine Howard Close";
            model.AddressLine2 = "Thetford";
            model.AddressLine3 = "Norfolk";
            model.AddressLine4 = "IP24 1TQ";

            return View(model);
        }

    
    }
}