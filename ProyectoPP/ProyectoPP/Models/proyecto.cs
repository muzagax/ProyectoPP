//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoPP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public partial class proyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proyecto()
        {
            this.historiasDeUsuario = new HashSet<historiasDeUsuario>();
            this.sprint = new HashSet<sprint>();
            this.persona = new HashSet<persona>();
        }

        [Display(Name = "Identificador del Proyecto")]
        public string id { get; set; }

        [Display(Name = "Nombre del Proyecto")]
        public string nombre { get; set; }

        [Display(Name = "Descripci�n del Proyecto")]
        public string descripcion { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaInicio { get; set; }

        [Display(Name = "Fecha de T�rmino")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fechaFinal { get; set; }

        [Display(Name = "L�der del equipo")]
        public string lider { get; set; }

        [Display(Name = "Estado del Proyecto")]
        public string estado { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<historiasDeUsuario> historiasDeUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sprint> sprint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<persona> persona { get; set; }
        public virtual persona persona1 { get; set; }
    }
}
