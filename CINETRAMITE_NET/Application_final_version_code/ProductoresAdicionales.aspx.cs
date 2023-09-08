using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto
{
    public partial class ProductoresAdicionales : System.Web.UI.Page
    {
        public int project_id;
        public bool ShowEditForm; //Variable para el control de la presentación del formulario
        public bool showAdvancedForm = false; //Variable que controla la presentación del formulario de administración

        public int project_state_id = 0; //Indica el estado del proyecto, el cual se utiliza para identificar los elementos a presentar particulares de cada estado
        public int section_state_id = 0; //Indica el estado de la sección actual, el cual se utiliza para identificar los elementos a presentar particulares de cada estado.

        public string tab_datos_productor_css_class = "";
        public string tab_datos_proyecto_css_class = "";
        public string tab_productores_adicionales_css_class = "";
        public string tab_datos_formato_personal_css_class = "";
        public string tab_datos_personal_css_class = "";
        public string tab_datos_adjuntos_css_class = "";
        public string tab_datos_finalizacion_css_class = "";
        public int producer_id_additional;
        public int user_role = 0;

        public bool showDomesticProducers = true;
        public bool showForeignProducers = false;
        public bool typeProducervalue = true;

        public string tab_datos_proyecto_revision_mark_image = "";
        public string tab_datos_productor_revision_mark_image = "";
        public string tab_datos_productores_adicionales_revision_mark_image = "";
        public string tab_datos_formato_personal_revision_mark_image = "";
        public string tab_datos_personal_revision_mark_image = "";
        public string tab_datos_adjuntos_revision_mark_image = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            var controlMaster = (System.Web.UI.HtmlControls.HtmlInputHidden)Master.FindControl("_scrollboton");
            controlMaster.Value = false.ToString();

            #region valida permisos y session
            /* Valida si hay un usuario en sesión de lo contrarió redirecciona a la 
            * página principal */
            if (Session["user_id"] == null || Convert.ToInt32(Session["user_id"]) <= 0)
            {
                Response.Redirect("Default.aspx", true);
            }

            /* Valida si el usuario tiene permiso de ver el formulario de administración */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();

                userObj.user_id = Convert.ToInt32(Session["user_id"]);
                this.user_role = userObj.GetUserRole(userObj.user_id);

                if (userObj.checkPermission("ver-formulario-gestion-solicitud"))
                {
                    this.showAdvancedForm = true;
                }
            }
            #endregion
            #region construye la tablas para los repetidores
            /* Crea un dataset para almacenar la informacion del personal que se vinculará al repetidor 
             * el cual se usa para presentar la información de los adjuntos de la sección a los administradores.
             */
            DataTable dtAttachment = new DataTable();
            dtAttachment.Columns.Add("attachment_id", typeof(int));
            dtAttachment.Columns.Add("attachment_render", typeof(string));
            dtAttachment.Columns.Add("attachment_father_id", typeof(int));
            dtAttachment.Columns.Add("attachment_company", typeof(string));
            DataSet attachmentDS = new DataSet();
            attachmentDS.Tables.Add(dtAttachment);

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Crea el objeto del proyecto para manejar la información */
            Project project = new Project();

            /* 
             * Crea una variable que controla si se presenta el formulario de registro
             * de datos del productor o la pantalla de resumen de productores adicionales
             */
            this.ShowEditForm = false;

            /* Crea un dataset para almacenar la informacion de los productores adicionales que se vinculan al repetidor */
            DataTable dtDomesticProducer = new DataTable();
            dtDomesticProducer.Columns.Add("producer_title", typeof(string));
            dtDomesticProducer.Columns.Add("producer_state_css_class", typeof(string));
            dtDomesticProducer.Columns.Add("producer_person_type", typeof(string));
            dtDomesticProducer.Columns.Add("producer_name", typeof(string));
            dtDomesticProducer.Columns.Add("producer_firstname", typeof(string));
            dtDomesticProducer.Columns.Add("producer_lastname", typeof(string));
            dtDomesticProducer.Columns.Add("producer_nit", typeof(string));
            dtDomesticProducer.Columns.Add("producer_identification_number", typeof(string));
            dtDomesticProducer.Columns.Add("producer_location", typeof(string));
            dtDomesticProducer.Columns.Add("producer_address", typeof(string));
            dtDomesticProducer.Columns.Add("producer_phone", typeof(string));
            dtDomesticProducer.Columns.Add("producer_movil", typeof(string));
            dtDomesticProducer.Columns.Add("producer_email", typeof(string));
            dtDomesticProducer.Columns.Add("producer_website", typeof(string));
            dtDomesticProducer.Columns.Add("action_form", typeof(string));

            DataSet AdditionalDomesticProducerDS = new DataSet();
            AdditionalDomesticProducerDS.Tables.Add(dtDomesticProducer);

            /* Crea un dataset para almacenar la informacion de los productores adicionales que se vinculan al repetidor */
            DataTable dtForeignProducer = new DataTable();
            dtForeignProducer.Columns.Add("producer_title", typeof(string));
            dtForeignProducer.Columns.Add("producer_state_css_class", typeof(string));
            dtForeignProducer.Columns.Add("producer_person_type", typeof(string));
            dtForeignProducer.Columns.Add("producer_name", typeof(string));
            dtForeignProducer.Columns.Add("producer_firstname", typeof(string));
            dtForeignProducer.Columns.Add("producer_lastname", typeof(string));
            dtForeignProducer.Columns.Add("producer_nit", typeof(string));
            dtForeignProducer.Columns.Add("producer_identification_number", typeof(string));
            dtForeignProducer.Columns.Add("producer_location", typeof(string));
            dtForeignProducer.Columns.Add("producer_address", typeof(string));
            dtForeignProducer.Columns.Add("producer_phone", typeof(string));
            dtForeignProducer.Columns.Add("producer_movil", typeof(string));
            dtForeignProducer.Columns.Add("producer_email", typeof(string));
            dtForeignProducer.Columns.Add("producer_website", typeof(string));
            dtForeignProducer.Columns.Add("producer_nacionalidad", typeof(string));
            dtForeignProducer.Columns.Add("action_form", typeof(string));

            DataSet AdditionalForeignProducerDS = new DataSet();
            AdditionalForeignProducerDS.Tables.Add(dtForeignProducer);
            #endregion
            /* Obtiene la información del proyecto seleccionado */
            if (lblCodProyecto.Text.Trim() != string.Empty)
            {
                Session["project_id"] = Convert.ToInt32(lblCodProyecto.Text);
            }
            else if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }

            if (Session["project_id"] != null)
            {
                lblCodProyecto.Text = Session["project_id"].ToString();
                project.LoadProject(Convert.ToInt32(Session["project_id"]));

                if (project.version == 2)//redirecciona a finalizacion nueva
                {
                    Response.Redirect("ProductoresAdicionales3.aspx", true);
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
                        if (project.state_id <= 1 && Session["superadmin"] == null)
                        {
                            Response.Redirect("Default.aspx", true);
                        }
                    }
                    else
                    {
                        if (project.project_idusuario != Convert.ToInt32(Session["user_id"]) && Session["superadmin"] == null)
                        {
                            Response.Redirect("Default.aspx", true);
                        }
                    }
                }
                #endregion
                /* Guarda en la variable de la clase el estado de la variable */
                this.project_state_id = project.state_id;
                this.section_state_id = project.sectionDatosProductoresAdicionales.tab_state_id;

                if (project_state_id == 5 && project.sectionDatosProductoresAdicionales.solicitud_aclaraciones_date.Year < 2005)
                {
                    project.sectionDatosProductoresAdicionales.observacion_inicial_date = DateTime.Now;
                    project.sectionDatosProductoresAdicionales.solicitud_aclaraciones_date = DateTime.Now;
                    project.sectionDatosProductoresAdicionales.aclaraciones_productor_date = DateTime.Now;
                    project.sectionDatosProductoresAdicionales.modified = DateTime.Now;
                    project.sectionDatosProductoresAdicionales.revision_state_id = 10;
                    project.sectionDatosProductoresAdicionales.tab_state_id = 10;
                    project.sectionDatosProductoresAdicionales.Save();
                }

                if (project.production_type_id == 2) //Agrega la información del productor extranjero a la lista
                {
                    showForeignProducers = true;
                }
                else
                {

                    showForeignProducers = false;
                }
                /* Hace la persistencia a la bd de la información de aclaraciones registrada por el productor */
                if (producer_clarifications_field.Value != "")
                {
                    project.sectionDatosProductoresAdicionales.aclaraciones_productor = producer_clarifications_field.Value;
                    project.sectionDatosProductoresAdicionales.aclaraciones_productor_date = DateTime.Now;

                    project.Save();
                }
                #region si es avanzado verifica que estado debe mostrar para el tab de prodcutores adicionales
                if (this.showAdvancedForm)
                {
                    if (Request.Form["save_producer_info_read_only"] != null)
                    {
                        if (Request.Form["save_producer_info_read_only"].ToString() != "combo")
                        {
                            controlMaster.Value = true.ToString();
                        }
                        /* Se pasan al objeto del proyecto los valores definidos en el formulario de administración para ser almacenados 
                           Este procedimiento de grabación solo se ejecuta si se envío el formulario de grabación de un productor.
                           en el caso de enviar el formulario de grabación desde el modo de solo lectura se ejecuta otro procedimiento
                           de grabación igual ubicado en al principio del script */

                        /* Interpretación del valor enviado del formulario para la gestión realizada */
                        project.sectionDatosProductoresAdicionales.revision_state_id = 0;

                        if (gestion_realizada_sin_revisar.Checked)
                        {
                            project.sectionDatosProductoresAdicionales.revision_state_id = 11;
                            project.sectionDatosProductoresAdicionales.tab_state_id = 11;
                        }
                        if (gestion_realizada_solicitar_aclaraciones.Checked)
                        {
                            project.sectionDatosProductoresAdicionales.revision_state_id = 10;
                            project.sectionDatosProductoresAdicionales.tab_state_id = 10;
                        }
                        if (gestion_realizada_informacion_correcta.Checked)
                        {
                            project.sectionDatosProductoresAdicionales.revision_state_id = 9;
                            project.sectionDatosProductoresAdicionales.tab_state_id = 9;
                        }

                        /* Valida si se modificó el texto de la solicitud de aclaraciones para grabarla y actualizar la fecha */
                        if (project.sectionDatosProductoresAdicionales.solicitud_aclaraciones != solicitud_aclaraciones.Value)
                        {
                            project.sectionDatosProductoresAdicionales.solicitud_aclaraciones = solicitud_aclaraciones.Value;
                            project.sectionDatosProductoresAdicionales.solicitud_aclaraciones_date = DateTime.Now;
                        }
                        /* Valida si se modificó el texto de la solicitud de la primera observación para grabarla y actualizar la fecha */
                        if (project.sectionDatosProductoresAdicionales.observacion_inicial != informacion_correcta.Value)
                        {
                            project.sectionDatosProductoresAdicionales.observacion_inicial = informacion_correcta.Value;
                            project.sectionDatosProductoresAdicionales.observacion_inicial_date = DateTime.Now;
                        }
                        project.sectionDatosProductoresAdicionales.modified = DateTime.Now;

                        /* Se almacena la información registrada sobre el estado de revisión de la pestaña */
                        if (estado_revision_sin_revisar.Checked)
                        {
                            project.sectionDatosProductoresAdicionales.revision_mark = "";
                        }
                        if (estado_revision_revisado.Checked)
                        {
                            project.sectionDatosProductoresAdicionales.revision_mark = "revisado";
                        }
                        if (estado_revision_aprobado.Checked)
                        {
                            project.sectionDatosProductoresAdicionales.revision_mark = "aprobado";
                        }

                        project.Save();
                    }
                }
                #endregion
                #region validacion de estilos de tabs
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
                }
                else if (project.state_id == 2)
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
                switch (project.sectionDatosProyecto.tab_state_id) /* Datos  de la obra */
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
                    if (project_state_id > 1 && project_state_id != 5)
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
                    if (project_state_id > 1 && project_state_id != 5)
                    {
                        tab_datos_productor_css_class = "tab_unmarked_inactive";
                    }
                }
                emtyform = project.validateNotInitForm("ProductoresAdicionales");
                switch (project.sectionDatosProductoresAdicionales.tab_state_id) /* Datos de los productores adicionales */
                {
                    case 10:
                        tab_productores_adicionales_css_class = "tab_incompleto_active";
                        break;
                    case 11:
                        tab_productores_adicionales_css_class = "tab_unmarked_active";
                        break;
                    case 9:
                        tab_productores_adicionales_css_class = "tab_completo_active";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_productores_adicionales_css_class = (project.ValidateProjectSection("ProductoresAdicionales")) ? "tab_completo_active" : "tab_incompleto_inactive";
                        }
                        else
                        {
                            tab_productores_adicionales_css_class = "tab_unmarked_active";
                        }

                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state_id > 1 && project_state_id != 5)
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
                        if (project_state_id > 1 && project_state_id != 5)
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
                    if (project_state_id > 1 && project_state_id != 5)
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
                    if (project_state_id > 1 && project_state_id != 5)
                    {
                        tab_datos_adjuntos_css_class = "tab_unmarked_inactive";
                    }
                }
                emtyform = project.validateNotInitForm("DatosFinalizacion");
                RequestForm form = new RequestForm(this.project_id);
                if (form.path == null || form.path.Trim() == string.Empty)
                {
                    project.sectionDatosFinalizacion.tab_state_id = 10;
                }

                switch (project.sectionDatosFinalizacion.tab_state_id) /* Datos finalizacion */
                {
                    case 10:
                        tab_datos_finalizacion_css_class = "tab_incompleto_inactive";
                        break;
                    case 11:
                        tab_datos_finalizacion_css_class = "tab_unmarked_inactive";
                        break;
                    case 9:
                        tab_datos_finalizacion_css_class = "tab_completo_inactive";
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_datos_finalizacion_css_class = (project.ValidateProjectSection("DatosFinalizacion")) ? "tab_completo_inactive" : "tab_incompleto_inactive";
                        }
                        else
                        {
                            tab_datos_finalizacion_css_class = "tab_unmarked_inactive";
                        }
                        break;
                }
                if (user_role <= 1)
                {
                    if (project_state_id > 1 && project_state_id != 5)
                    {
                        tab_datos_finalizacion_css_class = "tab_unmarked_inactive";
                    }
                }

            }
            #endregion
            /**************************************************/
            /* Se define la información del bloque contextual */
            nombre_proyecto.Text = (project.project_name.ToString() != "") ? project.project_name.ToString() : "Aún no se ha definido el nombre de la obra";
            tipo_produccion.Text = project.production_type_name.ToString();
            tipo_proyecto.Text = project.project_type_name.ToString();

            /* Buscamos el objeto del productor que hace la solicitud */
            int requesterProducerContextInfo = project.producer.FindIndex(
                delegate (Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
            if (requesterProducerContextInfo != -1)
            {

                if (project.producer[requesterProducerContextInfo].person_type_id == 2)
                    nombre_productor.Text = project.producer[requesterProducerContextInfo].producer_name;
                else
                    nombre_productor.Text = project.producer[requesterProducerContextInfo].producer_firstname.Trim() + " " + project.producer[requesterProducerContextInfo].producer_lastname.Trim();


                if (nombre_productor.Text.Trim() == string.Empty)
                {
                    nombre_productor.Text = project.producer[requesterProducerContextInfo].producer_firstname.Trim() + " " + project.producer[requesterProducerContextInfo].producer_lastname.Trim();
                }

                if (nombre_productor.Text.Trim() == string.Empty)
                {
                    nombre_productor.Text = project.producer[requesterProducerContextInfo].producer_email.Trim();
                }
            }
            opciones_adicionales.Text = "<a href=\"Lista.aspx\">Volver al listado de solicitudes</a>";
            /**************************************************/

            /* Si el proyecto ya tiene identificador y aun no está en sesión, es registrado en la variable de sesión */
            if (Session["project_id"] == null && project.project_id > 0)
            {
                Session["project_id"] = project.project_id;
            }
            #region carga los combos de tipo de persona, tipo de empresa y ciudad
            /* Obtiene las opciones para el select de tipo de persona */
            DataSet personTypeDS = db.GetSelectOptions("person_type", "person_type_id", "person_type_name");
            if (!Page.IsPostBack)
            {
                personTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < personTypeDS.Tables[0].Rows.Count; i++)
                {
                    personTypeDDL.Items.Add(new ListItem(personTypeDS.Tables[0].Rows[i]["person_type_name"].ToString(), personTypeDS.Tables[0].Rows[i]["person_type_id"].ToString()));
                }
            }

            /* Obtiene las opciones para el select de tipo de empresa */
            DataSet companyTypeDS = db.GetSelectOptions("producer_company_type", "producer_company_type_id", "producer_company_type_name");
            if (!Page.IsPostBack)
            {
                companyTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < companyTypeDS.Tables[0].Rows.Count; i++)
                {
                    companyTypeDDL.Items.Add(new ListItem(companyTypeDS.Tables[0].Rows[i]["producer_company_type_name"].ToString(), companyTypeDS.Tables[0].Rows[i]["producer_company_type_id"].ToString()));
                }
            }

            /* Obtiene las opciones para el select de tipo de documento */
            /*DataSet identificationTypeDS = db.GetSelectOptions("identification_type", "identification_type_id", "identification_type_name");
            if (!Page.IsPostBack)
            {
                identificationTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < personTypeDS.Tables[0].Rows.Count; i++)
                {
                    identificationTypeDDL.Items.Add(new ListItem(identificationTypeDS.Tables[0].Rows[i]["identification_type_name"].ToString(), identificationTypeDS.Tables[0].Rows[i]["identification_type_id"].ToString()));
                }
            }*/

            /* Obtiene las opciones para el select de departamentos */
            DataSet departamentoDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='0'");
            if (!Page.IsPostBack)
            {
                departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < departamentoDS.Tables[0].Rows.Count; i++)
                {
                    departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }
            }
            #endregion
            /* Valida si se envió un formulario para agregar productor, editar existente o grabar los datos de un productor. */
            if (Request.Form["change_producer_info"] != null || Request.Form["add_producer_info"] != null || Request.Form["save_producer_info"] != null)
            {
                #region si esta cargando el formulario de edicion de agregar o esta guardando
                /* Modifica la bandera que permite la presentación del formulario de registro de información */
                this.ShowEditForm = true;
                if (Request.Form["producer_type_origin"] != null && Request.Form["producer_type_origin"].Trim() == "1")
                {//nacional
                    pnlJuridicoExtranjero.Visible = false;
                    pnlJuridicoNacional.Visible = true;
                    divCedulaRep.Visible = true;
                }
                else
                {
                    pnlJuridicoExtranjero.Visible = true;
                    pnlJuridicoNacional.Visible = false;
                    divCedulaRep.Visible = false;
                }


                /* Obtiene los textos de los tooltips y los pasa a las funciones de javascript correspondientes */
                tooltip_personTypeDDL.Text = db.GetTooltip("personTypeDDL");
                tooltip_producer_firstname.Text = db.GetTooltip("producer_firstname");
                tooltip_producer_lastname.Text = db.GetTooltip("producer_lastname");
                tooltip_producer_identification_number.Text = db.GetTooltip("producer_identification_number");
                tooltip_producer_name.Text = db.GetTooltip("producer_name");

                tooltip_producer_firstname_juridica.Text = db.GetTooltip("producer_firstname_juridica");
                tooltip_producer_lastname_juridica.Text = db.GetTooltip("producer_lastname_juridica");

                tooltip_localization_out_of_colombia.Text = db.GetTooltip("localization_out_of_colombia");
                tooltip_departamentoDDL.Text = db.GetTooltip("departamentoDDL");
                tooltip_municipioDDL.Text = db.GetTooltip("municipioDDL");
                tooltip_producer_country.Text = db.GetTooltip("producer_country");
                tooltip_producer_city.Text = db.GetTooltip("producer_city");
                tooltip_producer_address.Text = db.GetTooltip("producer_address");
                tooltip_producer_phone.Text = db.GetTooltip("producer_phone");
                tooltip_producer_movil.Text = db.GetTooltip("producer_movil");
                tooltip_producer_email.Text = db.GetTooltip("producer_email");
                tooltip_producer_website.Text = db.GetTooltip("producer_website");


                if (Request.QueryString["project_id"] != null)
                {
                    Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
                }

                if (Session["project_id"] != null)
                {
                    project.LoadProject(Convert.ToInt32(Session["project_id"]));
                    project_id = project.project_id;
                }
                else
                {
                    Response.Redirect("Lista.aspx", true);
                }

                if (producer_type.Value != "1" && producer_type.Value != "2")
                {
                    producer_type.Value = Request.Form["producer_type_origin"];
                }


                if (producer_type.Value != "1")
                {
                    this.typeProducervalue = false;
                }
                //este codigo va aca y aplica para la creacion mas adelante se pone nuevamente y aplica para la edicion
                pnlNacionalidad.Visible = false;
                if (producer_type.Value == "2")
                {
                    pnlNacionalidad.Visible = true;
                }

                if (producer_id.Value == null || producer_id.Value == "")
                {
                    producer_id.Value = Request.Form["producer_id_origin"];
                }
                /* Proceso de precarga de información en el formulario para la edición 
                 * de un registro existente */
                if (Request.Form["change_producer_info"] != null)
                {
                    /* Buscamos el objeto del productor que hace se debe guardar */
                    int requesterProducer = project.producer.FindIndex(
                        delegate (Producer producerObj)
                        {
                            return producerObj.producer_id == Convert.ToInt32(producer_id.Value);
                        });
                    if (!(requesterProducer == -1))
                    {
                        this.producer_id_additional = project.producer[requesterProducer].producer_id;
                        producer_type.Value = project.producer[requesterProducer].producer_type_id.ToString();
                        if (producer_type.Value.Trim() == "1")
                        {//nacional
                            pnlJuridicoExtranjero.Visible = false;
                            pnlJuridicoNacional.Visible = true;
                        }
                        else
                        {
                            pnlJuridicoExtranjero.Visible = true;
                            pnlJuridicoNacional.Visible = false;
                        }

                        if (project.producer[requesterProducer].producer_type_id.ToString() == "1")
                        {
                            this.typeProducervalue = true;
                        }
                        personTypeDDL.SelectedValue = project.producer[requesterProducer].person_type_id.ToString();
                        producer_firstname.Value = project.producer[requesterProducer].producer_firstname;
                        producer_lastname.Value = project.producer[requesterProducer].producer_lastname;
                        producer_identification_number.Value = project.producer[requesterProducer].producer_identification_number;
                        producer_identification_number_juridica.Value = project.producer[requesterProducer].producer_identification_number;
                        producer_name.Value = project.producer[requesterProducer].producer_name;
                        companyTypeDDL.SelectedValue = project.producer[requesterProducer].producer_company_type_id.ToString();
                        producer_nit.Value = project.producer[requesterProducer].producer_nit;
                        producer_firstname_juridica.Value = project.producer[requesterProducer].producer_firstname;
                        producer_lastname_juridica.Value = project.producer[requesterProducer].producer_lastname;
                        //  identificationTypeDDL.SelectedValue = project.producer[requesterProducer].identification_type_id.ToString();
                        // identification_number_juridica.Value = project.producer[requesterProducer].producer_identification_number;
                        producer_country.Value = project.producer[requesterProducer].producer_country;
                        producer_city.Value = project.producer[requesterProducer].producer_city;
                        producer_address.Value = project.producer[requesterProducer].producer_address;
                        producer_phone.Value = project.producer[requesterProducer].producer_phone;
                        producer_movil.Value = project.producer[requesterProducer].producer_movil;
                        producer_email.Value = project.producer[requesterProducer].producer_email;
                        producer_website.Value = project.producer[requesterProducer].producer_website;
                        producer_nacionalidad.Value = project.producer[requesterProducer].producer_fax;

                        //este codigo va aca y aplica para la edicion mas atras se pone nuevamente y aplica para la creacion
                        pnlNacionalidad.Visible = false;
                        if (producer_type.Value == "2")
                        {
                            pnlNacionalidad.Visible = true;
                        }

                        /* Marca el departamento seleccionado en el select del departamento */
                        if (project.producer[requesterProducer].producer_localization_father_id == "")
                        {
                            departamentoDDL.SelectedValue = "0";
                        }
                        else
                        {
                            departamentoDDL.SelectedValue = project.producer[requesterProducer].producer_localization_father_id;
                        }

                        /* Carga las opciones de municipios segun el departamento seleccionado  */
                        DataSet municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.producer[requesterProducer].producer_localization_father_id + "'");
                        municipioDDL.Items.Clear();
                        municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                        for (int i = 0; i < municipioDS.Tables[0].Rows.Count; i++)
                        {
                            municipioDDL.Items.Add(new ListItem(municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                        }

                        /* Selecciona la opcion correcta en el dropdown */
                        if (project.producer[requesterProducer].producer_localization_id == "")
                        {
                            municipioDDL.SelectedValue = "0";
                        }
                        else
                        {
                            municipioDDL.SelectedValue = project.producer[requesterProducer].producer_localization_id;
                        }

                        /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                        if (project.producer[requesterProducer].producer_country == "")
                        {
                            localization_out_of_colombia.Checked = false;
                        }
                        else
                        {
                            localization_out_of_colombia.Checked = true;
                        }

                        /* Aplica formatos especiales - campos requeridos vacios - a los campos presentados en el formulario */
                        personTypeDDL.Attributes["Class"] = (project.producer[requesterProducer].person_type_id == 0) ? "required_field" : "";
                        producer_firstname.Attributes["Class"] = (project.producer[requesterProducer].producer_firstname == "") ? "required_field" : "";
                        producer_lastname.Attributes["Class"] = (project.producer[requesterProducer].producer_lastname == "") ? "required_field" : "";
                        producer_firstname_juridica.Attributes["Class"] = (project.producer[requesterProducer].producer_firstname == "") ? "required_field" : "";
                        producer_firstname_juridica.Attributes["Class"] = (project.producer[requesterProducer].producer_lastname == "") ? "required_field" : "";
                        producer_identification_number.Attributes["Class"] = (project.producer[requesterProducer].producer_identification_number == "") ? "required_field" : "";
                        producer_identification_number_juridica.Attributes["Class"] = (project.producer[requesterProducer].producer_identification_number == "") ? "required_field" : "";

                        //identification_number_juridica.Attributes["Class"] = (project.producer[requesterProducer].producer_identification_number == "") ? "required_field" : "";
                        producer_name.Attributes["Class"] = (project.producer[requesterProducer].producer_name == "") ? "required_field" : "";

                        producer_nit.Attributes["Class"] = (project.producer[requesterProducer].producer_nit == "") ? "required_field" : "";
                        // identificationTypeDDL.Attributes["Class"] = (project.producer[requesterProducer].identification_type_id == 0) ? "required_field" : "";
                        departamentoDDL.Attributes["Class"] = (departamentoDDL.SelectedValue == "0") ? "required_field" : "";
                        municipioDDL.Attributes["Class"] = (municipioDDL.SelectedValue == "0") ? "required_field" : "";
                        producer_country.Attributes["Class"] = (project.producer[requesterProducer].producer_country == "") ? "required_field" : "";
                        producer_city.Attributes["Class"] = (project.producer[requesterProducer].producer_city == "") ? "required_field" : "";
                        producer_address.Attributes["Class"] = (project.producer[requesterProducer].producer_address == "") ? "required_field" : "";
                        producer_phone.Attributes["Class"] = (project.producer[requesterProducer].producer_phone == "") ? "required_field" : "";
                        producer_movil.Attributes["Class"] = (project.producer[requesterProducer].producer_movil == "") ? "required_field" : "";
                        producer_email.Attributes["Class"] = (project.producer[requesterProducer].producer_email == "") ? "required_field" : "";
                    }
                }


                /* #### INCLUIMOS CODIGO DE ADJUNTOS PARA QUE SEA VISIBLE A TODOS SOLO QUE HA */
                /* LOS ADMINISTRADORES SE LES HACE VISIBLE EL CHECKBOX DE APROBAR O RECHAZAR EL ADJUNTO*/
                /* Agrega las opciones al repetidor */
                // se define el objeto attachment para cargar los tipo de adjuntos de la pestaña productores adicionales 
                Attachment adjuntoObj = new Attachment();
                // cargamos los tipos de adjuntos
                List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(project, 1, this.producer_id_additional, this.typeProducervalue);


                bool emtyform = project.validateNotInitForm("productoresAdicionales");
                if (producer_id_additional != 0)
                {
                    // iteración de los tipos de adjunto
                    foreach (Attachment item in adjuntos)
                    {
                        DataRow newRow = attachmentDS.Tables[0].NewRow();
                        // se define el objeto de tipo adjunto projecto
                        ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();
                        // se verifica si exite un adjunto ya cargado
                        projectAttachmentCurrent.loadAttachmentByFhaterAndProducerId(project.project_id, item.attachment_id, producer_id_additional);
                        int projectAttacmentId = 0;
                        if (projectAttachmentCurrent.project_attachment_id != 0)
                        {
                            projectAttacmentId = projectAttachmentCurrent.project_attachment_id;
                        }
                        newRow["attachment_id"] = item.attachment_id;
                        newRow["attachment_father_id"] = item.attachment_father_id;
                        newRow["attachment_render"] = project.renderAttachments(item.attachment_id,
                                                                                projectAttacmentId,
                                                                                item.attachment_name,
                                                                                projectAttachmentCurrent.project_attachment_path,
                                                                                showAdvancedForm,
                                                                                projectAttachmentCurrent.project_attachment_approved,
                                                                                item.attachment_required,
                                                                                this.project_state_id, this.user_role,
                                                                                projectAttachmentCurrent.nombre_original);
                        attachmentDS.Tables[0].Rows.Add(newRow);
                    }

                    /* Agrega las opciones al repetidor */
                    AttachmentRepeater.DataSource = attachmentDS;
                    AttachmentRepeater.DataBind();

                    //Agrega las opciones al repetidor uploadify
                    AttachmentRepeater2.DataSource = attachmentDS;
                    AttachmentRepeater2.DataBind();
                    /* ### FIN CODIGO ADJUNTOS ###*/
                    lblMensajeGuardarProductor.Text = "";
                }
                else
                {
                    if (producer_type.Value != "2")
                    {
                        //lblMensajeGuardarProductor.Text = "Debe primero guardar el productor para adjuntar archivos.";
                        lblMensajeGuardarProductor.Text = "Recuerde una vez guardado el Productor Adicional, deberá modificar este registro para adjuntar los archivos";
                        lblMensajeGuardarProductor.ForeColor = System.Drawing.Color.Black;
                        lblMensajeGuardarProductor.Font.Bold = true;
                    }
                }
                #endregion

                /* Procedimiento de persistencia del productor en la bd */
                if (Request.Form["save_producer_info"] != null)
                {
                    if (Request.Form["save_producer_info"].ToString() != "combo")
                    {
                        controlMaster.Value = true.ToString();
                    }
                    //guarda solo el prodcutor en caso de que los controles tengan valores
                    if (producer_type.Value.Trim() != string.Empty)
                    {
                        #region guardamos la informacion del prodcutor
                        /* Buscamos el objeto del productor que hace se debe guardar */
                        if (producer_id.Value == "")
                        {
                            producer_id.Value = "0";
                        }
                        int requesterProducer = project.producer.FindIndex(
                            delegate (Producer producerObj)
                            {
                                return producerObj.producer_id == Convert.ToInt32(producer_id.Value);
                            });

                        if (requesterProducer == -1)
                        {
                            /* Se inserta un nuevo productor en la lista de productores */
                            Producer newProducer = new Producer();
                            newProducer.person_type_id = Convert.ToInt32(personTypeDDL.SelectedValue);
                            if (newProducer.person_type_id == 1)
                            {
                                newProducer.producer_firstname = producer_firstname.Value;
                                newProducer.producer_lastname = producer_lastname.Value;
                                newProducer.producer_identification_number = producer_identification_number.Value;
                            }
                            else if (newProducer.person_type_id == 2)
                            {
                                newProducer.producer_firstname = producer_firstname_juridica.Value;
                                newProducer.producer_lastname = producer_lastname_juridica.Value;
                                newProducer.producer_nit = producer_nit.Value;
                                newProducer.producer_identification_number = producer_identification_number_juridica.Value;
                            }
                            else
                            {
                                newProducer.producer_firstname = "";
                                newProducer.producer_lastname = "";
                                newProducer.producer_identification_number = "";
                            }

                            newProducer.producer_name = producer_name.Value;
                            //newProducer.producer_nit = producer_nit.Value;
                            newProducer.producer_type_id = Convert.ToInt32(producer_type.Value);
                            //  newProducer.identification_type_id = (identificationTypeDDL.SelectedValue != "0")?Convert.ToInt32(identificationTypeDDL.SelectedValue):1;
                            newProducer.producer_company_type_id = (companyTypeDDL.SelectedValue != "0") ? Convert.ToInt32(companyTypeDDL.SelectedValue) : 1;
                            newProducer.producer_country = producer_country.Value;
                            newProducer.producer_city = producer_city.Value;
                            newProducer.producer_address = producer_address.Value;
                            newProducer.producer_phone = producer_phone.Value;
                            newProducer.producer_movil = producer_movil.Value;
                            newProducer.producer_email = producer_email.Value;
                            newProducer.producer_website = producer_website.Value;
                            newProducer.producer_fax = producer_nacionalidad.Value;
                            newProducer.producer_localization_id = Request.Form["selectedMunicipio"];
                            newProducer.requester = 0;

                            /* Se hace el manejo de datos de acuerdo a la ubicación del productor (dentro o fuera de colombia) */
                            if (localization_out_of_colombia.Checked)
                            {
                                newProducer.producer_localization_id = "0";
                            }
                            else
                            {
                                newProducer.producer_country = "";
                                newProducer.producer_city = "";
                            }

                            project.producer.Add(newProducer);
                        }
                        else
                        {
                            /* Se actualiza el productor actual */
                            if (personTypeDDL.SelectedValue != "0")
                                project.producer[requesterProducer].person_type_id = Convert.ToInt32(personTypeDDL.SelectedValue);

                            if (project.producer[requesterProducer].person_type_id == 1)
                            {
                                project.producer[requesterProducer].producer_firstname = producer_firstname.Value;
                                project.producer[requesterProducer].producer_lastname = producer_lastname.Value;
                                project.producer[requesterProducer].producer_identification_number = producer_identification_number.Value;
                            }
                            else if (project.producer[requesterProducer].person_type_id == 2)
                            {
                                project.producer[requesterProducer].producer_firstname = producer_firstname_juridica.Value;
                                project.producer[requesterProducer].producer_lastname = producer_lastname_juridica.Value;
                                project.producer[requesterProducer].producer_identification_number = producer_identification_number_juridica.Value;
                                //project.producer[requesterProducer].producer_identification_number = identification_number_juridica.Value;
                            }
                            else
                            {
                                project.producer[requesterProducer].producer_firstname = "";
                                project.producer[requesterProducer].producer_lastname = "";
                                project.producer[requesterProducer].producer_identification_number = "";
                            }

                            project.producer[requesterProducer].producer_name = producer_name.Value;
                            project.producer[requesterProducer].producer_nit = producer_nit.Value;
                            project.producer[requesterProducer].producer_type_id = Convert.ToInt32(producer_type.Value);
                            // project.producer[requesterProducer].identification_type_id = (identificationTypeDDL.SelectedValue != "0") ? Convert.ToInt32(identificationTypeDDL.SelectedValue) : 1;
                            project.producer[requesterProducer].producer_company_type_id = (companyTypeDDL.SelectedValue != "0") ? Convert.ToInt32(companyTypeDDL.SelectedValue) : 1;
                            project.producer[requesterProducer].producer_country = producer_country.Value;
                            project.producer[requesterProducer].producer_city = producer_city.Value;
                            project.producer[requesterProducer].producer_address = producer_address.Value;
                            project.producer[requesterProducer].producer_phone = producer_phone.Value;
                            project.producer[requesterProducer].producer_movil = producer_movil.Value;
                            project.producer[requesterProducer].producer_email = producer_email.Value;
                            project.producer[requesterProducer].producer_website = producer_website.Value;
                            project.producer[requesterProducer].producer_fax = producer_nacionalidad.Value;
                            project.producer[requesterProducer].producer_localization_id = Request.Form["selectedMunicipio"];
                            project.producer[requesterProducer].requester = 0;

                            /* Se hace el manejo de datos de acuerdo a la ubicación del productor (dentro o fuera de colombia) */
                            if (localization_out_of_colombia.Checked)
                            {
                                project.producer[requesterProducer].producer_localization_id = "0";
                            }
                            else
                            {
                                project.producer[requesterProducer].producer_country = "";
                                project.producer[requesterProducer].producer_city = "";
                            }
                        }

                        /* Se pasan al objeto del proyecto los valores definidos en el formulario de administración para ser almacenados 
                           Este procedimiento de grabación solo se ejecuta si se envío el formulario de grabación de un productor.
                           en el caso de enviar el formulario de grabación desde el modo de solo lectura se ejecuta otro procedimiento
                           de grabación igual ubicado en al principio del script */
                        if (this.showAdvancedForm)
                        {
                            /* Interpretación del valor enviado del formulario para la gestión realizada */
                            project.sectionDatosProductoresAdicionales.revision_state_id = 0;

                            if (gestion_realizada_sin_revisar.Checked)
                            {
                                project.sectionDatosProductoresAdicionales.revision_state_id = 11;
                                project.sectionDatosProductoresAdicionales.tab_state_id = 11;
                            }
                            if (gestion_realizada_solicitar_aclaraciones.Checked)
                            {
                                project.sectionDatosProductoresAdicionales.revision_state_id = 10;
                                project.sectionDatosProductoresAdicionales.tab_state_id = 10;
                            }
                            if (gestion_realizada_informacion_correcta.Checked)
                            {
                                project.sectionDatosProductoresAdicionales.revision_state_id = 9;
                                project.sectionDatosProductoresAdicionales.tab_state_id = 9;
                            }

                            /* Valida si se modificó el texto de la solicitud de aclaraciones para grabarla y actualizar la fecha */
                            if (project.sectionDatosProductoresAdicionales.solicitud_aclaraciones != solicitud_aclaraciones.Value)
                            {
                                project.sectionDatosProductoresAdicionales.solicitud_aclaraciones = solicitud_aclaraciones.Value;
                                project.sectionDatosProductoresAdicionales.solicitud_aclaraciones_date = DateTime.Now;
                            }
                            /* Valida si se modificó el texto de la solicitud de la primera observación para grabarla y actualizar la fecha */
                            if (project.sectionDatosProductoresAdicionales.observacion_inicial != informacion_correcta.Value)
                            {
                                project.sectionDatosProductoresAdicionales.observacion_inicial = informacion_correcta.Value;
                                project.sectionDatosProductoresAdicionales.observacion_inicial_date = DateTime.Now;
                            }
                            project.sectionDatosProductoresAdicionales.modified = DateTime.Now;
                        }

                        project.Save();

                        /* Si es administrador se almacena la información sobre aprobación de adjuntos */
                        foreach (ProjectAttachment item in project.attachment)
                        {
                            if (item.attachment.attachment_father_id == 1) //Solo los adjuntos de datos del productor y financiación
                            {
                                ProjectAttachment projectAttachmentObj = new ProjectAttachment();
                                string approve_var_name = "attachment_approved_" + item.attachment.attachment_id;
                                string id_attachment_Request = "attachment_approved_id_" + item.attachment.attachment_id;
                                string request = Request.Form[approve_var_name];
                                string idAttachment = Request.Form[id_attachment_Request];
                                if (Request.Form[approve_var_name] != null && Convert.ToInt32(Request.Form[approve_var_name]) > 0)
                                {
                                    projectAttachmentObj.LoadProjectAttachment(Convert.ToInt32(request));
                                    projectAttachmentObj.project_attachment_approved = 1;
                                    projectAttachmentObj.Save();
                                }
                                else
                                {
                                    projectAttachmentObj.LoadProjectAttachment(Convert.ToInt32(idAttachment));
                                    projectAttachmentObj.project_attachment_approved = 0;
                                    projectAttachmentObj.Save();
                                }
                            }
                        }

                        Response.Redirect("ProductoresAdicionales.aspx");
                        #endregion
                    }
                }
            }
            else
            {
                /* Carga la información de los productores adicionales en modo de solo lectura */
                if (Session["project_id"] != null)
                {
                    foreach (Producer producer in project.producer)
                    {
                        if (producer.requester == 0)
                        {
                            /* Obtiene el texto que se presenta en el encabezado de la informacion del productor */
                            string producer_title = "";
                            string producer_state_css_class = "";
                            #region carga la infomracion que aplica para todos los casos
                            if (producer.producer_name == "" && producer.producer_firstname == "" && producer.producer_lastname == "")
                            {
                                producer_title = "A&uacute;n no se ha definido la informaci&oacute;n de este productor, haga clic aqu&iacute; para definirla";
                            }
                            else if (producer.producer_name == "")
                            {
                                producer_title = producer.producer_firstname + " " + producer.producer_lastname;
                            }
                            else if (producer.producer_firstname == "" && producer.producer_lastname == "")
                            {
                                producer_title = producer.producer_name;
                            }
                            else
                            {
                                producer_title = producer.producer_name + " - " + producer.producer_firstname + " " + producer.producer_lastname;
                            }

                            /* Valida si el productor ya tiene toda la información requerida registrada */
                            if (producer_title == "")
                            {
                                producer_state_css_class = "producer_incomplete";
                            }
                            else
                            {
                                producer_state_css_class = "producer_complete";
                            }
                            #endregion
                            if (producer.producer_type_id == 1) //Agrega la información del productor colombiano a la lista
                            {
                                #region carga la informacion del prodcutor nacional
                                showDomesticProducers = true;
                                DataRow newRow = AdditionalDomesticProducerDS.Tables[0].NewRow();
                                bool informacionPendiente = false;
                                newRow["producer_title"] = producer_title;
                                newRow["producer_state_css_class"] = producer_state_css_class;
                                /*@todo obtener el nombre del tipo de persona de la bd */
                                if (producer.person_type_id == 1)
                                {
                                    newRow["producer_person_type"] = "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>Persona Natural</div></li>";
                                }
                                else if (producer.person_type_id == 2)
                                {
                                    newRow["producer_person_type"] = "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>Persona Jur&iacute;dica</div></li>";
                                }
                                else
                                {
                                    newRow["producer_person_type"] = "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>No definido</div></li>";
                                }


                                if (producer.person_type_id < 1)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_name"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>Nombre de la empresa:</div><div class='only_read_value'>" + producer.producer_name + "</div></li>" : "";
                                if (producer.person_type_id == 2 && producer.producer_name.Trim() == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_nit"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>NIT:</div><div class='only_read_value'>" + producer.producer_nit + "</div></li>" : "";
                                if (producer.person_type_id == 2 && producer.producer_nit.Trim() == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_firstname"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>Nombres del representante legal:</div><div class='only_read_value'>" + producer.producer_firstname + "</div></li>" : "<li><div class='only_read_label'>Nombres:</div><div class='only_read_value'>" + producer.producer_firstname + "</div></li>";
                                if (producer.producer_firstname.Trim() == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_lastname"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>Apellidos del representante legal:</div><div class='only_read_value'>" + producer.producer_lastname + "</div></li>" : "<li><div class='only_read_label'>Apellidos:</div><div class='only_read_value'>" + producer.producer_lastname + "</div></li>";
                                if (producer.producer_lastname.Trim() == string.Empty)
                                {
                                    informacionPendiente = true;
                                }


                                newRow["producer_identification_number"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>N&uacute;mero de identificaci&oacute;n  del representante legal:</div><div class='only_read_value'>" + producer.producer_identification_number + "</div></li>" : "<li><div class='only_read_label'>N&uacute;mero de identificaci&oacute;n :</div><div class='only_read_value'>" + producer.producer_identification_number + "</div></li>";
                                if (producer.producer_identification_number.Trim() == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_location"] = "<li><div class='only_read_label'>Ubicaci&oacute;n:</div><div class='only_read_value'>" + producer.GetLocationDescription() + "</div></li>";
                                if (producer.GetLocationDescription() == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                if ((producer.producer_country == string.Empty || producer.producer_city == string.Empty)
                                    && !(producer.producer_country == string.Empty && producer.producer_city == string.Empty)
                                    && producer.GetLocationDescription() != string.Empty
                                    )
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_address"] = (producer.producer_address == "") ? "" : "<li><div class='only_read_label'>Direcci&oacute;n:</div><div class='only_read_value'>" + producer.producer_address + "</div></li>";
                                if (producer.producer_address == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_phone"] = (producer.producer_phone == "") ? "" : "<li><div class='only_read_label'>Tel&eacute;fono:</div><div class='only_read_value'>" + producer.producer_phone + "</div></li>";
                                if (producer.producer_phone == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_movil"] = (producer.producer_movil == "") ? "" : "<li><div class='only_read_label'>Celular:</div><div class='only_read_value'>" + producer.producer_movil + "</div></li>";
                                if (producer.producer_movil == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_email"] = (producer.producer_email == "") ? "" : "<li><div class='only_read_label'>Correo electr&oacute;nico:</div><div class='only_read_value'>" + producer.producer_email + "</div></li>";
                                if (producer.producer_email == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_website"] = (producer.producer_website == "") ? "" : "<li><div class='only_read_label'>P&aacute;gina Web:</div><div class='only_read_value'>" + producer.producer_website + "</div></li>";


                                // se define el objeto attachment para cargar los tipo de adjuntos de la pestaña productores adicionales 
                                Attachment adjuntoObj = new Attachment();
                                // cargamos los tipos de adjuntos
                                List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(project, 1, producer.producer_id, true);
                                int cont = 0;
                                string attachmentComplete = "";
                                if (producer.producer_id != 0)
                                {
                                    #region carga adjuntos
                                    // iteración de los tipos de adjunto


                                    foreach (Attachment item in adjuntos)
                                    {
                                        // se define el objeto de tipo adjunto projecto
                                        ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();
                                        // se verifica si exite un adjunto ya cargado
                                        projectAttachmentCurrent.loadAttachmentByFhaterAndProducerId(project.project_id, item.attachment_id, producer.producer_id);
                                        if (projectAttachmentCurrent.project_attachment_id != 0)
                                        {
                                            if (projectAttachmentCurrent.project_attachment_approved == 1)
                                            {
                                                cont++;
                                            }
                                        }
                                        else
                                        {
                                            informacionPendiente = true;
                                        }
                                    }
                                    if (cont == adjuntos.Count)
                                    {
                                        attachmentComplete = "<input type='hidden' value='1' class='completeAttachment'/>";
                                        if ((project.state_id > 1 && user_role > 1) || (user_role <= 1 && (project.state_id == 5)))
                                        {
                                            string adjuntosPendientes = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Adjuntos Aprobados</b>";
                                            newRow["producer_title"] = producer_title + "" + adjuntosPendientes;
                                        }
                                    }
                                    else
                                    {
                                        attachmentComplete = "<input type='hidden' value='0' class='completeAttachment'/>";
                                        if ((project.state_id > 1 && user_role > 1) || (user_role <= 1 && (project.state_id == 5)))
                                        {
                                            string adjuntosPendientes = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Adjuntos Pendientes por Aprobar</b>";
                                            newRow["producer_title"] = producer_title + "" + adjuntosPendientes;
                                        }
                                    }
                                    #endregion
                                }



                                newRow["action_form"] = "<li>" + attachmentComplete + "<form action=\"\" method=\"post\" name=\"editproducer_" + producer.producer_id + "\"><input type='hidden' id='producer_id_origin' name='producer_id_origin' value='" + producer.producer_id + "' /><input  onclick='$(\"#loading\").show();' type='submit' name='change_producer_info' value='Por favor haga clic aquí para modificar la información del productor / Agregar adjuntos' /></form></li>";

                                if (user_role <= 1 && project.state_id <= 1 && informacionPendiente)
                                {
                                    newRow["producer_title"] = newRow["producer_title"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Información Pendiente</b>";
                                }

                                AdditionalDomesticProducerDS.Tables[0].Rows.Add(newRow);
                                #endregion
                            }
                            else if (producer.producer_type_id == 2) //Agrega la información del productor extranjero a la lista
                            {
                                #region carga la informacion del prodcutor extranjero
                                bool informacionPendiente = false;
                                showForeignProducers = true;
                                DataRow newRow = AdditionalForeignProducerDS.Tables[0].NewRow();

                                newRow["producer_title"] = producer_title;
                                newRow["producer_state_css_class"] = producer_state_css_class;

                                /*@todo obtener el nombre del tipo de persona de la bd */
                                // newRow["producer_person_type"] = (producer.person_type_id == 1) ? "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>Persona Natural</div></li>" : "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>Persona Jur&iacute;dica</div></li>";
                                if (producer.person_type_id == 1)
                                {
                                    newRow["producer_person_type"] = "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>Persona Natural</div></li>";
                                }
                                else if (producer.person_type_id == 2)
                                {
                                    newRow["producer_person_type"] = "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>Persona Jur&iacute;dica</div></li>";
                                }
                                else
                                {
                                    newRow["producer_person_type"] = "<li><div class='only_read_label'>Tipo de persona:</div><div class='only_read_value'>No definido</div></li>";
                                }


                                if (producer.person_type_id < 1)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_name"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>Nombre de la empresa:</div><div class='only_read_value'>" + producer.producer_name + "</div></li>" : "";
                                if (producer.person_type_id == 2 && producer.producer_name == string.Empty)
                                {
                                    informacionPendiente = true;
                                }


                                newRow["producer_nit"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>NIT:</div><div class='only_read_value'>" + producer.producer_nit + "</div></li>" : "";
                                if (producer.person_type_id == 2 && producer.producer_nit == string.Empty)
                                {
                                    //informacionPendiente = true;
                                    //en algun momemnto para las empresas juridicas no nacionales se le quito el nit
                                    newRow["producer_nit"] = "";
                                }

                                newRow["producer_firstname"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>Nombres del representante legal:</div><div class='only_read_value'>" + producer.producer_firstname + "</div></li>" : "<li><div class='only_read_label'>Nombres:</div><div class='only_read_value'>" + producer.producer_firstname + "</div></li>";
                                if (producer.producer_firstname == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_lastname"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>Apellidos del representante legal:</div><div class='only_read_value'>" + producer.producer_lastname + "</div></li>" : "<li><div class='only_read_label'>Apellidos:</div><div class='only_read_value'>" + producer.producer_lastname + "</div></li>";
                                if (producer.producer_lastname == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_identification_number"] = (producer.person_type_id == 2) ? "<li><div class='only_read_label'>N&uacute;mero de identificaci&oacute;n del representante legal:</div><div class='only_read_value'>" + producer.producer_identification_number + "</div></li>" : "<li><div class='only_read_label'>N&uacute;mero de identificaci&oacute;n:</div><div class='only_read_value'>" + producer.producer_identification_number + "</div></li>";
                                if (producer.producer_identification_number == string.Empty && producer.person_type_id == 2)
                                {
                                    informacionPendiente = true;
                                }

                                if (producer.producer_identification_number == string.Empty && producer.person_type_id == 1)
                                {
                                    newRow["producer_identification_number"] = "";
                                }

                                newRow["producer_location"] = "<li><div class='only_read_label'>Ubicaci&oacute;n:</div><div class='only_read_value'>" + producer.GetLocationDescription() + "</div></li>";
                                if (producer.GetLocationDescription() == string.Empty)
                                {
                                    informacionPendiente = true;
                                }
                                if ((producer.producer_country == string.Empty || producer.producer_city == string.Empty)
                                  && !(producer.producer_country == string.Empty && producer.producer_city == string.Empty)
                                  && producer.GetLocationDescription() != string.Empty
                                  )
                                {
                                    informacionPendiente = true;
                                }
                                newRow["producer_address"] = (producer.producer_address == "") ? "" : "<li><div class='only_read_label'>Direcci&oacute;n:</div><div class='only_read_value'>" + producer.producer_address + "</div></li>";
                                if (producer.producer_address == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_phone"] = (producer.producer_phone == "") ? "" : "<li><div class='only_read_label'>Tel&eacute;fono:</div><div class='only_read_value'>" + producer.producer_phone + "</div></li>";
                                if (producer.producer_phone == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_movil"] = (producer.producer_movil == "") ? "" : "<li><div class='only_read_label'>Celular:</div><div class='only_read_value'>" + producer.producer_movil + "</div></li>";
                                if (producer.producer_movil == string.Empty)
                                {
                                    informacionPendiente = true;
                                }

                                newRow["producer_email"] = (producer.producer_email == "") ? "" : "<li><div class='only_read_label'>Correo electr&oacute;nico:</div><div class='only_read_value'>" + producer.producer_email + "</div></li>";
                                if (producer.producer_email == string.Empty)
                                {
                                    informacionPendiente = true;
                                }
                                newRow["producer_website"] = (producer.producer_website == "") ? "" : "<li><div class='only_read_label'>P&aacute;gina Web:</div><div class='only_read_value'>" + producer.producer_website + "</div></li>";

                                newRow["producer_nacionalidad"] = (producer.producer_fax == "") ? "" : "<li><div class='only_read_label'>Nacionalidad:</div><div class='only_read_value'>" + producer.producer_fax + "</div></li>";

                                // se define el objeto attachment para cargar los tipo de adjuntos de la pestaña productores adicionales 
                                Attachment adjuntoObj = new Attachment();
                                // cargamos los tipos de adjuntos
                                List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(project, 1, 0, false);
                                int cont = 0;
                                string attachmentComplete = "";
                                if (producer.producer_id != 0)
                                {
                                    // iteración de los tipos de adjunto


                                    foreach (Attachment item in adjuntos)
                                    {
                                        // se define el objeto de tipo adjunto projecto
                                        ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();
                                        // se verifica si exite un adjunto ya cargado
                                        projectAttachmentCurrent.loadAttachmentByFhaterAndProducerId(project.project_id, item.attachment_id, producer.producer_id);
                                        if (projectAttachmentCurrent.project_attachment_id != 0)
                                        {
                                            if (projectAttachmentCurrent.project_attachment_approved == 1)
                                            {
                                                cont++;
                                            }
                                        }
                                        else
                                        {
                                            //los extranejeros no tienen adjuntos
                                            //informacionPendiente = true;
                                        }
                                    }
                                    if (cont == adjuntos.Count)
                                    {
                                        attachmentComplete = "<input type='hidden' value='1' class='completeAttachment'/>";
                                    }
                                    else
                                    {
                                        attachmentComplete = "<input type='hidden' value='0' class='completeAttachment'/>";
                                    }
                                }

                                newRow["action_form"] = "<li>" + attachmentComplete + "<form action=\"\" method=\"post\" name=\"editproducer_" + producer.producer_id + "\"><input type='hidden' id='producer_id_origin' name='producer_id_origin' value='" + producer.producer_id + "' /><input type='submit' onclick='$(\"#loading\").show();'  name='change_producer_info' value='Por favor haga clic aquí para modificar la información del productor' /></form></li>";

                                if (user_role <= 1 && project.state_id <= 1 && informacionPendiente)
                                {
                                    newRow["producer_title"] = newRow["producer_title"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Información Pendiente</b>";
                                }
                                AdditionalForeignProducerDS.Tables[0].Rows.Add(newRow);
                                #endregion
                            }
                        }
                    }

                    /* Se completa el dataset con los productores que hagan falta según la cantidad de 
                     * productores registrados en el formulario de datos de la obra 
                     */
                    int currentDomesticProducersQty = AdditionalDomesticProducerDS.Tables[0].Rows.Count;
                    int currentForeignProducersQty = AdditionalForeignProducerDS.Tables[0].Rows.Count;

                    for (int i = currentDomesticProducersQty; i < project.project_domestic_producer_qty - 1; i++)
                    {
                        DataRow newRow = AdditionalDomesticProducerDS.Tables[0].NewRow();
                        newRow["producer_title"] = "A&uacute;n no se ha definido la informaci&oacute;n de este productor";
                        newRow["producer_state_css_class"] = "producer_incomplete";

                        newRow["producer_name"] = "";
                        newRow["producer_nit"] = "";
                        newRow["producer_firstname"] = "";
                        newRow["producer_lastname"] = "";
                        newRow["producer_identification_number"] = "";
                        newRow["producer_location"] = "";
                        newRow["producer_address"] = "";
                        newRow["producer_phone"] = "";
                        newRow["producer_movil"] = "";
                        newRow["producer_email"] = "";
                        newRow["producer_website"] = "";
                        newRow["action_form"] = "<li><form action=\"\" method=\"post\" name=\"newproducer\"><input type='hidden' id='producer_type_origin' name='producer_type_origin' value='1' /><input onclick='$(\"#loading\").show();'  type='submit' name='add_producer_info' value='Por favor haga clic aquí para registrar la información del productor'  /></form></li>";

                        AdditionalDomesticProducerDS.Tables[0].Rows.Add(newRow);
                    }

                    for (int i = currentForeignProducersQty; i < project.project_foreign_producer_qty; i++)
                    {
                        DataRow newRow = AdditionalForeignProducerDS.Tables[0].NewRow();
                        newRow["producer_title"] = "A&uacute;n no se ha definido la informaci&oacute;n de este productor";
                        newRow["producer_state_css_class"] = "producer_incomplete";

                        newRow["producer_name"] = "";
                        newRow["producer_nit"] = "";
                        newRow["producer_firstname"] = "";
                        newRow["producer_lastname"] = "";
                        newRow["producer_identification_number"] = "";
                        newRow["producer_location"] = "";
                        newRow["producer_address"] = "";
                        newRow["producer_phone"] = "";
                        newRow["producer_movil"] = "";
                        newRow["producer_email"] = "";
                        newRow["producer_website"] = "";
                        newRow["producer_nacionalidad"] = "";
                        newRow["action_form"] = "<li><form action=\"\" method=\"post\" name=\"newproducer\"><input type='hidden' id='producer_type_origin' name='producer_type_origin' value='2' /><input onclick='$(\"#loading\").show();' type='submit' name='add_producer_info' value='Por favor haga clic aquí para registrar la información del productor' /></form></li>";

                        AdditionalForeignProducerDS.Tables[0].Rows.Add(newRow);
                    }

                    // Define el datasource del repetidor
                    AdditionalDomesticProducer.DataSource = AdditionalDomesticProducerDS;
                    AdditionalForeignProducer.DataSource = AdditionalForeignProducerDS;

                    // Carga la informacion del dataset en el repetidor  
                    AdditionalDomesticProducer.DataBind();
                    AdditionalForeignProducer.DataBind();
                }
            }
            #region formulario avanzado y estados de cada tab
            if (this.showAdvancedForm)
            {
                /* Carga en el formulario el valor que ha sido recuperado de la base de datos 
                 * para el checkbox del estado del formulario
                 */
                gestion_realizada_sin_revisar.Checked = false;
                gestion_realizada_solicitar_aclaraciones.Checked = false;
                gestion_realizada_informacion_correcta.Checked = false;

                if (project.sectionDatosProductoresAdicionales.revision_state_id == 11)
                {
                    gestion_realizada_sin_revisar.Checked = true;
                }
                if (project.sectionDatosProductoresAdicionales.revision_state_id == 10)
                {
                    gestion_realizada_solicitar_aclaraciones.Checked = true;
                }
                if (project.sectionDatosProductoresAdicionales.revision_state_id == 9)
                {
                    gestion_realizada_informacion_correcta.Checked = true;
                }

                /* Crea las etiquetas que incluyen la imagen que indica el estado de la marca
                * de revisión en cada pestaña y hace la persistencia en el formulario de acuerdo
                * a los valores guardados en la base de datos */
                estado_revision_sin_revisar.Checked = false;
                estado_revision_revisado.Checked = false;
                estado_revision_aprobado.Checked = false;

                if (project.sectionDatosProductoresAdicionales.revision_mark == "")
                {
                    estado_revision_sin_revisar.Checked = true;
                }
                if (project.sectionDatosProductoresAdicionales.revision_mark == "revisado")
                {
                    estado_revision_revisado.Checked = true;
                }
                if (project.sectionDatosProductoresAdicionales.revision_mark == "aprobado")
                {
                    estado_revision_aprobado.Checked = true;
                }

                /*
                 * Carga en el formulario los textos registrados por los
                 * administradores del trámite
                 */
                solicitud_aclaraciones.Value = project.sectionDatosProductoresAdicionales.solicitud_aclaraciones;
                informacion_correcta.Value = project.sectionDatosProductoresAdicionales.observacion_inicial;

                /* Consulta el estado de la marca de revisión de cada formulario para presentar en la pestaña 
                 * el indicador correspondiente. */
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
            #endregion
            /* Agrega al formulario la información relacionada con la solicitud de aclaraciones */
            if (project.sectionDatosProductoresAdicionales.solicitud_aclaraciones != "")
            {
                clarification_request.Text = project.sectionDatosProductoresAdicionales.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                if (user_role > 1 && project_state_id >= 6)
                {
                    clarification_request_summary.Text = project.sectionDatosProductoresAdicionales.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    producer_clarification_summary.Text = project.sectionDatosProductoresAdicionales.aclaraciones_productor.Replace("\r\n", "<br>");
                }
            }
            /* Recupera al formulario la información de aclaraciones registrada por el productor */
            producer_clarifications_field.Value = project.sectionDatosProductoresAdicionales.aclaraciones_productor;
        }
    }
}