using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPP.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ProyectoPP.Controllers
{
    public class sprintsController : Controller
    {
        private patopurificEntitiesGeneral db = new patopurificEntitiesGeneral();



        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private async Task<bool> revisarPermisos(string permiso)
        {

            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = UserManager.FindByName(userName);
            var rol = user.Roles.SingleOrDefault().RoleId;

            var permisoID = db.permisos.Where(m => m.permiso == permiso).First().id_permiso;
            var listaRoles = db.AspNetRoles.Where(m => m.permisos.Any(c => c.id_permiso == permisoID)).ToList().Select(n => n.Id);

            bool userRol = false;
            foreach (var element in listaRoles)
            {
                if (element == rol)
                    userRol = true;
            }
            return userRol;
        }

        // GET: sprints
        public ActionResult Index()
        {

            Sprint2 modelo = new Sprint2();

            if ( !System.Web.HttpContext.Current.User.IsInRole("1") ) // Si el usuario no es estudiante
            {

                // Seleccion para el dropdown de proyectos. Carga todos los proyectos que hay
                ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre", "Seleccione un Proyecto");
            }

            else
            {
                var idproyecto = db.persona.Where(m => m.cedula == System.Web.HttpContext.Current.User.Identity.Name).First().IdProyecto;                

                // Seleccion para el dropdown de proyectos. Carga solo el proyecto donde participa el estudiante
                ViewBag.Proyecto = new SelectList(db.proyecto.Where(x => x.id == idproyecto), "id", "nombre");
                ViewBag.NombreProyecto = db.proyecto.Where(m => m.id == idproyecto).First().nombre;

            }
            return View(modelo);
        }

        public ActionResult SprintPlanning()
        {
            sprint sprint1 = new sprint();

            ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre");
            ViewBag.Sprint = new SelectList(db.sprint, "id", "nombre");
            ViewBag.HU = db.historiasDeUsuario.ToList();

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
        public ActionResult Create(string proyectoId)
        {

            ViewBag.proyectoId = proyectoId;
            return View();
        }

        // POST: sprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Sprint2 sprint)
        {
            if (ModelState.IsValid)
            {
                sprint nuevoSprint = new sprint();

                nuevoSprint.historiasDeUsuario = sprint.historiasDeUsuario;
                nuevoSprint.id = sprint.id;
                nuevoSprint.fechaInicio = sprint.fechaInicio;
                nuevoSprint.fechaFinal = sprint.fechaFinal;
                nuevoSprint.proyectoId = sprint.proyectoId;
                nuevoSprint.proyecto = sprint.proyecto;
            
                db.sprint.Add(nuevoSprint);
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

        /** Motodo para actualizar la vista una vez seleccionado un un proyecto*/
        public ActionResult Actualizar(Sprint2 modelo)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("1")) // Si el usuario no es estudiante
            {
                ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre", modelo.proyectoId);
            }
            else
            {
                ViewBag.Proyecto = new SelectList(db.proyecto.Where(x => x.id == modelo.proyectoId), "id", "nombre");
            }

            //modelo.ListaPB = (from H in db.historiasDeUsuario where H.proyectoId == modelo.ProyectoID select H).ToList();

            return View("Index", modelo);
        }
    }
}
