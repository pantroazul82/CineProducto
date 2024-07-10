﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using CineProducto.Bussines;
using System.Globalization;
using DominioCineProducto;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DominioCineProducto.Data;
using DominioCineProducto.utils;

namespace CineProducto
{
    public partial class Finalizacion : System.Web.UI.Page
    {
        public int project_state = 0; //Variable que controla la presentación del formulario de administración
        public bool showSendButton = true;
        public bool showAclaraciones = true;
        public bool showDescargarButton = true;
        public bool showAtachhForm = true;
        public bool showDeleteFileButton = false;
        public bool showFileDownload = false;
        public bool showAdvancedForm = false; //Variable que controla la presentación del formulario de administración

        public string tab_datos_productor_css_class = "";
        public string tab_datos_proyecto_css_class = "";
        public string tab_productores_adicionales_css_class = "";
        public string tab_datos_formato_personal_css_class = "";
        public string tab_datos_personal_css_class = "";
        public string tab_datos_adjuntos_css_class = "";
        public string tab_datos_finalizacion_css_class = "";
        public string path_request_form = "";
        public string path_hojaTransferencia = "";

        public int user_role = 0;
        public int project_id;
        public bool previsualizar_solicitud_aclaraciones_permission;
        public bool acciones_finales;
        public bool pasar_solicitud_a_editor_permission;
        public bool deshacer_envio_solicitud_permission;
        public bool programar_cita_visualizacion_permission;
        public bool ver_hoja_de_control_permission;
        public bool registrar_fecha_de_resolucion_permission;
        public bool registrar_fecha_de_notificacion_permission;
        public bool cargar_archivo_de_resolucion_permission;
        public bool descargar_archivo_de_resolucion_permission = false;

        public string tab_datos_proyecto_revision_mark_image = "";
        public string tab_datos_productor_revision_mark_image = "";
        public string tab_datos_productores_adicionales_revision_mark_image = "";
        public string tab_datos_formato_personal_revision_mark_image = "";
        public string tab_datos_personal_revision_mark_image = "";
        public string tab_datos_adjuntos_revision_mark_image = "";
        protected void generarNuevoPDF(object sender, EventArgs e)
        {
            Project project = new Project();
            project.LoadProject(this.project_id);

            clsGeneracionPDF objgenerar = new clsGeneracionPDF();
            List<parametroRemplazo> listaReemplazar = new List<parametroRemplazo>();
            string direccion = "";
            string telefono = "";
            string celular = "";
            string email = "";
            string fax = "";
            string website = "";
            string municipio = "";
            string departamento = "";
            string departamento_optional = "";
            string municipio_optional = "";

            //DATOS DE EL PRODUCTOR
            foreach (Producer producer in project.producer)
            {

                if (producer.requester == 1)
                {
                    direccion = producer.producer_address;
                    telefono = producer.producer_phone;
                    celular = producer.producer_movil;
                    email = producer.producer_email;
                    website = producer.producer_website;
                    fax = producer.producer_fax;
                    municipio = producer.productor_localizacion_contacto_id;
                    departamento = producer.productor_localizacion_contacto_id_padre;
                    departamento_optional = producer.productor_pais_contacto;
                    municipio_optional = producer.productor_ciudad_contacto;
                }
                if (producer.producer_type_id == 1)
                {
                    if (producer.person_type_id == 1)
                    {
                        string PRIMER_NOMBRE = "";
                        string SEGUNDO_NOMBRE = "";
                        string PRIMER_APELLIDO = "";
                        string SEGUNDO_APELLIDO = "";

                        PRIMER_NOMBRE = producer.producer_firstname.Trim();
                        if (!string.IsNullOrEmpty(producer.producer_firstname2.Trim()))
                        {
                            SEGUNDO_NOMBRE = producer.producer_firstname2.Trim();
                        }
                        PRIMER_APELLIDO = producer.producer_lastname.Trim();
                        if (!string.IsNullOrEmpty(producer.producer_lastname2.Trim()))
                        {
                            SEGUNDO_APELLIDO = producer.producer_lastname2.Trim();
                        }

                        listaReemplazar.Add(new parametroRemplazo("@@NOMBRE_APELLIDOS", PRIMER_NOMBRE + " " + SEGUNDO_NOMBRE + " " + PRIMER_APELLIDO + " " + SEGUNDO_APELLIDO));
                        listaReemplazar.Add(new parametroRemplazo("@@NUMERO_DOC", producer.producer_identification_number));
                        listaReemplazar.Add(new parametroRemplazo("@@TD:", "CC"));
                        listaReemplazar.Add(new parametroRemplazo("@@NOM_REP_LEGAL", ""));
                        listaReemplazar.Add(new parametroRemplazo("@@NUM_DOC_REP", ""));
                        listaReemplazar.Add(new parametroRemplazo("@@TDR", ""));
                        listaReemplazar.Add(new parametroRemplazo("@@RAZON_SOCIAL", ""));
                        listaReemplazar.Add(new parametroRemplazo("@@NUM_NIT", ""));
                    }
                    else if (producer.person_type_id == 2)
                    {
                        string PRIMER_NOMBRE_REP_LEGAL = "";
                        string SEGUNDO_NOMBRE_REP_LEGAL = "";
                        string PRIMER_APELLIDO_REP_LEGAL = "";
                        string SEGUNDO_APELLIDO_REP_LEGAL = "";
                        listaReemplazar.Add(new parametroRemplazo("@@NOMBRE_APELLIDOS", ""));
                        listaReemplazar.Add(new parametroRemplazo("@@NUMERO_DOC", ""));
                        listaReemplazar.Add(new parametroRemplazo("@@TD:", ""));
                        PRIMER_NOMBRE_REP_LEGAL = producer.producer_firstname.Trim();
                        if (!string.IsNullOrEmpty(producer.producer_firstname2.Trim()))
                        {
                            SEGUNDO_NOMBRE_REP_LEGAL = producer.producer_firstname2.Trim();
                        }
                        PRIMER_APELLIDO_REP_LEGAL = producer.producer_lastname.Trim();
                        if (!string.IsNullOrEmpty(producer.producer_lastname2.Trim()))
                        {
                            SEGUNDO_APELLIDO_REP_LEGAL = producer.producer_lastname2.Trim();
                        }
                        listaReemplazar.Add(new parametroRemplazo("@@NOM_REP_LEGAL", PRIMER_NOMBRE_REP_LEGAL + " " + SEGUNDO_NOMBRE_REP_LEGAL + " " + PRIMER_APELLIDO_REP_LEGAL + " " + SEGUNDO_APELLIDO_REP_LEGAL));
                        listaReemplazar.Add(new parametroRemplazo("@@NUM_DOC_REP", producer.producer_identification_number));
                        if (producer.identification_type_id == 1)
                        {
                            listaReemplazar.Add(new parametroRemplazo("@@TDR", "CC"));
                        }
                        else if (producer.identification_type_id == 2)
                        {
                            listaReemplazar.Add(new parametroRemplazo("@@TDR", "CE"));
                        }
                        listaReemplazar.Add(new parametroRemplazo("@@RAZON_SOCIAL", (producer.producer_name != "") ? producer.producer_name : ""));
                        listaReemplazar.Add(new parametroRemplazo("@@NUM_NIT", "NIT: " + ((producer.producer_nit != "") ? producer.producer_nit : "")));
                    }
                }
            }
            DB db = new DB();
            if (municipio != "" && departamento != "")
            {

                DataSet ds = db.Select("SELECT localization_name "
                                     + "FROM dboPrd.localization "
                                     + "WHERE localization_id ='" + municipio
                                     + "' OR localization_id = '" + departamento + "'");
                if (ds.Tables[0].Rows.Count > 1)
                {
                    // Se consulta el nombre del tipo de producción, tipo de proyecto y genero 
                    departamento = ds.Tables[0].Rows[0]["localization_name"].ToString();
                    municipio = ds.Tables[0].Rows[1]["localization_name"].ToString();
                }
            }
            else
            {
                departamento = departamento_optional;
                municipio = municipio_optional;
            }

            //DATOS DE CONTACTO
            listaReemplazar.Add(new parametroRemplazo("@@DIRECCION", direccion));
            listaReemplazar.Add(new parametroRemplazo("@@DEPTO", departamento));
            listaReemplazar.Add(new parametroRemplazo("@@CIUDAD", municipio));
            listaReemplazar.Add(new parametroRemplazo("@@CORREO", email));
            listaReemplazar.Add(new parametroRemplazo("@@CEL", telefono));
            listaReemplazar.Add(new parametroRemplazo("@@SITIO_WEB", website));

            //DATOS DE LA OBRA
            #region datos de la obra
            string genre = "";

            if (project.project_genre_id == 1)
            {
                genre = "Ficción";
            }
            else if (project.project_genre_id == 2)
            {
                genre = "Documental";
            }
            else if (project.project_genre_id == 3)
            {
                genre = "Animación";
            }
            int start_day = project.project_filming_start_date.Day;
            int start_month = project.project_filming_start_date.Month;
            int start_year = project.project_filming_start_date.Year;
            int end_day = project.project_filming_end_date.Day;
            int end_month = project.project_filming_end_date.Month;
            int end_year = project.project_filming_end_date.Year;
            string preprint = (project.project_preprint_store_info == "") ? "No aplica" : project.project_preprint_store_info;


            string duration = project.GetProjectDuration("minutes").ToString().PadLeft(2, '0') + ":" + (project.GetProjectDuration("seconds").ToString().PadLeft(2, '0'));
            long presupuestoTotal = project.project_total_cost_desarrollo + project.project_total_cost_posproduccion + project.project_total_cost_preproduccion + project.project_total_cost_produccion;

            listaReemplazar.Add(new parametroRemplazo("@@TITULO_OBRA", project.project_name));
            listaReemplazar.Add(new parametroRemplazo("@@GENERO", genre));
            listaReemplazar.Add(new parametroRemplazo("@@TIPO", project.production_type_name));
            listaReemplazar.Add(new parametroRemplazo("@@TIP_DUR", project.project_type_name));
            listaReemplazar.Add(new parametroRemplazo("@@DUR_MIN", duration));
            listaReemplazar.Add(new parametroRemplazo("@@FECHA_INI", start_day + "/" + start_month + "/" + start_year));
            listaReemplazar.Add(new parametroRemplazo("@@FECHA_FIN", end_day + "/" + end_month + "/" + end_year));
            listaReemplazar.Add(new parametroRemplazo("@@LUGAR_FILMACION", project.project_recording_sites));
            listaReemplazar.Add(new parametroRemplazo("@@COSTO_TOTAL", presupuestoTotal.ToString("C0")));
            listaReemplazar.Add(new parametroRemplazo("@@DEPOSITO_FISICO", preprint));
            listaReemplazar.Add(new parametroRemplazo("@@SINOPSIS", project.project_synopsis));
            #endregion
            objgenerar.generarFormularioProducto(Server, Page, listaReemplazar);
        }
        private void cargarResolucion()
        {
            Resolution resolution = new Resolution();
            resolution.LoadByProject((int)Session["project_id"]);

            fileResolucion.Visible = false;//
            lnkResolucion.Visible = false;
            btnEliminarArchivoResolucion.Visible = false;
            btnCargarResolution.Visible = false;

            int estadoProyecto = this.project_state;

            if (resolution.path != null && resolution.path.Trim() != string.Empty)
            {
                lnkResolucion.ToolTip = resolution.path;
                lnkResolucion.Visible = true;
                if ((estadoProyecto == 9 || estadoProyecto == 10) && this.cargar_archivo_de_resolucion_permission)//this.user_role == 4)
                {//solo puede eliminar la resolucion en aprobada o rechazada
                    //si es director
                    btnEliminarArchivoResolucion.Visible = true;
                }
            }

            if ((estadoProyecto == 9 || estadoProyecto == 10) && this.cargar_archivo_de_resolucion_permission)//this.user_role == 4)
            {//solo puede eliminar la resolucion en aprobada o rechazada
                //si es director
                fileResolucion.Visible = true;
                btnCargarResolution.Visible = true;
            }

            //si es productor solo puede ver la resolucion  a partir de la fecha permitida
            if (this.user_role <= 1)
            {
                if (notification_date.Value.Trim() == string.Empty)
                {
                    lnkResolucion.Visible = false;
                }
                else
                {
                    int ano = int.Parse(notification_date.Value.Split('-')[0]);
                    int mes = int.Parse(notification_date.Value.Split('-')[1]);
                    int dia = int.Parse(notification_date.Value.Split('-')[2]);
                    DateTime fecha = new DateTime(ano, mes, dia);
                    if (fecha > DateTime.Now)
                    {
                        lnkResolucion.Visible = false;
                    }
                }
            }
            //si el estado es mayor a 4 y el formulario esta aprbado no deberia adjuntarlo nuevamente
           

            pnlResolucion.Visible = lnkResolucion.Visible || btnCargarResolution.Visible; 
        }

        private void cargarResolucion2()
        {
            Resolution resolution = new Resolution();
            resolution.LoadByProject((int)Session["project_id"]);

            fileResolucion2.Visible = false;//
            lnkResolucion2.Visible = false;
            btnEliminarArchivoResolucion2.Visible = false;
            btnCargarResolution2.Visible = false;

            int estadoProyecto = this.project_state;

            if (resolution.path2 != null && resolution.path2.Trim() != string.Empty)
            {
                lnkResolucion2.ToolTip = resolution.path;
                lnkResolucion2.Visible = true;
                if ((estadoProyecto == 9 || estadoProyecto == 10) && this.cargar_archivo_de_resolucion_permission)//this.user_role == 4)
                {//solo puede eliminar la resolucion en aprobada o rechazada
                    //si es director
                    btnEliminarArchivoResolucion2.Visible = true;
                }
            }

            if ((estadoProyecto == 9 ) && this.cargar_archivo_de_resolucion_permission)//this.user_role == 4)
            {//solo puede eliminar la resolucion en aprobada 
                //si es director
                fileResolucion2.Visible = true;
                btnCargarResolution2.Visible = true;
            }

            //si es productor solo puede ver la resolucion  a partir de la fecha permitida
            if (this.user_role <= 1)
            {
                if (notification_date2.Value.Trim() == string.Empty)
                {
                    lnkResolucion2.Visible = false;
                }
                else
                {
                    int ano = int.Parse(notification_date2.Value.Split('-')[0]);
                    int mes = int.Parse(notification_date2.Value.Split('-')[1]);
                    int dia = int.Parse(notification_date2.Value.Split('-')[2]);
                    DateTime fecha = new DateTime(ano, mes, dia);
                    if (fecha > DateTime.Now)
                    {
                        lnkResolucion2.Visible = false;
                    }
                }
            }
            //si el estado es mayor a 4 y el formulario esta aprbado no deberia adjuntarlo nuevamente


            pnlResolucion2.Visible = lnkResolucion2.Visible || btnCargarResolution2.Visible;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            var controlMaster = (System.Web.UI.HtmlControls.HtmlInputHidden)Master.FindControl("_scrollboton");
            controlMaster.Value = false.ToString();
            lblErrorEnviar.Text = "";
            lblErrorAprobacion.Text = "";
            /* Valida si hay un usuario en sesión de lo contrarió redirecciona a la 
            * página principal */
            if (Session["user_id"] == null || Convert.ToInt32(Session["user_id"]) <= 0)
            {
                Response.Redirect("Default.aspx", true);
            }

            /* Valida si el usuario tiene permiso de ver el formulario de administración */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(Session["user_id"]);
                if (userObj.checkPermission("ver-formulario-gestion-solicitud"))
                {
                    this.showAdvancedForm = true;
                }
                this.user_role = userObj.GetUserRole(userObj.user_id);
                this.previsualizar_solicitud_aclaraciones_permission = userObj.checkPermission("previsualizar-solicitud-aclaraciones");
                this.acciones_finales = userObj.checkPermission("aprobar-rechazar-cancelar-solicitud");
                //el editor en el inicio tenia la opcion de las acciones finales por solcitud de Jmutis se quitan las 
                //acciones finales al editor
                if (this.acciones_finales && user_role == 3)
                {
                    this.acciones_finales = false;
                }

                this.pasar_solicitud_a_editor_permission = userObj.checkPermission("pasar-solicitud-a-editor");
                this.deshacer_envio_solicitud_permission = userObj.checkPermission("deshacer-envio-solicitud");
                this.programar_cita_visualizacion_permission = userObj.checkPermission("programar-cita-visualizacion");
                this.ver_hoja_de_control_permission = userObj.checkPermission("ver-hoja-de-control");
                this.registrar_fecha_de_resolucion_permission = userObj.checkPermission("registrar-fecha-de-resolucion");
                this.registrar_fecha_de_notificacion_permission = userObj.checkPermission("registrar-fecha-de-notificacion");
                this.cargar_archivo_de_resolucion_permission = userObj.checkPermission("cargar-archivo-resolucion");
                this.descargar_archivo_de_resolucion_permission = userObj.checkPermission("descargar-archivo-resolucion");
            }

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Crea el objeto que gestiona la información del personal */
            //Staff staff = new Staff();

            /* Define la region */
            CultureInfo culture = new CultureInfo("es-CO");

            /* Crea el objeto del proyecto para manejar la información */
            Project project = new Project();

            /* Obtiene la información del proyecto seleccionado */
            if(lblCodProyecto.Text.Trim() != string.Empty)
            {
                Session["project_id"] = Convert.ToInt32(lblCodProyecto.Text);
            }else if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }

            if (Session["project_id"] != null)
            {
                if (bool.Parse(Session["ES_PRODUCTOR"].ToString()))
                {
                    this.FSHojaTransferencia.Visible = false;
                }
                    lblCodProyecto.Text = Session["project_id"].ToString();
                project.LoadProject(Convert.ToInt32(Session["project_id"]));

                if (project.version == 2)//redirecciona a finalizacion nueva
                {
                    Response.Redirect("Finalizacion2.aspx", true);
                }
                //si es largo no deberia mostrar este panel
                if (project.project_type_id ==3)
                {
                    pnlLargos.Visible = false;
                    pnlLargos2.Visible = false;
                }else {
                    pnlLargos.Visible = true;
                    pnlLargos2.Visible = true;
                }

                #region verificamos permisos
                if (Session["user_id"] == null || Convert.ToInt32(Session["user_id"]) <= 0)
                {
                    Response.Redirect("Default.aspx", true);
                }
                else
                {
                    if (user_role > 1)
                    {
                        if (project.state_id <= 1)
                        {
                            Response.Redirect("Default.aspx", true);
                        }
                    }
                    else
                    {
                        if (project.project_idusuario != Convert.ToInt32(Session["user_id"]))
                        {
                            Response.Redirect("Default.aspx", true);
                        }
                    }
                }
                #endregion

                this.project_id = Convert.ToInt32(Session["project_id"]);
                this.project_state = project.state_id;
                
                if (user_role <= 1)
                {
                    pnlAclaraciones.Visible = false;
                    
                }
                if (project_state == 1 && user_role <=1)
                {
                    pnlMensajeVisible.Visible = true;
                    if (project.project_type_id == 3)
                    {
                        lblCorto.Visible = true;
                        lblLargo.Visible = false;
                    }
                    else {
                        lblCorto.Visible = false;
                        lblLargo.Visible = true;
                    }
                }

                if (project_state > 1)
                {
                    comentarios_adicionales.ReadOnly = true;
                }

                if (this.user_role <= 1 && (project.state_id == 9 || project.state_id == 10) && (project.project_notification_date > DateTime.Now || project.project_notification_date.Year <=1 ))
                {
                    labelestadoproyecto.Text = "Estado actual de la solicitud: POR NOTIFICAR" ;
                }else{
                    labelestadoproyecto.Text = "Estado actual de la solicitud: " + project.state_name;
                }

                if (this.user_role <= 1)
                {
                    if ((project.state_id == 1 || project.state_id == 5))
                    {
                        pnlAdicionarOtrosAdjuntos.Visible = true;
                    }
                    else { pnlAdicionarOtrosAdjuntos.Visible = false; }
                }

                if (this.user_role > 1)
                {
                    pnlAclaraciones.Visible = true;
                    if ((project.state_id != 1 && project.state_id != 5 && project.state_id < 9))
                    {
                        pnlAdicionarOtrosAdjuntos.Visible = true;
                        btnGuardarTExtoAdicional.Visible = true;
                        txtComplementoCartaAclaraciones.ReadOnly = false;
                        txtSustitutoCartaAclaraciones.ReadOnly = false;
                    }
                    else
                    {
                        pnlAdicionarOtrosAdjuntos.Visible = false;
                        btnGuardarTExtoAdicional.Visible = false;
                        txtComplementoCartaAclaraciones.ReadOnly = true;
                        txtSustitutoCartaAclaraciones.ReadOnly = true;
                    }
                }

                #region calculo de tabs
                /* Si el proyecto aun esta en su primera etapa (estado creado - 1) se define el estilo
                 * de la pestaña de acuerdo al resultado de la validación del diligenciamiento de los
                 * campos de los formularios. */
                if (project.state_id == 1)
                {
                    project.sectionDatosProyecto.tab_state_id = 1;
                    project.sectionDatosProductor.tab_state_id = 1;
                    project.sectionDatosProductoresAdicionales.tab_state_id = 1;
                    project.sectionDatosFormatoPersonal.tab_state_id = 1;
                    project.sectionDatosPersonal.tab_state_id = 1;
                    project.sectionDatosAdjuntos.tab_state_id = 1;
                    project.sectionDatosFinalizacion.tab_state_id = 1;
                }else if (project.state_id == 2)
                {
                    if (project.sectionDatosProyecto.tab_state_id != 10 && project.sectionDatosProyecto.tab_state_id != 9)
                    {
                        project.sectionDatosProyecto.tab_state_id = 11;
                    }
                    if (project.sectionDatosProductor.tab_state_id != 10 && project.sectionDatosProductor.tab_state_id != 9)
                    {
                        project.sectionDatosProductor.tab_state_id = 11;
                    }
                    if (project.sectionDatosProductoresAdicionales.tab_state_id != 10 && project.sectionDatosProductoresAdicionales.tab_state_id != 9)
                    {
                        project.sectionDatosProductoresAdicionales.tab_state_id = 11;
                    }
                    if (project.sectionDatosFormatoPersonal.tab_state_id != 10 && project.sectionDatosFormatoPersonal.tab_state_id != 9)
                    {
                        project.sectionDatosFormatoPersonal.tab_state_id = 11;
                    }
                    if (project.sectionDatosPersonal.tab_state_id != 10 && project.sectionDatosPersonal.tab_state_id != 9)
                    {
                        project.sectionDatosPersonal.tab_state_id = 11;
                    }
                    if (project.sectionDatosAdjuntos.tab_state_id != 10 && project.sectionDatosAdjuntos.tab_state_id != 9)
                    {
                        project.sectionDatosAdjuntos.tab_state_id = 11;
                    }
                    if (project.sectionDatosFinalizacion.tab_state_id != 10 && project.sectionDatosFinalizacion.tab_state_id != 9)
                    {
                        project.sectionDatosFinalizacion.tab_state_id = 11;
                    }
                }

              

                /* Se identifica y define el estilo de la pestaña a presentar para cada formulario de acuerdo
                 * a su estado actual */
                bool emtyform = project.validateNotInitForm("DatosProyecto");
                switch (project.sectionDatosProyecto.tab_state_id) /* Datos del proyecto */
                {
                    case 10:
                        tab_datos_proyecto_css_class = "tab_incompleto_inactive";
                        break;
                    case 11:
                        tab_datos_proyecto_css_class = "tab_unmarked_inactive";
                        break;
                    case 9:
                        tab_datos_proyecto_css_class = "tab_completo_inactive";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_datos_proyecto_css_class = (project.ValidateProjectSection("DatosProyecto")) ? "tab_completo_inactive" : "tab_incompleto_inactive";
                        }
                        else
                        {
                            tab_datos_proyecto_css_class = "tab_unmarked_inactive";
                        }
                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state > 1 && project_state != 5)
                    {
                        tab_datos_proyecto_css_class = "tab_unmarked_inactive";
                    }
                }
                emtyform = project.validateNotInitForm("DatosProductor");
                switch (project.sectionDatosProductor.tab_state_id) /* Datos del productor */
                {
                    case 10:
                        tab_datos_productor_css_class = "tab_incompleto_inactive";
                        break;
                    case 11:
                        tab_datos_productor_css_class = "tab_unmarked_inactive";
                        break;
                    case 9:
                        tab_datos_productor_css_class = "tab_completo_inactive";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_datos_productor_css_class = (project.ValidateProjectSection("DatosProductor")) ? "tab_completo_inactive" : "tab_incompleto_inactive";
                        }
                        else
                        {
                            tab_datos_productor_css_class = "tab_unmarked_inactive";
                        }
                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state > 1 && project_state != 5)
                    {
                        tab_datos_productor_css_class = "tab_unmarked_inactive";
                    }
                }
                emtyform = project.validateNotInitForm("ProductoresAdicionales");
                switch (project.sectionDatosProductoresAdicionales.tab_state_id) /* Datos de los productores adicionales */
                {
                    case 10:
                        tab_productores_adicionales_css_class = "tab_incompleto_inactive";
                        break;
                    case 11:
                        tab_productores_adicionales_css_class = "tab_unmarked_inactive";
                        break;
                    case 9:
                        tab_productores_adicionales_css_class = "tab_completo_inactive";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_productores_adicionales_css_class = (project.ValidateProjectSection("ProductoresAdicionales")) ? "tab_completo_inactive" : "tab_incompleto_inactive";
                        }
                        else
                        {
                            tab_productores_adicionales_css_class = "tab_unmarked_inactive";
                        }

                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state > 1 && project_state != 5)
                    {
                        tab_productores_adicionales_css_class = "tab_unmarked_inactive";
                    }
                }
                if (
                   project.production_type_id == 0
                   ||
                   (project.production_type_id == 1 && project.project_domestic_producer_qty <= 1)//si es produccion y solo tiene 1 prodcuctor o menos no deberia mostrar
                   ||
                   (project.production_type_id == 2 && project.project_foreign_producer_qty <= 0 && project.project_domestic_producer_qty <= 1)
                   )
                {
                    tab_productores_adicionales_css_class = "tab_hide";
                }
                /* Verifica si el proyecto es de tipo largometraje */
                emtyform = project.validateNotInitForm("DatosFormatoPersonal");
                if (project.project_type_id == 1 || project.project_type_id == 2)
                {
                    switch (project.sectionDatosFormatoPersonal.tab_state_id) /* Datos de los productores adicionales */
                    {
                        case 10:
                            tab_datos_formato_personal_css_class = "tab_incompleto_inactive";
                            break;
                        case 11:
                            tab_datos_formato_personal_css_class = "tab_unmarked_inactive";
                            break;
                        case 9:
                            tab_datos_formato_personal_css_class = "tab_completo_inactive";
                            break;
                        default:
                            if (!emtyform)
                            {
                                tab_datos_formato_personal_css_class = (project.ValidateProjectSection("DatosFormatoPersonal")) ? "tab_completo_inactive" : "tab_incompleto_inactive";
                            }
                            else
                            {
                                tab_datos_formato_personal_css_class = "tab_unmarked_inactive";
                            }
                            break;
                    }
                    if (user_role <= 1)
                    {
                        if (project_state > 1 && project_state != 5)
                        {
                            tab_datos_formato_personal_css_class = "tab_unmarked_inactive";
                        }
                    }
                }
                else
                {
                    tab_datos_formato_personal_css_class = "tab_hide";
                }
                //SE PONE SIEMPRE OCULTA ESTRE TAB SOICITUD JMUTIS 01/02/2017
                tab_datos_formato_personal_css_class = "tab_hide";
                emtyform = project.validateNotInitForm("DatosPersonal");
                switch (project.sectionDatosPersonal.tab_state_id) /* Datos del personal */
                {
                    case 10:
                        tab_datos_personal_css_class = "tab_incompleto_inactive";
                        break;
                    case 11:
                        tab_datos_personal_css_class = "tab_unmarked_inactive";
                        break;
                    case 9:
                        tab_datos_personal_css_class = "tab_completo_inactive";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_datos_personal_css_class = (project.ValidateProjectSection("DatosPersonal")) ? "tab_completo_inactive" : "tab_incompleto_inactive";
                        }
                        else
                        {
                            tab_datos_personal_css_class = "tab_unmarked_inactive";
                        }
                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state > 1 && project_state != 5)
                    {
                        tab_datos_personal_css_class = "tab_unmarked_inactive";
                    }
                }
                emtyform = project.validateNotInitForm("DatosAdjuntos");
                switch (project.sectionDatosAdjuntos.tab_state_id) /* Archivos adjuntos */
                {
                    case 10:
                        tab_datos_adjuntos_css_class = "tab_incompleto_inactive";
                        break;
                    case 11:
                        tab_datos_adjuntos_css_class = "tab_unmarked_inactive";
                        break;
                    case 9:
                        tab_datos_adjuntos_css_class = "tab_completo_inactive";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_datos_adjuntos_css_class = (project.ValidateProjectSection("DatosAdjuntos")) ? "tab_completo_inactive" : "tab_incompleto_inactive";
                        }
                        else
                        {
                            tab_datos_adjuntos_css_class = "tab_unmarked_inactive";
                        }
                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state > 1 && project_state != 5)
                    {
                        tab_datos_adjuntos_css_class = "tab_unmarked_inactive";
                    }
                }
                emtyform = project.validateNotInitForm("DatosFinalizacion");
                RequestForm form = new RequestForm(this.project_id);
                this.path_request_form = form.path;
                
                //hoja de transferencia
                HojaTransferencia formhoja = new HojaTransferencia(this.project_id);
                this.path_hojaTransferencia = formhoja.path;

                if (form.path == null || form.path.Trim() == string.Empty)
                {
                    project.sectionDatosFinalizacion.tab_state_id = 10;
                }
                switch (project.sectionDatosFinalizacion.tab_state_id) /* Datos finalizacion */
                {
                    case 10:
                        tab_datos_finalizacion_css_class = "tab_incompleto_active";
                        break;
                    case 11:
                        tab_datos_finalizacion_css_class = "tab_unmarked_active";
                        break;
                    case 9:
                        tab_datos_finalizacion_css_class = "tab_completo_active";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_datos_finalizacion_css_class = (project.ValidateProjectSection("DatosFinalizacion")) ? "tab_completo_active" : "tab_incompleto_active";
                        }
                        else
                        {
                            tab_datos_finalizacion_css_class = "tab_unmarked_active";
                        }
                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state > 1 && project_state != 5)
                    {
                        tab_datos_finalizacion_css_class = "tab_unmarked_inactive";
                    }
                }
                if (this.showAdvancedForm)
                {
                    switch (project.sectionDatosProyecto.revision_mark)
                    {
                        case "revisado":
                            tab_datos_proyecto_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/error.png\">";
                            break;
                        case "aprobado":
                            tab_datos_proyecto_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/aprobado.png\">";
                            break;
                        default:
                            break;
                    }
                    switch (project.sectionDatosProductor.revision_mark)
                    {
                        case "revisado":
                            tab_datos_productor_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/error.png\">";
                            break;
                        case "aprobado":
                            tab_datos_productor_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/aprobado.png\">";
                            break;
                        default:
                            break;
                    }
                    switch (project.sectionDatosProductoresAdicionales.revision_mark)
                    {
                        case "revisado":
                            tab_datos_productores_adicionales_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/error.png\">";
                            break;
                        case "aprobado":
                            tab_datos_productores_adicionales_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/aprobado.png\">";
                            break;
                        default:
                            break;
                    }
                    switch (project.sectionDatosFormatoPersonal.revision_mark)
                    {
                        case "revisado":
                            tab_datos_formato_personal_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/error.png\">";
                            break;
                        case "aprobado":
                            tab_datos_formato_personal_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/aprobado.png\">";
                            break;
                        default:
                            break;
                    }
                    switch (project.sectionDatosPersonal.revision_mark)
                    {
                        case "revisado":
                            tab_datos_personal_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/error.png\">";
                            break;
                        case "aprobado":
                            tab_datos_personal_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/aprobado.png\">";
                            break;
                        default:
                            break;
                    }
                    switch (project.sectionDatosAdjuntos.revision_mark)
                    {
                        case "revisado":
                            tab_datos_adjuntos_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/error.png\">";
                            break;
                        case "aprobado":
                            tab_datos_adjuntos_revision_mark_image = "<img style=\"width:14px;padding:0 0 0 5px;\" src=\"images/aprobado.png\">";
                            break;
                        default:
                            break;
                    }
                }

            }
            else
            {
                Response.Redirect("Lista.aspx", true);
            }

            /**************************************************/
            /* Se define la información del bloque contextual */
            nombre_proyecto.Text = (project.project_name.ToString() != "") ? project.project_name.ToString() : "Aún no se ha definido el nombre de la obra";
            tipo_produccion.Text = project.production_type_name.ToString();
            tipo_proyecto.Text = project.project_type_name.ToString();
            /* Buscamos el objeto del productor que hace la solicitud */
            int requesterProducer = project.producer.FindIndex(
                delegate(Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
            if (requesterProducer != -1)
            {
                if (project.producer[requesterProducer].person_type_id == 2)
                    nombre_productor.Text = project.producer[requesterProducer].producer_name;
                else
                    nombre_productor.Text = project.producer[requesterProducer].producer_firstname.Trim() + " " + project.producer[requesterProducer].producer_lastname.Trim();


                if (nombre_productor.Text.Trim() == string.Empty)
                {
                    nombre_productor.Text = project.producer[requesterProducer].producer_firstname.Trim() + " " + project.producer[requesterProducer].producer_lastname.Trim();
                }

                if (nombre_productor.Text.Trim() == string.Empty)
                {
                    nombre_productor.Text = project.producer[requesterProducer].producer_email.Trim();
                }
            }
            opciones_adicionales.Text = "<a href =\"Lista.aspx\"><< Volver al listado de solicitudes</a>";
#endregion

            #region eventos de los botones
            /**************************************************/
            /* Se hace el procesamiento del formulario enviado */
            if (Request.Form["cancelarsolicitud1_field"] == "1")
            {
                #region cancelar solicitud
               // if (project.state_id == 8) //Para enviar aclaracones el proyecto debe estar en el estado 6 (Aclaraciones enviadas)
                if (project.state_id == 4 || project.state_id == 6 || project.state_id == 7 || project.state_id == 8)
                {
                    project.fecha_cancelacion = DateTime.Now;
                    project.state_id = 12; //Pasa a estado 12 (Cancelada)
                    if (project.Save(true))
                    {
                        /* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                        string mailTo = "";
                        string producerName = "";

                        /* Buscamos el objeto del productor que hace la solicitud */
                        int RequesterProducerTemp = project.producer.FindIndex(
                        delegate(Producer producerObj)
                        {
                            return producerObj.requester == 1;
                        });
                        if (RequesterProducerTemp != -1)
                        {
                            mailTo = project.producer[RequesterProducerTemp].producer_email;
                            producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                        }
                        if (mailTo != "")
                        {
                            string cuerpo = "Ha sido cancelada su solicitud del Trámite de Solicitud de Reconocimiento Como Obra Nacional del Ministerio de Cultura.";
                            string subject = System.Configuration.ConfigurationManager.AppSettings["CANCELED_MAIL_SUBJECT"];
                            string body = System.Configuration.ConfigurationManager.AppSettings["CANCELED_MAIL_BODY_SALUDO"] +"<br>"+ producerName;
                            body = body + "<br/><br/>" + cuerpo;// System.Configuration.ConfigurationManager.AppSettings["CANCELED_BODY_MENSAJE"];                            
                            body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["CANCELED_MAIL_BODY_FIN"];

                            /* Envío de notificación al productor solicitante */
                           // project.sendMailNotification(mailTo, subject, body);
                        }

                        Response.Redirect("Lista.aspx", true);
                    }

                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["rechazarsolicitud1_field"] == "1")
            {
                #region rechazar solicitud
                if (project.state_id >= 4 && project.state_id !=5 && project.state_id <= 8) //Para enviar aclaracones el proyecto debe estar en el estado 6 (Aclaraciones enviadas)
                {
                    project.state_id = 10; //Pasa a estado 10 (Rechazada)

                    BD.dsCine ds = new BD.dsCine();
                    BD.dsCineTableAdapters.firma_tramiteTableAdapter firma = new BD.dsCineTableAdapters.firma_tramiteTableAdapter();
                    firma.FillByActivo(ds.firma_tramite);
                    if(ds.firma_tramite.Count<= 0)
                    {//si no hay ningun activo 
                        firma.Fill(ds.firma_tramite);
                    }
                    project.cod_firma_tramite = ds.firma_tramite[0].cod_firma_tramite;
                    if (project.Save(true))
                    {
                        ///* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                        //string mailTo = "";
                        //string producerName = "";

                        ///* Buscamos el objeto del productor que hace la solicitud */
                        //int RequesterProducerTemp = project.producer.FindIndex(
                        //delegate(Producer producerObj)
                        //{
                        //    return producerObj.requester == 1;
                        //});
                        //if (RequesterProducerTemp != -1)
                        //{
                        //    mailTo = project.producer[RequesterProducerTemp].producer_email;
                        //    producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                        //}
                        //if (mailTo != "")
                        //{
                        //    string subject = System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_SUBJECT"];
                        //    string body = System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_SALUDO"] +"<br>"+ producerName;
                        //    body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_MENSAJE"];
                        //    body += "<br>" + System.Configuration.ConfigurationManager.AppSettings["NOTIFICATION_SECOND_PARAGRAPH"];
                        //    body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_FIN"];

                        //    /* Envío de notificación al productor solicitante */
                        //    project.sendMailNotification(mailTo, subject, body);
                        //}

                        Response.Redirect("Lista.aspx", true);
                    }

                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["aprobarsolicitud1_field"] == "1")
            {
                #region aprobar solicitud
                lblErrorAprobacion.Text = "";
                //validamos si es posible aprobar en la primera ronda
                //validamos que este adjunto el formulario
                RequestForm form = new RequestForm(this.project_id);
                if (form.path == null || form.path.Trim() == string.Empty)
                {
                    lblErrorAprobacion.Text = "Antes de aprobar la solicitud, debe cargar el formulario de solicitud firmado.";
                } else
                    if (//validamos que el formulario este aprobado
                    (project.state_id < 5 && (!project.formulario_aprobado_pronda.HasValue
                     || !project.formulario_aprobado_pronda.Value))
                    ||
                    (project.state_id > 5 
                    && (project.formulario_aprobado_pronda.HasValue && !project.formulario_aprobado_pronda.Value) 
                    && (!project.formulario_aprobado_sronda.HasValue || !project.formulario_aprobado_sronda.Value))
                    )
                    {
                        lblErrorAprobacion.Text = "Antes de aprobar la solicitud, debe aprobar el formulario de solicitud firmado.";
                    }

                //validamos que la visualizacion este aprobada
                    else
                        if (//validamos que el formulario este aprobado
                        (project.state_id < 5 && (!project.aprueba_visualizacion_proyecto_pronda.HasValue
                         || !project.aprueba_visualizacion_proyecto_pronda.Value))
                        ||
                        (project.state_id > 5 
                        && (project.aprueba_visualizacion_proyecto_pronda.HasValue && !project.aprueba_visualizacion_proyecto_pronda.Value)
                        && (!project.aprueba_visualizacion_proyecto_sronda.HasValue || !project.aprueba_visualizacion_proyecto_sronda.Value))
                        )
                        {
                            lblErrorAprobacion.Text = "Las observaciones de visualización no han sido aprobadas.";
                        }else if ( project.state_id <8 && (
        tab_datos_productor_css_class == "tab_incompleto_inactive" ||
        tab_datos_proyecto_css_class  == "tab_incompleto_inactive" ||
        tab_productores_adicionales_css_class  == "tab_incompleto_inactive" ||
        tab_datos_formato_personal_css_class  == "tab_incompleto_inactive" ||
        tab_datos_personal_css_class  == "tab_incompleto_inactive" ||
        tab_datos_adjuntos_css_class  == "tab_incompleto_inactive" ||
        tab_datos_finalizacion_css_class  == "tab_incompleto_inactive" ||
        path_request_form  == "tab_incompleto_inactive")
                  )
                {
                    string pestanas = "";
                    if (tab_datos_proyecto_css_class == "tab_incompleto_inactive")
                    {
                        pestanas += "Datos de la obra, ";
                    }
                    if (tab_datos_productor_css_class == "tab_incompleto_inactive")
                    {
                        pestanas += "Datos del productor, ";
                    }
                    if (tab_productores_adicionales_css_class == "revisado")
                    {
                        pestanas += "coproductores, ";
                    }
                    if (tab_datos_formato_personal_css_class == "revisado")
                    {
                        pestanas += " Formato Personal, ";
                    }
                    if (tab_datos_personal_css_class == "revisado")
                    {
                        pestanas += "Personal, ";
                    }
                    if (tab_datos_adjuntos_css_class == "revisado")
                    {
                        pestanas += "Adjuntos, ";
                    }
                    pestanas = pestanas.Substring(0, pestanas.Length - 2);
                    lblErrorAprobacion.Text = "<br>No puede aprobar el proyecto, hay pestañas pendientes. (" + pestanas + ")";

                }
                //validamos si es posible aprobar en la segunda ronda
                else if(
                    project.sectionDatosProyecto.revision_mark == "revisado" ||
project.sectionDatosProductoresAdicionales.revision_mark == "revisado" ||
project.sectionDatosProductor.revision_mark == "revisado" ||
project.sectionDatosFormatoPersonal.revision_mark == "revisado" ||
project.sectionDatosPersonal.revision_mark == "revisado" ||
project.sectionDatosAdjuntos.revision_mark == "revisado" 
                    )
                {


                    string pestanas = "";
                    if(project.sectionDatosProyecto.revision_mark == "revisado"){
                        pestanas += "Datos de la obra, ";
                    }
                    if (project.sectionDatosProductor.revision_mark == "revisado")
                    {
                        pestanas += "Datos del productor, ";
                    }
                    if (project.sectionDatosProductoresAdicionales.revision_mark == "revisado")
                    {
                        pestanas += "Coproductores, ";
                    }
                    if (project.sectionDatosFormatoPersonal.revision_mark == "revisado")
                    {
                        pestanas += " Formato Personal, ";
                    }
                    if (project.sectionDatosPersonal.revision_mark == "revisado")
                    {
                        pestanas += "Personal, ";
                    }
                    if (project.sectionDatosAdjuntos.revision_mark == "revisado")
                    {
                        pestanas += "Adjuntos, ";
                    }
                    pestanas = pestanas.Substring(0, pestanas.Length - 2);
                    lblErrorAprobacion.Text = "<br>No puede aprobar el proyecto, hay pestañas pendientes. ("+pestanas+")";
                
                }else
                if (project.state_id == 4 || project.state_id == 6 || project.state_id == 7 || project.state_id == 8) //Para enviar aclaracones el proyecto debe estar en el estado 6 (Aclaraciones enviadas)
                {
                    #region actualizamos la aprobacion
                    BD.dsCine ds = new BD.dsCine();
                    BD.dsCineTableAdapters.firma_tramiteTableAdapter firma = new BD.dsCineTableAdapters.firma_tramiteTableAdapter();
                    firma.FillByActivo(ds.firma_tramite);
                    if (ds.firma_tramite.Count <= 0)
                    {//si no hay ningun activo 
                        firma.Fill(ds.firma_tramite);
                    }
                    project.cod_firma_tramite = ds.firma_tramite[0].cod_firma_tramite;
                    project.state_id = 9; //Pasa a estado 9 (Aprobada)
                    if (project.Save())
                    {
                        ///* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                        //string mailTo = "";
                        //string producerName = "";

                        ///* Buscamos el objeto del productor que hace la solicitud */
                        //int RequesterProducerTemp = project.producer.FindIndex(
                        //delegate(Producer producerObj)
                        //{
                        //    return producerObj.requester == 1;
                        //});
                        //if (RequesterProducerTemp != -1)
                        //{
                        //    mailTo = project.producer[RequesterProducerTemp].producer_email;
                        //    producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                        //}
                        //if (mailTo != "")
                        //{
                        //    string subject = System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_SUBJECT"];
                        //    string body = System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_BODY_SALUDO"] +"<BR/><BR/>"+ producerName;
                        //    body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_BODY_MENSAJE"];
                        //    body += "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["NOTIFICATION_SECOND_PARAGRAPH"];
                        //    body = body + "<br/><br/><FONT  color='red' >" + System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_BODY_FIN"]+"</FONT>";

                        //    /* Envío de notificación al productor solicitante */
                        //    project.sendMailNotification(mailTo, subject, body);
                        //}

                        Response.Redirect("Finalizacion.aspx", true);
                    }

                    Response.Redirect("Finalizacion.aspx", true);
                    #endregion
                }
#endregion
            }
            if (Request.Form["enviaraclaraciones_field"] == "1")
            {
                lblErrorEnvirAclaraciones.Text = "";
                lblErrorEnvirAclaraciones.Text = "Por reestructuración Técnica actualmente no está habilitada la plataforma para él envió de su trámite. \r\nMientras se habilita podrá realizar modificaciones a su información y/o adjuntos. Le recomendamos validar que se encuentren todos los adjuntos de su solicitud cargados. \r\nLe estaremos notificando una vez se reestablezca el servicio en su totalidad para el envío de su solicitud.";
                return;
                //PANDEMIA
                //cuando el productor responde las aclaraciones
                #region enviar aclaraciones
                //el productor debio poner observaciones en los campos que le pidieron aclaraciones
                string respuesta ="";
                if (project.sectionDatosProyecto.revision_state_id == 10 && project.sectionDatosProyecto.aclaraciones_productor == string.Empty)
                {
                    respuesta += "<br>No ingreso las aclaraciones para datos de la obra";
                }
                if (project.sectionDatosProductor.revision_state_id == 10 && project.sectionDatosProductor.aclaraciones_productor == string.Empty)
                {
                    respuesta += "<br>No ingreso las aclaraciones para datos del productor";
                }

                //si produccion y tiene mas de  prodcutor nacional o si coproduccion
                if ((project.production_type_id == 1 && project.project_domestic_producer_qty > 1) || project.production_type_id == 2)
                {
                    if (project.sectionDatosProductoresAdicionales.revision_state_id == 10 && project.sectionDatosProductoresAdicionales.aclaraciones_productor == string.Empty)
                    {
                        respuesta += "<br>No ingreso las aclaraciones para coproductores";
                    }
                }
                if (project.sectionDatosPersonal.revision_state_id == 10 && project.sectionDatosPersonal.aclaraciones_productor == string.Empty)
                {
                    respuesta += "<br>No ingreso las aclaraciones para datos de personal";
                }
                if (project.sectionDatosFormatoPersonal.revision_state_id == 10 && project.sectionDatosFormatoPersonal.aclaraciones_productor == string.Empty)
                {
                    respuesta += "<br>No ingreso las aclaraciones para listado de personal";
                }
              
                if (respuesta != string.Empty)
                {
                    lblErrorEnvirAclaraciones.Text = respuesta;
                    return;
           
                }


                lblErrorEnvirAclaraciones.Text = "";
                /* Acciones de persistencia de acuerdo al estado actual */
                RequestForm form = new RequestForm(this.project_id);
                if (form.path == null || form.path.Trim() == string.Empty)
                {
                    lblErrorEnvirAclaraciones.Text = "Antes de enviar el proyecto, debe cargar el formulario de solicitud firmado.";
                }else
                    //validamos si no fue aprobado el resultado de la visualizacion debe agregar observaciones
                    if (
                        //solo debe validar esto si es corto
                        project.project_type_id != 3 &&
                        (project.observaciones_visualizacion_por_productor == null || project.observaciones_visualizacion_por_productor.Trim() == string.Empty)
                        && (project.aprueba_visualizacion_proyecto_pronda.HasValue && !project.aprueba_visualizacion_proyecto_pronda.Value))
                    {
                        lblErrorEnvirAclaraciones.Text = "Antes de enviar el proyecto, debe ingresar la Respuesta del Resultado de la visualización.";
                    }else
                if (project.state_id == 5) //Para enviar aclaracones el proyecto debe estar en el estado 5 (Aclaraciones solicitadas)
                {
                    project.project_clarification_response_date = DateTime.Now; /* Se define la fecha del momento del envío de aclaraciones usando la fecha del servidor de aplicación */
                    project.state_id = 6; //Pasa a estado 5 (Aclaraciones solicitadas)
                    if (project.Save(true))
                    {

                        //actualiza la fecha limite
                        NegocioCineProducto negCineP = new NegocioCineProducto();                        
                        negCineP.ActualizarFechaLimite(project.project_id, DateTime.Now);

                        /* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                        string mailTo = "";
                        string administratorMail = System.Configuration.ConfigurationManager.AppSettings["NOTIFICATIONS_MAIL"];
                        string producerName = "";

                        /* Buscamos el objeto del productor que hace la solicitud */
                        int RequesterProducerTemp = project.producer.FindIndex(
                        delegate(Producer producerObj)
                        {
                            return producerObj.requester == 1;
                        });
                        if (RequesterProducerTemp != -1)
                        {
                            mailTo = project.producer[RequesterProducerTemp].producer_email;
                            producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                        }
                        if (mailTo != "")
                        {
                            string subject = System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_MAIL_SUBJECT"]+" - "+producerName.Trim()+" - "+project.project_name.Trim();
                            string body = System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_MAIL_BODY_SALUDO"] + producerName;
                            body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_MAIL_BODY_MENSAJE"];
                            body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_MAIL_BODY_FIN"];

                            /* Envío de notificación al productor solicitante */
                            project.sendMailNotification(mailTo, subject, body,Server);
                        }
                        if (administratorMail != "") 
                        {
                            string adminSubject = System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_ADMIN_MAIL_SUBJECT"] + " - " + producerName.Trim() + " - " + project.project_name.Trim();
                            string adminBody =  System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_ADMIN_MAIL_BODY_MENSAJE"];

                            /* Envío de notificación al administrador del sistema */
                            project.sendMailNotification(administratorMail, adminSubject, adminBody,Server);
                        }

                        Response.Redirect("Lista.aspx", true);
                    }

                    Response.Redirect("Lista.aspx", true);
                }
#endregion
            }
            if (Request.Form["enviarsolicituddeaclaraciones_field"] == "1")
            {//cuando director lo envia de nuevo al productor
                #region enviar solicitud de acalarciones
                if (project.state_id == 4) //Para pasar al director debe estar en el estado 3 (Editada)
                {
                    project.project_clarification_request_date = DateTime.Now; /* Se define la fecha del momento de la solicitud de aclaraciones usando la fecha del servidor de aplicación */
                    project.state_id = 5; //Pasa a estado 5 (Aclaraciones solicitadas)

                    if (//validamos que el formulario este aprobado
                     (!project.formulario_aprobado_pronda.HasValue
                      || !project.formulario_aprobado_pronda.Value)
                    )
                    {
                        RequestForm form = new RequestForm();
                        form.DeleteByProject(project.project_id);
                    }

                    if (project.Save(true))
                    {
                        /* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                        string mailTo = "";
                        string producerName = "";

                        /* Buscamos el objeto del productor que hace la solicitud */
                        int RequesterProducerTemp = project.producer.FindIndex(
                        delegate(Producer producerObj)
                        {
                            return producerObj.requester == 1;
                        });
                        if (RequesterProducerTemp != -1)
                        {
                            mailTo = project.producer[RequesterProducerTemp].producer_email;
                            producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                        }
                        if (mailTo != "")
                        {
                            SolicitudAclaraciones sol = new SolicitudAclaraciones();
                            string subject = System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_REQUEST_MAIL_SUBJECT"]+" - "+producerName+" - "+project.project_name;
                            string body = "";
                            string cuerpo = sol.generarHtml(project);
                            body = body + "<br/><br/>" + cuerpo;
                            //guardamos la carta de solicitud de aclaraciones 
                            project.carta_aclaraciones_generada = cuerpo;
                            project.Save();
                            
                           // body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_REQUEST_MAIL_BODY_MENSAJE"];
                            body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["SEND_CLARIFICATION_REQUEST_MAIL_BODY_FIN"];

                            /* Envío de notificación al productor solicitante */
                            project.sendMailNotification(mailTo, subject, body,Server);

                         

                            /* Envía copia al usuario con el que ingreso */
                            if (Session["user_mail"].ToString() != mailTo && Session["user_mail"].ToString().Trim() != string.Empty)
                            {
                                body = "<b>Productor:</b>" +producerName+"<br><b>Correo:</b>"+mailTo  +"<br></br>"+body;
                                project.sendMailNotification(Session["user_mail"].ToString(), subject, body,Server);
                            }
                        }

                        Response.Redirect("Lista.aspx", true);
                    }

                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["back_to_revisor"] == "1")
            {
                #region devolver a revisor
                if (project.state_id == 3) //Para pasar al director debe estar en el estado 3 (Revisada)
                {
                    project.state_id = 2; //Pasa a estado 3 (Editada)
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
#endregion
            }
            if (Request.Form["back_to_revisor2"] == "1")
            {
                #region devolver a revisor
                if (project.state_id == 7) //Para pasar al director debe estar en el estado 3 (Revisada)
                {
                    project.state_id = 6; //Pasa a estado 3 (Editada)
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
#endregion
            }
            if (Request.Form["back_to_editor"] == "1")
            {
                #region devovler a editor
                if (project.state_id == 4) //Para pasar al director debe estar en el estado 3 (Revisada)
                {
                    project.state_id = 3; //Pasa a estado 3 (Editada)
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["back_to_editor2"] == "1")
            {
                #region delvover a editor 2
                if (project.state_id == 8) //Para pasar al director debe estar en el estado 3 (Revisada)
                {
                    project.state_id = 7; //Pasa a estado 3 (Editada)
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["pasaradirector_field"] == "1")
            {
                #region pasar a director
                if (project.state_id == 3) //Para pasar al director debe estar en el estado 3 (Revisada)
                {
                    project.fecha_editor_director = DateTime.Now;
                    project.state_id = 4; //Pasa a estado 3 (Editada)
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["pasaraeditor_field"] == "1")
            {
                #region pasar a editor field
                if (project.state_id == 2) //Para pasar al editor debe estar en el estado 2 (Enviada)
                {
                    project.fecha_revisor_editor = DateTime.Now;
                    project.state_id = 3; //Pasa a estado 3 (Revisada)
                    project.Save(true);
                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["pasaraeditor2_field"] == "1")
            {
                #region
                if (project.state_id == 6) //Para pasar al editor debe estar en el estado 2 (Aclaraciones recibidas)
                {
                    project.fecha_revisor_editor2 = DateTime.Now;
                    project.state_id = 7; //Pasa a estado 7 (Aclaraciones Revisadas)
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
                #endregion
            }
            if (Request.Form["pasaradirector2_field"] == "1")
            {
                if (project.state_id == 7) //Para pasar al editor debe estar en el estado 2 (Aclaraciones recibidas)
                {
                    project.fecha_editor_director2 = DateTime.Now;
                    project.state_id = 8; //Pasa a estado 7 (Aclaraciones Revisadas)
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
            }
            if (Request.Form["specialaction"] == "undorequest")
            {
                if (project.state_id == 2 || project.state_id == 3 || project.state_id == 4) //Si se desea devolver la solicitud al productor
                {
                    project.state_id = 1;
                    project.Save(true);

                    Response.Redirect("Lista.aspx", true);
                }
            }
            if (Request.Form["schedulefilmview_field"] == "1")
            {
                if (schedulefilmview_date.Value.Trim() != string.Empty)
                project.project_schedule_film_view = Convert.ToDateTime(schedulefilmview_date.Value, culture);

                project.project_result_film_view = schedulefilmview_result.Value;
                if (rdSinRevizarVisualizacion.Checked)
                {
                    if (project.state_id < 5)
                        project.aprueba_visualizacion_proyecto_pronda = null;
                    else
                        project.aprueba_visualizacion_proyecto_sronda = null;
                }else{
                    if (project.state_id < 5)
                        project.aprueba_visualizacion_proyecto_pronda = rdInformacionCorrectaVisualizacion.Checked;
                    else
                        project.aprueba_visualizacion_proyecto_sronda = rdInformacionCorrectaVisualizacion.Checked;
                }
                project.Save(true);
            }
            if (Request.Form["resolution_date_field"] == "1")
            {
                project.project_resolution_date = Convert.ToDateTime(resolution_date.Value, culture);
                project.Save(true);
            }
            if (Request.Form["resolution_date2_field"] == "1")
            {
                project.project_resolution2_date = Convert.ToDateTime(resolution_date2.Value, culture);
                project.Save(true);
            }
            if (Request.Form["notification_date_field"] == "1")
            {
                project.project_notification_date = Convert.ToDateTime(notification_date.Value, culture);
                project.Save(true);
                string producerName = "";string mailTo = "";
                int RequesterProducerTemp = project.producer.FindIndex(
                delegate (Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
                if (RequesterProducerTemp != -1)
                {
                    mailTo = project.producer[RequesterProducerTemp].producer_email;
                    producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                }
                //enviamos la notificacion por correo
                string subject = project.LoadCorreo("AsuntoNotificacion") + " - " + producerName.Trim() + " - " + project.project_name.Trim();
                string body = project.LoadCorreo("CuerpoNotificacion")
                    .Replace("@productor", producerName)
                    .Replace("@titulo", project.project_name.Trim());

    
                project.sendMailNotification(mailTo, subject, body, Server);
            } 
            if (Request.Form["notification_date2_field"] == "1")
            {
                project.project_notification2_date = Convert.ToDateTime(notification_date2.Value, culture);
                project.Save(true);
            }
            if (Request.Form["submit_field"] == "1")
            {
                lblErrorEnviar.Text = "";
                lblErrorEnviar.Text = "Por reestructuración Técnica actualmente no está habilitada la plataforma para él envió de su trámite. \r\nMientras se habilita podrá realizar modificaciones a su información y/o adjuntos. Le recomendamos validar que se encuentren todos los adjuntos de su solicitud cargados. \r\nLe estaremos notificando una vez se reestablezca el servicio en su totalidad para el envío de su solicitud.";
                return;
                //PANDEMIA
                #region enviar proyecto
                lblErrorEnviar.Text = "";
                /* Acciones de persistencia de acuerdo al estado actual */
                RequestForm form = new RequestForm(this.project_id);
                if (form.path == null || form.path.Trim() == string.Empty)
                {
                    lblErrorEnviar.Text = "Antes de enviar el proyecto, debe cargar el formulario de solicitud firmado.";
                }else
                if (project.state_id == 1) //Si la solicitud esta creada y el productor esta enviandola
                {
                    project.project_request_date = DateTime.Now; /* Se define la fecha del momento del envío usando la fecha del servidor de aplicación */
                    project.state_id = 2; /* Se cambia el estado a "Enviada" */
                    if (project.Save(true))
                    {
                        //se asigna responsable                                                                       
                        NegocioCineProducto negCineP = new NegocioCineProducto();
                        negCineP.AsignarPrimerResponsable(project.project_id, Convert.ToInt32(Session["user_id"]));                        
                        negCineP.ActualizarFechaLimite(project.project_id, project.project_request_date);

                        /* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                        string mailTo = "";
                        string administratorMail = System.Configuration.ConfigurationManager.AppSettings["NOTIFICATIONS_MAIL"];
                        string producerName = "";

                        /* Buscamos el objeto del productor que hace la solicitud */
                        int RequesterProducerTemp = project.producer.FindIndex(
                        delegate(Producer producerObj)
                        {
                            return producerObj.requester == 1;
                        });
                        if (RequesterProducerTemp != -1)
                        {
                            mailTo = project.producer[RequesterProducerTemp].producer_email;
                            producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                        }
                        if (mailTo != "" && administratorMail != "")
                        {
                            string subject = System.Configuration.ConfigurationManager.AppSettings["CREATE_REQUEST_MAIL_SUBJECT"] + " - " + producerName.Trim() + " - " + project.project_name.Trim();
                            string body = System.Configuration.ConfigurationManager.AppSettings["CREATE_REQUEST_MAIL_BODY_SALUDO"] + producerName;
                            body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["CREATE_REQUEST_MAIL_BODY_MENSAJE"];
                            body = body + "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["CREATE_REQUEST_MAIL_BODY_FIN"];
                            //agrgamos informacion del proyecto

                           
                            

                            /* Envío de notificación al productor solicitante */
                            project.sendMailNotification(mailTo, subject, body,Server);

                            string adminSubject = System.Configuration.ConfigurationManager.AppSettings["CREATE_REQUEST_ADMIN_MAIL_SUBJECT"]+" - "+producerName.Trim()+" - "+project.project_name.Trim();
                            string adminBody = System.Configuration.ConfigurationManager.AppSettings["CREATE_REQUEST_ADMIN_MAIL_BODY_MENSAJE"];
                            adminBody = adminBody + "<br/><b>Productor:</b>" + producerName;
                            adminBody = adminBody + "<br/><b>Titulo:</b>" + project.project_name;
                            adminBody = adminBody + "<br/><b>Tipo de Producción:</b>" + project.production_type_name;
                            adminBody = adminBody + "<br/><b>Tipo de obra:</b>" + project.project_genre_name;
                            adminBody = adminBody + "<br/>" + project.project_type_name;
                            adminBody = adminBody + "<br/><b>Sinopsis:</b>" + project.project_synopsis;
                            /* Envío de notificación al administrador del sistema */
                            project.sendMailNotification(administratorMail, adminSubject, adminBody,Server);
                        }

                        Response.Redirect("Lista.aspx", true);
                    }
                }
#endregion
            }
#endregion

            /* Si el proyecto ya tiene identificador y aun no está en sesión, es registrado en la variable de sesión */
            if (Session["project_id"] == null && project.project_id > 0)
            {
                Session["project_id"] = project.project_id;
            }
            #region validaciones de resumen
            /* Si ya esta cargado el proyecto en sesión, se procesan las validaciones */
            if (Session["project_id"] != null)
            {
                if (project.ValidateProjectSection("DatosProyecto"))
                {
                    mensaje_validacion_datos_proyecto.Text = "<span class=\"correcto\">Correcto</span>";
                }
                else 
                {
                    this.showSendButton = false; showAtachhForm = false;
                    this.showDescargarButton = false;
                    showAclaraciones = false;
                    mensaje_validacion_datos_proyecto.Text = "<span class=\"incorrecto\">Incompleto<ul>" + project.section_validation_result + "</ul></span>";
                }

                if (project.ValidateProjectSection("DatosProductor"))
                {
                    mensaje_validacion_datos_productor.Text = "<span class=\"correcto\">Correcto</span>";
                }
                else
                {
                    this.showSendButton = false; showAtachhForm = false;
                    this.showDescargarButton = false;
                    showAclaraciones = false;
                    mensaje_validacion_datos_productor.Text = "<span class=\"incorrecto\">Incompleto<ul>" + project.section_validation_result + "</ul></span>";
                }

                if (project.ValidateProjectSection("ProductoresAdicionales"))
                {
                    mensaje_validacion_productores_adicionales.Text = "<span class=\"correcto\">Correcto</span>";
                }
                else
                {
                    this.showSendButton = false; showAtachhForm = false;
                    this.showDescargarButton = false;
                    showAclaraciones = false;
                    mensaje_validacion_productores_adicionales.Text = "<span class=\"incorrecto\">Incompleto<ul>" + project.section_validation_result + "</ul></span>";
                }

                if (project.ValidateProjectSection("DatosFormatoPersonal"))
                {
                    mensaje_validacion_datos_fomato_personal.Text = "<span class=\"correcto\">Correcto</span>";
                }
                else
                {
                    this.showSendButton = false; showAtachhForm = false;
                    this.showDescargarButton = false;
                    showAclaraciones = false;
                    mensaje_validacion_datos_fomato_personal.Text = "<span class=\"incorrecto\">Incompleto<ul>" + project.section_validation_result + "</ul></span>";
                }

                if (project.ValidateProjectSection("DatosPersonal"))
                {
                    mensaje_validacion_datos_personales.Text = "<span class=\"correcto\">Correcto</span>";
                }
                else
                {
                    this.showSendButton = false; showAtachhForm = false;
                    this.showDescargarButton = false;
                    showAclaraciones = false;
                    mensaje_validacion_datos_personales.Text = "<span class=\"incorrecto\">Incompleto<ul>" + project.section_validation_result + "</ul></span>";
                }

                
                RequestForm form = new RequestForm(this.project_id);
                mensaje_validacion_datos_adjuntos.Text = "";

                if (form.path != null && form.path.Trim() != string.Empty)
                {
                    mensaje_validacion_datos_finalizacion.Text = "<span class=\"correcto\">Correcto</span>";
                    //showAtachhForm = false;
                }else{
                   // this.showSendButton = false;
                    //no ocultamos el boton de enviar, y validamos que ya haya adjuntado en el momento de enviar el proyecto
                    mensaje_validacion_datos_finalizacion.Text = "<span class=\"incorrecto\">Incompleto<ul>Debe cargar el formulario de solicitud firmado</ul></span>";

                }

                /*if (project.ValidateProjectSection("DatosAdjuntos"))
                {
                    mensaje_validacion_datos_adjuntos.Text = "<span class=\"correcto\">Correcto</span>";
                }
                else
                {
                    this.showSendButton = false;
                    mensaje_validacion_datos_adjuntos.Text = "<span class=\"incorrecto\">Incompleto<ul>" + project.section_validation_result + "</ul></span>";
                }*/
            }
            #endregion
            if (!IsPostBack)
            {
                txtComplementoCartaAclaraciones.Text = project.complemento_carta_aclaraciones;
                txtSustitutoCartaAclaraciones.Text = project.sustituto_carta_aclaracion;
                comentarios_adicionales.Text = project.obs_adicional_finalizacion;
            }
            /* Se valida el estado para definir si se presenta el botón de enviar y para precargar la información de los formularios */
            if (user_role <= 1)
            {
                if (project_state == 1 || project_state == 5)
                {
                    showDescargarButton = true;
                }
                else
                {
                    showDescargarButton = false;
                }
            }
            else {
                showDescargarButton = true;
            }
            
            if(project.state_id != 1)
            {
                /* Carga la información registrada en la base de datos sobre la programación y resultado de la reunión de visualización del proyecto */
                try
                {
                    DateTime d = new DateTime(project.project_schedule_film_view.Year , project.project_schedule_film_view.Month , project.project_schedule_film_view.Day,project.project_schedule_film_view.Hour , project.project_schedule_film_view.Minute,0);
                    if (d.Year > 1)
                    {
                        schedulefilmview_date.Value = d.Year + "-" + d.Month + "-" + d.Day + " " + d.Hour + ":" + d.Minute;
                    }
                }catch(Exception){}               

                /**
               * Se verifica que el proyecto esté terminado, para luego verificar si se disponde del link
               * apra descargar el archivo de resolucion.
               */
                if (project.state_id < 5 || (project.state_id == 5 && user_role >1))
                {
                    pnlObservacionesAclaracion.Visible = true;
                    if (project.aprueba_visualizacion_proyecto_pronda.HasValue)
                    {
                        rdInformacionCorrectaVisualizacion.Checked = project.aprueba_visualizacion_proyecto_pronda.Value;
                        rdSolicitarAclaracionesVisualizacion.Checked = !project.aprueba_visualizacion_proyecto_pronda.Value;
                    }

                    if (project.formulario_aprobado_pronda.HasValue)
                    {
                        rdFormularioAprobado.Checked = project.formulario_aprobado_pronda.Value;
                        rdFormularioNoAprobado.Checked = !project.formulario_aprobado_pronda.Value;
                    }
                }else {
                    //si fue aprobado desde la primera ronda ya queda aprobado
                    if (project.aprueba_visualizacion_proyecto_pronda.HasValue && project.aprueba_visualizacion_proyecto_pronda.Value)
                    {
                        rdInformacionCorrectaVisualizacion.Checked = true;
                        rdSolicitarAclaracionesVisualizacion.Checked = false;
                    }else if (project.aprueba_visualizacion_proyecto_sronda.HasValue)
                        {
                            rdInformacionCorrectaVisualizacion.Checked = project.aprueba_visualizacion_proyecto_sronda.Value;
                            rdSolicitarAclaracionesVisualizacion.Checked = !project.aprueba_visualizacion_proyecto_sronda.Value;
                        }

                    if (project.formulario_aprobado_pronda.HasValue && project.formulario_aprobado_pronda.Value)
                    {
                        rdFormularioAprobado.Checked = true;
                        rdFormularioNoAprobado.Checked = false;
                    }else if (project.formulario_aprobado_sronda.HasValue){
                            rdFormularioAprobado.Checked = project.formulario_aprobado_sronda.Value;
                            rdFormularioNoAprobado.Checked = !project.formulario_aprobado_sronda.Value;
                    }
                }

                if (project.state_id == 5 && user_role > 1)
                    //no permite modificar nada
                {
                    rdFormularioAprobado.Enabled = false;
                    rdFormularioNoAprobado.Enabled = false;
                    rdFormulariosinRevizar.Enabled = false;
                    rdInformacionCorrectaVisualizacion.Enabled = false;
                    rdSinRevizarVisualizacion.Enabled = false;
                    rdSolicitarAclaracionesVisualizacion.Enabled = false;
                    schedulefilmview_date.Disabled = true;
                    schedulefilmview_result.Disabled = true;
                    comentarios_adicionales.ReadOnly = true;
                }

                
                if (project.state_id >= 5 && user_role <= 1 && (project.aprueba_visualizacion_proyecto_pronda.HasValue && !project.aprueba_visualizacion_proyecto_pronda.Value))
                {
                        pnlObservacionesAclaracion.Visible = true;
                        if (project.state_id > 5)
                        {
                            txtRespuestaVisualizacion.ReadOnly = true;
                            btnGuardarRespuestaVisualizacion.Visible = false;
                        }
                }

                if (project.observaciones_visualizacion_por_productor != null
                    && project.observaciones_visualizacion_por_productor.Trim() != 
                    string.Empty)
                {
                    pnlRespuestaVisualizacionProductor.Visible = true;
                }

                schedulefilmview_result.Value = project.project_result_film_view;
                if (!IsPostBack)
                {
                    schedulefilmview_result2.Text = project.project_result_film_view;
                    txtRespuestaVisualizacion.Text = project.observaciones_visualizacion_por_productor;
                    txtRespuestaVisualizacion2.Text = project.observaciones_visualizacion_por_productor;
                   
                }
                /* Carga la información registrada en la base de datos sobre las fechas de resolución y notificación */
                if (project.project_resolution_date.Year > 1)
                resolution_date.Value = project.project_resolution_date.Year + "-" + project.project_resolution_date.Month + "-" + project.project_resolution_date.Day;

                if (project.project_notification_date.Year >1 )
                notification_date.Value = project.project_notification_date.Year + "-" + project.project_notification_date.Month + "-" + project.project_notification_date.Day;

                if (project.project_resolution2_date.Year > 1)
                    resolution_date2.Value = project.project_resolution2_date.Year + "-" + project.project_resolution2_date.Month + "-" + project.project_resolution2_date.Day;

                if (project.project_notification2_date.Year > 1)
                    notification_date2.Value = project.project_notification2_date.Year + "-" + project.project_notification2_date.Month + "-" + project.project_notification2_date.Day;


                if ((project.state_id > 1 && project.formulario_aprobado_pronda.HasValue && project.formulario_aprobado_pronda.Value) ||
                    (project.state_id > 5 && project.formulario_aprobado_sronda.HasValue && project.formulario_aprobado_sronda.Value))
                {
                showAtachhForm=false;
                }
                cargarResolucion();
                cargarResolucion2();
            }
           
                
        }



        protected void generatePDFForm(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" || Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
                Request.Form["back_to_revisor"] == "1" || Request.Form["back_to_revisor2"] == "1" ||
                Request.Form["back_to_editor"] == "1" ||  Request.Form["back_to_editor2"] == "1" ||
                Request.Form["pasaradirector_field"] == "1" || Request.Form["pasaraeditor_field"] == "1" ||
                Request.Form["pasaraeditor2_field"] == "1" || Request.Form["pasaradirector2_field"] == "1" ||
                Request.Form["specialaction"] == "undorequest" || Request.Form["schedulefilmview_field"] == "1" ||
                Request.Form["resolution_date_field"] == "1" || Request.Form["notification_date_field"] == "1" ||
                (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
                )
            
                return;
            Project project = new Project();
            project.LoadProject(this.project_id);

            string fileName = Guid.NewGuid().ToString() + ".pdf";
            Document document = new Document(PageSize.A4, 50, 50, 15, 25);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                PdfPTable t = new PdfPTable(3);
                t.SetWidthPercentage(new float[] { 100, 400, 100 }, PageSize.A4);
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("images/mincultura.png"));
                jpg.ScaleToFit(80f, 60f);
                //Give space before image
                jpg.SpacingBefore = 10f;
                //Give some space after the image
                jpg.SpacingAfter = 10f;
                jpg.Alignment = Element.ALIGN_RIGHT;
                t.AddCell(new PdfPCell(jpg) { Rowspan = 2, PaddingLeft = 1, PaddingTop = 6 });
                t.AddCell(new PdfPCell(new Paragraph("Dirección de Audiovisuales, Cine y Medios Interactivos\n\nSOLICITUD DE RECONOCIMENTO COMO OBRA\nCINEMATOGRÁFICA COLOMBIANA\n")) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2 });
                t.AddCell(new PdfPCell(new Paragraph("Página 1 de 1")));
                t.AddCell(new PdfPCell(new Paragraph("Código: F-DCI-032\nVersión: 0\nFecha: 08-MAYO-2014", FontFactory.GetFont("Arial", 8))));
                document.Add(t);

                Paragraph separtor = new Paragraph("\n");

                Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                Font normal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                document.Add(separtor);
                document.Add(separtor);
                var phrase1 = new Phrase();
                phrase1.Add(new Chunk("Datos del productor ", boldFont));
                //phrase1.Add(new Chunk("(Si son varios productores colombianos, favor diligenciar la información específica de cada uno)", normal));
                phrase1.Add(new Chunk("", normal));
                document.Add(phrase1);

                string direccion = "";
                string telefono = "";
                string celular = "";
                string email = "";
                string fax = "";
                string website = "";
                string municipio = "";
                string departamento = "";
                string departamento_optional = "";
                string municipio_optional = "";

                foreach (Producer producer in project.producer)
                {
                    PdfPTable producerData = new PdfPTable(9);
                    producerData.SetWidthPercentage(new float[] { 130f, 130f, 72f, 28f, 70f, 60f, 22f, 60f, 28f }, PageSize.A4);
                    string natural_person = "";
                    string juridic_person = "";
                    string razonSocial = "";
                    string nit = "";
                    string representanteName = "";
                    string representanteLastname = "";
                    string cc = "";
                    string ce = "";
                    string numeroCedulaNatural = "";
                    string numeroCedulaJuridica = "";

                    string naturalPersonName = "";
                    string naturalPersonLastName = "";
                    if (producer.requester == 1)
                    {
                        direccion = producer.producer_address;
                        telefono = producer.producer_phone;
                        celular = producer.producer_movil;
                        email = producer.producer_email;
                        website = producer.producer_website;
                        fax = producer.producer_fax;
                        municipio = producer.producer_localization_id;
                        departamento = producer.producer_localization_father_id;
                        departamento_optional = producer.producer_country;
                        municipio_optional = producer.producer_city;
                    }
                    if (producer.producer_type_id == 1)
                    {
                        if (producer.person_type_id == 1)
                        {
                            natural_person = "X";
                            naturalPersonName = (producer.producer_firstname != "") ? producer.producer_firstname : "";
                            naturalPersonLastName = (producer.producer_lastname != "") ? producer.producer_lastname : "";
                            numeroCedulaNatural = producer.producer_identification_number;

                        }
                        else if (producer.person_type_id == 2)
                        {
                            juridic_person = "X";
                            representanteName = (producer.producer_name != "") ? producer.producer_firstname : "";
                            representanteLastname = (producer.producer_lastname != "") ? producer.producer_lastname : "";
                            numeroCedulaJuridica = producer.producer_identification_number;
                            if (producer.identification_type_id == 1)
                            {
                                cc = "X";
                            }
                            else if (producer.identification_type_id == 2)
                            {
                                ce = "X";
                            }
                            razonSocial = (producer.producer_name != "") ? producer.producer_name : "";
                            nit = (producer.producer_nit != "") ? producer.producer_nit : "";
                        }

                        //Headers
                        producerData.AddCell(new PdfPCell(new Paragraph("Tipo de Productor", FontFactory.GetFont("Arial", 10))) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 3 });
                        producerData.AddCell(new PdfPCell(new Paragraph("Persona Juridica", FontFactory.GetFont("Arial", 10))) { Colspan = 2 });
                        producerData.AddCell(new PdfPCell(new Paragraph(juridic_person)));
                        producerData.AddCell(new PdfPCell(new Paragraph("Persona Natural", FontFactory.GetFont("Arial", 10))) { Colspan = 4 });
                        producerData.AddCell(new PdfPCell(new Paragraph(natural_person, FontFactory.GetFont("Arial", 10))));

                        producerData.AddCell(new PdfPCell(new Paragraph("Datos de la Persona Jurídica", FontFactory.GetFont("Arial", 10))) { Colspan = 9, BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 3 });

                        producerData.AddCell(new PdfPCell(new Paragraph("Nombre o Razón Social\n" + razonSocial, FontFactory.GetFont("Arial", 10))) { Colspan = 4 });
                        producerData.AddCell(new PdfPCell(new Paragraph("Número NIT\n" + nit, FontFactory.GetFont("Arial", 10))) { Colspan = 5 });

                        producerData.AddCell(new PdfPCell(new Paragraph("Nombres y Apellidos Representante Legal\n" + representanteName + " " + representanteLastname, FontFactory.GetFont("Arial", 10))) { Colspan = 4 });
                        producerData.AddCell(new PdfPCell(new Paragraph("Tipo\nDocumento", FontFactory.GetFont("Arial", 10))));
                        producerData.AddCell(new PdfPCell(new Paragraph("CC", FontFactory.GetFont("Arial", 10))));
                        producerData.AddCell(new PdfPCell(new Paragraph(cc, FontFactory.GetFont("Arial", 10))));
                        producerData.AddCell(new PdfPCell(new Paragraph("CE", FontFactory.GetFont("Arial", 10))));
                        producerData.AddCell(new PdfPCell(new Paragraph(ce, FontFactory.GetFont("Arial", 10))));

                        producerData.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 10))) { Colspan = 4 });
                        producerData.AddCell(new PdfPCell(new Paragraph("Número: " + numeroCedulaJuridica, FontFactory.GetFont("Arial", 10))) { Colspan = 5 });

                        producerData.AddCell(new PdfPCell(new Paragraph("Datos de la Persona Natural", FontFactory.GetFont("Arial", 10))) { Colspan = 9, BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 3 });

                        producerData.AddCell(new PdfPCell(new Paragraph("Nombres y Apellidos\n" + naturalPersonName + " " + naturalPersonLastName, FontFactory.GetFont("Arial", 10))) { Colspan = 4 });
                        producerData.AddCell(new PdfPCell(new Paragraph("Número CC\n" + numeroCedulaNatural, FontFactory.GetFont("Arial", 10))) { Colspan = 5 });

                        document.Add(producerData);

                        document.Add(separtor);
                    }
                }
                DB db = new DB();
                if (municipio != "" && departamento != "")
                {

                    DataSet ds = db.Select("SELECT localization_name "
                                         + "FROM dboPrd.localization "
                                         + "WHERE localization_id =" + municipio
                                         + "OR localization_id = " + departamento);
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        /* Se consulta el nombre del tipo de producción, tipo de proyecto y genero */
                        departamento = ds.Tables[0].Rows[0]["localization_name"].ToString();
                        municipio = ds.Tables[0].Rows[1]["localization_name"].ToString();
                    }
                }
                else
                {
                    departamento = departamento_optional;
                    municipio = municipio_optional;
                }
                var phrase2 = new Phrase();
                phrase2.Add(new Chunk("DATOS DE CORRESPONDENCIA ", boldFont));
                //phrase2.Add(new Chunk("(Si son varios productores colombianos, favor diligenciar los datos del productor delegado, persona natural o juridica, para adelantar este trámite)", normal));
                phrase2.Add(new Chunk("", normal));
                document.Add(phrase2);

                PdfPTable correspondence = new PdfPTable(4);
                correspondence.SetWidthPercentage(new float[] { 150f, 150f, 150f, 150f }, PageSize.A4);

                correspondence.AddCell(new PdfPCell(new Paragraph("Dirección de correspondencia: " + direccion, FontFactory.GetFont("Arial", 10))) { Colspan = 4 });

                correspondence.AddCell(new PdfPCell(new Paragraph("Teléfono: " + telefono, FontFactory.GetFont("Arial", 10))));
                correspondence.AddCell(new PdfPCell(new Paragraph("Fax: " + fax, FontFactory.GetFont("Arial", 10))));
                correspondence.AddCell(new PdfPCell(new Paragraph("celular: " + celular, FontFactory.GetFont("Arial", 10))));
                correspondence.AddCell(new PdfPCell(new Paragraph("E-mail: " + email, FontFactory.GetFont("Arial", 10))));

                correspondence.AddCell(new PdfPCell(new Paragraph("Sitio Web: " + website, FontFactory.GetFont("Arial", 10))));
                correspondence.AddCell(new PdfPCell(new Paragraph("Municipio: " + municipio, FontFactory.GetFont("Arial", 10))) { Colspan = 2 });
                correspondence.AddCell(new PdfPCell(new Paragraph("DPTO: " + departamento, FontFactory.GetFont("Arial", 10))));

                document.Add(correspondence);

                var phrase3 = new Phrase();
                phrase3.Add(new Chunk("DATOS DE LOS COPRODUCTORES EXTRANJEROS ", boldFont));
                //phrase3.Add(new Chunk("(diligenciar los datos de todos aquellos con quienes se tenga contrato de coproducción firmado)", normal));
                phrase3.Add(new Chunk("", normal));

                bool ExistenExtranjeros = false;
                foreach (Producer producer in project.producer)
                {
                    if (producer.producer_type_id == 2)
                    {
                        ExistenExtranjeros = true;
                    }
                }
                if (ExistenExtranjeros)
                {
                    document.Add(phrase3);
                }
                foreach (Producer producer in project.producer)
                {
                    if (producer.producer_type_id == 2)
                    {
                        if (producer.producer_localization_id != "" && producer.producer_localization_father_id != "")
                        {
                            DataSet dss = db.Select("SELECT localization_name "
                                         + "FROM dboPrd.localization "
                                         + "WHERE localization_id =" + producer.producer_localization_id
                                         + "OR localization_id = " + producer.producer_localization_father_id);
                            if (dss.Tables[0].Rows.Count > 1)
                            {
                                /* Se consulta el nombre del tipo de producción, tipo de proyecto y genero */
                                departamento = dss.Tables[0].Rows[0]["localization_name"].ToString();
                                municipio = dss.Tables[0].Rows[1]["localization_name"].ToString();
                            }
                        }
                        else if (producer.producer_city != "" && producer.producer_country != "")
                        {
                            departamento = producer.producer_country;
                            municipio = producer.producer_city;
                        }
                        PdfPTable foreingProducer = new PdfPTable(3);
                        foreingProducer.SetWidthPercentage(new float[] { 200f, 200f, 200f }, PageSize.A4);
                        string name = "";
                        name = (producer.producer_name != "") ? producer.producer_name : producer.producer_firstname + " " + producer.producer_lastname;
                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Nombre o Razón social  " + name, FontFactory.GetFont("Arial", 10))) { Colspan = 3 });

                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Nombre de Contacto  " + producer.producer_firstname + " " + producer.producer_lastname, FontFactory.GetFont("Arial", 10))) { Colspan = 3 });

                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Teléfonos (indicativo) " + producer.producer_phone, FontFactory.GetFont("Arial", 10))));
                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Fax (Indicativo)"+producer.producer_fax, FontFactory.GetFont("Arial", 10))));
                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Correo Electrónico "+producer.producer_email, FontFactory.GetFont("Arial", 10))));

                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Sitio web "+producer.producer_website, FontFactory.GetFont("Arial", 10))));
                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Ciudad "+municipio, FontFactory.GetFont("Arial", 10))));
                        foreingProducer.AddCell(new PdfPCell(new Paragraph("Pais "+departamento, FontFactory.GetFont("Arial", 10))));

                        document.Add(foreingProducer);
                        document.Add(separtor);
                    }
                }

                document.NewPage();
                var phrase4 = new Phrase();
                phrase4.Add(new Chunk("DATOS DE LA PELÍCULA ", boldFont));

                document.Add(phrase4);
                PdfPTable productData = new PdfPTable(8);
                productData.SetWidthPercentage(new float[] { 80f, 80f, 120f, 80f, 120f, 40f, 40f, 40f }, PageSize.A4);

                string genre = "";

                string legal_deposit_yes = "";
                string legal_deposit_no = "";

                if (project.project_genre_id == 1)
                {
                    genre = "Ficción";
                }
                else if (project.project_genre_id == 2)
                {
                    genre = "Documental";
                }
                else if (project.project_genre_id == 3)
                {
                    genre = "Animación";
                }
                string formatosRodaje = "";
                string formatosExhibicion = "";
                foreach (Format format in project.project_format)
                {
                    if (format.format_project_detail.Trim() != string.Empty)
                        format.format_project_detail = "("+format.format_project_detail.Trim() + ")";
                    if (format.format_type_id == 1)
                    {
                        formatosRodaje +=  format.format_name + " " + format.format_project_detail.Trim() + ", ";
                    }
                    else {
                        formatosExhibicion += format.format_name + " "+format.format_project_detail.Trim()+", ";
                    }
                    
                }

                if (project.project_legal_deposit != 0)
                {
                    legal_deposit_yes = "X";
                }
                else
                {
                    legal_deposit_no = "X";
                }
                int start_day = project.project_filming_start_date.Day;
                int start_month = project.project_filming_start_date.Month;
                int start_year = project.project_filming_start_date.Year;
                int end_day = project.project_filming_end_date.Day;
                int end_month = project.project_filming_end_date.Month;
                int end_year = project.project_filming_end_date.Year;
                string negative = (project.project_development_lab_info == "") ? "No aplica" : project.project_development_lab_info;
                string preprint = (project.project_preprint_store_info == "") ? "No aplica" : project.project_preprint_store_info;


                string duration = project.GetProjectDuration("minutes").ToString().PadLeft(2,'0') + ":" + (project.GetProjectDuration("seconds").ToString().PadLeft(2,'0'));
                long presupuestoTotal = project.project_total_cost_desarrollo + project.project_total_cost_posproduccion + project.project_total_cost_preproduccion + project.project_total_cost_produccion;

                productData.AddCell(new PdfPCell(new Paragraph("Título de la película " + project.project_name, FontFactory.GetFont("Arial", 10))) { Colspan = 8 });

                productData.AddCell(new PdfPCell(new Paragraph("Tipo de Producción:", FontFactory.GetFont("Arial", 10))) { Colspan = 2 });

                productData.AddCell(new PdfPCell(new Paragraph(project.production_type_name, FontFactory.GetFont("Arial", 10))) { Colspan = 2 });
                
                productData.AddCell(new PdfPCell(new Paragraph(project.project_type_name, FontFactory.GetFont("Arial", 10))) { Colspan = 4 });



                productData.AddCell(new PdfPCell(new Paragraph("Marque con una (X) o complete la siguiente información", FontFactory.GetFont("Arial", 10))) { Colspan = 8, BackgroundColor = BaseColor.LIGHT_GRAY });
                productData.AddCell(new PdfPCell(new Paragraph("¿Ha realizado el deposito legal?", FontFactory.GetFont("Arial", 10))) { Rowspan = 2, Colspan = 2 });
                productData.AddCell(new PdfPCell(new Paragraph("Duración (mns:sgs)", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(duration, FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph("Género", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(genre, FontFactory.GetFont("Arial", 10))) { Colspan = 3 });

                productData.AddCell(new PdfPCell(new Paragraph("Formato(s) de rodaje", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(formatosRodaje, FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph("Fecha inicial de\nrodaje", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(start_day.ToString(), FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(start_month.ToString(), FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(start_year.ToString(), FontFactory.GetFont("Arial", 10))));

                productData.AddCell(new PdfPCell(new Paragraph("SI", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph("NO", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph("Formato exhibición", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(formatosExhibicion, FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph("Fecha final de \nrodaje", FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(end_day.ToString(), FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(end_month.ToString(), FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(end_year.ToString(), FontFactory.GetFont("Arial", 10))));

                productData.AddCell(new PdfPCell(new Paragraph(legal_deposit_yes, FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph(legal_deposit_no, FontFactory.GetFont("Arial", 10))));
                productData.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 10))) { Colspan = 6 });

                productData.AddCell(new PdfPCell(new Paragraph("Lugares de filmación: \n" + project.project_recording_sites, FontFactory.GetFont("Arial", 10))) { Colspan = 8 });

                productData.AddCell(new PdfPCell(new Paragraph("Costo total de la película en pesos colombianos (sin promoción): " + string.Format("{0:c}", presupuestoTotal), FontFactory.GetFont("Arial", 10))) { Colspan = 8 });
                if (negative.Trim() == string.Empty)
                {
                    negative = " No Aplica";
                }
                productData.AddCell(new PdfPCell(new Paragraph("Nombre y dirección del laboratorio donde se reveló el negativo:\n" + negative, FontFactory.GetFont("Arial", 10))) { Colspan = 8 });

                if (preprint.Trim() == string.Empty)
                {
                    preprint = " No Aplica";
                }
                productData.AddCell(new PdfPCell(new Paragraph("Nombre y dirección del laboratorio donde reposan los elementos de tiraje de la película:\n" + preprint, FontFactory.GetFont("Arial", 10))) { Colspan = 8 });
                productData.AddCell(new PdfPCell(new Paragraph("Sinopsis: \n" + project.project_synopsis, FontFactory.GetFont("Arial", 10))) { Colspan = 8 });

                document.Add(productData);
                //document.Add(projectData);

                Paragraph footerParagraph = new Paragraph("* La  “Coproducción Nacional” es realizada entre productores nacionales y extranjeros.\n"
                    + "** El depósito legal es la obligación de entregar una copia idéntica de la obra en la Biblioteca Nacional (se recibe en la Fundación Patrimonio Fílmico Colombiano), una vez estrenada.");
                
                
                footerParagraph.SetLeading(1.0f, 1.5f);
                footerParagraph.Alignment = Element.ALIGN_JUSTIFIED;
                footerParagraph.Font = FontFactory.GetFont("Arial", 8);
                document.Add(footerParagraph);

                var phrase6 = new Phrase();
                phrase6.Add(new Chunk("FIRMA ", boldFont));
                phrase6.Add(new Chunk("(del productor persona natural o del representante legal de la empresa productora)", normal));
                document.Add(phrase6);

                PdfPTable sing = new PdfPTable(2);
                sing.SetWidthPercentage(new float[] { 360f, 240f }, PageSize.A4);

                sing.AddCell(new PdfPCell(new Paragraph("\n\nCON MI FIRMA CERTIFICO LA VERACIDAD DE TODO CUANTO QUEDA CONSIGNADO EN EL PRESENTE FORMULARIO Y SUS RESPECTIVOS ANEXOS", FontFactory.GetFont("Arial", 10))));
                sing.AddCell(new PdfPCell(new Paragraph("CIUDAD Y FECHA", FontFactory.GetFont("Arial", 10))));

                document.Add(sing);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename="+fileName);
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
                // Process prc = new System.Diagnostics.Process();
                // prc.StartInfo.FileName = fileName;
                // prc.Start();
            }
        }

      
        protected void btnCargarResolution_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
              Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
              Request.Form["back_to_revisor"] == "1" ||
              Request.Form["back_to_revisor2"] == "1" ||
              Request.Form["back_to_editor"] == "1" ||
              Request.Form["back_to_editor2"] == "1" ||
              Request.Form["pasaradirector_field"] == "1" ||
              Request.Form["pasaraeditor_field"] == "1" ||
              Request.Form["pasaraeditor2_field"] == "1" ||
              Request.Form["pasaradirector2_field"] == "1" ||
              Request.Form["specialaction"] == "undorequest" ||
              Request.Form["schedulefilmview_field"] == "1" ||
              Request.Form["resolution_date_field"] == "1" ||
              Request.Form["notification_date_field"] == "1" ||
              (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
              )

                return;
            lblErrorReolucion.Text = "";
            lblErrorReolucion2.Text = "";
            if (!fileResolucion.HasFile)
            {
                lblErrorReolucion.Text = "Debe seleccionar un archivo";
                return;
            }

            if (System.IO.Path.GetExtension(fileResolucion.FileName).ToLower() != ".pdf")
            {
                lblErrorReolucion.Text = "solo es valido subir archivos en formato pdf.";
                return;
            }

            if (fileResolucion.FileBytes.LongLength > 5242880)
            {
                lblErrorReolucion.Text = "El archivo " + fileResolucion.FileName + "  supera el tamaño máximo de 5 Megas.";
                return;
            }

            string path = Server.MapPath("~/uploads/resolutions/" + Session["project_id"]);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            string nombre = DateTime.Now.Ticks.ToString().Substring(6) +"res.pdf";
            // string fileName = Path.Combine(path, fileResolucion.FileName);
            string fileName = Path.Combine(path, nombre);

            try
            {
                fileResolucion.SaveAs(fileName);
                Resolution resolution = new Resolution();
                resolution.LoadByProject( (int)Session["project_id"] );

//                resolution.path = fileResolucion.FileName;
                resolution.path = nombre;

                resolution.Save();
                lblErrorReolucion.Text = "Resolución cargada satisfactoriamente";
                cargarResolucion();
                /*enviamos el email al productor*/
                ///* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                string mailTo = "";
                string producerName = "";
                Project project= new Project();
                project.LoadProject((int)Session["project_id"]);
                ///* Buscamos el objeto del productor que hace la solicitud */
                int RequesterProducerTemp = project.producer.FindIndex(
                delegate(Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
                if (RequesterProducerTemp != -1)
                {
                    mailTo = project.producer[RequesterProducerTemp].producer_email;
                    producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                }
                if (mailTo != "")
                {

                    string subject = project.LoadCorreo("AsuntoResolucion") + " - " + producerName.Trim()+" - "+project.project_name.Trim();
                    string body = project.LoadCorreo("CuerpoResolucion")
                        .Replace("@productor", producerName)
                        .Replace("@titulo", project.project_name.Trim());
               
                    if (project.state_id ==10)
                    {
                        //esto se comenta por que cuando se aprueba o rechaza no debe existir ninguna diferencia en la notifiacion
                        //si fue rechazado 
                      /*subject = System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_SUBJECT"];
                      body = System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_SALUDO"] +"<br>"+ producerName;
                      body += "<br/><b>Titulo: " + project.project_name + "</b>";
                      body += "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_MENSAJE"];
                      body += "<br>" + System.Configuration.ConfigurationManager.AppSettings["NOTIFICATION_SECOND_PARAGRAPH"];
                      body = body + "<br/><br/><FONT  color='red' >" + System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_FIN"] + "</FONT>";
                      */
                    }
                    /* Envío de notificación al productor solicitante */
                    project.sendMailNotification(mailTo, subject, body,Server);
                }
               
            }
            catch (Exception crap)
            {
                lblErrorReolucion.Text = "Error al cargar el archivo " + crap.InnerException;
            }
        }

        protected void btnCargarResolution2_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
              Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
              Request.Form["back_to_revisor"] == "1" ||
              Request.Form["back_to_revisor2"] == "1" ||
              Request.Form["back_to_editor"] == "1" ||
              Request.Form["back_to_editor2"] == "1" ||
              Request.Form["pasaradirector_field"] == "1" ||
              Request.Form["pasaraeditor_field"] == "1" ||
              Request.Form["pasaraeditor2_field"] == "1" ||
              Request.Form["pasaradirector2_field"] == "1" ||
              Request.Form["specialaction"] == "undorequest" ||
              Request.Form["schedulefilmview_field"] == "1" ||
              Request.Form["resolution_date_field"] == "1" ||
              Request.Form["notification_date_field"] == "1" ||
              Request.Form["resolution_date2_field"] == "1" ||
              Request.Form["notification_date2_field"] == "1" ||
              (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
              )

                return;
            lblErrorReolucion2.Text = "";
            lblErrorReolucion.Text = "";
            if (!fileResolucion2.HasFile)
            {
                lblErrorReolucion2.Text = "Debe seleccionar un archivo";
                return;
            }

            if (System.IO.Path.GetExtension(fileResolucion2.FileName).ToLower() != ".pdf")
            {
                lblErrorReolucion2.Text = "solo es valido subir archivos en formato pdf.";
                return;
            }

            if (fileResolucion2.FileBytes.LongLength > 5242880)
            {
                lblErrorReolucion2.Text = "El archivo " + fileResolucion2.FileName + "  supera el tamaño máximo de 5 Megas.";
                return;
            }

            string path = Server.MapPath("~/uploads/resolutions/" + Session["project_id"]);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string nombre = DateTime.Now.Ticks.ToString().Substring(6) + "res.pdf";
            string fileName = Path.Combine(path, nombre);
            try
            {
                fileResolucion2.SaveAs(fileName);
                Resolution resolution = new Resolution();
                resolution.LoadByProject((int)Session["project_id"]);
                resolution.path2 = nombre;
                resolution.Save();
                lblErrorReolucion2.Text = "Resolución cargada satisfactoriamente";
                cargarResolucion2();
                /*enviamos el email al productor*/
                ///* Variable que almacena el correo electrónico y nombre del productor que hace la solicitud */
                string mailTo = "";
                string producerName = "";
                Project project = new Project();
                project.LoadProject((int)Session["project_id"]);
                ///* Buscamos el objeto del productor que hace la solicitud */
                int RequesterProducerTemp = project.producer.FindIndex(
                delegate(Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
                if (RequesterProducerTemp != -1)
                {
                    mailTo = project.producer[RequesterProducerTemp].producer_email;
                    producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
                }
                if (mailTo != "")
                {
                    string subject = System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_SUBJECT"]+" - "+producerName.Trim()+" - "+project.project_name.Trim() +"";
                    string body = System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_BODY_SALUDO"] + "<BR/><BR/>" + producerName;
                    body += "<br/><b>Titulo: " + project.project_name + "</b>";
                    body += "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_BODY_MENSAJE"];
                    body += "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["NOTIFICATION_SECOND_PARAGRAPH"];
                    body = body + "<br/><br/><FONT  color='red' >" + System.Configuration.ConfigurationManager.AppSettings["APPROVED_MAIL_BODY_FIN"] + "</FONT>";

                    if (project.state_id == 10)
                    {//si fue rechazado 
                        subject = System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_SUBJECT"] + " - " + producerName.Trim() + " - " + project.project_name.Trim() + "";
                        body = System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_SALUDO"] + "<br>" + producerName;
                        body += "<br/><b>Titulo: " + project.project_name + "</b>";
                        body += "<br/><br/>" + System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_MENSAJE"];
                        body += "<br>" + System.Configuration.ConfigurationManager.AppSettings["NOTIFICATION_SECOND_PARAGRAPH"];
                        body = body + "<br/><br/><FONT  color='red' >" + System.Configuration.ConfigurationManager.AppSettings["REJECTED_MAIL_BODY_FIN"] + "</FONT>";
                    }
                    /* Envío de notificación al productor solicitante */
                    project.sendMailNotification(mailTo, subject, body,Server);
                }

            }
            catch (Exception crap)
            {
                lblErrorReolucion2.Text = "Error al cargar el archivo " + crap.InnerException;
            }
        }


        protected void btnEliminarArchivoResolucion_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
              Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
              Request.Form["back_to_revisor"] == "1" ||
              Request.Form["back_to_revisor2"] == "1" ||
              Request.Form["back_to_editor"] == "1" ||
              Request.Form["back_to_editor2"] == "1" ||
              Request.Form["pasaradirector_field"] == "1" ||
              Request.Form["pasaraeditor_field"] == "1" ||
              Request.Form["pasaraeditor2_field"] == "1" ||
              Request.Form["pasaradirector2_field"] == "1" ||
              Request.Form["specialaction"] == "undorequest" ||
              Request.Form["schedulefilmview_field"] == "1" ||
              Request.Form["resolution_date_field"] == "1" ||
              Request.Form["notification_date_field"] == "1" ||
              (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
              )

                return;
            lblErrorReolucion.Text = "";
            lblErrorReolucion2.Text = "";
            Resolution resolution = new Resolution();
            resolution.LoadByProject((int)Session["project_id"]);

            resolution.path = "";
            resolution.Save();
            
            cargarResolucion();
        }

        protected void btnEliminarArchivoResolucion2_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
              Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
              Request.Form["back_to_revisor"] == "1" ||
              Request.Form["back_to_revisor2"] == "1" ||
              Request.Form["back_to_editor"] == "1" ||
              Request.Form["back_to_editor2"] == "1" ||
              Request.Form["pasaradirector_field"] == "1" ||
              Request.Form["pasaraeditor_field"] == "1" ||
              Request.Form["pasaraeditor2_field"] == "1" ||
              Request.Form["pasaradirector2_field"] == "1" ||
              Request.Form["specialaction"] == "undorequest" ||
              Request.Form["schedulefilmview_field"] == "1" ||
              Request.Form["resolution_date_field"] == "1" ||
              Request.Form["notification_date_field"] == "1" ||
              (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
              )

                return;
            lblErrorReolucion.Text = "";
            lblErrorReolucion2.Text = "";
            Resolution resolution = new Resolution();
            resolution.LoadByProject((int)Session["project_id"]);
            resolution.path2 = "";
            resolution.Save();
            
            cargarResolucion2();
        }


        protected void lnkResolucion_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
                Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
                Request.Form["back_to_revisor"] == "1" ||
                Request.Form["back_to_revisor2"] == "1" ||
                Request.Form["back_to_editor"] == "1" ||
                Request.Form["back_to_editor2"] == "1" ||
                Request.Form["pasaradirector_field"] == "1" ||
                Request.Form["pasaraeditor_field"] == "1" ||
                Request.Form["pasaraeditor2_field"] == "1" ||
                Request.Form["pasaradirector2_field"] == "1" ||
                Request.Form["specialaction"] == "undorequest" ||
                Request.Form["schedulefilmview_field"] == "1" ||
                Request.Form["resolution_date_field"] == "1" ||
                Request.Form["notification_date_field"] == "1" ||
                (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
                )

                return;

            Resolution resolution = new Resolution();
            resolution.LoadByProject((int)Session["project_id"]);

            //Response.ContentType = "Application/pdf";
           // Response.TransmitFile("~/uploads/resolutions/" + Session["project_id"] + "/" + resolution.path);

            //Response.Redirect("~/uploads/resolutions/" + Session["project_id"] + "/" + resolution.path); 

            Response.Write("<script>");
            Response.Write("window.open('../uploads/resolutions/" + Session["project_id"] + "/" + resolution.path+"', '_newtab');");
            Response.Write("</script>");
        }

        protected void lnkResolucion2_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
                Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
                Request.Form["back_to_revisor"] == "1" ||
                Request.Form["back_to_revisor2"] == "1" ||
                Request.Form["back_to_editor"] == "1" ||
                Request.Form["back_to_editor2"] == "1" ||
                Request.Form["pasaradirector_field"] == "1" ||
                Request.Form["pasaraeditor_field"] == "1" ||
                Request.Form["pasaraeditor2_field"] == "1" ||
                Request.Form["pasaradirector2_field"] == "1" ||
                Request.Form["specialaction"] == "undorequest" ||
                Request.Form["schedulefilmview_field"] == "1" ||
                Request.Form["resolution_date_field"] == "1" ||
                Request.Form["notification_date_field"] == "1" ||
                (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
                )

                return;

            Resolution resolution = new Resolution();
            resolution.LoadByProject((int)Session["project_id"]);
            //Response.Redirect("~/uploads/resolutions/" + Session["project_id"] + "/" + resolution.path2);

            Response.Write("<script>");
            Response.Write("window.open('../uploads/resolutions/" + Session["project_id"] + "/" + resolution.path2 + "', '_newtab');");
            Response.Write("</script>");
        }


        protected void btnAprobarFormulario_Click(object sender, EventArgs e)
        {
            //if (Request.Form["enviaraclaraciones_field"] == "1" ||
            //  Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
            //  Request.Form["back_to_revisor"] == "1" ||
            //  Request.Form["back_to_revisor2"] == "1" ||
            //  Request.Form["back_to_editor"] == "1" ||
            //  Request.Form["back_to_editor2"] == "1" ||
            //  Request.Form["pasaradirector_field"] == "1" ||
            //  Request.Form["pasaraeditor_field"] == "1" ||
            //  Request.Form["pasaraeditor2_field"] == "1" ||
            //  Request.Form["pasaradirector2_field"] == "1" ||
            //  Request.Form["specialaction"] == "undorequest" ||
            //  Request.Form["schedulefilmview_field"] == "1" ||
            //  Request.Form["resolution_date_field"] == "1" ||
            //  Request.Form["notification_date_field"] == "1" ||
            //  (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
            //  )

            //    return;
            int codPrj = 0;
             if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }
             if (Session["project_id"] != null)
             {
                codPrj=Convert.ToInt32(Session["project_id"]);
             }
            //executamos un query
             Project p = new Project();
             p.LoadProject(codPrj, true);
             if (p.state_id < 5)
             {
                 p.formulario_aprobado_pronda = true;
                 
             }else {
                 p.formulario_aprobado_sronda = true;
                 
             }
             rdFormularioAprobado.Checked = true;
             rdFormulariosinRevizar.Checked = false;
             rdFormularioNoAprobado.Checked = false;
             p.Save(true);
        }

        protected void btnRechazarFormulario_Click(object sender, EventArgs e)
        {
        //    if (Request.Form["enviaraclaraciones_field"] == "1" ||
        //      Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
        //      Request.Form["back_to_revisor"] == "1" ||
        //      Request.Form["back_to_revisor2"] == "1" ||
        //      Request.Form["back_to_editor"] == "1" ||
        //      Request.Form["back_to_editor2"] == "1" ||
        //      Request.Form["pasaradirector_field"] == "1" ||
        //      Request.Form["pasaraeditor_field"] == "1" ||
        //      Request.Form["pasaraeditor2_field"] == "1" ||
        //      Request.Form["pasaradirector2_field"] == "1" ||
        //      Request.Form["specialaction"] == "undorequest" ||
        //      Request.Form["schedulefilmview_field"] == "1" ||
        //      Request.Form["resolution_date_field"] == "1" ||
        //      Request.Form["notification_date_field"] == "1" ||
        //      (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
        //      )

        //        return;
            int codPrj = 0;
            if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }
            if (Session["project_id"] != null)
            {
                codPrj = Convert.ToInt32(Session["project_id"]);
            }
            //executamos un query
            Project p = new Project();
            p.LoadProject(codPrj, true);
            if (p.state_id < 5)
            {
                p.formulario_aprobado_pronda = false;
            }else{
                p.formulario_aprobado_sronda = false;
            }
            rdFormularioAprobado.Checked = false;
            rdFormulariosinRevizar.Checked = false;
            rdFormularioNoAprobado.Checked = true;
            p.Save(true);
        }

        protected void btnGuardarRespuestaVisualizacion_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
              Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
              Request.Form["back_to_revisor"] == "1" ||
              Request.Form["back_to_revisor2"] == "1" ||
              Request.Form["back_to_editor"] == "1" ||
              Request.Form["back_to_editor2"] == "1" ||
              Request.Form["pasaradirector_field"] == "1" ||
              Request.Form["pasaraeditor_field"] == "1" ||
              Request.Form["pasaraeditor2_field"] == "1" ||
              Request.Form["pasaradirector2_field"] == "1" ||
              Request.Form["specialaction"] == "undorequest" ||
              Request.Form["schedulefilmview_field"] == "1" ||
              Request.Form["resolution_date_field"] == "1" ||
              Request.Form["notification_date_field"] == "1" ||
              (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
              )

                return;
            int codPrj = 0;
            if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }
            if (Session["project_id"] != null)
            {
                codPrj = Convert.ToInt32(Session["project_id"]);
            }
            //executamos un query
            Project project = new Project();
            project.LoadProject(codPrj, true);
 
            project.observaciones_visualizacion_por_productor =txtRespuestaVisualizacion.Text;
            
            project.Save(true);
            lblErrorRespuestaVisualizacion.Text = "Se guardo la información satisfactoriamente.";
        }

        protected void btnCargarARchivo_Click(object sender, EventArgs e)
        {
            lblErrorAdjuntos.Text = "";
            if (!fileOtrosAdjuntos.HasFile)
            {
                lblErrorAdjuntos.Text = "Debe seleccionar un archivo";
                return;
            }
            if (System.IO.Path.GetExtension(fileOtrosAdjuntos.FileName).ToLower() != ".pdf")
            {
                lblErrorAdjuntos.Text = "solo es valido subir archivos en formato pdf.";
                return;
            }

            if (txtOtrosAdjuntos.Text == string.Empty)
            {
                lblErrorAdjuntos.Text = "Debe ingresar la descripcion archivo";
                return;
            }


            if (fileOtrosAdjuntos.FileBytes.LongLength > 5242880)
            {
                lblErrorAdjuntos.Text = "El archivo " + fileOtrosAdjuntos.FileName + "  supera el tamaño máximo de 5 Megas.";
                return;
            }

            string path = Server.MapPath("~/uploads/" + Session["project_id"]+"/");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            string nombreArchivo = DateTime.Now.Ticks.ToString().Substring(8) + fileOtrosAdjuntos.FileName;

            string fileName = Path.Combine(path, nombreArchivo);
            try
            {
                fileOtrosAdjuntos.SaveAs(fileName);
                //creamos el registro en la base de datos
                BD.dsCineTableAdapters.adjunto_projectoTableAdapter objAdjunto = new BD.dsCineTableAdapters.adjunto_projectoTableAdapter();
                objAdjunto.Insert("~/uploads/" + Session["project_id"] +"/"+ nombreArchivo, fileOtrosAdjuntos.FileName, txtOtrosAdjuntos.Text, false, int.Parse(lblCodProyecto.Text));

                Response.Redirect("Finalizacion.aspx?project_id=" + lblCodProyecto.Text);
            }
            catch (Exception crap)
            {
                lblErrorReolucion.Text = "Error al cargar el archivo " + crap.InnerException;
            }
        }

        protected void btnQuitarADjuntoADicional_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string tr=b.CommandArgument;
            BD.dsCineTableAdapters.adjunto_projectoTableAdapter objAdjunto = new BD.dsCineTableAdapters.adjunto_projectoTableAdapter();
            objAdjunto.UpdateEliminado(true, int.Parse(b.CommandArgument));

            Response.Redirect("Finalizacion.aspx?project_id=" + lblCodProyecto.Text);
        }

     

        protected void btnGuardarTExtoAdicional_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
             Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
             Request.Form["back_to_revisor"] == "1" ||
             Request.Form["back_to_revisor2"] == "1" ||
             Request.Form["back_to_editor"] == "1" ||
             Request.Form["back_to_editor2"] == "1" ||
             Request.Form["pasaradirector_field"] == "1" ||
             Request.Form["pasaraeditor_field"] == "1" ||
             Request.Form["pasaraeditor2_field"] == "1" ||
             Request.Form["pasaradirector2_field"] == "1" ||
             Request.Form["specialaction"] == "undorequest" ||
             Request.Form["schedulefilmview_field"] == "1" ||
             Request.Form["resolution_date_field"] == "1" ||
             Request.Form["notification_date_field"] == "1" || 
             Request.Form["resolution_date2_field"] == "1" ||
             Request.Form["notification_date2_field"] == "1" ||
             (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
             )

                return;
            int codPrj = 0;
            if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }
            if (Session["project_id"] != null)
            {
                codPrj = Convert.ToInt32(Session["project_id"]);
            }
            //executamos un query
            Project project = new Project();
            project.LoadProject(codPrj, true);

            project.complemento_carta_aclaraciones = txtComplementoCartaAclaraciones.Text;
            project.sustituto_carta_aclaracion = txtSustitutoCartaAclaraciones.Text;
            project.Save(true);
        }

        protected void btnGuardarInfoAdicional_Click(object sender, EventArgs e)
        {
            if (Request.Form["enviaraclaraciones_field"] == "1" ||
            Request.Form["enviarsolicituddeaclaraciones_field"] == "1" ||
            Request.Form["back_to_revisor"] == "1" ||
            Request.Form["back_to_revisor2"] == "1" ||
            Request.Form["back_to_editor"] == "1" ||
            Request.Form["back_to_editor2"] == "1" ||
            Request.Form["pasaradirector_field"] == "1" ||
            Request.Form["pasaraeditor_field"] == "1" ||
            Request.Form["pasaraeditor2_field"] == "1" ||
            Request.Form["pasaradirector2_field"] == "1" ||
            Request.Form["specialaction"] == "undorequest" ||
            Request.Form["schedulefilmview_field"] == "1" ||
            Request.Form["resolution_date_field"] == "1" ||
            Request.Form["notification_date_field"] == "1" ||
            Request.Form["resolution_date2_field"] == "1" ||
            Request.Form["notification_date2_field"] == "1" ||
            (Request.Form["submit_field"] != null && Request.Form["submit_field"].Trim() != string.Empty)
            )

                return;
            int codPrj = 0;
            if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }
            if (Session["project_id"] != null)
            {
                codPrj = Convert.ToInt32(Session["project_id"]);
            }
            Project project = new Project();
            project.LoadProject(codPrj, true);
            project.obs_adicional_finalizacion = comentarios_adicionales.Text;
            project.Save(true);
        }
        
    }
}