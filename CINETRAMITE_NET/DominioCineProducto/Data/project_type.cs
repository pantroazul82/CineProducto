//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DominioCineProducto.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class project_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public project_type()
        {
            this.project = new HashSet<project>();
            this.staff_option = new HashSet<staff_option>();
        }
    
        public int project_type_id { get; set; }
        public string project_type_name { get; set; }
        public string project_type_description { get; set; }
        public byte project_type_deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project> project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<staff_option> staff_option { get; set; }
    }
}
