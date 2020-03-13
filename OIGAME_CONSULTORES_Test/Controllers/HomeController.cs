using OIGAME_CONSULTORES_Test.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIGAME_CONSULTORES_Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var strE = Encr_Decr.Encrypt("data source=DESARROLLOVILA;initial catalog=OIGAME_CONSULTORES_Test;user id=sa;password=TURjvb/2014;MultipleActiveResultSets=True;App=EntityFramework");

            var dec = Encr_Decr.Decrypt(strE);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}