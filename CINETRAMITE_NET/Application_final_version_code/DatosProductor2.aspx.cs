using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CineProducto.Bussines;
using System.Globalization;
using DominioCineProducto;
using DominioCineProducto.Data;

namespace CineProducto
{
    public partial class DatosProductor2 : System.Web.UI.Page
    {
        public int project_id; //Variable que hace disponible el id del proyecto actual en la plantilla
        public bool showAdvancedForm = false; //Variable que controla la presentación del formulario de administración
        public static bool showAdvancedFormInt = false;
        public int project_state_id = 0; //Indica el estado del proyecto, el cual se utiliza para identificar los elementos a presentar particulares de cada estado
        public int section_state_id = 0; //Indica el estado de la sección actual, el cual se utiliza para identificar los elementos a presentar particulares de cada estado.
        public int dataSave = 0;
        public string tab_datos_productor_css_class = "";
        public string tab_datos_proyecto_css_class = "";
        public string tab_productores_adicionales_css_class = "";
        public string tab_datos_formato_personal_css_class = "";
        public string tab_datos_personal_css_class = "";
        public string tab_datos_adjuntos_css_class = "";
        public string tab_datos_finalizacion_css_class = "";
        public int producer_id;
        public int user_role;

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
                    showAdvancedFormInt = true;
                }
            }

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

            if (!Page.IsPostBack)
            {  /* Obtiene las opciones para el select de tipo de productor */
                DataSet personTypeDS = db.GetSelectOptions("person_type", "person_type_id", "person_type_name");

                personTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < personTypeDS.Tables[0].Rows.Count; i++)
                {
                    personTypeDDL.Items.Add(new ListItem(personTypeDS.Tables[0].Rows[i]["person_type_name"].ToString(), personTypeDS.Tables[0].Rows[i]["person_type_id"].ToString()));
                }

                DataSet typeCompany = db.GetSelectOptions("producer_company_type", "producer_company_type_id", "producer_company_type_name");

                companyTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < typeCompany.Tables[0].Rows.Count; i++)
                {
                    companyTypeDDL.Items.Add(new ListItem(typeCompany.Tables[0].Rows[i]["producer_company_type_name"].ToString(), typeCompany.Tables[0].Rows[i]["producer_company_type_id"].ToString()));
                }
                /* Obtiene las opciones para el select de tipo de documento */
                DataSet identificationTypeDS = db.GetSelectOptions("identification_type", "identification_type_id", "identification_type_name");

                identificationTypeDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < identificationTypeDS.Tables[0].Rows.Count; i++)
                {
                    identificationTypeDDL.Items.Add(new ListItem(identificationTypeDS.Tables[0].Rows[i]["identification_type_name"].ToString(), identificationTypeDS.Tables[0].Rows[i]["identification_type_id"].ToString()));
                }


                /* Obtiene las opciones para el select de departamentos */
                DataSet departamentoDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='0'");

                departamentoDDL.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < departamentoDS.Tables[0].Rows.Count; i++)
                {
                    departamentoDDL.Items.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
                }

                /* Obtiene las opciones para el select de departamentos */
                DataSet departamentoDSContact = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='0'");

                departamentoDDL_contact.Items.Add(new ListItem("Seleccione", "0"));
                for (int i = 0; i < departamentoDSContact.Tables[0].Rows.Count; i++)
                {
                    departamentoDDL_contact.Items.Add(new ListItem(departamentoDSContact.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDSContact.Tables[0].Rows[i]["localization_id"].ToString()));
                }
            }

            /* Obtiene los textos de los tooltips y los pasa a las funciones de javascript correspondientes */
            tooltip_personTypeDDL.Text = db.GetTooltip("personTypeDDL");
            tooltip_producer_firstname.Text = db.GetTooltip("producer_firstname");

            //tooltip_producer_firstname2.Text = db.GetTooltip("producer_firstname2");
            tooltip_producer_lastname.Text = db.GetTooltip("producer_lastname");
            //tooltip_producer_lastname2.Text = db.GetTooltip("producer_lastname2");
            tooltip_producer_identification_number.Text = db.GetTooltip("producer_identification_number");
            tooltip_producer_name.Text = db.GetTooltip("producer_name");
            tooltip_producer_nit.Text = db.GetTooltip("producer_nit");
            tooltip_producer_firstname_juridica.Text = db.GetTooltip("producer_firstname_juridica");
            tooltip_producer_lastname_juridica.Text = db.GetTooltip("producer_lastname_juridica");
            tooltip_identificationTypeDDL.Text = db.GetTooltip("identificationTypeDDL");
            tooltip_identification_number_juridica.Text = db.GetTooltip("identification_number_juridica");
            tooltip_localization_out_of_colombia.Text = db.GetTooltip("localization_out_of_colombia");
            tooltip_departamentoDDL.Text = db.GetTooltip("departamentoDDL");
            tooltip_municipioDDL.Text = db.GetTooltip("municipioDDL");
            tooltip_producer_country.Text = db.GetTooltip("producer_country");
            tooltip_producer_city.Text = db.GetTooltip("producer_city");

            tooltip_localization_out_of_colombia_contact.Text = db.GetTooltip("localization_out_of_colombia_contact");
            tooltip_departamentoDDL_contact.Text = db.GetTooltip("departamentoDDL_contact");
            tooltip_municipioDDL_contact.Text = db.GetTooltip("municipioDDL_contact");
            tooltip_producer_country_contact.Text = db.GetTooltip("producer_country_contact");
            tooltip_producer_city_contact.Text = db.GetTooltip("producer_city_contact");

            tooltip_producer_address.Text = db.GetTooltip("producer_address");
            tooltip_producer_phone.Text = db.GetTooltip("producer_phone");
            tooltip_producer_movil.Text = db.GetTooltip("producer_movil");
            tooltip_producer_email.Text = db.GetTooltip("producer_email");
            tooltip_producer_website.Text = db.GetTooltip("producer_website");

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
            int requesterProducerContextualInfo = project.producer.FindIndex(
                delegate (Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
            if (requesterProducerContextualInfo != -1)
            {
                if (project.producer[requesterProducerContextualInfo].person_type_id == 2)
                    nombre_productor.Text = project.producer[requesterProducerContextualInfo].producer_name;
                else
                    nombre_productor.Text = project.producer[requesterProducerContextualInfo].producer_firstname.Trim() + " " + project.producer[requesterProducerContextualInfo].producer_lastname.Trim();

                if (nombre_productor.Text.Trim() == string.Empty)
                {
                    nombre_productor.Text = project.producer[requesterProducerContextualInfo].producer_firstname.Trim() + " " + project.producer[requesterProducerContextualInfo].producer_lastname.Trim();
                }

                if (nombre_productor.Text.Trim() == string.Empty)
                {
                    nombre_productor.Text = project.producer[requesterProducerContextualInfo].producer_email.Trim();
                }
            }
            opciones_adicionales.Text = "<a href =\"Lista.aspx\"><< Volver al listado de solicitudes</a>";
            /**************************************************/

            /* Se hace el procesamiento del formulario enviado */
            /* Valida si fue enviado el formulario */
            var controlMaster = (System.Web.UI.HtmlControls.HtmlInputHidden)Master.FindControl("_scrollboton");
            controlMaster.Value = false.ToString();
            /* Valida si fue enviado el formulario */
            if (Request.Form["submit_producer_data"] != null)
            {
                if (Request.Form["submit_producer_data"].ToString() != "combo")
                {
                    controlMaster.Value = true.ToString();
                }

                /* Buscamos el objeto del productor que hace la solicitud para guardar su informacion */
                int requesterProducer = project.producer.FindIndex(
                    delegate (Producer producerObj)
                    {
                        return producerObj.requester == 1;
                    });
                if (requesterProducer == -1)
                {
                    /* Se inserta un nuevo productor en la lista de productores */
                    Producer newProducer = new Producer();
                    newProducer.person_type_id = Convert.ToInt32(personTypeDDL.SelectedValue);
                    if (participation_percentage.Value != "")
                        newProducer.participation_percentage = decimal.Parse(participation_percentage.Value.Replace(",", ".").Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                    newProducer.id_genero = Convert.ToInt32(cmbGenero.SelectedValue);
                    newProducer.id_etnia = Convert.ToInt32(cmbEtnia.SelectedValue);


                    if (cmbGrupoPoblacional.SelectedValue != "")
                        newProducer.id_grupo_poblacional = Convert.ToInt32(cmbGrupoPoblacional.SelectedValue);

                    newProducer.abreviatura = abreviatura.Value;
                    newProducer.primer_nombre_sup = primer_nombre_sup.Value.ToUpper();
                    newProducer.segundo_nombre_sup = segundo_nombre_sup.Value.ToUpper();
                    newProducer.primer_apellido_sup = primer_apellido_sup.Value.ToUpper();
                    newProducer.segundo_apellido_sup = segundo_apellido_sup.Value.ToUpper();

                    //if (fecha_nacimiento.Value != "")
                    //    newProducer.fecha_nacimiento = Convert.ToDateTime(fecha_nacimiento.Value, new CultureInfo("es-CO"));



                    if (newProducer.person_type_id == 1)
                    {
                        newProducer.producer_firstname = producer_firstname.Value.ToUpper();
                        newProducer.producer_firstname2 = producer_firstname2.Value.ToUpper();
                        newProducer.producer_lastname = producer_lastname.Value.ToUpper();
                        newProducer.producer_lastname2 = producer_lastname2.Value.ToUpper();
                        newProducer.producer_identification_number = producer_identification_number.Value;
                        newProducer.fecha_nacimiento = txtFechaNacimiento.Date;
                    }
                    else if (newProducer.person_type_id == 2)
                    {
                        newProducer.producer_firstname = producer_firstname_juridica.Value.ToUpper();
                        newProducer.producer_firstname2 = producer_firstname_juridica2.Value.ToUpper();
                        newProducer.producer_lastname = producer_lastname_juridica.Value.ToUpper();
                        newProducer.producer_lastname2 = producer_lastname_juridica2.Value.ToUpper();
                        newProducer.producer_company_type_id = Convert.ToInt32(companyTypeDDL.SelectedValue);
                        newProducer.producer_identification_number = identification_number_juridica.Value;
                    }
                    else
                    {
                        newProducer.producer_firstname = "";
                        newProducer.producer_firstname2 = "";
                        newProducer.producer_lastname = "";
                        newProducer.producer_lastname2 = "";
                        newProducer.producer_identification_number = "";
                    }
                    newProducer.producer_name = producer_name.Value.ToUpper();
                    newProducer.producer_nit = producer_nit.Value;
                    if (producer_nit_dig_verif.Value != "")
                        newProducer.producer_nit_dig_verif = Convert.ToInt32(producer_nit_dig_verif.Value);
                    newProducer.producer_type_id = 1; //Es productor colombiano
                    newProducer.identification_type_id = Convert.ToInt32(identificationTypeDDL.SelectedValue);

                    newProducer.producer_country = producer_country.Value;
                    newProducer.productor_pais_contacto = producer_country_contact.Value;
                    newProducer.producer_city = producer_city.Value;
                    newProducer.productor_ciudad_contacto = producer_city_contact.Value;


                    newProducer.producer_address = producer_address.Value;
                    newProducer.producer_phone = producer_phone.Value;
                    newProducer.producer_movil = producer_movil.Value;
                    newProducer.producer_email = producer_email.Value;
                    newProducer.producer_website = producer_website.Value;
                    newProducer.producer_user_id = (int)Session["user_id"];
                    newProducer.producer_localization_id = Request.Form["selectedMunicipio"];
                    newProducer.productor_localizacion_contacto_id = Request.Form["selectedMunicipio_contact"];
                    newProducer.requester = 1;
                    project.producer.Add(newProducer);
                }
                else
                {
                    /* Se actualiza el productor actual */
                    project.producer[requesterProducer].person_type_id = Convert.ToInt32(personTypeDDL.SelectedValue);

                    if (participation_percentage.Value != "")
                        project.producer[requesterProducer].participation_percentage = decimal.Parse(participation_percentage.Value.Replace(",", ".").Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                    project.producer[requesterProducer].id_genero = Convert.ToInt32(cmbGenero.SelectedValue);
                    project.producer[requesterProducer].id_etnia = Convert.ToInt32(cmbEtnia.SelectedValue);

                    if (cmbGrupoPoblacional.SelectedValue != "")
                        project.producer[requesterProducer].id_grupo_poblacional = Convert.ToInt32(cmbGrupoPoblacional.SelectedValue);

                    project.producer[requesterProducer].abreviatura = abreviatura.Value;
                    project.producer[requesterProducer].primer_nombre_sup = primer_nombre_sup.Value.ToUpper();
                    project.producer[requesterProducer].segundo_nombre_sup = segundo_nombre_sup.Value.ToUpper();
                    project.producer[requesterProducer].primer_apellido_sup = primer_apellido_sup.Value.ToUpper();
                    project.producer[requesterProducer].segundo_apellido_sup = segundo_apellido_sup.Value.ToUpper();

                    //if (fecha_nacimiento.Value != "")
                    //    project.producer[requesterProducer].fecha_nacimiento = Convert.ToDateTime(fecha_nacimiento.Value, new CultureInfo("es-CO"));


                    if (project.producer[requesterProducer].person_type_id == 1)
                    {
                        project.producer[requesterProducer].producer_firstname = producer_firstname.Value.ToUpper();
                        project.producer[requesterProducer].producer_firstname2 = producer_firstname2.Value.ToUpper();
                        project.producer[requesterProducer].producer_lastname = producer_lastname.Value.ToUpper();
                        project.producer[requesterProducer].producer_lastname2 = producer_lastname2.Value.ToUpper();
                        project.producer[requesterProducer].producer_identification_number = producer_identification_number.Value;
                        project.producer[requesterProducer].fecha_nacimiento = txtFechaNacimiento.Date;
                    }
                    else if (project.producer[requesterProducer].person_type_id == 2)
                    {
                        project.producer[requesterProducer].producer_firstname = producer_firstname_juridica.Value.ToUpper();
                        project.producer[requesterProducer].producer_firstname2 = producer_firstname_juridica2.Value.ToUpper();
                        project.producer[requesterProducer].producer_lastname = producer_lastname_juridica.Value.ToUpper();
                        project.producer[requesterProducer].producer_lastname2 = producer_lastname_juridica2.Value.ToUpper();
                        project.producer[requesterProducer].producer_company_type_id = Convert.ToInt32(companyTypeDDL.SelectedValue);
                        project.producer[requesterProducer].producer_identification_number = identification_number_juridica.Value;
                    }
                    else
                    {
                        project.producer[requesterProducer].producer_firstname = "";
                        project.producer[requesterProducer].producer_firstname2 = "";
                        project.producer[requesterProducer].producer_lastname = "";
                        project.producer[requesterProducer].producer_lastname2 = "";
                        project.producer[requesterProducer].producer_identification_number = "";
                    }

                    project.producer[requesterProducer].producer_name = producer_name.Value;
                    project.producer[requesterProducer].producer_nit = producer_nit.Value;
                    project.producer[requesterProducer].producer_nit_dig_verif = Convert.ToInt32(producer_nit_dig_verif.Value);
                    project.producer[requesterProducer].producer_type_id = 1; //Es un productor colombiano
                    project.producer[requesterProducer].identification_type_id = Convert.ToInt32(identificationTypeDDL.SelectedValue);
                    project.producer[requesterProducer].producer_country = producer_country.Value;
                    project.producer[requesterProducer].producer_city = producer_city.Value;
                    project.producer[requesterProducer].productor_ciudad_contacto = producer_city_contact.Value;
                    project.producer[requesterProducer].productor_pais_contacto = producer_country_contact.Value;
                    project.producer[requesterProducer].producer_address = producer_address.Value;
                    project.producer[requesterProducer].producer_phone = producer_phone.Value;
                    project.producer[requesterProducer].producer_movil = producer_movil.Value;
                    project.producer[requesterProducer].producer_email = producer_email.Value;
                    project.producer[requesterProducer].producer_website = producer_website.Value;
                    string valueMunicipio = Request.Form["selectedMunicipio"];
                    if (valueMunicipio == "0")
                    {
                        project.producer[requesterProducer].producer_localization_id = municipioDDL.SelectedValue;
                    }
                    else
                    {
                        project.producer[requesterProducer].producer_localization_id = Request.Form["selectedMunicipio"];
                    }
                    string valueMunicipioContacto = Request.Form["selectedMunicipio_contact"];
                    if (valueMunicipioContacto == "0")
                    {
                        project.producer[requesterProducer].productor_localizacion_contacto_id = municipioDDL_contact.SelectedValue;
                    }
                    else
                    {
                        project.producer[requesterProducer].productor_localizacion_contacto_id = Request.Form["selectedMunicipio_contact"];
                    }
                    project.producer[requesterProducer].requester = 1;

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


                    /* Se hace el manejo de datos de acuerdo a la ubicación del productor (dentro o fuera de colombia) */
                    if (localization_out_of_colombia_contact.Checked)
                    {
                        project.producer[requesterProducer].productor_localizacion_contacto_id = "0";
                    }
                    else
                    {
                        project.producer[requesterProducer].producer_country = "";
                        project.producer[requesterProducer].producer_city = "";
                    }
                }

                /* Hace la persistencia a la bd de la información de aclaraciones registrada por el productor */
                if (producer_clarifications_field.Value != "")
                {
                    project.sectionDatosProductor.aclaraciones_productor = producer_clarifications_field.Value;
                    project.sectionDatosProductor.aclaraciones_productor_date = DateTime.Now;
                }
                project.obs_adicional_productor = comentarios_adicionales.Value;
                /* Se pasan al objeto del proyecto los valores definidos en el formulario de administración para ser almacenados */
                if (this.showAdvancedForm)
                {
                    /* Interpretación del valor enviado del formulario para la gestión realizada */
                    project.sectionDatosProductor.revision_state_id = 0;

                    if (gestion_realizada_sin_revisar.Checked)
                    {
                        project.sectionDatosProductor.revision_state_id = 11;
                        project.sectionDatosProductor.tab_state_id = 11;
                    }
                    if (gestion_realizada_solicitar_aclaraciones.Checked)
                    {
                        project.sectionDatosProductor.revision_state_id = 10;
                        project.sectionDatosProductor.tab_state_id = 10;
                    }
                    if (gestion_realizada_informacion_correcta.Checked)
                    {
                        project.sectionDatosProductor.revision_state_id = 9;
                        project.sectionDatosProductor.tab_state_id = 9;
                    }

                    /* Valida si se modificó el texto de la solicitud de aclaraciones para grabarla y actualizar la fecha */
                    if (project.sectionDatosProductor.solicitud_aclaraciones != solicitud_aclaraciones.Value)
                    {
                        project.sectionDatosProductor.solicitud_aclaraciones = solicitud_aclaraciones.Value;
                        project.sectionDatosProductor.solicitud_aclaraciones_date = DateTime.Now;
                    }
                    /* Valida si se modificó el texto de la solicitud de la primera observación para grabarla y actualizar la fecha */
                    if (project.sectionDatosProductor.observacion_inicial != informacion_correcta.Value)
                    {
                        project.sectionDatosProductor.observacion_inicial = informacion_correcta.Value;
                        project.sectionDatosProductor.observacion_inicial_date = DateTime.Now;
                    }
                    project.sectionDatosProductor.modified = DateTime.Now;

                    /* Se almacena la información registrada sobre el estado de revisión de la pestaña */
                    if (estado_revision_sin_revisar.Checked)
                    {
                        project.sectionDatosProductor.revision_mark = "";
                    }
                    if (estado_revision_revisado.Checked)
                    {
                        project.sectionDatosProductor.revision_mark = "revisado";
                    }
                    if (estado_revision_aprobado.Checked)
                    {
                        project.sectionDatosProductor.revision_mark = "aprobado";
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

                    project.LoadProject(project.project_id);
                }

                project.Save();
                project.LoadProject(project.project_id);
                this.dataSave = 1;
            }

            /* Si el proyecto ya tiene identificador y aun no está en sesión, es registrado en la variable de sesión */
            if (Session["project_id"] == null && project.project_id > 0)
            {
                Session["project_id"] = project.project_id;
            }

            /* Carga en el formulario la información del productor principal */
            if (Session["project_id"] != null)
            {
                /* Guarda en la variable de la clase el estado de la variable */
                this.project_state_id = project.state_id;
                this.section_state_id = project.sectionDatosProductor.tab_state_id;

                comentarios_adicionales.Value = project.obs_adicional_productor;

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
                        tab_datos_productor_css_class = "tab_incompleto_active";
                        break;
                    case 11:
                        tab_datos_productor_css_class = "tab_unmarked_active";
                        break;
                    case 9:
                        tab_datos_productor_css_class = "tab_completo_active";
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
                            tab_datos_productor_css_class = (project.ValidateProjectSection("DatosProductor")) ? "tab_completo_active" : "tab_incompleto_active";
                        }
                        else
                        {
                            tab_datos_productor_css_class = "tab_unmarked_active";
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


                foreach (Producer producer in project.producer)
                {
                    if (producer.requester == 1)
                    {
                        if (producer.person_type_id == 1 && producer.producer_type_id == 1)
                        {
                            pnlDatosNacimiento.Visible = true;
                            departamentoDDL.Attributes["Class"] = (departamentoDDL.SelectedValue == "0") ? "required_field" : "";
                            municipioDDL.Attributes["Class"] = (municipioDDL.SelectedValue == "0") ? "required_field" : "";
                        }
                        else
                        {
                            pnlDatosNacimiento.Visible = false;
                        }
                        this.producer_id = producer.producer_id;
                        personTypeDDL.SelectedValue = producer.person_type_id.ToString();
                        //cmbGenero.DataBind();
                        //cmbEtnia.DataBind();

                        participation_percentage.Value = producer.participation_percentage.ToString().Replace(".", ",");
                        cmbEtnia.SelectedValue = producer.id_etnia.ToString();

                        cmbGrupoPoblacional.SelectedValue = producer.id_grupo_poblacional.ToString();
                        if (producer.fecha_nacimiento != null)
                        {
                            DateTime fecha_nac = DateTime.Parse(producer.fecha_nacimiento.ToString());
                            //fecha_nacimiento.Value = fecha_nac.Year + "-" + fecha_nac.Month + "-" + fecha_nac.Day;                            
                            txtFechaNacimiento.Date = fecha_nac;
                        }
                        txtFechaNacimiento.MinDate = DateTime.Parse("1870-01-01");
                        txtFechaNacimiento.MaxDate = DateTime.Now;

                        abreviatura.Value = producer.abreviatura;
                        primer_nombre_sup.Value = producer.primer_nombre_sup;
                        segundo_nombre_sup.Value = producer.segundo_nombre_sup;
                        primer_apellido_sup.Value = producer.primer_apellido_sup;
                        segundo_apellido_sup.Value = producer.segundo_apellido_sup;


                        cmbGenero.SelectedValue = producer.id_genero.ToString();
                        producer_firstname.Value = producer.producer_firstname;
                        producer_firstname2.Value = producer.producer_firstname2;
                        producer_lastname.Value = producer.producer_lastname;
                        producer_lastname2.Value = producer.producer_lastname2;

                        producer_identification_number.Value = producer.producer_identification_number;
                        producer_name.Value = producer.producer_name;
                        producer_nit.Value = producer.producer_nit;
                        producer_nit_dig_verif.Value = producer.producer_nit_dig_verif.ToString();
                        producer_firstname_juridica.Value = producer.producer_firstname;
                        producer_firstname_juridica2.Value = producer.producer_firstname2;                        
                        producer_lastname_juridica.Value = producer.producer_lastname;
                        producer_lastname_juridica2.Value = producer.producer_lastname2;
                        identificationTypeDDL.SelectedValue = producer.identification_type_id.ToString();
                        identification_number_juridica.Value = producer.producer_identification_number;
                        producer_country.Value = producer.producer_country;
                        producer_city.Value = producer.producer_city;
                        producer_address.Value = producer.producer_address;
                        producer_phone.Value = producer.producer_phone;
                        producer_movil.Value = producer.producer_movil;
                        producer_email.Value = producer.producer_email;
                        producer_website.Value = producer.producer_website;
                        // Se selecciona el tipo de empresa
                        companyTypeDDL.SelectedValue = producer.producer_company_type_id.ToString();
                        /* Marca el departamento seleccionado en el select del departamento */
                        if (producer.producer_localization_father_id == "")
                        {
                            departamentoDDL.SelectedValue = "0";
                        }
                        else
                        {
                            departamentoDDL.SelectedValue = producer.producer_localization_father_id;
                        }

                        if (producer.productor_localizacion_contacto_id_padre == "")
                        {
                            departamentoDDL_contact.SelectedValue = "0";
                        }
                        else
                        {
                            departamentoDDL_contact.SelectedValue = producer.productor_localizacion_contacto_id_padre;
                        }

                        /* Carga las opciones de municipios segun el departamento seleccionado  */
                        DataSet municipioDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + producer.producer_localization_father_id + "'");
                        municipioDDL.Items.Clear();
                        municipioDDL.Items.Add(new ListItem("Seleccione", "0"));
                        for (int i = 0; i < municipioDS.Tables[0].Rows.Count; i++)
                        {
                            municipioDDL.Items.Add(new ListItem(municipioDS.Tables[0].Rows[i]["localization_name"].ToString(), municipioDS.Tables[0].Rows[i]["localization_id"].ToString()));
                        }

                        /* Selecciona la opcion correcta en el dropdown */
                        if (producer.producer_localization_id == "")
                        {
                            municipioDDL.SelectedValue = "0";
                        }
                        else
                        {
                            municipioDDL.SelectedValue = producer.producer_localization_id;
                        }

                        /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                        if (producer.producer_country == "")
                        {
                            localization_out_of_colombia.Checked = false;
                        }
                        else
                        {
                            localization_out_of_colombia.Checked = true;
                        }

                        /* Carga las opciones de municipios contacto segun el departamento seleccionado  */
                        DataSet municipioDSContact = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + producer.productor_localizacion_contacto_id_padre + "'");
                        municipioDDL_contact.Items.Clear();
                        municipioDDL_contact.Items.Add(new ListItem("Seleccione", "0"));
                        for (int i = 0; i < municipioDSContact.Tables[0].Rows.Count; i++)
                        {
                            municipioDDL_contact.Items.Add(new ListItem(municipioDSContact.Tables[0].Rows[i]["localization_name"].ToString(), municipioDSContact.Tables[0].Rows[i]["localization_id"].ToString()));
                        }

                        /* Selecciona la opcion correcta en el dropdown */
                        if (producer.productor_localizacion_contacto_id == "")
                        {
                            municipioDDL_contact.SelectedValue = "0";
                        }
                        else
                        {
                            municipioDDL_contact.SelectedValue = producer.productor_localizacion_contacto_id;
                        }

                        /* Si el campo de pais esta vacio entonces se marca el checkbox de ubicación indicando que está en colombia */
                        if (producer.productor_pais_contacto == "")
                        {
                            localization_out_of_colombia_contact.Checked = false;
                        }
                        else
                        {
                            localization_out_of_colombia_contact.Checked = true;
                        }

                        if (this.showAdvancedForm)
                        {
                            /* Carga en el formulario el valor que ha sido recuperado de la base de datos 
                             * para el checkbox del estado del formulario
                             */
                            gestion_realizada_sin_revisar.Checked = false;
                            gestion_realizada_solicitar_aclaraciones.Checked = false;
                            gestion_realizada_informacion_correcta.Checked = false;

                            if (project.sectionDatosProductor.revision_state_id == 11)
                            {
                                gestion_realizada_sin_revisar.Checked = true;
                            }
                            if (project.sectionDatosProductor.revision_state_id == 10)
                            {
                                gestion_realizada_solicitar_aclaraciones.Checked = true;
                            }
                            if (project.sectionDatosProductor.revision_state_id == 9)
                            {
                                gestion_realizada_informacion_correcta.Checked = true;
                            }

                            /* Crea las etiquetas que incluyen la imagen que indica el estado de la marca
                             * de revisión en cada pestaña y hace la persistencia en el formulario de acuerdo
                             * a los valores guardados en la base de datos */
                            estado_revision_sin_revisar.Checked = false;
                            estado_revision_revisado.Checked = false;
                            estado_revision_aprobado.Checked = false;

                            if (project.sectionDatosProductor.revision_mark == "")
                            {
                                estado_revision_sin_revisar.Checked = true;
                            }
                            if (project.sectionDatosProductor.revision_mark == "revisado")
                            {
                                estado_revision_revisado.Checked = true;
                            }
                            if (project.sectionDatosProductor.revision_mark == "aprobado")
                            {
                                estado_revision_aprobado.Checked = true;
                            }

                            /*
                             * Carga en el formulario los textos registrados por los
                             * administradores del trámite
                             */
                            solicitud_aclaraciones.Value = project.sectionDatosProductor.solicitud_aclaraciones;
                            informacion_correcta.Value = project.sectionDatosProductor.observacion_inicial;

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

                        /* Agrega al formulario la información relacionada con la solicitud de aclaraciones */
                        if (project.sectionDatosProductor.solicitud_aclaraciones != "")
                        {
                            clarification_request.Text = project.sectionDatosProductor.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                            if (user_role > 1 && project_state_id >= 6)
                            {
                                clarification_request_summary.Text = project.sectionDatosProductor.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                                producer_clarification_summary.Text = project.sectionDatosProductor.aclaraciones_productor.Replace("\r\n", "<br>");
                            }
                        }
                        /* Recupera al formulario la información de aclaraciones registrada por el productor */
                        producer_clarifications_field.Value = project.sectionDatosProductor.aclaraciones_productor;

                        /* Aplica formatos especiales - campos requeridos vacios - a los campos presentados en el formulario */
                        personTypeDDL.Attributes["Class"] = (producer.person_type_id == 0) ? "required_field" : "";
                        //cmbEtnia.Attributes["Class"] = (producer.id_etnia == 0) ? "required_field" : "";
                        //cmbGenero.Attributes["Class"] = (producer.id_genero == 0) ? "required_field" : "";
                        producer_firstname.Attributes["Class"] = (producer.producer_firstname == "") ? "required_field" : "";
                        //producer_firstname2.Attributes["Class"] = (producer.producer_firstname2 == "") ? "required_field" : "";
                        producer_lastname.Attributes["Class"] = (producer.producer_lastname == "") ? "required_field" : "";
                        //producer_lastname2.Attributes["Class"] = (producer.producer_lastname2 == "") ? "required_field" : "";
                        producer_firstname_juridica.Attributes["Class"] = (producer.producer_firstname == "") ? "required_field" : "";
                        producer_firstname_juridica.Attributes["Class"] = (producer.producer_lastname == "") ? "required_field" : "";
                        producer_identification_number.Attributes["Class"] = (producer.producer_identification_number == "") ? "required_field" : "";
                        identification_number_juridica.Attributes["Class"] = (producer.producer_identification_number == "") ? "required_field" : "";
                        producer_name.Attributes["Class"] = (producer.producer_name == "") ? "required_field" : "";
                        producer_nit.Attributes["Class"] = (producer.producer_nit == "") ? "required_field" : "";
                        producer_nit_dig_verif.Attributes["Class"] = (producer.producer_nit_dig_verif == 0) ? "required_field" : "";
                        identificationTypeDDL.Attributes["Class"] = (producer.identification_type_id == 0) ? "required_field" : "";
                        
                        departamentoDDL_contact.Attributes["Class"] = (departamentoDDL_contact.SelectedValue == "0") ? "required_field" : "";
                        
                        municipioDDL_contact.Attributes["Class"] = (municipioDDL_contact.SelectedValue == "0") ? "required_field" : "";
                        producer_country.Attributes["Class"] = (producer.producer_country == "") ? "required_field" : "";
                        producer_city.Attributes["Class"] = (producer.producer_city == "") ? "required_field" : "";
                        producer_address.Attributes["Class"] = (producer.producer_address == "") ? "required_field" : "";
                        producer_phone.Attributes["Class"] = (producer.producer_phone == "") ? "required_field" : "";
                        //producer_movil.Attributes["Class"] = (producer.producer_movil == "") ? "required_field" : "";
                        producer_email.Attributes["Class"] = (producer.producer_email == "") ? "required_field" : "";
                    }
                }

                /* #### INCLUIMOS CODIGO DE ADJUNTOS PARA QUE SEA VISIBLE A TODOS SOLO QUE HA */
                /* LOS ADMINISTRADORES SE LES HACE VISIBLE EL CHECKBOX DE APROBAR O RECHAZAR EL ADJUNTO*/
                /* Agrega las opciones al repetidor */
                Attachment adjuntoObj = new Attachment();
                // cargamos los tipos de adjuntos
                List<Attachment> adjuntos = adjuntoObj.GetAttachmentListByConsult(project, 1, this.producer_id);
                // Si ya existe infirmación del productor se mustran los adjuntos
                emtyform = project.validateNotInitForm("DatosProductor");
                if ((project.sectionDatosProductor.tab_state_id == 9 || project.sectionDatosProductor.tab_state_id == 10 || project.sectionDatosProductor.tab_state_id == 11) || (!emtyform))
                {
                    // iteración de los tipos de adjunto
                    foreach (Attachment item in adjuntos)
                    {
                        DataRow newRow = attachmentDS.Tables[0].NewRow();
                        // se define el objeto de tipo adjunto projecto
                        ProjectAttachment projectAttachmentCurrent = new ProjectAttachment();
                        // se verifica si exite un adjunto ya cargado
                        projectAttachmentCurrent.loadAttachmentByFhaterAndProducerId(project.project_id, item.attachment_id, producer_id);
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
                                                                                this.project_state_id,
                                                                                this.user_role, projectAttachmentCurrent.nombre_original);
                        attachmentDS.Tables[0].Rows.Add(newRow);
                    }


                    /* Agrega las opciones al repetidor */
                    AttachmentRepeater.DataSource = attachmentDS;
                    AttachmentRepeater.DataBind();

                    //Agrega las opciones al repetidor uploadify
                    AttachmentRepeater2.DataSource = attachmentDS;
                    AttachmentRepeater2.DataBind();
                    /* ### FIN CODIGO ADJUNTOS ###*/
                }

            }

        }



    }
}