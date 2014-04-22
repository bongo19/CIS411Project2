using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameVoteSolution.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "home", });
            //ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
            ViewBag.Message = "Home Page";

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