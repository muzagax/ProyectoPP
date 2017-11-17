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
    public class historiasDeUsuariosController : Controller
    {
        private patopurificEntitiesGeneral db = new patopurificEntitiesGeneral();

        private ApplicationUserManager _userManager;
        private ProyectoPP.Models.patopurificEntitiesRoles enRoles = new ProyectoPP.Models.patopurificEntitiesRoles();

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

            var permisoID = enRoles.permisos.Where(m => m.permiso == permiso).First().id_permiso;
            var listaRoles = enRoles.AspNetRoles.Where(m => m.permisos.Any(c => c.id_permiso == permisoID)).ToList().Select(n => n.Id);

            bool userRol = false;
            foreach (var element in listaRoles)
            {
                if (element == rol)
                    userRol = true;
            }
            return userRol;
        }


        // GET: historiasDeUsuarios
        public ActionResult Index()
        {
            ModeloProductBacklog modelo = new ModeloProductBacklog();

            if (revisarPermisos("Ver proyecto").Result) // Si el usuario no es estudiante
            {

                // Seleccion para el dropdown de proyectos. Carga todos los proyectos que hay
                ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre");
            }

            else
            {
                var idproyecto = db.persona.Where(m => m.cedula == System.Web.HttpContext.Current.User.Identity.Name).First().IdProyecto;
                modelo.ListaPB = db.historiasDeUsuario.Where(m => m.proyectoId == idproyecto).ToList();

                // Seleccion para el dropdown de proyectos. Carga solo el proyecto donde participa el estudiante
                ViewBag.Proyecto = new SelectList(db.proyecto.Where(x => x.id == idproyecto), "id", "nombre");

            }
            return View(modelo);
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
        [HttpPost]
        public ActionResult Actualizar(ModeloProductBacklog modelo)
        {
            //ModeloProductBacklog modelo = new ModeloProductBacklog();

            ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre", modelo.ProyectoID);

            modelo.ListaPB = (from H in db.historiasDeUsuario where H.proyectoId == modelo.ProyectoID select H).ToList();

            return View("Index", modelo);
        }
    }
}
