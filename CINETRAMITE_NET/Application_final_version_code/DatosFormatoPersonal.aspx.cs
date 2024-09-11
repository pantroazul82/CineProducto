using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CineProducto.Bussines;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace CineProducto
{
    public partial class DatosFormatoPersonal : System.Web.UI.Page
    {
        /* Atributo que permite controlar la presentación del campo de información 
         * del laboratorio de revelado */
        public bool showAdvancedForm = false; //Variable que controla la presentación del formulario de administración

        public int project_state_id = 0; //Indica el estado del proyecto, el cual se utiliza para identificar los elementos a presentar particulares de cada estado
        public int section_state_id = 0; //Indica el estado de la sección actual, el cual se utiliza para identificar los elementos a presentar particulares de cada estado.
        public int user_role;

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
            }

            /* Define la region */
            CultureInfo culture = new CultureInfo("es-CO");

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Crea el objeto del proyecto para manejar la información */
            Project project = new Project();

            #region  Obtiene las opciones para el select de departamentos 
            DataSet departamentoDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='0'");
            if (!Page.IsPostBack)
            {
                director_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                writer_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                photography_director_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                art_director_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                music_author_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                editor_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cameraman_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                makeup_artist_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                costume_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                dresser_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                casting_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                script_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                soundman_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo14_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo15_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo16_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo17_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo18_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo19_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo20_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo21_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo22_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo23_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo24_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo25_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                cargo26_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                
                mixer_departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < departamentoDS.Tables[0].Rows.Count; i++)
                {
                    director_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    writer_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    photography_director_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    art_director_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    music_author_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    editor_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cameraman_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    makeup_artist_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    costume_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    dresser_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    casting_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    script_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    soundman_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));


                    cargo14_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo15_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo16_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo17_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo18_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo19_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo20_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo21_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo22_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo23_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo24_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo25_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    cargo26_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                    
                    mixer_departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }
            }
#endregion
            /* Obtiene los textos de los tooltips y los pasa a las funciones de javascript correspondientes */
            tooltip_name.Text = db.GetTooltip("staff_format_name");
            tooltip_identification_number.Text = db.GetTooltip("staff_format_identification_number");
            tooltip_address.Text = db.GetTooltip("staff_format_address");
            tooltip_email.Text = db.GetTooltip("staff_format_email");
            tooltip_phone.Text = db.GetTooltip("staff_format_phone");
            tooltip_movil.Text = db.GetTooltip("staff_format_movil");

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
            /**************************************************/

            /* Valida si fue enviado el formulario */
            if (Request.Form["submit_technic_personal_data"] != null)
            {
                /* Actualiza objeto con la información del DIRECTOR */
                project.StaffFormatDirector.staff_format_name = director_name.Value;
                project.StaffFormatDirector.staff_format_identification_number = director_identification_number.Value;
                project.StaffFormatDirector.staff_format_address = director_address.Value;
                project.StaffFormatDirector.staff_format_email = director_email.Value;
                project.StaffFormatDirector.staff_format_phone = director_phone.Value;
                project.StaffFormatDirector.staff_format_movil = director_movil.Value;
                project.StaffFormatDirector.staff_format_country = director_country.Value;
                project.StaffFormatDirector.staff_format_state = director_state.Value;
                project.StaffFormatDirector.staff_format_localization_id = Request.Form["director_selectedMunicipio"];
                //project.StaffFormatDirector.staff_format_localization_id = director_departamentoDDL.SelectedValue;

                if (director_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatDirector.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatDirector.staff_format_localization_id =  Request.Form["director_selectedMunicipio"];
                    //project.StaffFormatDirector.staff_format_localization_id = director_departamentoDDL.SelectedValue;
                }

                /* Actualiza objeto con la información del GUIONISTA */
                project.StaffFormatGuionista.staff_format_name = writer_name.Value;
                project.StaffFormatGuionista.staff_format_identification_number = writer_identification_number.Value;
                project.StaffFormatGuionista.staff_format_address = writer_address.Value;
                project.StaffFormatGuionista.staff_format_email = writer_email.Value;
                project.StaffFormatGuionista.staff_format_phone = writer_phone.Value;
                project.StaffFormatGuionista.staff_format_movil = writer_movil.Value;
                project.StaffFormatGuionista.staff_format_country = writer_country.Value;
                project.StaffFormatGuionista.staff_format_state = writer_state.Value;
                project.StaffFormatGuionista.staff_format_localization_id = Request.Form["writer_selectedMunicipio"];
                if (writer_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatGuionista.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatGuionista.staff_format_localization_id = Request.Form["writer_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del DIRECTOR DE FOTOGRAFÍA */
                project.StaffFormatDirectorFotografía.staff_format_name = photography_director_name.Value;
                project.StaffFormatDirectorFotografía.staff_format_identification_number = photography_director_identification_number.Value;
                project.StaffFormatDirectorFotografía.staff_format_address = photography_director_address.Value;
                project.StaffFormatDirectorFotografía.staff_format_email = photography_director_email.Value;
                project.StaffFormatDirectorFotografía.staff_format_phone = photography_director_phone.Value;
                project.StaffFormatDirectorFotografía.staff_format_movil = photography_director_movil.Value;
                project.StaffFormatDirectorFotografía.staff_format_country = photography_director_country.Value;
                project.StaffFormatDirectorFotografía.staff_format_state = photography_director_state.Value;
                project.StaffFormatDirectorFotografía.staff_format_localization_id = Request.Form["photography_director_selectedMunicipio"];
                if (photography_director_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatDirectorFotografía.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatDirectorFotografía.staff_format_localization_id = Request.Form["photography_director_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del DIRECTOR DE ARTE */
                project.StaffFormatDirectorArte.staff_format_name = art_director_name.Value;
                project.StaffFormatDirectorArte.staff_format_identification_number = art_director_identification_number.Value;
                project.StaffFormatDirectorArte.staff_format_address = art_director_address.Value;
                project.StaffFormatDirectorArte.staff_format_email = art_director_email.Value;
                project.StaffFormatDirectorArte.staff_format_phone = art_director_phone.Value;
                project.StaffFormatDirectorArte.staff_format_movil = art_director_movil.Value;
                project.StaffFormatDirectorArte.staff_format_country = art_director_country.Value;
                project.StaffFormatDirectorArte.staff_format_state = art_director_state.Value;
                project.StaffFormatDirectorArte.staff_format_localization_id = Request.Form["art_director_selectedMunicipio"];
                if (art_director_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatDirectorArte.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatDirectorArte.staff_format_localization_id = Request.Form["art_director_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del AUTOR DE LA MÚSICA */
                project.StaffFormatAutorMusica.staff_format_name = music_author_name.Value;
                project.StaffFormatAutorMusica.staff_format_identification_number = music_author_identification_number.Value;
                project.StaffFormatAutorMusica.staff_format_address = music_author_address.Value;
                project.StaffFormatAutorMusica.staff_format_email = music_author_email.Value;
                project.StaffFormatAutorMusica.staff_format_phone = music_author_phone.Value;
                project.StaffFormatAutorMusica.staff_format_movil = music_author_movil.Value;
                project.StaffFormatAutorMusica.staff_format_country = music_author_country.Value;
                project.StaffFormatAutorMusica.staff_format_state = music_author_state.Value;
                project.StaffFormatAutorMusica.staff_format_localization_id = Request.Form["music_author_selectedMunicipio"];
                if (music_author_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatAutorMusica.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatAutorMusica.staff_format_localization_id = Request.Form["music_author_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del AUTOR DE LA EDITOR/MONTAJISTA */
                project.StaffFormatEditorMontajista.staff_format_name = editor_name.Value;
                project.StaffFormatEditorMontajista.staff_format_identification_number = editor_identification_number.Value;
                project.StaffFormatEditorMontajista.staff_format_address = editor_address.Value;
                project.StaffFormatEditorMontajista.staff_format_email = editor_email.Value;
                project.StaffFormatEditorMontajista.staff_format_phone = editor_phone.Value;
                project.StaffFormatEditorMontajista.staff_format_movil = editor_movil.Value;
                project.StaffFormatEditorMontajista.staff_format_country = editor_country.Value;
                project.StaffFormatEditorMontajista.staff_format_state = editor_state.Value;
                project.StaffFormatEditorMontajista.staff_format_localization_id = Request.Form["editor_selectedMunicipio"];
                if (editor_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatEditorMontajista.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatEditorMontajista.staff_format_localization_id = Request.Form["editor_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del AUTOR DE LA CAMAROGRAFO */
                project.StaffFormatCamarografo.staff_format_name = cameraman_name.Value;
                project.StaffFormatCamarografo.staff_format_identification_number = cameraman_identification_number.Value;
                project.StaffFormatCamarografo.staff_format_address = cameraman_address.Value;
                project.StaffFormatCamarografo.staff_format_email = cameraman_email.Value;
                project.StaffFormatCamarografo.staff_format_phone = cameraman_phone.Value;
                project.StaffFormatCamarografo.staff_format_movil = cameraman_movil.Value;
                project.StaffFormatCamarografo.staff_format_country = cameraman_country.Value;
                project.StaffFormatCamarografo.staff_format_state = cameraman_state.Value;
                project.StaffFormatCamarografo.staff_format_localization_id = Request.Form["cameraman_selectedMunicipio"];
                if (cameraman_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatCamarografo.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatCamarografo.staff_format_localization_id = Request.Form["cameraman_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del MAQUILLADOR */
                project.StaffFormatMaquillador.staff_format_name = makeup_artist_name.Value;
                project.StaffFormatMaquillador.staff_format_identification_number = makeup_artist_identification_number.Value;
                project.StaffFormatMaquillador.staff_format_address = makeup_artist_address.Value;
                project.StaffFormatMaquillador.staff_format_email = makeup_artist_email.Value;
                project.StaffFormatMaquillador.staff_format_phone = makeup_artist_phone.Value;
                project.StaffFormatMaquillador.staff_format_movil = makeup_artist_movil.Value;
                project.StaffFormatMaquillador.staff_format_country = makeup_artist_country.Value;
                project.StaffFormatMaquillador.staff_format_state = makeup_artist_state.Value;
                project.StaffFormatMaquillador.staff_format_localization_id = Request.Form["makeup_artist_selectedMunicipio"];
                if (makeup_artist_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatMaquillador.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatMaquillador.staff_format_localization_id = Request.Form["makeup_artist_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del VESTUARISTA */
                project.StaffFormatVestuarista.staff_format_name = costume_name.Value;
                project.StaffFormatVestuarista.staff_format_identification_number = costume_identification_number.Value;
                project.StaffFormatVestuarista.staff_format_address = costume_address.Value;
                project.StaffFormatVestuarista.staff_format_email = costume_email.Value;
                project.StaffFormatVestuarista.staff_format_phone = costume_phone.Value;
                project.StaffFormatVestuarista.staff_format_movil = costume_movil.Value;
                project.StaffFormatVestuarista.staff_format_country = costume_country.Value;
                project.StaffFormatVestuarista.staff_format_state = costume_state.Value;
                project.StaffFormatVestuarista.staff_format_localization_id = Request.Form["costume_selectedMunicipio"];
                if (costume_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatVestuarista.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatVestuarista.staff_format_localization_id = Request.Form["costume_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del AMBIENTADOR */
                project.StaffFormatAmbientador.staff_format_name = dresser_name.Value;
                project.StaffFormatAmbientador.staff_format_identification_number = dresser_identification_number.Value;
                project.StaffFormatAmbientador.staff_format_address = dresser_address.Value;
                project.StaffFormatAmbientador.staff_format_email = dresser_email.Value;
                project.StaffFormatAmbientador.staff_format_phone = dresser_phone.Value;
                project.StaffFormatAmbientador.staff_format_movil = dresser_movil.Value;
                project.StaffFormatAmbientador.staff_format_country = dresser_country.Value;
                project.StaffFormatAmbientador.staff_format_state = dresser_state.Value;
                project.StaffFormatAmbientador.staff_format_localization_id = Request.Form["dresser_selectedMunicipio"];
                if (dresser_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatAmbientador.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatAmbientador.staff_format_localization_id = Request.Form["dresser_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del ENCARGADO DEL CASTING */
                project.StaffFormatEncargadoCasting.staff_format_name = casting_name.Value;
                project.StaffFormatEncargadoCasting.staff_format_identification_number = casting_identification_number.Value;
                project.StaffFormatEncargadoCasting.staff_format_address = casting_address.Value;
                project.StaffFormatEncargadoCasting.staff_format_email = casting_email.Value;
                project.StaffFormatEncargadoCasting.staff_format_phone = casting_phone.Value;
                project.StaffFormatEncargadoCasting.staff_format_movil = casting_movil.Value;
                project.StaffFormatEncargadoCasting.staff_format_country = casting_country.Value;
                project.StaffFormatEncargadoCasting.staff_format_state = casting_state.Value;
                project.StaffFormatEncargadoCasting.staff_format_localization_id = Request.Form["casting_selectedMunicipio"];
                if (casting_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatEncargadoCasting.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatEncargadoCasting.staff_format_localization_id = Request.Form["casting_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del SCRIPT */
                project.StaffFormaScript.staff_format_name = script_name.Value;
                project.StaffFormaScript.staff_format_identification_number = script_identification_number.Value;
                project.StaffFormaScript.staff_format_address = script_address.Value;
                project.StaffFormaScript.staff_format_email = script_email.Value;
                project.StaffFormaScript.staff_format_phone = script_phone.Value;
                project.StaffFormaScript.staff_format_movil = script_movil.Value;
                project.StaffFormaScript.staff_format_country = script_country.Value;
                project.StaffFormaScript.staff_format_state = script_state.Value;
                project.StaffFormaScript.staff_format_localization_id = Request.Form["script_selectedMunicipio"];
                if (script_localization_out_of_colombia.Checked)
                {
                    project.StaffFormaScript.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormaScript.staff_format_localization_id = Request.Form["script_selectedMunicipio"];
                }

                /* Actualiza objeto con la información del SONIDISTA */
                project.StaffFormatSonidista.staff_format_name = soundman_name.Value;
                project.StaffFormatSonidista.staff_format_identification_number = soundman_identification_number.Value;
                project.StaffFormatSonidista.staff_format_address = soundman_address.Value;
                project.StaffFormatSonidista.staff_format_email = soundman_email.Value;
                project.StaffFormatSonidista.staff_format_phone = soundman_phone.Value;
                project.StaffFormatSonidista.staff_format_movil = soundman_movil.Value;
                project.StaffFormatSonidista.staff_format_country = soundman_country.Value;
                project.StaffFormatSonidista.staff_format_state = soundman_state.Value;
                project.StaffFormatSonidista.staff_format_localization_id = Request.Form["soundman_selectedMunicipio"];
                if (soundman_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatSonidista.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatSonidista.staff_format_localization_id = Request.Form["soundman_selectedMunicipio"];
                }

                for (int k = 0; k < 13; k++)
                {
                    project.StaffFormatArreglo[k].staff_format_name = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_name")).Value;
                    project.StaffFormatArreglo[k].staff_format_identification_number = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_identification_number")).Value;
                    project.StaffFormatArreglo[k].staff_format_address = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_address")).Value;
                    project.StaffFormatArreglo[k].staff_format_email = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_email")).Value;
                    project.StaffFormatArreglo[k].staff_format_phone = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_phone")).Value;
                    project.StaffFormatArreglo[k].staff_format_movil = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_movil")).Value;
                    project.StaffFormatArreglo[k].staff_format_country = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_country")).Value;
                    project.StaffFormatArreglo[k].staff_format_state = ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_state")).Value;
                    project.StaffFormatArreglo[k].staff_format_localization_id = Request.Form["cargo"+(k+14)+"_selectedMunicipio"];
                    if (((HtmlInputCheckBox)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + (k + 14) + "_localization_out_of_colombia")).Checked)
                    {
                        project.StaffFormatArreglo[k].staff_format_localization_id = "";
                    }
                    else
                    {
                        project.StaffFormatArreglo[k].staff_format_localization_id = Request.Form["cargo" + (k + 14) + "_selectedMunicipio"];
                    }
                }
                /* Actualiza objeto con la información del MEZCLADOR */
                project.StaffFormatMezclador.staff_format_name = mixer_name.Value;
                project.StaffFormatMezclador.staff_format_identification_number = mixer_identification_number.Value;
                project.StaffFormatMezclador.staff_format_address = mixer_address.Value;
                project.StaffFormatMezclador.staff_format_email = mixer_email.Value;
                project.StaffFormatMezclador.staff_format_phone = mixer_phone.Value;
                project.StaffFormatMezclador.staff_format_movil = mixer_movil.Value;
                project.StaffFormatMezclador.staff_format_country = mixer_country.Value;
                project.StaffFormatMezclador.staff_format_state = mixer_state.Value;
                project.StaffFormatMezclador.staff_format_localization_id = Request.Form["mixer_selectedMunicipio"];
                if (mixer_localization_out_of_colombia.Checked)
                {
                    project.StaffFormatMezclador.staff_format_localization_id = "";
                }
                else
                {
                    project.StaffFormatMezclador.staff_format_localization_id = Request.Form["mixer_selectedMunicipio"];
                }

                /* Hace la persistencia a la bd de la información de aclaraciones registrada por el productor */


                #region  Se pasan al objeto del proyecto los valores definidos en el formulario de administración para ser almacenados */
                if (this.showAdvancedForm)
                {
                    /* Interpretación del valor enviado del formulario para la gestión realizada */
             
                }
#endregion
                /* Se guarda en la base de datos toda la información recuperada del formulario diligenciado pro el productor */
                project.Save();

                /* Se recarga la información del proyecto para presentar la información como haya quedado en la bd */
                project.LoadProject(project.project_id);
            }

            if(Session["project_id"] == null && project.project_id > 0)
            {
                Session["project_id"] = project.project_id;
            }
            
            if(Session["project_id"] != null)
            {
                /* Guarda en la variable de la clase el estado de la variable */
                this.project_state_id = project.state_id;
                this.section_state_id = project.sectionDatosPersonal.tab_state_id;
                #region zona de estados de tabs
                /* Si el proyecto aun esta en su primera etapa (estado creado - 1) se define el estilo
                 * de la pestaña de acuerdo al resultado de la validación del diligenciamiento de los
                 * campos de los formularios. */
                if (project.state_id == 1)
                {
                    project.sectionDatosProyecto.tab_state_id = 1;
                    project.sectionDatosProductor.tab_state_id = 1;
                    project.sectionDatosProductoresAdicionales.tab_state_id = 1;
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
                       (project.production_type_id == 2 && project.project_foreign_producer_qty <= 0 && project.project_domestic_producer_qty <= 1)
                       )
                {
                    tab_productores_adicionales_css_class = "tab_hide";
                }
                /* Verifica si el proyecto es de tipo largometraje */
                emtyform = project.validateNotInitForm("DatosFormatoPersonal");
                if (project.project_type_id == 1 || project.project_type_id == 2)
                {
                    switch (project.sectionDatosPersonal.tab_state_id) /* Datos de los productores adicionales */
                    {
                        case 10:
                            tab_datos_formato_personal_css_class = "tab_incompleto_active";
                            break;
                        case 11:
                            tab_datos_formato_personal_css_class = "tab_unmarked_active";
                            break;
                        case 9:
                            tab_datos_formato_personal_css_class = "tab_completo_active";
                            break;
                        default:
                            if (!emtyform)
                            {
                                tab_datos_formato_personal_css_class = (project.ValidateProjectSection("DatosFormatoPersonal")) ? "tab_completo_active" : "tab_incompleto_active";
                            }
                            else
                            {
                                tab_datos_formato_personal_css_class = "tab_unmarked_active";
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
                RequestForm form = new RequestForm(int.Parse(Session["project_id"].ToString()));
                if (form.path == null || form.path.Trim() == string.Empty)
                {
                    project.sectionDatosFinalizacion.tab_state_id = 10;
                }

                switch (project.sectionDatosFinalizacion.tab_state_id) /* Pestaña finalización */
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
                /* Carga la información recuperada de la base de datos en el formulario */

                /* ************************** */
                /* CARGA INFORMACIÓN DIRECTOR */
                /* ************************** */
                director_name.Value = project.StaffFormatDirector.staff_format_name;
                director_identification_number.Value = project.StaffFormatDirector.staff_format_identification_number;
                director_address.Value = project.StaffFormatDirector.staff_format_address;
                director_email.Value = project.StaffFormatDirector.staff_format_email;
                director_phone.Value = project.StaffFormatDirector.staff_format_phone;
                director_movil.Value = project.StaffFormatDirector.staff_format_movil;
                director_country.Value = project.StaffFormatDirector.staff_format_country;
                director_state.Value = project.StaffFormatDirector.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatDirector.staff_format_localization_father_id == "")
                {
                    director_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    director_departamentoDDL.SelectedValue = project.StaffFormatDirector.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet director_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatDirector.staff_format_localization_father_id + "'");
                director_municipioDDL.Items.Clear();
                director_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < director_municipioDS.Tables[0].Rows.Count; i++)
                {
                    director_municipioDDL.Items.Add(new ListItem(director_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), director_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatDirector.staff_format_localization_id == "")
                {
                    director_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    director_municipioDDL.SelectedValue = project.StaffFormatDirector.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatDirector.staff_format_country == "")
                {
                    director_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    director_localization_out_of_colombia.Checked = true;
                }

                /* *************************** */
                /* CARGA INFORMACIÓN GUIONISTA */
                /* *************************** */
                writer_name.Value = project.StaffFormatGuionista.staff_format_name;
                writer_identification_number.Value = project.StaffFormatGuionista.staff_format_identification_number;
                writer_address.Value = project.StaffFormatGuionista.staff_format_address;
                writer_email.Value = project.StaffFormatGuionista.staff_format_email;
                writer_phone.Value = project.StaffFormatGuionista.staff_format_phone;
                writer_movil.Value = project.StaffFormatGuionista.staff_format_movil;
                writer_country.Value = project.StaffFormatGuionista.staff_format_country;
                writer_state.Value = project.StaffFormatGuionista.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatGuionista.staff_format_localization_father_id == "")
                {
                    writer_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    writer_departamentoDDL.SelectedValue = project.StaffFormatGuionista.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet writer_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatGuionista.staff_format_localization_father_id + "'");
                writer_municipioDDL.Items.Clear();
                writer_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < writer_municipioDS.Tables[0].Rows.Count; i++)
                {
                    writer_municipioDDL.Items.Add(new ListItem(writer_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), writer_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatGuionista.staff_format_localization_id == "")
                {
                    writer_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    writer_municipioDDL.SelectedValue = project.StaffFormatGuionista.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatGuionista.staff_format_country == "")
                {
                    writer_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    writer_localization_out_of_colombia.Checked = true;
                }

                /* **************************************** */
                /* CARGA INFORMACIÓN DIRECTOR DE FOTOGRAFÍA */
                /* **************************************** */
                photography_director_name.Value = project.StaffFormatDirectorFotografía.staff_format_name;
                photography_director_identification_number.Value = project.StaffFormatDirectorFotografía.staff_format_identification_number;
                photography_director_address.Value = project.StaffFormatDirectorFotografía.staff_format_address;
                photography_director_email.Value = project.StaffFormatDirectorFotografía.staff_format_email;
                photography_director_phone.Value = project.StaffFormatDirectorFotografía.staff_format_phone;
                photography_director_movil.Value = project.StaffFormatDirectorFotografía.staff_format_movil;
                photography_director_country.Value = project.StaffFormatDirectorFotografía.staff_format_country;
                photography_director_state.Value = project.StaffFormatDirectorFotografía.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatDirectorFotografía.staff_format_localization_father_id == "")
                {
                    photography_director_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    photography_director_departamentoDDL.SelectedValue = project.StaffFormatDirectorFotografía.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet photography_director_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatDirectorFotografía.staff_format_localization_father_id + "'");
                photography_director_municipioDDL.Items.Clear();
                photography_director_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < photography_director_municipioDS.Tables[0].Rows.Count; i++)
                {
                    photography_director_municipioDDL.Items.Add(new ListItem(photography_director_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), photography_director_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatDirectorFotografía.staff_format_localization_id == "")
                {
                    photography_director_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    photography_director_municipioDDL.SelectedValue = project.StaffFormatDirectorFotografía.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatDirectorFotografía.staff_format_country == "")
                {
                    photography_director_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    photography_director_localization_out_of_colombia.Checked = true;
                }

                /* **************************************** */
                /* CARGA INFORMACIÓN DIRECTOR DE ARTE       */
                /* **************************************** */
                art_director_name.Value = project.StaffFormatDirectorArte.staff_format_name;
                art_director_identification_number.Value = project.StaffFormatDirectorArte.staff_format_identification_number;
                art_director_address.Value = project.StaffFormatDirectorArte.staff_format_address;
                art_director_email.Value = project.StaffFormatDirectorArte.staff_format_email;
                art_director_phone.Value = project.StaffFormatDirectorArte.staff_format_phone;
                art_director_movil.Value = project.StaffFormatDirectorArte.staff_format_movil;
                art_director_country.Value = project.StaffFormatDirectorArte.staff_format_country;
                art_director_state.Value = project.StaffFormatDirectorArte.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatDirectorArte.staff_format_localization_father_id == "")
                {
                    art_director_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    art_director_departamentoDDL.SelectedValue = project.StaffFormatDirectorArte.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet art_director_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatDirectorArte.staff_format_localization_father_id + "'");
                art_director_municipioDDL.Items.Clear();
                art_director_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < art_director_municipioDS.Tables[0].Rows.Count; i++)
                {
                    art_director_municipioDDL.Items.Add(new ListItem(art_director_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), art_director_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatDirectorArte.staff_format_localization_id == "")
                {
                    art_director_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    art_director_municipioDDL.SelectedValue = project.StaffFormatDirectorArte.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatDirectorArte.staff_format_country == "")
                {
                    art_director_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    art_director_localization_out_of_colombia.Checked = true;
                }

                /* **************************************** */
                /* CARGA INFORMACIÓN DE AUTOR DE LA MÚSICA  */
                /* **************************************** */
                music_author_name.Value = project.StaffFormatAutorMusica.staff_format_name;
                music_author_identification_number.Value = project.StaffFormatAutorMusica.staff_format_identification_number;
                music_author_address.Value = project.StaffFormatAutorMusica.staff_format_address;
                music_author_email.Value = project.StaffFormatAutorMusica.staff_format_email;
                music_author_phone.Value = project.StaffFormatAutorMusica.staff_format_phone;
                music_author_movil.Value = project.StaffFormatAutorMusica.staff_format_movil;
                music_author_country.Value = project.StaffFormatAutorMusica.staff_format_country;
                music_author_state.Value = project.StaffFormatAutorMusica.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatAutorMusica.staff_format_localization_father_id == "")
                {
                    music_author_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    music_author_departamentoDDL.SelectedValue = project.StaffFormatAutorMusica.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet music_author_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatAutorMusica.staff_format_localization_father_id + "'");
                music_author_municipioDDL.Items.Clear();
                music_author_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < music_author_municipioDS.Tables[0].Rows.Count; i++)
                {
                    music_author_municipioDDL.Items.Add(new ListItem(music_author_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), music_author_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatAutorMusica.staff_format_localization_id == "")
                {
                    music_author_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    music_author_municipioDDL.SelectedValue = project.StaffFormatAutorMusica.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatAutorMusica.staff_format_country == "")
                {
                    music_author_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    music_author_localization_out_of_colombia.Checked = true;
                }

                /* **************************************** */
                /* CARGA INFORMACIÓN DEL EDITOR/MONTAJISTA  */
                /* **************************************** */
                editor_name.Value = project.StaffFormatEditorMontajista.staff_format_name;
                editor_identification_number.Value = project.StaffFormatEditorMontajista.staff_format_identification_number;
                editor_address.Value = project.StaffFormatEditorMontajista.staff_format_address;
                editor_email.Value = project.StaffFormatEditorMontajista.staff_format_email;
                editor_phone.Value = project.StaffFormatEditorMontajista.staff_format_phone;
                editor_movil.Value = project.StaffFormatEditorMontajista.staff_format_movil;
                editor_country.Value = project.StaffFormatEditorMontajista.staff_format_country;
                editor_state.Value = project.StaffFormatEditorMontajista.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatEditorMontajista.staff_format_localization_father_id == "")
                {
                    editor_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    editor_departamentoDDL.SelectedValue = project.StaffFormatEditorMontajista.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet editor_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatEditorMontajista.staff_format_localization_father_id + "'");
                editor_municipioDDL.Items.Clear();
                editor_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < editor_municipioDS.Tables[0].Rows.Count; i++)
                {
                    editor_municipioDDL.Items.Add(new ListItem(editor_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), editor_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatEditorMontajista.staff_format_localization_id == "")
                {
                    editor_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    editor_municipioDDL.SelectedValue = project.StaffFormatEditorMontajista.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatEditorMontajista.staff_format_country == "")
                {
                    editor_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    editor_localization_out_of_colombia.Checked = true;
                }

                /* ********************************* */
                /* CARGA INFORMACIÓN DEL CAMAROGRAFO */
                /* ********************************* */
                cameraman_name.Value = project.StaffFormatCamarografo.staff_format_name;
                cameraman_identification_number.Value = project.StaffFormatCamarografo.staff_format_identification_number;
                cameraman_address.Value = project.StaffFormatCamarografo.staff_format_address;
                cameraman_email.Value = project.StaffFormatCamarografo.staff_format_email;
                cameraman_phone.Value = project.StaffFormatCamarografo.staff_format_phone;
                cameraman_movil.Value = project.StaffFormatCamarografo.staff_format_movil;
                cameraman_country.Value = project.StaffFormatCamarografo.staff_format_country;
                cameraman_state.Value = project.StaffFormatCamarografo.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatCamarografo.staff_format_localization_father_id == "")
                {
                    cameraman_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    cameraman_departamentoDDL.SelectedValue = project.StaffFormatCamarografo.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet cameraman_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatCamarografo.staff_format_localization_father_id + "'");
                cameraman_municipioDDL.Items.Clear();
                cameraman_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < cameraman_municipioDS.Tables[0].Rows.Count; i++)
                {
                    cameraman_municipioDDL.Items.Add(new ListItem(cameraman_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), cameraman_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatCamarografo.staff_format_localization_id == "")
                {
                    cameraman_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    cameraman_municipioDDL.SelectedValue = project.StaffFormatCamarografo.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatCamarografo.staff_format_country == "")
                {
                    cameraman_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    cameraman_localization_out_of_colombia.Checked = true;
                }

                /* ********************************* */
                /* CARGA INFORMACIÓN DEL MAQUILLADOR */
                /* ********************************* */
                makeup_artist_name.Value = project.StaffFormatMaquillador.staff_format_name;
                makeup_artist_identification_number.Value = project.StaffFormatMaquillador.staff_format_identification_number;
                makeup_artist_address.Value = project.StaffFormatMaquillador.staff_format_address;
                makeup_artist_email.Value = project.StaffFormatMaquillador.staff_format_email;
                makeup_artist_phone.Value = project.StaffFormatMaquillador.staff_format_phone;
                makeup_artist_movil.Value = project.StaffFormatMaquillador.staff_format_movil;
                makeup_artist_country.Value = project.StaffFormatMaquillador.staff_format_country;
                makeup_artist_state.Value = project.StaffFormatMaquillador.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatMaquillador.staff_format_localization_father_id == "")
                {
                    makeup_artist_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    makeup_artist_departamentoDDL.SelectedValue = project.StaffFormatMaquillador.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet makeup_artist_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatMaquillador.staff_format_localization_father_id + "'");
                makeup_artist_municipioDDL.Items.Clear();
                makeup_artist_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < makeup_artist_municipioDS.Tables[0].Rows.Count; i++)
                {
                    makeup_artist_municipioDDL.Items.Add(new ListItem(makeup_artist_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), makeup_artist_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatMaquillador.staff_format_localization_id == "")
                {
                    makeup_artist_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    makeup_artist_municipioDDL.SelectedValue = project.StaffFormatMaquillador.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatMaquillador.staff_format_country == "")
                {
                    makeup_artist_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    makeup_artist_localization_out_of_colombia.Checked = true;
                }

                /* ********************************* */
                /* CARGA INFORMACIÓN DEL VESTUARISTA */
                /* ********************************* */
                costume_name.Value = project.StaffFormatVestuarista.staff_format_name;
                costume_identification_number.Value = project.StaffFormatVestuarista.staff_format_identification_number;
                costume_address.Value = project.StaffFormatVestuarista.staff_format_address;
                costume_email.Value = project.StaffFormatVestuarista.staff_format_email;
                costume_phone.Value = project.StaffFormatVestuarista.staff_format_phone;
                costume_movil.Value = project.StaffFormatVestuarista.staff_format_movil;
                costume_country.Value = project.StaffFormatVestuarista.staff_format_country;
                costume_state.Value = project.StaffFormatVestuarista.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatVestuarista.staff_format_localization_father_id == "")
                {
                    costume_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    costume_departamentoDDL.SelectedValue = project.StaffFormatVestuarista.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet costume_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatVestuarista.staff_format_localization_father_id + "'");
                costume_municipioDDL.Items.Clear();
                costume_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < costume_municipioDS.Tables[0].Rows.Count; i++)
                {
                    costume_municipioDDL.Items.Add(new ListItem(costume_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), costume_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatVestuarista.staff_format_localization_id == "")
                {
                    costume_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    costume_municipioDDL.SelectedValue = project.StaffFormatVestuarista.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatVestuarista.staff_format_country == "")
                {
                    costume_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    costume_localization_out_of_colombia.Checked = true;
                }

                /* ********************************* */
                /* CARGA INFORMACIÓN DEL AMBIENTADOR */
                /* ********************************* */
                dresser_name.Value = project.StaffFormatAmbientador.staff_format_name;
                dresser_identification_number.Value = project.StaffFormatAmbientador.staff_format_identification_number;
                dresser_address.Value = project.StaffFormatAmbientador.staff_format_address;
                dresser_email.Value = project.StaffFormatAmbientador.staff_format_email;
                dresser_phone.Value = project.StaffFormatAmbientador.staff_format_phone;
                dresser_movil.Value = project.StaffFormatAmbientador.staff_format_movil;
                dresser_country.Value = project.StaffFormatAmbientador.staff_format_country;
                dresser_state.Value = project.StaffFormatAmbientador.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatAmbientador.staff_format_localization_father_id == "")
                {
                    dresser_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    dresser_departamentoDDL.SelectedValue = project.StaffFormatAmbientador.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet dresser_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatAmbientador.staff_format_localization_father_id + "'");
                dresser_municipioDDL.Items.Clear();
                dresser_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < dresser_municipioDS.Tables[0].Rows.Count; i++)
                {
                    dresser_municipioDDL.Items.Add(new ListItem(dresser_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), dresser_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatAmbientador.staff_format_localization_id == "")
                {
                    dresser_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    dresser_municipioDDL.SelectedValue = project.StaffFormatAmbientador.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatAmbientador.staff_format_country == "")
                {
                    dresser_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    dresser_localization_out_of_colombia.Checked = true;
                }

                /* ****************************************** */
                /* CARGA INFORMACIÓN DEL ENCARGADO DE CASTING */
                /* ****************************************** */
                casting_name.Value = project.StaffFormatEncargadoCasting.staff_format_name;
                casting_identification_number.Value = project.StaffFormatEncargadoCasting.staff_format_identification_number;
                casting_address.Value = project.StaffFormatEncargadoCasting.staff_format_address;
                casting_email.Value = project.StaffFormatEncargadoCasting.staff_format_email;
                casting_phone.Value = project.StaffFormatEncargadoCasting.staff_format_phone;
                casting_movil.Value = project.StaffFormatEncargadoCasting.staff_format_movil;
                casting_country.Value = project.StaffFormatEncargadoCasting.staff_format_country;
                casting_state.Value = project.StaffFormatEncargadoCasting.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatEncargadoCasting.staff_format_localization_father_id == "")
                {
                    casting_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    casting_departamentoDDL.SelectedValue = project.StaffFormatEncargadoCasting.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet casting_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatEncargadoCasting.staff_format_localization_father_id + "'");
                casting_municipioDDL.Items.Clear();
                casting_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < casting_municipioDS.Tables[0].Rows.Count; i++)
                {
                    casting_municipioDDL.Items.Add(new ListItem(casting_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), casting_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatEncargadoCasting.staff_format_localization_id == "")
                {
                    casting_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    casting_municipioDDL.SelectedValue = project.StaffFormatEncargadoCasting.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatEncargadoCasting.staff_format_country == "")
                {
                    casting_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    casting_localization_out_of_colombia.Checked = true;
                }

                /* **************************** */
                /* CARGA INFORMACIÓN DEL SCRIPT */
                /* **************************** */
                script_name.Value = project.StaffFormaScript.staff_format_name;
                script_identification_number.Value = project.StaffFormaScript.staff_format_identification_number;
                script_address.Value = project.StaffFormaScript.staff_format_address;
                script_email.Value = project.StaffFormaScript.staff_format_email;
                script_phone.Value = project.StaffFormaScript.staff_format_phone;
                script_movil.Value = project.StaffFormaScript.staff_format_movil;
                script_country.Value = project.StaffFormaScript.staff_format_country;
                script_state.Value = project.StaffFormaScript.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormaScript.staff_format_localization_father_id == "")
                {
                    script_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    script_departamentoDDL.SelectedValue = project.StaffFormaScript.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet script_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormaScript.staff_format_localization_father_id + "'");
                script_municipioDDL.Items.Clear();
                script_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < script_municipioDS.Tables[0].Rows.Count; i++)
                {
                    script_municipioDDL.Items.Add(new ListItem(script_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), script_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormaScript.staff_format_localization_id == "")
                {
                    script_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    script_municipioDDL.SelectedValue = project.StaffFormaScript.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormaScript.staff_format_country == "")
                {
                    script_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    script_localization_out_of_colombia.Checked = true;
                }

                /* ******************************* */
                /* CARGA INFORMACIÓN DEL SONIDISTA */
                /* ******************************* */
                soundman_name.Value = project.StaffFormatSonidista.staff_format_name;
                soundman_identification_number.Value = project.StaffFormatSonidista.staff_format_identification_number;
                soundman_address.Value = project.StaffFormatSonidista.staff_format_address;
                soundman_email.Value = project.StaffFormatSonidista.staff_format_email;
                soundman_phone.Value = project.StaffFormatSonidista.staff_format_phone;
                soundman_movil.Value = project.StaffFormatSonidista.staff_format_movil;
                soundman_country.Value = project.StaffFormatSonidista.staff_format_country;
                soundman_state.Value = project.StaffFormatSonidista.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatSonidista.staff_format_localization_father_id == "")
                {
                    soundman_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    soundman_departamentoDDL.SelectedValue = project.StaffFormatSonidista.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet soundman_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatSonidista.staff_format_localization_father_id + "'");
                soundman_municipioDDL.Items.Clear();
                soundman_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < soundman_municipioDS.Tables[0].Rows.Count; i++)
                {
                    soundman_municipioDDL.Items.Add(new ListItem(soundman_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), soundman_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatSonidista.staff_format_localization_id == "")
                {
                    soundman_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    soundman_municipioDDL.SelectedValue = project.StaffFormatSonidista.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatSonidista.staff_format_country == "")
                {
                    soundman_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    soundman_localization_out_of_colombia.Checked = true;
                }
                //cargamos el arreglo dinamico
                for (int k = 0; k < 13; k++)
                {

                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_name")).Value = project.StaffFormatArreglo[k].staff_format_name;
                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_identification_number")).Value = project.StaffFormatArreglo[k].staff_format_identification_number;
                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_address")).Value = project.StaffFormatArreglo[k].staff_format_address;
                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_email")).Value = project.StaffFormatArreglo[k].staff_format_email;
                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_phone")).Value = project.StaffFormatArreglo[k].staff_format_phone;
                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_movil")).Value = project.StaffFormatArreglo[k].staff_format_movil;
                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_country")).Value = project.StaffFormatArreglo[k].staff_format_country;
                    ((HtmlInputText)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_state")).Value = project.StaffFormatArreglo[k].staff_format_state;

                    /* Marca el departamento seleccionado en el select del departamento */
                    if (project.StaffFormatArreglo[k].staff_format_localization_father_id == "")
                    {

                        ((DropDownList)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_departamentoDDL")).SelectedValue = "0";
                    }
                    else
                    {
                        ((DropDownList)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_departamentoDDL")).SelectedValue =
                            project.StaffFormatArreglo[k].staff_format_localization_father_id;
                    }

                    /* Carga las opciones de municipios segun el departamento seleccionado  */
                    DataSet ds = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatArreglo[k].staff_format_localization_father_id + "'");


                    ((DropDownList)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_municipioDDL")).Items.Clear();
                    ((DropDownList)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_municipioDDL")).Items.Add(new ListItem("Seleccione", "0"));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ((DropDownList)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_municipioDDL")).Items.Add(new ListItem(ds.Tables[0].Rows[i]["localization_name"].ToString(), ds.Tables[0].Rows[i]["localization_id"].ToString()));
                    }

                    /* Selecciona la opcion correcta en el dropdown */
                    if (project.StaffFormatArreglo[k].staff_format_localization_id == "")
                    {
                        ((DropDownList)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_municipioDDL")).SelectedValue = "0";
                    }
                    else
                    {
                        ((DropDownList)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_municipioDDL")).SelectedValue = project.StaffFormatArreglo[k].staff_format_localization_id;
                    }

                    /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                    if (project.StaffFormatArreglo[k].staff_format_country == "")
                    {

                        ((HtmlInputCheckBox)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_localization_out_of_colombia")).Checked = false;
                    }
                    else
                    {
                        ((HtmlInputCheckBox)Page.Controls[0].Controls[1].Controls[0].FindControl("cargo" + ((k + 14)) + "_localization_out_of_colombia")).Checked = true;
                    }
                }

                /* ******************************* */
                /* CARGA INFORMACIÓN DEL MEZCLADOR */
                /* ******************************* */
                mixer_name.Value = project.StaffFormatMezclador.staff_format_name;
                mixer_identification_number.Value = project.StaffFormatMezclador.staff_format_identification_number;
                mixer_address.Value = project.StaffFormatMezclador.staff_format_address;
                mixer_email.Value = project.StaffFormatMezclador.staff_format_email;
                mixer_phone.Value = project.StaffFormatMezclador.staff_format_phone;
                mixer_movil.Value = project.StaffFormatMezclador.staff_format_movil;
                mixer_country.Value = project.StaffFormatMezclador.staff_format_country;
                mixer_state.Value = project.StaffFormatMezclador.staff_format_state;

                /* Marca el departamento seleccionado en el select del departamento */
                if (project.StaffFormatMezclador.staff_format_localization_father_id == "")
                {
                    mixer_departamentoDDL.SelectedValue = "0";
                }
                else
                {
                    mixer_departamentoDDL.SelectedValue = project.StaffFormatMezclador.staff_format_localization_father_id;
                }

                /* Carga las opciones de municipios segun el departamento seleccionado  */
                DataSet mixer_municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + project.StaffFormatMezclador.staff_format_localization_father_id + "'");
                mixer_municipioDDL.Items.Clear();
                mixer_municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < mixer_municipioDS.Tables[0].Rows.Count; i++)
                {
                    mixer_municipioDDL.Items.Add(new ListItem(mixer_municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), mixer_municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                } 

                /* Selecciona la opcion correcta en el dropdown */
                if (project.StaffFormatMezclador.staff_format_localization_id == "")
                {
                    mixer_municipioDDL.SelectedValue = "0";
                }
                else
                {
                    mixer_municipioDDL.SelectedValue = project.StaffFormatMezclador.staff_format_localization_id;
                }

                /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                if (project.StaffFormatMezclador.staff_format_country == "")
                {
                    mixer_localization_out_of_colombia.Checked = false;
                }
                else
                {
                    mixer_localization_out_of_colombia.Checked = true;
                }

                if (this.showAdvancedForm)
                {
                    /* Carga en el formulario el valor que ha sido recuperado de la base de datos 
                     * para el checkbox del estado del formulario
                     */
                    gestion_realizada_sin_revisar.Checked = false;
                    gestion_realizada_solicitar_aclaraciones.Checked = false;
                    gestion_realizada_informacion_correcta.Checked = false;

                    if (project.sectionDatosPersonal.revision_state_id == 11)
                    {
                        gestion_realizada_sin_revisar.Checked = true;
                    }
                    if (project.sectionDatosPersonal.revision_state_id == 10)
                    {
                        gestion_realizada_solicitar_aclaraciones.Checked = true;
                    }
                    if (project.sectionDatosPersonal.revision_state_id == 9)
                    {
                        gestion_realizada_informacion_correcta.Checked = true;
                    }

                    /* Crea las etiquetas que incluyen la imagen que indica el estado de la marca
                    * de revisión en cada pestaña y hace la persistencia en el formulario de acuerdo
                    * a los valores guardados en la base de datos */
                    estado_revision_sin_revisar.Checked = false;
                    estado_revision_revisado.Checked = false;
                    estado_revision_aprobado.Checked = false;

                    if (project.sectionDatosPersonal.revision_mark == "")
                    {
                        estado_revision_sin_revisar.Checked = true;
                    }
                    if (project.sectionDatosPersonal.revision_mark == "revisado")
                    {
                        estado_revision_revisado.Checked = true;
                    }
                    if (project.sectionDatosPersonal.revision_mark == "aprobado")
                    {
                        estado_revision_aprobado.Checked = true;
                    }

                    /*
                     * Carga en el formulario los textos registrados por los
                     * administradores del trámite
                     */
                    solicitud_aclaraciones.Value = project.sectionDatosPersonal.solicitud_aclaraciones;
                    informacion_correcta.Value = project.sectionDatosPersonal.observacion_inicial;

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
                    switch (project.sectionDatosPersonal.revision_mark)
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

                /* Agrega al formulario la información relacionada con la solicitud de aclaraciones */
                if (project.sectionDatosPersonal.solicitud_aclaraciones != "")
                {
                    clarification_request.Text = project.sectionDatosPersonal.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    if (user_role>1 && project_state_id >= 6 )
                    {
                        clarification_request_summary.Text = project.sectionDatosPersonal.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        producer_clarification_summary.Text = project.sectionDatosPersonal.aclaraciones_productor.Replace("\r\n", "<br>");
                    }
                }
                /* Recupera al formulario la información de aclaraciones registrada por el productor */
                producer_clarifications_field.Value = project.sectionDatosPersonal.aclaraciones_productor;

                /* Aplica formatos especiales - campos requeridos vacios - a los campos presentados en el formulario 
                director_name.Attributes["Class"] = (project.StaffFormatDirector.staff_format_name == "") ? "required_field" : "";
                director_identification_number.Attributes["Class"] = (project.StaffFormatDirector.staff_format_identification_number == "") ? "required_field" : "";
                director_address.Attributes["Class"] = (project.StaffFormatDirector.staff_format_address == "") ? "required_field" : "";
                director_email.Attributes["Class"] = (project.StaffFormatDirector.staff_format_email == "") ? "required_field" : "";
                director_phone.Attributes["Class"] = (project.StaffFormatDirector.staff_format_phone == "") ? "required_field" : "";
                director_movil.Attributes["Class"] = (project.StaffFormatDirector.staff_format_movil == "") ? "required_field" : "";
                director_fax.Attributes["Class"] = (project.StaffFormatDirector.staff_format_fax == "") ? "required_field" : "";

                writer_name.Attributes["Class"] = (project.StaffFormatGuionista.staff_format_name == "") ? "required_field" : "";
                writer_identification_number.Attributes["Class"] = (project.StaffFormatGuionista.staff_format_identification_number == "") ? "required_field" : "";
                writer_address.Attributes["Class"] = (project.StaffFormatGuionista.staff_format_address == "") ? "required_field" : "";
                writer_email.Attributes["Class"] = (project.StaffFormatGuionista.staff_format_email == "") ? "required_field" : "";
                writer_phone.Attributes["Class"] = (project.StaffFormatGuionista.staff_format_phone == "") ? "required_field" : "";
                writer_movil.Attributes["Class"] = (project.StaffFormatGuionista.staff_format_movil == "") ? "required_field" : "";
                writer_fax.Attributes["Class"] = (project.StaffFormatGuionista.staff_format_fax == "") ? "required_field" : "";

                photography_director_name.Attributes["Class"] = (project.StaffFormatDirectorFotografía.staff_format_name == "") ? "required_field" : "";
                photography_director_identification_number.Attributes["Class"] = (project.StaffFormatDirectorFotografía.staff_format_identification_number == "") ? "required_field" : "";
                photography_director_address.Attributes["Class"] = (project.StaffFormatDirectorFotografía.staff_format_address == "") ? "required_field" : "";
                photography_director_email.Attributes["Class"] = (project.StaffFormatDirectorFotografía.staff_format_email == "") ? "required_field" : "";
                photography_director_phone.Attributes["Class"] = (project.StaffFormatDirectorFotografía.staff_format_phone == "") ? "required_field" : "";
                photography_director_movil.Attributes["Class"] = (project.StaffFormatDirectorFotografía.staff_format_movil == "") ? "required_field" : "";
                photography_director_fax.Attributes["Class"] = (project.StaffFormatDirectorFotografía.staff_format_fax == "") ? "required_field" : "";

                art_director_name.Attributes["Class"] = (project.StaffFormatDirectorArte.staff_format_name == "") ? "required_field" : "";
                art_director_identification_number.Attributes["Class"] = (project.StaffFormatDirectorArte.staff_format_identification_number == "") ? "required_field" : "";
                art_director_address.Attributes["Class"] = (project.StaffFormatDirectorArte.staff_format_address == "") ? "required_field" : "";
                art_director_email.Attributes["Class"] = (project.StaffFormatDirectorArte.staff_format_email == "") ? "required_field" : "";
                art_director_phone.Attributes["Class"] = (project.StaffFormatDirectorArte.staff_format_phone == "") ? "required_field" : "";
                art_director_movil.Attributes["Class"] = (project.StaffFormatDirectorArte.staff_format_movil == "") ? "required_field" : "";
                art_director_fax.Attributes["Class"] = (project.StaffFormatDirectorArte.staff_format_fax == "") ? "required_field" : "";

                music_author_name.Attributes["Class"] = (project.StaffFormatAutorMusica.staff_format_name == "") ? "required_field" : "";
                music_author_identification_number.Attributes["Class"] = (project.StaffFormatAutorMusica.staff_format_identification_number == "") ? "required_field" : "";
                music_author_address.Attributes["Class"] = (project.StaffFormatAutorMusica.staff_format_address == "") ? "required_field" : "";
                music_author_email.Attributes["Class"] = (project.StaffFormatAutorMusica.staff_format_email == "") ? "required_field" : "";
                music_author_phone.Attributes["Class"] = (project.StaffFormatAutorMusica.staff_format_phone == "") ? "required_field" : "";
                music_author_movil.Attributes["Class"] = (project.StaffFormatAutorMusica.staff_format_movil == "") ? "required_field" : "";
                music_author_fax.Attributes["Class"] = (project.StaffFormatAutorMusica.staff_format_fax == "") ? "required_field" : "";

                editor_name.Attributes["Class"] = (project.StaffFormatEditorMontajista.staff_format_name == "") ? "required_field" : "";
                editor_identification_number.Attributes["Class"] = (project.StaffFormatEditorMontajista.staff_format_identification_number == "") ? "required_field" : "";
                editor_address.Attributes["Class"] = (project.StaffFormatEditorMontajista.staff_format_address == "") ? "required_field" : "";
                editor_email.Attributes["Class"] = (project.StaffFormatEditorMontajista.staff_format_email == "") ? "required_field" : "";
                editor_phone.Attributes["Class"] = (project.StaffFormatEditorMontajista.staff_format_phone == "") ? "required_field" : "";
                editor_movil.Attributes["Class"] = (project.StaffFormatEditorMontajista.staff_format_movil == "") ? "required_field" : "";
                editor_fax.Attributes["Class"] = (project.StaffFormatEditorMontajista.staff_format_fax == "") ? "required_field" : "";

                cameraman_name.Attributes["Class"] = (project.StaffFormatCamarografo.staff_format_name == "") ? "required_field" : "";
                cameraman_identification_number.Attributes["Class"] = (project.StaffFormatCamarografo.staff_format_identification_number == "") ? "required_field" : "";
                cameraman_address.Attributes["Class"] = (project.StaffFormatCamarografo.staff_format_address == "") ? "required_field" : "";
                cameraman_email.Attributes["Class"] = (project.StaffFormatCamarografo.staff_format_email == "") ? "required_field" : "";
                cameraman_phone.Attributes["Class"] = (project.StaffFormatCamarografo.staff_format_phone == "") ? "required_field" : "";
                cameraman_movil.Attributes["Class"] = (project.StaffFormatCamarografo.staff_format_movil == "") ? "required_field" : "";
                cameraman_fax.Attributes["Class"] = (project.StaffFormatCamarografo.staff_format_fax == "") ? "required_field" : "";

                makeup_artist_name.Attributes["Class"] = (project.StaffFormatMaquillador.staff_format_name == "") ? "required_field" : "";
                makeup_artist_identification_number.Attributes["Class"] = (project.StaffFormatMaquillador.staff_format_identification_number == "") ? "required_field" : "";
                makeup_artist_address.Attributes["Class"] = (project.StaffFormatMaquillador.staff_format_address == "") ? "required_field" : "";
                makeup_artist_email.Attributes["Class"] = (project.StaffFormatMaquillador.staff_format_email == "") ? "required_field" : "";
                makeup_artist_phone.Attributes["Class"] = (project.StaffFormatMaquillador.staff_format_phone == "") ? "required_field" : "";
                makeup_artist_movil.Attributes["Class"] = (project.StaffFormatMaquillador.staff_format_movil == "") ? "required_field" : "";
                makeup_artist_fax.Attributes["Class"] = (project.StaffFormatMaquillador.staff_format_fax == "") ? "required_field" : "";

                costume_name.Attributes["Class"] = (project.StaffFormatVestuarista.staff_format_name == "") ? "required_field" : "";
                costume_identification_number.Attributes["Class"] = (project.StaffFormatVestuarista.staff_format_identification_number == "") ? "required_field" : "";
                costume_address.Attributes["Class"] = (project.StaffFormatVestuarista.staff_format_address == "") ? "required_field" : "";
                costume_email.Attributes["Class"] = (project.StaffFormatVestuarista.staff_format_email == "") ? "required_field" : "";
                costume_phone.Attributes["Class"] = (project.StaffFormatVestuarista.staff_format_phone == "") ? "required_field" : "";
                costume_movil.Attributes["Class"] = (project.StaffFormatVestuarista.staff_format_movil == "") ? "required_field" : "";
                costume_fax.Attributes["Class"] = (project.StaffFormatVestuarista.staff_format_fax == "") ? "required_field" : "";

                dresser_name.Attributes["Class"] = (project.StaffFormatAmbientador.staff_format_name == "") ? "required_field" : "";
                dresser_identification_number.Attributes["Class"] = (project.StaffFormatAmbientador.staff_format_identification_number == "") ? "required_field" : "";
                dresser_address.Attributes["Class"] = (project.StaffFormatAmbientador.staff_format_address == "") ? "required_field" : "";
                dresser_email.Attributes["Class"] = (project.StaffFormatAmbientador.staff_format_email == "") ? "required_field" : "";
                dresser_phone.Attributes["Class"] = (project.StaffFormatAmbientador.staff_format_phone == "") ? "required_field" : "";
                dresser_movil.Attributes["Class"] = (project.StaffFormatAmbientador.staff_format_movil == "") ? "required_field" : "";
                dresser_fax.Attributes["Class"] = (project.StaffFormatAmbientador.staff_format_fax == "") ? "required_field" : "";

                casting_name.Attributes["Class"] = (project.StaffFormatEncargadoCasting.staff_format_name == "") ? "required_field" : "";
                casting_identification_number.Attributes["Class"] = (project.StaffFormatEncargadoCasting.staff_format_identification_number == "") ? "required_field" : "";
                casting_address.Attributes["Class"] = (project.StaffFormatEncargadoCasting.staff_format_address == "") ? "required_field" : "";
                casting_email.Attributes["Class"] = (project.StaffFormatEncargadoCasting.staff_format_email == "") ? "required_field" : "";
                casting_phone.Attributes["Class"] = (project.StaffFormatEncargadoCasting.staff_format_phone == "") ? "required_field" : "";
                casting_movil.Attributes["Class"] = (project.StaffFormatEncargadoCasting.staff_format_movil == "") ? "required_field" : "";
                casting_fax.Attributes["Class"] = (project.StaffFormatEncargadoCasting.staff_format_fax == "") ? "required_field" : "";

                script_name.Attributes["Class"] = (project.StaffFormaScript.staff_format_name == "") ? "required_field" : "";
                script_identification_number.Attributes["Class"] = (project.StaffFormaScript.staff_format_identification_number == "") ? "required_field" : "";
                script_address.Attributes["Class"] = (project.StaffFormaScript.staff_format_address == "") ? "required_field" : "";
                script_email.Attributes["Class"] = (project.StaffFormaScript.staff_format_email == "") ? "required_field" : "";
                script_phone.Attributes["Class"] = (project.StaffFormaScript.staff_format_phone == "") ? "required_field" : "";
                script_movil.Attributes["Class"] = (project.StaffFormaScript.staff_format_movil == "") ? "required_field" : "";
                script_fax.Attributes["Class"] = (project.StaffFormaScript.staff_format_fax == "") ? "required_field" : "";

                soundman_name.Attributes["Class"] = (project.StaffFormatSonidista.staff_format_name == "") ? "required_field" : "";
                soundman_identification_number.Attributes["Class"] = (project.StaffFormatSonidista.staff_format_identification_number == "") ? "required_field" : "";
                soundman_address.Attributes["Class"] = (project.StaffFormatSonidista.staff_format_address == "") ? "required_field" : "";
                soundman_email.Attributes["Class"] = (project.StaffFormatSonidista.staff_format_email == "") ? "required_field" : "";
                soundman_phone.Attributes["Class"] = (project.StaffFormatSonidista.staff_format_phone == "") ? "required_field" : "";
                soundman_movil.Attributes["Class"] = (project.StaffFormatSonidista.staff_format_movil == "") ? "required_field" : "";
                soundman_fax.Attributes["Class"] = (project.StaffFormatSonidista.staff_format_fax == "") ? "required_field" : "";

                mixer_name.Attributes["Class"] = (project.StaffFormatMezclador.staff_format_name == "") ? "required_field" : "";
                mixer_identification_number.Attributes["Class"] = (project.StaffFormatMezclador.staff_format_identification_number == "") ? "required_field" : "";
                mixer_address.Attributes["Class"] = (project.StaffFormatMezclador.staff_format_address == "") ? "required_field" : "";
                mixer_email.Attributes["Class"] = (project.StaffFormatMezclador.staff_format_email == "") ? "required_field" : "";
                mixer_phone.Attributes["Class"] = (project.StaffFormatMezclador.staff_format_phone == "") ? "required_field" : "";
                mixer_movil.Attributes["Class"] = (project.StaffFormatMezclador.staff_format_movil == "") ? "required_field" : "";
                mixer_fax.Attributes["Class"] = (project.StaffFormatMezclador.staff_format_fax == "") ? "required_field" : "";*/
            }
        }
    }
}