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

            DatosSprintPlanning modelo = new DatosSprintPlanning();

            if (!System.Web.HttpContext.Current.User.IsInRole("1")) // Si el usuario no es estudiante
            {

                // Seleccion para el dropdown de proyectos. Carga todos los proyectos que hay
                ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre");

                // El dropdown de sprints queda en blanco porque no sabemos aún cuál proyecto se va a seleccionar. Para esto solamente busco los sprints que id ""
                ViewBag.Sprint = new SelectList(db.sprint.Where(x => x.proyectoId == ""), "id", "nombre");
                
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

                // Lss hitorias de usuario no se cargan ya que necesito seleccionar un sprint
                ViewBag.HU = db.historiasDeUsuario.Where(x => x.proyectoId == "").ToList();

            }

            return View(modelo);
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

            ViewBag.nombreProyecto = db.proyecto.Where( p => p.id == proyectoId).First().nombre;
            Sprint2 nuevoS = new Sprint2();
            nuevoS.proyectoId = proyectoId;
            return View(nuevoS);
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
            
           // ViewBag.proyectoId = new SelectList(db.proyecto, "id", "nombre", sprint.proyectoId);
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



        /** Metodo para actualizar la vista una vez seleccionado un un proyecto*/
        public ActionResult Actualizar(Sprint2 modelo)
        {
            // Si el usuario no es estudiante
            if (!System.Web.HttpContext.Current.User.IsInRole("1")) 
            {
                ViewBag.Sprints = db.sprint.Where(m => m.proyectoId == modelo.proyectoId).ToList();
                ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre", modelo.proyectoId);
            }
            // Si el usuario es estudiante
            else
            {
                ViewBag.Sprints = new SelectList(db.sprint.Where(x => x.id == modelo.id), "id", "fechaInicio", "fechaFinal", "proyectoId", modelo.id);
            }

            //modelo.ListaPB = (from H in db.historiasDeUsuario where H.proyectoId == modelo.ProyectoID select H).ToList();

            return View("Index", modelo);
        }

        public ActionResult ActualizarSprintPlanning(DatosSprintPlanning modelo)
        {

            if (!System.Web.HttpContext.Current.User.IsInRole("1")) // Si el usuario no es estudiante
            {

                // Seleccion para el dropdown de proyectos. Carga todos los proyectos que hay
                ViewBag.Proyecto = new SelectList(db.proyecto, "id", "nombre", modelo.ProyectoId);

                // El dropdown de sprints carga solamente los sprints asociados al proyecto seleccionado
                ViewBag.Sprint = new SelectList(db.sprint.Where(x => x.proyectoId == modelo.ProyectoId), "id", "id", modelo.SprintID);
                
                // El dropdonw de las hisotrias de usuario se hace con la combinación de ambas cosas
                ViewBag.HU = db.historiasDeUsuario.Where(x => x.proyectoId == modelo.ProyectoId && x.id == modelo.SprintID).ToList();

            }

            else
            {
                var idproyecto = db.persona.Where(m => m.cedula == System.Web.HttpContext.Current.User.Identity.Name).First().IdProyecto;

                // Seleccion para el dropdown de proyectos. Carga solo el proyecto donde participa el estudiante
                ViewBag.Proyecto = new SelectList(db.proyecto.Where(x => x.id == idproyecto), "id", "id", modelo.ProyectoId);

                // Seleccion para el dropdown de sprints. Carga todos los sprints que hay asociados al proyecto seleccionado
                ViewBag.Sprint = new SelectList(db.sprint.Where(x => x.proyectoId == idproyecto), "id", "id", modelo.SprintID);

                ViewBag.HU = db.historiasDeUsuario.Where(x => x.proyectoId == idproyecto && x.id == modelo.SprintID).ToList();

            }

            return View("SprintPlanning", modelo);
        }
    }
}
