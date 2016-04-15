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
    public class CirclesController : Controller
    {
        private CanvasStateContext db = new CanvasStateContext();

        // GET: Circles
        public ActionResult Index()
        {
            return View(db.Figures.ToList());
        }

        // GET: Circles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Circle circle = db.Figures.Find(id) is Circle ? (Circle)db.Figures.Find(id) : null;
            if (circle == null)
            {
                return HttpNotFound();
            }
            return View(circle);
        }

        // GET: Circles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Circles/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FigureID,CanvasID,Color,X,Y,Radius")] Circle circle)
        {
            if (ModelState.IsValid)
            {
                db.Figures.Add(circle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(circle);
        }

        // GET: Circles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Circle circle = db.Figures.Find(id) is Circle ? (Circle)db.Figures.Find(id) : null;
            if (circle == null)
            {
                return HttpNotFound();
            }
            return View(circle);
        }

        // POST: Circles/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FigureID,CanvasID,Color,X,Y,Radius")] Circle circle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(circle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(circle);
        }

        // GET: Circles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Circle circle = db.Figures.Find(id) is Circle ? (Circle)db.Figures.Find(id) : null;
            if (circle == null)
            {
                return HttpNotFound();
            }
            return View(circle);
        }

        // POST: Circles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Circle circle = db.Figures.Find(id) is Circle ? (Circle)db.Figures.Find(id) : null;
            db.Figures.Remove(circle);
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
