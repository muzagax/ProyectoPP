
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
    
public partial class criteriosDeAceptacion
{

    public string idHU { get; set; }

    public int numCriterio { get; set; }

    public string nombre { get; set; }

    public string contexto { get; set; }

    public string evento { get; set; }

    public string resultado { get; set; }

    public Nullable<int> numeroEscenario { get; set; }



    public virtual historiasDeUsuario historiasDeUsuario { get; set; }

}

}
