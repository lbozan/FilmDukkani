using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmDukkaniMvc.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index(Exception exception)
        {
            //var a = exception;
            ViewBag.message = "Bir Hata Meydana Geldi."; //+a;
            return View();
        }
        public ViewResult Http404()
        {
            ViewBag.message = "404";
            return View();
        }

        public ViewResult Http403()
        {
            ViewBag.message = "403";
            return View();
        }
    }
}
