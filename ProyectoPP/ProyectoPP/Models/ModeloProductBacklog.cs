using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPP.Models
{
    public class ModeloProductBacklog
    {
        public ModeloProductBacklog()
        {
            ListaPB = new List<historiasDeUsuario>();
        }
        public List<historiasDeUsuario> ListaPB{ get; set; }
    }
}