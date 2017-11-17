using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Identificador del Proyecto")]
        public string ProyectoID { get; set; }

        public string IdSeleccionado { get; set; }
        public string IdSeleccionadoTexto { get; set; }
    }
}