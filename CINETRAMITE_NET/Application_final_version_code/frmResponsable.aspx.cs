using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CineProducto.Bussines;
using System.Globalization;

namespace CineProducto
{
    public partial class frmResponsable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {            
                if (Request.QueryString["project_id"] != null)
                {
                    Session["project_id"] = Convert.ToInt32(Request.QueryString["project_id"]);
                }
                else {
                    Response.Redirect("Lista.aspx");
                }
                /* Define la region */
                CultureInfo culture = new CultureInfo("es-CO");

                /* Hace disponible la conexión a la base de datos */
                DB db = new DB();
                /* Crea el objeto del proyecto para manejar la información */
                Project project = new Project();
                project.LoadProject(Convert.ToInt32(Session["project_id"]));
                lblProjectId.Text = Session["project_id"].ToString();
                //project.Save();//para probar el calendario
                cargarDatosProyecto(project);

                if (project.state_id >= 9){
                    btnGuardar.Visible = false;
                    lblMsgEstado.Visible = true;
                }
                
            }
        }

        public void cargarDatosProyecto(Project project) {
            
            lblNombre.Text = project.project_name;
            lblSinopsis.Text = project.project_synopsis;
            lblDuracion.Text = project.project_duration.ToString();
            lblTipo.Text = project.production_type_name;
            lblGenero.Text = project.project_genre_name;

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedValue == "-1")
            {
                lblError.Text = "Debe escoger el usuario responsable";
                return;
            }

            User userObj = new User();
            userObj.user_id = Convert.ToInt32(Session["user_id"]);
            int rol = (int)Session["user_role_id"];

            DB db = new DB();

            string responsable = cmbUsuario.SelectedValue;
            string project_id = lblProjectId.Text;
            string asignado_por = userObj.user_id.ToString();
            string insertProjectResp = "INSERT INTO project_responsable (fecha, responsable, project_id, asignado_por) "
                                      + " VALUES (GETDATE()," + responsable + "," + project_id + ","+ asignado_por +")";
            string updateProjectResp = "update project set responsable = "+responsable+" where project_id="+project_id;

            db.Execute(insertProjectResp);
            db.Execute(updateProjectResp);

            grdDevDatos.DataBind();
            lblError.Text = "El responsable se asigno correctamente";
        }
    }
}