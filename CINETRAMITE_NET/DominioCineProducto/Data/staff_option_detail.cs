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
    
    public partial class staff_option_detail
    {
        public int staff_option_detail_id { get; set; }
        public int staff_option_id { get; set; }
        public int position_id { get; set; }
        public int staff_option_detail_quantity { get; set; }
        public int staff_option_detail_optional_quantity { get; set; }
        public byte staff_option_detail_deleted { get; set; }
        public Nullable<int> version { get; set; }
    
        public virtual position position { get; set; }
        public virtual staff_option staff_option { get; set; }
    }
}
