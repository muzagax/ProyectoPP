﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoPP.Models
{
    public class Sprint2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sprint2()
        {
            this.historiasDeUsuario = new HashSet<historiasDeUsuario>();
        }

        [Display(Name = "Número Sprint/Id")]
        public string id { get; set; }

        [Display(Name = "Fecha inicial")]
        public System.DateTime fechaInicio { get; set; }

        [Display(Name = "Fecha final")]
        public Nullable<System.DateTime> fechaFinal { get; set; }
        public string proyectoId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<historiasDeUsuario> historiasDeUsuario { get; set; }
        public virtual proyecto proyecto { get; set; }
    }
}