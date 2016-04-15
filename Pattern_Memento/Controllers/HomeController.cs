using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Pattern_Memento.Models;
using Pattern_Memento.Domain;
using System.Diagnostics;

namespace Pattern_Memento.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                CanvasStateContext context = new CanvasStateContext();
                //context.Database.Initialize(false);
                context.Canvases.Add(new Canvas
                {
                    Figures = new List<Figure>
                    {
                        new Rectangle
                        {
                            Color = "#123123",
                            X = 777,
                            Y = 400,
                            Width = 30,
                            Height = 40
                        }
                    }
                });

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
            }
            return View();
        }
    }
}