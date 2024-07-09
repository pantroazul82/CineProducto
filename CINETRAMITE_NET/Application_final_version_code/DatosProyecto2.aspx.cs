using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CineProducto.Bussines;
using System.Data;
using System.Globalization;
using DominioCineProducto;
using DominioCineProducto.Data;

namespace CineProducto
{
    public partial class DatosProyecto2 : System.Web.UI.Page
    {
        public int project_id; //Variable que hace disponible el id del proyecto actual en la plantilla

        /* Atributo que permite controlar la presentación del campo de información 
         * del laboratorio de revelado */
        public bool showProjectDevelopmentLabInfo;
        public int shootingFormatMMCounter;
        public int shootingFormatItemsQty;
        public bool showAdvancedForm = false; //Variable que controla la presentación del formulario de administración
        public bool showLogging = false; //Variable que controla la presentación del listado histórico
        public bool otherFilmingFormatChecked = false;
        public bool otherExhibicionFormatChecked = false;


        public string otherFilmingFormatDetail = "";
        public string otherExhibicionFormatDetail = "";
        public int project_state_id = 0; //Indica el estado del proyecto, el cual se utiliza para identificar los elementos a presentar particulares de cada estado
        public int section_state_id = 0; //Indica el estado de la sección actual, el cual se utiliza para identificar los elementos a presentar particulares de cada estado.
        public int user_role = 0;

        public string tab_datos_productor_css_class = "";
        public string tab_datos_proyecto_css_class = "";
        public string tab_productores_adicionales_css_class = "";
        public string tab_datos_formato_personal_css_class = "";
        public string tab_datos_personal_css_class = "";
        public string tab_datos_adjuntos_css_class = "";
        public string tab_datos_finalizacion_css_class = "";

        public string tab_datos_proyecto_revision_mark_image = "";
        public string tab_datos_productor_revision_mark_image = "";
        public string tab_datos_productores_adicionales_revision_mark_image = "";
        public string tab_datos_formato_personal_revision_mark_image = "";
        public string tab_datos_personal_revision_mark_image = "";
        public string tab_datos_adjuntos_revision_mark_image = "";

        protected void Page_Load(object sender, EventArgs e)
        {

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
                if (userObj.checkPermission("ver-historial-cambios"))
                {
                    this.showLogging = true;
                }
            }


            /* Crea un dataset para almacenar la informacion del personal que se vinculará al repetidor 
             * el cual se usa para presentar la información de los adjuntos de la sección a los administradores.
             */
            DataTable dtAttachment = new DataTable();
            dtAttachment.Columns.Add("attachment_id", typeof(int));
            //dtAttachment.Columns.Add("attachment_name", typeof(string));
            //dtAttachment.Columns.Add("attachment_checkbox", typeof(string));            
            dtAttachment.Columns.Add("attachment_render", typeof(string));
            dtAttachment.Columns.Add("attachment_father_id", typeof(int));
            //nuevas columnas para soportar el nuevo control
            dtAttachment.Columns.Add("estadoProyecto", typeof(int));
            dtAttachment.Columns.Add("codigoProyecto", typeof(int));
            dtAttachment.Columns.Add("opcional", typeof(bool));
            dtAttachment.Columns.Add("codigoAdjunto", typeof(int));
            dtAttachment.Columns.Add("nombreAdjunto", typeof(string));
            dtAttachment.Columns.Add("url", typeof(string));
            dtAttachment.Columns.Add("FileNameCaption", typeof(string));
            dtAttachment.Columns.Add("aprobado", typeof(bool));

            DataSet attachmentDS = new DataSet();
            attachmentDS.Tables.Add(dtAttachment);

            /* Define la region */
            CultureInfo culture = new CultureInfo("es-CO");

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Crea el objeto del proyecto para manejar la información */
            Project project = new Project();

            /* Se definen variables auxiliares */
            int shootingFormatCounter = 0;
            int exhibitionFormatCounter = 0;
            shootingFormatMMCounter = 0;
            shootingFormatItemsQty = 0;

            if (!IsPostBack)
            {
                /* Obtiene las opciones para el select de departamentos */
                DataSet departamentoDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='0'");

                departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < departamentoDS.Tables[0].Rows.Count; i++)
                {
                    departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }
            }

            /* Incialización de la variable de control de presentación del 
             campo de información del laboratorio */
            showProjectDevelopmentLabInfo = false;
            if (!IsPostBack)
            {
                cmbIdiomaPrincipal.SelectedValue = "0";
                cmbIdiomaPrincipal.DataBind();
            }
            #region carga los dropdownlist
            /* Obtiene las opciones para el select de tipo de producción */
            DataSet productionTypeDS = db.GetSelectOptions("production_type", "production_type_id", "production_type_name");
            if (!Page.IsPostBack)
            {
                productionTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < productionTypeDS.Tables[0].Rows.Count; i++)
                {
                    productionTypeDDL.Items.Add(new ListItem(productionTypeDS.Tables[0].Rows[i]["production_type_name"].ToString(), productionTypeDS.Tables[0].Rows[i]["production_type_id"].ToString()));
                }
            }
            /* Obtiene las opciones para el select de tipo de producción */
            DataSet personalTypeDS = db.GetSelectOptions("personal_type", "personal_type_id", "personal_type_name");
            if (!Page.IsPostBack)
            {
                personalTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < personalTypeDS.Tables[0].Rows.Count; i++)
                {
                    personalTypeDDL.Items.Add(new ListItem(personalTypeDS.Tables[0].Rows[i]["personal_type_name"].ToString(), personalTypeDS.Tables[0].Rows[i]["personal_type_id"].ToString()));
                }
            }
            /* Obtiene las opciones para el select de tipo de empresa */


            /* Obtiene las opciones para el select del tipo de proyecto */
            DataSet projectTypeDS = db.GetSelectOptions("project_type", "project_type_id", "project_type_name");
            if (!Page.IsPostBack)
            {
                projectTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < projectTypeDS.Tables[0].Rows.Count; i++)
                {
                    projectTypeDDL.Items.Add(new ListItem(projectTypeDS.Tables[0].Rows[i]["project_type_name"].ToString(), projectTypeDS.Tables[0].Rows[i]["project_type_id"].ToString()));
                }
            }

            /* Obtiene las opciones para la segunda clasificación del tipo de proyecto (ficcion, documental, etc.) */
            DataSet projectGenreDS = db.GetSelectOptions("project_genre", "project_genre_id", "project_genre_name");
            if (!Page.IsPostBack)
            {
                for (int i = 0; i < projectGenreDS.Tables[0].Rows.Count; i++)
                {
                    projectGenreDDL.Items.Add(new ListItem(projectGenreDS.Tables[0].Rows[i]["project_genre_name"].ToString(), projectGenreDS.Tables[0].Rows[i]["project_genre_id"].ToString()));
                }
            }

            /* Obtiene las opciones para los checkbox del formato de rodaje */
            DataSet projectShootingFormatDS = db.GetSelectOptions("format", "format_id", "format_name", "format_type_id = 1");
            shootingFormatItemsQty = projectShootingFormatDS.Tables[0].Rows.Count;

            if (!Page.IsPostBack)
            {
                grdFormatosRodaje.DataSource = projectShootingFormatDS.Tables[0];
                grdFormatosRodaje.DataBind();
            }

            /* Obtiene las opciones para los checkbox del formato de exhibicion */
            DataSet projectExhibitionFormatDS = db.GetSelectOptions("format", "format_id", "format_name", "format_type_id = 2");
            if (!Page.IsPostBack)
            {
                for (int i = 0; i < projectExhibitionFormatDS.Tables[0].Rows.Count; i++)
                {
                    if (projectExhibitionFormatDS.Tables[0].Rows[i]["format_id"].ToString() == "25")
                    {
                        continue;//no agregamos la casilla otro a la grilla
                    }

                    projectExhibitionFormatCBL.Items.Add(new ListItem(projectExhibitionFormatDS.Tables[0].Rows[i]["format_name"].ToString(), projectExhibitionFormatDS.Tables[0].Rows[i]["format_id"].ToString()));
                }
            }
            #endregion
            /* Obtiene los textos de los tooltips y los pasa a las funciones de javascript correspondientes */
            #region cargatoolTip
            tooltip_project_name.Text = db.GetTooltip("project_name");
            if (tooltip_project_name.Text.Trim() == string.Empty) tooltip_project_name.Visible = false;


            tooltip_project_provisional_name.Text = db.GetTooltip("project_provisional_name");
            if (tooltip_project_provisional_name.Text.Trim() == string.Empty) tooltip_project_provisional_name.Visible = false;

            tooltip_txtPaginaWeb.Text = db.GetTooltip("pagina_web");
            if (tooltip_txtPaginaWeb.Text.Trim() == string.Empty) tooltip_txtPaginaWeb.Visible = false;

            tooltip_txtPaginaFacebook.Text = db.GetTooltip("pagina_facebook");
            if (tooltip_txtPaginaFacebook.Text.Trim() == string.Empty) tooltip_txtPaginaFacebook.Visible = false;

            tooltip_productionTypeDDL.Text = db.GetTooltip("productionTypeDDL");
            if (tooltip_productionTypeDDL.Text.Trim() == string.Empty) tooltip_productionTypeDDL.Visible = false;

            tooltip_project_domestic_producer_qty.Text = db.GetTooltip("project_domestic_producer_qty");
            if (tooltip_project_domestic_producer_qty.Text.Trim() == string.Empty) tooltip_project_domestic_producer_qty.Visible = false;

            tooltip_project_foreign_producer_qty.Text = db.GetTooltip("project_foreign_producer_qty");
            if (tooltip_project_foreign_producer_qty.Text.Trim() == string.Empty) tooltip_project_foreign_producer_qty.Visible = false;

            tooltip_project_total_cost_desarrollo.Text = db.GetTooltip("project_total_cost_desarrollo");
            if (tooltip_project_total_cost_desarrollo.Text.Trim() == string.Empty) tooltip_project_total_cost_desarrollo.Visible = false;

            tooltip_project_total_cost_preproduccion.Text = db.GetTooltip("project_total_cost_preproduccion");
            if (tooltip_project_total_cost_preproduccion.Text.Trim() == string.Empty) tooltip_project_total_cost_preproduccion.Visible = false;

            tooltip_project_total_cost_produccion.Text = db.GetTooltip("project_total_cost_produccion");
            if (tooltip_project_total_cost_produccion.Text.Trim() == string.Empty) tooltip_project_total_cost_produccion.Visible = false;

            tooltip_project_total_cost_posproduccion.Text = db.GetTooltip("project_total_cost_posproduccion");
            if (tooltip_project_total_cost_posproduccion.Text.Trim() == string.Empty) tooltip_project_total_cost_posproduccion.Visible = false;

            tooltip_project_total_cost_promotion.Text = db.GetTooltip("project_total_cost_promotion");
            if (tooltip_project_total_cost_promotion.Text.Trim() == string.Empty) tooltip_project_total_cost_promotion.Visible = false;

            tooltip_projectTypeDDL.Text = db.GetTooltip("projectTypeDDL");
            if (tooltip_projectTypeDDL.Text.Trim() == string.Empty) tooltip_projectTypeDDL.Visible = false;

            tooltip_projectGenreDDL.Text = db.GetTooltip("projectGenreDDL");
            if (tooltip_projectGenreDDL.Text.Trim() == string.Empty) tooltip_projectGenreDDL.Visible = false;

            tooltip_project_synopsis.Text = db.GetTooltip("project_synopsis");


            tooltip_project_recording_sites.Text = db.GetTooltip("project_recording_sites");
            if (tooltip_project_recording_sites.Text.Trim() == string.Empty) tooltip_project_recording_sites.Visible = false;

            tooltip_project_duration_minutes.Text = db.GetTooltip("project_duration_minutes");
            if (tooltip_project_duration_minutes.Text.Trim() == string.Empty) tooltip_project_duration_minutes.Visible = false;

            tooltip_project_duration_seconds.Text = db.GetTooltip("project_duration_seconds");
            if (tooltip_project_duration_seconds.Text.Trim() == string.Empty) tooltip_project_duration_seconds.Visible = false;

            tooltip_project_filming_start_date.Text = db.GetTooltip("project_filming_start_date");
            if (tooltip_project_filming_start_date.Text.Trim() == string.Empty) tooltip_project_filming_start_date.Visible = false;

            tooltip_project_filming_end_date.Text = db.GetTooltip("project_filming_end_date");
            if (tooltip_project_filming_end_date.Text.Trim() == string.Empty) tooltip_project_filming_end_date.Visible = false;

            tooltip_project_filming_date_obs.Text = db.GetTooltip("project_filming_date_obs");
            if (tooltip_project_filming_date_obs.Text.Trim() == string.Empty) tooltip_project_filming_date_obs.Visible = false;

            //tooltip_projectShootingFormatCBL.Text = db.GetTooltip("projectShootingFormatCBL");
            //if (tooltip_projectShootingFormatCBL.Text.Trim() == string.Empty) tooltip_projectShootingFormatCBL.Visible = false;

            tooltip_projectExhibitionFormatCBL.Text = db.GetTooltip("projectExhibitionFormatCBL");
            if (tooltip_projectExhibitionFormatCBL.Text.Trim() == string.Empty) tooltip_projectExhibitionFormatCBL.Visible = false;

            tooltip_project_development_lab_info.Text = db.GetTooltip("project_development_lab_info");
            if (tooltip_project_development_lab_info.Text.Trim() == string.Empty) tooltip_project_development_lab_info.Visible = false;

            tooltip_project_preprint_store_info.Text = db.GetTooltip("project_preprint_store_info");
            if (tooltip_project_preprint_store_info.Text.Trim() == string.Empty) tooltip_project_preprint_store_info.Visible = false;

            tooltip_project_legal_deposit_yes.Text = db.GetTooltip("project_legal_deposit_yes");
            if (tooltip_project_legal_deposit_yes.Text.Trim() == string.Empty) tooltip_project_legal_deposit_yes.Visible = false;

            tooltip_project_legal_deposit_no.Text = db.GetTooltip("project_legal_deposit_no");
            if (tooltip_project_legal_deposit_no.Text.Trim() == string.Empty) tooltip_project_legal_deposit_no.Visible = false;

            tooltip_project_percentage.Text = db.GetTooltip("project_percentage");
            if (tooltip_project_percentage.Text.Trim() == string.Empty) tooltip_project_percentage.Visible = false;

            string tooltip = db.GetTooltip("project_legal_deposit_img");
            if (tooltip != null && tooltip != string.Empty)
            {
                imgDepositoLegal.ToolTip = tooltip;
            }

            tooltip = db.GetTooltip("project_lugar_img");
            if (tooltip != null && tooltip != string.Empty)
            {
                imgLugarDeposito.ToolTip = tooltip;
            }

            #endregion

            /* Obtiene la información del proyecto que esté en sesión */
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
                project_id = project.project_id;
            }
            else
            {
                Response.Redirect("Lista.aspx", true);
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


            /**************************************************/
            /* Se define la información del bloque contextual */
            nombre_proyecto.Text = (project.project_name.ToString() != "") ? project.project_name.ToString() : "Aún no se ha definido el nombre de la obra";
            tipo_produccion.Text = project.production_type_name.ToString();
            tipo_proyecto.Text = project.project_type_name.ToString();
            /* Buscamos el objeto del productor que hace la solicitud */
            int requesterProducer = project.producer.FindIndex(
                delegate (Producer producerObj)
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

            //opciones_adicionales.Text = "<a href =\"Lista.aspx\"><< Volver al listado de solicitudes</a>";
            opciones_adicionales.Text = "<a href =\"Lista.aspx\"><< Volver al listado de solicitudes</a>";


            /**************************************************/
            var controlMaster = (System.Web.UI.HtmlControls.HtmlInputHidden)Master.FindControl("_scrollboton");
            controlMaster.Value = false.ToString();
            /* Valida si fue enviado el formulario */
            if (Request.Form["submit_project_data"] != null)
            {
                if (Request.Form["submit_project_data"].ToString() != "combo")
                {
                    controlMaster.Value = true.ToString();
                }
                #region este codigo se ejecuta cuando oprimen el boton guardar, asigna valores al objeto proyecto y los guarda en la base de datos
                /* Si el nombre de la obra es diferente de vacio se asigna el nombre de lo contrario no se hace la modificación */
                project.project_name = (project_name.Value == "") ? project.project_name.ToUpper() : project_name.Value.ToUpper();
                
                project.titulo_provisional = project_provisional_name.Value.ToUpper();
                project.pagina_facebook = txtPaginaFacebook.Text;

                project.tiene_reconocimiento = cmbReconocimientoNal.SelectedValue;
                project.ano_resolucion = txtAnoResolucion.Text;
                project.num_resolucion = txtNumeroResolucion.Text;
                if (cmbPremio.SelectedValue == "1")
                {
                    project.tiene_estimulos = "Si";
                }
                else
                {
                    project.tiene_estimulos = "No";
                    NegocioCineProducto neg = new NegocioCineProducto();
                    neg.eliminarEstimulosProjecto(Convert.ToInt32(Session["project_id"]));  //borra los estimujlos pq dijo que no
                    GridViewEstimulos.DataBind();
                }

                if (cmbExhibida.SelectedValue == "1")
                    project.exhibida_publicamente = "Si";
                else
                    project.exhibida_publicamente = "No";

                project.no_acta_deposito = no_acta_deposito.Text;


                project.fdc = cmbFDC.SelectedValue;
                project.fdc_especificacion = txtFdcEspecificiacion.Text;
                project.ibermedia = cmbIbermedia.SelectedValue;
                project.ibermedia_especificacion = txtIberrmediaEspecificacion.Text;
                project.otros_estimulos = txtOtrosEspecifique.Text;

                project.pagina_web = txtPaginaWeb.Text;

                //si cambia el tipo de projecto o el genero se debe reiniciar el personal
                if (project.project_type_id != Convert.ToInt32(projectTypeDDL.SelectedValue) || project.project_genre_id != Convert.ToInt32(projectGenreDDL.SelectedValue)
                    || project.production_type_id != Convert.ToInt32(productionTypeDDL.SelectedValue))
                {
                    project.project_has_domestic_director = -1;
                }

                project.production_type_id = Convert.ToInt32(productionTypeDDL.SelectedValue);                
                project.project_type_id = Convert.ToInt32(projectTypeDDL.SelectedValue);
                project.project_genre_id = Convert.ToInt32(projectGenreDDL.SelectedValue);               

                project.project_personal_type = Convert.ToInt32(personalTypeDDL.SelectedValue);
                project.project_domestic_producer_qty = (project_domestic_producer_qty.Value == "") ? 0 : Convert.ToInt32(project_domestic_producer_qty.Value);
                project.project_foreign_producer_qty = (project_foreign_producer_qty.Value == "") ? 0 : Convert.ToInt32(project_foreign_producer_qty.Value);
                project.project_total_cost_desarrollo = (project_total_cost_desarrollo.Value == "") ? 0 : Convert.ToInt64(project_total_cost_desarrollo.Value);
                project.project_total_cost_preproduccion = (project_total_cost_preproduccion.Value == "") ? 0 : Convert.ToInt64(project_total_cost_preproduccion.Value);
                project.project_total_cost_produccion = (project_total_cost_produccion.Value == "") ? 0 : Convert.ToInt64(project_total_cost_produccion.Value);
                project.project_total_cost_posproduccion = (project_total_cost_posproduccion.Value == "") ? 0 : Convert.ToInt64(project_total_cost_posproduccion.Value);
                project.project_total_cost_promotion = (project_total_cost_promotion.Value == "") ? 0 : Convert.ToInt64(project_total_cost_promotion.Value);
                decimal porcentaje = 0;
                decimal.TryParse(project_percentage.Value.TrimEnd('%').Replace(",", ".").Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator), out porcentaje);
                project.project_percentage = porcentaje;
                project.project_synopsis = project_synopsis.Value;
                project.project_recording_sites = project_recording_sites.Text;
                project.SetProjectDuration(Convert.ToInt32(project_duration_minutes.Value), Convert.ToInt32(project_duration_seconds.Value));
               

              

                if (project_filming_start_date.Value != "")
                {
                    project.project_filming_start_date = Convert.ToDateTime(project_filming_start_date.Value, culture);
                }
                if (project_filming_end_date.Value != "")
                {
                    project.project_filming_end_date = Convert.ToDateTime(project_filming_end_date.Value, culture);
                }
                project.project_filming_date_obs = project_filming_date_obs.Value;
                project.project_development_lab_info = project_development_lab_info.Value;
                                 
                string valueMunicipio = Request.Form["selectedMunicipio"];
                if (valueMunicipio == "0")
                {
                    project.municipio_lab = municipioDDL.SelectedValue;
                }
                else
                {
                    project.municipio_lab = Request.Form["selectedMunicipio"];
                }
                project.municipio_lab_otro = txtMunicipioOtro.Text;
                project.nombre_lab = txtNombreLabRev.Value;
                project.otros_idiomas = txtOtrosIdiomas.Value;
                
                //project.no_acta_deposito = project_development_lab_info.Value;
                //project.otros_idiomas = project_development_lab_info.Value;




                
                project.project_preprint_store_info = project_preprint_store_info.Value;
                project.obs_adicional_obra = comentarios_adicionales.Value;
                project.cod_idioma = int.Parse(cmbIdiomaPrincipal.SelectedValue);

                if (cmbPremio.SelectedValue == "-1")
                {
                    project.tiene_premio = null;
                }
                else
                {
                    project.tiene_premio = cmbPremio.SelectedValue == "1";
                    //project.premio = premioTxt.Value;
                }
                if (project_legal_deposit_yes.Checked)
                {
                    project.project_legal_deposit = 1;
                }
                else if (project_legal_deposit_no.Checked)
                {
                    project.project_legal_deposit = 0;
                }
                else
                {
                    project.project_legal_deposit = -1;
                }

                /* reinicializa la lista de formatos del projecto para evitar elementos repetidos */
                project.project_format = new List<Format>();

                /* Carga la información de los formatos de rodaje y exhibición */
                for (int i = 0; i < grdFormatosRodaje.Rows.Count; i++)
                {
                    var check = (CheckBox)grdFormatosRodaje.Rows[i].Cells[0].FindControl("chkFormatoRodaje");
                    var text = (TextBox)grdFormatosRodaje.Rows[i].Cells[0].FindControl("txtDetalle");
                    if (check.Checked)
                    {
                        Format newformat = new Format();
                        newformat.LoadFormat(Convert.ToInt32(check.ValidationGroup));
                        newformat.format_project_detail = text.Text;
                        project.project_format.Add(newformat);
                    }
                }

                /* Valida si se seleccionó el checkbox para otro formato de rodaje */
                if (Request.Form["otro_formato_rodaje"] != null)
                {
                    int otroFormatoRodaje = Convert.ToInt32(Request.Form["otro_formato_rodaje"]);
                    Format newformat = new Format();
                    newformat.LoadFormat(Convert.ToInt32(otroFormatoRodaje));
                    if (Request.Form["otro_formato_rodaje_detail"] != null)
                    {
                        newformat.format_project_detail = Request.Form["otro_formato_rodaje_detail"];
                    }
                    project.project_format.Add(newformat);
                }

                if (cmbFormatoRodaje.SelectedValue != "")
                {
                    Format newformat = new Format();
                    newformat.LoadFormat(Convert.ToInt32(cmbFormatoRodaje.SelectedValue));
                    newformat.format_project_detail = newformat.format_name;
                    project.project_format.Add(newformat);
                }


                for (int i = 0; i < projectExhibitionFormatCBL.Items.Count; i++)
                {
                    if (projectExhibitionFormatCBL.Items[i].Selected)
                    {
                        Format newformat = new Format();
                        newformat.LoadFormat(Convert.ToInt32(projectExhibitionFormatCBL.Items[i].Value));
                        project.project_format.Add(newformat);
                    }
                }

                if (chkOtroFormatoExhibicion.Checked)
                {
                    Format newformat = new Format();

                    newformat.LoadFormat(25);
                    newformat.format_project_detail = txtOtro_formato_exibicion_detail.Text;
                    project.project_format.Add(newformat);
                }

                /* Hace la persistencia a la bd de la información de aclaraciones registrada por el productor */
                if (producer_clarifications_field.Value != "")
                {
                    project.sectionDatosProyecto.aclaraciones_productor = producer_clarifications_field.Value;
                    project.sectionDatosProyecto.aclaraciones_productor_date = DateTime.Now;
                }

                project.Save();

                /* Se pasan al objeto del proyecto los valores definidos en el formulario de administración para ser almacenados */
                if (this.showAdvancedForm)
                {
                    /* Interpretación del valor enviado del formulario para la gestión realizada */
                    project.sectionDatosProyecto.revision_state_id = 0;

                    if (gestion_realizada_sin_revisar.Checked)
                    {
                        project.sectionDatosProyecto.revision_state_id = 11;
                        project.sectionDatosProyecto.tab_state_id = 11;
                    }
                    if (gestion_realizada_solicitar_aclaraciones.Checked)
                    {
                        project.sectionDatosProyecto.revision_state_id = 10;
                        project.sectionDatosProyecto.tab_state_id = 10;
                    }
                    if (gestion_realizada_informacion_correcta.Checked)
                    {
                        project.sectionDatosProyecto.revision_state_id = 9;
                        project.sectionDatosProyecto.tab_state_id = 9;
                    }

                    /* Valida si se modificó el texto de la solicitud de aclaraciones para grabarla y actualizar la fecha */
                    if (project.sectionDatosProyecto.solicitud_aclaraciones != solicitud_aclaraciones.Value)
                    {
                        project.sectionDatosProyecto.solicitud_aclaraciones = solicitud_aclaraciones.Value;
                        project.sectionDatosProyecto.solicitud_aclaraciones_date = DateTime.Now;
                    }
                    /* Valida si se modificó el texto de la solicitud de la primera observación para grabarla y actualizar la fecha */
                    if (project.sectionDatosProyecto.observacion_inicial != informacion_correcta.Value)
                    {
                        project.sectionDatosProyecto.observacion_inicial = informacion_correcta.Value;
                        project.sectionDatosProyecto.observacion_inicial_date = DateTime.Now;
                    }
                    project.sectionDatosProyecto.modified = DateTime.Now;

                    /* Se almacena la información registrada sobre el estado de revisión de la pestaña */
                    if (estado_revision_sin_revisar.Checked)
                    {
                        project.sectionDatosProyecto.revision_mark = "";
                    }
                    if (estado_revision_revisado.Checked)
                    {
                        project.sectionDatosProyecto.revision_mark = "revisado";
                    }
                    if (estado_revision_aprobado.Checked)
                    {
                        project.sectionDatosProyecto.revision_mark = "aprobado";
                    }

                    project.Save();

                    /* Si es administrador se almacena la información sobre aprobación de adjuntos */
                    foreach (ProjectAttachment item in project.attachment)
                    {
                        if (item.attachment.attachment_father_id == 7) //Solo los adjuntos de datos de la obra
                        {
                            ProjectAttachment projectAttachmentObj = new ProjectAttachment();
                            string approve_var_name = "attachment_approved_" + item.attachment.attachment_id;

                            if (Request.Form[approve_var_name] != null && Convert.ToInt32(Request.Form[approve_var_name]) > 0)
                            {
                                projectAttachmentObj.LoadProjectAttachment(Convert.ToInt32(Request.Form[approve_var_name]));
                                projectAttachmentObj.project_attachment_approved = 1;
                                projectAttachmentObj.Save();
                            }
                            else
                            {
                                projectAttachmentObj.LoadProjectAttachment(item.project_attachment_id);
                                projectAttachmentObj.project_attachment_approved = 0;
                                projectAttachmentObj.Save();
                            }

                        }
                    }

                    project.LoadProject(project.project_id);
                }
                #endregion
            }

            if (Session["project_id"] == null && project.project_id > 0)
            {
                Session["project_id"] = project.project_id;
            }
            if (Session["project_id"] != null)
            {
                /* Guarda en la variable de la clase el estado de la variable */
                this.project_state_id = project.state_id;
                this.section_state_id = project.sectionDatosProyecto.tab_state_id;

                bool subsanacion = false;

                NegocioCineProducto proj = new NegocioCineProducto();
                project myProject = proj.getProject((int)Session["project_id"]);
                var product = Session["ES_PRODUCTOR"].ToString();
                if (bool.Parse(Session["ES_PRODUCTOR"].ToString()))
                {
                    if (myProject.FECHA_SUBSANACION.HasValue && myProject.SUBSANADO.HasValue == true && myProject.FECHA_SUBSANACION > DateTime.Now && myProject.SUBSANACION_ENVIADA == false)
                    {
                        //Mira el hidden
                        subsanacion = true;
                        hdHabilitarForm.Value = "Activo";
                        this.project_state_id = 5;
                    }
                }


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
                #region ajusta el estuilo de cada tab
                /* Se identifica y define el estilo de la pestaña a presentar para cada formulario de acuerdo
                 * a su estado actual */
                bool emtyform = project.validateNotInitForm("DatosProyecto");
                switch (project.sectionDatosProyecto.tab_state_id) /* Datos del proyecto */
                {
                    case 10:
                        tab_datos_proyecto_css_class = "tab_incompleto_active";
                        break;
                    case 11:
                        tab_datos_proyecto_css_class = "tab_unmarked_active";
                        break;
                    case 9:
                        tab_datos_proyecto_css_class = "tab_completo_active";
                        if (bool.Parse(Session["ES_PRODUCTOR"].ToString()))
                        {
                            if (myProject.FECHA_SUBSANACION.HasValue && myProject.SUBSANADO.HasValue == true && myProject.FECHA_SUBSANACION > DateTime.Now && myProject.SUBSANACION_ENVIADA == false)
                            {
                                hdHabilitarForm.Value = "Inactivo";
                            }
                        }
                        break;
                    default:
                        if (!emtyform)
                        {
                            tab_datos_proyecto_css_class = (project.ValidateProjectSection("DatosProyecto")) ? "tab_completo_active" : "tab_incompleto_active";
                        }
                        else
                        {
                            tab_datos_proyecto_css_class = "tab_unmarked_active";
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
                    (project.production_type_id >= 2 && project.project_foreign_producer_qty <= 0 && project.project_domestic_producer_qty <= 1)
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
                #endregion
                project_name.Value = project.project_name;

                cmbIdiomaPrincipal.SelectedValue = project.cod_idioma.ToString();
                if (project.tiene_premio.HasValue)
                {
                    cmbPremio.SelectedValue = (project.tiene_premio.Value) ? "1" : "2";
                    //premioTxt.Value = project.premio;
                }
                else
                {
                    cmbPremio.SelectedValue = "-1";
                }

                project_provisional_name.Value = project.titulo_provisional;
                txtPaginaFacebook.Text = project.pagina_facebook;

                cmbReconocimientoNal.SelectedValue = project.tiene_reconocimiento;
                txtAnoResolucion.Text = project.ano_resolucion;
                txtNumeroResolucion.Text = project.num_resolucion;

                if (project.tiene_estimulos == "Si")
                    cmbPremio.SelectedValue = "1";
                else if (project.tiene_estimulos == "No")
                    cmbPremio.SelectedValue = "2";


                if (project.exhibida_publicamente == "Si")
                    cmbExhibida.SelectedValue = "1";
                else if (project.exhibida_publicamente == "No")
                    cmbExhibida.SelectedValue = "2";

                no_acta_deposito.Text = project.no_acta_deposito;
               

                if (project.fdc != "")
                    cmbFDC.SelectedValue = project.fdc;
                txtFdcEspecificiacion.Text = project.fdc_especificacion;

                if (project.ibermedia != "")
                    cmbIbermedia.SelectedValue = project.ibermedia;

                txtIberrmediaEspecificacion.Text = project.ibermedia_especificacion;
                txtOtrosEspecifique.Text = project.otros_estimulos;

                txtPaginaWeb.Text = project.pagina_web;
                productionTypeDDL.SelectedValue = project.production_type_id.ToString();
                projectTypeDDL.SelectedValue = project.project_type_id.ToString();
                if (project.project_type_id > 0 && project_duration_minutes.Value != "")
                {
                    switch (project.project_type_id)
                    {
                        case 1:
                            if (Convert.ToInt32(project_duration_minutes.Value) < 70) { lblErrorMinutes.Text = "La obra debe ser mayor de 70 minutos"; } else { lblErrorMinutes.Text = ""; }
                            break;
                        case 2:
                            if (Convert.ToInt32(project_duration_minutes.Value) < 52) { lblErrorMinutes.Text = "La obra debe ser mayor de 52 minutos"; } else { lblErrorMinutes.Text = ""; }
                            break;
                        case 3:
                            if (Convert.ToInt32(project_duration_minutes.Value) < 7 || Convert.ToInt32(project_duration_minutes.Value) > 69) { lblErrorMinutes.Text = "La obra debe ser mayor de 7 minutos y menor de 70 minutos"; } else { lblErrorMinutes.Text = ""; }
                            break;
                    }


                }
                projectGenreDDL.SelectedValue = project.project_genre_id.ToString();
                personalTypeDDL.SelectedValue = project.project_personal_type.ToString();
                project_domestic_producer_qty.Value = project.project_domestic_producer_qty.ToString();
                project_foreign_producer_qty.Value = project.project_foreign_producer_qty.ToString();
                project_total_cost_desarrollo.Value = project.project_total_cost_desarrollo.ToString();
                project_total_cost_preproduccion.Value = project.project_total_cost_preproduccion.ToString();
                project_total_cost_produccion.Value = project.project_total_cost_produccion.ToString();
                project_total_cost_posproduccion.Value = project.project_total_cost_posproduccion.ToString();
                project_total_cost_promotion.Value = project.project_total_cost_promotion.ToString();
                project_percentage.Value = project.project_percentage.ToString().Replace(",", ".").Replace(".000", "");
                //si tiene 0 a la derecha los quitamos
                while (project_percentage.Value.LastIndexOf('0') == (project_percentage.Value.Length - 1) && project_percentage.Value.IndexOf('.') > 0)
                {
                    project_percentage.Value = project_percentage.Value.Substring(0, project_percentage.Value.Length - 1);
                }

                lblErrorPorcentaje.Text = "";
                if (productionTypeDDL.SelectedValue == "1" && project.project_percentage < 51)
                    lblErrorPorcentaje.Text = "El porcentaje debe ser superior al 51%";
                if (productionTypeDDL.SelectedValue == "2" && project.project_percentage < 20)
                    lblErrorPorcentaje.Text = "El porcentaje debe ser superior al 20%";


                project_synopsis.Value = project.project_synopsis.ToString();
                if (!IsPostBack) {
                    project_recording_sites.Text = project.project_recording_sites.ToString();
                }
                
                project_duration_minutes.Value = project.GetProjectDuration("minutes").ToString();
                project_duration_seconds.Value = project.GetProjectDuration("seconds").ToString();
                if (project.project_filming_start_date.Year > 1)
                {
                    project_filming_start_date.Value = project.project_filming_start_date.Year + "-" + project.project_filming_start_date.Month + "-" + project.project_filming_start_date.Day;
                }

                if (project.project_filming_end_date.Year > 1)
                {
                    project_filming_end_date.Value = project.project_filming_end_date.Year + "-" + project.project_filming_end_date.Month + "-" + project.project_filming_end_date.Day;
                }

                project_filming_date_obs.Value = project.project_filming_date_obs.ToString();
                project_development_lab_info.Value = project.project_development_lab_info.ToString();

                txtNombreLabRev.Value = project.nombre_lab;
                txtOtrosIdiomas.Value = project.otros_idiomas;


                String DeptoLabSeleccionado = "";
                if (project.municipio_lab  != null && project.municipio_lab != string.Empty && project.municipio_lab.Trim() != "0") {
                    DataSet municipioLabSeleccionado = db.GetSelectOptions("localization", "localization_father_id", "localization_name", "localization_id='" + project.municipio_lab + "'");
                    DeptoLabSeleccionado = municipioLabSeleccionado.Tables[0].Rows[0]["localization_father_id"].ToString();
                    departamentoDDL.SelectedValue = DeptoLabSeleccionado;
                }
                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet municipioLabDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + DeptoLabSeleccionado + "'");
                municipioDDL.Items.Clear();
                municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < municipioLabDS.Tables[0].Rows.Count; i++)
                {
                    municipioDDL.Items.Add(new ListItem(municipioLabDS.Tables[0].Rows[i]["localization_name"].ToString(), municipioLabDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                if (project.municipio_lab == null)
                {
                    municipioDDL.SelectedValue = "0";
                }
                else
                {
                    municipioDDL.SelectedValue = project.municipio_lab.ToString().Trim(); 
                }


                project_preprint_store_info.Value = project.project_preprint_store_info.ToString();
                comentarios_adicionales.Value = project.obs_adicional_obra;
                if (project.project_legal_deposit == 1)
                {
                    project_legal_deposit_no.Checked = false;
                    project_legal_deposit_yes.Checked = true;
                }
                else if (project.project_legal_deposit == 0)
                {
                    project_legal_deposit_no.Checked = true;
                    project_legal_deposit_yes.Checked = false;
                }
                else
                {
                    project_legal_deposit_no.Checked = false;
                    project_legal_deposit_yes.Checked = false;
                    project_legal_deposit_no.Attributes["Class"] = "required_field";
                    project_legal_deposit_yes.Attributes["Class"] = "required_field";
                }

                cmbFormatoRodaje.DataBind();
                foreach (Format format in project.project_format)
                {
                    if (format.format_type_id == 1)
                    {
                        try
                        {
                            if (!IsPostBack)
                            {
                                cmbFormatoPadre.DataBind();
                                //cmbFormatoRodaje.Items.Add(new ListItem("Seleccione", "0"));
                                cmbFormatoPadre.SelectedValue = format.format_padre.ToString();
                                DataSet municipioDS = db.Select("select format_id, format_name  from dboPrd.format where format_type_id = 1 and format_padre = " + cmbFormatoPadre.SelectedValue + " order by format_name");
                                cmbFormatoRodaje.Items.Clear();
                                cmbFormatoRodaje.Items.Add(new ListItem("Seleccione", "0"));
                                for (int i = 0; i < municipioDS.Tables[0].Rows.Count; i++)
                                {
                                    cmbFormatoRodaje.Items.Add(new ListItem(municipioDS.Tables[0].Rows[i]["format_name"].ToString(), municipioDS.Tables[0].Rows[i]["format_id"].ToString()));
                                }
                                cmbFormatoRodaje.SelectedValue = format.format_id.ToString();
                            }

                        }
                        catch
                        {
                            cmbFormatoRodaje.SelectedValue = null;
                        }
                    }

                    /* Carga de los checkbox de las opciones de formato de rodaje */
                    for (int i = 0; i < grdFormatosRodaje.Rows.Count; i++)
                    {
                        var check = (CheckBox)grdFormatosRodaje.Rows[i].Cells[0].FindControl("chkFormatoRodaje");
                        var text = (TextBox)grdFormatosRodaje.Rows[i].Cells[0].FindControl("txtDetalle");
                        var label = (Label)grdFormatosRodaje.Rows[i].Cells[0].FindControl("Span1");
                        if (check.Text.Equals(format.format_name) && format.format_type_id == 1)
                        {
                            /* Si el elemento esta almacenado en la bd se selecciona en el checkbox */
                            check.Checked = true;
                            /* Aumenta el contador de formatos seleccionados */
                            shootingFormatCounter++;

                            /* Si la variable de control de presentación del campo de información
                             * del laboratorio de revelado aun tiene el valor "false" se hace 
                             la verificación de la nueva opciòn seleccionada para definir si se 
                             cambia su valor */
                            string lastTwoChars = format.format_name.Substring(format.format_name.Length - 2);
                            if (lastTwoChars.Equals("mm"))
                            {
                                /* Aumenta el contador de items seleccionados con m */
                                shootingFormatMMCounter++;
                                showProjectDevelopmentLabInfo = true;
                            }
                            text.Text = format.format_project_detail;
                            text.Style.Add("display", "");
                            label.Style.Add("display", "");
                        }
                    }
                    /* Carga de la opción de otro formato de rodaje */
                    if (format.format_id == 19)
                    {
                        otherFilmingFormatChecked = true;
                        otherFilmingFormatDetail = format.format_project_detail;
                    }

                    /* Carga de las opciones de formato de exhibición */
                    shootingFormatMM.Value = shootingFormatMMCounter.ToString();
                    for (int i = 0; i < projectExhibitionFormatCBL.Items.Count; i++)
                    {

                        if (projectExhibitionFormatCBL.Items[i].Text.Equals(format.format_name) && format.format_type_id == 2)
                        {
                            projectExhibitionFormatCBL.Items[i].Selected = true;
                            exhibitionFormatCounter++;
                        }
                    }

                    /* Carga de la opción de otro formato de exhibicion */
                    if (format.format_id == 25)
                    {
                        chkOtroFormatoExhibicion.Checked = true;
                        txtOtro_formato_exibicion_detail.Text = format.format_project_detail;

                    }
                }

                if (chkOtroFormatoExhibicion.Checked)
                {
                    lblOtro_formato_exibicion_label.Style.Add("display", "");
                    txtOtro_formato_exibicion_detail.Style.Add("display", "");
                }
                else
                {
                    lblOtro_formato_exibicion_label.Style.Add("display", "none");
                    txtOtro_formato_exibicion_detail.Style.Add("display", "none");
                }


                if (this.showAdvancedForm)
                {
                    /* Carga en el formulario el valor que ha sido recuperado de la base de datos 
                     * para el checkbox del estado del formulario
                     */
                    gestion_realizada_sin_revisar.Checked = false;
                    gestion_realizada_solicitar_aclaraciones.Checked = false;
                    gestion_realizada_informacion_correcta.Checked = false;

                    if (project.sectionDatosProyecto.revision_state_id == 11)
                    {
                        gestion_realizada_sin_revisar.Checked = true;
                    }
                    if (project.sectionDatosProyecto.revision_state_id == 10)
                    {
                        gestion_realizada_solicitar_aclaraciones.Checked = true;
                    }
                    if (project.sectionDatosProyecto.revision_state_id == 9)
                    {
                        gestion_realizada_informacion_correcta.Checked = true;
                    }

                    /* Crea las etiquetas que incluyen la imagen que indica el estado de la marca
                     * de revisión en cada pestaña y hace la persistencia en el formulario de acuerdo
                     * a los valores guardados en la base de datos */
                    estado_revision_sin_revisar.Checked = false;
                    estado_revision_revisado.Checked = false;
                    estado_revision_aprobado.Checked = false;

                    if (project.sectionDatosProyecto.revision_mark == "")
                    {
                        estado_revision_sin_revisar.Checked = true;
                    }
                    if (project.sectionDatosProyecto.revision_mark == "revisado")
                    {
                        estado_revision_revisado.Checked = true;
                    }
                    if (project.sectionDatosProyecto.revision_mark == "aprobado")
                    {
                        estado_revision_aprobado.Checked = true;
                    }

                    /*
                     * Carga en el formulario los textos registrados por los
                     * administradores del trámite
                     */
                    solicitud_aclaraciones.Value = project.sectionDatosProyecto.solicitud_aclaraciones;
                    informacion_correcta.Value = project.sectionDatosProyecto.observacion_inicial;

                    /* @cad: Aqui iba el codigo de ajuntos para administradores, se cambio para que sea visible a todo el mundo */

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

                /* #### INCLUIMOS CODIGO DE ADJUNTOS PARA QUE SEA VISIBLE A TODOS SOLO QUE HA */
                /* LOS ADMINISTRADORES SE LES HACE VISIBLE EL CHECKBOX DE APROBAR O RECHAZAR EL ADJUNTO*/
                /* Agrega las opciones al repetidor */
                Attachment adjuntoObj = new Attachment();
                // cargamos los tipos de adjuntos

                foreach (ProjectAttachment item in project.attachment)
                {
                    if (item.attachment.attachment_father_id == 7) //Solo los adjuntos de datos de la obra
                    {
                        adjuntoObj.LoadAttachment(item.attachment.attachment_id);
                        string operation = adjuntoObj.partOfProject(project);
                        if (operation == "excluded ") continue;

                        DataRow newRow = attachmentDS.Tables[0].NewRow();
                        newRow["estadoProyecto"] = project.state_id;
                        newRow["opcional"] = item.attachment.attachment_required == "required";
                        newRow["codigoAdjunto"] = item.attachment.attachment_id;
                        newRow["codigoProyecto"] = project.project_id;
                        newRow["url"] = item.project_attachment_path;
                        newRow["FileNameCaption"] = item.nombre_original;
                        newRow["aprobado"] = item.project_attachment_approved;



                        newRow["nombreAdjunto"] = item.attachment.attachment_name.
                            Replace("<span>", "").
                            Replace("</span>", "").
                            Replace("<img src= '../../images/icon_excel.gif'>", "").
                            Replace("<img src= 'https://investor.shareholder.com/bid/images/irIcons/ico_pdf.gif'>", "");



                        newRow["attachment_id"] = item.attachment.attachment_id;
                        newRow["attachment_father_id"] = item.attachment.attachment_father_id;
                        newRow["attachment_render"] = project.renderAttachments(item.attachment.attachment_id,
                                                                                item.project_attachment_id,

                                                                                item.attachment.attachment_name,
                                                                                item.project_attachment_path,
                                                                                showAdvancedForm,
                                                                                item.project_attachment_approved,
                                                                                item.attachment.attachment_required,
                                                                                this.project_state_id,
                                                                                this.user_role, item.nombre_original,
                                                                                item.attachment.tooltip
                                                                                );
                        attachmentDS.Tables[0].Rows.Add(newRow);
                    }
                }


                /* Agrega las opciones al repetidor */
                AttachmentRepeater.DataSource = attachmentDS;
                AttachmentRepeater.DataBind();

                //Agrega las opciones al repetidor uploadify
                AttachmentRepeater2.DataSource = attachmentDS;
                AttachmentRepeater2.DataBind();
                /* ### FIN CODIGO ADJUNTOS ###*/


                /* Agrega al formulario la información relacionada con la solicitud de aclaraciones */
                if (project.sectionDatosProyecto.solicitud_aclaraciones != "")
                {
                    clarification_request.Text = project.sectionDatosProyecto.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    if (user_role > 1 && project_state_id >= 6)
                    {
                        clarification_request_summary.Text = project.sectionDatosProyecto.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        producer_clarification_summary.Text = project.sectionDatosProyecto.aclaraciones_productor.Replace("\r\n", "<br>");
                    }
                }
                /* Recupera al formulario la información de aclaraciones registrada por el productor */
                producer_clarifications_field.Value = project.sectionDatosProyecto.aclaraciones_productor;

                /* Aplica formatos especiales - campos requeridos vacios - a los campos presentados en el formulario */
                project_domestic_producer_qty.Attributes["Class"] = (project.project_domestic_producer_qty == 0) ? "required_field" : "";
                project_foreign_producer_qty.Attributes["Class"] = (project.project_foreign_producer_qty == 0) ? "required_field" : "";

                projectTypeDDL.Attributes["Class"] = (project.project_type_id == 0) ? "required_field" : "";
                productionTypeDDL.Attributes["Class"] = (project.production_type_id == 0) ? "required_field" : "";
                projectGenreDDL.Attributes["Class"] = (project.project_genre_id == 0) ? "required_field" : "";
                personalTypeDDL.Attributes["Class"] = (project.project_personal_type == 0) ? "required_field" : "";

                project_total_cost_desarrollo.Attributes["Class"] = (project.project_total_cost_desarrollo == 0) ? "required_field currencyformat" : "currencyformat";
                project_total_cost_preproduccion.Attributes["Class"] = (project.project_total_cost_preproduccion == 0) ? "required_field currencyformat" : "currencyformat";
                project_total_cost_produccion.Attributes["Class"] = (project.project_total_cost_produccion == 0) ? "required_field currencyformat" : "currencyformat";
                project_total_cost_posproduccion.Attributes["Class"] = (project.project_total_cost_posproduccion == 0) ? "required_field currencyformat" : "currencyformat";
                project_total_cost_promotion.Attributes["Class"] = (project.project_total_cost_promotion == 0) ? "required_field currencyformat" : "currencyformat";
                project_percentage.Attributes["Class"] = (project.project_percentage.ToString() == "" || project.project_percentage > 100 || project.project_percentage <= 0) ? "required_field percentageformat" : "percentageformat";

                //si es produccion debe tener minimo 51%
                if (project.production_type_id == 1 && project.project_percentage < 51)
                {
                    project_percentage.Attributes["Class"] = "required_field percentageformat";
                }

                //si es coproduccion debe tener minimo 20% maximo 99%
                if (project.production_type_id == 2 && (project.project_percentage < 20 || project.project_percentage >= 100))
                {
                    project_percentage.Attributes["Class"] = "required_field percentageformat";
                }

                //si es coproduccion debe tener minimo 20% maximo 99%
                if (project.production_type_id == 3 && (project.project_percentage < 10 || project.project_percentage >= 100))
                {
                    project_percentage.Attributes["Class"] = "required_field percentageformat";
                }




                project_synopsis.Attributes["Class"] = (project.project_synopsis == "") ? "required_field" : "";
                project_recording_sites.Attributes["Class"] = (project.project_recording_sites == "") ? "required_field" : "";
                if (project.ValidateProjectDuration())
                {
                    project_duration_minutes.Attributes["Class"] = "";
                    project_duration_seconds.Attributes["Class"] = "";
                }
                else
                {
                    project_duration_minutes.Attributes["Class"] = "required_field";
                    project_duration_seconds.Attributes["Class"] = "required_field";
                }
                //  project_filming_date_obs.Attributes["Class"] = (project.project_filming_date_obs == "") ? "required_field" : "";
                project_development_lab_info.Attributes["Class"] = (project.project_development_lab_info == "") ? "required_field" : "";
                project_preprint_store_info.Attributes["Class"] = (project.project_preprint_store_info == "") ? "required_field" : "";

                formato_rodaje_input.Attributes["Class"] = (shootingFormatCounter == 0) ? "required_field" : "";
                projectExhibitionFormatCBL.Attributes["Class"] = (exhibitionFormatCounter == 0) ? "required_field" : "";
                if (project_filming_start_date.Value == string.Empty)
                {
                    project_filming_start_date.Attributes["Class"] = "required_field";
                }
                else
                {
                    project_filming_start_date.Attributes["Class"] = "";
                }

                if (project_filming_end_date.Value == string.Empty)
                {
                    project_filming_end_date.Attributes["Class"] = "required_field";
                }
                else
                {
                    project_filming_end_date.Attributes["Class"] = "";
                }

                if (cmbIdiomaPrincipal.SelectedValue == "0")
                {
                    cmbIdiomaPrincipal.Attributes["Class"] = "required_field";
                }
                else
                {
                    cmbIdiomaPrincipal.Attributes["Class"] = "";
                }

                if (cmbPremio.SelectedValue == "-1")
                {
                    cmbPremio.Attributes["Class"] = "required_field";
                }
                else
                {
                    cmbPremio.Attributes["Class"] = "";
                }

                /*if (premioTxt.Value == string.Empty)
                {
                    premioTxt.Attributes["Class"] = "required_field";
                }
                else
                {
                    premioTxt.Attributes["Class"] = "";
                }*/

            }

            if (!IsPostBack)
            {
                if (project.production_type_id != 0 && project.project_genre_id != 0 && project.project_type_id != 0 && project.project_percentage != 0)
                {
                    if (user_role <= 1 && (project.state_id == 1 || project.state_id == 5))
                    {
                        pnlMensajeVisible.Visible = true;
                    }
                }
            }

        
        }

        protected void cmbFormatoPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            DataSet municipioDS = db.Select("select format_id, format_name  from dboPrd.format where format_type_id = 1 and format_padre = " + cmbFormatoPadre.SelectedValue + " order by format_name");
            cmbFormatoRodaje.Items.Clear();
            cmbFormatoRodaje.Items.Add(new ListItem("Seleccione", "0"));
            for (int i = 0; i < municipioDS.Tables[0].Rows.Count; i++)
            {
                cmbFormatoRodaje.Items.Add(new ListItem(municipioDS.Tables[0].Rows[i]["format_name"].ToString(), municipioDS.Tables[0].Rows[i]["format_id"].ToString()));
            }
            cmbFormatoRodaje.Focus();
        }

        protected void btnAdicionarEstimulo_Click(object sender, EventArgs e)
        {
            NegocioCineProducto neg = new NegocioCineProducto();
            estimulo est = new estimulo();

            if (txtBeneficiarioEstimulo.Text == "" || txtValorEstimulo.Text == "" || txtNombreEstimulo.Text == "" || cmbTipoEstimulo.SelectedValue == "-1") {
                lblErrorEstimulo.Text = "Debe diligenciar todos los datos del estimulo";
                return  ;
            }                       

            est.beneficiario = txtBeneficiarioEstimulo.Text;
            est.valor = float.Parse(txtValorEstimulo.Text);
            est.nombre = txtNombreEstimulo.Text;
            est.id_tipo_estimulo = int.Parse(cmbTipoEstimulo.SelectedValue);
            est.project_id = Convert.ToInt32(Session["project_id"]);
            neg.AdicionarEstimulo(est);

            project p1 = new project();
            p1.project_id = Convert.ToInt32(Session["project_id"]);
            p1.tiene_estimulos = "Si";
            neg.ActualizarEstimuloProyecto(p1);

            GridViewEstimulos.DataBind();
            txtBeneficiarioEstimulo.Text = "";
            txtValorEstimulo.Text = "";
            txtNombreEstimulo.Text = "";
            cmbTipoEstimulo.SelectedValue = "-1";


        }

        protected void GridViewEstimulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idEstimulo = int.Parse(e.CommandArgument.ToString());
            NegocioCineProducto neg = new NegocioCineProducto();
            neg.eliminarEstimulo(idEstimulo);
            GridViewEstimulos.DataBind();
        }

        protected void cmbDeptoLugar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void esColombiaLugar_CheckedChanged(object sender, EventArgs e)
        {
            if (esColombiaLugar.Checked)
            {
                pnFueraColombia.Visible = false;
                pnLugarEnColombia.Visible = true;
            }
            else {
                pnFueraColombia.Visible = true;
                pnLugarEnColombia.Visible = false;
            }
            //project_recording_sites.Text = project_recording_sites.Text;

        }

        protected void btnAgregarLugar_Click(object sender, EventArgs e)
        {

            lblErrorLugar.Text = "";
            if (cmbPais.SelectedValue =="COLOMBIA")
            {
                if (cmbDeptoLugar.SelectedValue == "-1" || cmbCiudadLugar.SelectedValue == "-1" )
                {
                    lblErrorLugar.Text = "Debe diligenciar el lugar de filmacion completo";
                    return;
                }
                project_recording_sites.Text += "(Colombia -" + cmbDeptoLugar.SelectedItem.Text + " - " + cmbCiudadLugar.SelectedItem.Text + " - " + txtNombreLugar.Text + "),\n";
            }
            else {
                if (cmbPais.SelectedValue == "-1" || txtCiudadLugar.Text == "-1" )
                {
                    lblErrorLugar.Text = "Debe diligenciar el lugar de filmacion completo";
                    return;
                }
                project_recording_sites.Text += "(" + cmbPais.SelectedValue + " - " + txtCiudadLugar.Text + " - " + txtNombreLugar.Text + "),\n";
            }

            //cmbPais.SelectedValue = "";
            txtCiudadLugar.Text = "";
            txtNombreLugar.Text = "";
            try
            {
                cmbDeptoLugar.SelectedValue = "";
                cmbCiudadLugar.SelectedValue = "";
            }
            catch { 
            }


        }

        protected void btnLimpiarLugaresFilm_Click(object sender, EventArgs e)
        {
            project_recording_sites.Text = "";
        }

        protected void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPais.SelectedValue == "COLOMBIA")
            {
                pnFueraColombia.Visible = false;
                pnLugarEnColombia.Visible = true;
            }
            else
            {
                pnFueraColombia.Visible = true;
                pnLugarEnColombia.Visible = false;
            }
        }


        //public string AttachmentRender(object attachment_father_id, object attachment_name, object attachment_id)
        //{
        //    int father_id = (int)attachment_father_id;
        //    string local_attachment_id = attachment_id.ToString();
        //    string local_attachment_name = (string)attachment_name;
        //    string item = "";

        //    if (father_id == 0)
        //    {
        //        item = "<h2 style='text-align:center;'>" + local_attachment_name + "</h2>";
        //    }
        //    else
        //    {
        //        item = "<div class='field_label'><span id=\"name_" + attachment_id + "\">" + local_attachment_name + "</span><span class=\"required_field_text\">*</span></div>"
        //               + "<div class='field_input'>"
        //               + "<div id='FileUpload" + local_attachment_id + "' />"
        //               + "</div>";

        //    }
        //    return item;

        //}
    }
}