using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPP.Models;

using System.Threading.Tasks;


namespace ProyectoPP.Controllers
{
    public class RolesController : Controller
    {

        patopurificEntitiesRoles baseDatos = new patopurificEntitiesRoles();
        // GET: Roles
        public ActionResult RolesView()
        {

            Roles modelo = new Roles();
            modelo.ListaRoles = baseDatos.AspNetRoles.ToList();
            modelo.ListaPermisos = baseDatos.permisos.ToList();
            modelo.ListaAscociaciones = new List<Roles.Asociaciones>();

           

            foreach (var AspNetRoles in modelo.ListaRoles)
            {

                foreach (var permisos in AspNetRoles.permisos)
                {
                    modelo.ListaAscociaciones.Add(new Roles.Asociaciones(AspNetRoles.Name, permisos.permiso));
                }
            }

            return View(modelo);
        }

        [HttpPost, ActionName("Aceptar")]
        [ValidateAntiForgeryToken]
        public ActionResult  AceptarAux()
        {


            return RedirectToAction("RolesView");
        }
    }
}