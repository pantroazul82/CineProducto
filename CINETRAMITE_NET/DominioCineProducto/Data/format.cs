//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DominioCineProducto.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class format
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public format()
        {
            this.project_format = new HashSet<project_format>();
        }
    
        public int format_id { get; set; }
        public int format_type_id { get; set; }
        public string format_name { get; set; }
        public string format_description { get; set; }
        public byte format_deleted { get; set; }
        public Nullable<int> format_padre { get; set; }
    
        public virtual format_type format_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_format> project_format { get; set; }
    }
}
