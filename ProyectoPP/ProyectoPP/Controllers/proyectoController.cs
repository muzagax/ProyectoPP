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
    public class proyectoController : Controller
    {
        private patopurificEntitiesGeneral db = new patopurificEntitiesGeneral();

        // GET: proyecto
        public ActionResult Index()
        {
            var proyecto = db.proyecto.Include(p => p.persona);
            return View(proyecto.ToList());
        }

        // GET: proyecto/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proyecto proyecto = db.proyecto.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // GET: proyecto/Create
        public ActionResult Create()
        {
            ViewBag.lider = new SelectList(db.persona, "cedula", "nombre");
            return View();
        }

        // POST: proyecto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,fechaInicio,fechaFinal,lider")] proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.proyecto.Add(proyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lider = new SelectList(db.persona, "cedula", "nombre", proyecto.lider);
            return View(proyecto);
        }

        // GET: proyecto/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proyecto proyecto = db.proyecto.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.lider = new SelectList(db.persona, "cedula", "nombre", proyecto.lider);
            return View(proyecto);
        }

        // POST: proyecto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,fechaInicio,fechaFinal,lider")] proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.lider = new SelectList(db.persona, "cedula", "nombre", proyecto.lider);
            return View(proyecto);
        }

        // GET: proyecto/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proyecto proyecto = db.proyecto.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // POST: proyecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            proyecto proyecto = db.proyecto.Find(id);
            db.proyecto.Remove(proyecto);
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
