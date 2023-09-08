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

namespace CineProducto
{
    public partial class Web : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Variable que almacena el resultado */
            string resultado = "";

            /* Verificamos si existe un usuario autenticado o de lo contrario lo redirigimos a la página inicial */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(Session["user_id"]);

                if (!userObj.checkPermission("administrar-sistema"))
                {
                    Response.Redirect("Default.aspx", true);
                }
            }
            else
            {
                Response.Redirect("Default.aspx", true);
            }

            if (Request.QueryString["method"] != null && Request.QueryString["method"] != "")
            {
                switch (Request.QueryString["method"])
                {
                    case "GetPositionsSelect":
                        Staff staffObj = new Staff();

                        resultado = "<select>";

                        DataSet topPositions = staffObj.getTopPositions();
                        foreach (DataRow row in topPositions.Tables[0].Rows)
                        {
                            resultado = resultado + "<option value='" + row["position_id"] + "'>" + row["position_name"] + "</option> ";
                        }
                        resultado = resultado + "</select>";

                        MainLabel.Text = resultado;
                        break;
                    case "GetProjectTypesSelect":
                        ProjectType projectTypeObj = new ProjectType();

                        resultado = "<select>";

                        DataSet projectTypes = projectTypeObj.getProjectTypes();
                        foreach (DataRow row in projectTypes.Tables[0].Rows)
                        {
                            resultado = resultado + "<option value='" + row["project_type_id"] + "'>" + row["project_type_name"] + "</option> ";
                        }
                        resultado = resultado + "</select>";

                        MainLabel.Text = resultado;
                        break;
                    case "GetGenresSelect":
                        ProjectGenre projectGenreObj = new ProjectGenre();

                        resultado = "<select>";

                        DataSet projectGenres = projectGenreObj.getProjectGenres();
                        foreach (DataRow row in projectGenres.Tables[0].Rows)
                        {
                            resultado = resultado + "<option value='" + row["project_genre_id"] + "'>" + row["project_genre_name"] + "</option> ";
                        }
                        resultado = resultado + "</select>";

                        MainLabel.Text = resultado;
                        break;
                    case "GetAttachmentsSelect":
                        Attachment attachmentObj = new Attachment();

                        resultado = "<select>";

                        DataSet Attachments = attachmentObj.getAttachmentList(100,1,"attachment_name","desc");
                        foreach (DataRow row in Attachments.Tables[0].Rows)
                        {
                            resultado = resultado + "<option value='" + row["attachment_id"] + "'>" + row["attachment_name"] + "</option> ";
                        }
                        resultado = resultado + "</select>";

                        MainLabel.Text = resultado;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
