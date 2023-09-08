using CineProducto.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public bool loggeduser = false;
        public int valorsesion = 10000000;
        public bool showAdvancedForm = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(Session["user_id"]);
                int rol = (int)Session["user_role_id"];
                if (!IsPostBack)
                {
                    int user_role = userObj.GetUserRole(userObj.user_id);
                }

                if (userObj.checkPermission("ver-formulario-busqueda-en-listado-solicitudes"))
                {
                    showAdvancedForm = true;
                }
            }
            Session["minutosSession"] = 10;
            if (Session["minutosSession"] != null)
            {
                valorsesion = int.Parse(Session["minutosSession"].ToString()) * 60 * 1000;
            }
        }
    }
}
