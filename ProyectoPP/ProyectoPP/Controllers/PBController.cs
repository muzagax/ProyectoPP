using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPP.Models;
using System.Data.Entity;

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using System.Net;



namespace ProyectoPP.Controllers
{
    public class PBController : Controller
    {


        patopurificEntitiesGeneral bd = new patopurificEntitiesGeneral();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ProyectoPP.Models.patopurificEntitiesRoles enRoles = new ProyectoPP.Models.patopurificEntitiesRoles();

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        // GET: PB
        public ActionResult ProductBacklogIndex()
        {
            ModeloProductBacklog modelo = new ModeloProductBacklog();

            ViewBag.Proyecto = new SelectList(bd.proyecto, "id", "nombre");

            if (revisarPermisos("Ver proyecto").Result)
            {
                modelo.ListaPB = bd.historiasDeUsuario.ToList();
                modelo.ProyectoID = "0";
            }
            
            else
            {
                string tmp = System.Web.HttpContext.Current.User.Identity.Name;
                var idpersona = bd.persona.Where(m => m.cedula == System.Web.HttpContext.Current.User.Identity.Name).First().IdProyecto;
                modelo.ListaPB = bd.historiasDeUsuario.Where(m => m.proyectoId == idpersona).ToList();
            }
            return View(modelo);
        }
        public ActionResult DetallePB(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historiasDeUsuario hu = bd.historiasDeUsuario.Find(id);
            if (hu == null)
            {
                return HttpNotFound();
            }
            return View(hu);
        }
        
        public ActionResult Actualizar(string id)
        {
            ModeloProductBacklog modelo = new ModeloProductBacklog();

            ViewBag.Proyecto = new SelectList(bd.proyecto, "id", "nombre", id);

            modelo.ListaPB = (from H in bd.historiasDeUsuario where H.proyectoId == id select H).ToList();

            return View("ProductBacklogIndex", modelo);
        }
        
    }
}