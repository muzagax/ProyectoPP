
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
    
public partial class proyecto
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public proyecto()
    {

        this.historiasDeUsuario = new HashSet<historiasDeUsuario>();

        this.sprint = new HashSet<sprint>();

        this.persona = new HashSet<persona>();

    }


    public string id { get; set; }

    public string nombre { get; set; }

    public string descripcion { get; set; }

    public System.DateTime fechaInicio { get; set; }

    public Nullable<System.DateTime> fechaFinal { get; set; }

    public string lider { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<historiasDeUsuario> historiasDeUsuario { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<sprint> sprint { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<persona> persona { get; set; }

    public virtual persona persona1 { get; set; }

}

}
