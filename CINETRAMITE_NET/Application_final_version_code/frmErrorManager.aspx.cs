using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmErrorManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["er"] != null)
                {
                    try
                    {
                        string ruta = Server.MapPath("~/logs/");
                        ruta = ruta + Request.QueryString["er"].Trim() + ".log";
                        string error = System.IO.File.ReadAllText(ruta);
                        lblError.Text = error.Replace("@%@", "<br><br>");
                    }catch(Exception){
                        lblError.Text = "Ocurrio un problema en la apicacion codigo del error " + Request.QueryString["er"].Trim();
                    }
                }
            }
        }
    }
}