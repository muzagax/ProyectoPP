
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
    
public partial class historiasDeUsuario
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public historiasDeUsuario()
    {

        this.criteriosDeAceptacion = new HashSet<criteriosDeAceptacion>();

        this.tarea = new HashSet<tarea>();

    }


    public string id { get; set; }

    public string rol { get; set; }

    public string funcionalidad { get; set; }

    public string resultado { get; set; }

    public int prioridad { get; set; }

    public int estimacion { get; set; }

    public int numeroEscenario { get; set; }

    public string proyectoId { get; set; }

    public string sprintId { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<criteriosDeAceptacion> criteriosDeAceptacion { get; set; }

    public virtual proyecto proyecto { get; set; }

    public virtual sprint sprint { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tarea> tarea { get; set; }

}

}
