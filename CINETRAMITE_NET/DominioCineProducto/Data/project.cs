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
    
    public partial class project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public project()
        {
            this.adjunto_projecto = new HashSet<adjunto_projecto>();
            this.estimulo = new HashSet<estimulo>();
            this.project_attachment = new HashSet<project_attachment>();
            this.project_format = new HashSet<project_format>();
            this.project_optional_staff = new HashSet<project_optional_staff>();
            this.project_producer = new HashSet<project_producer>();
            this.project_responsable = new HashSet<project_responsable>();
            this.project_staff = new HashSet<project_staff>();
            this.project_status = new HashSet<project_status>();
            this.resolution = new HashSet<resolution>();
        }
    
        public int project_id { get; set; }
        public Nullable<int> production_type_id { get; set; }
        public string project_name { get; set; }
        public Nullable<int> project_type_id { get; set; }
        public Nullable<int> project_genre_id { get; set; }
        public string project_synopsis { get; set; }
        public Nullable<int> project_duration { get; set; }
        public Nullable<long> project_total_cost_desarrollo { get; set; }
        public Nullable<long> project_total_cost_preproduccion { get; set; }
        public Nullable<long> project_total_cost_produccion { get; set; }
        public Nullable<long> project_total_cost_posproduccion { get; set; }
        public Nullable<long> project_total_cost_promotion { get; set; }
        public string project_recording_sites { get; set; }
        public Nullable<int> project_has_domestic_director { get; set; }
        public Nullable<int> project_staff_option_id { get; set; }
        public Nullable<int> project_domestic_producer_qty { get; set; }
        public Nullable<int> project_foreign_producer_qty { get; set; }
        public Nullable<System.DateTime> project_filming_start_date { get; set; }
        public Nullable<System.DateTime> project_filming_end_date { get; set; }
        public string project_filming_date_obs { get; set; }
        public string project_development_lab_info { get; set; }
        public string project_preprint_store_info { get; set; }
        public Nullable<byte> project_legal_deposit { get; set; }
        public string project_clarification_request_additional_text { get; set; }
        public Nullable<System.DateTime> project_schedule_film_view { get; set; }
        public string project_result_film_view { get; set; }
        public Nullable<int> project_idusuario { get; set; }
        public Nullable<System.DateTime> project_request_date { get; set; }
        public Nullable<System.DateTime> project_clarification_request_date { get; set; }
        public Nullable<System.DateTime> project_clarification_response_date { get; set; }
        public Nullable<System.DateTime> project_resolution_date { get; set; }
        public Nullable<System.DateTime> project_notification_date { get; set; }
        public Nullable<int> state_id { get; set; }
        public Nullable<decimal> project_percentage { get; set; }
        public Nullable<int> project_personal_type { get; set; }
        public Nullable<bool> formulario_aprobado_pronda { get; set; }
        public Nullable<bool> formulario_aprobado_sronda { get; set; }
        public Nullable<bool> aprueba_visualizacion_proyecto_pronda { get; set; }
        public string titulo_provisional { get; set; }
        public string observaciones_visualizacion_por_productor { get; set; }
        public Nullable<bool> aprueba_visualizacion_proyecto_sronda { get; set; }
        public Nullable<int> cod_idioma { get; set; }
        public string complemento_carta_aclaraciones { get; set; }
        public Nullable<System.DateTime> project_resolution2_date { get; set; }
        public Nullable<System.DateTime> project_notification2_date { get; set; }
        public Nullable<int> cod_firma_tramite { get; set; }
        public string texto_adicional_carta_negacion { get; set; }
        public Nullable<bool> tiene_premio { get; set; }
        public string premio { get; set; }
        public Nullable<System.DateTime> fecha_revisor_editor { get; set; }
        public Nullable<System.DateTime> fecha_editor_director { get; set; }
        public Nullable<System.DateTime> fecha_revisor_editor2 { get; set; }
        public Nullable<System.DateTime> fecha_editor_director2 { get; set; }
        public Nullable<System.DateTime> fecha_cancelacion { get; set; }
        public string sustituto_carta_aclaracion { get; set; }
        public string obs_adicional_obra { get; set; }
        public string obs_adicional_productor { get; set; }
        public string obs_adicional_otros_prd { get; set; }
        public string obs_adicional_personal { get; set; }
        public string obs_adicional_finalizacion { get; set; }
        public string carta_aclaraciones_generada { get; set; }
        public Nullable<int> responsable { get; set; }
        public Nullable<System.DateTime> fecha_limite { get; set; }
        public Nullable<int> version { get; set; }
        public string codigo_validacion { get; set; }
        public Nullable<int> numero_certificado { get; set; }
        public Nullable<System.DateTime> fecha_aprobacion { get; set; }
        public Nullable<int> IMPORTADO_SIREC { get; set; }
        public string pagina_web { get; set; }
        public string pagina_facebook { get; set; }
        public string inf_visualizacion { get; set; }
        public string tiene_reconocimiento { get; set; }
        public Nullable<int> ano_resolucion { get; set; }
        public string num_resolucion { get; set; }
        public string tiene_estimulos { get; set; }
        public string fdc { get; set; }
        public string fdc_especificacion { get; set; }
        public string ibermedia { get; set; }
        public string ibermedia_especificacion { get; set; }
        public string otros_estimulos { get; set; }
        public Nullable<System.DateTime> fecha_notificacion_certificado { get; set; }
        public string razones_rechazo { get; set; }
        public string municipio_lab { get; set; }
        public string nombre_lab { get; set; }
        public string no_acta_deposito { get; set; }
        public string otros_idiomas { get; set; }
        public string exhibida_publicamente { get; set; }
        public string ruta_certificado { get; set; }
        public Nullable<int> notificaciones_desistidos_enviadas { get; set; }
        public Nullable<System.DateTime> fecha_final { get; set; }
        public Nullable<System.DateTime> FECHA_SUBSANACION { get; set; }
        public Nullable<int> DIAS_SUBSANACION { get; set; }
        public Nullable<bool> SUBSANADO { get; set; }
        public string OBSERVACIONES_SUBSANACION { get; set; }
        public Nullable<System.DateTime> FECHA_ENVIO_SUBSANACION { get; set; }
        public Nullable<bool> SUBSANACION_ENVIADA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<adjunto_projecto> adjunto_projecto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<estimulo> estimulo { get; set; }
        public virtual firma_tramite firma_tramite { get; set; }
        public virtual idioma idioma { get; set; }
        public virtual production_type production_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_attachment> project_attachment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_format> project_format { get; set; }
        public virtual project_genre project_genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_optional_staff> project_optional_staff { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_producer> project_producer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_responsable> project_responsable { get; set; }
        public virtual staff_option staff_option { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_staff> project_staff { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_status> project_status { get; set; }
        public virtual project_type project_type { get; set; }
        public virtual usuario usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<resolution> resolution { get; set; }
    }
}
