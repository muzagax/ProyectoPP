using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPP.Models;
using System.Data.Entity;

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
            modelo.ListaGuardar = new List<Roles.GuardarAux>();

            foreach (var AspNetRoles in modelo.ListaRoles)
            {

                foreach (var permisos in AspNetRoles.permisos)
                {
                    modelo.ListaAscociaciones.Add(new Roles.Asociaciones(AspNetRoles.Name, permisos.permiso));
                }
            }

            foreach(var AspNetRoles in modelo.ListaRoles)
            {
                foreach (var permisos in modelo.ListaPermisos)
                {
                    bool asignado = false;
                    for(int contador = 0; contador < modelo.ListaAscociaciones.Count();contador++)
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
                //baseDatos.SaveChanges(); 
                return RedirectToAction("RolesView");
            }
            return View(modelo);
        }
    }
}