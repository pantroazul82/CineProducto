using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Net.Mail;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using CineProducto.Bussines;
using System.Text.RegularExpressions;
using System.Net.Mime;
using System.Text;
using DominioCineProducto.utils;
using DominioCineProducto;
using DevExpress.XtraPrinting.Native;

namespace CineProducto.Bussines
{
    /* Clase que modela los proyectos con todos sus atributos, donde cada proyecto es a la vez una solicitud */
    public class Project
    {
        public int project_id;
        public string project_name;
        public string project_synopsis;
        public int project_duration;
        public long project_total_cost_desarrollo;
        public long project_total_cost_preproduccion;
        public long project_total_cost_produccion;
        public long project_total_cost_posproduccion;
        public long project_total_cost_promotion;
        public int project_domestic_producer_qty;
        public int project_foreign_producer_qty;
        public string project_recording_sites;
        public int project_has_domestic_director;
        public int project_staff_option_id;
        public DateTime project_filming_start_date;
        public DateTime project_filming_end_date;
        public string project_filming_date_obs;
        public string project_development_lab_info;
        public string municipio_lab;
        public string nombre_lab;
        public string municipio_lab_otro;
        public string no_acta_deposito;
        public string otros_idiomas;

        public string project_preprint_store_info;
        public int project_legal_deposit;
        public string project_clarification_request_additional_text;
        public DateTime project_schedule_film_view; //Programación de la reunión para ver el proyecto cinematográfico
        public string project_result_film_view; //Resultado de la reunión de visualización del proyecto cinematográfico
        public int project_idusuario;
        public DateTime project_request_date;
        public DateTime project_clarification_request_date;
        public DateTime project_clarification_response_date;
        public DateTime project_resolution_date;
        public DateTime project_resolution2_date;
        public DateTime project_notification_date;
        public DateTime project_notification2_date;
        public int production_type_id;
        public string production_type_name;
        public int project_type_id;
        public string project_type_name;
        public int project_genre_id;
        public string project_genre_name;
        public string section_validation_result;
        public bool complete_form;
        public int state_id;
        public decimal project_percentage;
        public int project_personal_type;
        public string state_name;
        public bool? formulario_aprobado_pronda;
        public bool? formulario_aprobado_sronda;
        public bool? aprueba_visualizacion_proyecto_pronda;
        public bool? aprueba_visualizacion_proyecto_sronda;
        public string titulo_provisional;
        public string pagina_web;
        public string pagina_facebook;
        public string tiene_reconocimiento;
        public string exhibida_publicamente;        
        public string ano_resolucion;
        public string num_resolucion;
        public string tiene_estimulos;
        public string fdc;
        public string fdc_especificacion;
        public string ibermedia;
        public string ibermedia_especificacion;
        public string otros_estimulos;
        public string inf_visualizacion;
        public DateTime? fecha_notificacion_certificado;
        public string observaciones_visualizacion_por_productor;
        public int? cod_idioma;
        public int? responsable;
        public string complemento_carta_aclaraciones;
        public int? cod_firma_tramite;
        public int version;

        public string obs_adicional_obra;
        public string obs_adicional_productor;
        public string obs_adicional_otros_prd;
        public string obs_adicional_personal;
        public string obs_adicional_finalizacion;

        public bool? tiene_premio;
        public string premio;
        public DateTime? fecha_revisor_editor;
        public DateTime? fecha_editor_director;
        public DateTime? fecha_revisor_editor2;
        public DateTime? fecha_editor_director2;
        public DateTime? fecha_cancelacion;
        public string sustituto_carta_aclaracion;
        public string carta_aclaraciones_generada;


        public List<Format> project_format;
        public List<Producer> producer;
        public List<ProjectAttachment> attachment;
        public List<Staff> staff;
        
        /* Crea objetos para el personal del formato de personal */
        public StaffFormat StaffFormatDirector;
        public StaffFormat StaffFormatGuionista;
        public StaffFormat StaffFormatDirectorFotografía;
        public StaffFormat StaffFormatDirectorArte;
        public StaffFormat StaffFormatAutorMusica;
        public StaffFormat StaffFormatEditorMontajista;
        public StaffFormat StaffFormatCamarografo;
        public StaffFormat StaffFormatMaquillador;
        public StaffFormat StaffFormatVestuarista;
        public StaffFormat StaffFormatAmbientador;
        public StaffFormat StaffFormatEncargadoCasting;
        public StaffFormat StaffFormaScript;
        public StaffFormat StaffFormatSonidista;
        public StaffFormat StaffFormatMezclador;

        const int numeroCargos=13;
        public StaffFormat[] StaffFormatArreglo=new StaffFormat[numeroCargos];
        /* Crea objetos para las secciones */
        public Section sectionDatosProyecto;
        public Section sectionDatosProductor;
        public Section sectionDatosProductoresAdicionales;
        public Section sectionDatosPersonal;
        public Section sectionDatosAdjuntos;
        public Section sectionDatosFormatoPersonal;
        public Section sectionDatosFinalizacion;

        /* Constructor de la clase Project */
        public Project(int project_id = 0)
        {
            if(project_id != 0)
            {
                LoadProject(project_id); 
            }
        }

        public DataTable obtenerIdiomas()
        {
          DB db = new DB();
          DataSet ds = db.Select("SELECT cod_idioma, nombre_idioma from dboPrd.idioma ");
          return ds.Tables[0];
        }

        /* Método que carga la información de un proyecto en el objeto donde se indica
         * si el la carga se hace completa o si se hace una carga se información básica
         * unicamente, lo cual se utiliza para evitar ciclos infinitos cuando se usa
         * recursivamente la clase o cuando se desea usar solamente la información básica
         * del proyecto y así se evita procesamiento adicional.
         * @param int project_id identificador del proyecto que se desea cargar en el objeto
         * @param bool basic_load parametro que indica si se debe realizar una carga básica del proyecto
         * este valor se encuentra por defecto definido en false, cuando se activa se carga
         * el proyecto sin la información adicional (adjuntos y staff)
         */
        public void LoadProject(int project_id, bool load_basic = false)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_id, project_name, "
                                 + "production_type_id, "
                                 + "project_domestic_producer_qty, project_foreign_producer_qty, "
                                 + "project_total_cost_desarrollo, project_total_cost_preproduccion, "
                                 + "project_total_cost_produccion, project_total_cost_posproduccion, project_total_cost_promotion, "
                                 + "project_type_id, project_genre_id, "
                                 + "project_synopsis, project_recording_sites, "
                                 + "project_has_domestic_director, project_staff_option_id, project_duration, project_filming_start_date, project_filming_end_date, "
                                 + "project_filming_date_obs, project_idusuario, project_development_lab_info, municipio_lab, municipio_lab_otro, nombre_lab,exhibida_publicamente, no_acta_deposito,otros_idiomas,project_preprint_store_info, "
                                 + "project_legal_deposit, project_clarification_request_additional_text, project_schedule_film_view, project_result_film_view,"
                                 + "project_request_date, project_clarification_request_date, "
                                 + "project_clarification_response_date, project_resolution_date, project_notification_date,project_resolution2_date, project_notification2_date, "
                                 + "state_id,"
                                 + "project_percentage ,"
                                 + "project_personal_type,formulario_aprobado_pronda,formulario_aprobado_sronda,aprueba_visualizacion_proyecto_pronda,aprueba_visualizacion_proyecto_sronda,titulo_provisional, observaciones_visualizacion_por_productor, "
                                 + "cod_idioma,responsable ,complemento_carta_aclaraciones,cod_firma_tramite"
                                 + ",tiene_premio,premio,fecha_revisor_editor,fecha_editor_director,fecha_revisor_editor2,fecha_editor_director2,fecha_cancelacion,sustituto_carta_aclaracion "
                                 + ",obs_adicional_obra,obs_adicional_productor,obs_adicional_otros_prd,obs_adicional_personal,obs_adicional_finalizacion,carta_aclaraciones_generada,version "
                                 + ",pagina_web,pagina_facebook, tiene_reconocimiento,ano_resolucion,num_resolucion,tiene_estimulos,fdc,fdc_especificacion,ibermedia,otros_estimulos,ibermedia_especificacion,"
                                 + "inf_visualizacion,fecha_notificacion_certificado "
                                 + " FROM dboPrd.project "
                                 + "WHERE project_id=" + project_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                /* Se consulta el nombre del tipo de producción, tipo de proyecto y genero */
                DataSet ProductionTypeDS = db.Select("SELECT production_type_name FROM dboPrd.production_type WHERE production_type_id = '" + ds.Tables[0].Rows[0]["production_type_id"].ToString() + "'");
                this.production_type_name = ProductionTypeDS.Tables[0].Rows.Count == 1 ? ProductionTypeDS.Tables[0].Rows[0]["production_type_name"].ToString() : "";
                
                DataSet ProjectTypeDS = db.Select("SELECT project_type_name FROM dboPrd.project_type WHERE project_type_id = '" + ds.Tables[0].Rows[0]["project_type_id"].ToString() + "'");
                this.project_type_name = ProjectTypeDS.Tables[0].Rows.Count == 1 ? ProjectTypeDS.Tables[0].Rows[0]["project_type_name"].ToString() : "";
                
                DataSet ProjectGenreDS = db.Select("SELECT project_genre_name FROM dboPrd.project_genre WHERE project_genre_id = '" + ds.Tables[0].Rows[0]["project_genre_id"].ToString() + "'");
                this.project_genre_name = ProjectGenreDS.Tables[0].Rows.Count == 1 ? ProjectGenreDS.Tables[0].Rows[0]["project_genre_name"].ToString() : "";

                DataSet ProjectStateDS = db.Select("SELECT state_name FROM dboPrd.state WHERE state_id = '" + ds.Tables[0].Rows[0]["state_id"].ToString() + "'");
                this.state_name = ProjectStateDS.Tables[0].Rows.Count == 1 ? ProjectStateDS.Tables[0].Rows[0]["state_name"].ToString() : "";

                this.project_id = (int)ds.Tables[0].Rows[0]["project_id"];
                this.project_name = ds.Tables[0].Rows[0]["project_name"].ToString() != "" ? ds.Tables[0].Rows[0]["project_name"].ToString() : "";

                this.titulo_provisional = (ds.Tables[0].Rows[0]["titulo_provisional"] != null && ds.Tables[0].Rows[0]["titulo_provisional"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["titulo_provisional"].ToString() : "";

                this.pagina_facebook = (ds.Tables[0].Rows[0]["pagina_facebook"] != null && ds.Tables[0].Rows[0]["pagina_facebook"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["pagina_facebook"].ToString() : "";

                this.tiene_reconocimiento = (ds.Tables[0].Rows[0]["tiene_reconocimiento"] != null && ds.Tables[0].Rows[0]["tiene_reconocimiento"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["tiene_reconocimiento"].ToString() : "";
                this.ano_resolucion = (ds.Tables[0].Rows[0]["ano_resolucion"] != null && ds.Tables[0].Rows[0]["ano_resolucion"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["ano_resolucion"].ToString() : "";
                this.num_resolucion = (ds.Tables[0].Rows[0]["num_resolucion"] != null && ds.Tables[0].Rows[0]["num_resolucion"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["num_resolucion"].ToString() : "";
                this.tiene_estimulos = (ds.Tables[0].Rows[0]["tiene_estimulos"] != null && ds.Tables[0].Rows[0]["tiene_estimulos"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["tiene_estimulos"].ToString() : "";
                this.fdc = (ds.Tables[0].Rows[0]["fdc"] != null && ds.Tables[0].Rows[0]["fdc"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["fdc"].ToString() : "";
                this.fdc_especificacion = (ds.Tables[0].Rows[0]["fdc_especificacion"] != null && ds.Tables[0].Rows[0]["fdc_especificacion"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["fdc_especificacion"].ToString() : "";
                this.ibermedia = (ds.Tables[0].Rows[0]["ibermedia"] != null && ds.Tables[0].Rows[0]["ibermedia"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["ibermedia"].ToString() : "";
                this.otros_estimulos = (ds.Tables[0].Rows[0]["otros_estimulos"] != null && ds.Tables[0].Rows[0]["otros_estimulos"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["otros_estimulos"].ToString() : "";
                this.inf_visualizacion = (ds.Tables[0].Rows[0]["inf_visualizacion"] != null && ds.Tables[0].Rows[0]["inf_visualizacion"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["inf_visualizacion"].ToString() : "";
                this.ibermedia_especificacion = (ds.Tables[0].Rows[0]["ibermedia_especificacion"] != null && ds.Tables[0].Rows[0]["ibermedia_especificacion"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["ibermedia_especificacion"].ToString() : "";
                if (ds.Tables[0].Rows[0]["fecha_notificacion_certificado"].ToString() != "")
                {
                    this.project_filming_start_date = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_notificacion_certificado"].ToString());
                }

                this.pagina_web = (ds.Tables[0].Rows[0]["pagina_web"] != null && ds.Tables[0].Rows[0]["pagina_web"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["pagina_web"].ToString() : "";
                this.observaciones_visualizacion_por_productor = (ds.Tables[0].Rows[0]["observaciones_visualizacion_por_productor"] != null && ds.Tables[0].Rows[0]["observaciones_visualizacion_por_productor"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["observaciones_visualizacion_por_productor"].ToString() : "";
                this.formulario_aprobado_pronda = (ds.Tables[0].Rows[0]["formulario_aprobado_pronda"] != null && ds.Tables[0].Rows[0]["formulario_aprobado_pronda"] != System.DBNull.Value) ?bool.Parse( ds.Tables[0].Rows[0]["formulario_aprobado_pronda"].ToString() ): ((bool?)null);
                this.formulario_aprobado_sronda = (ds.Tables[0].Rows[0]["formulario_aprobado_sronda"] != null && ds.Tables[0].Rows[0]["formulario_aprobado_sronda"] != System.DBNull.Value) ? bool.Parse(ds.Tables[0].Rows[0]["formulario_aprobado_sronda"].ToString() ): ((bool?)null);
                this.aprueba_visualizacion_proyecto_pronda = (ds.Tables[0].Rows[0]["aprueba_visualizacion_proyecto_pronda"] != null && ds.Tables[0].Rows[0]["aprueba_visualizacion_proyecto_pronda"] != System.DBNull.Value) ? bool.Parse(ds.Tables[0].Rows[0]["aprueba_visualizacion_proyecto_pronda"].ToString()) : ((bool?)null);
                this.aprueba_visualizacion_proyecto_sronda = (ds.Tables[0].Rows[0]["aprueba_visualizacion_proyecto_sronda"] != null && ds.Tables[0].Rows[0]["aprueba_visualizacion_proyecto_sronda"] != System.DBNull.Value) ? bool.Parse(ds.Tables[0].Rows[0]["aprueba_visualizacion_proyecto_sronda"].ToString()) : ((bool?)null);
                
                this.project_synopsis = ds.Tables[0].Rows[0]["project_synopsis"].ToString() != "" ? ds.Tables[0].Rows[0]["project_synopsis"].ToString() : "";
                this.project_duration = ds.Tables[0].Rows[0]["project_duration"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_duration"] : 0;
                this.project_domestic_producer_qty = ds.Tables[0].Rows[0]["project_domestic_producer_qty"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_domestic_producer_qty"]:0;
                this.project_foreign_producer_qty = ds.Tables[0].Rows[0]["project_foreign_producer_qty"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_foreign_producer_qty"] : 0;
                this.project_total_cost_desarrollo = ds.Tables[0].Rows[0]["project_total_cost_desarrollo"].ToString() != "" ? (long)ds.Tables[0].Rows[0]["project_total_cost_desarrollo"] : 0;
                this.project_total_cost_preproduccion = ds.Tables[0].Rows[0]["project_total_cost_preproduccion"].ToString() != "" ? (long)ds.Tables[0].Rows[0]["project_total_cost_preproduccion"] : 0;
                this.project_total_cost_produccion = ds.Tables[0].Rows[0]["project_total_cost_produccion"].ToString() != "" ? (long)ds.Tables[0].Rows[0]["project_total_cost_produccion"] : 0;
                this.project_total_cost_posproduccion = ds.Tables[0].Rows[0]["project_total_cost_posproduccion"].ToString() != "" ? (long)ds.Tables[0].Rows[0]["project_total_cost_posproduccion"] : 0;
                this.project_total_cost_promotion = ds.Tables[0].Rows[0]["project_total_cost_promotion"].ToString() != "" ? (long)ds.Tables[0].Rows[0]["project_total_cost_promotion"] : 0;
                this.project_recording_sites = ds.Tables[0].Rows[0]["project_recording_sites"].ToString() != "" ? ds.Tables[0].Rows[0]["project_recording_sites"].ToString() : "";
                this.project_has_domestic_director = ds.Tables[0].Rows[0]["project_has_domestic_director"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_has_domestic_director"] : -1;
                this.project_staff_option_id = ds.Tables[0].Rows[0]["project_staff_option_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_staff_option_id"] : 0;
                this.project_filming_date_obs = ds.Tables[0].Rows[0]["project_filming_date_obs"].ToString() != "" ? ds.Tables[0].Rows[0]["project_filming_date_obs"].ToString() : "";
                this.project_development_lab_info = ds.Tables[0].Rows[0]["project_development_lab_info"].ToString() != "" ? ds.Tables[0].Rows[0]["project_development_lab_info"].ToString() : "";
                this.municipio_lab = ds.Tables[0].Rows[0]["municipio_lab"].ToString() != "" ? ds.Tables[0].Rows[0]["municipio_lab"].ToString() : "0";
                this.nombre_lab = ds.Tables[0].Rows[0]["nombre_lab"].ToString() != "" ? ds.Tables[0].Rows[0]["nombre_lab"].ToString() : "";
                this.municipio_lab_otro = ds.Tables[0].Rows[0]["municipio_lab_otro"].ToString() != "" ? ds.Tables[0].Rows[0]["municipio_lab_otro"].ToString() : "";
                this.no_acta_deposito = ds.Tables[0].Rows[0]["no_acta_deposito"].ToString() != "" ? ds.Tables[0].Rows[0]["no_acta_deposito"].ToString() : "";
                this.exhibida_publicamente = ds.Tables[0].Rows[0]["exhibida_publicamente"].ToString() != "" ? ds.Tables[0].Rows[0]["exhibida_publicamente"].ToString() : "";                
                this.otros_idiomas = ds.Tables[0].Rows[0]["otros_idiomas"].ToString() != "" ? ds.Tables[0].Rows[0]["otros_idiomas"].ToString() : "";
                this.project_preprint_store_info = ds.Tables[0].Rows[0]["project_domestic_producer_qty"].ToString() != "" ? ds.Tables[0].Rows[0]["project_preprint_store_info"].ToString():"";
                this.project_legal_deposit = ds.Tables[0].Rows[0]["project_legal_deposit"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_legal_deposit"]) : -1;
                this.project_result_film_view = ds.Tables[0].Rows[0]["project_result_film_view"].ToString() != "" ? ds.Tables[0].Rows[0]["project_result_film_view"].ToString() : "";
                this.project_clarification_request_additional_text = ds.Tables[0].Rows[0]["project_clarification_request_additional_text"].ToString() != "" ? ds.Tables[0].Rows[0]["project_clarification_request_additional_text"].ToString() : "";
                this.project_idusuario = ds.Tables[0].Rows[0]["project_idusuario"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_idusuario"] : 0;
                this.production_type_id = ds.Tables[0].Rows[0]["production_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["production_type_id"] : 0;
                this.project_type_id = ds.Tables[0].Rows[0]["project_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_type_id"] : 0;
                this.project_genre_id = ds.Tables[0].Rows[0]["project_genre_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_genre_id"] : 0;
                this.state_id = ds.Tables[0].Rows[0]["state_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["state_id"] : 0;
                this.project_percentage = ds.Tables[0].Rows[0]["project_percentage"].ToString() != "" ? (decimal)ds.Tables[0].Rows[0]["project_percentage"] : 0;
                this.project_personal_type = ds.Tables[0].Rows[0]["project_personal_type"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_personal_type"] : 0;
                this.cod_idioma = (ds.Tables[0].Rows[0]["cod_idioma"] != System.DBNull.Value &&  ds.Tables[0].Rows[0]["cod_idioma"].ToString() != "") ? (int)ds.Tables[0].Rows[0]["cod_idioma"] : 0;
                this.responsable = (ds.Tables[0].Rows[0]["responsable"] != System.DBNull.Value && ds.Tables[0].Rows[0]["responsable"].ToString() != "") ? (int?)ds.Tables[0].Rows[0]["responsable"] : (int?)null;
                this.complemento_carta_aclaraciones = (ds.Tables[0].Rows[0]["complemento_carta_aclaraciones"] != System.DBNull.Value && ds.Tables[0].Rows[0]["complemento_carta_aclaraciones"].ToString() != "") ? ds.Tables[0].Rows[0]["complemento_carta_aclaraciones"].ToString() : "";

                this.cod_firma_tramite = (ds.Tables[0].Rows[0]["cod_firma_tramite"] != System.DBNull.Value && ds.Tables[0].Rows[0]["cod_firma_tramite"].ToString() != "") ? (int)ds.Tables[0].Rows[0]["cod_firma_tramite"] : (0);

                this.tiene_premio = (ds.Tables[0].Rows[0]["tiene_premio"] != null && ds.Tables[0].Rows[0]["tiene_premio"] != System.DBNull.Value) ? bool.Parse(ds.Tables[0].Rows[0]["tiene_premio"].ToString()) : ((bool?)null);
                this.premio = (ds.Tables[0].Rows[0]["premio"] != null && ds.Tables[0].Rows[0]["premio"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["premio"].ToString() : "";
                this.sustituto_carta_aclaracion = (ds.Tables[0].Rows[0]["sustituto_carta_aclaracion"] != null && ds.Tables[0].Rows[0]["sustituto_carta_aclaracion"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["sustituto_carta_aclaracion"].ToString() : "";

                this.obs_adicional_obra = (ds.Tables[0].Rows[0]["obs_adicional_obra"] != null && ds.Tables[0].Rows[0]["obs_adicional_obra"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["obs_adicional_obra"].ToString() : "";
                this.obs_adicional_productor = (ds.Tables[0].Rows[0]["obs_adicional_productor"] != null && ds.Tables[0].Rows[0]["obs_adicional_productor"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["obs_adicional_productor"].ToString() : "";
                this.obs_adicional_otros_prd = (ds.Tables[0].Rows[0]["obs_adicional_otros_prd"] != null && ds.Tables[0].Rows[0]["obs_adicional_otros_prd"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["obs_adicional_otros_prd"].ToString() : "";
                this.obs_adicional_personal = (ds.Tables[0].Rows[0]["obs_adicional_personal"] != null && ds.Tables[0].Rows[0]["obs_adicional_personal"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["obs_adicional_personal"].ToString() : "";
                this.obs_adicional_finalizacion = (ds.Tables[0].Rows[0]["obs_adicional_finalizacion"] != null && ds.Tables[0].Rows[0]["obs_adicional_finalizacion"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["obs_adicional_finalizacion"].ToString() : "";

                this.carta_aclaraciones_generada = (ds.Tables[0].Rows[0]["carta_aclaraciones_generada"] != null && ds.Tables[0].Rows[0]["carta_aclaraciones_generada"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["carta_aclaraciones_generada"].ToString() : "";

                this.version = int.Parse(ds.Tables[0].Rows[0]["version"].ToString());


                if (ds.Tables[0].Rows[0]["project_filming_start_date"].ToString() != "")
                {
                    this.project_filming_start_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_filming_start_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["project_filming_end_date"].ToString() != "")
                {
                    this.project_filming_end_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_filming_end_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["project_schedule_film_view"].ToString() != "")
                {
                    this.project_schedule_film_view = DateTime.Parse(ds.Tables[0].Rows[0]["project_schedule_film_view"].ToString());
                }
                if(ds.Tables[0].Rows[0]["project_request_date"].ToString() != "")
                {
                    this.project_request_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_request_date"].ToString());
                }
                if(ds.Tables[0].Rows[0]["project_clarification_request_date"].ToString() != "")
                {
                    this.project_clarification_request_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_clarification_request_date"].ToString());
                }
                if(ds.Tables[0].Rows[0]["project_clarification_response_date"].ToString() != "")
                {
                    this.project_clarification_response_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_clarification_response_date"].ToString());
                }
                if(ds.Tables[0].Rows[0]["project_resolution_date"].ToString() != "")
                {
                    this.project_resolution_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_resolution_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["project_resolution2_date"].ToString() != "")
                {
                    this.project_resolution2_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_resolution2_date"].ToString());
                }
                if(ds.Tables[0].Rows[0]["project_notification_date"].ToString() != "")
                {
                    this.project_notification_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_notification_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["project_notification2_date"].ToString() != "")
                {
                    this.project_notification2_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_notification2_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fecha_revisor_editor"].ToString() != "")
                {
                    this.fecha_revisor_editor = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_revisor_editor"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fecha_editor_director"].ToString() != "")
                {
                    this.fecha_editor_director = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_editor_director"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fecha_revisor_editor2"].ToString() != "")
                {
                    this.fecha_revisor_editor2 = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_revisor_editor2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fecha_editor_director2"].ToString() != "")
                {
                    this.fecha_editor_director2 = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_editor_director2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fecha_cancelacion"].ToString() != "")
                {
                    this.fecha_cancelacion = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_cancelacion"].ToString());
                }

                /* Obtiene los formatos asociados al proyecto, tanto de rodaje como de exhibición */
                this.GetFormats();

                /* Obtiene los productores asociados al proyecto */
                this.GetProducers();

                /* Obtiene la información de personal relacionada con el proyecto */
                this.GetStaff();

                if (!load_basic) 
                {
                    /* Obtiene los adjuntos requeridos y los cargados al proyecto */
                    this.GetAttachments();
                }

                /* Obtiene las secciones del proyecto con su información de administración */
                this.GetSections();

                /* Obtiene la información del personal registrada en el formato de personal */
                this.GetStaffFormat();
            }
        }



        public string  LoadCorreo(string codigoManual)
        {
            DB db = new DB();
            DataSet ds = db.Select("select texto from dboPrd.correo where cod_manual_correo ='" + codigoManual+"' ");
            if (ds.Tables[0].Rows.Count >= 1)
            {
                if (ds.Tables[0].Rows[0][0] == null || ds.Tables[0].Rows[0][0] == System.DBNull.Value ||
                    ds.Tables[0].Rows[0][0].ToString().Trim() == string.Empty )
                    return codigoManual + " mal configurado para envio de email";
                else
                    return ds.Tables[0].Rows[0][0].ToString();
            }
            else {
                return codigoManual + " no configurado para envio de email";
            }
        }

        /* Metodo que permite obtener la duración del proyecto en minutos y segundos */
        public int GetProjectDuration(string part = "") 
        {
            /* Retorna el valor solicitado */
            if (part == "minutes") 
            {
                /* Obtiene la cantidad de minutos */
                int duracion_minutos = this.project_duration / 60;

                return duracion_minutos;
            }
            else if (part == "seconds")
            {
                /* Obtiene la cantidad de segudos para completar la duración */
                int duracion_segundos = this.project_duration % 60;

                return duracion_segundos;
            }
            else 
            {
                return this.project_duration;
            }
        }

        /* Metodo que permite definir la duración del proyecto en minutos y segundos */
        public void SetProjectDuration(int minutes, int seconds)
        {
            this.project_duration = (60 * minutes) + seconds;
        }

        /// <summary>
        /// retorna el id del proyecto en caso de que exista
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public int validarNombreProyecto(string nombre, string idUsuario) 
        {
            DB db = new DB();
            string query = @"select project_id from dboPrd.project where lower(rtrim(project_name)) ='" + nombre.Trim().ToLower() + "' and (state_id=5 or state_id=1) and project_idusuario =" + idUsuario;
            var ds = db.Select(query).Tables[0];

            if (ds.Rows.Count == 0) return 0;

            return int.Parse( ds.Rows[0][0].ToString() );

        }

        public bool Save(bool basico=false)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Si esta definido un project_id se hace una actualización, de lo contrario se hace una inserción */
            if (this.project_id.ToString() != "" && this.project_id > 0)
            {
                /* Ajuste de la información de fechas, donde se valida si vienen fechas por defecto se
                 * ajustan en null para la actualización correcta en la base de datos */
                string project_request_date_update_query = (this.project_request_date.Year == 1) ? "null" : "'" + this.project_request_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string project_schedule_film_view_update_query = (this.project_schedule_film_view.Year == 1) ? "null" : "'" + this.project_schedule_film_view.ToString("dd-MM-yy HH:mm") + "'";
                string project_clarafication_request_date_update_query = (this.project_clarification_request_date.Year == 1) ? "null" : "'" + this.project_clarification_request_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string project_clarification_response_date_update_query = (this.project_clarification_response_date.Year == 1) ? "null" : "'" + this.project_clarification_response_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string project_resolution_date_update_query = (this.project_resolution_date.Year == 1) ? "null" : "'" + this.project_resolution_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string project_notification_date_update_query = (this.project_notification_date.Year == 1) ? "null" : "'" + this.project_notification_date.ToString("dd-MM-yy HH:mm:ss") + "'";

                string project_resolution2_date_update_query = (this.project_resolution2_date.Year == 1) ? "null" : "'" + this.project_resolution2_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string project_notification2_date_update_query = (this.project_notification2_date.Year == 1) ? "null" : "'" + this.project_notification2_date.ToString("dd-MM-yy HH:mm:ss") + "'";

                string fecha_revisor_editor_update_query = (this.fecha_revisor_editor.HasValue == false || this.fecha_revisor_editor.Value.Year == 1) ? "null" : "'" + this.fecha_revisor_editor.Value.ToString("dd-MM-yy HH:mm:ss") + "'";
                string fecha_editor_director_update_query = (this.fecha_editor_director.HasValue == false || this.fecha_editor_director.Value.Year == 1) ? "null" : "'" + this.fecha_editor_director.Value.ToString("dd-MM-yy HH:mm:ss") + "'";
                string fecha_revisor_editor2_update_query = (this.fecha_revisor_editor2.HasValue == false || this.fecha_revisor_editor2.Value.Year == 1) ? "null" : "'" + this.fecha_revisor_editor2.Value.ToString("dd-MM-yy HH:mm:ss") + "'";
                string fecha_editor_director2_update_query = (this.fecha_editor_director2.HasValue == false || this.fecha_editor_director2.Value.Year == 1) ? "null" : "'" + this.fecha_editor_director2.Value.ToString("dd-MM-yy HH:mm:ss") + "'";
                string fecha_cancelacion_update_query = (this.fecha_cancelacion.HasValue == false || this.fecha_cancelacion.Value.Year == 1) ? "null" : "'" + this.fecha_cancelacion.Value.ToString("dd-MM-yy HH:mm:ss") + "'";
                string fecha_notificacion_certificado_update_query = (this.fecha_notificacion_certificado.HasValue == false || this.fecha_notificacion_certificado.Value.Year == 1) ? "null" : "'" + this.fecha_notificacion_certificado.Value.ToString("dd-MM-yy HH:mm:ss") + "'";

                string project_legal_deposit_update_query = (this.project_legal_deposit == -1) ? "null" : "'" + this.project_legal_deposit + "'";

                /* Ajuste de la información del tipo de proyecto para su correcto almacenamiento en la base de datos */
                string project_type_update_query = (this.project_type_id == 0) ? "null" : "'" + this.project_type_id + "'";
                string production_type_update_query = (this.production_type_id == 0) ? "null" : "'" + this.production_type_id + "'";
                string project_genre_update_query = (this.project_genre_id == 0) ? "null" : "'" + this.project_genre_id + "'";

                /* Ajuste de la variable que se salvara sobre la nacionalidad del director y sobre la opción de personal */
                string project_has_domestic_director = (this.project_has_domestic_director == -1) ? "null" : "'" + this.project_has_domestic_director + "'";
                string project_staff_option_id = (this.project_staff_option_id == 0) ? "null" : "'" + this.project_staff_option_id + "'";
                string str_cod_idioma = (this.cod_idioma == 0) ? "null" : "'" + this.cod_idioma + "'";
                string str_responsable = (this.responsable == null) ? "null" : "'" + this.responsable + "'";


                string str_cod_firma = (this.cod_firma_tramite == 0) ? "null" : "'" + this.cod_firma_tramite + "'";
                /* Creación de la sentencia de actualizacion */
                string updateProject = "UPDATE dboPrd.project SET ";


                List<System.Data.SqlClient.SqlParameter> listaParametros = new List<System.Data.SqlClient.SqlParameter>();

                updateProject = updateProject + "project_name = '" + this.project_name.Replace("'", "´") + "', ";
                updateProject = updateProject + "project_synopsis = '" + this.project_synopsis.Replace("'", "´") + "', ";
                updateProject = updateProject + "project_duration = '" + this.project_duration + "', ";
                updateProject = updateProject + "project_domestic_producer_qty = '" + this.project_domestic_producer_qty + "', ";
                updateProject = updateProject + "project_foreign_producer_qty = '" + this.project_foreign_producer_qty + "', ";
                updateProject = updateProject + "project_total_cost_desarrollo = '" + this.project_total_cost_desarrollo + "', ";
                updateProject = updateProject + "project_total_cost_preproduccion = '" + this.project_total_cost_preproduccion + "', ";
                updateProject = updateProject + "project_total_cost_produccion = '" + this.project_total_cost_produccion + "', ";
                updateProject = updateProject + "project_total_cost_posproduccion = '" + this.project_total_cost_posproduccion + "', ";
                updateProject = updateProject + "project_total_cost_promotion = '" + this.project_total_cost_promotion + "', ";
                updateProject = updateProject + "project_recording_sites = '" + this.project_recording_sites + "', ";
                updateProject = updateProject + "project_has_domestic_director = " + project_has_domestic_director + ", ";
                updateProject = updateProject + "project_staff_option_id = " + project_staff_option_id + ", ";
                updateProject = updateProject + "project_filming_start_date = " + ((this.project_filming_start_date.Year==1)?"null":("'"+this.project_filming_start_date.Year + "-" + this.project_filming_start_date.Month + "-" + this.project_filming_start_date.Day+"'")) + ", ";
                updateProject = updateProject + "project_schedule_film_view = " + project_schedule_film_view_update_query + ", ";
                updateProject = updateProject + "project_result_film_view = '" + this.project_result_film_view.Replace("'", "´") + "', ";
                updateProject = updateProject + "project_filming_end_date = " + ((this.project_filming_end_date.Year ==1)?"null":("'"+this.project_filming_end_date.Year + "-" + project_filming_end_date.Month + "-" + project_filming_end_date.Day+"'")) + ", ";
                updateProject = updateProject + "project_filming_date_obs = '" + this.project_filming_date_obs.Replace("'", "´") + "', ";
                updateProject = updateProject + "project_development_lab_info = '" + this.project_development_lab_info.Replace("'", "´") + "', ";

                updateProject = updateProject + "municipio_lab = '" + municipio_lab + "', ";
                updateProject = updateProject + "nombre_lab = '" + this.nombre_lab.Replace("'", "´") + "', ";
                updateProject = updateProject + "municipio_lab_otro = '" + municipio_lab_otro + "', ";
                updateProject = updateProject + "no_acta_deposito = '" + this.no_acta_deposito.Replace("'", "´") + "', ";
                updateProject = updateProject + "exhibida_publicamente = '" + this.exhibida_publicamente.Replace("'", "´") + "', ";
                updateProject = updateProject + "otros_idiomas = '" + this.otros_idiomas.Replace("'", "´") + "', ";                

                updateProject = updateProject + "project_preprint_store_info = '" + this.project_preprint_store_info.Replace("'", "´") + "', ";
                updateProject = updateProject + "project_legal_deposit = " + project_legal_deposit_update_query + ", ";
                updateProject = updateProject + "project_clarification_request_additional_text = '" + this.project_clarification_request_additional_text.Replace("'", "´") + "', ";
                updateProject = updateProject + "production_type_id = " + production_type_update_query + ", ";
                updateProject = updateProject + "project_type_id = " + project_type_update_query + ", ";
                updateProject = updateProject + "project_genre_id = " + project_genre_update_query + ", ";
                updateProject = updateProject + "project_request_date = " + project_request_date_update_query + ", ";
                updateProject = updateProject + "project_clarification_request_date = " + project_clarafication_request_date_update_query + ", ";
                updateProject = updateProject + "project_clarification_response_date = " + project_clarification_response_date_update_query + ", ";
                updateProject = updateProject + "project_resolution_date = " + project_resolution_date_update_query + ", ";
                updateProject = updateProject + "project_resolution2_date = " + project_resolution2_date_update_query + ", ";
                updateProject = updateProject + "project_notification_date = " + project_notification_date_update_query + ", ";
                updateProject = updateProject + "project_notification2_date = " + project_notification2_date_update_query + ", ";
                updateProject = updateProject + "fecha_revisor_editor = " + fecha_revisor_editor_update_query + ", ";
                updateProject = updateProject + "fecha_editor_director = " + fecha_editor_director_update_query + ", ";
                updateProject = updateProject + "fecha_revisor_editor2 = " + fecha_revisor_editor2_update_query + ", ";
                updateProject = updateProject + "fecha_editor_director2 = " + fecha_editor_director2_update_query + ", ";
                updateProject = updateProject + "fecha_cancelacion = " + fecha_cancelacion_update_query + ", ";
                updateProject = updateProject + "state_id = " + this.state_id + ", ";
                updateProject = updateProject + "project_percentage = " + this.project_percentage.ToString().Replace(",",".") + ", ";
                updateProject = updateProject + "observaciones_visualizacion_por_productor = '" + this.observaciones_visualizacion_por_productor.Replace("'", "´") + "', ";
                updateProject = updateProject + "premio = '" + this.premio.Replace("'", "´") + "', ";
                updateProject = updateProject + "sustituto_carta_aclaracion = '" + this.sustituto_carta_aclaracion.Replace("'", "´") + "', ";
                updateProject = updateProject + "titulo_provisional = '" + this.titulo_provisional.Replace("'", "´") + "', ";
                updateProject = updateProject + "pagina_facebook = '" + this.pagina_facebook.Replace("'", "´") + "', ";

                updateProject = updateProject + "tiene_reconocimiento = '" + this.tiene_reconocimiento.Replace("'", "´") + "', ";
                updateProject = updateProject + "ano_resolucion = '" + this.ano_resolucion.Replace("'", "´") + "', ";
                updateProject = updateProject + "num_resolucion = '" + this.num_resolucion.Replace("'", "´") + "', ";
                updateProject = updateProject + "tiene_estimulos = '" + this.tiene_estimulos.Replace("'", "´") + "', ";
                updateProject = updateProject + "fdc = '" + this.fdc.Replace("'", "´") + "', ";
                updateProject = updateProject + "fdc_especificacion = '" + this.fdc_especificacion.Replace("'", "´") + "', ";
                updateProject = updateProject + "ibermedia = '" + this.ibermedia.Replace("'", "´") + "', ";
                updateProject = updateProject + "otros_estimulos = '" + this.otros_estimulos.Replace("'", "´") + "', ";
                updateProject = updateProject + "ibermedia_especificacion = '" + this.ibermedia_especificacion.Replace("'", "´") + "', ";
                updateProject = updateProject + "inf_visualizacion = '" + this.inf_visualizacion.Replace("'", "´") + "', ";
                updateProject = updateProject + "fecha_notificacion_certificado = " + fecha_notificacion_certificado_update_query + ", ";

                updateProject = updateProject + "pagina_web = '" + this.pagina_web.Replace("'", "´") + "', ";
                updateProject = updateProject + "aprueba_visualizacion_proyecto_sronda = " + ((this.aprueba_visualizacion_proyecto_sronda == null) ? "null" : ((this.aprueba_visualizacion_proyecto_sronda.Value) ? "1" : "0")) + ", ";
                updateProject = updateProject + "tiene_premio = " + ((this.tiene_premio == null) ? "null" : ((this.tiene_premio.Value) ? "1" : "0")) + ", ";
                updateProject = updateProject + "aprueba_visualizacion_proyecto_pronda = " + ((this.aprueba_visualizacion_proyecto_pronda==null)?"null": ((this.aprueba_visualizacion_proyecto_pronda.Value)?"1":"0")  ) + ", ";
                updateProject = updateProject + "formulario_aprobado_sronda = " + ((this.formulario_aprobado_sronda==null)?"null": ((this.formulario_aprobado_sronda.Value)?"1":"0")  ) + ", ";
                updateProject = updateProject + "formulario_aprobado_pronda = " + ((this.formulario_aprobado_pronda==null)?"null": ((this.formulario_aprobado_pronda.Value)?"1":"0")  ) + ", ";
                updateProject = updateProject + "project_personal_type = " + this.project_personal_type + ", ";                
                updateProject = updateProject + "cod_firma_tramite = " + str_cod_firma + ", ";
                updateProject = updateProject + "cod_idioma = " + str_cod_idioma + ", ";
                updateProject = updateProject + "responsable = " + str_responsable + ", ";
                updateProject = updateProject + "complemento_carta_aclaraciones = '" + complemento_carta_aclaraciones.Replace("'", "´") + "', ";

                //if (this.state_id == 2 )//esta enviandola radicandola o enviando aclaraciones
                //{
                //    Calendario cal = new Calendario();
                //    DateTime fechaLimite = cal.SumarDiasLaborales(this.project_request_date, 10);                    
                //    updateProject = updateProject + "fecha_limite = " + ((fechaLimite.Year == 1) ? "null" : ("'" + fechaLimite.Year + "-" + fechaLimite.Month + "-" + fechaLimite.Day + "'")) + ", ";                    
                //}
                //if (this.state_id == 6)//esta enviandola radicandola o enviando aclaraciones
                //{
                //    Calendario cal = new Calendario();
                //    DateTime fechaLimite = cal.SumarDiasLaborales(DateTime.Now, 10);
                //    updateProject = updateProject + "fecha_limite = " + ((fechaLimite.Year == 1) ? "null" : ("'" + fechaLimite.Year + "-" + fechaLimite.Month + "-" + fechaLimite.Day + "'")) + ", ";
                //}

                updateProject = updateProject + "obs_adicional_obra = @obsAdcional ,";


                System.Data.SqlClient.SqlParameter parametroObsAdcional = new System.Data.SqlClient.SqlParameter();
                parametroObsAdcional.Value = obs_adicional_obra;
                parametroObsAdcional.ParameterName = "@obsAdcional";
                parametroObsAdcional.Direction = ParameterDirection.Input;
                parametroObsAdcional.SqlDbType = SqlDbType.VarChar;
                listaParametros.Add(parametroObsAdcional);
                //updateProject = updateProject + "obs_adicional_obra = '" + obs_adicional_obra.Replace("'", "´") + "', ";
                updateProject = updateProject + "obs_adicional_productor = @obs_adicional_productor ,";
                System.Data.SqlClient.SqlParameter parametroobs_adicional_productor = new System.Data.SqlClient.SqlParameter();
                parametroobs_adicional_productor.Value = obs_adicional_productor;
                parametroobs_adicional_productor.ParameterName = "@obs_adicional_productor";
                parametroobs_adicional_productor.Direction = ParameterDirection.Input;
                parametroobs_adicional_productor.SqlDbType = SqlDbType.VarChar;
                listaParametros.Add(parametroobs_adicional_productor);

                updateProject = updateProject + "obs_adicional_otros_prd = @obs_adicional_otros_prd, ";
                System.Data.SqlClient.SqlParameter parametroobs_adicional_otros_prd = new System.Data.SqlClient.SqlParameter();
                parametroobs_adicional_otros_prd.Value = obs_adicional_otros_prd;
                parametroobs_adicional_otros_prd.ParameterName = "@obs_adicional_otros_prd";
                parametroobs_adicional_otros_prd.Direction = ParameterDirection.Input;
                parametroobs_adicional_otros_prd.SqlDbType = SqlDbType.VarChar;
                listaParametros.Add(parametroobs_adicional_otros_prd);

                updateProject = updateProject + "obs_adicional_personal = @obs_adicional_personal ,";
                System.Data.SqlClient.SqlParameter parametroobs_adicional_personal = new System.Data.SqlClient.SqlParameter();
                parametroobs_adicional_personal.Value = obs_adicional_personal;
                parametroobs_adicional_personal.ParameterName = "@obs_adicional_personal";
                parametroobs_adicional_personal.Direction = ParameterDirection.Input;
                parametroobs_adicional_personal.SqlDbType = SqlDbType.VarChar;
                listaParametros.Add(parametroobs_adicional_personal);

                updateProject = updateProject + "obs_adicional_finalizacion = @obs_adicional_finalizacion ,";
                System.Data.SqlClient.SqlParameter parametroobs_adicional_finalizacion = new System.Data.SqlClient.SqlParameter();
                parametroobs_adicional_finalizacion.Value = obs_adicional_finalizacion;
                parametroobs_adicional_finalizacion.ParameterName = "@obs_adicional_finalizacion";
                parametroobs_adicional_finalizacion.Direction = ParameterDirection.Input;
                parametroobs_adicional_finalizacion.SqlDbType = SqlDbType.VarChar;
                listaParametros.Add(parametroobs_adicional_finalizacion);

                updateProject = updateProject + "carta_aclaraciones_generada = @carta_aclaraciones_generada ";
                System.Data.SqlClient.SqlParameter parametrocarta_aclaraciones_generada = new System.Data.SqlClient.SqlParameter();
                parametrocarta_aclaraciones_generada.Value = carta_aclaraciones_generada;
                parametrocarta_aclaraciones_generada.ParameterName = "@carta_aclaraciones_generada";
                parametrocarta_aclaraciones_generada.Direction = ParameterDirection.Input;
                parametrocarta_aclaraciones_generada.SqlDbType = SqlDbType.VarChar;
                listaParametros.Add(parametrocarta_aclaraciones_generada);



                updateProject = updateProject + " WHERE project_id = " + this.project_id.ToString();



                bool execution=db.Execute(updateProject,listaParametros);
                if (basico)
                {
                    return execution;
                }

                /* Si se actualizó correctamente la tabla del proyecto, se procede a actualizar la tabla de formatos del proyecto */
                if (execution && basico==false)
                {
                    /* Se borran todos los formatos relacionados con el proyecto */
                    string deleteFormats = "DELETE FROM dboPrd.project_format WHERE project_id=" + this.project_id;
                    db.Execute(deleteFormats);

                    /* Se graban todos los formatos relacionados con el proyecto */
                    foreach (Format format in this.project_format)
                    {
                        if (format.format_id > 0)
                        {
                            string insertFormats = "INSERT INTO dboPrd.project_format (format_id, project_id, project_format_detail) ";
                            insertFormats = insertFormats + "VALUES ('" + format.format_id + "','" + this.project_id + "','" + format.format_project_detail + "')";
                            if (db.Execute(insertFormats) == false)
                            {
                                return false;
                            }
                         }

                    }

                    /* Se borran todas la relaciones de productores con el proyecto */
                    string deleteProducers = "DELETE FROM dboPrd.project_producer WHERE project_id=" + this.project_id;
                    db.Execute(deleteProducers);

                    /* Se graba la informacion de los productores y se crean las relaciones */
                    int domesticProducersQty = 0; //Variable para el control de la cantidad de productores nacionales a salvar.
                    int foreignProducersQty = 0; //Variable para el control de la cantidad de productores extranjeros a salvar.


                    foreach (Producer producer in this.producer.OrderByDescending(p => p.requester))
                    {
                        bool saveProducer = false;
                        if(producer.producer_type_id == 1 && domesticProducersQty < this.project_domestic_producer_qty)
                        {
                            domesticProducersQty++;
                            saveProducer = true;
                        }
                        if (producer.producer_type_id == 2 && foreignProducersQty < this.project_foreign_producer_qty)
                        {
                            foreignProducersQty++;
                            saveProducer = true;
                        }
                        if (saveProducer)
                        {
                            if (producer.Save(this.project_id) && saveProducer)
                            {
                                string addProducer = "INSERT INTO dboPrd.project_producer (producer_id, project_id, project_producer_participation_percentage, project_producer_requester) ";
                                addProducer = addProducer + "VALUES ('" + producer.producer_id + "','" + this.project_id + "'," + producer.participation_percentage.ToString().Replace(',','.') + ",'" + producer.requester + "')";
                                if (db.Execute(addProducer) == false)
                                {
                                    return false;
                                }
                            } else {
                                return false;
                            }
                        }
                    }

                    //inserta la cantidad de productores nacionales y extranjeros.
                    NegocioCineProducto neg = new NegocioCineProducto();
                    if (domesticProducersQty < this.project_domestic_producer_qty) {
                        neg.crearProjectProducts(this.project_id, 1, this.project_domestic_producer_qty - domesticProducersQty, 0);
                    }
                    if (foreignProducersQty < this.project_foreign_producer_qty) {
                        neg.crearProjectProducts(this.project_id, 2, this.project_foreign_producer_qty - foreignProducersQty, 0);
                    }                   
                    
                    


                    /* Cuando se finaliza la grabación se recarga en el objeto la informacion de los productores
                     * con el fin de obtener nuevament el identificador del departamento */
                    this.GetProducers();

                    /* Guarda los cambios realizados en los objetos de personal */
                    foreach (Staff staff in this.staff)
                    {
                        staff.Save();
                    }

                    /* Guarda los cambios realizados en los objetos de las secciones */
                    this.sectionDatosProyecto.Save();
                    this.sectionDatosProductor.Save();
                    this.sectionDatosProductoresAdicionales.Save();
                    this.sectionDatosPersonal.Save();
                    this.sectionDatosAdjuntos.Save();
                    this.sectionDatosFormatoPersonal.Save();
                    this.sectionDatosFinalizacion.Save();

                    /* Guarda la información registrada del personal del formato de personal */
                    this.StaffFormatDirector.Save();
                    this.StaffFormatGuionista.Save();
                    this.StaffFormatDirectorFotografía.Save();
                    this.StaffFormatDirectorArte.Save();
                    this.StaffFormatAutorMusica.Save();
                    this.StaffFormatEditorMontajista.Save();
                    this.StaffFormatCamarografo.Save();
                    this.StaffFormatMaquillador.Save();
                    this.StaffFormatVestuarista.Save();
                    this.StaffFormatAmbientador.Save();
                    this.StaffFormatEncargadoCasting.Save();
                    this.StaffFormaScript.Save();
                    this.StaffFormatSonidista.Save();
                    this.StaffFormatMezclador.Save();
                    for (int k = 0; k < numeroCargos; k++)
                    {
                        this.StaffFormatArreglo[k].Save();
                    }
                    /* Si todas las inserciones fueron exitosas se devuelve el valor true */
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else if(this.project_name != "" && this.project_idusuario > 0) //Se inserta el registro para crear una nueva solicitud
            {
                /* Creación de la sentencia de actualizacion */
                string insertProject = "INSERT INTO dboPrd.project (project_name, state_id, project_idusuario) "
                                      + " VALUES ('"+ this.project_name +"',1,'"+ this.project_idusuario +"')";
                
                /* Si se actualizó correctamente la tabla del proyecto, se procede a actualizar la tabla de formatos del proyecto */
                if (db.Execute(insertProject))
                {
                    /* Obtiene el id del registro insertado y vuelve a cargar la inforamación del objeto */
                    string queryId = "SELECT max(project_id) as project_id FROM dboPrd.project WHERE project_name='" + this.project_name + "'";
                    DataSet queryIdDS = db.Select(queryId);
                    if (queryIdDS.Tables[0].Rows.Count == 1)
                    {
                        this.LoadProject(Convert.ToInt32(queryIdDS.Tables[0].Rows[0]["project_id"]),basico);
                        if (this.project_id > 0)
                        {
                            return true;
                        }
                        else 
                        {
                            return false;
                        }
                    }
                    else 
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

        /* Función que obtiene los formatos de rodaje y exhibición registrados para una solicitud 
         * no tiene parametros ni retorna valor
         */
        public void GetFormats()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Se inicializa la lista de formatos */
            this.project_format = new List<Format>();

            /* Si esta definido un project_id se hace la consulta de los formatos */
            if (this.project_id.ToString() != "" && this.project_id > 0)
            {
                string queryFormats = "SELECT format_id, project_format_detail FROM dboPrd.project_format WHERE project_id= " + this.project_id;
                DataSet queryFormatsDS = db.Select(queryFormats);
                for (int i = 0; i < queryFormatsDS.Tables[0].Rows.Count; i++)
                {
                    Format newFormat = new Format();
                    newFormat.LoadFormat((int)queryFormatsDS.Tables[0].Rows[i]["format_id"]);
                    newFormat.format_project_detail = queryFormatsDS.Tables[0].Rows[i]["project_format_detail"].ToString();
                    this.project_format.Add(newFormat);
                }
            }
        }

        
        /* Este método consulta y carga en el objeto todos los productores del proyecto */
        public void GetProducers()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Se inicializa la lista de formatos */
            this.producer = new List<Producer>();

            /* Si esta definido un project_id se hace la consulta de los productores */
            if (this.project_id.ToString() != "" && this.project_id > 0)
            {
                string queryProducers = "SELECT producer_id, project_producer_participation_percentage, project_producer_requester ";
                queryProducers = queryProducers + "FROM dboPrd.project_producer WHERE project_id= " + this.project_id;
                DataSet queryProducersDS = db.Select(queryProducers);
                for (int i = 0; i < queryProducersDS.Tables[0].Rows.Count; i++)
                {
                    Producer newProducer = new Producer();
                    newProducer.LoadProducer((int)queryProducersDS.Tables[0].Rows[i]["producer_id"]);
                    newProducer.participation_percentage = queryProducersDS.Tables[0].Rows[0]["project_producer_participation_percentage"].ToString() != "" ? decimal.Parse(queryProducersDS.Tables[0].Rows[i]["project_producer_participation_percentage"].ToString()) : 0;
                    newProducer.requester = (int)queryProducersDS.Tables[0].Rows[i]["project_producer_requester"];
                    this.producer.Add(newProducer);
                }
            }
        }

        /* Este método consulta en la base de datos la información del personal
         * relacionado con el proyecto cargado en el objeto y crea un objeto de
           la clase Staff para cada persona, y lo deja disponible en el atributo
           staff el cual es una lista de objetos de tipo Staff */
        public void GetStaff()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Se inicializa la lista de formatos */
            this.staff = new List<Staff>();

            /* Si esta definido un project_id se hace la consulta del personal relacionado */
            if (this.project_id.ToString() != "" && this.project_id > 0)
            {
                string queryPersonal = @"
                        set dateformat dmy
select project_staff_id from (

select
project_staff_id,
case when position.position_father_id = 0 then position.position_name else p2.position_name end as TipoCargo,
case when position.position_father_id = 0 then '' else position.position_name end as Position,
case when position.position_father_id = 0 then position.position_id else p2.position_id end pid,
ROW_NUMBER() OVER (PARTITION BY case when position.position_father_id = 0 then position.position_name else p2.position_name end order by project_staff_id) AS cnt 
,st.staff_option_detail_quantity
from dboPrd.project_staff  
left join dboPrd.position on position.position_id =  project_staff.project_staff_position_id
left join dboPrd.position p2 on position.position_father_id =  p2.position_id
left join dboPrd.genero on genero.id_genero = project_staff.id_genero
left join dboPrd.etnia on etnia.id_etnia = project_staff.id_etnia
left join dboPrd.grupo_poblacional on grupo_poblacional.id_grupo_poblacional = project_staff.id_grupo_poblacional
left join dboPrd.identification_type on identification_type.identification_type_id = project_staff.identification_type_id
join(
select p.project_id,position.position_id,
position.position_name,staff_option_detail.[staff_option_detail_quantity]
 from dboPrd.project p 
join dboPrd.staff_option on staff_option.project_type_id = p.project_type_id and
staff_option.project_type_id = p.project_type_id and staff_option.project_genre_id = p.project_genre_id and 
staff_option.staff_option_has_domestic_director = p.project_has_domestic_director and 
p.project_percentage between staff_option.staff_option_percentage_init and  staff_option.staff_option_percentage_end
and staff_option.staff_option_deleted=0
join dboPrd.staff_option_detail on staff_option_detail.staff_option_id= staff_option.staff_option_id and staff_option_detail.version = p.version and staff_option_detail.staff_option_detail_deleted=0
join dboPrd.position on position.position_id= staff_option_detail.position_id

) st on st.project_id = project_staff.project_staff_project_id and st.position_id = (case when position.position_father_id = 0 then position.position_id else p2.position_id end)

WHERE project_staff.project_staff_project_id= " + this.project_id +

")ss where ss.cnt<= staff_option_detail_quantity";
     
                string queryStaff = "SELECT project_staff_id " +
                                        "FROM dboPrd.project_staff " +
                                        "WHERE project_staff_project_id= " + this.project_id;
                DataSet queryStaffDS = db.Select(queryPersonal);
                for (int i = 0; i < queryStaffDS.Tables[0].Rows.Count; i++)
                {
                    Staff newStaff = new Staff();
                    newStaff.LoadStaff((int)queryStaffDS.Tables[0].Rows[i]["project_staff_id"]);
                    this.staff.Add(newStaff);
                }
            }
        }

        /* Este método consulta en la base de datos la información de administración 
         * de las secciones relacionadas con el proyecto cargado en el objeto y crea 
         * un objeto de la clase Section para cada sección, y lo deja disponible en el 
         * atributo section el cual es una lista de objetos de tipo Section */
        public void GetSections()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            this.sectionDatosProyecto = new Section(1,this.project_id);
            this.sectionDatosProductor = new Section(2,this.project_id);
            this.sectionDatosProductoresAdicionales = new Section(3,this.project_id);
            this.sectionDatosPersonal = new Section(4,this.project_id);
            this.sectionDatosAdjuntos = new Section(5,this.project_id);
            this.sectionDatosFormatoPersonal = new Section(7,this.project_id);
            this.sectionDatosFinalizacion = new Section(6, this.project_id);
        }

        /* Este método consulta la informacion del personal registrada en
         * el formato de personal */
        public void GetStaffFormat()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            this.StaffFormatDirector = new StaffFormat(this.project_id, "director");
            this.StaffFormatGuionista = new StaffFormat(this.project_id, "guionista");
            this.StaffFormatDirectorFotografía = new StaffFormat(this.project_id, "director_fotografia");
            this.StaffFormatDirectorArte = new StaffFormat(this.project_id, "director_arte");
            this.StaffFormatAutorMusica = new StaffFormat(this.project_id, "autor_musica");
            this.StaffFormatEditorMontajista = new StaffFormat(this.project_id, "editor_montajista");
            this.StaffFormatCamarografo = new StaffFormat(this.project_id, "camarografo");
            this.StaffFormatMaquillador = new StaffFormat(this.project_id, "maquillador");
            this.StaffFormatVestuarista = new StaffFormat(this.project_id, "vestuarista");
            this.StaffFormatAmbientador = new StaffFormat(this.project_id, "ambientador");
            this.StaffFormatEncargadoCasting = new StaffFormat(this.project_id, "encargado_casting");
            this.StaffFormaScript = new StaffFormat(this.project_id, "script");
            this.StaffFormatSonidista = new StaffFormat(this.project_id, "sonidista");
            this.StaffFormatMezclador = new StaffFormat(this.project_id, "mezclador");

            for (int j = 0; j < numeroCargos; j++)
            {
                int k = j + 13;
                string text = "";
                if (k == 0)
                {
                    text = "aActor / Actriz Protagónico (a)";
                }
                else if (k == 1)
                {
                    text = "aActor / Actriz Protagónico (a)";
                }
                else if (k == 2)
                {
                    text = "aAutor del Guión o Adaptador";
                }
                else if (k == 3)
                {
                    text = "aAutor de la Música Original";
                }
                else if (k == 4)
                {
                    text = "aActor Secundario";
                }
                else if (k == 5)
                {
                    text = "aDirector de Fotografía";
                }
                else if (k == 6)
                {
                    text = "aDirector de Arte o Diseñador de Producción";
                }
                else if (k == 7)
                {
                    text = "aDiseñador de Vestuario";
                }
                else if (k == 8)
                {
                    text = "aSonidista";
                }
                else if (k == 9)
                {
                    text = "aMontajista";
                }
                else if (k == 10)
                {
                    text = "aDiseñador de Sonido, Editor de Sonido Jefe o Montajista de Sonido";
                }
                else if (k == 11)
                {
                    text = "aOperador de Cámara";
                }
                else if (k == 12)
                {
                    text = "aPrimer Asistente de Cámara o Foquista";
                }
                else if (k == 13)
                {
                    text = "aGaffer, Jefe de Eléctricos, Jefe de Luces o Jefe de Luminotécnicos";
                }
                else if (k == 14)
                {
                    text = "aMaquillador";
                }
                else if (k == 15)
                {
                    text = "aVestuarista";
                }
                else if (k == 16)
                {
                    text = "aAmbientador o Utilero ";
                }
                else if (k == 17)
                {
                    text = "aNombre Script (Continuista)";
                }
                else if (k == 18)
                {
                    text = "aAsistente de Dirección";
                }
                else if (k == 19)
                {
                    text = "aDirector de Casting ";
                }
                else if (k == 20)
                {
                    text = "aEfectos especiales en escena (SFX) ";
                }
                else if (k == 21)
                {
                    text = "aEfectos visuales (VFX / CGI) ";
                }
                else if (k == 22)
                {
                    text = "aColorista";
                }
                else if (k == 23)
                {
                    text = "aMicrofonista";
                }
                else if (k == 24)
                {
                    text = "aGrabador o Artista de Foley";
                }
                else if (k == 25)
                {
                    text = "aEditor de Diálogos o Efectos";
                }
                else if (k == 26)
                {
                    text = "aMezclador";
                }
                
                this.StaffFormatArreglo[j]=new StaffFormat(this.project_id, text);
            }
        }


        /**
        * 
        * 
        * */

        public void getAttachmentsByConsult(int Father_id,int producer_id = 0)
        {
            this.attachment = new List<ProjectAttachment>();

            Attachment attachmentObj = new Attachment();
            List<Attachment> attachmentList = new List<Attachment>();

            attachmentList = attachmentObj.GetAttachmentListByConsult(this, Father_id, producer_id);

            foreach (Attachment attachmentItem in attachmentList)
            {
                if (attachmentItem.partOfProject(this) != "")
                {
                    ProjectAttachment newProjectAttachment = new ProjectAttachment();
                    newProjectAttachment.LoadProjectAttachment(this.project_id, attachmentItem.attachment_id, attachmentItem.attachment_required);
                    this.attachment.Add(newProjectAttachment);
                }
            }

        }


        /* Este método permite consultar los adjuntos relacionados con un projecto en particular y los adjuntos que aplican
         * para el projecto así algunos no tengan la información */
  
        public void GetAttachments()
        {
            /* Se inicializa la lista de formatos */
            this.attachment = new List<ProjectAttachment>();

            Attachment attachmentObj = new Attachment();
            List<Attachment> attachmentList= new List<Attachment>();
            attachmentList = attachmentObj.GetAttachmentList(this);

            foreach (Attachment attachmentItem in attachmentList)
            {
                if (attachmentItem.partOfProject(this) != "")
                {
                    ProjectAttachment newProjectAttachment = new ProjectAttachment();
                    newProjectAttachment.LoadProjectAttachment(this.project_id, attachmentItem.attachment_id,attachmentItem.attachment_required);
                    this.attachment.Add(newProjectAttachment);
                }
            }
        }

        /* Metodo que permite validar la duración del proyecto en minutos y segundos */
        public bool ValidateProjectDuration()
        {
            bool result = false;

            if (this.project_type_id == 1 && this.project_duration > (70*60) && this.project_duration < (200*60))//Si es largometraje de mas de 70
            {
                result = true;
            }
            if (this.project_type_id == 2 && this.project_duration > (52 * 60) && this.project_duration < (200 * 60))//Si es largometraje de mas de 52
            {
                result = true;
            }
            else if (this.project_type_id == 3 && this.project_duration >= (7*60) && this.project_duration <= (69*60)) //Si es cortometraje
            {
                result = true;
            }
            else if (this.project_type_id == 4 && this.project_duration > (52 * 60)) { // largometraje para TV
                result = true;
            }
            return result;
        }

        /* Método que calcula el costo total del proyecto haciendo la suma de los costos de cada etapa */
        public long getTotalCost()
        {
            return this.project_total_cost_desarrollo + this.project_total_cost_posproduccion + this.project_total_cost_preproduccion
               + this.project_total_cost_produccion + this.project_total_cost_promotion;
        }



        public string agregarFormatoEmail(string contenido,string firma) 
        {
            string inicio = @"
<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
	<head>
		
		<meta content='text/html; charset=utf-8' http-equiv='Content-Type' />
		<style type='text/css'>
body{ margin:0px; padding:0px; width:100%; }		</style>
	</head>
	<body>
		<table align='center' bgcolor='#ebebeb' border='0' cellpadding='0' cellspacing='0' style='padding:0px; margin:0px;' width='100%'>
			<tbody>
				<tr>
					<td>
						<table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
							<tbody>
					
                                <tr>
									<td align='left' bgcolor='#ffffff' valign='top' style='padding:0 100px;font-family:Arial, Helvetica, sans-serif;font-size:12px;'>
									<p style='font-size:20px;font-weight:bold;'>Notificación del Trámite de Cinematografía </p>
									<p style='font-size:12px;'>";

            string pie = @"<br>


									</p>
                                </tr>
                            </tbody>
						</table>
                        
                        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
							<tbody>
								<tr>
									<td align='left' valign='top' bgcolor='ffffff' style='padding:18px 20px 10px 20px;color:#3d3d3d;font-family:Arial, Helvetica, sans-serif;font-weight:bold;font-size:14px;border-top:1px solid #afafaf'>
									Dirección de Audiovisuales, Cine y Medios Interactivos <br>
									Ministerio de Cultura<br>
									<a href='www.mincultura.gov.co'>www.mincultura.gov.co</a><br>
									Cra. 8 No 8-55  Bogotá, Colombia
                                    
<div style='display: block; float:right;margin-top:-150px !important;'>  
									  <img alt='' style='width: 120px; ' 
									  src='cid:EmbeddedContent_1'/>
                                    <img alt='' style='width: 120px;' 
									  src='cid:EmbeddedContent_2'/>
</div>
									
                                    
							  </tr>
                             
                                
							</tbody>
						</table>
			      <br />
						<table align='center' border='0' cellpadding='0' cellspacing='0' height='176' width='600'>
							<tbody>
								<tr>
									<td style='display:block; padding-bottom:15px; font-size:12px; font-family:Helvetica, Arial, sans-serif; color:#777777;' valign='top'>
										<div style='text-align: justify;'>
											" + firma+@"
											
											<br />
											<br />
											&nbsp;</div>
										
										<br />
										&nbsp;</td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
			</tbody>
		</table>
	</body>
</html>";

            return inicio+ contenido+pie;
        }

        /* Este método envia notificaciones por correo electrónico */
        public void sendMailNotification(string To, string subject, string body,HttpServerUtility server)
        {
            string to = To.Trim();
            //validamos que el email este en formato valido si no no lo intentamos enviar
            if (!Regex.IsMatch(to,
                @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {
                return;
            }
           
            String userName = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            String password = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];
            String correoCopia = System.Configuration.ConfigurationManager.AppSettings["SmtpCopy"];
            string ruta = server.MapPath("~/images/");

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.From = new MailAddress(userName);
            msg.Subject = subject;
            if (correoCopia != null && correoCopia.Trim() != string.Empty)
            {
                msg.Bcc.Add(correoCopia);
                msg.Bcc.Add("cgarzon@mincultura.gov.co");

            }
            var firma = this.LoadCorreo("firma"); ;
            msg.Body = agregarFormatoEmail(body,firma);
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.Normal;
            msg.ReplyToList.Add(msg.From);
            msg.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;

            msg.Body += "</br>Correo enviado a:"+To;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msg.Body, Encoding.UTF8,MediaTypeNames.Text.Html);
          
            string mediaType = MediaTypeNames.Image.Jpeg;
            LinkedResource img = new LinkedResource(ruta+@"\banner1.png", mediaType);
            img.ContentId = "EmbeddedContent_1";
            img.ContentType.MediaType = mediaType;
            img.TransferEncoding = TransferEncoding.Base64;
            img.ContentType.Name = img.ContentId;
            img.ContentLink = new Uri("cid:" + img.ContentId);

            LinkedResource img2 = new LinkedResource(ruta+@"\banner2.png", mediaType);
            img2.ContentId = "EmbeddedContent_2";
            img2.ContentType.MediaType = mediaType;
            img2.TransferEncoding = TransferEncoding.Base64;
            img2.ContentType.Name = img.ContentId;
            img2.ContentLink = new Uri("cid:" + img.ContentId);



            htmlView.LinkedResources.Add(img);
            htmlView.LinkedResources.Add(img2);
            msg.AlternateViews.Add(htmlView);


  
            SmtpClient cliente = new SmtpClient();
            cliente.Host = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"];
            string port = System.Configuration.ConfigurationManager.AppSettings["SmtpPort"];
            cliente.Port = int.Parse(port);
            cliente.EnableSsl = (System.Configuration.ConfigurationManager.AppSettings["SmtpEnabledSSL"].Trim() == "true");

            cliente.Credentials = new System.Net.NetworkCredential(userName, password);
            
            
            
            try
            {
                cliente.Send(msg);
            }catch(Exception ex){
            
            }
        }

        public void sendMailNotificationResolucion(string To, string subject, string body, HttpServerUtility server, List<string> adjuntos)
        {
            string to = To.Trim();
            //validamos que el email este en formato valido si no no lo intentamos enviar
            if (!Regex.IsMatch(to,
                @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {
                return;
            }

            String userName = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            String password = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];
            String correoCopia = System.Configuration.ConfigurationManager.AppSettings["SmtpCopy"];
            string ruta = server.MapPath("~/images/");

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.From = new MailAddress(userName);
            msg.Subject = subject;

            for (int k = 0; adjuntos != null && k < adjuntos.Count; k++)
            {
                msg.Attachments.Add(new System.Net.Mail.Attachment(adjuntos[k]));
            }
            if (correoCopia != null && correoCopia.Trim() != string.Empty)
            {
                msg.Bcc.Add(correoCopia);
                msg.Bcc.Add("cgarzon@mincultura.gov.co");

            }
            var firma = this.LoadCorreo("firma"); ;
            msg.Body = agregarFormatoEmail(body, firma);
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.Normal;
            msg.ReplyToList.Add(msg.From);
            msg.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;

            msg.Body += "</br>Correo enviado a:" + To;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msg.Body, Encoding.UTF8, MediaTypeNames.Text.Html);

            string mediaType = MediaTypeNames.Image.Jpeg;
            LinkedResource img = new LinkedResource(ruta + @"\banner1.png", mediaType);
            img.ContentId = "EmbeddedContent_1";
            img.ContentType.MediaType = mediaType;
            img.TransferEncoding = TransferEncoding.Base64;
            img.ContentType.Name = img.ContentId;
            img.ContentLink = new Uri("cid:" + img.ContentId);

            LinkedResource img2 = new LinkedResource(ruta + @"\banner2.png", mediaType);
            img2.ContentId = "EmbeddedContent_2";
            img2.ContentType.MediaType = mediaType;
            img2.TransferEncoding = TransferEncoding.Base64;
            img2.ContentType.Name = img.ContentId;
            img2.ContentLink = new Uri("cid:" + img.ContentId);



            htmlView.LinkedResources.Add(img);
            htmlView.LinkedResources.Add(img2);
            msg.AlternateViews.Add(htmlView);



            SmtpClient cliente = new SmtpClient();
            cliente.Host = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"];
            string port = System.Configuration.ConfigurationManager.AppSettings["SmtpPort"];
            cliente.Port = int.Parse(port);
            cliente.EnableSsl = (System.Configuration.ConfigurationManager.AppSettings["SmtpEnabledSSL"].Trim() == "true");

            cliente.Credentials = new System.Net.NetworkCredential(userName, password);
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            // cliente.UseDefaultCredentials = false;


            try
            {
                cliente.Send(msg);
            }
            catch(Exception e)
            {

            }
        }

        public bool validarExtensionAdjunto(string path,string formato)
        {
            if (formato != "xls")
            {
                if (System.IO.Path.GetExtension(path).ToUpper() == ".PDF")
                {
                    return true;
                }
            }
            else {
                if (System.IO.Path.GetExtension(path).ToUpper() == ".XLS" || System.IO.Path.GetExtension(path).ToUpper() == ".XLSX")
                {
                    return true;
                }
            }
            return false;
        }

        /* Método de validación de los formularios, el cual revisa la información registrada para el proyecto e indica si un formulario
         * en particular esta completo o incompleto. */
        public bool ValidateProjectSection(string section,bool emptyform = false)
        {
            if (this.project_id <= 0)
            {
                return false;
            }

            bool resultado = false;
            this.section_validation_result = "";
            int cont=0;
            int verifycont = 0;
            switch(section)
            {
                #region datos de proyecto
                case "DatosProyecto":
                    if(this.project_domestic_producer_qty == 0)
                    {
                        this.section_validation_result = "<li>No se han definido la cantidad de productores nacionales</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_foreign_producer_qty == 0 && this.production_type_id == 2)
                    {
                        this.section_validation_result = "<li>Cuando es coproducción debe definir la cantidad de productores extranjeros</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.production_type_id == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha definido el tipo de producci&oacute;n</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.cod_idioma == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha definido el Idioma principal </li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_type_id == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha definido el tipo de obra (largometraje, cortometraje, etc.)</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_genre_id == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha definido el tipo de obra (Animación, Ficción, etc.)</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_total_cost_desarrollo == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado el costo de la etapa de desarrollo.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_total_cost_preproduccion == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado el costo de la etapa de preproducci&oacute;n.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_total_cost_produccion == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado el costo de la etapa de producci&oacute;n.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_total_cost_posproduccion == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado el costo de la etapa de posproducci&oacute:n.</li>";
                        verifycont++;
                    }
                    //cont++;
                    //if (this.project_total_cost_promotion == 0)
                    //{
                    //    this.section_validation_result = this.section_validation_result + "<li>No se ha registrado el costo de la etapa de promoci&oacute;n.</li>";
                    //    verifycont++;
                    //}
                    cont++;
                    if (this.project_percentage.ToString() == "" || this.project_percentage > 100 || this.project_percentage <= 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>El porcentaje no es valido</li>";
                        verifycont++;
                    }

                    cont++;
                    //si es produccion debe tener minimo 51%
                    if (this.production_type_id == 1 && this.project_percentage <51)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>El porcentaje para producción debe ser mínimo 51%</li>";
                        verifycont++;
                    }
                    cont++;
                    //si es coproduccion debe tener minimo 20% maximo 99%
                    if (this.production_type_id == 2 && (this.project_percentage < 20 || this.project_percentage >= 100))
                    {
                        this.section_validation_result = this.section_validation_result + "<li>El porcentaje para coproducción debe ser mínimo 51% e inferior a 100%</li>";
                        verifycont++;
                    }


                    cont++;
                    if (this.project_synopsis == "")
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado la sinopsis de la obra.</li>";
                        verifycont++;
                    }

                    cont++;
                    //if (this.tiene_premio.HasValue == false)
                    //{
                    //    this.section_validation_result = this.section_validation_result + "<li>No se ha registrado si tiene premio.</li>";
                    //    verifycont++;
                    //}else {
                    //    if (this.tiene_premio.Value && this.premio.Trim() == string.Empty)
                    //    {
                    //        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado el premio.</li>";
                    //        verifycont++;
                    //    }
                    //}

                    if (this.tiene_reconocimiento == "" && this.version == 2 )
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado si tiene reconocimiento como proyecto nacional.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.tiene_estimulos == ""  && this.version == 2)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se ha registrado si tiene estimulos o premios.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.tiene_estimulos == "Si") {
                        string consultaEstimulos = @"select count(*) as cantidad from dboPrd.estimulo
                                             join dboPrd.tipo_estimulo on tipo_estimulo.id_tipo_estimulo = estimulo.id_tipo_estimulo
                                            where estimulo.project_id = " + this.project_id.ToString();
                        DB db = new DB();
                        DataSet dsEstimulos = db.Select(consultaEstimulos);
                        if ((int)dsEstimulos.Tables[0].Rows[0]["cantidad"] == 0) {
                            this.section_validation_result = this.section_validation_result + "<li>No ha registrado los estímulos o premios de financiación.</li>";
                            verifycont++;
                        }
                    }
                    cont++;
                    if (this.tiene_reconocimiento == "Si")
                    {                        
                        if (this.num_resolucion == string.Empty ||  this.num_resolucion == null || this.ano_resolucion == string.Empty || this.ano_resolucion == null)
                        {
                            this.section_validation_result = this.section_validation_result + "<li>No ha registrado los la fecha y año de resolución del reconocimiento como proyecto nacional.</li>";
                            verifycont++;
                        }
                    }
                    cont++;
                    if (this.project_recording_sites == "")
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se han registrado los sitios de grabaci&oacute;n.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (!this.ValidateProjectDuration())
                    {
                        this.section_validation_result = this.section_validation_result + "<li>La duraci&oacute;n de la obra registrada no es v&aacute;lida. </li>";
                        verifycont++;
                    }
                    cont++;

                    /***************************************************************************************************/
                    /*************************** Consulta los formatos de rojade seleccionados *************************/
                    int shootingFormatCounter = 0;
                    int exhibitionFormatCounter = 0;
                    bool validateProjectDevelopmentLabInfoField = false;
                    foreach (Format format in this.project_format)
                    {
                        if(format.format_type_id == 1)
                        {

                            if (format.format_project_detail == null || format.format_project_detail == string.Empty)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>Ingrese la especificación del formato de rodaje '"+format.format_name+"'</li>";
                                verifycont++;
                            }
                            shootingFormatCounter++;
                            string lastTwoChars = format.format_name.Substring(format.format_name.Length - 2);
                            if (lastTwoChars.Equals("mm"))
                            {
                                validateProjectDevelopmentLabInfoField = true;
                            }
                        }
                        else
                        {
                            exhibitionFormatCounter++;
                        }
                    }
                    

                    /*************************** Consulta los formatos de rojade seleccionados *************************/
                    /***************************************************************************************************/

                    if (shootingFormatCounter == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se han registrado formatos de rodaje de la obra.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (exhibitionFormatCounter == 0)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se han registrado formatos de exhibici&oacute;n de la obra.</li>";
                        verifycont++;
                    }
                    cont++;
                    if (this.project_development_lab_info == "" && validateProjectDevelopmentLabInfoField)
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se han registrado la información del laboratorio de revelado.</li>";
                        verifycont++;
                    }
                    if (validateProjectDevelopmentLabInfoField)
                    {
                        cont++;
                    }
                    if (this.project_preprint_store_info == "")
                    {
                        this.section_validation_result = this.section_validation_result + "<li>No se han registrado la información del lugar de almacenamiento de los elementos de tiraje.</li>";
                        verifycont++;
                    }
                    cont++;
                    foreach (ProjectAttachment item in this.attachment)
                    {
                        //Validamos que se trate de los adjuntos con id padre 7
                        //que son los correspondientes a esta seccion
                        if (item.attachment.attachment_father_id == 7)
                        {
                            /* Validamos si hay un adjunto que no esta cargado */
                            if (item.project_attachment_id <= 0)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se ha cargado el adjunto '" + item.attachment.attachment_name.ToString() + "'</li>";
                                verifycont++;
                            }
                            else if( !validarExtensionAdjunto(item.project_attachment_path,item.attachment.attachment_format)) {
                                this.section_validation_result = this.section_validation_result + "<li>Adjunto '" + item.attachment.attachment_name.ToString() + "' con extensión invalida.</li>";
                                verifycont++;
                            }
                            cont++;
                        }
                    }

                    /* Verifica si existe información faltante y según define el resultado que se retornará */
                    if (this.section_validation_result == "" && !emptyform)
                    {
                        resultado = true;
                    }
                    else if ((emptyform) && verifycont == cont)
                    {
                        return true;
                    }        
                    break;
                #endregion
                #region datos del productor
                case "DatosProductor":
                    if (this.producer.Count == 0)
                    {
                        if (this.section_validation_result == "" && !emptyform)
                        {
                            return false;
                        }
                        else if ((emptyform) && verifycont == cont)
                        {
                            return true;
                        }        
                    }
                    decimal porcentajeParticipacion = this.producer.Sum(x => x.participation_percentage);

                    foreach (Producer producer in this.producer)
                    {
                        if (producer.requester == 1)
                        {
                            if (producer.person_type_id == 0)
                            {
                                this.section_validation_result = "<li>No se ha definido la naturaleza de la persona, para el productor solicitante</li>";
                                verifycont++;
                            }
                            cont++;

                            if (porcentajeParticipacion != 100  && this.version == 2)
                            {
                                this.section_validation_result = "<li>La suma del porcentaje de participación de productores y coproductores debe ser del 100%. Actualmente es de "+ porcentajeParticipacion .ToString()+ "%</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.participation_percentage <= 0 || producer.participation_percentage > 100)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>El porcentaje del productor no es válido</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.producer_firstname == "")
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado los nombres del productor solicitante</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.producer_lastname == "")
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado los apellidos del productor solicitante</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.producer_identification_number == "" && producer.producer_type_id != 2)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado el n&uacute;mero de identificaci&oacute;n del solicitante</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.fecha_nacimiento is null && producer.person_type_id == 1)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se ha  registrado la fecha de nacimineto</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.producer_name == "" && producer.person_type_id== 2)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado el nombre de la empresa</li>";
                                verifycont++;
                            }
                            cont++;
                            if ((producer.producer_nit == "" || producer.producer_nit.Length < 9 ) && producer.person_type_id == 2 && producer.producer_type_id != 2)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>El nit de la empresa debe tener 9 digitos</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.identification_type_id == 0 && producer.person_type_id == 2)
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado el tipo de identificación del representante legal</li>";
                                verifycont++;
                            }
                            cont++;
                            if (producer.person_type_id == 1)
                            {
                                if ((producer.producer_localization_id == "" && producer.producer_country == "") ||  (producer.producer_country != "" && producer.producer_city == ""))
                                {
                                    this.section_validation_result = this.section_validation_result + "<li>No se han registrado la ubicaci&oacute;n del productor</li>";
                                    verifycont++;
                                }
                                cont++;
                            }
                            if ((producer.productor_localizacion_contacto_id == "" && producer.productor_pais_contacto == "") || (producer.productor_pais_contacto != "" && producer.productor_ciudad_contacto == ""))
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado la ubicaci&oacute;n de contacto del productor</li>";
                                verifycont++;
                            }
                            cont++;
                            //if (producer.producer_address == "" && producer.producer_type_id != 2)
                            //{
                            //    this.section_validation_result = this.section_validation_result + "<li>No se han registrado la direcci&oacute;n del productor</li>";
                            //    verifycont++;
                            //}
                            //cont++;
                            if (producer.producer_phone == "")
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado el n&uacute;mero telef&oacute;nico del productor</li>";
                                verifycont++;
                            }
                            //cont++;
                            //if (producer.producer_movil == "")
                            //{
                            //    this.section_validation_result = this.section_validation_result + "<li>No se han registrado el n&uacute;mero de celular del productor</li>";
                            //    verifycont++;
                            //}
                            cont++;
                            if (producer.producer_email == "")
                            {
                                this.section_validation_result = this.section_validation_result + "<li>No se han registrado el correo electr&oacute;nico del productor</li>";
                                verifycont++;
                            }
                            cont++;
                            Attachment adjuntoObj = new Attachment();
                            // cargamos los tipos de adjuntos
                            List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(this, 1, producer.producer_id);
                            int producer_id = 0;
                            // Recorremos los adjuntos
                            foreach(Attachment item in adjuntos){

                                // Verificamos que pertenescan a la pestaña datos productor
                                if (item.attachment_father_id == 1) {
                                    ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();
                                    // recorremos un objeto con los productores
                                   foreach (Producer firstProducer in this.producer){
                                       //Obtenemos el Identificador del productor principal
                                       if (firstProducer.requester == 1) {
                                           producer_id = firstProducer.producer_id;
                                       }
                                    }
                                    projectAttachmentCurrent.loadAttachmentByFhaterAndProducerId(this.project_id, item.attachment_id,producer_id);
                                    if (producer.producer_type_id == 1) {//1 es nacional
                                        // se verifica si exite un adjunto ya cargado
                                        if (projectAttachmentCurrent.project_attachment_id <= 0)
                                        {
                                            // Mensaje de error
                                            this.section_validation_result = this.section_validation_result + "<li>No se ha cargado el adjunto '" + item.attachment_description.ToString() + "'</li>";
                                            verifycont++;
                                        }
                                        else if (!validarExtensionAdjunto(projectAttachmentCurrent.project_attachment_path, projectAttachmentCurrent.attachment.attachment_format))
                                        {
                                            section_validation_result = section_validation_result + "<li>Adjunto '" + projectAttachmentCurrent.attachment.attachment_name.ToString() + "' con extensión invalida.</li>";
                                            verifycont++;
                                        }
                                    }

                                    cont++;
                                }
                            }

                            if (this.section_validation_result == "" && !emptyform)
                            {
                                resultado = true;
                            }
                            else if ((emptyform) && verifycont == cont)
                            {
                                return true;
                            }  
                        }
                    }
                    break;
                #endregion
                #region productores adicionales
                case "ProductoresAdicionales":

                    
                    //el tab de prodcuto tiene 1 porodcutor por defecto
                    int cantidadNacionalesDebeTener = this.project_domestic_producer_qty - 1;
                    int cantidadExtranjerosDebeTener = this.project_foreign_producer_qty;
//si es produccion y solo tiene un productor nacional no debe ingresar nada en este tab
                    if (this.production_type_id == 1 && this.project_domestic_producer_qty == 1)
                    {
                        resultado = true;
                        return !emptyform;
                    }
                    if (this.production_type_id == 1){
                        cantidadExtranjerosDebeTener = 0;
                    }

                    int cantidadProductoresCreados = this.producer.Count - 1;
                    if (cantidadProductoresCreados < (cantidadNacionalesDebeTener + cantidadExtranjerosDebeTener))
                    //if (this.producer.Count < ((this.project_domestic_producer_qty - 1) + this.project_foreign_producer_qty))
                    {
                        resultado = false;
                        this.section_validation_result = "<li>Existen coproductores sin ninguna informaci&oacute;n registrada .</li>";
                        verifycont++;
                    }
                    cont++;

                    //if (   this.producer.Count < ((this.project_domestic_producer_qty - 1) + this.project_foreign_producer_qty))
                    //{
                    //    this.section_validation_result = "<li>Existen productores adicionales sin ninguna informaci&oacute;n registrada .</li>";
                    //    verifycont++;
                    //}
                    //if (this.producer.Count == 0) {
                    //    verifycont++;
                    //}
                    cont++;
                    foreach (Producer producer in this.producer)
                    {
                        if (producer.requester == 0)
                        {
                            string local_validation_result = "";
                            if (producer.producer_id > 0)
                            {
                                if (producer.person_type_id == 0)
                                {
                                    local_validation_result = "<li>No se ha definido la naturaleza de la persona, para el productor solicitante</li>";
                                    verifycont++;
                                }
                                cont++;
                                if (producer.participation_percentage <=0 || producer.participation_percentage > 100)
                                {
                                    local_validation_result = local_validation_result + "<li>El porcentaje del productor no es válido</li>";
                                    verifycont++;
                                }
                                cont++;
                                if (producer.producer_firstname == "")
                                {
                                    local_validation_result = local_validation_result + "<li>No se han registrado los nombres del productor solicitante</li>";
                                    verifycont++;
                                }
                                cont++;
                                if (producer.producer_lastname == "")
                                {
                                    local_validation_result = local_validation_result + "<li>No se han registrado los apellidos del productor solicitante</li>";
                                    verifycont++;
                                }
                                cont++;                
                                if (producer.identification_type_id == 0 && producer.person_type_id == 2 )
                                {
                                    local_validation_result = local_validation_result + "<li>No se han registrado el tipo de identificación del representante legal</li>";
                                    verifycont++;
                                }
                                if (producer.producer_type_id == 1)
                                {
                                    if ((producer.producer_nit == "" || producer.producer_nit.Length < 9) && producer.person_type_id == 2)
                                    {
                                        local_validation_result = local_validation_result + "<li>El NIT debe tener 9 digitos</li>";
                                        verifycont++;
                                    }
                                }
                                cont++;
                                if (producer.person_type_id == 1)
                                {
                                    if ((producer.producer_localization_id == "" && producer.producer_country == "") || (producer.producer_country != "" && producer.producer_city == ""))
                                    {
                                        local_validation_result = local_validation_result + "<li>No se han registrado la ubicaci&oacute;n del productor</li>";
                                        verifycont++;
                                    }
                                    cont++;
                                }
                                
                                if ((producer.productor_localizacion_contacto_id == "" && producer.productor_pais_contacto == "") || (producer.productor_pais_contacto != "" && producer.productor_ciudad_contacto == ""))
                                {
                                    local_validation_result = local_validation_result + "<li>No se han registrado la ubicaci&oacute;n de contacto del productor</li>";
                                    verifycont++;
                                }
                                cont++;
                                //if (producer.producer_address == "" && producer.producer_type_id != 2)
                                //{
                                //    local_validation_result = local_validation_result + "<li>No se han registrado la direcci&oacute;n del productor</li>";
                                //    verifycont++;
                                //}
                                //cont++;
                                if (producer.producer_phone == "" && producer.producer_type_id != 2)
                                {
                                    local_validation_result = local_validation_result + "<li>No se han registrado el n&uacute;mero telef&oacute;nico del productor</li>";
                                    verifycont++;
                                }
                                //cont++;
                                //if (producer.producer_movil == "")
                                //{
                                //    local_validation_result = local_validation_result + "<li>No se han registrado el n&uacute;mero de celular del productor</li>";
                                //    verifycont++;
                                //}
                                cont++;
                                if (producer.producer_email == "")
                                {
                                    local_validation_result = local_validation_result + "<li>No se han registrado el correo electr&oacute;nico del productor</li>";
                                    verifycont++;
                                }
                                cont++;

                                bool typeProducer = true;
                                if (producer.producer_type_id == 1)
                                {
                                    typeProducer = true;
                                }
                                else if (producer.producer_type_id == 2)
                                {
                                    typeProducer = false;
                                }
                                Attachment adjuntoObj = new Attachment();
                                // cargamos los tipos de adjuntos
                                List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(this, 1, producer.producer_id, typeProducer);
                                int producer_id = 0;
                                

                                // Recorremos los adjuntos
                                foreach (Attachment item in adjuntos)
                                {

                                    // Verificamos que pertenescan a la pestaña datos productor
                                    if (item.attachment_father_id == 1)
                                    {
                                        ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();                                      
                                        
                                            //Obtenemos el Identificador del productor principal
                                            if (producer.requester != 1 && producer.producer_type_id == 1)//producer_type_idproducer_type_id 1 es nacional
                                            {                                            
                                                producer_id = producer.producer_id;
                                                projectAttachmentCurrent.loadAttachmentByFhaterAndProducerId(this.project_id, item.attachment_id, producer_id);
                                                // se verifica si exite un adjunto ya cargado
                                                string s = projectAttachmentCurrent.project_attachment_path.ToString();
                                                string[] split = s.Split("/".ToCharArray());
                                            
                                                if (projectAttachmentCurrent.project_attachment_id <= 0)
                                                {
                                                    // Mensaje de error
                                                    local_validation_result = local_validation_result + "<li>No se ha cargado el adjunto '" + item.attachment_description.ToString() + "'</li>";
                                                    verifycont++;
                                                }
                                                else if (!validarExtensionAdjunto(projectAttachmentCurrent.project_attachment_path, projectAttachmentCurrent.attachment.attachment_format))
                                                {
                                                    local_validation_result = local_validation_result + "<li>Adjunto '" + projectAttachmentCurrent.attachment.attachment_name.ToString() + "' con extensión invalida.</li>";
                                                    verifycont++;
                                                }
                                                cont++;
                                            }
                                        
                                        
                                    }
                                }
                                if (local_validation_result != "")
                                {
                                    string nombreCompuesto = "";
                                    if(producer.producer_firstname == "" && producer.producer_lastname == "")
                                    {
                                        nombreCompuesto = "Sin nombre";
                                    }
                                    else
                                    {
                                        nombreCompuesto = producer.producer_firstname +" "+ producer.producer_lastname;
                                    }

                                    this.section_validation_result = this.section_validation_result + "<li><h3>Productor adicional '" + nombreCompuesto + "'</h3></li><li><ul>" + local_validation_result + "</ul></li>";
                                    verifycont++;
                                }
                                cont++;
                            }
                        }
                    }
                    if (this.section_validation_result == "" && !emptyform)
                    {
                        resultado = true;
                    }
                    else if ((emptyform) && verifycont == cont)
                    {
                        return true;
                    } 
                    break;
                #endregion
                #region datos formato personal
                case "DatosFormatoPersonal":
                    List<StaffFormat> staff_formats_temp = new List<StaffFormat>();
                    staff_formats_temp.Add(this.StaffFormatDirector);
                    staff_formats_temp.Add(this.StaffFormatGuionista);
                    staff_formats_temp.Add(this.StaffFormatDirectorArte);
                    staff_formats_temp.Add(this.StaffFormatAutorMusica);
                    staff_formats_temp.Add(this.StaffFormatEditorMontajista);
                    staff_formats_temp.Add(this.StaffFormatCamarografo);
                    staff_formats_temp.Add(this.StaffFormatMaquillador);
                    staff_formats_temp.Add(this.StaffFormatVestuarista);
                    staff_formats_temp.Add(this.StaffFormatAmbientador);
                    staff_formats_temp.Add(this.StaffFormatEncargadoCasting);
                    staff_formats_temp.Add(this.StaffFormaScript);
                    staff_formats_temp.Add(this.StaffFormatSonidista);
                    staff_formats_temp.Add(this.StaffFormatMezclador);
                    for (int k = 0; k < numeroCargos; k++)
                    {
                        staff_formats_temp.Add(this.StaffFormatArreglo[k]);
                    }
                    foreach (StaffFormat staff_format in staff_formats_temp)
                    {
                        string cargo = "";
                        string local_validation_result = "";

                        switch(staff_format.staff_format_position)
                        {
                            case "director":
                                cargo = "Director";
                                break;
                            case "guionista":
                                cargo = "Guionista";
                                break;
                            case "director_fotografia":
                                cargo = "Director de Fotografía";
                                break;
                            case "director_arte":
                                cargo = "Director de Arte";
                                break;
                            case "autor_musica":
                                cargo = "Autor de la Música";
                                break;
                            case "editor_montajista":
                                cargo = "Editor / Montajista";
                                break;
                            case "camarografo":
                                cargo = "Camarógrafo";
                                break;
                            case "maquillador":
                                cargo = "Maquillador";
                                break;
                            case "vestuarista":
                                cargo = "Vestuarista";
                                break;
                            case "ambientador":
                                cargo = "Ambientador";
                                break;
                            case "encargado_casting":
                                cargo = "Encargado del Casting";
                                break;
                            case "script":
                                cargo = "Script";
                                break;
                            case "sonidista":
                                cargo = "Sonidista";
                                break;
                            case "mezclador":
                                cargo = "Mezclador";
                                break;
                            default:
                                break;
                        }
                        if (staff_format.staff_format_name == "")
                        {
                            //local_validation_result = local_validation_result + "<li>No se ha definido el nombre</li>";
                            verifycont++;
                        }
                        cont++;
                        if (staff_format.staff_format_identification_number == "")
                        {
                            //local_validation_result = local_validation_result + "<li>No se ha definido el número de identificación</li>";
                            verifycont++;
                        }
                        cont++;
                        //if (staff_format.staff_format_address == "")
                        //{
                        //    //local_validation_result = local_validation_result + "<li>No se ha definido la dirección</li>";
                        //    verifycont++;
                        //}
                        //cont++;
                        if (staff_format.staff_format_email == "")
                        {
                            //local_validation_result = local_validation_result + "<li>No se ha definido el correo electrónico</li>";
                            verifycont++;
                        }
                        cont++;
                        if (staff_format.staff_format_phone == "")
                        {
                            //local_validation_result = local_validation_result + "<li>No se ha definido el número telefónico</li>";
                            verifycont++;
                        }
                        cont++;
                        if (staff_format.staff_format_movil == "")
                        {
                            //local_validation_result = local_validation_result + "<li>No se ha definido el número de celular</li>";
                            verifycont++;
                        }
                        cont++;
                        if (staff_format.staff_format_fax == "")
                        {
                            //local_validation_result = local_validation_result + "<li>No se ha definido el número del fax</li>";
                            verifycont++;
                        }
                        cont++;

                        if (local_validation_result != "")
                        {
                            //this.section_validation_result = this.section_validation_result + "<li><h3>Formato de personal - '" + cargo + "'</h3></li><li><ul>" + local_validation_result + "</ul></li>";
                            verifycont++;
                        }
                        cont++;
                    }
                    /* Verifica si existe información faltante y según define el resultado que se retornará */
                    if (this.section_validation_result == "" && !emptyform)
                    {
                        resultado = true;
                    }
                    else if ((emptyform) && verifycont == cont)
                    {
                        return true;
                    } 
                    break;
#endregion
                #region datos de personal
                case "DatosPersonal": //Validación de todas las opciones de personal


                    if (this.production_type_id == 3) {
                        return true;
                        break;
                    }
                    else if (this.project_has_domestic_director < 0) //Valida si la solicitud ya tiene definido si tiene director colombiano o no
                    {
                        this.section_validation_result = "<li>No se ha definido si la obra tiene director nacional</li>";
                        verifycont++;
                    }
                    //else if (this.project_staff_option_id <= 0  ) //Si tiene director colombiano valida si ya tiene definida una opción del personal posible y no es financiera valida q tenga almenos un personal
                    //{
                    //    this.section_validation_result = this.section_validation_result + "No se ha definido la configuraci&oacute;n de personal para la obra";
                    //}
                    else //Si ya tiene seleccionada una de las opciones de personal disponible entonces se procede con la validación de los datos de cada persona regitrada
                    {
                        /* Creación de la variable que mantiene los identificadores del
                         * personal que ya se ha precargado en el dataset que pobla el
                           formulario */
                        List<Int32> preLoadedStaff = new List<Int32>();

                        /* Crea el objeto que gestiona la información del personal */
                        Staff staff = new Staff();
                        DataSet staffOptionDS = staff.getStaffOptions(this.project_type_id, this.production_type_id, 
                            this.project_genre_id, this.project_has_domestic_director, (int)this.project_percentage, 
                        this.project_personal_type);
                        //resulta que la opcion puede cambiar de acuerdo a lo que seleccionen en datos de la obra, la validacion
                        //no se debe hacer basado en lo que tiene seleccionado, si no basado en lo que deberia seleccionar en el combo
                        //de acuerdo a lo que selecciono en el primer tab
                        int opcion_personal = this.project_staff_option_id;

                        if (staffOptionDS.Tables[0].Rows.Count == 0)
                        {
                            this.section_validation_result = "Hay una problema con la opción de personal.";
                            verifycont++;
                        }
                        else
                        {
                            bool contiene = false;
                            for (int i = 0; i < staffOptionDS.Tables[0].Rows.Count; i++)
                            {
                                if (staffOptionDS.Tables[0].Rows[i]["staff_option_id"].ToString() == opcion_personal.ToString())
                                {
                                    contiene = true;
                                    break;
                                }
                            }

                            if (!contiene && staffOptionDS.Tables[0].Rows.Count == 1)
                            {
                                opcion_personal = int.Parse(staffOptionDS.Tables[0].Rows[0]["staff_option_id"].ToString());
                            }

                            if (!contiene && staffOptionDS.Tables[0].Rows.Count >1)
                            {
                                this.section_validation_result = "Hay una problema con la opción de personal, no hay una selección valida.";
                                verifycont++;
                            }

                        }


                 
                        /* Se obtiene el detalle de la opción de presonal seleccionada */
                        DataSet staffOptionDetailDS = staff.getStaffOptionDetail(opcion_personal, this.version);

                        if (staffOptionDetailDS.Tables.Count > 0)
                        {
                            /* Agrega las opciones al repetidor */
                            foreach (DataRow item in staffOptionDetailDS.Tables[0].Rows)
                            {
                                int personalQty = (int)item["staff_option_detail_quantity"];
                                for (int i = 0; i < personalQty; i++)
                                {
                                    string local_validation_result = "";

                                    Staff loadedStaff = new Staff();
                                    foreach (Staff staffItem in this.staff)
                                    {
                                        /* Si la persona ya fue cargada se salta el resto del ciclo */
                                        if (!preLoadedStaff.Contains(staffItem.project_staff_id))
                                        {
                                            int staffTopPosition = staffItem.getTopPosition();
                                            if (staffTopPosition == (int)item["position_id"])
                                            {
                                                loadedStaff = staffItem;
                                                preLoadedStaff.Add(staffItem.project_staff_id);
                                                break;
                                            }
                                        }
                                    }

                                    if (loadedStaff.project_staff_firstname == "" || loadedStaff.project_staff_firstname == null)
                                    {
                                        local_validation_result = local_validation_result + "<li>No se ha definido el nombre</li>";
                                        verifycont++;
                                    }
                                    cont++;
                                    if (loadedStaff.project_staff_lastname == "" || loadedStaff.project_staff_lastname == null)
                                    {
                                        local_validation_result = local_validation_result + "<li>No se ha definido el apellido</li>";
                                        verifycont++;
                                    }
                                    cont++;
                                    if (loadedStaff.project_staff_identification_number == "" || loadedStaff.project_staff_identification_number == null)
                                    {
                                        local_validation_result = local_validation_result + "<li>No se ha definido el n&uacute;mero de identificaci&oacute;n</li>";
                                        verifycont++;
                                    }
                                    cont++;


                                    if (this.version == 2)
                                    {
                                        if (loadedStaff.project_staff_localization_id == "" || loadedStaff.project_staff_localization_id == null)
                                        {
                                            local_validation_result = local_validation_result + "<li>No se ha definido la ciudad </li>";
                                            verifycont++;
                                        }
                                        cont++;

                                    }
                                    else
                                    {

                                        if (loadedStaff.project_staff_city == "" || loadedStaff.project_staff_city == null)
                                        {
                                            local_validation_result = local_validation_result + "<li>No se ha definido la ciudad</li>";
                                            verifycont++;
                                        }
                                        cont++;
                                        if (loadedStaff.project_staff_state == "" || loadedStaff.project_staff_state == null)
                                        {
                                            local_validation_result = local_validation_result + "<li>No se ha definido el estado o departamento</li>";
                                            verifycont++;
                                        }
                                        cont++;
                                    }

                                    if (this.version == 2)
                                    {
                                        if (loadedStaff.id_etnia == 0)
                                        {
                                            local_validation_result = local_validation_result + "<li>No se ha definido la etnia </li>";
                                            verifycont++;
                                        }
                                        cont++;
                                        if (loadedStaff.id_genero == 0)
                                        {
                                            local_validation_result = local_validation_result + "<li>No se ha definido el genero </li>";
                                            verifycont++;
                                        }
                                        cont++;

                                    }

                                    //if (loadedStaff.project_staff_address == "" || loadedStaff.project_staff_address == null)
                                    //{
                                    //    local_validation_result = local_validation_result + "<li>No se ha definido la direcci&oacute;n</li>";
                                    //    verifycont++;
                                    //}
                                    //cont++;
                                    if (loadedStaff.project_staff_phone == "" || loadedStaff.project_staff_phone == null)
                                    {
                                        local_validation_result = local_validation_result + "<li>No se ha definido el tel&eacute;fono</li>";
                                        verifycont++;
                                    }
                                    //cont++;
                                    //if (loadedStaff.project_staff_movil == "" || loadedStaff.project_staff_movil == null)
                                    //{
                                    //    local_validation_result = local_validation_result + "<li>No se ha definido el celular</li>";
                                    //    verifycont++;
                                    //}
                                    cont++;
                                    if (loadedStaff.project_staff_email == "" || loadedStaff.project_staff_email == null)
                                    {
                                        local_validation_result = local_validation_result + "<li>No se ha definido el correo electr&oacute;nico</li>";
                                        verifycont++;
                                    }
                                    cont++;
                                    Attachment adjuntoObj = new Attachment();
                                    // cargamos los tipos de adjuntos
                                    List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(this, 41, 0);
                                    if (this.version == 2)
                                    {
                                        adjuntos = adjuntoObj.GetAttachmentListByConsult(this, 63, 0);
                                    }
                                    foreach (Attachment current_item in adjuntos)
                                    {
                                        ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();
                                        // se verifica si exite un adjunto ya cargado
                                        // projectAttachmentCurrent.LoadPersonalProjectAttachment(this.project_id, current_item.attachment_id, i+1);

                                        projectAttachmentCurrent.LoadPersonalProjectAttachmentByProject_staff(this.project_id, current_item.attachment_id, loadedStaff.project_staff_id);
                                        if (projectAttachmentCurrent.project_attachment_id == 0)
                                        {
                                            local_validation_result += local_validation_result + "<li>Falta el adjunto " + current_item.attachment_description + "</li>";
                                            verifycont++;
                                        }
                                        else if (!validarExtensionAdjunto(projectAttachmentCurrent.project_attachment_path, projectAttachmentCurrent.attachment.attachment_format))
                                        {
                                            local_validation_result = local_validation_result + "<li>Adjunto '" + projectAttachmentCurrent.attachment.attachment_name.ToString() + "' con extensión invalida.</li>";
                                            verifycont++;
                                        }
                                        cont++;
                                    }
                                    /* Si la variable que almacena la validación del usuario actual no está vacia se registra el mensaje de advertencia
                                     * en la variable de resultado. */
                                    if (local_validation_result != "")
                                    {
                                        string showName = "Personal sin nombre registrado";
                                        if (loadedStaff.project_staff_firstname != "" || loadedStaff.project_staff_lastname != "")
                                        {
                                            showName = loadedStaff.project_staff_firstname + " " + loadedStaff.project_staff_lastname;
                                        }
                                        this.section_validation_result = this.section_validation_result + "<li><h3>" + showName + "</h3><ul>" + local_validation_result + "</ul></li>";
                                    }

                                }
                            }
                        }
                    }
                    cont++;
                    if (this.section_validation_result == "" && !emptyform)
                    {
                        resultado = true;
                    }
                    else if ((emptyform) && verifycont == cont)
                    {
                        return true;
                    } 
                    break;
                #endregion
                #region datos adjuntos
                case "DatosAdjuntos":
                    foreach (ProjectAttachment item in this.attachment)
                    {
                        /* Se verifica si ya hay un adjunto cargado se presenta el
                         * vínculo o de lo contrario solo se presenta el nombre */
                        if (item.project_attachment_id <= 0 && item.attachment.attachment_father_id > 0)
                        {
                            this.section_validation_result = this.section_validation_result + "<li>No se ha cargado el adjunto '"+ item.attachment.attachment_name.ToString() +"'</li>";
                        }
                    }
                    if (this.section_validation_result == "" )
                    {
                        resultado = true;
                    }
                    break;
                #endregion
                #region datos finalizacion
                case "DatosFinalizacion":
                    
                    
                    //Validamos cada una de las pestañas del proyecto para determinar 
                    //si falta un dato en alguna de ellas y de acuerdo a ello indicar
                    //la pestaña de finalizacion como completa o incompleta
                    if (this.ValidateProjectSection("DatosProyecto", emptyform) &&
                        this.ValidateProjectSection("DatosProductor", emptyform) &&
                        this.ValidateProjectSection("ProductoresAdicionales", emptyform) &&
                        this.ValidateProjectSection("DatosFormatoPersonal", emptyform) &&
                        this.ValidateProjectSection("DatosPersonal", emptyform))
                        resultado = true;
                        
                    break;
                default:
                    this.section_validation_result = "No se encontró el formulario indicado: " + section;
                    break;
#endregion
            }
            return resultado;
        }
        /**
         * 
         * 
         **/
        public bool validateNotInitForm(string section){
            return ValidateProjectSection(section,true);
        }
        
        /**
         * Metodo que realiza el proceso de pintar enlace o texto del adjunto con o 
         * sin checkbox de aprobacion de acuerdo a parametros recibidos
         */
        public string renderAttachments(int attachment_id = 0, int project_attachment_id = 0, string attachment_name = "", string project_attachment_path = "", bool showAdvancedForm = false, int project_attachment_approved = 0,string attachment_required ="",int project_state_id=0,int user_role = 0,string nombre_original="",string tooltip="")
        {
            //Inicializamos
            string item = "";

            /* Validamos si se indica si el checkbox se visualiza checkeado o no */
            string approved_attachment = (project_attachment_approved == 1)?"checked":"";

            /* Se verifica si ya hay un adjunto cargado se presenta el
             * vínculo o de lo contrario solo se presenta el nombre */
            string attachment_text = (project_attachment_id > 0) ? "<a class='showFlexPaper' title='" + ((tooltip==string.Empty)?nombre_original:tooltip) + "' target=\"_blank\" href=\"" + project_attachment_path + "\">" + attachment_name.ToString() + "</a>"
                 :attachment_name.ToString();
            if (tooltip.Trim() != string.Empty)
            {
                attachment_text = "<label title='" + tooltip + "'> " + attachment_text + "</label>";
            }
            /* Se verifica si es visible el formulario de administracion y se activa checkbox */
             string attachment_checkbox = (showAdvancedForm) ? "<input type=\"checkbox\" name=\"attachment_approved_" + attachment_id + "\" value=\"" + project_attachment_id + "\" " + approved_attachment + " class='form-checkbox'/> <input type='hidden' name='attachment_approved_id_" + attachment_id + "' value='"+project_attachment_id+"' />" : "";
            string spanrequired = (attachment_required == "required") ? "<span class=\"required_field_text\">*</span>" : "";
            string loadAttachment = ((user_role>1) || 
                (user_role <=1 && project_state_id == 1) ||
                (user_role <=1 && project_state_id == 5  && project_attachment_approved == 0)
                )?(
                    @"<div style='float:right;'>
                            <div id='div" + attachment_id + @"' class='upload_input field_input' />
                                <input type='file' name='file' id='FileUpload" + attachment_id + @"' style='display:none' />  
                                <input type='button' style='width:110px;height:30px;background-color:darkblue;color:white;' id='btnFileUploadText' value='Seleccionar' 
                                onclick='FileUpload" + attachment_id + @".click();' />
                            </div>
                         
                           <div class='progressbar' id='progressbar" + attachment_id + @"' style='width:100px;display:none;background-color:green;'>
                               <div>                    </div>
                            </div>
                      </div>
                    ") : "";


            item = "<div class='field_labelarchivo'><div id=\"name_" + attachment_id + "\">" + attachment_checkbox + attachment_text + "" + spanrequired + "</div></div>"
                   + loadAttachment
                   + "";
            if (project_attachment_id <= 0 || (((project_state_id == 5 && user_role <= 1) || (user_role > 1 && project_state_id > 4)) && project_attachment_approved != 1 && !showAdvancedForm))
            {
                item = "<div id='contenedorAdjunto' class='required_field' style='min-height:50px;'>" + item + "</div>";
            }
            else {
                item = "<div id='contenedorAdjunto' class='' style='min-height:50px;'>" + item + "</div>";
            }
            return item;
        }
        
        public string renderAttachmentsPersonal(int attachment_id = 0, int project_attachment_id = 0, string attachment_name = "", string project_attachment_path = "", bool showAdvancedForm = false, int project_attachment_approved = 0, string attachment_required = "", int index = 0, int project_state_id = 0, int user_role = 0,string nombre_original="")
        {
            //Inicializamos
            string item = "";

            /* Validamos si se indica si el checkbox se visualiza checkeado o no */
            string approved_attachment = (project_attachment_approved == 1) ? "checked" : "";

            /* Se verifica si ya hay un adjunto cargado se presenta el
             * vínculo o de lo contrario solo se presenta el nombre */
            string attachment_text = (project_attachment_id > 0) ? "<a class='showFlexPaper' title='" + nombre_original + "' target=\"_blank\" href=\"" + project_attachment_path + "\">" + attachment_name.ToString() + "</a> " 
                : attachment_name.ToString();

            attachment_text = "<label title=''> " + attachment_text + "</label>";


            /* Se verifica si es visible el formulario de administracion y se activa checkbox */
            string attachment_checkbox = (showAdvancedForm) ? "<input type=\"checkbox\" name=\"attachment_approved_" + attachment_id +"_"+ index + "\" value=\"" + project_attachment_id + "\" " + approved_attachment + " class='form-checkbox'/>" : "";
            string spanrequired = (attachment_required == "required") ? "<span class=\"required_field_text\">*</span>" : "<span class=\"required_field_text\">*</span>";
            string loadAttachment = (
                (user_role <= 1 && (project_state_id <= 1 ))//cuando es productor y esta creando
                ||
                (user_role <= 1 && project_state_id == 5 && project_attachment_approved==0) 
                || user_role > 1
                //cuando es prodcutor esta en solicitud de aclaraciones y el adjunto no es aprovado
                ////|| quitamos esta condifiucon por que el que debe subir el adjunto es el productor
                ////(user_role > 1 && project_attachment_approved == 0 && (project_state_id != 5 && project_state_id >1))
                //////cuando es direccion y el adjunto no esta aprobado y esta en los estados 
                ) ? (
                    @"<div style='float:right;'>
                            <div id='div" + attachment_id + @"' class='upload_input field_input' />
                                <input type='file' name='file' id='FileUpload" + attachment_id +"_"+index+ @"' style='display:none' />  
                                <input type='button' style='width:110px;height:30px;background-color:darkblue;color:white;' id='btnFileUploadText' value='Seleccionar' 
                                onclick='FileUpload" + attachment_id + "_" + index  +@".click();' />
                            </div>
                         
                           <div class='progressbar' id='progressbar" + attachment_id + "_" + index + @"' style='width:100px;display:none;'>
                               <div>                    </div>
                            </div>
                      </div>
                    "):
          


               // ("<div class='upload_input field_input'><div id='FileUpload" + attachment_id + "_" + index + "' ></div>"):
                "";

            item = "<div class='field_labelarchivo'><div id=\"name_" + attachment_id + "_" + index + "\">" + attachment_checkbox + attachment_text + spanrequired + "</div></div>"
                   + loadAttachment
                   + " ";

            if (project_attachment_id <= 0 || (((project_state_id == 5 && user_role<=1) || (user_role>1 && project_state_id>4)) && project_attachment_approved != 1 && !showAdvancedForm))
            {
                item = "<div id='contenedorAdjunto' class='required_field' style='min-height:50px;'>" + item + "</div>";
            }else{
                item = "<div id='contenedorAdjunto2' class='' style='min-height:50px;'>" + item + "</div>";
            }

            return item;
        }
     }
}