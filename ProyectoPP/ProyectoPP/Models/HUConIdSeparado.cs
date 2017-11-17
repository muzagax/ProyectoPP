using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoPP.Models
{
    public class HUConIdSeparado
    {
        [Display(Name = "Tipo de requerimiento")]
        public string tipoDeRequerimiento { get; set; }

        [Display(Name = "Módulo")]
        public string modulo { get; set; }

        [Display(Name = "Número de sprint")]
        public string numSprint { get; set; }

        [Display(Name = "Rol")]
        public string rol { get; set; }

        [Display(Name = "Funcionalidad")]
        public string funcionalidad { get; set; }

        [Display(Name = "Resultado")]
        public string resultado { get; set; }

        [Display(Name = "Prioridad")]
        public int prioridad { get; set; }

        [Display(Name = "Estimación")]
        public int estimacion { get; set; }

        [Display(Name = "Nombre del Proyecto")]
        public string proyectoId { get; set; }

        [Display(Name = "Número de escenario")]
        public int NumeroEscenario { get; set; }



    }
}