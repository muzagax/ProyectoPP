using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using ProyectoPP.Models;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Contexts;

namespace ProyectoPP.Controllers
{
    public class personasController : Controller
    {
        private patopurificEntities db = new patopurificEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

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


        // GET: personas
        public ActionResult Index()
        {
            return View(db.persona.ToList());
        }

        // GET: personas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona personaTmp = db.persona.Find(id);

            PersonaConRol persona = new PersonaConRol();
            persona.apellido1 = personaTmp.apellido1;
            persona.apellido2 = personaTmp.apellido2;
            persona.nombre = personaTmp.nombre;
            persona.cedula = personaTmp.cedula;
            persona.carne = personaTmp.carne;
            persona.email = personaTmp.email;

            //Ahora obtenemos el ID de ASPNET para obtener el rol
            var user = UserManager.FindByName(persona.cedula);
            string ID = user.Id;

            var aspUser = UserManager.FindById(ID);
            var rol = aspUser.Roles.SingleOrDefault().RoleId;

            switch (rol)
            {
                case "1":
                    persona.rol ="Estudiante";
                break;

                case "2":
                    persona.rol ="Administrador";
                break;

                case "3":
                    persona.rol = "Administrador";
                break;

            }

            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonaConRol persona)
        {
            // se crea un aplication user para el aspnetuser
            var user = new ApplicationUser { UserName = persona.cedula, Email = persona.email };
            

            //puesto que PersonaCrear posee más datos que los que necesita la base de datos se crea userentry
            var userEntry = new persona(); 
            userEntry.apellido1 = persona.apellido1;
            userEntry.apellido2 = persona.apellido2;
            userEntry.nombre = persona.nombre;
            userEntry.cedula = persona.cedula;
            userEntry.carne = persona.carne;
            userEntry.email = persona.email;
            

            //genera el password generico
            string pass = "Ucr."+ persona.cedula;
            //metodo para crear el usuario con su contraseña de aspnetuser
            var result = await UserManager.CreateAsync(user, pass);

            if (ModelState.IsValid)
            {
                if (result.Succeeded)
                {
                    string ID = user.Id.ToString();
                    userEntry.id = ID;
                    db.persona.Add(userEntry);
                    db.SaveChanges();

                    var resultado = await this.UserManager.AddToRoleAsync(ID, persona.rol);


                    return RedirectToAction("Index");
                }
            }

            return View(persona);
        }

        // GET: personas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            persona personaTmp = db.persona.Find(id);

            PersonaConRol persona = new PersonaConRol();
            persona.apellido1 = personaTmp.apellido1;
            persona.apellido2 = personaTmp.apellido2;
            persona.nombre = personaTmp.nombre;
            persona.cedula = personaTmp.cedula;
            persona.carne = personaTmp.carne;
            persona.email = personaTmp.email;

            //Ahora obtenemos el ID de ASPNET para obtener el rol
            var user = UserManager.FindByName(persona.cedula);
            string ID = user.Id;

            var aspUser = UserManager.FindById(ID);
            var rol = aspUser.Roles.SingleOrDefault().RoleId;
            
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombre,apellido1,apellido2,cedula,carne,email")] persona persona)
        {
            
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(persona.cedula);
                string ID = user.Id;
                persona.id = ID;
                db.Entry(persona).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // GET: personas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona persona = db.persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            persona persona = db.persona.Find(id);
            var user = UserManager.FindById(db.persona.Find(id).id);
            UserManager.Delete(user);
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
