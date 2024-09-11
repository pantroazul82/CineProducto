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
using System.Net;
using System.Text;

namespace CineProducto
{
    public partial class SolicitudAclaraciones : System.Web.UI.Page
    {
        public int project_id; //Variable que hace disponible el id del proyecto actual en la plantilla
        public string solicitud_aclaracion;
        public string sustituto_aclaracion;
        public string texto_adicional;
        public string director_cinematografia;
        public string cargo_director;
        public string director_cinematografia_2;
        public string cargo_asistente;
        public string body;
        public string message_footer;
        public string note;
        public string fecha;
        public string greeting;
        public string message_prefirma;
        public string cartaGeneradaHistorico;

        protected void Page_Load(object sender, EventArgs e)
        {
            /* Valida si hay un usuario en sesión de lo contrarió redirecciona a la 
            * página principal */
            if (Session["user_id"] == null /*|| Convert.ToInt32(Session["user_id"]) <= 1*/)
            {
                Response.Redirect("Default.aspx", true);
            }

            /* Valida si el usuario tiene permiso de ver el formulario de administración */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                ///* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                //User userObj = new User();
                //userObj.user_id = Convert.ToInt32(Session["user_id"]);

                //if (!userObj.checkPermission("ver-formulario-gestion-solicitud"))
                //{
                //    Response.Redirect("Default.aspx", true);
                //}
            }

            /* Obtiene la información del proyecto que se presentará */

            /* Crea el objeto del proyecto para manejar la información */
            Project project = new Project();
 
            /* Crea un dataset para almacenar la informacion del personal que se vinculará al repetidor */
            DataTable dtSection = new DataTable();
            dtSection.Columns.Add("section_name", typeof(string));
            dtSection.Columns.Add("clarification_request", typeof(string));
            Letter letter = new Letter();
            letter.LoadLetter();
            this.body = letter.letter_body;
            this.message_footer = letter.letter_message;
            this.message_prefirma = letter.letter_prefirma;
            this.note = letter.letter_note;
            this.greeting = letter.letter_greeting;
            DataSet sectionDS = new DataSet();
            sectionDS.Tables.Add(dtSection);

            /* Obtiene la información del proyecto que esté en sesión */
            if (Request.QueryString["project_id"] != null)
            {
                Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
            }

            if (Session["project_id"] != null)
            {
                project.LoadProject(Convert.ToInt32(Session["project_id"]));

                if(project.carta_aclaraciones_generada != null && project.carta_aclaraciones_generada != string.Empty)
                {
                    cartaGeneradaHistorico = project.carta_aclaraciones_generada;
                    return;
                }else{
                    cartaGeneradaHistorico = generarHtml(project);
                    //validamos si ya esta en un estado que debio actualizar la carta
                    if (project.state_id >= 5)
                    {
                        project.carta_aclaraciones_generada = cartaGeneradaHistorico;
                        project.Save();
                    }
                }

                return;
            }
            else
            {
                Response.Redirect("Lista.aspx", true);
            }
        }

        public string generarHtml(Project project) 
        {
          if(project.carta_aclaraciones_generada != null && project.carta_aclaraciones_generada != string.Empty)
                {
                    return  project.carta_aclaraciones_generada;
                }
            string res = "";
            
            //res = res + "<br><br>Solicitud de Aclaraciones"  ;
            if (project.sustituto_carta_aclaracion != null && project.sustituto_carta_aclaracion != string.Empty)
            {
                res = project.sustituto_carta_aclaracion.Replace("\r\n","<br>");
            }else{
            
            Letter letter = new Letter();
            letter.LoadLetter();
            if (project.project_clarification_request_date != null && project.project_clarification_request_date.Year > 2000)
            {
                res = project.project_clarification_request_date.ToLongDateString();
            } else {
                res = DateTime.Now.ToLongDateString();
            }
                res +=  ("<br>"+letter.letter_greeting);


            string producerName = "";

            /* Buscamos el objeto del productor que hace la solicitud */
            int requesterProducer = project.producer.FindIndex(
                        delegate(Producer producerObj)
                        {
                            return producerObj.requester == 1;
                        });
            if (requesterProducer != -1)
                        {

                            if (project.producer[requesterProducer].person_type_id == 2)
                                producerName = project.producer[requesterProducer].producer_name;
                            else
                                producerName = project.producer[requesterProducer].producer_firstname.Trim() + " " + project.producer[requesterProducer].producer_lastname.Trim();


                            if (producerName.Trim() == string.Empty)
                            {
                                producerName = project.producer[requesterProducer].producer_firstname.Trim() + " " + project.producer[requesterProducer].producer_lastname.Trim();
                            }

                            if (producerName == string.Empty)
                            {
                                producerName = project.producer[requesterProducer].producer_email.Trim();
                            }
                        }



             res = res + "<br>" + producerName;
                res = res + "<br><br>" + letter.letter_body.Replace("\r\n", "<br>");

                res = res + "<br><br><h2>" + "Título: " + project.project_name + "</h2>";

                if (project.complemento_carta_aclaraciones != null && project.complemento_carta_aclaraciones.Trim() != string.Empty)
                {
                    res = res + project.complemento_carta_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;");
                    res = res + @"</br>";
                    res = res + @"Adicionalmente, le informamos también que:";
                    res = res + @"</br>";

                }

                if (project.sectionDatosProyecto.solicitud_aclaraciones.Trim() != string.Empty)
                {
                    res = res + "<br><br><b>" + "Datos de la obra</b>";
                    res = res + "<br>" + project.sectionDatosProyecto.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;"); ;
                }

                if (project.sectionDatosProductor.solicitud_aclaraciones.Trim() != string.Empty)
                {
                    res = res + "<br><br><b>" + "Datos del Productor</b>";
                    res = res + "<br>" + project.sectionDatosProductor.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;"); ;
                }


                if (project.sectionDatosProductoresAdicionales.solicitud_aclaraciones.Trim() != string.Empty)
                {
                    res = res + "<br><br><b>" + "Datos de coproductores</b>";
                    res = res + "<br>" + project.sectionDatosProductoresAdicionales.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;"); ;
                }

                if (project.sectionDatosPersonal.solicitud_aclaraciones.Trim() != string.Empty)
                {
                    res = res + "<br><br><b>" + "Datos de Personal</b>";
                    res = res + "<br>" + project.sectionDatosPersonal.solicitud_aclaraciones.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;"); ;
                }


                #region  observaciones formulario
                string tobservacionesFormulario = "<br><br><b>" + "Datos de formulario de solicitud</b>";
                string observacionesFormulario = "";
                //if (project.state_id < 4)
                //{
                if(project.formulario_aprobado_pronda.HasValue)
                observacionesFormulario = (project.formulario_aprobado_pronda.Value) ? "" : "El formulario de solicitud debe descargarse y adjuntarse nuevamente.";
                //}
                //else
                //{
                //    //si fue aprobado en la primera ronda
                //    if (project.formulario_aprobado_pronda.HasValue && project.formulario_aprobado_pronda.Value)
                //    {
                //        observacionesFormulario = "";
                //    }else{
                //        observacionesFormulario = (project.formulario_aprobado_sronda.HasValue && project.formulario_aprobado_sronda.Value) ? "" : "Formulario de solicitud debe descargarse y adjuntarse nuevamente.";
                //    }
                //}

                if (observacionesFormulario != string.Empty)
                {
                    res = res + tobservacionesFormulario + "<br>" + observacionesFormulario;
                }
                #endregion

                bool mostrarVisualizacion = false;
                if (project.state_id <= 5)
                {
                    if (project.aprueba_visualizacion_proyecto_pronda.HasValue &&
                        project.aprueba_visualizacion_proyecto_pronda.Value == false)
                    {
                        mostrarVisualizacion = true;
                    }
                }
                else
                {
                    if (
                        (project.aprueba_visualizacion_proyecto_pronda.HasValue &&
                        project.aprueba_visualizacion_proyecto_pronda.Value == false &&
                        project.aprueba_visualizacion_proyecto_sronda.HasValue == false
                        ) ||
                        (project.aprueba_visualizacion_proyecto_sronda.HasValue &&
                        project.aprueba_visualizacion_proyecto_sronda.Value == false)
                        )
                    {
                        mostrarVisualizacion = true;
                    }
                }

                if (project.project_result_film_view != string.Empty && mostrarVisualizacion)
                {
                    res = res + "<br><br>" + "<b>Observaciones visualización de la obra</b>";
                    res = res + "<br>" + project.project_result_film_view.Replace("\r\n", "<br>").Replace("\t", "&nbsp;&nbsp;");
                }

                if (project.project_clarification_request_additional_text != null && project.project_clarification_request_additional_text != "")
                {
                    res = res + "<br><br><p>" + project.project_clarification_request_additional_text + "</p>";
                }

                res = res + "<br><br><p style='text-align:justify;'>" + letter.letter_prefirma.Replace("\r\n","<br>")+"</p>";

                
                res = res + "<br><br>" + letter.letter_message.Replace("\r\n", "<br>");

                
                if (project.responsable.HasValue == false)
                {
                    BD.dsCine ds = new BD.dsCine();
                    BD.dsCineTableAdapters.firma_tramiteTableAdapter firma = new BD.dsCineTableAdapters.firma_tramiteTableAdapter();
                    firma.FillByActivo(ds.firma_tramite);
                    if (ds.firma_tramite.Count <= 0)
                    {//si no hay ningun activo 
                        firma.Fill(ds.firma_tramite);
                    }

                    res = res + ("<p><strong>" + ds.firma_tramite[0].nombre_firma_tramite + "<br/>" +
                        ds.firma_tramite[0].cargo_firma_tramite + "<br /><br />" + "</strong></p>");
                }
                else {
                    BD.dsCine ds = new BD.dsCine();
                    BD.dsCineTableAdapters.usuarioTableAdapter usr = new BD.dsCineTableAdapters.usuarioTableAdapter();
                    usr.FillByidusuario(ds.usuario,project.responsable.Value);
                  

                    res = res + ("<p><strong>" + ds.usuario[0].nombres+" "+ ds.usuario[0].apellidos +  "<br/>" +
                       ds.usuario[0].email + "<br /><br />" + "</strong></p>");
                }

                if (letter.letter_note.Trim() != string.Empty)
                {
                    res = res + "<br><br><br><br>" + letter.letter_note;
                }

            }
            return res;
        }

    }
}
