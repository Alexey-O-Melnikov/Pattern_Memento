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
    public class CanvasController : Controller
    {
        private CanvasStateContext db = new CanvasStateContext();

        // GET: Canvas
        public ActionResult Index()
        {
            return View(db.Canvases.ToList());
        }

        // GET: Canvas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canvas canvas = db.Canvases.Find(id);
            if (canvas == null)
            {
                return HttpNotFound();
            }
            return View(canvas);
        }

        // GET: Canvas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Canvas/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CanvasID")] Canvas canvas)
        {
            if (ModelState.IsValid)
            {
                db.Canvases.Add(canvas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(canvas);
        }

        // GET: Canvas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canvas canvas = db.Canvases.Find(id);
            if (canvas == null)
            {
                return HttpNotFound();
            }
            return View(canvas);
        }

        // POST: Canvas/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CanvasID")] Canvas canvas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(canvas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(canvas);
        }

        // GET: Canvas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canvas canvas = db.Canvases.Find(id);
            if (canvas == null)
            {
                return HttpNotFound();
            }
            return View(canvas);
        }

        // POST: Canvas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Canvas canvas = db.Canvases.Find(id);
            db.Canvases.Remove(canvas);
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
