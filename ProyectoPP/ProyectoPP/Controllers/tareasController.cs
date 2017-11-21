using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPP.Models;

namespace ProyectoPP.Controllers
{
    public class tareasController : Controller
    {
        private patopurificEntitiesGeneral db = new patopurificEntitiesGeneral();

        // GET: tareas
        public ActionResult Index()
        {
            var tarea = db.tarea.Include(t => t.historiasDeUsuario);
            return View(tarea.ToList());
        }

        // GET: tareas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = db.tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // GET: tareas/Create
        public ActionResult Create()
        {
            ViewBag.HU = new SelectList(db.historiasDeUsuario, "id", "rol");
            return View();
        }

        // POST: tareas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HU,id,nombre,esfuerzo")] tarea tarea)
        {
            if (ModelState.IsValid)
            {
                db.tarea.Add(tarea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HU = new SelectList(db.historiasDeUsuario, "id", "rol", tarea.HU);
            return View(tarea);
        }

        // GET: tareas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = db.tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            ViewBag.HU = new SelectList(db.historiasDeUsuario, "id", "rol", tarea.HU);
            return View(tarea);
        }

        // POST: tareas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HU,id,nombre,esfuerzo")] tarea tarea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HU = new SelectList(db.historiasDeUsuario, "id", "rol", tarea.HU);
            return View(tarea);
        }

        // GET: tareas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = db.tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // POST: tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tarea tarea = db.tarea.Find(id);
            db.tarea.Remove(tarea);
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
