//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoPP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sprint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sprint()
        {
            this.historiasDeUsuario = new HashSet<historiasDeUsuario>();
        }
    
        public string id { get; set; }
        public System.DateTime fechaInicio { get; set; }
        public Nullable<System.DateTime> fechaFinal { get; set; }
        public string proyectoId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<historiasDeUsuario> historiasDeUsuario { get; set; }
        public virtual proyecto proyecto { get; set; }
    }
}
