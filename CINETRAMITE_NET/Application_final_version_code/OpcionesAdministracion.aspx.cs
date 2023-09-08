using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CineProducto.Bussines;
using System.Data;

namespace CineProducto
{
    public partial class OpcionesAdministracion : System.Web.UI.Page
    {
        public string currentForm = "";
        public string versionFiltro = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (Request.QueryString["form"] != null && Request.QueryString["form"] != "")
            {
                this.currentForm = Request.QueryString["form"];
                switch (Request.QueryString["form"])
                {
                    case "configuraciongeneral":
                        break;
                    case "modificaciontextosayuda":
                        break;
                    default:
                        break;
                }
            }
            if (!IsPostBack) {
                if (Session["versionFiltro"] != null)
                {
                    this.versionFiltro = Session["versionFiltro"].ToString();
                    cmbVersion2.SelectedValue = versionFiltro;
                }
            }
            
            
        }

        protected void cmbVersion2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["versionFiltro"] = cmbVersion2.SelectedValue;
            Response.Redirect("OpcionesAdministracion.aspx?form=administracionopcionespersonal");
        }
    }
}