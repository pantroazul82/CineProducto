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
    
    public partial class project_staff
    {
        public int project_staff_id { get; set; }
        public int project_staff_project_id { get; set; }
        public string project_staff_firstname { get; set; }
        public string project_staff_lastname { get; set; }
        public string project_staff_identification_type { get; set; }
        public string project_staff_identification_number { get; set; }
        public string project_staff_city { get; set; }
        public string project_staff_state { get; set; }
        public string project_staff_address { get; set; }
        public string project_staff_phone { get; set; }
        public string project_staff_movil { get; set; }
        public string project_staff_email { get; set; }
        public string project_staff_identification_attachment { get; set; }
        public string project_staff_cv_attachment { get; set; }
        public string project_staff_contract_attachment { get; set; }
        public Nullable<int> project_staff_position_id { get; set; }
        public byte project_staff_identification_approved { get; set; }
        public byte project_staff_cv_approved { get; set; }
        public byte project_staff_contract_approved { get; set; }
        public string project_staff_firstname2 { get; set; }
        public string project_staff_lastname2 { get; set; }
        public string project_staff_genero { get; set; }
        public string project_staff_localization_id { get; set; }
        public Nullable<int> id_genero { get; set; }
        public Nullable<int> id_etnia { get; set; }
        public Nullable<int> identification_type_id { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public Nullable<int> id_grupo_poblacional { get; set; }
        public Nullable<int> id_especialidad_cargo { get; set; }
    
        public virtual localization localization { get; set; }
        public virtual position position { get; set; }
        public virtual project project { get; set; }
    }
}
