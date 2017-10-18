
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

    public partial class persona
{

        [Display(Name = "Nombre")]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El nombre solo puede estar compuesto por letras")]
        public string nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El primer apellido solo puede estar compuesto por letras")]
        public string apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "El segundo apellido solo puede estar compuesto por letras")]
        public string apellido2 { get; set; }

        [RegularExpression(@"^[0-9]{9,9}$", ErrorMessage ="La c�dula debe contener 9 numeros")]
        [Display(Name = "C�dula")]
        public string cedula { get; set; }

        [RegularExpression(@"^[A-Z][0-9]{5,5}$", ErrorMessage = "La primera letra debe estar en may�scula y contener 5 digitos despues de esta")]
        [Display(Name = "Carn�")]
        public string carne { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\.\-]+@[a-zA-Z0-9\.\-]+\.[a-z]{1,3}$", ErrorMessage = "No es un formato de correo electronico v�lido")]
        [Display(Name = "Correo electr�nico")]
        public string email { get; set; }

    public string id { get; set; }

}

}
