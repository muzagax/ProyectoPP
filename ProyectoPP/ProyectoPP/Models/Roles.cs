using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPP.Models
{
    public class Roles
    {

        public class Asociaciones
        {
            public Asociaciones(string rolParam, string permisoParam)
            {
                this.rol = rolParam;
                this.permiso = permisoParam;
            }

            public string rol { get; set; }
            string permiso  { get; set; }
        }
        public permisos ModeloPermisos { get; set; }
        public AspNetRoles ModeloNetRoles { get; set; }

        public List<permisos> ListaPermisos { get; set; }

        public List<AspNetRoles> ListaRoles { get; set; }

        public List<Asociaciones>  ListaAscociaciones { get; set; }
}
}