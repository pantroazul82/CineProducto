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

namespace CineProducto
{
    public partial class Nuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            tooltip_project_name.Text = db.GetTooltip("project_name");
            if (tooltip_project_name.Text.Trim() == string.Empty) tooltip_project_name.Visible = false;



            /* Verifica si el usuario ya está autenticado, y si no es así lo redirige a la 
             * página inicial del trámite para que se autentique o cree una cuenta. */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                /* Instancia el objeto que permitirá la creación del proyecto */
                Project project = new Project();

                /* Se verifica si se envió el formulario de creación */
                if (Request.Form["submit_new_project"] != null && Request.Form["project_name"] != "")
                {
                    //validamos si el productor tiene un proyecto creado con el mismo nombre y esta en solicitud de aclaracion
                    int id=project.validarNombreProyecto(Request.Form["project_name"].ToString(), Session["user_id"].ToString());
                    if ( id != 0)
                    {
                        Response.Redirect("DatosProyecto.aspx?project_id=" + id);
                        return;
                    }


                    project.project_name = Request.Form["project_name"].ToUpper();
                    project.project_idusuario = Convert.ToInt32(Session["user_id"]);
                    Producer producer = new Producer();
                    if (!project.Save(true))
                    {
                        message.Text = "<div class=\"error\">No fue posible crear la nueva solicitud</div>";
                        return;
                    }
                    if (producer.isUser((int)Session["user_id"]))
                    {
                        producer.LoadProducerByUserClon((int)Session["user_id"]);
                        producer.requester = 1;

                        if (producer.producer_country == "")
                        {
                            project.project_domestic_producer_qty = 1;
                        }
                        else {
                            project.project_foreign_producer_qty = 1;
                        }
                        project.producer = new List<Producer>();
                        project.producer.Add(producer);

                    }
                    if (project.Save())
                    {
                        Session["project_id"] = project.project_id;
                        Response.Redirect("DatosProyecto.aspx", true);
                    }
                    else 
                    {
                        message.Text = "<div class=\"error\">No fue posible crear la nueva solicitud</div>";
                    }
                }
            }
            else
            {
                Response.Redirect("Default.aspx", true);
            }
        }

        [WebMethod()]
        public static ArrayList obtenerMunicipios(string departamento)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            ArrayList elementos = new ArrayList();

            /* Obtiene las opciones para el select de departamentos */
            DataSet departamentoDS = db.GetSelectOptions("localization", "localization_id", "localization_name", "localization_father_id='" + departamento  + "'");
 
            for (int i = 0; i < departamentoDS.Tables[0].Rows.Count; i++)
            {
                elementos.Add(new ListItem(departamentoDS.Tables[0].Rows[i]["localization_name"].ToString(), departamentoDS.Tables[0].Rows[i]["localization_id"].ToString()));
            }

            return elementos;
        }

        [WebMethod]
        public static string GetTooltip()
        {
            return "texto para el tooltip";
        }

    }
}
