using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPP.Models
{
    public class Roles
    {

        public permisos ModeloPermisos { get; set; }
        public AspNetRoles ModeloNetRoles { get; set; }

        public List<permisos> ListaPermisos { get; set; }

        public List<AspNetRoles> ListaRoles { get; set; }
    }
}