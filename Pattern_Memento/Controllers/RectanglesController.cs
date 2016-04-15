using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pattern_Memento.Domain;

namespace Pattern_Memento.Controllers
{
    public class RectanglesController : Controller
    {
        private CanvasStateContext db = new CanvasStateContext();

        // GET: Rectangles
        public ActionResult Index()
        {
            return View(db.Figures.ToList());
        }

        // GET: Rectangles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rectangle rectangle = db.Figures.Find(id) is Rectangle ? (Rectangle)db.Figures.Find(id) : null;
            if (rectangle == null)
            {
                return HttpNotFound();
            }
            return View(rectangle);
        }

        // GET: Rectangles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rectangles/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FigureID,CanvasID,Color,X,Y,Width,Height")] Rectangle rectangle)
        {
            if (ModelState.IsValid)
            {
                db.Figures.Add(rectangle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rectangle);
        }

        // GET: Rectangles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rectangle rectangle = db.Figures.Find(id) is Rectangle ? (Rectangle)db.Figures.Find(id) : null;
            if (rectangle == null)
            {
                return HttpNotFound();
            }
            return View(rectangle);
        }

        // POST: Rectangles/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FigureID,CanvasID,Color,X,Y,Width,Height")] Rectangle rectangle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rectangle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rectangle);
        }

        // GET: Rectangles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rectangle rectangle = db.Figures.Find(id) is Rectangle ? (Rectangle)db.Figures.Find(id) : null;
            if (rectangle == null)
            {
                return HttpNotFound();
            }
            return View(rectangle);
        }

        // POST: Rectangles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rectangle rectangle = db.Figures.Find(id) is Rectangle ? (Rectangle)db.Figures.Find(id) : null;
            db.Figures.Remove(rectangle);
            db.SaveChanges();
            return RedirectToAction("Index");
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
