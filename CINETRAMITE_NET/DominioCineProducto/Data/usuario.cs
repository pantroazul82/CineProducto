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
    
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.project = new HashSet<project>();
            this.role_assignment = new HashSet<role_assignment>();
        }
    
        public int idusuario { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<int> idzona { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public string cargo { get; set; }
        public string validacion { get; set; }
        public Nullable<int> cod_ciudad { get; set; }
        public string empresa { get; set; }
        public string tipoidentificacion { get; set; }
        public string identificacion { get; set; }
        public string profesion { get; set; }
        public string hash { get; set; }
        public string activo { get; set; }
        public string personalizacion { get; set; }
        public string sitio_registro { get; set; }
        public string eliminado { get; set; }
        public string hash_restauracion { get; set; }
        public Nullable<System.DateTime> timestamp_restauracion { get; set; }
        public string color_pagina { get; set; }
        public string telefono_2 { get; set; }
        public string telefono_3 { get; set; }
        public string sms_autoriza { get; set; }
        public string sms_validado { get; set; }
        public string sms_numero_pin { get; set; }
        public string telefono_movil { get; set; }
        public Nullable<bool> es_responsable { get; set; }
        public Nullable<System.DateTime> FECHA_ASIGNACION { get; set; }
        public Nullable<bool> ES_ASIGNACION_AUTOMATICA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project> project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<role_assignment> role_assignment { get; set; }
    }
}
