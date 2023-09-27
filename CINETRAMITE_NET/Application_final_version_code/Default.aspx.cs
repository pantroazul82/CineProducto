using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections;
using CineProducto.Bussines;
using System.Data;
using System.IO;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Configuration;

using System.Web.Script.Serialization;

namespace CineProducto
{
    public partial class _Default : System.Web.UI.Page
    {
        public bool loggeduser = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Valida si se debe terminar la sesión del usuario actual y se ejecuta el procedimiento */
            if (Convert.ToInt32(Request.QueryString["logout"]) == 1)
            {
                Session["user_id"] = null;
            }
            /* Verifica si el usuario ya está autenticado, y si es así esconde el formulario de autenticación 
             y registro rápido y presenta las opciones de nueva solicitud e ir al listado. */
            if (Session["user_id"] == null)
            {
                /* Se verifica si se enviaron los parametros de autenticación */
                if (Request.Form["user"] != null && Request.Form["pass"] != null)
                {
                    User currentUser = new User();
                    currentUser.LoadUser(Request.Form["user"], Request.Form["pass"]);
                    if (currentUser.logged)
                    {
                        Session["user_id"] = currentUser.user_id;
                        Session["user_username"] = currentUser.username;
                        Session["user_firstname"] = currentUser.firstname;

                        //codigo temporal
                        if (Session["user_username"].ToString().Trim() == "cgarzon@mincultura.gov.co" ||
                            Session["user_username"].ToString().Trim() == "revisor" ||
                            Session["user_username"].ToString().Trim() == "editor" ||
                            Session["user_username"].ToString().Trim() == "director"
                            
                            )
                        {
                            Session["superadmin"] = "ok";
                        }
                        else {
                            Session["superadmin"] = null;
                        }

                        if (currentUser.firstname.Trim() == currentUser.lastname.Trim())
                        {
                            Session["user_completename"] = currentUser.firstname;
                        }
                        else
                        {
                            Session["user_completename"] = currentUser.firstname + " " + currentUser.lastname;
                        }

                        if (Session["user_completename"].ToString().Trim() == string.Empty)
                        {
                            Session["user_completename"] = currentUser.username;
                        }
                        Session["user_mail"] = currentUser.mail;
                        Session["user_role_id"] = currentUser.role_id;
                        //
                        Session["ES_EDITOR"]=false;
                        Session["ES_REVISOR"]=false;;
                        Session["ES_PRODUCTOR"]=false;
                        Session["ES_DIRECTOR"] = false;
                        if (currentUser.role_id <= 1)
                        {
                            Session["ES_PRODUCTOR"] = true;
                        }
                        else if (currentUser.role_id == 2)
                        {

                            Session["ES_REVISOR"] = true; 
                        }
                        else if (currentUser.role_id == 3)
                        {
                            Session["ES_EDITOR"] = true;
                        }
                        else if (currentUser.role_id == 4)
                        {

                            Session["ES_DIRECTOR"] = true;
                        }
                        else if (currentUser.role_id == 5)
                        {
                            Session["ES_EDITOR"] = true;
                            Session["ES_REVISOR"] = true;
                            Session["ES_DIRECTOR"] = true;
                            Session["superadmin"] = "ok";
                        }
                        this.loggeduser = true;
                    }
                    else 
                    {
                        auth_error_message.Text = "Error en la autenticaci&oacute;n, por favor intente nuevamente";
                    }
                }
                else if (Request.Form["nombre_form"] != null && Request.Form["apellido_form"] != null && Request.Form["email_form"] != null)
                {
                    try
                    {
                        if (Request.Form["txtcaptcha"] == null)
                        {
                            confirmacion_creacion_cuenta.Text = "Ingrese el texto de la imagen.";
                            return;
                        }
                        if (Request.Form["email_form"].IndexOf('@')<=0)
                        {
                            confirmacion_creacion_cuenta.Text = "Ingrese Una dirección de correo electrónico válida.";
                            return;
                        }
                        if (Request.Form["txtcaptcha"].Trim().ToUpper() != this.Session["CaptchaImageText"].ToString().ToUpper())
                        {
                            confirmacion_creacion_cuenta.Text = "El texto de la imagen no corresponde.";
                            return;
                        }
                        
                        User newUser = new User();
                        if (newUser.createUser(QuitarCaracatersEspeciales(Request.Form["nombre_form"]),
                            QuitarCaracatersEspeciales(Request.Form["apellido_form"]),
                            QuitarCaracatersEspeciales(Request.Form["email_form"]).Trim().ToLower(),Server))
                        {
                            confirmacion_creacion_cuenta.Text = "La cuenta fue creada exitosamente y la contraseña fue enviada a su correo electrónico";
                        }
                        else
                        {
                            error_creacion_cuenta.Text = "No fue posible crear la cuenta, es posible que ya este registrado el correo electrónico. Si usted ya tiene una cuenta y no recuerda la contraseña puede usar la utilidad de recordar contraseña.";
                        }
                    }catch(Exception)
                    {
                        error_creacion_cuenta.Text = "No fue posible crear la cuenta, es posible que ya este registrado el correo electrónico. Si usted ya tiene una cuenta y no recuerda la contraseña puede usar la utilidad de recordar contraseña.";
                    }
                }
            }
            else
            {
                this.loggeduser = true;
            }
        }

        public string QuitarCaracatersEspeciales(string cadena) 
        {
            string respuesta = cadena.Replace("'","");

            return respuesta;
        }
        
        [WebMethod()]
        public static string getResolutionPath(int project_id)
        {
            Resolution resolution = new Resolution();
            resolution.LoadByProject(project_id);
            return resolution.path;
        }
        /*Validacion para crear adjuntos según empresa*/
        /*Función para traer adjuntos según persona natural o juridica*/

       

        [WebMethod()]
        public static ArrayList obtenerAdjuntos(string type_company, string project_attachment_project_id)
        {

            int project_project_id = Convert.ToInt32(project_attachment_project_id);
            
            if (type_company == "1")
            {

                type_company = "Persona Natural";
            }
            DB db = new DB();

            ArrayList elementos = new ArrayList();

            /* Obtiene los tipos de adjuntos */
            DataSet attachmentDSCompany = new DataSet();
            attachmentDSCompany = db.LoadProjectAttachmentCompany(type_company);



            for (int i = 0; i < attachmentDSCompany.Tables[0].Rows.Count; i++)
            {
                
                attachmentDSCompany = db.LoadProjectAttachment_ID(type_company, project_project_id);
                elementos.Add(new ListItem(attachmentDSCompany.Tables[0].Rows[i]["attachment_name"].ToString(), attachmentDSCompany.Tables[0].Rows[i]["attachment_name"].ToString()));
            }
       
            return elementos;
            

        }
        /*Fin de la función*/

      

        [WebMethod()]
        public static ArrayList obtenerMunicipios(string departamento)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            ArrayList elementos = new ArrayList();

            /* Obtiene las opciones para el select de departamentos */
            DataSet departamentoDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + departamento  + "' order by localization_name");
 
            for (int i = 0; i < departamentoDS.Tables[0].Rows.Count; i++)
            {
                elementos.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
            }

            return elementos;
        }


        [WebMethod()]
        public static string proccessRequestForm(int project_id, string filename) {
            String rootPath = HttpContext.Current.Server.MapPath("~");

            /* Obtenemos la extensión del archivo cargado */
            string[] uploadedfilenamesplit = filename.Split('.');
            string extension = uploadedfilenamesplit[(uploadedfilenamesplit.Count() - 1)];
            String uploadFolderName = "/uploads/";
            String savepath = @rootPath + "uploads\\" + project_id + "\\requestForm\\";
            String sourcePath = @rootPath + "uploads\\" + project_id + "\\" + filename;
            string nameDestino = "Formulario-"+project_id+"-"+DateTime.Now.Ticks.ToString().Substring(8)+".pdf";
            String destinationPath = @rootPath + "uploads\\" + project_id + "\\requestForm\\" + nameDestino;
            String destinationURL = @uploadFolderName + project_id + "/requestForm/" + nameDestino;
            bool showAdvancedForm = false;
            if (HttpContext.Current.Session["user_id"] != null && Convert.ToInt32(HttpContext.Current.Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(HttpContext.Current.Session["user_id"]);

                if (userObj.checkPermission("ver-formulario-gestion-solicitud"))
                {
                    showAdvancedForm = true;
                }
            }
            try
            {
                /* Se verifica si el archivo ya existe en la carpeta y de ser así se elimina
                 * para permitir la carga del nuevo archivo */
                // Ensure that the target does not exist.

                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                /* renombramos el archivo recien cargado */
                File.Move(sourcePath, destinationPath);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            RequestForm form = new RequestForm(project_id);
            if (form.path == "" || form.path == null || form.path != destinationURL)
            {
                form.path = destinationURL;
                form.projectID = project_id;
                form.Save();
            }
            if (showAdvancedForm)
            {
                /* Devuelve el checkbox de aprobacion y el vínculo de acceso al archivo recien cargado */
                return "<input type=\"checkbox\" name='request_form_'" + project_id + " value=\"" + project_id + "\" />" +
                       "<a target=\"_blank\" href=\"" + destinationURL + " \">Formulario de solicitud Firmado</a>";
            }
            else
            {
                /* Devuelve el vínculo de acceso al archivo recien cargado */
                return "<a target=\"_blank\" href=\"" + destinationURL + " \">Formulario de solicitud Firmado</a>";
            }
        }

        public static string proccessHojaTransferencia(int project_id, string filename)
        {
            String rootPath = HttpContext.Current.Server.MapPath("~");

            /* Obtenemos la extensión del archivo cargado */
            string[] uploadedfilenamesplit = filename.Split('.');
            string extension = uploadedfilenamesplit[(uploadedfilenamesplit.Count() - 1)];
            String uploadFolderName = "/uploads/";
            String savepath = @rootPath + "uploads\\" + project_id + "\\HojaTransferencia\\";
            String sourcePath = @rootPath + "uploads\\" + project_id + "\\" + filename;
            string nameDestino = "HojaTransferencia-" + project_id + "-" + DateTime.Now.Ticks.ToString().Substring(8) + ".pdf";
            String destinationPath = @rootPath + "uploads\\" + project_id + "\\HojaTransferencia\\" + nameDestino;
            String destinationURL = @uploadFolderName + project_id + "/HojaTransferencia/" + nameDestino;
            bool showAdvancedForm = false;
            if (HttpContext.Current.Session["user_id"] != null && Convert.ToInt32(HttpContext.Current.Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(HttpContext.Current.Session["user_id"]);

                if (userObj.checkPermission("ver-formulario-gestion-solicitud"))
                {
                    showAdvancedForm = true;
                }
            }
            try
            {
                /* Se verifica si el archivo ya existe en la carpeta y de ser así se elimina
                 * para permitir la carga del nuevo archivo */
                // Ensure that the target does not exist.

                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                /* renombramos el archivo recien cargado */
                File.Move(sourcePath, destinationPath);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            
            
            
            HojaTransferencia form = new HojaTransferencia(project_id);
            if (form.path == "" || form.path == null || form.path != destinationURL)
            {
                form.path = destinationURL;
                form.projectID = project_id;
                form.Save();
            }
            if (showAdvancedForm)
            {
                /* Devuelve el checkbox de aprobacion y el vínculo de acceso al archivo recien cargado */
                return "<a target=\"_blank\" href=\"" + destinationURL + " \">Hoja de Transferencia</a>";
            }
            else
            {
                /* Devuelve el vínculo de acceso al archivo recien cargado */
                return "<a target=\"_blank\" href=\"" + destinationURL + " \">Hoja de Transferencia</a>";
            }
        }



        [WebMethod]
        public static string GetTooltip()
        {
            return "texto para el tooltip";
        }
        [WebMethod()]
        public static bool getDateNotification(int project_id){

            Project project = new Project();
            project.LoadProject(project_id);

            if (project.project_notification_date.ToString() != "")
            {
                return true;
            }
            else {
                return false;
            }
        }
        [WebMethod()]
        public static string getPersonalAttachmentStatus(string pAttachment_id, int idproyecto, string uploadedfilename, string _project_staff_id)
        {
            /* Inicialización de variables */
            int producer_id = 0;
            string sourcePath = "";
            string destinationPath = "";
            string destinationURL = "";
            bool showAdvancedForm = false;
            int project_staff_id = 0;
            int.TryParse(_project_staff_id, out project_staff_id);

            int attachment_id = 0;

            String rootPath = HttpContext.Current.Server.MapPath("~");
            String uploadFolderName = "/uploads/";

            if (HttpContext.Current.Session["user_id"] != null && Convert.ToInt32(HttpContext.Current.Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(HttpContext.Current.Session["user_id"]);

                if (userObj.checkPermission("ver-formulario-gestion-solicitud"))
                {
                    showAdvancedForm = true;
                }
            }

            string[] exploded = pAttachment_id.Split('_');
            attachment_id = Convert.ToInt32(exploded[0]);

            /* Instancia la clase Attachment */
            Attachment attachmentObj = new Attachment();
            attachmentObj.LoadAttachment(attachment_id);
            ProjectAttachment projectAttachmentObj = new ProjectAttachment();

            /* Se valida que se tenga un proyecto en sesión */
            if (idproyecto > 0)
            {
                if (producer_id == 0)
                {
                    projectAttachmentObj.LoadPersonalProjectAttachment(idproyecto, attachment_id,Convert.ToInt32(exploded[1]));
                }
                else
                {
                    projectAttachmentObj.loadAttachmentByFhaterAndProducerId(idproyecto, attachment_id, producer_id);
                }

                /* Obtenemos la extensión del archivo cargado */
                string[] uploadedfilenamesplit = uploadedfilename.Split('.');
                string extension = uploadedfilenamesplit[(uploadedfilenamesplit.Count() - 1)];

                /* Se definen las rutas del archivo de origen y destino */

                //sourcePath = @rootPath + "uploads\\" + idproyecto + "\\" + pAttachment_id + "\\" + uploadedfilename;
                //destinationPath = @rootPath + "uploads\\" + idproyecto + "\\" + pAttachment_id + "\\" + attachmentObj.attachment_machine_name + "." + extension;
                //destinationURL = @uploadFolderName + idproyecto + "/" + pAttachment_id + "/" + attachmentObj.attachment_machine_name + "." + extension;
                string unicName = DateTime.Now.Ticks.ToString().Substring(8);
                sourcePath = @rootPath + "uploads\\" + idproyecto + "\\" + uploadedfilename;
                destinationPath = @rootPath + "uploads\\" + idproyecto + "\\" + pAttachment_id + "\\" +unicName+ attachmentObj.attachment_machine_name + "." + extension;
                destinationURL = @uploadFolderName + idproyecto + "/" + pAttachment_id + "/" + unicName+attachmentObj.attachment_machine_name + "." + extension;

                if (!System.IO.Directory.Exists( System.IO.Path.GetDirectoryName( destinationPath )  ))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationPath));
                }
                
                try
                {
                    /* Se verifica si el archivo ya existe en la carpeta y de ser así se elimina
                     * para permitir la carga del nuevo archivo */
                    // Ensure that the target does not exist.
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }

                    /* renombramos el archivo recien cargado */
                    File.Move(sourcePath, destinationPath);
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }

                /* Se guarda la información del adjunto en la base de datos */
                projectAttachmentObj.project_id = idproyecto;
                if (producer_id != 0)
                {
                    projectAttachmentObj.project_attachement_producer_id = producer_id;
                }
                projectAttachmentObj.attachment = new Attachment(attachment_id);
                projectAttachmentObj.project_attachment_path = destinationURL;
                projectAttachmentObj.project_attachment_project_staff_order = Convert.ToInt32(exploded[1]);
                projectAttachmentObj.project_attachment_date = System.DateTime.Now;
                projectAttachmentObj.nombre_original = uploadedfilename;
                if (project_staff_id !=0)
                projectAttachmentObj.project_staff_id = project_staff_id;
                projectAttachmentObj.Save();

                //Validamos si ya existe un project_attachment_id asignado, si no, volvemos a cargar el proyecto
                //para obtener el nuevo id
                if (projectAttachmentObj.project_attachment_id == 0)
                {
                    projectAttachmentObj.LoadProjectAttachment(idproyecto, attachment_id);
                }

                if (showAdvancedForm)
                {
                    string attachment_checkbox_approved = "<input type=\"checkbox\" name=\"attachment_approved_" + projectAttachmentObj.project_attachment_id + "\" value=\"" + projectAttachmentObj.project_attachment_id + "\"/>";
                    string attachment_file_link = "<a target=\"_blank\" title='"+uploadedfilename+"' href=\"" + destinationURL + "\">" + attachmentObj.attachment_name + "</a>";

                    /* Devuelve el vínculo de acceso al archivo recien cargado */
                    return attachment_checkbox_approved + attachment_file_link;
                }
                else
                {
                    /* Devuelve el vínculo de acceso al archivo recien cargado */
                    return "<a target=\"_blank\"  title='" + uploadedfilename + "' href=\"" + destinationURL + " \">" + attachmentObj.attachment_name + "</a>";
                }
            }
            return "";
        }
        [WebMethod()]
        public static string getAttachmentStatus(string pAttachment_id, int idproyecto, string uploadedfilename, int producer_id = 0)
        {
            /* Inicialización de variables */
            string sourcePath = "";
            string destinationPath = "";
            string destinationURL = "";
            bool showAdvancedForm = false;

            int attachment_id = 0;

            String rootPath = HttpContext.Current.Server.MapPath("~");
            String uploadFolderName = "/uploads/";

            if (HttpContext.Current.Session["user_id"] != null && Convert.ToInt32(HttpContext.Current.Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(HttpContext.Current.Session["user_id"]);

                if (userObj.checkPermission("ver-formulario-gestion-solicitud"))
                {
                    showAdvancedForm = true;
                }                
            }

            /* Valida si el parametro del id del adjunto es numerico caso en el cual se trata como 
             * un adjunto normal de la aplicación, o si tiene una cadena de caracteres se trata como
             * un adjunto de personal (hoja de vida, contrato o cédula)
             */
            if (pAttachment_id.Contains("identification") || pAttachment_id.Contains("cv") || pAttachment_id.Contains("contract"))
            {
                int project_staff_id = 0;
                string project_staff_attachment_type = "";
                string[] attachment_id_part;
                string attachmentName = "";

                if (idproyecto > 0)
                {
                    attachment_id_part =  pAttachment_id.Split(new string[] { "_" }, StringSplitOptions.None);

                    project_staff_id = Convert.ToInt32(attachment_id_part[0]);
                    project_staff_attachment_type = attachment_id_part[1];

                    /* Obtenemos la extensión del archivo cargado */
                    string[] uploadedfilenamesplit = uploadedfilename.Split('.');
                    string extension = uploadedfilenamesplit[(uploadedfilenamesplit.Count() - 1)];

                    /* Se definen las rutas del archivo de origen y destino */

                    if (producer_id != 0)
                    {
                        sourcePath = @rootPath + "uploads\\" + idproyecto + "\\" + producer_id + "\\" + uploadedfilename;
                        destinationPath = @rootPath + "uploads\\" + idproyecto + "\\" + producer_id + "\\" + pAttachment_id + "." + extension;
                        destinationURL = @uploadFolderName + idproyecto + producer_id + "/" + pAttachment_id + "." + extension;
                    }
                    else
                    {
                        sourcePath = @rootPath + "uploads\\" + idproyecto + "\\" + uploadedfilename;
                        destinationPath = @rootPath + "uploads\\" + idproyecto + "\\" + pAttachment_id + "." + extension;
                        destinationURL = @uploadFolderName + "/" + idproyecto + "/" + pAttachment_id + "." + extension;
                    }

                    string unic = DateTime.Now.Ticks.ToString().Substring(8);
                    sourcePath = @rootPath + "uploads\\" + idproyecto + "\\" + uploadedfilename;
                    destinationPath = @rootPath + "uploads\\" + idproyecto + "\\" +unic+ pAttachment_id + "." + extension;
                    destinationURL = @uploadFolderName + idproyecto + "/" + unic+pAttachment_id + "." + extension;

                    try
                    {
                        /* Se verifica si el archivo ya existe en la carpeta y de ser así se elimina
                         * para permitir la carga del nuevo archivo */
                        // Ensure that the target does not exist.
                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }

                        /* renombramos el archivo recien cargado */
                        File.Move(sourcePath, destinationPath);
                    }
                    catch (Exception ex)
                    {
                        return "Error: " + ex.Message;
                    }

                    /* Instancia el objeto que representa la información de una persona en el proyecto */
                    Staff staffObj = new Staff(project_staff_id);
                    
                    //Inicializamos variable para definir el nombre del checkbox
                    string staff_name_checkbox = "";

                    /* Se guarda la ruta al archivo en el campo correspondiente según
                     * el nombre del archivo */
                    switch (project_staff_attachment_type)
                    { 
                        case "identification":
                            staffObj.project_staff_identification_attachment = destinationURL;
                            attachmentName = "Cédula";
                            staff_name_checkbox = "identification_attachment_approved_" + project_staff_id;
                            break;
                        case "cv":
                            staffObj.project_staff_cv_attachment = destinationURL;
                            attachmentName = "Hoja de Vida";
                            staff_name_checkbox = "cv_attachment_approved_" + project_staff_id;
                            break;
                        case "contract":
                            staffObj.project_staff_contract_attachment = destinationURL;
                            attachmentName = "Contrato";
                            staff_name_checkbox = "contract_attachment_approved_" + project_staff_id;
                            break;
                        default:
                            break;
                    }

                    /* Se guarda la información del adjunto en la base de datos */
                    staffObj.Save();

                    if (showAdvancedForm)
                    {
                        /* Devuelve el checkbox de aprobacion y el vínculo de acceso al archivo recien cargado */                                                                                                                           
                        return "<input type=\"checkbox\" name=\"" + staff_name_checkbox + "\" value=\"" + project_staff_id + "\" />" +
                               "<a target=\"_blank\"  title='" + uploadedfilename + "' href=\"" + destinationURL + " \">" + attachmentName + "</a>";
                    }
                    else
                    {
                        /* Devuelve el vínculo de acceso al archivo recien cargado */
                        return "<a target=\"_blank\"  title='" + uploadedfilename + "' href=\"" + destinationURL + " \">" + attachmentName + "</a>";
                    }
                }
            }
            else 
            {
                attachment_id = Convert.ToInt32(pAttachment_id);

                /* Instancia la clase Attachment */
                Attachment attachmentObj = new Attachment();
                attachmentObj.LoadAttachment(attachment_id);
                ProjectAttachment projectAttachmentObj = new ProjectAttachment();

                /* Se valida que se tenga un proyecto en sesión */
                if (idproyecto > 0)
                {
                    if (producer_id == 0)
                    {
                        projectAttachmentObj.LoadProjectAttachment(idproyecto, attachment_id);
                    }
                    else {
                        projectAttachmentObj.loadAttachmentByFhaterAndProducerId(idproyecto,attachment_id,producer_id);
                    }

                    /* Obtenemos la extensión del archivo cargado */
                    string[] uploadedfilenamesplit = uploadedfilename.Split('.');
                    string extension = uploadedfilenamesplit[(uploadedfilenamesplit.Count() - 1)];

                    /* Se definen las rutas del archivo de origen y destino */
                    string unic = DateTime.Now.Ticks.ToString().Substring(8);
                    if (producer_id != 0)
                    {
                        sourcePath = @rootPath + "uploads\\" + idproyecto + "\\" + producer_id + "\\" + uploadedfilename;
                        destinationPath = @rootPath + "uploads\\" + idproyecto + "\\" + producer_id + "\\" +unic+ attachmentObj.attachment_machine_name + "." + extension;
                        destinationURL = @uploadFolderName + idproyecto +"/"+ producer_id + "/" +unic+ attachmentObj.attachment_machine_name + "." + extension;
                    }
                    else {
                        sourcePath = @rootPath + "uploads\\" + idproyecto + "\\" + uploadedfilename;
                        destinationPath = @rootPath + "uploads\\" + idproyecto + "\\" + unic+attachmentObj.attachment_machine_name + "." + extension;
                        destinationURL = @uploadFolderName + idproyecto + "/" +unic+ attachmentObj.attachment_machine_name + "." + extension;                    
                    }
                    
                    try
                    {
                        /* Se verifica si el archivo ya existe en la carpeta y de ser así se elimina
                         * para permitir la carga del nuevo archivo */
                        // Ensure that the target does not exist.
                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }

                        /* renombramos el archivo recien cargado */
                        File.Move(sourcePath, destinationPath);
                    }
                    catch (Exception ex)
                    {
                        return "Error: " + ex.Message;
                    }

                    /* Se guarda la información del adjunto en la base de datos */
                    projectAttachmentObj.project_id = idproyecto;
                    if(producer_id != 0){
                        projectAttachmentObj.project_attachement_producer_id = producer_id;
                    }                    
                    projectAttachmentObj.attachment = new Attachment(attachment_id);
                    projectAttachmentObj.project_attachment_path = destinationURL;
                    projectAttachmentObj.project_attachment_date = System.DateTime.Now;
                    projectAttachmentObj.nombre_original = uploadedfilename;
                    projectAttachmentObj.Save();

                    //Validamos si ya existe un project_attachment_id asignado, si no, volvemos a cargar el proyecto
                    //para obtener el nuevo id
                    if (projectAttachmentObj.project_attachment_id == 0)
                    {
                        projectAttachmentObj.LoadProjectAttachment(idproyecto, attachment_id);
                    }

                    if (showAdvancedForm)
                    {
                        string attachment_checkbox_approved = "<input type=\"checkbox\" name=\"attachment_approved_" + projectAttachmentObj.project_attachment_id + "\" value=\"" + projectAttachmentObj.project_attachment_id + "\"/>";
                        string attachment_file_link = "<a target=\"_blank\"  title='" + uploadedfilename + "' href=\"" + destinationURL + "\">" + attachmentObj.attachment_name + "</a>";
                        
                        /* Devuelve el vínculo de acceso al archivo recien cargado */
                        return attachment_checkbox_approved + attachment_file_link;                               
                    }
                    else
                    {
                        /* Devuelve el vínculo de acceso al archivo recien cargado */
                        return "<a target=\"_blank\"  title='" + uploadedfilename + "' href=\"" + destinationURL + " \">" + attachmentObj.attachment_name + "</a>";
                    }
                }
            }
            
            return "";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string EditLetter(string body = "", string message = "", string greeting = "", string note = "", string letter_prefirma="")
        {
            Letter letter = new Letter();
            
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            /* Obtiene el dataset con el listado de opciones de personal */
            if (message != "" && body != "" && message != "null" && body != "null" && greeting !="" && greeting != "null" && note != "" && note != "null")
            {
                letter.letter_body = body;
                letter.letter_message = message;
                letter.letter_greeting = greeting;
                letter.letter_note = note;
                letter.letter_prefirma = letter_prefirma;
                letter.save();
            }
            letter.LoadLetter();
            List<Letter> letters = new List<Letter>();
            letters.Add(new Letter { letter_id = letter.letter_id, letter_body = letter.letter_body, letter_message = letter.letter_message, letter_greeting = letter.letter_greeting, letter_note = letter.letter_note, letter_prefirma = letter.letter_prefirma });


            string json = TheSerializer.Serialize(letters);
            return json;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse getPersonalPosition(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
        {
            position positionObj = new position();
            DataSet dataSet = new DataSet();
            positionObj.loadTopPositions(pPageSize, pCurrentPage, pSortColumn, pSortOrder);
            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = positionObj.getTopPositions(pPageSize,pCurrentPage,pSortColumn,pSortOrder);

            /* Crea el listado de opciones */
            var position_options = new List<position>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                position position = new position
                {
                    position_id = Convert.ToInt32(row["position_id"]),
                    position_name = row["position_name"].ToString(),
                    position_description = row["position_description"].ToString()
                };
                position_options.Add(position);     
            }

            var a = new JQGridJsonResponse(positionObj.page_count,
                     positionObj.page_index + 1,
                     positionObj.record_count,
                     position_options);
            return a;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridpersonalPosition getPositionDetail(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder, int id)
        {
            position position = new position();
            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de cargos de la opción de personal seleccionada*/
            dataSet = position.getChildPositions(id, pPageSize, pCurrentPage, pSortColumn, pSortOrder);

            var positionList = new List<position>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                position positionChild = new position
                {
                    position_id = Convert.ToInt32(row["position_id"]),
                    position_father_id = Convert.ToInt32(row["position_father_id"]),
                    position_name = row["position_name"].ToString(),
                    position_description = row["position_description"].ToString()
                };

                positionList.Add(positionChild);
            }

            return new JQGridpersonalPosition(position.page_count,
                                              position.page_index + 1,
                                              position.record_count,
                                              positionList);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetStaffOptions(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
        {
            var a = GetStaffOptionsJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder);
            return a;
        }

        internal static JQGridJsonResponse GetStaffOptionsJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder)
        {
            StaffOption staffOptionObj = new StaffOption();
            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = staffOptionObj.getStaffOptionList(pPageSize, pPageNumber, pSortColumn, pSortOrder);
            
            /* Crea el listado de opciones */
            var staff_options = new List<StaffOption>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string hasDomesticDirectorTemp = "";
                string personal_type = "No Seleccionado";
                if (row["staff_option_has_domestic_director"].ToString() == "1")
                {
                    hasDomesticDirectorTemp = "Si";
                }
                else 
                {
                    hasDomesticDirectorTemp = "No";
                }


                if (row["staff_option_personal_option"].ToString() == "1")
                {
                    personal_type = "Acuerdo Iberoamericano";
                }
                else if (row["staff_option_personal_option"].ToString() == "2")
                {
                    personal_type = "Decreto 255";
                }               

                StaffOption staff_option = new StaffOption
                {
                    id = Convert.ToInt32(row["staff_option_id"]),
                    production_type_name = row["production_type_name"].ToString(),
                    project_type_name = row["project_type_name"].ToString(),
                    project_genre_name = row["project_genre_name"].ToString(),
                    has_domestic_director_description = hasDomesticDirectorTemp,
                    description = row["staff_option_description"].ToString(),
                    staff_option_personal_option = personal_type,
                    staff_option_percentage_init = (row["staff_option_percentage_init"].ToString() != "")? Convert.ToInt32(row["staff_option_percentage_init"]):0,
                    staff_option_percentage_end  = (row["staff_option_percentage_end"].ToString() != "")? Convert.ToInt32(row["staff_option_percentage_end"]):0,
                };
                staff_options.Add(staff_option);
            }
 
            var a = new JQGridJsonResponse(staffOptionObj.staff_options_page_count,
                     staffOptionObj.staff_options_page_index + 1,
                     staffOptionObj.staff_options_record_count,
                     staff_options);
            return a;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridStaffOptionDetailJsonResponse GetStaffOptionDetail(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder, int id, string version)
        {
            return GetStaffOptionDetailJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder, id, version);
        }

        internal static JQGridStaffOptionDetailJsonResponse GetStaffOptionDetailJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder, int id, string version)
        {
            StaffOption staffOptionObj = new StaffOption();
            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de cargos de la opción de personal seleccionada*/
            dataSet = staffOptionObj.getStaffOptionDetail(pPageSize, pPageNumber, pSortColumn, pSortOrder, id, version);

            var StaffOptionDetailList = new List<StaffOptionDetail>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                StaffOptionDetail myChild = new StaffOptionDetail();
                myChild.id =  Convert.ToInt32(row["staff_option_detail_id"]);
                myChild.LoadData();

                StaffOptionDetailList.Add(myChild);
            }

            return new JQGridStaffOptionDetailJsonResponse(staffOptionObj.staff_options_page_count,
                                                           staffOptionObj.staff_options_page_index + 1,
                                                           staffOptionObj.staff_options_record_count, 
                                                           StaffOptionDetailList);
        }

        /* Este método permite almacenar los cambios realizados en la tabla de parametrización de opciones de personal */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditStaffOptionDetail(string position_name, string position_qty, string staff_option_id, string oper, string id)
        {
            StaffOptionDetail staffOptionDetailObj = new StaffOptionDetail();

            /* Se definen los valores a almacenar en el objeto */
            staffOptionDetailObj.staff_option_id = Convert.ToInt32(staff_option_id);
            staffOptionDetailObj.position_id = Convert.ToInt32(position_name);
            staffOptionDetailObj.position_qty = Convert.ToInt32(position_qty);

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 || String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                staffOptionDetailObj.id = 0;

                /* Se guarda la información en la base de datos */
                staffOptionDetailObj.Save();
            }
            else if (String.Compare(oper, "edit", StringComparison.Ordinal) == 0)
            {
                staffOptionDetailObj.id = Convert.ToInt32(id);

                /* Se guarda la información en la base de datos */
                staffOptionDetailObj.Save();
            }
            else if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                staffOptionDetailObj.id = Convert.ToInt32(id);

                /* Se guarda la información en la base de datos */
                staffOptionDetailObj.Delete();
            }
       }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelPositionDetail(string oper, string id)
        {
            position position = new position();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                position.position_id = Convert.ToInt32(id);

                /* Se guarda la información en la base de datos */
                position.Delete();
            }
        }
        /* Este método permite almacenar los cambios realizados en la tabla de parametrización de opciones de personal */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelStaffOptionDetail(string oper, string id)
        {
            StaffOptionDetail staffOptionDetailObj = new StaffOptionDetail();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                staffOptionDetailObj.id = Convert.ToInt32(id);

                /* Se guarda la información en la base de datos */
                staffOptionDetailObj.Delete();
            }
        }
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditPositionOption(string position_name, string position_description,string oper,string id)
        {
            position position = new position();
            position.position_name = position_name;
            position.position_description = position_description;
            position.position_father_id = 0;
            if (id != "_empty")
            {
                position.position_id = Convert.ToInt32(id);
            }
            position.save();

        }
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditPositionOptionDetail(string position_name, string position_description,string position_father_id, string id)
        {
            position position = new position();
            position.position_name = position_name;
            position.position_description = position_description;
            position.position_father_id = Convert.ToInt32(position_father_id);
            if (id != "_empty") {
                position.position_id = Convert.ToInt32(id);
            }
            
            position.save();

        }
        /* Este método permite almacenar los cambios realizados en la tabla de parametrización de opciones de personal */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditStaffOption(string production_type_name, string project_type_name, string project_genre_name, string has_domestic_director_description, 
                    string description, string oper, string id,string staff_option_personal_option, string percentage_init,string percentage_end)
        {
            StaffOption staffOptionObj = new StaffOption();

            /* Se definen los valores a almacenar en el objeto */
            staffOptionObj.production_type_id = Convert.ToInt32(production_type_name);
            staffOptionObj.project_type_id = Convert.ToInt32(project_type_name);
            staffOptionObj.project_genre_id = Convert.ToInt32(project_genre_name);
            staffOptionObj.has_domestic_director = Convert.ToInt32(has_domestic_director_description);
            staffOptionObj.description = description;
            staffOptionObj.staff_option_personal_option = (staff_option_personal_option != "")? staff_option_personal_option:"0";
            staffOptionObj.staff_option_percentage_init = (percentage_init != "")? Convert.ToInt32(percentage_init):0;
            staffOptionObj.staff_option_percentage_end = (percentage_end != "" ) ? Convert.ToInt32(percentage_end):0;

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 || String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                staffOptionObj.id = 0;

                /* Se guarda la información en la base de datos */
                staffOptionObj.Save();
            }
            else if (String.Compare(oper, "edit", StringComparison.Ordinal) == 0)
            {
                staffOptionObj.id = Convert.ToInt32(id);

                /* Se guarda la información en la base de datos */
                staffOptionObj.Save();
            }
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelStaffOption(string oper, string id)
        {
            StaffOption staffOptionObj = new StaffOption();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                staffOptionObj.id = Convert.ToInt32(id);

                /* Se guarda la información en la base de datos */
                staffOptionObj.Delete();
            }
        }
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetFormats(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
        {

            Format formatObj = new Format();

            Attachment attachmentObj = new Attachment();

            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = formatObj.getFormats(pPageSize,pCurrentPage,pSortColumn,pSortOrder);

            /* Crea el listado de opciones */
            var format_options = new List<Format>();
            string format_type_name = "";
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (Convert.ToInt32(row["format_type_id"]) == 1) {
                    format_type_name = "Formatos de rodaje";
                }else if(Convert.ToInt32(row["format_type_id"]) == 2){
                    format_type_name = "Formatos de exhibición";
                }
                Format format_option = new Format
                {
                    format_id = Convert.ToInt32(row["format_id"]),
                    format_type_name = format_type_name,
                    format_name = row["format_name"].ToString()
                };
                format_options.Add(format_option);
            }

            return new JQGridJsonResponse(formatObj.format_options_page_count,
                     formatObj.format_options_page_index + 1,
                     formatObj.format_options_record_count,
                     format_options);
        }
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditFormatOption(string format_type_id, string format_name, string oper="", string id="")
        {
            Format formatObj = new Format();

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 || String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                formatObj.format_id = 0;
            }
            else if (String.Compare(oper, "edit", StringComparison.Ordinal) == 0)
            {
                formatObj.LoadFormat(Convert.ToInt32(id));
            }

            /* Se definen los valores a almacenar en el objeto */
            formatObj.format_type_id = Convert.ToInt32(format_type_id);
            formatObj.format_name = format_name;
            formatObj.format_description = format_name;
            formatObj.format_deleted = 0;

            /* Se guarda la información en la base de datos */
            formatObj.Save();
        }
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelFormatOption(string oper = "", string id = "")
        {
            Format formatObj = new Format();
            /* Se definen los valores a almacenar en el objeto */
            formatObj.format_id = Convert.ToInt32(id);
            formatObj.format_deleted = 1;

            /* Se guarda la información en la base de datos */
            formatObj.Delete();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetAttachmentOptions(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
        {
            return GetAttachmentOptionsJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder);
        }

        internal static JQGridJsonResponse GetAttachmentOptionsJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder)
        {
            Attachment attachmentObj = new Attachment();
            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = attachmentObj.getAttachmentList(pPageSize, pPageNumber, pSortColumn, pSortOrder);

            /* Crea el listado de opciones */
            var attachment_options = new List<Attachment>();
            string valueProducer = "";
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (row["attachment_foreing_producer"].ToString() == "1") {
                    valueProducer = "Si";
                }else{
                    valueProducer = "No";
                }
                Attachment attachment_option = new Attachment
                {
                    attachment_id = Convert.ToInt32(row["attachment_id"]),
                    attachment_name = row["attachment_name"].ToString(),
                    attachment_description = row["attachment_description"].ToString(),
                    attachment_section = row["attachment_section"].ToString(),
                    attachment_format = row["attachment_format"].ToString(),
                    attachment_order = Convert.ToInt32(row["attachment_order"].ToString()),
                    attachment_to_foreing_producer = valueProducer
                };
                attachment_options.Add(attachment_option);
            }

            return new JQGridJsonResponse(attachmentObj.attachment_options_page_count,
                     attachmentObj.attachment_options_page_index + 1,
                     attachmentObj.attachment_options_record_count,
                     attachment_options);
        }

        /* Este método permite almacenar los cambios realizados en la tabla de parametrización de opciones de personal */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditAttachmentOption(string attachment_name, string attachment_description, string attachment_section, string attachment_format,
                    string attachment_order, string attachment_foreing_producer, string oper, string id)
        {
            Attachment attachmentObj = new Attachment();

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 || String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                attachmentObj.attachment_id = 0;
            }
            else if (String.Compare(oper, "edit", StringComparison.Ordinal) == 0)
            {
                attachmentObj.LoadAttachment(Convert.ToInt32(id));
            }

            /* Se definen los valores a almacenar en el objeto */
            attachmentObj.attachment_name = attachment_name;
            attachmentObj.attachment_description = attachment_description;
            attachmentObj.attachment_father_id = Convert.ToInt32(attachment_section);
            attachmentObj.attachment_format = attachment_format;
            attachmentObj.attachment_order = Convert.ToInt32(attachment_order);
            attachmentObj.attachment_foreing_producer = Convert.ToInt32(attachment_foreing_producer);

            /* Se guarda la información en la base de datos */
            attachmentObj.Save();
        }

        /* Este método permite eliminar un adjunto de la tabla de adjuntos */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelAttachmentOption(string oper, string id)
        {
            Attachment attachmentObj = new Attachment();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                attachmentObj.attachment_id = Convert.ToInt32(id);

                /* Se guarda la información en la base de datos */
                attachmentObj.Delete();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetRoleOptions(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
        {
            return GetRoleOptionsJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder);
        }

        internal static JQGridJsonResponse GetRoleOptionsJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder)
        {
            Role roleObj = new Role();
            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = roleObj.getRoleList(pPageSize, pPageNumber, pSortColumn, pSortOrder);

            /* Crea el listado de opciones */
            var role_options = new List<Role>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Role role_option = new Role
                {
                    role_id = Convert.ToInt32(row["role_id"]),
                    role_name = row["role_name"].ToString(),
                    role_description = row["role_description"].ToString(),
                    role_deleted = Convert.ToInt32(row["role_deleted"])
                };
                role_options.Add(role_option);
            }

            return new JQGridJsonResponse(roleObj.role_options_page_count,
                     roleObj.role_options_page_index + 1,
                     roleObj.role_options_record_count,
                     role_options);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetAssignedUsers(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder, string id)
        {
            return GetAssignedUsersJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder, id);
        }

        internal static JQGridJsonResponse GetAssignedUsersJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder, string id)
        {
            Role roleObj = new Role();
            DataSet dataSet = new DataSet();

            int role_id = Convert.ToInt32(id);

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = roleObj.getAssignedUsers(pPageSize, pPageNumber, pSortColumn, pSortOrder, role_id);

            /* Crea el listado de opciones */
            var assigned_users = new List<User>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                User assigned_user = new User
                {
                    user_id = Convert.ToInt32(row["idusuario"]),
                    username = row["username"].ToString()
                };
                assigned_users.Add(assigned_user);
            }

            return new JQGridJsonResponse(roleObj.assigned_users_page_count,
                     roleObj.assigned_users_page_index + 1,
                     roleObj.assigned_users_record_count,
                     assigned_users);
        }

        /* Este método permite almacenar los cambios realizados en la tabla de parametrización de la tabla de asignación de roles */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditAssignedUser(string idusuario, string role_id, string oper, string id)
        {
            User userObj = new User();

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 || String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                userObj.assignUserRole(Convert.ToInt32(idusuario), Convert.ToInt32(role_id));
            }
        }

        /* Este método permite eliminar un adjunto de la tabla de adjuntos */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelAssignedUser(string role_id, string oper, string id)
        {
            User userObj = new User();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                userObj.deleteUserRole(Convert.ToInt32(id), Convert.ToInt32(role_id));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetPermissionOptions(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
        {
            return GetPermissionOptionsJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder);
        }

        internal static JQGridJsonResponse GetPermissionOptionsJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder)
        {
            Permission permissionObj = new Permission();
            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = permissionObj.getPermissionList(pPageSize, pPageNumber, pSortColumn, pSortOrder);

            /* Crea el listado de opciones */
            var permission_options = new List<Permission>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Permission permission_option = new Permission
                {
                    permission_id = Convert.ToInt32(row["permission_id"]),
                    permission_name = row["permission_name"].ToString(),
                    permission_description = row["permission_description"].ToString()
                };
                permission_options.Add(permission_option);
            }

            return new JQGridJsonResponse(permissionObj.permission_options_page_count,
                     permissionObj.permission_options_page_index + 1,
                     permissionObj.permission_options_record_count,
                     permission_options);
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetAssignedRoles(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder, string id)
        {
            return GetAssignedRolesJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder, id);
        }

        internal static JQGridJsonResponse GetAssignedRolesJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder, string id)
        {
            Permission permissionObj = new Permission();
            DataSet dataSet = new DataSet();

            int permission_id = Convert.ToInt32(id);

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = permissionObj.getAssignedRoles(pPageSize, pPageNumber, pSortColumn, pSortOrder, permission_id);

            /* Crea el listado de roles */
            var assigned_roles = new List<Role>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Role assigned_role = new Role
                {
                    role_id = Convert.ToInt32(row["role_id"]),
                    role_name = row["role_name"].ToString(),
                    permission_id = Convert.ToInt32(row["permission_id"])
                };
                assigned_roles.Add(assigned_role);
            }

            return new JQGridJsonResponse(permissionObj.assigned_roles_page_count,
                     permissionObj.assigned_roles_page_index + 1,
                     permissionObj.assigned_roles_record_count,
                     assigned_roles);
        }

        /* Este método permite almacenar los cambios realizados en la tabla de parametrización de la tabla de asignación de roles */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditAssignedRole(string role_name, string oper, string id, string permission_id)
        {
            Permission permissionObj = new Permission();

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 || String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                permissionObj.assignRolePermission(Convert.ToInt32(permission_id), Convert.ToInt32(role_name));
            }
        }

        /* Este método permite eliminar un adjunto de la tabla de adjuntos */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelAssignedRole(string permission_id, string oper, string id)
        {
            Permission permissionObj = new Permission();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                permissionObj.deleteRolePermission(Convert.ToInt32(permission_id), Convert.ToInt32(id));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JQGridJsonResponse GetValidationAttachmentOptions(int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
        {
            return GetValidationAttachmentOptionsJSon(pPageSize, pCurrentPage, pSortColumn, pSortOrder);
        }

        internal static JQGridJsonResponse GetValidationAttachmentOptionsJSon(int pPageSize, int pPageNumber, string pSortColumn, string pSortOrder)
        {
            ValidationAttachment validationObj = new ValidationAttachment();
            DataSet dataSet = new DataSet();

            /* Obtiene el dataset con el listado de opciones de personal */
            dataSet = validationObj.getValidationAttachmentList(pPageSize, pPageNumber, pSortColumn, pSortOrder);

            /* Crea el listado de opciones */
            var validationattachment_options = new List<ValidationAttachment>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                ValidationAttachment validationattachment_option = new ValidationAttachment
                {
                    validation_id = Convert.ToInt32(row["validation_id"]),
                    attachment_id = Convert.ToInt32(row["attachment_id"]),
                    attachment_name = row["attachment_name"].ToString(),
                    validation_variable = row["variable"].ToString(),
                    validation_type = row["validation_type"].ToString(),
                    validation_operator = row["operator"].ToString(),
                    validation_value = row["value"].ToString(),
                    active = Convert.ToInt32(row["active"])
                };
                validationattachment_options.Add(validationattachment_option);
            }

            return new JQGridJsonResponse(validationObj.validation_attachment_options_page_count,
                     validationObj.validation_attachment_options_page_index + 1,
                     validationObj.validation_attachment_options_record_count,
                     validationattachment_options);
        }

        /* Este método permite almacenar los cambios realizados en la tabla de parametrización de la tabla de asignación de roles */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void EditValidationAttachmentOption(  string attachment_name,
                                                            string variable,
                                                            string validation_type,
                                                            string value, 
                                                            string validation_operator,
                                                            string active,
                                                            string oper, string id)
        {
            ValidationAttachment validationObj = new ValidationAttachment();

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 || String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                validationObj.validation_id = 0;
            }
            else
            {
                validationObj.validation_id = Convert.ToInt32(id);
            }
            
            validationObj.attachment_id = Convert.ToInt32(attachment_name);
            validationObj.validation_variable = variable;
            validationObj.validation_type = validation_type;
            validationObj.validation_value = value;
            validationObj.validation_operator = validation_operator;
            validationObj.active = Convert.ToInt32(active);
            
            validationObj.Save();
        }

        /* Este método permite eliminar un adjunto de la tabla de adjuntos */
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DelValidationAttachmentOption(string oper, string id)
        {
            ValidationAttachment validationObj = new ValidationAttachment();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                validationObj.validation_id = Convert.ToInt32(id);
                validationObj.Delete();
            }
        }
    }
}