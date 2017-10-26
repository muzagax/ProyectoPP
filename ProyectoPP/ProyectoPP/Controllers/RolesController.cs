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

namespace ProyectoPP.Controllers
{
    public class RolesController : Controller
    {

        patopurificEntitiesRoles baseDatos = new patopurificEntitiesRoles();
        
        // GET: Roles

        private ProyectoPP.Models.patopurificEntitiesRoles enRoles = new ProyectoPP.Models.patopurificEntitiesRoles();

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

        public ActionResult RolesView()
        {
            Roles modelo = new Roles();
            //if (revisarPermisos("ver accesos").Result)
            //{
                modelo.ListaRoles = baseDatos.AspNetRoles.ToList();
                modelo.ListaPermisos = baseDatos.permisos.ToList();
                modelo.ListaAscociaciones = new List<Roles.Asociaciones>();
                modelo.ListaGuardar = new List<Roles.GuardarAux>();

                foreach (var AspNetRoles in modelo.ListaRoles)
                {

                    foreach (var permisos in AspNetRoles.permisos)
                    {
                        modelo.ListaAscociaciones.Add(new Roles.Asociaciones(AspNetRoles.Name, permisos.permiso));
                    }
                }

                foreach (var AspNetRoles in modelo.ListaRoles)
                {
                    foreach (var permisos in modelo.ListaPermisos)
                    {
                        bool asignado = false;
                        for (int contador = 0; contador < modelo.ListaAscociaciones.Count(); contador++)
                        {
                            if (AspNetRoles.Name == modelo.ListaAscociaciones.ElementAt(contador).rol)
                            {
                                if (modelo.ListaAscociaciones.ElementAt(contador).permiso == permisos.permiso)
                                {
                                    modelo.ListaGuardar.Add(new Roles.GuardarAux(AspNetRoles.Id, permisos.id_permiso, true));
                                    asignado = true;
                                }
                            }
                            if (contador == modelo.ListaAscociaciones.Count() - 1 && asignado == false)
                            {
                                modelo.ListaGuardar.Add(new Roles.GuardarAux(AspNetRoles.Id, permisos.id_permiso, false));
                                asignado = false;
                            }
                        }

                    }
                }
                return View(modelo);
            //}
            //else
            //{
                //return RedirectToAction("Index", "Home");
            //}
        }

        [HttpPost]
        public ActionResult Aceptar(Roles mod)
        {   
            Roles modelo = new Roles();
            modelo.ListaRoles = baseDatos.AspNetRoles.ToList();
            modelo.ListaPermisos = baseDatos.permisos.ToList();

            int numero = -1;
            foreach (var rol in modelo.ListaRoles)
            {
                numero++;
                rol.permisos.Clear();
                foreach (var asoc in mod.ListaGuardar)
                {
                    if (asoc.rol == rol.Id)
                    {
                        if (asoc.existe == true)
                        {
                            foreach (var permiso in modelo.ListaPermisos)
                            {
                                if (permiso.id_permiso == asoc.permiso)
                                {
                                    //rol.permisos.Add(permiso);
                                    modelo.ListaRoles.ElementAt(numero).permisos.Add(permiso);
                                }
                            }
                            
                        }
                    }
                }
            }

            int contador = -1;

            foreach (var permiso in modelo.ListaPermisos)
            {
                contador++;
                
                permiso.AspNetRoles.Clear();
                foreach (var asoc in mod.ListaGuardar)
                {
                    if (asoc.permiso == permiso.id_permiso)
                    {
                        if (asoc.existe == true)
                        {
                            foreach (var rol in modelo.ListaRoles)
                            {
                                if (rol.Id == asoc.rol)
                                {
                                    modelo.ListaPermisos.ElementAt(contador).AspNetRoles.Add(rol);
                                    //permiso.AspNetRoles.Add(rol);
                                }
                            }
                        }

                    }

                }
            }
            


            if (ModelState.IsValid)
            {
                
                foreach (var permiso in modelo.ListaPermisos)
                {
                    //var algo = permiso.AspNetRoles;
                    
                    baseDatos.Entry(permiso).State = EntityState.Modified;

                }
                foreach (var roles in modelo.ListaRoles)
                {
                    baseDatos.Entry(roles).State = EntityState.Modified;
                }
                //baseDatos.Entry(modelo.ListaAscociaciones).State = EntityState.Modified;
                baseDatos.SaveChanges(); 
                return RedirectToAction("RolesView");
            }
            return View(modelo);
        }
    }
}