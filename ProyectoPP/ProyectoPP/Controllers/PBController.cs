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
    }
}