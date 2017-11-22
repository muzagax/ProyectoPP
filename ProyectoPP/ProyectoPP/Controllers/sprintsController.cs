using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoPP.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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
            
            var sprint = db.sprint.Include(s => s.proyecto);
            return View(sprint.ToList());
            
        }

        public ActionResult SprintPlanning()
        {
            sprint sprint1 = new sprint();

            if (revisarPermisos("Ver proyecto").Result) // Si el usuario no es estudiante
            {

                // Seleccion para el dropdown de proyectos. Carga todos los proyectos que hay
                ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre");

                // El dropdown de sprints queda en blanco porque no sabemos aún cuál proyecto se va a seleccionar. Para esto solamente busco los sprints que id ""
                ViewBag.Sprint = new SelectList(db.proyecto.Where(x => x.id == ""), "id", "nombre");
                
                // Repito el proceso con las HU, busco las que tengan ID ""
                ViewBag.HU = db.historiasDeUsuario.Where(x => x.proyectoId == "").ToList();
            }

            else
            {
                var idproyecto = db.persona.Where(m => m.cedula == System.Web.HttpContext.Current.User.Identity.Name).First().IdProyecto;
                
                // Seleccion para el dropdown de proyectos. Carga solo el proyecto donde participa el estudiante
                ViewBag.Proyecto = new SelectList(db.proyecto.Where(x => x.id == idproyecto), "id", "nombre");

                // Seleccion para el dropdown de sprints. Carga todos los sprints que hay asociados al proyecto seleccionado
                ViewBag.Sprint = new SelectList(db.sprint.Where(x => x.proyectoId == idproyecto), "id", "nombre");

                ViewBag.HU = db.historiasDeUsuario.Where(x => x.proyectoId == idproyecto).ToList();

            }

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
