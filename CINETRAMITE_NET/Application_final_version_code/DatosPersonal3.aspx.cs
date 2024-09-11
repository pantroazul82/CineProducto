using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CineProducto.Bussines;
using DominioCineProducto;
using DominioCineProducto.Data;

namespace CineProducto
{
    public partial class DatosPersonal3 : System.Web.UI.Page
    {
        public bool showDirectorSelect;
        public bool showStaffOptions;
        public bool ProductorPuedeEditar = false;
        public bool showStaffOptionsMessage;
        public bool showPersonalForms;

        public bool showAdvancedForm = false; //Variable que controla la presentación del formulario de administración
        public bool subsanado = false;

        public int project_state_id = 0; //Indica el estado del proyecto, el cual se utiliza para identificar los elementos a presentar particulares de cada estado
        public int section_state_id = 0; //Indica el estado de la sección actual, el cual se utiliza para identificar los elementos a presentar particulares de cada estado.
        public int project_id;
        public int repeater_index;
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

        public string fecha_subsanacion;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            var controlMaster = (System.Web.UI.HtmlControls.HtmlInputHidden)Master.FindControl("_scrollboton");
            controlMaster.Value = false.ToString();
            #region validaciones de session y de permisos
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
            #region creamos variables e inicializamos
            /* Crea un dataset para almacenar la informacion del personal que se vinculará al repetidor 
             * el cual se usa para presentar la información de los adjuntos de la sección a los administradores.
             */
            string items = "";
            DataTable dtAttachment = new DataTable();
            dtAttachment.Columns.Add("attachment_id", typeof(string));
            dtAttachment.Columns.Add("attachment_render", typeof(string));
            dtAttachment.Columns.Add("attachment_father_id", typeof(int));
            dtAttachment.Columns.Add("attachment_company", typeof(string));
            dtAttachment.Columns.Add("project_staff_id", typeof(string));
            DataSet attachmentDS = new DataSet();
            attachmentDS.Tables.Add(dtAttachment);

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Crea el objeto que gestiona la información del personal */
            Staff staff = new Staff();

            /* Crea el objeto del proyecto para manejar la información */
            Project project = new Project();

            /* Creación de la variable que mantiene los identificadores del
             * personal que ya se ha precargado en el dataset que pobla el
               formulario */
            List<Int32> preLoadedStaff = new List<Int32>();

            /* Crea las variables que controlan la presentación de elementos en el formulario */
            showDirectorSelect = true;
            showStaffOptions = false;
            showStaffOptionsMessage = false;
            showPersonalForms = false;

            /* Variable que permite crear el indice par asignar al dataset del repetidor y así poder identificar
             * de forma inequivoca los campos del formulario de cada persona */
            int repeater_index = 1;
            #endregion
            #region Crea un dataset para almacenar la informacion del personal que se vinculará al repetidor
            /* Crea un dataset para almacenar la informacion del personal que se vinculará al repetidor */
            DataTable dtStaff = new DataTable();
            dtStaff.Columns.Add("repeater_index", typeof(int)); //Consecutivo que permite la identificación única de las listas de selección para cada persona
            dtStaff.Columns.Add("project_staff_id", typeof(int)); //Identificador de la información de una persona relacionada con el proyecto
            dtStaff.Columns.Add("project_staff_position", typeof(string)); //Almacena la lista desplegable de cargos disponibles
            dtStaff.Columns.Add("project_staff_position_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_firstname", typeof(string));
            dtStaff.Columns.Add("project_staff_firstname2", typeof(string));
            dtStaff.Columns.Add("project_staff_firstname_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_lastname", typeof(string));
            dtStaff.Columns.Add("project_staff_lastname2", typeof(string));
            dtStaff.Columns.Add("project_staff_genero", typeof(string));
            dtStaff.Columns.Add("project_staff_localization_id", typeof(string));
            dtStaff.Columns.Add("project_staff_lastname_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_identification_type", typeof(string));
            dtStaff.Columns.Add("project_staff_identification_type_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_identification_number", typeof(string));
            dtStaff.Columns.Add("project_staff_identification_number_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_city", typeof(string));
            dtStaff.Columns.Add("project_staff_city_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_state", typeof(string));
            dtStaff.Columns.Add("project_staff_state_id", typeof(string));
            dtStaff.Columns.Add("id_genero", typeof(string));
            dtStaff.Columns.Add("id_etnia", typeof(string));
            dtStaff.Columns.Add("identification_type_id", typeof(string));
            dtStaff.Columns.Add("id_grupo_poblacional", typeof(string));
            dtStaff.Columns.Add("fecha_nacimiento", typeof(string));

            dtStaff.Columns.Add("project_staff_state_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_address", typeof(string));
            dtStaff.Columns.Add("project_staff_address_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_phone", typeof(string));
            dtStaff.Columns.Add("project_staff_phone_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_movil", typeof(string));
            dtStaff.Columns.Add("project_staff_movil_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_email", typeof(string));
            dtStaff.Columns.Add("project_staff_email_css_class", typeof(string));
            dtStaff.Columns.Add("position_name", typeof(string));
            dtStaff.Columns.Add("staff_option_detail_id", typeof(string));
            dtStaff.Columns.Add("additional_option_block", typeof(string));
            dtStaff.Columns.Add("fielset_css_class", typeof(string));
            dtStaff.Columns.Add("project_staff_identification_attachment", typeof(string));
            dtStaff.Columns.Add("project_staff_identification_attachment_checkbox", typeof(string));
            dtStaff.Columns.Add("project_staff_cv_attachment", typeof(string));
            dtStaff.Columns.Add("project_staff_cv_attachment_checkbox", typeof(string));
            dtStaff.Columns.Add("project_staff_contract_attachment", typeof(string));
            dtStaff.Columns.Add("project_staff_contract_attachment_checkbox", typeof(string));
            dtStaff.Columns.Add("attachment_id", typeof(string));
            dtStaff.Columns.Add("attachment_code", typeof(string));
            dtStaff.Columns.Add("attachment_status", typeof(bool));
            dtStaff.Columns.Add("attachment_father_id", typeof(int));
            dtStaff.Columns.Add("adjuntosPendientes", typeof(bool));
            dtStaff.Columns.Add("textoCargo", typeof(string));


            DataSet staffDS = new DataSet();
            staffDS.Tables.Add(dtStaff);
            #endregion
            #region carga el codigo del proyecto
            
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
            }
            else
            {
                Response.Redirect("Lista.aspx", true);
            }
            #endregion
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
            #region Obtiene los textos de los tooltips y los pasa a las funciones de javascript correspondientes
            /* Obtiene los textos de los tooltips y los pasa a las funciones de javascript correspondientes */
            tooltip_staff_hasdomesticdirector.Text = db.GetTooltip("staff_hasdomesticdirector");
            tooltip_staff_option.Text = db.GetTooltip("staff_option");
            tooltip_staff_firstname.Text = db.GetTooltip("staff_firstname");
            tooltip_staff_lastname.Text = db.GetTooltip("staff_lastname");
            tooltip_staff_identification_type.Text = db.GetTooltip("staff_identification_type");
            tooltip_staff_identification_number.Text = db.GetTooltip("staff_identification_number");
            tooltip_staff_city.Text = db.GetTooltip("staff_city");
            tooltip_staff_state.Text = db.GetTooltip("staff_state");
            tooltip_staff_address.Text = db.GetTooltip("staff_address");
            tooltip_staff_phone.Text = db.GetTooltip("staff_phone");
            tooltip_staff_movil.Text = db.GetTooltip("staff_movil");
            tooltip_staff_email.Text = db.GetTooltip("staff_email");

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
            opciones_adicionales.Text = "<a href =\"Lista.aspx\"><< Volver al listado de solicitudes</a>";
            #endregion

            NegocioCineProducto proj = new NegocioCineProducto();
            project myProject = proj.getProject((int)Session["project_id"]);
            #region verificamos si tiene el codigo del proyecto en la sesion y valida los prerequisitos de otros tabs
            /* Si el proyecto ya tiene identificador y aun no está en sesión, es registrado en la variable de sesión */
            if (Session["project_id"] == null && project.project_id > 0)
            {
                Session["project_id"] = project.project_id;
                this.project_id = project.project_id;
            }
            if (Session["project_id"] != null)
            {
                project.LoadProject(Convert.ToInt32(Session["project_id"]));
                this.project_id = Convert.ToInt32(Session["project_id"]);
                /* Guarda en la variable de la clase el estado de la variable */
                this.project_state_id = project.state_id;
                this.section_state_id = project.sectionDatosPersonal.tab_state_id;

                bool subsanacion = false;

                
                var product = Session["ES_PRODUCTOR"].ToString();
                if (bool.Parse(Session["ES_PRODUCTOR"].ToString()))
                {
                    if (myProject.FECHA_SUBSANACION.HasValue && myProject.SUBSANADO.HasValue == true && myProject.FECHA_SUBSANACION > DateTime.Now && myProject.SUBSANACION_ENVIADA == false)
                    {
                        //Mira el hidden
                        subsanacion = true;
                        fecha_subsanacion = myProject.FECHA_SUBSANACION.ToString();
                        subsanado = myProject.SUBSANADO.HasValue;
                        this.project_state_id = 5;
                    }
                    else
                    {
                        fecha_subsanacion = "";
                        subsanado = false;
                    }
                }

                if (this.project_state_id > 1  && this.project_state_id != 5) {
                    cmbProductorNacional.Enabled = false;
                }
            }
            else
            {
                Response.Redirect("Lista.aspx", true);
            }

            /* Define la etiqueta que indica si el director es colombiano */
            if (project.project_has_domestic_director > -1)
            {
                if (Request.Form["change_has_domestic_director"] == null || Request.Form["change_has_domestic_director"] == "0")
                {
                    showDirectorSelect = false;
                    showStaffOptions = true;
                    //has_domestic_director_label.Text = (project.project_has_domestic_director == 0) ? "No" : "Sí";
                }
            }


            ///el nuevo select 
            if (!IsPostBack)
            {
                if (project.project_has_domestic_director == 0 || project.project_has_domestic_director == 1)
                {
                    cmbProductorNacional.SelectedValue = project.project_has_domestic_director.ToString();
                }
                else {
                    NegocioCineProducto neg = new NegocioCineProducto();
                    neg.eliminarProjectStaffByProject(project.project_id);
                }
            }
            
            

            if (project.project_percentage == 0 || project.production_type_id == 0
                || project.project_genre_id == 0 || project.project_type_id == 0)
            {
                pnlMensajeVisible.Visible = true;
                cmbProductorNacional.Enabled = false;
            }

            /* Si se va a presentar el dropdown de selección de nacionalidad del productor se preselecciona el valor
             * almacenado en la base de datos. */
            //hasDomesticDirectorDDL.SelectedValue = project.project_has_domestic_director.ToString();
            #endregion
            #region Valida si se debe presentar las opciones de personal, y si se cumplieron los requisitos de información que se debían registrar en otros formularios (tipo de proyecto y tipo de produccion)
            /* Valida si se debe presentar las opciones de personal, y si se cumplieron los requisitos
             * de información que se debían registrar en otros formularios (tipo de proyecto y tipo de produccion)*/
            if (showStaffOptions == true)
            {
                if (project.project_type_id > 0 && project.production_type_id > 0 && project.project_genre_id > 0)
                {                    
                }
                else
                {
                    
                    
                    //staff_option_label.Text = "Debe registrar el tipo de obra, el tipo de producci&oacute;n y el "
                    //    + "subtipo de producci&oacute;n en la pesta&ntilde;a de datos "
                    //    + "de la obra para continuar con el diligenciamiento de "
                    //    + "este formulario. Gracias.";
                }
            }
            #endregion
            /* Procesamiento de los formularios de las personas */
            if (showPersonalForms == true)
            {

                #region guarda la informacion en la base de datos
                /* Procesa la grabación de la información enviada en el formulario */
                if (Request.Form["submit_personal_data"] != null ||
                    (Request.Form["add_optional_position"] != null && Convert.ToInt32(Request.Form["add_optional_position"]) > 0) ||
                    (Request.Form["remove_optional_position"] != null && Convert.ToInt32(Request.Form["remove_optional_position"]) > 0))
                {
                    if (Request.Form["submit_personal_data"] != null && Request.Form["submit_personal_data"].ToString() != "combo")
                    {
                        controlMaster.Value = true.ToString();
                    }
                    /* Para cada persona se va a almacenar en la base de datos la siguiente información
                     * Cargo -> position_id, Nombre, Apellido, Dirección, Teléfono y Correo electrónico */
                    bool centinelaCiclo = true;
                    int contadorCiclo = 1;
                    List<Int32> sentStaffByForm = new List<Int32>(); //Lista que almacena las opciones de personal enviadas en el formulario

                    while (centinelaCiclo)
                    {
                        /* Validamos is existe el campo */
                        if (Request.Form["project_staff_id_" + contadorCiclo] != null)
                        {
                            /* Obtenemos la información registrada para el usuario */
                            /* Se crea un objeto de tipo staff para crear o actualizar la información de la persona */
                            Staff newPerson = new Staff(Convert.ToInt32(Request.Form["project_staff_id_" + contadorCiclo]));
                            newPerson.project_staff_project_id = project.project_id;
                            newPerson.project_staff_firstname = Request.Form["firstname_" + contadorCiclo];
                            newPerson.project_staff_firstname2 = Request.Form["firstname2_" + contadorCiclo];
                            newPerson.project_staff_lastname = Request.Form["lastname_" + contadorCiclo];
                            newPerson.project_staff_lastname2 = Request.Form["lastname2_" + contadorCiclo];
                            newPerson.project_staff_genero = Request.Form["genero_" + contadorCiclo];
                            newPerson.project_staff_localization_id = Request.Form["localizacion_id_" + contadorCiclo];
                            newPerson.project_staff_identification_type = Request.Form["identification_type_" + contadorCiclo];
                            newPerson.project_staff_identification_number = Request.Form["identification_number_" + contadorCiclo];
                            newPerson.project_staff_city = Request.Form["city_" + contadorCiclo];
                            newPerson.project_staff_state = Request.Form["state_" + contadorCiclo];
                            newPerson.project_staff_state = Request.Form["state_id_" + contadorCiclo];
                            newPerson.project_staff_address = Request.Form["address_" + contadorCiclo];
                            newPerson.project_staff_phone = Request.Form["phone_" + contadorCiclo];
                            newPerson.project_staff_movil = Request.Form["movil_" + contadorCiclo];
                            newPerson.project_staff_email = Request.Form["email_" + contadorCiclo];

                            newPerson.project_staff_position_id = Convert.ToInt32(Request.Form["position_" + contadorCiclo]);
                            if (Request.Form["cmbGenero_" + contadorCiclo] != null && Request.Form["cmbGenero_" + contadorCiclo] != "")
                                newPerson.id_genero = Convert.ToInt32(Request.Form["cmbGenero_" + contadorCiclo]);
                            if (Request.Form["cmbEtnia_" + contadorCiclo] != null && Request.Form["cmbEtnia_" + contadorCiclo] != "")
                                newPerson.id_etnia = Convert.ToInt32(Request.Form["cmbEtnia_" + contadorCiclo]);


                            if (Request.Form["cmbIdentificationType_" + contadorCiclo] != null && Request.Form["cmbIdentificationType_" + contadorCiclo] != "")
                                newPerson.identification_type_id = Convert.ToInt32(Request.Form["cmbIdentificationType_" + contadorCiclo]);

                            if (Request.Form["cmbGrupoPoblacional_" + contadorCiclo] != null && Request.Form["cmbGrupoPoblacional_" + contadorCiclo] != "")
                                newPerson.id_grupo_poblacional = Convert.ToInt32(Request.Form["cmbGrupoPoblacional_" + contadorCiclo]);

                            if (Request.Form["fecha_nacimiento_" + contadorCiclo] != "")
                                newPerson.fecha_nacimiento = DateTime.Parse(Request.Form["fecha_nacimiento_" + contadorCiclo]);

                            //newPerson.Save();

                            sentStaffByForm.Add(newPerson.project_staff_id);
                        }
                        else
                        {
                            centinelaCiclo = false;
                        }

                        /* Aumenta el contador del ciclo */
                        contadorCiclo++;
                    }

                    /* Una vez se ha almacenado en la base de datos la información
                     * del personal enviada en el formulario, se borran los registros
                       de personal que sobran (los que están registrados en la base de
                       datos pero que no fueron enviados en el formulario, pues quedan
                       sin usar, probablemente por un cambio en la opción de personal) */
                    foreach (Staff oldStaffItem in project.staff)
                    {
                        if (!sentStaffByForm.Contains(oldStaffItem.project_staff_id))
                        {
                            oldStaffItem.Delete();
                        }
                    }

                    /* Se verifica si se debe agregar un conjunto de campos para 
                     * el registro de un cargo opcional */
                    if (Request.Form["add_optional_position"] != null && Convert.ToInt32(Request.Form["add_optional_position"]) > 0)
                    {
                        StaffOptionDetail addOptionObj = new StaffOptionDetail();
                        addOptionObj.id = Convert.ToInt32(Request.Form["add_optional_position"]);
                        addOptionObj.LoadData();
                        addOptionObj.position_optional_qty = addOptionObj.position_optional_qty + 1;
                        //addOptionObj.Save();
                    }

                    /* Se verifica si se debe remover un conjunto de campos para 
                     * el registro de un cargo opcional */
                    if (Request.Form["remove_optional_position"] != null && Convert.ToInt32(Request.Form["remove_optional_position"]) > 0)
                    {
                        StaffOptionDetail removeOptionObj = new StaffOptionDetail();
                        removeOptionObj.id = Convert.ToInt32(Request.Form["remove_optional_position"]);
                        removeOptionObj.LoadData();
                        removeOptionObj.position_optional_qty = removeOptionObj.position_optional_qty - 1;
                        //removeOptionObj.Save();
                    }

                    //Se recarga la información para cargar en el formulario la información de personal guardada en la base de datos
                    project.LoadProject(project.project_id);

                    
                    /* Se pasan al objeto del proyecto los valores definidos en el formulario de administración para ser almacenados */
                    if (this.showAdvancedForm)
                    {
                        /* Interpretación del valor enviado del formulario para la gestión realizada */
                        project.sectionDatosPersonal.revision_state_id = 0;

                        if (gestion_realizada_sin_revisar.Checked)
                        {
                            project.sectionDatosPersonal.revision_state_id = 11;
                            project.sectionDatosPersonal.tab_state_id = 11;
                        }
                        if (gestion_realizada_solicitar_aclaraciones.Checked)
                        {
                            project.sectionDatosPersonal.revision_state_id = 10;
                            project.sectionDatosPersonal.tab_state_id = 10;
                        }
                        if (gestion_realizada_informacion_correcta.Checked)
                        {
                            project.sectionDatosPersonal.revision_state_id = 9;
                            project.sectionDatosPersonal.tab_state_id = 9;
                        }

                        /* Valida si se modificó el texto de la solicitud de aclaraciones para grabarla y actualizar la fecha */
                        if (project.sectionDatosPersonal.solicitud_aclaraciones != solicitud_aclaraciones.Value)
                        {
                            project.sectionDatosPersonal.solicitud_aclaraciones = solicitud_aclaraciones.Value;
                            project.sectionDatosPersonal.solicitud_aclaraciones_date = DateTime.Now;
                        }
                        /* Valida si se modificó el texto de la solicitud de la primera observación para grabarla y actualizar la fecha */
                        if (project.sectionDatosPersonal.observacion_inicial != informacion_correcta.Value)
                        {
                            project.sectionDatosPersonal.observacion_inicial = informacion_correcta.Value;
                            project.sectionDatosPersonal.observacion_inicial_date = DateTime.Now;
                        }
                        project.sectionDatosPersonal.modified = DateTime.Now;

                        /* Se almacena la información registrada sobre el estado de revisión de la pestaña */
                        if (estado_revision_sin_revisar.Checked)
                        {
                            project.sectionDatosPersonal.revision_mark = "";
                        }
                        if (estado_revision_revisado.Checked)
                        {
                            project.sectionDatosPersonal.revision_mark = "revisado";
                        }
                        if (estado_revision_aprobado.Checked)
                        {
                            project.sectionDatosPersonal.revision_mark = "aprobado";
                        }

                        //project.Save(); // se graba la información del proyecto para guardar la información de los formularios de administración

                        /* Si es administrador se almacena la información sobre aprobación de adjuntos */

                        /* Se obtiene el detalle de la opción de presonal seleccionada */
                        DataSet staffOptionDetailDSTemp = staff.getStaffOptionDetail(project.project_staff_option_id, project.version);

                        /* Agrega las opciones al repetidor */
                        int staffcount = 0;

                        foreach (DataRow item in staffOptionDetailDSTemp.Tables[0].Rows)
                        {
                            int personalQty = (int)item["staff_option_detail_quantity"];
                            for (int i = 0; i < personalQty; i++)
                            {
                                staffcount++;
                                /* Se identifica si existe un registro de personal guardado para
                                * el cargo y la iteración actual para precargarlo */
                                Staff loadedStaff = new Staff();
                                foreach (Staff staffItem in project.staff)
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

                                /* Se verifica la información de aprobación de adjuntos enviada en el formulario */
                                string identification_attachment_var_name = "identification_attachment_approved_" + loadedStaff.project_staff_id;
                                if (Request.Form[identification_attachment_var_name] != null && Convert.ToInt32(Request.Form[identification_attachment_var_name]) > 0)
                                {
                                    loadedStaff.project_staff_identification_approved = 1;
                                }
                                else
                                {
                                    loadedStaff.project_staff_identification_approved = 0;
                                }

                                string cv_attachment_var_name = "cv_attachment_approved_" + loadedStaff.project_staff_id;
                                if (Request.Form[cv_attachment_var_name] != null && Convert.ToInt32(Request.Form[cv_attachment_var_name]) > 0)
                                {
                                    loadedStaff.project_staff_cv_approved = 1;
                                }
                                else
                                {
                                    loadedStaff.project_staff_cv_approved = 0;
                                }

                                string contract_attachment_var_name = "contract_attachment_approved_" + loadedStaff.project_staff_id;
                                if (Request.Form[contract_attachment_var_name] != null && Convert.ToInt32(Request.Form[contract_attachment_var_name]) > 0)
                                {
                                    loadedStaff.project_staff_contract_approved = 1;
                                }
                                else
                                {
                                    loadedStaff.project_staff_contract_approved = 0;
                                }

                                //loadedStaff.Save();
                                foreach (ProjectAttachment attachment in project.attachment)
                                {
                                    if (attachment.attachment.attachment_father_id == 63) //Solo los adjuntos de datos del productor y financiación  /// SE CAMBIAN LOS ATACHMEN AK EN attachment_id 41 POR 63
                                    {
                                        ProjectAttachment projectAttachmentObj = new ProjectAttachment();
                                        string approve_var_name = "attachment_approved_" + attachment.attachment.attachment_id + "_" + staffcount;
                                        string request = Request.Form[approve_var_name];

                                        //projectAttachmentObj.LoadPersonalProjectAttachment(project.project_id,
                                        //  attachment.attachment.attachment_id, staffcount);

                                        projectAttachmentObj.LoadPersonalProjectAttachmentByProject_staff(project.project_id,
                                            attachment.attachment.attachment_id, loadedStaff.project_staff_id);


                                        projectAttachmentObj.project_attachment_approved = 0;
                                        //projectAttachmentObj.Save();
                                        if (Request.Form[approve_var_name] != null && Convert.ToInt32(Request.Form[approve_var_name]) > 0)
                                        {
                                            projectAttachmentObj.LoadProjectAttachment(Convert.ToInt32(request));
                                            projectAttachmentObj.project_attachment_approved = 1;
                                            //projectAttachmentObj.Save();
                                        }


                                    }
                                }
                            }


                        }

                        project.LoadProject(project.project_id);
                    }
                }

                #endregion

                DominioCineProducto.NegocioCineProducto negCP = new DominioCineProducto.NegocioCineProducto();
                List<DominioCineProducto.Data.localization> localizaciones = negCP.getLocalizacionesByPadre("0");

                /* Se inicializa el arreglo de cargos registrados para evitar 
                 * conflictos con información residual, de un uso anterior de la variable */
                preLoadedStaff = new List<Int32>();

                /* Se obtiene el detalle de la opción de presonal seleccionada */
                DataSet staffOptionDetailDS = staff.getStaffOptionDetail(project.project_staff_option_id, project.version);
                /* carga los grupos que aplican al proyecto por ejemplo
                  27	Personal artístico ficción	4
                  26	Actor Protagónico	2
                  39	Personal técnico ficción	7
                  212	Director	1
                    **/
                /* Agrega las opciones al repetidor */
                foreach (DataRow item in staffOptionDetailDS.Tables[0].Rows)
                {
                    /* Se inicializa la variable que almacena los cargos */
                    string childPositionsHtml = "";

                    /* Se inicializa la variable que almacena el bloque de opciones de los cargos 
                     * adicionales, según el caso estará en blanco, o incluirá la opción de agregar un cargo
                     * o incluirá la opción de eliminar un cargo. */
                    string additionalOptionBlockHtml = "";

                    /* Se inicializa la variable que tiene los tipos de documentos */
                    string identificationTypeHtml = "";

                    List<string> identificationTypeList = new List<string>();
                    identificationTypeList.Add("CC");
                    identificationTypeList.Add("CE");
                    identificationTypeList.Add("TI");
                    identificationTypeList.Add("NUI");

                    /* Obtiene el listado de cargos para el tipo de personal seleccionado */
                    DataSet childPositions = staff.getChildPositions((int)item["position_id"]);

                    /* Se agregan las opciones para el personal obligatorio de la opción de personal */
                    int personalQty = (int)item["staff_option_detail_quantity"];
                    for (int i = 0; i < personalQty; i++)
                    {
                        /* Se identifica si existe un registro de personal guardado para
                         * el cargo y la iteración actual para precargarlo en el formulario 
                         * que se presentará */
                        Staff loadedStaff = new Staff();
                        foreach (Staff staffItem in project.staff)
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

                        if (loadedStaff.project_staff_id == 0 && loadedStaff.project_staff_project_id == 0)
                        {//si entra aca significa que no ha guardado los cargos
                            loadedStaff.project_staff_project_id = this.project_id;
                            loadedStaff.project_staff_position_id = (int)item["position_id"];
                            //loadedStaff.Save();

                        }

                        #region en child positions estan los cargos, dependiendo la cantidad hace un combo o un cuadro de texto
                        /* Si no existen cargos se deja en blanco, si existe un cargo se presenta 
                         * como una etiqueta y si existen varios cargos se crea una lista desplegable */
                        if (childPositions.Tables[0].Rows.Count == 0)
                        {
                            childPositionsHtml = "<input style=\"min-width:300px !important\" type=\"hidden\" name=\"position_" + repeater_index +
                                                 "\" id=\"position_" + repeater_index + "\" value=\"" +
                                                 item["position_id"] + "\"/>";
                        }
                        else if (childPositions.Tables[0].Rows.Count == 1)
                        {
                            childPositionsHtml = "<li>" +
                                                 "<input style=\"min-width:300px !important\" type=\"hidden\" name=\"position_" + repeater_index +
                                                 "\" id=\"position_" + repeater_index + "\" value=\"" +
                                                 childPositions.Tables[0].Rows[0]["position_id"] + "\"/>" +
                                                 "<div class=\"field_label\">Cargo:</div>" +
                                                 "<div class=\"field_input\">" + childPositions.Tables[0].Rows[0]["position_name"].ToString() + "</div></li>";
                        }
                        else if (childPositions.Tables[0].Rows.Count > 1)
                        {
                            childPositionsHtml = "<li>" +
                                                 "<div class=\"field_label\">Cargo:<span class=\"required_field_text\">*</span></div>" +
                                                 "<div class=\"field_input\">" +
                                                 "<select style=\"FONT-WEIGHT:BOLD;font-size:14px; min-width:300px !important\"  class=\"user-input tooltip-child-position\" name=\"position_" + repeater_index + "\" id=\"position_" + repeater_index + "\">";
                            foreach (DataRow childPosition in childPositions.Tables[0].Rows)
                            {
                                string childOptionSelected = "";
                                if (Convert.ToInt32(childPosition["position_id"]) == loadedStaff.project_staff_position_id)
                                {
                                    childOptionSelected = " selected ";
                                }
                                childPositionsHtml = childPositionsHtml + "<option value=\"" +
                                                     childPosition["position_id"] + "\" " + childOptionSelected + ">" +
                                                     childPosition["position_name"] + "</option>";
                            }
                            childPositionsHtml = childPositionsHtml + "</select>" + "</div></li>";
                        }
                        #endregion

                        if ((personalQty - 1) == i) //Se valida que se esté incluyendo la opción de agregar la opción en la última iteración
                        {
                            additionalOptionBlockHtml = "<div class=\"link_agregar_cargo\" onclick=\"agregarCargo(" + item["staff_option_detail_id"] + ");\">Agregar cargo (+)</div>";
                        }
                        else
                        {
                            additionalOptionBlockHtml = "";
                        }
                        /* LOS ADMINISTRADORES SE LES HACE VISIBLE EL CHECKBOX DE APROBAR O RECHAZAR EL ADJUNTO*/
                        /* Agrega las opciones al repetidor */
                        Attachment adjuntoObj = new Attachment();
                        // cargamos los tipos de adjuntos
                        List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(project, 63, 0); /// SE CAMBIAN LOS ATACHMEN AK EN attachment_id 41 POR 63
                        // Si ya existe infirmación del productor se mustran los adjuntos

                        DataRow newRow = staffDS.Tables[0].NewRow();
                        #region asigna valores al nueva fila newRow
                        newRow["repeater_index"] = repeater_index;
                        newRow["project_staff_id"] = loadedStaff.project_staff_id;
                        newRow["project_staff_position"] = childPositionsHtml;
                        newRow["project_staff_position_css_class"] = "";
                        newRow["project_staff_firstname"] = loadedStaff.project_staff_firstname;
                        newRow["project_staff_firstname2"] = loadedStaff.project_staff_firstname2;
                        newRow["project_staff_firstname_css_class"] = (loadedStaff.project_staff_firstname == "") ? "required_field user-input tooltip-staff-firstname" : "user-input tooltip-staff-firstname";
                        newRow["project_staff_lastname"] = loadedStaff.project_staff_lastname;
                        newRow["project_staff_lastname2"] = loadedStaff.project_staff_lastname2;
                        newRow["project_staff_genero"] = loadedStaff.project_staff_genero;

                        newRow["project_staff_lastname_css_class"] = (loadedStaff.project_staff_lastname == "") ? "required_field user-input tooltip-staff-lastname" : "user-input tooltip-staff-lastname";
                        //newRow["project_staff_identification_type"] = identificationTypeHtml;
                        //newRow["project_staff_identification_type_css_class"] = "user-input tooltip-staff-identification-type";
                        newRow["project_staff_identification_number"] = loadedStaff.project_staff_identification_number;
                        newRow["project_staff_identification_number_css_class"] = (loadedStaff.project_staff_identification_number == "") ? "required_field user-input tooltip-staff-identification-number" : "user-input tooltip-staff-identification-number";
                        newRow["project_staff_city"] = loadedStaff.project_staff_city;
                        newRow["project_staff_city_css_class"] = (loadedStaff.project_staff_city == "") ? "required_field user-input tooltip-staff-city" : "user-input tooltip-staff-city";
                        newRow["project_staff_state"] = loadedStaff.project_staff_state;

                        string valueSelectedDepto = "";
                        if (loadedStaff.project_staff_localization_id != null && loadedStaff.project_staff_localization_id != "")
                        {
                            valueSelectedDepto = negCP.getLocalizacionById(loadedStaff.project_staff_localization_id).localization_father_id;

                        }
                        newRow["project_staff_state_id"] = getSelectDepto(localizaciones, repeater_index.ToString(), valueSelectedDepto);


                        List<DominioCineProducto.Data.genero> generos = negCP.getGeneros();
                        newRow["id_genero"] = getSelectGenero(generos, repeater_index.ToString(), loadedStaff.id_genero.ToString());

                        List<DominioCineProducto.Data.etnia> etnias = negCP.getEtnias();
                        newRow["id_etnia"] = getSelectEtnia(etnias, repeater_index.ToString(), loadedStaff.id_etnia.ToString());


                        List<DominioCineProducto.Data.identification_type> identification_types = negCP.getIdentificationTypes();
                        newRow["identification_type_id"] = getSelectIdentificationType(identification_types, repeater_index.ToString(), loadedStaff.identification_type_id.ToString());


                        List<DominioCineProducto.Data.grupo_poblacional> grupos_poblacionales = negCP.getGruposPoblacionales();
                        newRow["id_grupo_poblacional"] = getSelectGrupoPoblacional(grupos_poblacionales, repeater_index.ToString(), loadedStaff.id_grupo_poblacional.ToString());

                        if (loadedStaff.fecha_nacimiento != null)
                        {
                            string year = DateTime.Parse(loadedStaff.fecha_nacimiento.ToString()).Year.ToString();
                            string mes = DateTime.Parse(loadedStaff.fecha_nacimiento.ToString()).Month.ToString();
                            string dia = DateTime.Parse(loadedStaff.fecha_nacimiento.ToString()).Day.ToString();

                            newRow["fecha_nacimiento"] = year + "-" + mes + "-" + dia;
                        }


                        if (loadedStaff.project_staff_localization_id != null && loadedStaff.project_staff_localization_id != "")
                        {
                            newRow["project_staff_localization_id"] = getSelectMunicipio(negCP.getLocalizacionesByPadre(valueSelectedDepto), repeater_index.ToString(), loadedStaff.project_staff_localization_id);
                        }
                        else
                        {
                            newRow["project_staff_localization_id"] = getSelectMunicipio(null, repeater_index.ToString(), "");
                        }

                        newRow["project_staff_state_css_class"] = (loadedStaff.project_staff_state == "") ? "required_field user-input tooltip-staff-state" : "user-input tooltip-staff-state";
                        newRow["project_staff_address"] = loadedStaff.project_staff_address;
                        newRow["project_staff_address_css_class"] = (loadedStaff.project_staff_address == "") ? "required_field user-input tooltip-staff-address" : "user-input tooltip-staff-address";
                        newRow["project_staff_phone"] = loadedStaff.project_staff_phone;
                        newRow["project_staff_phone_css_class"] = (loadedStaff.project_staff_phone == "") ? "required_field user-input tooltip-staff-phone" : "user-input tooltip-staff-phone";
                        newRow["project_staff_movil"] = loadedStaff.project_staff_movil;
                        newRow["project_staff_movil_css_class"] = (loadedStaff.project_staff_movil == "") ? "required_field user-input tooltip-staff-movil" : "user-input tooltip-staff-movil";
                        newRow["project_staff_email"] = loadedStaff.project_staff_email;
                        newRow["project_staff_email_css_class"] = (loadedStaff.project_staff_email == "") ? "required_field user-input tooltip-staff-email" : "user-input tooltip-staff-email";
                        newRow["position_name"] = item["position_name"];
                        newRow["staff_option_detail_id"] = item["staff_option_detail_id"];
                        if (childPositions.Tables[0].Rows.Count > 0 && childPositions.Tables[0].Select("position_id=" + loadedStaff.project_staff_position_id).Length > 0)
                        {
                            newRow["textoCargo"] = childPositions.Tables[0].Select("position_id=" + loadedStaff.project_staff_position_id)[0]["position_name"].ToString();
                        }
                        else
                        {
                            newRow["textoCargo"] = "";
                        }

                        newRow["additional_option_block"] = additionalOptionBlockHtml;
                        newRow["fielset_css_class"] = "";

                        newRow["additional_option_block"] = "";

                        #endregion
                        #region carga los adjuntos para e cargo en el cual esta haciendo la iteración.
                        items = "";
                        bool adjuntosPendientes = false;
                        foreach (Attachment current_item in adjuntos)
                        {

                            DataRow newRows = attachmentDS.Tables[0].NewRow();
                            // se define el objeto de tipo adjunto projecto
                            ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();
                            // se verifica si exite un adjunto ya cargado
                            //projectAttachmentCurrent.LoadPersonalProjectAttachment(project.project_id, current_item.attachment_id, repeater_index);
                            projectAttachmentCurrent.LoadPersonalProjectAttachmentByProject_staff(project.project_id, current_item.attachment_id, loadedStaff.project_staff_id);


                            int projectAttacmentId = 0;
                            if (projectAttachmentCurrent.project_attachment_approved == 0)
                            {
                                adjuntosPendientes = true;
                            }
                            if (projectAttachmentCurrent.project_attachment_id != 0)
                            {
                                projectAttacmentId = projectAttachmentCurrent.project_attachment_id;
                                //codigo TEMPORAL hasta la transicion de que todos los adjuntos tengan el codigo de cargo
                                //AL CREAR EL FILE UPLOAD SE LE PASA EL PROJECT_STAFF_ID
                                projectAttachmentCurrent.project_staff_id = loadedStaff.project_staff_id;
                                //projectAttachmentCurrent.Save();
                                //FIN CODIGO TEMPORAL
                            }

                            newRows["attachment_id"] = current_item.attachment_id + "_" + repeater_index;
                            newRows["attachment_father_id"] = current_item.attachment_father_id;
                            newRows["attachment_render"] = project.renderAttachmentsPersonal(current_item.attachment_id,
                                                                                    projectAttacmentId,
                                                                                    current_item.attachment_name,
                                                                                    projectAttachmentCurrent.project_attachment_path,
                                                                                    showAdvancedForm,
                                                                                    projectAttachmentCurrent.project_attachment_approved,
                                                                                    current_item.attachment_required, repeater_index,
                                                                                    this.project_state_id,
                                                                                    this.user_role, projectAttachmentCurrent.nombre_original);
                            items += newRows["attachment_render"].ToString();
                            newRows["project_staff_id"] = loadedStaff.project_staff_id;
                            attachmentDS.Tables[0].Rows.Add(newRows);
                        }

                        if (adjuntosPendientes)
                            newRow["attachment_status"] = false;
                        else
                            newRow["attachment_status"] = true;

                        if (project_state_id != 5 && this.user_role > 1)
                        //solicitud de acalraciones y rol de prodcutor
                        {
                            adjuntosPendientes = false;
                        }
                        //si adjuntos pendientes permite editar el formulario
                        //los usuarios de la direccion siempre deberian poder hacerlo
                        if (this.user_role > 1)
                        {
                            adjuntosPendientes = true;
                        }

                        newRow["adjuntosPendientes"] = adjuntosPendientes;
                        if (!adjuntosPendientes)
                        {
                            newRow["project_staff_position"] = newRow["project_staff_position"].ToString().Replace("<select style=\"min-width:300px !important\" ", "<select style=\"min-width:300px !important\"  disabled");
                        }
                        newRow["Attachment_code"] = items;
                        //Agrega las opciones al repetidor uploadify
                        //AttachmentRepeater2.DataSource = attachmentDS;
                        //AttachmentRepeater2.DataBind();

                        #endregion
                        /* ### FIN CODIGO ADJUNTOS ###*/
                        if (loadedStaff.project_staff_identification_attachment == "")
                        {
                            newRow["project_staff_identification_attachment"] = "Cédula";
                            newRow["project_staff_identification_attachment_checkbox"] = "";
                            newRow["project_staff_cv_attachment"] = "Hoja de vida";
                            newRow["project_staff_cv_attachment_checkbox"] = "";
                            newRow["project_staff_contract_attachment"] = "Contrato";
                            newRow["project_staff_contract_attachment_checkbox"] = "";
                        }
                        else
                        {
                            #region a esta parte del codigo nunca esta entrando //negativo si entra
                            string approved_identification_attachment = "";
                            string approved_cv_attachment = "";
                            string approved_contract_attachment = "";

                            if (loadedStaff.project_staff_identification_approved == 1)
                            {
                                approved_identification_attachment = "checked";
                            }

                            if (loadedStaff.project_staff_cv_approved == 1)
                            {
                                approved_cv_attachment = "checked";
                            }

                            if (loadedStaff.project_staff_contract_approved == 1)
                            {
                                approved_contract_attachment = "checked";
                            }

                            //Si es administrador presentamos los checkbox 
                            if (this.showAdvancedForm)
                            {
                                newRow["project_staff_identification_attachment_checkbox"] = "<input style=\"min-width:300px !important\" type=\"checkbox\" id=\"identification_attachment_approved_" + loadedStaff.project_staff_id + "\" name=\"identification_attachment_approved_" + loadedStaff.project_staff_id + "\" value=\"" + loadedStaff.project_staff_id + "\" " + approved_identification_attachment + "/>";
                                newRow["project_staff_cv_attachment_checkbox"] = "<input style=\"min-width:300px !important\" type=\"checkbox\" id=\"cv_attachment_approved_" + loadedStaff.project_staff_id + "\" name=\"cv_attachment_approved_" + loadedStaff.project_staff_id + "\" value=\"" + loadedStaff.project_staff_id + "\" " + approved_cv_attachment + "/>";
                                newRow["project_staff_contract_attachment_checkbox"] = "<input style=\"min-width:300px !important\" type=\"checkbox\" id=\"contract_attachment_approved_" + loadedStaff.project_staff_id + "\" name=\"contract_attachment_approved_" + loadedStaff.project_staff_id + "\" value=\"" + loadedStaff.project_staff_id + "\" " + approved_contract_attachment + "/>";
                            }
                            else
                            {
                                newRow["project_staff_identification_attachment_checkbox"] = "";
                                newRow["project_staff_cv_attachment_checkbox"] = "";
                                newRow["project_staff_contract_attachment_checkbox"] = "";
                            }



                            //*******************///




                            string attachment_text = "";
                            //Identificacion
                            if (loadedStaff.project_staff_identification_attachment != "" && loadedStaff.project_staff_identification_attachment != null)
                            {
                                attachment_text = "<a target=\"_blank\" href=\"" + loadedStaff.project_staff_identification_attachment + "\">Cédula</a>";
                            }
                            else
                            {
                                attachment_text = "Cédula";
                            }
                            items = "<div class='field_label'><span id=\"name_cedula" + repeater_index + "\">" + newRow["project_staff_identification_attachment_checkbox"] + attachment_text + "</span>*</div>"
                                      + "<div class='upload_input field_input'>"
                                      + "<div id='FileUpload_cedula" + repeater_index + "' class='fileUpload' />"
                                      + "</div>";
                            newRow["project_staff_identification_attachment"] = items;
                            //Hoja de vida
                            if (loadedStaff.project_staff_cv_attachment != "" && loadedStaff.project_staff_cv_attachment != null)
                            {
                                attachment_text = "<a target=\"_blank\" href=\"" + loadedStaff.project_staff_cv_attachment + "\">Hoja de Vida</a>";
                            }

                            else
                            {
                                attachment_text = "Hoja de Vida";
                            }
                            items = "<div class='field_label'><span id=\"name_cv" + repeater_index + "\">" + newRow["project_staff_identification_attachment_checkbox"] + attachment_text + "</span>*</div>"
                                      + "<div class='upload_input field_input'>"
                                      + "<div id='FileUpload_cv" + repeater_index + "' class='fileUpload' />"
                                      + "</div>";
                            newRow["project_staff_cv_attachment"] = items;
                            //Contrato
                            if (loadedStaff.project_staff_contract_attachment != "" && loadedStaff.project_staff_contract_attachment != null)
                            {
                                attachment_text = "<a target=\"_blank\" href=\"" + loadedStaff.project_staff_contract_attachment + "\">Contrato</a>";
                            }
                            else
                            {
                                attachment_text = "Contrato";
                            }
                            items = "<div class='field_label'><span id=\"name_cv" + repeater_index + "\">" + newRow["project_staff_identification_attachment_checkbox"] + attachment_text + "</span>*</div>"
                                      + "<div class='upload_input field_input'>"
                                      + "<div id='FileUpload_contrato" + repeater_index + "' class='fileUpload' />"
                                      + "</div>";
                            newRow["project_staff_contract_attachment"] = items;
                            #endregion
                        }
                        staffDS.Tables[0].Rows.Add(newRow);
                        this.repeater_index = repeater_index;
                        repeater_index++;

                    }

                    /* Se agregan las opciones para el personal opcional de la opción de personal */
                    int personalOptionalQty = (int)item["staff_option_detail_optional_quantity"];
                    for (int i = 0; i < personalOptionalQty; i++)
                    {
                        #region hace el ciclo nuevamente y carga algo asi como los opcionales, creo que esto no esta configurado, por lo tanto nunca ingresa aca
                        /* Se identifica si existe un registro de personal guardado para
                         * el cargo y la iteración actual para precargarlo en el formulario 
                         * que se presentará */
                        Staff loadedStaff = new Staff();
                        foreach (Staff staffItem in project.staff)
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

                        /* Si no existen cargos se deja en blanco, si existe un cargo se presenta 
                         * como una etiqueta y si existen varios cargos se crea una lista desplegable */
                        if (childPositions.Tables[0].Rows.Count == 0)
                        {
                            childPositionsHtml = "<input style=\"min-width:300px !important\" type=\"hidden\" name=\"position_" + repeater_index +
                                                 "\" id=\"position_" + repeater_index + "\" value=\"" +
                                                 item["position_id"] + "\"/>";
                        }
                        else if (childPositions.Tables[0].Rows.Count == 1)
                        {
                            childPositionsHtml = "<li>" +
                                                 "<input style=\"min-width:300px !important\" type=\"hidden\" name=\"position_" + repeater_index +
                                                 "\" id=\"position_" + repeater_index + "\" value=\"" +
                                                 childPositions.Tables[0].Rows[0]["position_id"] + "\"/>" +
                                                 "<div class=\"field_label\">Cargo:</div>" +
                                                 "<div class=\"field_input\">" + childPositions.Tables[0].Rows[0]["position_name"].ToString() + "</div></li>";
                        }
                        else if (childPositions.Tables[0].Rows.Count > 1)
                        {
                            childPositionsHtml = "<li>" +
                                                 "<div class=\"field_label\">Cargo:<span class=\"required_field_text\">*</span></div>" +
                                                 "<div class=\"field_input\">" +
                                                 "<select style=\"min-width:300px !important\"  name=\"position_" + repeater_index + "\" id=\"position_" + repeater_index + "\">";
                            foreach (DataRow childPosition in childPositions.Tables[0].Rows)
                            {
                                string childOptionSelected = "";
                                if (Convert.ToInt32(childPosition["position_id"]) == loadedStaff.project_staff_position_id)
                                {
                                    childOptionSelected = " selected ";
                                }
                                childPositionsHtml = childPositionsHtml + "<option value=\"" +
                                                     childPosition["position_id"] + "\" " + childOptionSelected + ">" +
                                                     childPosition["position_name"] + "</option>";
                            }
                            childPositionsHtml = childPositionsHtml + "</select>" + "</div></li>";
                        }

                        /* Crea el select de los tipos de documento */
                        identificationTypeHtml = "<li>" +
                                                 "<div class=\"field_label\">Tipo de documento:<span class=\"required_field_text\">*</span></div>" +
                                                 "<div class=\"field_input\">" +
                                                 "<select style=\"min-width:300px !important\"  class=\"user-input tooltip-staff-identification-type\" name=\"identification_type_" + repeater_index + "\" id=\"identification_type_" + repeater_index + "\">";
                        foreach (string identificationType in identificationTypeList)
                        {
                            string identificationTypeOptionSelected = "";
                            if (identificationType == loadedStaff.project_staff_identification_type)
                            {
                                identificationTypeOptionSelected = " selected ";
                            }
                            identificationTypeHtml = identificationTypeHtml + "<option value=\"" +
                                                 identificationType + "\" " + identificationTypeOptionSelected + ">" +
                                                 identificationType + "</option>";
                        }
                        identificationTypeHtml = identificationTypeHtml + "</select>" + "</div></li>";

                        /* Se genera el código del bloque de opciones adicionales */

                        additionalOptionBlockHtml = "<div class=\"link_agregar_cargo\" onclick=\"removerCargo(" + item["staff_option_detail_id"] + ");\">Quitar cargo (-)</div>";

                        DataRow newRow = staffDS.Tables[0].NewRow();
                        newRow["repeater_index"] = repeater_index;
                        newRow["project_staff_id"] = loadedStaff.project_staff_id;
                        newRow["project_staff_position"] = childPositionsHtml;
                        newRow["project_staff_position_css_class"] = "";
                        newRow["project_staff_firstname"] = loadedStaff.project_staff_firstname;
                        newRow["project_staff_firstname2"] = loadedStaff.project_staff_firstname2;
                        newRow["project_staff_firstname_css_class"] = (loadedStaff.project_staff_firstname == "") ? "required_field user-input tooltip-staff-firstname" : "user-input tooltip-staff-firstname";
                        newRow["project_staff_lastname"] = loadedStaff.project_staff_lastname;
                        newRow["project_staff_lastname2"] = loadedStaff.project_staff_lastname2;
                        newRow["project_staff_genero"] = loadedStaff.project_staff_genero;
                        newRow["project_staff_localization_id"] = loadedStaff.project_staff_localization_id;
                        newRow["project_staff_lastname_css_class"] = (loadedStaff.project_staff_lastname == "") ? "required_field user-input tooltip-staff-lastname" : "user-input tooltip-staff-lastname";
                        //newRow["project_staff_identification_type"] = identificationTypeHtml;
                        //newRow["project_staff_identification_type_css_class"] = "user-input tooltip-staff-identification-type";
                        newRow["project_staff_identification_number"] = loadedStaff.project_staff_identification_number;
                        newRow["project_staff_identification_number_css_class"] = (loadedStaff.project_staff_identification_number == "") ? "required_field user-input tooltip-staff-identification-number" : "user-input tooltip-staff-identification-number";
                        newRow["project_staff_city"] = loadedStaff.project_staff_city;
                        newRow["project_staff_city_css_class"] = (loadedStaff.project_staff_city == "") ? "required_field user-input tooltip-staff-city" : "user-input tooltip-staff-city";
                        newRow["project_staff_state"] = loadedStaff.project_staff_state;
                        newRow["project_staff_state_id"] = loadedStaff.project_staff_state;
                        newRow["id_genero"] = loadedStaff.id_genero;
                        newRow["id_etnia"] = loadedStaff.id_etnia;

                        if (loadedStaff.fecha_nacimiento != null)
                        {
                            string year = DateTime.Parse(loadedStaff.fecha_nacimiento.ToString()).Year.ToString();
                            string mes = DateTime.Parse(loadedStaff.fecha_nacimiento.ToString()).Month.ToString();
                            string dia = DateTime.Parse(loadedStaff.fecha_nacimiento.ToString()).Day.ToString();

                            newRow["fecha_nacimiento"] = year + " - " + mes + " - " + dia;
                        }

                        newRow["identification_type_id"] = loadedStaff.identification_type_id;
                        newRow["id_grupo_poblacional"] = loadedStaff.id_grupo_poblacional;
                        //newRow["fecha_nacimiento"] = loadedStaff.fecha_nacimiento;
                        newRow["project_staff_state_css_class"] = (loadedStaff.project_staff_state == "") ? "required_field user-input tooltip-staff-state" : "user-input tooltip-staff-state";
                        newRow["project_staff_address"] = loadedStaff.project_staff_address;
                        newRow["project_staff_address_css_class"] = (loadedStaff.project_staff_address == "") ? "required_field user-input tooltip-staff-address" : "user-input tooltip-staff-address";
                        newRow["project_staff_phone"] = loadedStaff.project_staff_phone;
                        newRow["project_staff_phone_css_class"] = (loadedStaff.project_staff_phone == "") ? "required_field user-input tooltip-staff-phone" : "user-input tooltip-staff-phone";
                        newRow["project_staff_movil"] = loadedStaff.project_staff_movil;
                        newRow["project_staff_movil_css_class"] = (loadedStaff.project_staff_movil == "") ? "required_field user-input tooltip-staff-movil" : "user-input tooltip-staff-movil";
                        newRow["project_staff_email"] = loadedStaff.project_staff_email;
                        newRow["project_staff_email_css_class"] = (loadedStaff.project_staff_email == "") ? "required_field user-input tooltip-staff-email" : "user-input tooltip-staff-email";
                        newRow["position_name"] = item["position_name"];
                        newRow["staff_option_detail_id"] = item["staff_option_detail_id"];
                        newRow["additional_option_block"] = additionalOptionBlockHtml;
                        newRow["fielset_css_class"] = "optional_staff_fields";

                        staffDS.Tables[0].Rows.Add(newRow);

                        repeater_index++;
                        #endregion
                    }
                }
                StaffRepeater.DataSource = staffDS;
                StaffRepeater.DataBind();
            }


            #region  Si el proyecto aun esta en su primera etapa (estado creado - 1) se define el estilo de la pestaña de acuerdo al resultado de la validación del diligenciamiento de los campos de los formularios. 
            if (project.state_id == 1)
            {
                project.sectionDatosProyecto.tab_state_id = 1;
                project.sectionDatosProductor.tab_state_id = 1;
                project.sectionDatosProductoresAdicionales.tab_state_id = 1;
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
            #endregion
            #region marca los tabs con los colores respectivos de acuerdo a su estado
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

            //SE PONE SIEMPRE OCULTA ESTRE TAB SOICITUD JMUTIS 01/02/2017
            tab_datos_formato_personal_css_class = "tab_hide";

            emtyform = project.validateNotInitForm("DatosPersonal");
            switch (project.sectionDatosPersonal.tab_state_id) /* Datos del personal */
            {
                case 10:
                    tab_datos_personal_css_class = "tab_incompleto_active";
                    break;
                case 11:
                    tab_datos_personal_css_class = "tab_unmarked_active";
                    break;
                case 9:
                    tab_datos_personal_css_class = "tab_completo_active";
                    if (myProject.FECHA_SUBSANACION.HasValue && myProject.SUBSANADO.HasValue == true && myProject.FECHA_SUBSANACION > DateTime.Now && myProject.SUBSANACION_ENVIADA == false)
                    {
                        fecha_subsanacion = "";
                        subsanado = false;
                    }
                    break;
                default:
                    if (!emtyform)
                    {
                        tab_datos_personal_css_class = (project.ValidateProjectSection("DatosPersonal")) ? "tab_completo_active" : "tab_incompleto_active";
                    }
                    else
                    {
                        tab_datos_personal_css_class = "tab_unmarked_active";
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
            #region marca los tabs como aprobados y revizados
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
            //comentarios_adicionales.Text = project.obs_adicional_personal;
            /* Agrega al formulario la información relacionada con la solicitud de aclaraciones */
            if (project.sectionDatosPersonal.solicitud_aclaraciones != "")
            {
                txtAclaracionesProductor.Text = project.sectionDatosPersonal.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                if (user_role > 1 && project_state_id >= 6)
                {
                    clarification_request_summary.Text = project.sectionDatosPersonal.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    producer_clarification_summary.Text = project.sectionDatosPersonal.aclaraciones_productor.Replace("\r\n", "<br>");
                }
            }
            /* Recupera al formulario la información de aclaraciones registrada por el productor */
   

            if (!IsPostBack)
            {
                lblAclaracionesSolicitadas.Text = project.sectionDatosPersonal.solicitud_aclaraciones;
                txtAclaracionesProductor.Text = project.sectionDatosPersonal.aclaraciones_productor;
                if (project_state_id != 5)
                {
                    btnGuardarAclaracionesProductor.Visible = false;
                    txtAclaracionesProductor.Enabled = false;
                }
                
            }

            llenarGrillas();
            if (!IsPostBack)
            {
                DominioCineProducto.NegocioCineProducto neg = new DominioCineProducto.NegocioCineProducto();
                int sectionActual = 4;
                project_status ps = neg.getProjectStatus(Convert.ToInt32(Session["project_id"]), sectionActual);
                if (ps != null)
                {
                    txtAclaraciones.Text = ps.project_status_solicitud_aclaraciones;
                    txtObservaciones.Text = ps.project_status_observacion_inicial;

                    radiobRevisarInfo.DataBind();

                    if (ps.project_status_tab_state_id == 11)
                    {
                        var item = radiobRevisarInfo.Items.FindByValue("1");
                        radiobRevisarInfo.SelectedItem = item;
                    }
                    if (ps.project_status_tab_state_id == 10)
                    {
                        var item = radiobRevisarInfo.Items.FindByValue("2");
                        radiobRevisarInfo.SelectedItem = item;
                    }
                    if (ps.project_status_tab_state_id == 9)
                    {
                        var item = radiobRevisarInfo.Items.FindByValue("3");
                        radiobRevisarInfo.SelectedItem = item;
                    }

                    if (ps.project_status_revision_mark == "")
                    {
                        var item = radiobRevisarInfoMark.Items.FindByValue("1");
                        radiobRevisarInfoMark.SelectedItem = item;
                    }
                    if (ps.project_status_revision_mark == "revisado")
                    {
                        var item = radiobRevisarInfoMark.Items.FindByValue("2");
                        radiobRevisarInfoMark.SelectedItem = item;
                    }
                    if (ps.project_status_revision_mark == "aprobado")
                    {
                        var item = radiobRevisarInfoMark.Items.FindByValue("3");
                        radiobRevisarInfoMark.SelectedItem = item;
                    }

                } 
                if (project.state_id == 1)
                {
                    pnAclaraciones.Visible = false;
                }
                else
                {
                    pnAclaraciones.Visible = true;
                    if (project.state_id > 5 || user_role == 6)
                    {
                        txtAclaraciones.Enabled = false;
                        if (project.state_id == 9 && project.state_id == 10)
                        {
                            radiobRevisarInfo.Enabled = false;
                            txtObservaciones.Enabled = false;
                            btnGuardar.Enabled = false;
                        }

                    }
                }
                
            }

            DominioCineProducto.NegocioCineProducto neg2 = new DominioCineProducto.NegocioCineProducto();
            int sectionActual2 = 4;
            project_status ps2 = neg2.getProjectStatus(Convert.ToInt32(Session["project_id"]), sectionActual2);
            if (ps2 != null)
            {
                if (ps2.project_status_revision_state_id == 10)
                {
                    ProductorPuedeEditar = true;
                }
            }
        }

        public void llenarGrillas()
        {
            grdPersonal.DataBind();            
        }

        protected string getSelectDepto(List<DominioCineProducto.Data.localization> Loc, string numRepeat, string valueSelected)
        {
            string inputSelect = "";

            inputSelect = "<select style=\"min-width:300px !important\"  class=\"user-input tooltip-child-position\" name=\"cmbDepto_" + numRepeat + "\" id=\"cmbDepto_" + numRepeat + "\">";
            inputSelect += "<option value=\"\">Seleccione..</option>";
            if (Loc != null)
            {
                foreach (DominioCineProducto.Data.localization unLoc in Loc)
                {
                    string optionSelected = "";
                    if (unLoc.localization_id.ToString() == valueSelected)
                    {
                        optionSelected = " selected ";
                    }
                    inputSelect += "<option value=\"" +
                                          unLoc.localization_id + "\" " + optionSelected + ">" +
                                          unLoc.localization_name + "</option>";
                }
            }
            inputSelect += "</select>";

            return inputSelect;
        }

        protected string getSelectEtnia(List<DominioCineProducto.Data.etnia> Loc, string numRepeat, string valueSelected)
        {
            string inputSelect = "";

            inputSelect = "<select style=\"min-width:300px !important\"  class=\"user-input tooltip-child-position\" name=\"cmbEtnia_" + numRepeat + "\" id=\"cmbEtnia_" + numRepeat + "\">";
            inputSelect += "<option value=\"0\">Seleccione..</option>";
            if (Loc != null)
            {
                foreach (DominioCineProducto.Data.etnia unLoc in Loc)
                {
                    string optionSelected = "";
                    if (unLoc.id_etnia.ToString() == valueSelected)
                    {
                        optionSelected = " selected ";
                    }
                    inputSelect += "<option value=\"" +
                                          unLoc.id_etnia + "\" " + optionSelected + ">" +
                                          unLoc.nombre + "</option>";
                }
            }
            inputSelect += "</select>";

            return inputSelect;
        }

        protected string getSelectGrupoPoblacional(List<DominioCineProducto.Data.grupo_poblacional> Loc, string numRepeat, string valueSelected)
        {
            string inputSelect = "";

            inputSelect = "<select style=\"min-width:300px !important\"  class=\"user-input tooltip-child-position\" name=\"cmbGrupoPoblacional_" + numRepeat + "\" id=\"cmbGrupoPoblacional_" + numRepeat + "\">";

            inputSelect += "<option value=\"\">Seleccione..</option>";

            if (Loc != null)
            {
                foreach (DominioCineProducto.Data.grupo_poblacional unLoc in Loc)
                {
                    string optionSelected = "";
                    if (unLoc.id_grupo_poblacional.ToString() == valueSelected)
                    {
                        optionSelected = " selected ";
                    }
                    inputSelect += "<option value=\"" +
                                          unLoc.id_grupo_poblacional + "\" " + optionSelected + ">" +
                                          unLoc.nombre + "</option>";
                }
            }
            inputSelect += "</select>";

            return inputSelect;
        }


        protected string getSelectIdentificationType(List<DominioCineProducto.Data.identification_type> Loc, string numRepeat, string valueSelected)
        {
            string inputSelect = "";
            if (valueSelected == "")
            {
                valueSelected = "1";
            }

            inputSelect = "<select style=\"min-width:300px !important\"  class=\"user-input tooltip-child-position\" name=\"cmbIdentificationType_" + numRepeat + "\" id=\"cmbIdentificationType_" + numRepeat + "\">";
            //inputSelect += "<option value=\"\">Seleccione..</option>";
            if (Loc != null)
            {
                foreach (DominioCineProducto.Data.identification_type unLoc in Loc)
                {
                    string optionSelected = "";
                    if (unLoc.identification_type_id.ToString() == valueSelected)
                    {
                        optionSelected = " selected ";
                    }
                    inputSelect += "<option value=\"" +
                                          unLoc.identification_type_id + "\" " + optionSelected + ">" +
                                          unLoc.identification_type_name + "</option>";
                }
            }
            inputSelect += "</select>";

            return inputSelect;
        }



        protected string getSelectGenero(List<DominioCineProducto.Data.genero> Loc, string numRepeat, string valueSelected)
        {
            string inputSelect = "";

            inputSelect = "<select style=\"min-width:300px !important\"  class=\"user-input tooltip-child-position\" name=\"cmbGenero_" + numRepeat + "\" id=\"cmbGenero_" + numRepeat + "\">";
            inputSelect += "<option value=\"0\">Seleccione..</option>";
            if (Loc != null)
            {
                foreach (DominioCineProducto.Data.genero unLoc in Loc)
                {
                    string optionSelected = "";
                    if (unLoc.id_genero.ToString() == valueSelected)
                    {
                        optionSelected = " selected ";
                    }
                    inputSelect += "<option value=\"" +
                                          unLoc.id_genero + "\" " + optionSelected + ">" +
                                          unLoc.nombre + "</option>";
                }
            }
            inputSelect += "</select>";

            return inputSelect;
        }

        protected string getSelectMunicipio(List<DominioCineProducto.Data.localization> Loc, string numRepeat, string valueSelected)
        {
            string inputSelect = "";

            inputSelect = "<select style=\"min-width:300px !important\"  class=\"user-input tooltip-child-position\" name=\"localizacion_id_" + numRepeat + "\" id=\"localizacion_id_" + numRepeat + "\">";
            inputSelect += "<option value=\"\">Seleccione..</option>";
            if (Loc != null)
            {
                foreach (DominioCineProducto.Data.localization unLoc in Loc)
                {
                    string optionSelected = "";
                    if (unLoc.localization_id.ToString() == valueSelected)
                    {
                        optionSelected = " selected ";
                    }
                    inputSelect += "<option value=\"" +
                                          unLoc.localization_id + "\" " + optionSelected + ">" +
                                          unLoc.localization_name + "</option>";
                }
            }
            inputSelect += "</select>";

            return inputSelect;
        }


        protected void btShowModal_Click(object sender, EventArgs e)
        {

            LinkButton b = (LinkButton)sender;
            string datosEnviados = b.CommandArgument;
            pcEditFormProducer.ContentUrl = "frmEditarPersonal.aspx?project_staff_id=" + datosEnviados;
            pcEditFormProducer.ShowOnPageLoad = true;

        }

        protected void btShowModalVer_Click(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            string datosEnviados = b.CommandArgument;
            pcVerCoProductor.ContentUrl = "frmVerPersonal.aspx?project_staff_id=" + datosEnviados;
            pcVerCoProductor.ShowOnPageLoad = true;

        }

        protected void cmbProductorNacional_SelectedIndexChanged(object sender, EventArgs e)
        {
            NegocioCineProducto neg = new NegocioCineProducto();
            project p = neg.getProject(Convert.ToInt32(Session["project_id"]));
            
            if (cmbProductorNacional.SelectedValue != "")
            {
                p.project_has_domestic_director = int.Parse(cmbProductorNacional.SelectedValue);
                neg.actualizarDomesticProject(p);
            }
            
            staff_option optionSt = neg.getStaffOptionByOptionVersion(p.project_type_id, p.production_type_id, p.project_genre_id, p.project_has_domestic_director, 2, p.project_percentage);

            if (optionSt != null)
            {
                List<staff_option_detail> listOptionDetail = neg.getStaffOptionDetailByOptionVersion(optionSt.staff_option_id, 2);

                neg.eliminarProjectStaffByProject(p.project_id);
                foreach (staff_option_detail unSpd in listOptionDetail)
                {
                    for (int i = 0; i < unSpd.staff_option_detail_quantity; i++)
                    {
                        project_staff objPs = new project_staff();
                        objPs.project_staff_project_id = p.project_id;
                        objPs.project_staff_position_id = unSpd.position_id;
                        objPs.identification_type_id = 1;
                        objPs.project_staff_firstname = "";
                        objPs.project_staff_lastname = "";
                        neg.adicionarProjectStaff(objPs);
                    }
                }

                grdPersonal.DataBind();
            }

            //DataSet staffOptionDS = staff.getStaffOptions(project.project_type_id, project.production_type_id, project.project_genre_id, project.project_has_domestic_director, (int)project.project_percentage, project.project_personal_type);
            //DataSet staffOptionDetailDS = staff.getStaffOptionDetail(project.project_staff_option_id, project.version);
            /*
             SELECT staff_option_id
                                     FROM staff_option 
                                     WHERE project_type_id=3 AND 
                                     production_type_id=2 AND 
                                     project_genre_id=3 AND                                     
                                     staff_option_personal_option =2 AND 
                                     staff_option_has_domestic_director=0 and staff_option_deleted=0 
            SELECT staff_option_detail_id, staff_option_detail_quantity, staff_option_detail_optional_quantity 
            FROM staff_option_detail WHERE staff_option_id = '51' AND staff_option_detail_deleted='0'  AND staff_option_detail.version = 2
             */
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //string observaciones = txtObservaciones.Text;
            //string aclaraciones = txtAclaraciones.Text;

            //// Validar si el texto contiene el carácter no permitido
            //if (observaciones.Contains("'"))
            //{
            //    // Mostrar un mensaje de error
            //    Response.Write(@"<script language='javascript'>alert('El carácter \' no está permitido en las observaciones.');</script>");
            //    return;
            //}else if (aclaraciones.Contains("'"))
            //{
            //        // Mostrar un mensaje de error
            //        Response.Write(@"<script language='javascript'>alert('El carácter \' no está permitido en las aclaraciones.');</script>");
            //        return;
            //}
            DominioCineProducto.NegocioCineProducto neg = new DominioCineProducto.NegocioCineProducto();
            project MyProject = neg.getProject(Convert.ToInt32(Session["project_id"]));
            int sectionActual = 4;

            project_status ps = neg.getProjectStatus(Convert.ToInt32(Session["project_id"]), sectionActual);
            if (ps == null)
            {
                ps = new project_status();
                ps.project_status_section_id = sectionActual;
                ps.project_status_project_id = Convert.ToInt32(Session["project_id"]);
                neg.crearProjectStatus(ps);
            }
            ps.project_status_observacion_inicial = txtObservaciones.Text;
            ps.project_status_observacion_inicial_date = DateTime.Now;
            ps.project_status_solicitud_aclaraciones = txtAclaraciones.Text;
            ps.project_status_solicitud_aclaraciones_date = DateTime.Now;
            ps.project_status_aclaraciones_productor_date = DateTime.Now;
            ps.project_status_modified = DateTime.Now;
            //ps.project_status_revision_state_id = 10;
            //ps.project_status_tab_state_id = 10;

            neg.actualizarProjectStatus(ps);
            Response.Write(@"<script language='javascript'>alert('Información guardada correctamente!');</script>");
        }

        protected void radiobRevisarInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DominioCineProducto.NegocioCineProducto neg = new DominioCineProducto.NegocioCineProducto();
            project MyProject = neg.getProject(Convert.ToInt32(Session["project_id"]));
            int sectionActual = 4;
            project_status ps = neg.getProjectStatus(Convert.ToInt32(Session["project_id"]), sectionActual);
            if (ps == null)
            {
                ps = new project_status();
                ps.project_status_section_id = sectionActual;
                ps.project_status_project_id = Convert.ToInt32(Session["project_id"]);
                neg.crearProjectStatus(ps);
            }


            if (radiobRevisarInfo.SelectedItem.Value.ToString() == "1")
            {
                ps.project_status_revision_state_id = 11;
                ps.project_status_tab_state_id = 11;
            }
            if (radiobRevisarInfo.SelectedItem.Value.ToString() == "2")
            {
                ps.project_status_revision_state_id = 10;
                ps.project_status_tab_state_id = 10;
            }
            if (radiobRevisarInfo.SelectedItem.Value.ToString() == "3")
            {
                ps.project_status_revision_state_id = 9;
                ps.project_status_tab_state_id = 9;
            }

            neg.actualizarEstadoProjectStatus(ps);

            Response.Write(@"<script language='javascript'>alert('Información guardada correctamente!');</script>");
        }


        
        protected void radiobRevisarInfoMark_SelectedIndexChanged(object sender, EventArgs e)
        {
            DominioCineProducto.NegocioCineProducto neg = new DominioCineProducto.NegocioCineProducto();
            project MyProject = neg.getProject(Convert.ToInt32(Session["project_id"]));
            int sectionActual = 4;
            project_status ps = neg.getProjectStatus(Convert.ToInt32(Session["project_id"]), sectionActual);
            if (ps == null)
            {
                ps = new project_status();
                ps.project_status_section_id = sectionActual;
                ps.project_status_project_id = Convert.ToInt32(Session["project_id"]);
                neg.crearProjectStatus(ps);
            }


            if (radiobRevisarInfoMark.SelectedItem.Value.ToString() == "1")
            {
                ps.project_status_revision_mark =  "";
            }
            if (radiobRevisarInfoMark.SelectedItem.Value.ToString() == "2")
            {
                ps.project_status_revision_mark = "revisado";
            }
            if (radiobRevisarInfoMark.SelectedItem.Value.ToString() == "3")
            {
                ps.project_status_revision_mark = "aprobado";
            }

            neg.actualizarEstadoProjectStatus(ps);

            Response.Write(@"<script language='javascript'>alert('Información guardada correctamente!');</script>");
        }

        protected void btnGuardarAclaracionesProductor_Click(object sender, EventArgs e)
        {
            DominioCineProducto.NegocioCineProducto neg = new DominioCineProducto.NegocioCineProducto();
            project MyProject = neg.getProject(Convert.ToInt32(Session["project_id"]));

            project_status ps = neg.getProjectStatus(Convert.ToInt32(Session["project_id"]), 4);
            if (ps != null)
            {
                ps.project_status_aclaraciones_productor = txtAclaracionesProductor.Text;
                ps.project_status_aclaraciones_productor_date = DateTime.Now;
                neg.actualizarProjectStatusAclaracionesProductor(ps);
            }

            Response.Write(@"<script language='javascript'>alert('Información guardada correctamente!');</script>");
        }

        protected bool EstanAprobadosAdjuntos(object project_staff_id) 
        {
            bool retorno = true;
            NegocioCineProducto neg = new NegocioCineProducto();
            List<project_attachment> objPa = neg.getProjectAttachmentByStaffId(int.Parse(project_staff_id.ToString()));

            foreach (project_attachment unPa in objPa) {
                if (unPa.project_attachment_approved == 0) {
                    retorno = false;
                }
            }
            return retorno;
        }

        
        protected bool EstanRechazadosAdjuntos(object project_staff_id)
        {
            bool retorno = true;
            NegocioCineProducto neg = new NegocioCineProducto();
            List<project_attachment> objPa = neg.getProjectAttachmentByStaffId(int.Parse(project_staff_id.ToString()));

            foreach (project_attachment unPa in objPa)
            {
                if (unPa.project_attachment_approved == 0)
                {
                    retorno = false;
                }
            }

            if (retorno)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}