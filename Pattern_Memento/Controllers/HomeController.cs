using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pattern_Memento.Models;

namespace Pattern_Memento.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Canvas canvas = new Canvas();

            return View(canvas);
        }
    }
}