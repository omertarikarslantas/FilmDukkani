//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilmDukkaniProje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Film_Altyazi
    {
        public int FilmAltID { get; set; }
        public int AltyaziID { get; set; }
        public int FilmID { get; set; }
    
        public virtual AltYazi AltYazi { get; set; }
        public virtual Filmler Filmler { get; set; }
    }
}
