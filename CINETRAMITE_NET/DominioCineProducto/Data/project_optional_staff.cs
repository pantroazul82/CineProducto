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
    
    public partial class project_optional_staff
    {
        public int project_optional_staff_id { get; set; }
        public int project_optional_staff_project_id { get; set; }
        public string project_optional_staff_firstname { get; set; }
        public string project_optional_staff_lastname { get; set; }
        public string project_optional_staff_identification_type { get; set; }
        public string project_optional_staff_identification_number { get; set; }
        public string project_optional_staff_city { get; set; }
        public string project_optional_staff_state { get; set; }
        public string project_optional_staff_address { get; set; }
        public string project_optional_staff_phone { get; set; }
        public string project_optional_staff_movil { get; set; }
        public string project_optional_staff_email { get; set; }
        public Nullable<int> project_optional_staff_position_id { get; set; }
    
        public virtual position position { get; set; }
        public virtual project project { get; set; }
    }
}
