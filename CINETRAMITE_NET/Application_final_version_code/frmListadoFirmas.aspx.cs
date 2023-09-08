using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmListadoFirmas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Response.Redirect("frmDetalleFirmas.aspx?cod=" + b.CommandArgument);
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmDetalleFirmas.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            BD.dsCineTableAdapters.firma_tramiteTableAdapter ft = new BD.dsCineTableAdapters.firma_tramiteTableAdapter();
            ft.DeleteQuery(int.Parse(b.CommandArgument));
            grdDatos.DataBind();
        }

    }
}