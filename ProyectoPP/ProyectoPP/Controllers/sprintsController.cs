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
    public class sprintsController : Controller
    {
        private patopurificEntitiesGeneral db = new patopurificEntitiesGeneral();

        // GET: sprints
        public ActionResult Index()
        {
            
            var sprint = db.sprint.Include(s => s.proyecto);
            return View(sprint.ToList());
            
        }

        public ActionResult SprintPlanning()
        {
            sprint sprint1 = new sprint();

            ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre");
            ViewBag.Sprint = new SelectList(db.sprint, "id", "nombre");

            return View(sprint1);
        }

        // GET: sprints/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sprint sprint = db.sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // GET: sprints/Create
        public ActionResult Create()
        {
            ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre");
            return View();
        }

        // POST: sprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fechaInicio,fechaFinal,proyectoId")] sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.sprint.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre", sprint.proyectoId);
            return View(sprint);
        }

        // GET: sprints/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sprint sprint = db.sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre", sprint.proyectoId);
            return View(sprint);
        }

        // POST: sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fechaInicio,fechaFinal,proyectoId")] sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre", sprint.proyectoId);
            return View(sprint);
        }

        // GET: sprints/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sprint sprint = db.sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // POST: sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            sprint sprint = db.sprint.Find(id);
            db.sprint.Remove(sprint);
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
