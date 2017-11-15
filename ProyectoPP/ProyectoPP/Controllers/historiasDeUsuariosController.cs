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
    public class historiasDeUsuariosController : Controller
    {
        private patopurificEntitiesGeneral db = new patopurificEntitiesGeneral();

        // GET: historiasDeUsuarios
        public ActionResult Index()
        {
            var historiasDeUsuario = db.historiasDeUsuario.Include(h => h.proyecto).Include(h => h.sprint);
            return View(historiasDeUsuario.ToList());
        }

        // GET: historiasDeUsuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiasDeUsuario historiasDeUsuario = db.historiasDeUsuario.Find(id);
            if (historiasDeUsuario == null)
            {
                return HttpNotFound();
            }
            return View(historiasDeUsuario);
        }

        // GET: historiasDeUsuarios/Create
        public ActionResult Create(string nombreProyecto)
        {
            ViewBag.proyectoId = nombreProyecto;
            ViewBag.sprintId = new SelectList(db.sprint, "id", "proyectoId");
            return View();
        }

        // POST: historiasDeUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,rol,funcionalidad,resultado,prioridad,estimacion,numeroEscenario,proyectoId,sprintId")] historiasDeUsuario historiasDeUsuario)
        {
            if (ModelState.IsValid)
            {
                db.historiasDeUsuario.Add(historiasDeUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre", historiasDeUsuario.proyectoId);
            ViewBag.sprintId = new SelectList(db.sprint, "id", "proyectoId", historiasDeUsuario.sprintId);
            return View(historiasDeUsuario);
        }

        // GET: historiasDeUsuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiasDeUsuario historiasDeUsuario = db.historiasDeUsuario.Find(id);
            if (historiasDeUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre", historiasDeUsuario.proyectoId);
            ViewBag.sprintId = new SelectList(db.sprint, "id", "proyectoId", historiasDeUsuario.sprintId);
            return View(historiasDeUsuario);
        }

        // POST: historiasDeUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,rol,funcionalidad,resultado,prioridad,estimacion,numeroEscenario,proyectoId,sprintId")] historiasDeUsuario historiasDeUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historiasDeUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre", historiasDeUsuario.proyectoId);
            ViewBag.sprintId = new SelectList(db.sprint, "id", "proyectoId", historiasDeUsuario.sprintId);
            return View(historiasDeUsuario);
        }

        // GET: historiasDeUsuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiasDeUsuario historiasDeUsuario = db.historiasDeUsuario.Find(id);
            if (historiasDeUsuario == null)
            {
                return HttpNotFound();
            }
            return View(historiasDeUsuario);
        }

        // POST: historiasDeUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            historiasDeUsuario historiasDeUsuario = db.historiasDeUsuario.Find(id);
            db.historiasDeUsuario.Remove(historiasDeUsuario);
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
