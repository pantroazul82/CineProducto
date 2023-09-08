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
using System.Globalization;

namespace CineProducto
{
    public partial class HojaControl : System.Web.UI.Page
    {
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

                if (!userObj.checkPermission("ver-formulario-gestion-solicitud"))
                {
                    Response.Redirect("Default.aspx", true);
                }
            }
            /* Define la region */
            CultureInfo culture = new CultureInfo("es-CO");

            /* Obtiene la información del proyecto que se presentará */
            /* Crea el objeto del proyecto para manejar la información */
            Project project = new Project();
            
            /* Crea un dataset para almacenar la informacion del personal que se vinculará al repetidor */
            DataTable dtSection = new DataTable();
            dtSection.Columns.Add("section_name", typeof(string));
            dtSection.Columns.Add("clarification_request", typeof(string));
            dtSection.Columns.Add("aclaraciones_productor", typeof(string));
            dtSection.Columns.Add("initial_observation", typeof(string));

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
            }
            else
            {
                Response.Redirect("Lista.aspx", true);
            }

            /* Buscamos el objeto del productor que hace la solicitud */
            int requesterProducer = project.producer.FindIndex(
                delegate(Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
            if (requesterProducer != -1)
            {
                producer_name.Text = project.producer[requesterProducer].producer_name;

                if (project.producer[requesterProducer].producer_type_id ==1)
                {
                    producer_name.Text = project.producer[requesterProducer].producer_firstname.Trim() + " " + project.producer[requesterProducer].producer_lastname;
                }

                if (producer_name.Text == string.Empty)
                {
                    producer_name.Text = project.producer[requesterProducer].producer_firstname.Trim() + " " + project.producer[requesterProducer].producer_lastname;
                }


            }

            /* Carga la información en la plantilla */
            project_name.Text = project.project_name;
            titulo_anterior.Text = project.titulo_provisional;
            project_type.Text = project.project_type_name.ToString();
            request_date.Text = evaluateDate(project.project_request_date,project.project_request_date.ToLongDateString() + " " + project.project_request_date.ToLongTimeString());
            project_genre.Text = project.project_genre_name.ToString();
            clarification_request_date.Text = evaluateDate(project.project_clarification_request_date,project.project_clarification_request_date.ToLongDateString() + " " + project.project_clarification_request_date.ToLongTimeString());
            production_type.Text = project.production_type_name.ToString();
            clarification_date.Text = evaluateDate(project.project_clarification_response_date,project.project_clarification_response_date.ToLongDateString() + " " + project.project_clarification_response_date.ToLongTimeString());
            project_duration.Text = project.GetProjectDuration("minutes") + " minutos, " + project.GetProjectDuration("seconds") + " segundos.";
            project_resolution_date.Text = evaluateDate(project.project_resolution_date,project.project_resolution_date.ToLongDateString() + " " + project.project_resolution_date.ToLongTimeString());
            project_total_cost_total.Text = string.Format("{0:c}", project.getTotalCost());
            project_total_cost.Text = string.Format("{0:c}", (project.getTotalCost()-project.project_total_cost_promotion) );

            foreach(Format formatItem in project.project_format)
            {
                if(formatItem.format_type_id == 1)
                {
                    project_shooting_formats.Text = project_shooting_formats.Text + formatItem.format_name +" ("+formatItem.format_project_detail+"), ";
                }   
            }

            /* Crea el repetidor con la información de solicitud de aclaraciones de cada formulario */
            DataRow DatosProyectoRow = sectionDS.Tables[0].NewRow();
            DataRow DatosProductorRow = sectionDS.Tables[0].NewRow();
            DataRow DatosProductoresAdicionalesRow = sectionDS.Tables[0].NewRow();
            DataRow DatosFormatoPersonalRow = sectionDS.Tables[0].NewRow();
            DataRow DatosPersonalRow = sectionDS.Tables[0].NewRow();
            DataRow DatosAdjuntosRow = sectionDS.Tables[0].NewRow();
            DataRow DatosFormularioRow = sectionDS.Tables[0].NewRow();
            DataRow DatosVisualizacionRow = sectionDS.Tables[0].NewRow();

           


            DatosProyectoRow["section_name"] = "Datos de la Obra";
            DatosProyectoRow["clarification_request"] = project.sectionDatosProyecto.solicitud_aclaraciones.Replace("\n", "<br>"); ;
            DatosProyectoRow["aclaraciones_productor"] = project.sectionDatosProyecto.aclaraciones_productor.Replace("\n", "<br>"); ;
            DatosProyectoRow["initial_observation"] = project.sectionDatosProyecto.observacion_inicial.Replace("\n", "<br>"); ;

            DatosProductorRow["section_name"] = "Datos del Productor";
            DatosProductorRow["clarification_request"] = project.sectionDatosProductor.solicitud_aclaraciones.Replace("\n", "<br>"); ;
            DatosProductorRow["aclaraciones_productor"] = project.sectionDatosProductor.aclaraciones_productor.Replace("\n", "<br>"); ;
            DatosProductorRow["initial_observation"] = project.sectionDatosProductor.observacion_inicial.Replace("\n", "<br>"); ;

            DatosProductoresAdicionalesRow["section_name"] = "Datos de coproductores";
            DatosProductoresAdicionalesRow["clarification_request"] = project.sectionDatosProductoresAdicionales.solicitud_aclaraciones.Replace("\n", "<br>"); ;
            DatosProductoresAdicionalesRow["aclaraciones_productor"] = project.sectionDatosProductoresAdicionales.aclaraciones_productor.Replace("\n", "<br>"); ;
            DatosProductoresAdicionalesRow["initial_observation"] = project.sectionDatosProductoresAdicionales.observacion_inicial.Replace("\n", "<br>"); ;
   DatosPersonalRow["section_name"] = "Datos de Personal";
            DatosPersonalRow["clarification_request"] = project.sectionDatosPersonal.solicitud_aclaraciones.Replace("\n", "<br>"); ;
            DatosPersonalRow["aclaraciones_productor"] = project.sectionDatosPersonal.aclaraciones_productor.Replace("\n", "<br>"); ;
            DatosPersonalRow["initial_observation"] = project.sectionDatosPersonal.observacion_inicial.Replace("\n", "<br>"); ;

            DatosFormatoPersonalRow["section_name"] = "Datos del Formato Personal";
            DatosFormatoPersonalRow["clarification_request"] = project.sectionDatosFormatoPersonal.solicitud_aclaraciones.Replace("\n", "<br>"); ;
            DatosFormatoPersonalRow["aclaraciones_productor"] = project.sectionDatosFormatoPersonal.aclaraciones_productor.Replace("\n", "<br>"); ;
            DatosFormatoPersonalRow["initial_observation"] = project.sectionDatosFormatoPersonal.observacion_inicial.Replace("\n", "<br>"); ;

            Texto_adicional_carta_de_aclaraciones.Text = project.complemento_carta_aclaraciones.Replace("\n", "<br>"); ;
            Texto_sustituto_carta_de_aclaraciones.Text= project.sustituto_carta_aclaracion.Replace("\n", "<br>");           
            

            DatosFormularioRow["section_name"] = "Datos de formulario de solicitud";
            string observacionesFormulario = "";
            if (project.state_id < 4)
            {
                observacionesFormulario = (project.formulario_aprobado_pronda.HasValue && project.formulario_aprobado_pronda.Value) ? "Formulario de solicitud aprobado" : "Formulario de solicitud debe descargarse y adjuntarse nuevamente.";
            }
            else
            {
                observacionesFormulario = (project.formulario_aprobado_sronda.HasValue && project.formulario_aprobado_sronda.Value) ? "Formulario de solicitud aprobado" : "Formulario de solicitud debe descargarse y adjuntarse nuevamente.";
            }
            DatosFormularioRow["clarification_request"] = observacionesFormulario;

            DatosVisualizacionRow["section_name"] = "Datos de visualización del proyecto cinematográfico";
            DatosVisualizacionRow["clarification_request"] = project.project_result_film_view;
            DatosVisualizacionRow["aclaraciones_productor"] = project.observaciones_visualizacion_por_productor.Replace("\n","<br>");
            //DatosVisualizacionRow["initial_observation"] = project.sectionDatosPersonal.observacion_inicial;

            /**
             * Eliminado en ajustes de 2014
             * */
            //DatosAdjuntosRow["section_name"] = "Datos Adjuntos";
            //DatosAdjuntosRow["clarification_request"] = project.sectionDatosAdjuntos.solicitud_aclaraciones;
            //DatosAdjuntosRow["initial_observation"] = project.sectionDatosAdjuntos.observacion_inicial;

            sectionDS.Tables[0].Rows.Add(DatosProyectoRow);
            sectionDS.Tables[0].Rows.Add(DatosProductorRow);
           // sectionDS.Tables[0].Rows.Add(DatosFormatoPersonalRow);
            sectionDS.Tables[0].Rows.Add(DatosProductoresAdicionalesRow);
            sectionDS.Tables[0].Rows.Add(DatosPersonalRow);
            //sectionDS.Tables[0].Rows.Add(DatosFormularioRow);
            sectionDS.Tables[0].Rows.Add(DatosVisualizacionRow);
            //sectionDS.Tables[0].Rows.Add(DatosAdjuntosRow);

            SectionRepeater.DataSource = sectionDS;
            SectionRepeater.DataBind();
        }
        private string evaluateDate(DateTime date, string dateString)
        {
            long dateTime = ConvertToTimestamp(date);
            if (dateTime != -62135578800)
            {
                return dateString;
            }
            return "";
        }
        private long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
    }
}
