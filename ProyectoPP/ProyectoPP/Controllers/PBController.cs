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
        // GET: PB
        public ActionResult ProductBacklogIndex()
        {
            ModeloProductBacklog modelo = new ModeloProductBacklog();



            modelo.ListaPB = bd.historiasDeUsuario.ToList();

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

    }
}